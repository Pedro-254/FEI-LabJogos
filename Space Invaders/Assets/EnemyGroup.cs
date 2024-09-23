using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public float moveSpeed = 2f;  // Velocidade de movimento horizontal
    public float moveDownAmount = 0.5f;  // Quantidade de movimento para baixo
    private Vector3 direction = Vector3.right;  // Direção inicial

    void Update()
    {
        // Movimentar o grupo de inimigos na direção atual
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    // Função que será chamada para mover o grupo para baixo e inverter a direção
    public void MoveDownAndChangeDirection()
    {
        // Mover para baixo
        transform.position += Vector3.down * moveDownAmount;

        // Inverter direção
        direction = (direction == Vector3.right) ? Vector3.left : Vector3.right;
    }
}
