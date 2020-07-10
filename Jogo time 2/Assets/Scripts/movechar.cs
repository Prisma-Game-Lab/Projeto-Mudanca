using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movechar : MonoBehaviour
{
    private float speed = 600.0f;
    public GameObject NPC;
    public bool activate = false;
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (activate == true)
        {

            Vector3 wayPointPos = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);

            NPC.transform.position = Vector3.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
            activate=false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player = other.gameObject;

        activate = true;
    }
}

