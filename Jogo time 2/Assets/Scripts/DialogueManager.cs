using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text NameText;
    public Text DialogueText;
    private Queue<string> _sentences;
    // Start is called before the first frame update
    void Start()
    {
        _sentences= new Queue<string>();
    }
    public void StartDialogue (Dialogue dialogue){

        NameText.text= dialogue.Name;
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
        StartCoroutine(TypeSentence(sentence));
    }
        IEnumerator TypeSentence(string sentence){ //animação das letras aparecendo
            DialogueText.text="";
            foreach(char letter in sentence.ToCharArray()){
                DialogueText.text+= letter;
                yield return null;

            }
         
    }
      void EndDialogue(){
       Debug.log("cabou o texto"); //futuramente pode-se colocar animações de fechar a caixa de dialogo     
    }
    
}
