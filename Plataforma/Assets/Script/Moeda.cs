using UnityEngine;

public class Moeda : MonoBehaviour
{
    // Referência ao Game Controller
    public GameController gameController;

    // Método que é chamado quando a moeda colide com algo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu é o player
        if (collision.CompareTag("Player"))
        {
            // Aumenta o contador de moedas no Game Controller
            gameController.AddCoin();

            // Destroi a moeda
            Destroy(gameObject);
        }
    }
}
