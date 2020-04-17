using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPlayer : MonoBehaviour
{
    private Animator animator;
    public float StepsTime;
    public float speed;
    private Rigidbody2D rb;
    Vector2 movement;
    private AudioSource steps;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        steps= gameObject.GetComponent<AudioSource>();
        animator= gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //A: Captura Movimento de jogador
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //A: Previne rotação do jogador
        //transform.eulerAngles = new Vector3(0, 0, 0);
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
         steps.Pause();
         
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        if (movement.x!=0 || movement.y!=0){
            if (movement.x < 0)
		    {
			    transform.rotation = new Quaternion(0, 180, 0, 0);
            }
		    else if (movement.x > 0)
		    {
			    transform.rotation = new Quaternion(0, 0, 0, 0);
            }
         steps.Play();
         animator.SetBool("IsWalking",true);
        }
       
        else animator.SetBool("IsWalking",false); 
    }
}