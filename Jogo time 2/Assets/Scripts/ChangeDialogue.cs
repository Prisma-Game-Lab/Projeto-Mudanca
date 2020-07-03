using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ChangeDialogue : MonoBehaviour
{
    private SceneControl scene;
    private ListaDialogos lista;
    private void Start()
    {
       scene=FindObjectOfType<SceneControl>();
       lista=FindObjectOfType<ListaDialogos>();
    }
    private void Update()
    {
         {
            if (lista.Listadedialogosfinaldodia.Contains(gameObject.GetComponent<DialogueTrigger>().Dialogue[0]))
            {
                if (scene.derrota == true)
        
                gameObject.GetComponent<DialogueTrigger>().i = 1;
            }
        }

    }

}
