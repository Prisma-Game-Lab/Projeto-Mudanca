using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
 

public class CutsceneTrigger : MonoBehaviour
{

    public PlayableDirector timeline;
    private DialogueManager dialogue;

 
    // Use this for initialization
    void Start()
    {
         
        dialogue=FindObjectOfType<DialogueManager>();
    }
 
 
    void OnTriggerEnter2D(Collider2D other)
    {
       if (other.CompareTag("Player"))
        {
            timeline.Play();
        }
    }
}