using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriangle : MonoBehaviour
{
        private void Start()
    {
        StartCoroutine(desaparecer());
        
    }

    IEnumerator desaparecer()
    {

        while (true){
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(1);
    }
    }
}