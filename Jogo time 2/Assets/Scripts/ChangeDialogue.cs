using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ChangeDialogue: MonoBehaviour {
  private SceneControl scene;
  private ListaDialogos lista;
  private void Start() {
     scene = FindObjectOfType<SceneControl>();
     lista = FindObjectOfType<ListaDialogos>();

 }
   private void Update() {
       if (scene.derrota==true&& lista.Listadedialogos.Contains(gameObject.GetComponent<DialogueTrigger>().Dialogue[0])){
           gameObject.GetComponent<DialogueTrigger>().i=1;
       }
   }
}
