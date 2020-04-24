using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{

    public GameObject dialogueCanvas;
    private bool canActivate;

    
    public Dialogue Dialogue;
    
   
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canActivate)
        {
            dialogueCanvas.gameObject.SetActive(true);
            TriggerDialogue();
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
