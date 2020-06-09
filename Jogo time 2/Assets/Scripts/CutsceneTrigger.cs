using UnityEngine;
using UnityEngine.Playables;
 
public class Cutscenetrigger : MonoBehaviour
{
    public PlayableDirector timeline;
    private DialogueManager dialogue;

 
    // Use this for initialization
    void Start()
    {
        timeline = GetComponent<PlayableDirector>();
        dialogue=FindObjectOfType<DialogueManager>();
    }
 
 
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player" &&dialogue.DialogueOn==false)
        {
            timeline.Play();
        }
    }
}