using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text NameText;
    public Text DialogueText;
    public bool DialogueOn = false;
    private Queue<string> _sentences;

    private bool complete;
    public GameObject dialogue;
    public DialogueBlock DialogueBlock;

    [HideInInspector]
    public int i;
    public SceneControl sceneControl;
    public bool Boss;
    // Start is called before the first frame update
    void Start()
    {
        _sentences = new Queue<string>();
    }

    void Update()
    {
        if (DialogueOn == true)
        {
            if (Input.GetKeyDown("z")&&complete==true)
            {
                complete=false;
                DisplayNextSentence();
            }
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        NameText.text = dialogue.Name;
        DialogueBlock = dialogue.dialogueBlock; //ele pega o bloco de dialogo que contem todos os dialogos
        DialogueBlock.index = i;
        Boss = DialogueBlock.Boss;

        _sentences.Clear();
        foreach (string sentence in dialogue.Sentences)
        {
            _sentences.Enqueue(sentence); //organiza em ordem os textos para escreve-los

        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    { //ele conta os textos existentes e vai desempilhando pra escrever
        if (_sentences.Count == 0)
        {
            if (i >= DialogueBlock.Dialogue.Length - 1)
            { 
                EndDialogue(); //veriifca se há mais algum dialogo disponivel, se esse for o ultimo ele encerra o dialogo
            }
            else
            {
                if (DialogueBlock.Dialogue[i].Continuo == false)
                {
                    EndDialogue(); //se o dialogo não for continuo, ele encerra
                }
                else
                {
                    i++;
                    DisplayDialogue(DialogueBlock);//caso ainda hajam dialogos e eles forem continuos, o proximo dialogo sera lido
                }
            }
            return;
        }

        string sentence = _sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    { //animação das letras aparecendo
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return null;
            DialogueOn = true;
             if (Input.GetKeyDown("z"))
            {
                 DialogueText.text = "";
                 DialogueText.text= sentence;
                 complete=true;
                break;
            }
        }
        complete=true;
    }
    public void DisplayDialogue(DialogueBlock dialogueBlock)
    {
        StartDialogue(dialogueBlock.Dialogue[i]); //le o proximo dialogo do bloco
    }
    void EndDialogue()
    {
        if (i >= DialogueBlock.Dialogue.Length - 1)
        {
            i = 0; //se o index passar do limite, ele reseta pro primeiro dialogo 
        }
        else i++; //caso contrario ele adiciona 1 ao index para da proxima vez que se clicar, o proxmo dialogo seja exibido

        dialogue.SetActive(false);
        DialogueOn = false;
        if (Boss == true)
            sceneControl.LoadScene("Teste Combate");
    }
}
