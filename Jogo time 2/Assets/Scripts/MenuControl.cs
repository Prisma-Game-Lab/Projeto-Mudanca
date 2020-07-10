using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public string cenacreditos;
    public void startGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
       // SaveSystem.GetInstance().NewGame();
    }

    public void Load()
    {
        PauseMenu.GetInstance().LoadGame();
    }
    public void Credits()
    {
        SceneManager.LoadScene(cenacreditos);
    }
    public void quit()
    {
        PauseMenu.GetInstance().QuitGame();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }


}
