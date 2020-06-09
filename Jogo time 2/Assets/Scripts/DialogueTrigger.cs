using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueCanvas;
     [HideInInspector]
    public bool canActivate;
    [Tooltip("Isso ativa o dialogo sem que precise apertar uma tecla")]
    public bool Automatico;

    [Tooltip("Desaparece após o dialogo ter finalizado, serve para cutscenes")]
    public bool Cutscene;
    int index;
    [Tooltip("Coloque aqui o dialogo criado")]   
    public DialogueBlock Dialogue;
    public void Update()
    {
        index=FindObjectOfType<DialogueManager>().i;
        if (Input.GetKeyDown(KeyCode.Z)||Input.GetKeyDown("space")){
        if( canActivate){
            if (FindObjectOfType<DialogueManager>().DialogueOn == false){
                     TriggerDialogue();    
                }
        }
    }
    }
    
    public void TriggerDialogue()
    {
        //dialogueCanvas.gameObject.SetActive(true);
        FindObjectOfType<DialogueManager>().DisplayDialogue(Dialogue);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = true;
            if (Automatico == true)
            { 
                TriggerDialogue();
                if (Cutscene == true)
                {
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
