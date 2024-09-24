using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidade do jogador
    private Rigidbody2D rb;        // Referência ao Rigidbody2D do jogador
    public Text feedbackText;
    public int vidaAtual=3;

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
        Vector2 movement = new Vector2(moveX, 0);
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);  // Mantém a velocidade vertical
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Colidiu com a parede!");
            // Para o movimento do jogador
            rb.velocity = Vector2.zero;  
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Wall"))
        {
            Debug.Log("Colidiu com a parede!");
            rb.velocity = Vector2.zero;  // Para o movimento do jogador
        }
        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("Colidiu com inimigo!");
            rb.velocity = Vector2.zero;  // Para o movimento do jogador
        }
        if (collider.CompareTag("Bullet"))
        {
            Debug.Log("Colidiu com bala!");
            if(vidaAtual-1==0){
                SceneManager.LoadScene("Lose");
            }
            feedbackText.text = "vida atual: "+(vidaAtual-1).ToString();
            rb.velocity = Vector2.zero;  // Para o movimento do jogador
        }
    }



    
}
