using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ChangeDialogue: MonoBehaviour {
  private SceneControl scene;
  private ListaDialogos lista;
  private void Start() {
     scene = GameObject.Find("GameManager").GetComponent<SceneControl>();
     lista = GameObject.Find("ListManager").GetComponent<ListaDialogos>();

 }
   private void Update() {
       if (scene.derrota==true
       && lista.Listadedialogosfinaldodia.Contains(gameObject.GetComponent<DialogueTrigger>().Dialogue[0])){
           gameObject.GetComponent<DialogueTrigger>().i=1;
       }
         
   
   }
   
}
