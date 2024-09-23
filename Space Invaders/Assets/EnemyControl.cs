using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveAmount = 0.5f;     // Quanto o inimigo se move a cada passo
    public float dropAmount = 0.5f;     // Quanto o inimigo desce ao atingir os limites
    public float moveInterval = 0.5f;   // Intervalo de tempo entre cada movimento
    public float xMin = -7f;            // Limite esquerdo
    public float xMax = 7f;             // Limite direito

    private bool movingRight = true;    // Direção inicial (direita)

    void Start()
    {
        // Inicia a movimentação repetida
        StartCoroutine(MoveEnemy());
    }

    IEnumerator MoveEnemy()
    {
        while (true)
        {
            // Movimenta para direita ou esquerda baseado na direção
            if (movingRight)
            {
                transform.position += new Vector3(moveAmount, 0f, 0f);

                // Verifica se atingiu o limite direito
                if (transform.position.x >= xMax)
                {
                    movingRight = false;
                    MoveDown(); // Desce ao atingir o limite direito
                }
            }
            else
            {
                transform.position -= new Vector3(moveAmount, 0f, 0f);

                // Verifica se atingiu o limite esquerdo
                if (transform.position.x <= xMin)
                {
                    movingRight = true;
                    MoveDown(); // Desce ao atingir o limite esquerdo
                }
            }

            // Espera o intervalo de tempo antes do próximo movimento
            yield return new WaitForSeconds(moveInterval);
        }
    }

    // Função para mover o inimigo para baixo
    void MoveDown()
    {
        transform.position -= new Vector3(0f, dropAmount, 0f);
    }
}
