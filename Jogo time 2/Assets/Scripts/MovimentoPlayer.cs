using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPlayer : MonoBehaviour
{
    private Animator animator;

    private DialogueManager DialogueManager;
    public float StepsTime;
    [Tooltip("Velocidade de movimento do jogador")]
    public float speed;
    private Rigidbody2D rb;
    Vector2 movement;
    private AudioSource steps;

    //public Animator AlexAnimator;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        steps= gameObject.GetComponent<AudioSource>();
        animator= gameObject.GetComponent<Animator>();
        DialogueManager= FindObjectOfType<DialogueManager>();
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
        animator.SetBool("IsMoving", false);

        if (DialogueManager.DialogueOn==false){
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
         animator.SetBool("IsMoving",true);
        }
         }
        //else animator.SetBool("IsMoving",false); 
    }
}