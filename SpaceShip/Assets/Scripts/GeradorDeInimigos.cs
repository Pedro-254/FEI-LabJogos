using UnityEngine;

public class GeradorDeInimigos : MonoBehaviour
{
    public GameObject inimigoPrefab;      // Prefab do inimigo
    public float intervaloSpawn = 2f;     // Tempo entre gerações iniciais de inimigos
    public float alturaMinima = -4f;      // Altura mínima para spawn
    public float alturaMaxima = 4f;       // Altura máxima para spawn
    public float distanciaForaDaCamera = 2f; // Distância base fora da câmera à direita
    public float variacaoX = 1f;          // Variação aleatória na posição X

    public int maxInimigosPorVez = 5;     // Máximo de inimigos gerados ao mesmo tempo
    public float tempoAumentarInimigos = 10f; // Intervalo para aumentar a quantidade de inimigos
    public int aumentoInimigos = 1;       // Quantidade de inimigos a aumentar por intervalo

    private Camera mainCamera;
    private int inimigosParaGerar = 1;    // Quantidade atual de inimigos a gerar

    void Start()
    {
        mainCamera = Camera.main;

        // Inicia o processo de spawn
        InvokeRepeating("SpawnInimigos", 1f, intervaloSpawn);

        // Aumenta a quantidade de inimigos com o tempo
        InvokeRepeating("AumentarInimigos", tempoAumentarInimigos, tempoAumentarInimigos);
    }

    void SpawnInimigos()
    {
        for (int i = 0; i < inimigosParaGerar; i++)
        {
            // Gera inimigos dentro do limite atual
            GerarInimigo();
        }
    }

    void GerarInimigo()
    {
        // Obtém a posição da borda direita da câmera
        Vector3 bordaDireita = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, mainCamera.nearClipPlane));

        // Gera uma posição aleatória em Y entre a altura mínima e máxima
        float yAleatorio = Random.Range(alturaMinima, alturaMaxima);

        // Gera uma variação aleatória em X
        float variacaoAleatoriaX = Random.Range(-variacaoX, variacaoX);

        // Define a posição de spawn à direita da câmera, com variação em X
        Vector3 posicaoSpawn = new Vector3(bordaDireita.x + distanciaForaDaCamera + variacaoAleatoriaX, yAleatorio, 0);

        // Instancia o inimigo na posição gerada
        Instantiate(inimigoPrefab, posicaoSpawn, Quaternion.identity);
    }

    void AumentarInimigos()
    {
        // Aumenta a quantidade de inimigos gerados por vez até o limite máximo
        if (inimigosParaGerar < maxInimigosPorVez)
        {
            inimigosParaGerar += aumentoInimigos;
        }
    }
}
