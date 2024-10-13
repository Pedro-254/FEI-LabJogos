using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yoshi : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public bool isjump;
    public bool doublejump;
    private Rigidbody2D rb;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
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
        if(col.gameObject.layer == 6){
            isjump = false;
        }
    }

    void OnCollisionExit2D(Collision2D col){
        if(col.gameObject.layer == 6){
            isjump = true;
        }
    }
}
