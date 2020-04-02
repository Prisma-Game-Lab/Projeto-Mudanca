using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoCamera : MonoBehaviour
{
    public GameObject Alvo;
    public Vector3 offset;
    public float moveSpeed;

    private Transform transfPlayer;

    // Start is called before the first frame update
    void Start()
    {
        transfPlayer = Alvo.GetComponent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //A: move-se junto com o player 
        transform.position = Vector3.Lerp(transform.position, transfPlayer.position + new Vector3(0,0,-10), moveSpeed * Time.deltaTime);
    }
}
