using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPlayer : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
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
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
