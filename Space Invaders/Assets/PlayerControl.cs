using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidade do jogador
    private Rigidbody2D rb;        // Referência ao Rigidbody2D do jogador

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
        rb.velocity = movement * moveSpeed;          // Define a velocidade do jogador
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Colidiu com a parede!");
            // Aqui você pode adicionar lógica adicional, como parar o movimento ou mudar a direção
            rb.velocity = Vector2.zero;  // Para o movimento do jogador
        }
    }
}
