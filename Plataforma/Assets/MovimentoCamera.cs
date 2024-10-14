using UnityEngine;

public class MovimentoCamera : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocidade de movimento da câmera
    public Vector3 limitPosition; // Posição limite máxima que a câmera pode alcançar

    private Vector3 startPosition; // Posição inicial da câmera

    void Start()
    {
        // Armazena a posição inicial da câmera
        startPosition = transform.position;
    }

    void Update()
    {
        // Move a câmera para a direita ao longo do eixo X
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;

        // Verifica se a câmera ultrapassou a posição limite
        if (transform.position.x <= limitPosition.x)
        {
            // Define a posição da câmera para o limite
            transform.position = limitPosition;
            // Para o movimento da câmera
            enabled = false; // Desativa este script
        }
    }
}
