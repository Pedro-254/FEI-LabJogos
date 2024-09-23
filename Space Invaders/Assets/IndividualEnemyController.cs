using System.Diagnostics;
using UnityEngine;

public class IndividualEnemyController : MonoBehaviour
{
    private EnemyGroup enemyGroup;  // Referência ao grupo de inimigos

    void Start()
    {
        // Encontra o script "EnemyGroup" no objeto pai
        enemyGroup = GetComponentInParent<EnemyGroup>();
    }

    void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Wall"))
        {
            enemyGroup.MoveDownAndChangeDirection();
        }
        else if (colision.CompareTag("Bullet"))
        {
            // Chama o método no EnemyGroup para atualizar a lista de inimigos
            enemyGroup.OnEnemyDestroyed(gameObject);
            Destroy(gameObject);
        }
    }

}
