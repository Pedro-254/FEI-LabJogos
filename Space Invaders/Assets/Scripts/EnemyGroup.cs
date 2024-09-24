using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGroup : MonoBehaviour
{
    //____ Movimentacao____
    public float moveSpeed = 2f;
    public float moveDownAmount = 0.5f;
    public float speedIncrement = 0.2f;
    private Vector3 direction = Vector3.right;  // Direção inicial
    public bool isChangingDirection = false; // Impede a troca de direção múltiplas vezes

    //____Tiro____
    public GameObject firePointPrefab; 
    public GameObject projectilePrefab;
    private Dictionary<GameObject, GameObject> firePoints = new Dictionary<GameObject, GameObject>();  // Mapa de inimigos com seus FirePoints
    private List<GameObject> enemiesWithFirePoints = new List<GameObject>();  // Lista de inimigos com FirePoints
    public float firePointDistance = 0.7f;
    public float projectileSpeed = 10f; 
    public float FireDelay = 1f;

    //____Posicionamento____
    public float columnThreshold = 0.5f;  // Distância mínima para considerar inimigos em diferentes colunas
    public List<GameObject> enemies;  // Lista de inimigos no grupo
    private Dictionary<float, GameObject> bottomEnemiesByColumn = new Dictionary<float, GameObject>();  // Inimigos mais baixos por coluna



    void Start()
    {
        // Rotina de Disparo
        StartCoroutine(FireProjectileRoutine());
    }

    void Update()
    {
        // Movimentar o grupo de inimigos na direção atual
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (enemies == null || enemies.Count == 0)
        {
            return;
        }

        // Atualiza os inimigos mais baixos de cada coluna
        UpdateBottomEnemiesInEachColumn();
    }

    // Função que será chamada para mover o grupo para baixo e inverter a direção
    public void MoveDownAndChangeDirection()
    {
        if (isChangingDirection) return;

        isChangingDirection = true;

        transform.position += Vector3.down * moveDownAmount;

        moveSpeed += speedIncrement;

        // Inverter direção
        direction = (direction == Vector3.right) ? Vector3.left : Vector3.right;

        Invoke(nameof(ResetDirectionFlag), 0.1f);
    }

    private void ResetDirectionFlag()
    {
        isChangingDirection = false;
    }

    // Método para atualizar os inimigos mais baixos em cada coluna e mover os FirePoints
    void UpdateBottomEnemiesInEachColumn()
    {
        Dictionary<float, GameObject> newBottomEnemiesByColumn = new Dictionary<float, GameObject>();

        foreach (GameObject enemy in enemies)
        {
            // if (enemy == null)
            // {
            //     continue;  // Ignora inimigos destruídos
            // }

            float enemyX = enemy.transform.position.x;
            float enemyY = enemy.transform.position.y;

            // Encontra a coluna mais próxima (considerando uma margem de tolerância)
            bool columnFound = false;
            foreach (float columnX in newBottomEnemiesByColumn.Keys)
            {
                if (Mathf.Abs(enemyX - columnX) < columnThreshold)
                {
                    // Já existe uma coluna próxima, verificamos qual inimigo está mais embaixo
                    if (enemyY < newBottomEnemiesByColumn[columnX].transform.position.y)
                    {
                        newBottomEnemiesByColumn[columnX] = enemy;  // Atualiza o inimigo mais baixo na coluna
                    }
                    columnFound = true;
                    break;
                }
            }

            // Se não encontramos uma coluna próxima, criamos uma nova
            if (!columnFound)
            {
                newBottomEnemiesByColumn[enemyX] = enemy;
            }
        }

        // Atualiza os FirePoints para os novos inimigos mais baixos
        foreach (var enemyEntry in newBottomEnemiesByColumn)
        {
            float columnX = enemyEntry.Key;
            GameObject newBottomEnemy = enemyEntry.Value;

            if (newBottomEnemy != null)  // Verifica se o novo inimigo ainda existe
            {
                // Verifica se a coluna já tinha um inimigo mais baixo antes
                if (bottomEnemiesByColumn.ContainsKey(columnX))
                {
                    GameObject currentBottomEnemy = bottomEnemiesByColumn[columnX];

                    // Se o novo inimigo mais baixo mudou, transfere o FirePoint
                    if (currentBottomEnemy != newBottomEnemy)
                    {
                        MoveFirePoint(currentBottomEnemy, newBottomEnemy);
                    }
                }
                else
                {
                    // Não havia inimigo antes, cria um novo FirePoint para o novo inimigo mais baixo
                    CreateFirePoint(newBottomEnemy);
                }
            }
        }

        // Atualiza o dicionário com os novos inimigos mais baixos
        bottomEnemiesByColumn = newBottomEnemiesByColumn;
    }

    // Método para mover o FirePoint de um inimigo para outro
    void MoveFirePoint(GameObject oldEnemy, GameObject newEnemy)
    {
        if (firePoints.ContainsKey(oldEnemy))
        {
            // Move o FirePoint para o novo inimigo
            GameObject firePoint = firePoints[oldEnemy];

            if (firePoint != null)  // Verifica se o FirePoint ainda existe
            {
                firePoint.transform.SetParent(newEnemy.transform);
                firePoint.transform.localPosition = Vector3.zero;  // Posiciona o FirePoint no centro do novo inimigo

                // Atualiza o dicionário para refletir o novo dono do FirePoint
                firePoints.Remove(oldEnemy);
                firePoints[newEnemy] = firePoint;

                // Atualiza a lista de inimigos com FirePoints
                enemiesWithFirePoints.Remove(oldEnemy);
                enemiesWithFirePoints.Add(newEnemy);
            }
        }
    }

    // Método para criar um novo FirePoint para um inimigo
    void CreateFirePoint(GameObject enemy)
    {
        if (!firePoints.ContainsKey(enemy))
        {
            // Cria um novo FirePoint e o associa ao inimigo
            GameObject firePoint = Instantiate(firePointPrefab, enemy.transform.position, Quaternion.identity, enemy.transform);
            firePoint.transform.localPosition = new Vector3(0, -firePointDistance, 0);  // Garante que o FirePoint esteja centralizado no inimigo
            firePoints[enemy] = firePoint;

            // Adiciona o inimigo à lista de inimigos com FirePoints
            enemiesWithFirePoints.Add(enemy);
        }
    }

    // Método para remover o inimigo destruído da lista e transferir o FirePoint
    public void OnEnemyDestroyed(GameObject destroyedEnemy)
    {
        if (firePoints.ContainsKey(destroyedEnemy))
        {
            GameObject firePoint = firePoints[destroyedEnemy];

            if (firePoint != null)  
            {
                Destroy(firePoint);  // Remove o FirePoint
            }

            firePoints.Remove(destroyedEnemy);    // Remove da lista de FirePoints
        }

        // Remove da lista de inimigos com FirePoints
        if (enemiesWithFirePoints.Contains(destroyedEnemy))
        {
            enemiesWithFirePoints.Remove(destroyedEnemy);
        }
        Debug.Log("Inimigo destruído!");
        ScoreManager.AddScore(10);


        enemies.Remove(destroyedEnemy);  // Remove o inimigo da lista

        // Atualiza o novo inimigo mais baixo
        UpdateBottomEnemiesInEachColumn();
    }


    IEnumerator FireProjectileRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(FireDelay);  // Dispara a cada 3 segundos

            if (enemiesWithFirePoints.Count > 0)
            {
                // Escolhe um inimigo aleatório que tenha um FirePoint
                int randomIndex = Random.Range(0, enemiesWithFirePoints.Count);
                GameObject randomEnemy = enemiesWithFirePoints[randomIndex];

                // Verifica se o inimigo ainda existe
                if (randomEnemy != null && firePoints.ContainsKey(randomEnemy))
                {
                    GameObject firePoint = firePoints[randomEnemy];

                    // Verifica se o FirePoint ainda existe
                    if (firePoint != null)
                    {
                        Debug.Log(randomEnemy.name + " atirou!"); // Log do disparo
                        FireProjectile(firePoint.transform);
                    }
                    else
                    {
                        Debug.LogWarning("FirePoint não existe para " + randomEnemy.name);
                        enemiesWithFirePoints.RemoveAt(randomIndex);  // Remove o inimigo da lista
                    }
                }
                else
                {
                    Debug.LogWarning("Inimigo aleatório não existe ou não tem FirePoint!");
                    enemiesWithFirePoints.RemoveAt(randomIndex);  // Remove o inimigo da lista
                }
            }
            else
            {
                Debug.LogWarning("Nenhum inimigo com FirePoints disponível para atirar!");
            }
        }
    }


    // Método para instanciar e disparar o projétil
    void FireProjectile(Transform firePoint)
    {
        // Instancia o projétil na posição do FirePoint
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Adiciona uma força para o projétil se mover para baixo
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = -firePoint.up * projectileSpeed; 
    }
}
