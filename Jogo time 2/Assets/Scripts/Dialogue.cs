using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Dialogue 
{ [Tooltip("o nome do personagem")] 
    public string Name;
    [TextArea(3,10)] //só pra aumentar o tamanho da caixa pros gds escreverem mais
 [Tooltip("O numero de frases do seu dialogo")] 
    public string[] Sentences;
    [Tooltip("Isso faz com que o proximo dialogo seja triggerado automaticamente")]  
        public bool Continuo=false;

    [HideInInspector]
    public DialogueBlock dialogueBlock;

}