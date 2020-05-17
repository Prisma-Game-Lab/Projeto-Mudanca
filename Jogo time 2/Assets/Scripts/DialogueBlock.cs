using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="Novo Diálogo", menuName = "Bloco de Dialogo")]
public class DialogueBlock : ScriptableObject
{
    [TextArea(5,10)]
    [Tooltip("Apenas para escrever anotacoes sobre o dialogo")]
    public string Descricao;
    [Tooltip("O numero de dialogos diferentes que vão ter")]
    public Dialogue[] Dialogue;
     [Tooltip("Caso marcado, esse botão faz com que se inicie a boss battle")]
    public bool Boss=false; 
    [HideInInspector]
    public int i;
    [HideInInspector]
    public int index;
    public void OnEnable() {
        for (i=0;i<Dialogue.Length;i++){
            Dialogue[i].dialogueBlock=this;// coloca o dialogue box atual na variavel dialogue box dentro do dialogue
        }
    }
}
