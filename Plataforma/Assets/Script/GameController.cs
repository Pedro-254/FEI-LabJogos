using UnityEngine;

public class GameController : MonoBehaviour
{
    public int coinCount = 0; // Contador de moedas

    public GameObject unidade; // Referência para o objeto Unidade
    public GameObject dezena;  // Referência para o objeto Dezena
    public GameObject centena; // Referência para o objeto Centena

    public Sprite[] numberSprites; // Array para armazenar os sprites dos números de 0 a 9

    public AudioSource audioSource; // Componente AudioSource para tocar o som

    void Start()
    {
        // Obtém o componente AudioSource do GameObject
        audioSource = GetComponent<AudioSource>();
    }

    // Método para adicionar uma moeda
    public void AddCoin()
    {
        coinCount++; // Incrementa o contador
        UpdateScore(); // Atualiza a representação do score

        // Toca o som de coleta de moeda
        audioSource.Play();
    }

    // Método para atualizar o score
    private void UpdateScore()
    {
        // Calcula as unidades, dezenas e centenas
        int unidades = coinCount % 10; // Resto da divisão por 10
        int dezenas = (coinCount / 10) % 10; // Divide por 10 e pega o resto
        int centenas = (coinCount / 100) % 10; // Divide por 100 e pega o resto

        // Atualiza os sprites
        unidade.GetComponent<SpriteRenderer>().sprite = numberSprites[unidades];
        dezena.GetComponent<SpriteRenderer>().sprite = numberSprites[dezenas];
        centena.GetComponent<SpriteRenderer>().sprite = numberSprites[centenas];
    }
}
