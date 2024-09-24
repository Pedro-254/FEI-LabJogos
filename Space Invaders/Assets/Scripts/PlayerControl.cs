using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidade do jogador
    private Rigidbody2D rb;        // Referência ao Rigidbody2D do jogador
    public Text feedbackText;
    public int vidaAtual = 3;
    private bool invulneravel = false;
    private float tempoInvulneravel = 0.5f;  // Meio segundo de invulnerabilidade

    private bool isTouchingRightWall = false; // Controle da colisão com a parede direita
    private bool isTouchingLeftWall = false;  // Controle da colisão com a parede esquerda

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");  // Movimento horizontal

        // Se colidiu com a parede direita, bloqueia movimento para a direita
        if (isTouchingRightWall && moveX > 0)
        {
            moveX = 0;
        }

        // Se colidiu com a parede esquerda, bloqueia movimento para a esquerda
        if (isTouchingLeftWall && moveX < 0)
        {
            moveX = 0;
        }

        Vector2 movement = new Vector2(moveX, 0);
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);  // Mantém a velocidade vertical
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Wall"))
        {
            // Verifica se colidiu com a parede direita ou esquerda
            if (collider.transform.position.x > transform.position.x)
            {
                isTouchingRightWall = true;
            }
            else if (collider.transform.position.x < transform.position.x)
            {
                isTouchingLeftWall = true;
            }

            rb.velocity = Vector2.zero;  // Para o movimento do jogador
        }
        else if (collider.CompareTag("Bullet") && !invulneravel)
        {
            StartCoroutine(Invulnerabilidade());
            Destroy(collider.gameObject);
            Debug.Log("Colidiu com bala!");
            vidaAtual--;
            if (vidaAtual <= 0)
            {
                SceneManager.LoadScene("Lose");
            }
            feedbackText.text = "Vida: " + vidaAtual.ToString();
            rb.velocity = Vector2.zero;
        }
        else if(collider.CompareTag("Enemy"))
        {
            SceneManager.LoadScene("Lose");
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Wall"))
        {
            // Quando sair da colisão com a parede, libera o movimento
            if (collider.transform.position.x > transform.position.x)
            {
                isTouchingRightWall = false;
            }
            else if (collider.transform.position.x < transform.position.x)
            {
                isTouchingLeftWall = false;
            }
        }
    }

    private IEnumerator Invulnerabilidade()
    {
        invulneravel = true;
        yield return new WaitForSeconds(tempoInvulneravel);
        invulneravel = false;
    }
}
