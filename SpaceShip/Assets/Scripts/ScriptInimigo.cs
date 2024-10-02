using UnityEngine;
using System.Collections;

public class ScriptInimigo : MonoBehaviour
{
    public float velocidade = 5f;  // Velocidade do inimigo
    private Camera mainCamera;     // Referência à câmera principal

    void Start()
    {
        // Obtém a referência da câmera principal
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Move o inimigo da direita para a esquerda
        transform.Translate(Vector2.left * velocidade * Time.deltaTime);

        // Verifica se o inimigo saiu da tela
        if (SaiuDaTela())
        {
            Destroy(gameObject); // Destroi o inimigo se ele sair da tela
        }
    }

    // Verifica se o inimigo está fora da área visível da câmera
    bool SaiuDaTela()
    {
        Vector3 posicaoNaTela = mainCamera.WorldToViewportPoint(transform.position);
        if (posicaoNaTela.x < 0)
        {
            return true;
        }
        return false;
    }

    // Método chamado quando o inimigo entra em colisão com outro objeto
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto colidido tem a tag "Projetil"
        if (collision.CompareTag("Projetil"))
        {
            // Destroi o inimigo
            Destroy(gameObject);

            // Destroi o projétil
            Destroy(collision.gameObject);
            ScoreManager.AddScore(10);
        }
    }
}
