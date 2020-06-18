using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChangeManager : MonoBehaviour
{


    public GameObject OldDialogueObject;

    private SceneControl scene;
     

    private void Start()
    {
        scene = GameObject.Find("GameManager").GetComponent<SceneControl>();
    }
    void Update()
    {
         if (scene.derrota == true)
            {
                  OldDialogueObject.GetComponent<DialogueTrigger>().Dialogue[0]=OldDialogueObject.GetComponent<DialogueTrigger>().Dialogue[2];
                  
                  OldDialogueObject.GetComponent<DialogueTrigger>().Dialogue[1]=OldDialogueObject.GetComponent<DialogueTrigger>().Dialogue[3];
            }
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown("space")) && gameObject.GetComponent<DialogueTrigger>().canActivate)
        {
             
            OldDialogueObject.GetComponent<DialogueTrigger>().i =1;
            

        }


    }
}