using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneManager : MonoBehaviour
{
   
     private SceneControl SceneController;
     public string scene;
     private void Awake() {
         SceneController=FindObjectOfType<SceneControl>();
     }
             public void Click1(string sceneToLoad)
           {
                 SceneManager.LoadScene( sceneToLoad);
           }
}
