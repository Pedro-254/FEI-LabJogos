using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MovimentaçãoSpaceShip : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidade de movimento
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Captura a entrada do jogador no eixo horizontal e vertical
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Move a nave
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto colidido tem a tag "Projetil"
        if (collision.CompareTag("Inimigo"))
        {
            // Destroi o nave
            Destroy(gameObject);

            SceneManager.LoadScene("Lose");
            
        }

        // Verifica se o objeto colidido tem a tag "Projetil"
        if (collision.CompareTag("ProjetilInimigo"))
        {
            // Destroi o nave
            Destroy(gameObject);

            SceneManager.LoadScene("Lose");
            
        }
    }
}
