using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class yoshi : MonoBehaviour
{  
    public string cenaV;
    public string cenaD;
    public float Speed;
    public float JumpForce;
    public bool isjump;
    public bool doublejump;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer; // Para alterar a cor do player

    public int totalLives = 3; // Número total de vidas configurável no Unity Inspector
    public Color damageColor = Color.red; // Cor que o player vai piscar ao tomar dano
    public float damageFlashDuration = 0.5f; // Duração do piscar

    public HeartSystem heart;

    private Color originalColor; // Cor original do sprite
    private bool isInvulnerable = false; // Flag para invulnerabilidade

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Pega o SpriteRenderer
        originalColor = spriteRenderer.color; // Armazena a cor original
        
    }
    

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        if(totalLives<=0){
            Invoke("LoadGameOver",0f);
        }
    }
    void LoadGameOver(){
        SceneManager.LoadScene(cenaD);
    }

    void Move(){
        Vector3 mov = new Vector3(Input.GetAxis("Horizontal"), 0,0);
        transform.position += mov * Speed * Time.deltaTime;

        if(Input.GetAxis("Horizontal") > 0){
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if(Input.GetAxis("Horizontal") < 0){
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0,180,0);
        }
        else{
            anim.SetBool("walk", false);
        }
    }

    void Jump(){
        if(Input.GetButtonDown("Jump")){
            if(!isjump){
                rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                doublejump = true;
            }
            else{
                if(doublejump){
                    rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                    doublejump = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        // Detecta se o player colidiu com o chão
        if(col.gameObject.layer == 6){
            isjump = false;
        }

        if (col.gameObject.CompareTag("Enemy"))
        {
            // Verifica se o jogador está em uma posição acima do inimigo
            if (col.transform.position.y + 0.05f < transform.position.y )
            {
            }
            else
            {
                heart.vida--;
                LoseLife();
            }
        }

        if (col.gameObject.CompareTag("Star")){
            SceneManager.LoadScene(cenaV);
        }
        

    }

    void OnCollisionExit2D(Collision2D col){
        if(col.gameObject.layer == 6){
            isjump = true;
        }
    }

    // Método para reduzir uma vida e piscar em vermelho
    void LoseLife(){


        totalLives--;
        Debug.Log("Vidas restantes: " + totalLives);

        // Pisca em vermelho ao tomar dano e fica invulnerável
        StartCoroutine(DamageFlash());
        if(totalLives <= 0){
            // Ações a serem tomadas quando as vidas acabarem, como reiniciar o nível
            Debug.Log("Game Over!");
        }
    }

    // Corrotina para lidar com a invulnerabilidade
    IEnumerator InvulnerabilityCoroutine()
    {
        // Ativa a invulnerabilidade
        isInvulnerable = true;

        // Aguarda o tempo de invulnerabilidade
        yield return new WaitForSeconds(0.2f);

        // Desativa a invulnerabilidade
        isInvulnerable = false;
    }

    // Coroutine para fazer o player piscar em vermelho e ficar invulnerável temporariamente
    IEnumerator DamageFlash(){
        // Ativa a invulnerabilidade
        isInvulnerable = true;

        // Altera a cor para vermelho
        spriteRenderer.color = damageColor;

        // Aguarda o tempo de invulnerabilidade
        yield return new WaitForSeconds(damageFlashDuration);

        // Retorna a cor original
        spriteRenderer.color = originalColor;

        // Desativa a invulnerabilidade
        isInvulnerable = false;
    }
}
