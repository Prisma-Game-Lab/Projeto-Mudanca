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

    public float TextSpeed;
    public bool Festa; 

    public float FastTextSpeed;

    public float SlowTextSpeed;

    private bool complete;
    public GameObject dialogueUI;
    public DialogueBlock DialogueBlock;

    [HideInInspector]
    public int i;
    private SceneControl sceneControl;

    public Animator animator;
    public bool Boss;

    public bool Reflexao;


    // Start is called before the first frame update
    void Start()
    {
        _sentences = new Queue<string>();
        sceneControl=   GameObject.Find("GameManager").GetComponent<SceneControl>();
    }

    void Update()
    {
        if (DialogueOn == true)
        {
            if ((Input.GetKeyDown("z") || Input.GetKeyDown("space")) && complete == true)
            {
                complete = false;
                DisplayNextSentence();
            }
        }
    }
    public void StartDialogue(Dialogue dialogue)
    {
        dialogueUI.transform.GetChild(0).gameObject.SetActive(false);
        dialogueUI.transform.GetChild(1).gameObject.SetActive(false);
        dialogueUI.transform.GetChild(2).gameObject.SetActive(false);
        NameText.text = dialogue.Name;


        DialogueBlock = dialogue.dialogueBlock; //ele pega o bloco de dialogo que contem todos os dialogos
        DialogueBlock.index = i;
        Boss = DialogueBlock.Boss;
        Festa= DialogueBlock.Festa;
        Reflexao= DialogueBlock.Reflexão;
        if (NameText.text == "Alex")
        {
            dialogueUI.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (NameText.text != "Alex" && Boss == true)
        {
            dialogueUI.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            dialogueUI.transform.GetChild(0).gameObject.SetActive(true);
        }
        animator.SetBool("IsOpen", true);


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
        bool mudavelocidade = false;
        float pivot = TextSpeed;
        DialogueOn = true;

        foreach (char letter in sentence.ToCharArray())
        {
            if (letter == '+')
            {
                if (!mudavelocidade)
                {
                    TextSpeed = FastTextSpeed;
                    mudavelocidade = true;
                }
                else
                {
                    mudavelocidade = false;
                    TextSpeed = pivot;
                }
            }
            if (letter == '-')
            {
                if (!mudavelocidade)
                {
                    TextSpeed = SlowTextSpeed;
                    mudavelocidade = true;
                }
                else
                {
                    mudavelocidade = false;
                    TextSpeed = pivot;
                }
            }
            else
                DialogueText.text += letter;
                
            yield return new WaitForSeconds(TextSpeed / 100);
            if (Input.GetKeyDown("z") || Input.GetKeyDown("space"))
            {
                string temp = "";
                DialogueText.text = "";
                if (sentence.Contains("+") || sentence.Contains("-"))
                {
                    temp = sentence;
                    temp = temp.Replace("+", "");
                    temp = temp.Replace("-", "");
                    DialogueText.text = temp;
                }
                else
                {

                    DialogueText.text = sentence;
                }
                complete = true;
                TextSpeed = pivot;
                break;

            }
        }
        complete = true;

    }
    public void DisplayDialogue(DialogueBlock dialogueBlock)
    {
        StartDialogue(dialogueBlock.Dialogue[i]); //le o proximo dialogo do bloco
    }

    IEnumerator displaydialogue(){
      
     yield return new WaitForSeconds(0.1f);
     DialogueOn = false;
      Debug.Log("ACABOU");
       
 }
    void EndDialogue()
    {
        if (i >= DialogueBlock.Dialogue.Length - 1)
        {
            i = 0; //se o index passar do limite, ele reseta pro primeiro dialogo 
        }
        else i++; //caso contrario ele adiciona 1 ao index para da proxima vez que se clicar, o proxmo dialogo seja exibido
        animator.SetBool("IsOpen", false);
        StartCoroutine( displaydialogue());
         
        
        if (Boss == true)
            sceneControl.LoadScene("Teste Combate");

        else if (Festa == true){ 
          if (sceneControl.getscene()=="Quarto")
          sceneControl.LoadScene("Festa");  
          else if (sceneControl.getscene()=="Quarto2")
          sceneControl.LoadScene("Festa2");  
          //else if (sceneControl.getscene()=="Quarto3")
          //sceneControl.LoadScene("Festa3");  
        }
        else if (Reflexao==true){
            sceneControl.LoadScene("Quarto2");  
        }
    }

  
}
