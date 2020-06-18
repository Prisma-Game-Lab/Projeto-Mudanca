using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneManager : MonoBehaviour
{
    public ListaDialogos Lista;
     private SceneControl SceneController;
     public string scene;
     public int size;
     private void Awake() {
         SceneController=FindObjectOfType<SceneControl>();
         Lista=  GameObject.Find("ListManager").GetComponent<ListaDialogos>();
     }
             public void Click1(string sceneToLoad)
           {
                 SceneManager.LoadScene( sceneToLoad);
                 size= Lista.Listadedialogos.Count;
                 for (int i=0;i<size;i++){
                 Lista.Listadedialogosfinaldodia.Add( Lista.Listadedialogos[i]);
                 }
           }

       public void TriggerDerrota(bool estado)
    {
        SceneController.derrota=estado;
    }
}
