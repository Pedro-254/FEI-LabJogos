using UnityEngine;

public class TiroNave : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab do projétil
    public Transform firePoint;     // Ponto de origem do tiro
    public float bulletSpeed = 10f; // Velocidade do projétil
    public float bulletLifetime = 2f; // Tempo de vida do projétil (em segundos)

    void Update()
    {
        // Verifica se o jogador pressionou a tecla espaço
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instancia o projétil na posição e rotação do ponto de disparo
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Obtém o Rigidbody2D do projétil para aplicar velocidade
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        
        // Adiciona força ao projétil na direção em que a nave está apontando
        rb.velocity = firePoint.up * bulletSpeed;

        // Destroi o projétil após bulletLifetime segundos
        Destroy(bullet, bulletLifetime);
    }
}
