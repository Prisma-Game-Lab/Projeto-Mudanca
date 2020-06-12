using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDialogue : MonoBehaviour
{

    public DialogueBlock NewDialogue;
    public GameObject OldDialogueObject;

    private DialogueTrigger DialogueTrigger;
    void Start()
    {
        DialogueTrigger = gameObject.GetComponent<DialogueTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Z)||Input.GetKeyDown("space")) && DialogueTrigger.canActivate)
        {
            OldDialogueObject.GetComponent<DialogueTrigger>().Dialogue = NewDialogue;
        }


    }
    }