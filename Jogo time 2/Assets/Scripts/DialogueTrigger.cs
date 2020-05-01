using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{

    public GameObject dialogueCanvas;
    private bool canActivate;
    public bool Automatic;
    public bool Cutscene;


    public Dialogue Dialogue;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && canActivate)
            if (FindObjectOfType<DialogueManager>().DialogueOn == false)
            {
                {
                    dialogueCanvas.gameObject.SetActive(true);
                    TriggerDialogue();
                    
                }
            }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(Dialogue);

    }
     

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = true;
            if (Automatic==true){
                dialogueCanvas.gameObject.SetActive(true);
                TriggerDialogue();
                if(Cutscene==true){
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = false;
        }
    }
}
