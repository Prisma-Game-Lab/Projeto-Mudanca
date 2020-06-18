using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ChangeDialogue: MonoBehaviour {
  private GameObject scene;
  private GameObject lista;
  private void Start() {
     scene = GameObject.Find("GameManager");
     lista = GameObject.Find("ListManager");

 }
   private void Update() {
       if (scene.GetComponent<SceneControl>().derrota==true
       && lista.GetComponent<ListaDialogos>().Listadedialogos.Contains(gameObject.GetComponent<DialogueTrigger>().Dialogue[0])){
           gameObject.GetComponent<DialogueTrigger>().i=1;
       }
         if (Input.GetKeyDown(KeyCode.Q)){
             Debug.Log(scene);
              Debug.Log(lista);
         }
   
   }
   
}
