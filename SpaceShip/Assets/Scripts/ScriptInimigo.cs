using UnityEngine;
using System.Collections;

public class ScriptInimigo : MonoBehaviour
{
    public float velocidade = 5f;  // Velocidade do inimigo
    public GameObject projétilPrefab; // Prefab do projétil a ser disparado
    public float intervaloDisparo = 2f; // Intervalo entre os disparos
    private Camera mainCamera; // Referência à câmera principal

    void Start()
    {
        // Obtém a referência da câmera principal
        mainCamera = Camera.main;
        StartCoroutine(Atirar()); // Inicia a coroutine de disparo
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
        return posicaoNaTela.x < 0;
    }

    // Coroutine para atirar periodicamente
    private IEnumerator Atirar()
    {
        while (true)
        {
            // Instancia um projétil
            InstanciarProjetil();

            // Espera pelo intervalo de disparo
            yield return new WaitForSeconds(intervaloDisparo);
        }
    }

    // Método para instanciar um projétil
    private void InstanciarProjetil()
    {
        // Define a posição de spawn do projétil (ajuste conforme necessário)
        Vector3 posicaoSpawn = transform.position + new Vector3(1f, 0, 0); // Um pouco à direita do inimigo

        // Instancia o projétil
        GameObject projétil = Instantiate(projétilPrefab, posicaoSpawn, Quaternion.Euler(0, 0, 90)); // Rotaciona 90 graus

        // Ajuste a direção do projétil para que vá da direita para a esquerda
        Rigidbody2D rb = projétil.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.left * 10f; // Define a velocidade do projétil (ajuste conforme necessário)
        }
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
