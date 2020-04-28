using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Dialogue 
{
    public string Name;
    [TextArea(3,10)] //só pra aumentar o tamanho da caixa pros gds escreverem mais

    public string[] Sentences;
    public bool Boss=false; 
}
