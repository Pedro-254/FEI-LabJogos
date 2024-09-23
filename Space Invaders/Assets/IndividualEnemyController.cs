using System.Diagnostics;
using UnityEngine;

public class IndividualEnemyController : MonoBehaviour
{
    private EnemyGroup enemyGroup;  // Referência ao grupo de inimigos

    void Start()
    {
        // Encontra o script "EnemyGroup" no objeto pai
        enemyGroup = GetComponentInParent<EnemyGroup>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        UnityEngine.Debug.Log("Esta é uma mensagem de log.");
        // Se colidir com uma parede (por exemplo, um objeto com a tag "Wall")
        if (other.CompareTag("Wall"))
        {
            // Notificar o grupo de inimigos para descer e inverter a direção
            enemyGroup.MoveDownAndChangeDirection();
        }
    }
}
