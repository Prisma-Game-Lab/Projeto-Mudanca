using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public GameObject GM;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GM.GetComponent<SceneControl>().LoadtNextScene("Festa");
    }
}
