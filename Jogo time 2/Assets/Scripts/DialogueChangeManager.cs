using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChangeManager : MonoBehaviour
{
   
   
    public GameObject OldDialogueObject;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown("space")) && gameObject.GetComponent< DialogueTrigger>().canActivate)
        {
           OldDialogueObject.GetComponent<DialogueTrigger>().i=1;
        }

   
}
}