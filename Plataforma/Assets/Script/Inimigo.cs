using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f; // Velocidade do inimigo
    public float patrolDistance = 5f; // Distância que o inimigo vai percorrer em uma direção
    public float waitTime = 1f; // Tempo que o inimigo vai esperar ao atingir os limites
    public Animator animator; // Referência ao Animator do inimigo
    public BoxCollider2D boxCollider; // Referência ao BoxCollider2D do inimigo
    public Rigidbody2D rb; // Referência ao Rigidbody2D do inimigo

    private bool movingRight = true; // Controla a direção do movimento
    private float startPositionX; // Posição inicial no eixo X
    private bool isWaiting = false; // Verifica se o inimigo está esperando
    private bool isDead = false; // Verifica se o inimigo está morto

    void Start()
    {
        startPositionX = transform.position.x; // Salva a posição inicial do inimigo
    }

    void Update()
    {
        // Se o inimigo não estiver esperando ou morto, ele pode se mover
        if (!isWaiting && !isDead)
        {
            Patrol();
            animator.SetBool("walk", true); // Ativa a animação de andar enquanto está se movendo
        }
        else
        {
            animator.SetBool("walk", false); // Desativa a animação de andar quando estiver esperando ou morto
        }
    }

    void Patrol()
    {
        // Movimenta o inimigo
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            // Verifica se já percorreu a distância definida para a direita
            if (transform.position.x >= startPositionX + patrolDistance)
            {
                StartCoroutine(WaitAtPoint()); // Espera antes de voltar
                movingRight = false; // Inverte a direção para voltar
                Flip(); // Vira o sprite do inimigo, se necessário
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            // Verifica se já percorreu a distância definida para a esquerda
            if (transform.position.x <= startPositionX - patrolDistance)
            {
                StartCoroutine(WaitAtPoint()); // Espera antes de ir novamente
                movingRight = true; // Inverte a direção para ir novamente para a direita
                Flip(); // Vira o sprite do inimigo, se necessário
            }
        }
    }

    // Corrotina para esperar 1 segundo ao atingir um limite
    IEnumerator WaitAtPoint()
    {
        isWaiting = true; // Define que o inimigo está esperando
        animator.SetBool("walk", false); // Para a animação de andar enquanto espera
        yield return new WaitForSeconds(waitTime); // Espera pelo tempo definido
        isWaiting = false; // Retorna ao movimento
        animator.SetBool("walk", true); // Retoma a animação de andar após esperar
    }

    // Método para virar o sprite do inimigo
    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Inverte o eixo X do sprite
        transform.localScale = localScale;
    }

    // Método para lidar com o jogador "pisando" no inimigo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Verifica se o jogador está em uma posição acima do inimigo
            if (collision.transform.position.y > transform.position.y + 0.15f)
            {
                StartCoroutine(Die()); // Inicia a sequência de morte
            }
        }
    }

    // Corrotina para ativar a animação de morte, mudar o BoxCollider2D e Rigidbody2D, e destruir o inimigo
    IEnumerator Die()
    {   
        // Remove a tag do inimigo
        gameObject.tag = "Untagged";
        isDead = true; // O inimigo está morto
        animator.SetBool("dead", true); // Define a animação de morte

        // Modifica o BoxCollider2D após a morte
        boxCollider.offset = new Vector2(-0.001233459f, 0.03200474f); // Define o novo offset
        boxCollider.size = new Vector2(0.3380117f, 0.1659905f); // Define o novo tamanho

        yield return new WaitForSeconds(0.3f);
        // Remove o BoxCollider2D
        Destroy(boxCollider);
        // Muda o Rigidbody2D para static para que o inimigo não se mova mais
        rb.bodyType = RigidbodyType2D.Static;

        // Espera 2 segundos antes de destruir o inimigo
        yield return new WaitForSeconds(2f);
        

        

        // Finalmente, destrói o inimigo
        Destroy(gameObject);
    }
}
