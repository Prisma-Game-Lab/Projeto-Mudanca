using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text NameText;
    public Text DialogueText;
    public bool DialogueOn =false;
    private Queue<string> _sentences;
    public GameObject dialogue;
    public SceneControl sceneControl;

    public bool Boss;
    // Start is called before the first frame update
    void Start()
    {
        _sentences= new Queue<string>();
    }

    void Update(){
        if(DialogueOn==true){   
            if(Input.GetKeyDown("z")){
                DisplayNextSentence();
            }
        }
    }
    public void StartDialogue (Dialogue dialogue){

        NameText.text= dialogue.Name;
        Boss= dialogue.Boss;
        
        _sentences.Clear();
        foreach(string sentence in dialogue.Sentences){
            _sentences.Enqueue(sentence); //organiza em ordem os textos para escreve-los

        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence(){ //ele conta os textos existentes e vai desempilhando pra escrever
        if (_sentences.Count==0){
            EndDialogue();
            return;
        }

        string sentence= _sentences.Dequeue(); 
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
        IEnumerator TypeSentence(string sentence){ //animação das letras aparecendo
            DialogueText.text="";
            foreach(char letter in sentence.ToCharArray()){
                DialogueText.text+= letter;
                yield return null;
                DialogueOn=true;

            }
         
    }
      void EndDialogue(){
        dialogue.SetActive(false);
        DialogueOn=false;
        if (Boss==true)
            sceneControl.LoadScene("Teste Combate"); 
    }
    
}
