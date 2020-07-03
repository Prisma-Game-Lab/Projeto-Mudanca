using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    [HideInInspector]
    public SaveSystem savesystem;
    [HideInInspector]
    public int scenetocontinue;
    public GameObject player;
    public PlayableDirector[] timeline;
    public SceneControl sceneControl;
    public DialogueManager dialogueManager;
    public ListaDialogos listaDialogos;
    private static PauseMenu instance;
    public static PauseMenu GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        if (instance != null)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            instance = this;

        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {

        listaDialogos = FindObjectOfType<ListaDialogos>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        timeline = FindObjectsOfType<PlayableDirector>();
        savesystem = SaveSystem.GetInstance();
        sceneControl = FindObjectOfType<SceneControl>();
        player = GameObject.Find("Player");
        if (savesystem.playerinfo.saved == SceneManager.GetActiveScene().buildIndex)
        {
            player.transform.position = new Vector3(savesystem.playerinfo.posx, savesystem.playerinfo.posy, savesystem.playerinfo.posz);
            foreach (PlayableDirector timelines in timeline)
                timelines.Stop();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                if (!dialogueManager.DialogueOn)
                    Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void Save()
    {
        savesystem.playerinfo.sceneindex = SceneManager.GetActiveScene().buildIndex;
        savesystem.playerinfo.posx = player.transform.position.x;
        savesystem.playerinfo.posy = player.transform.position.y;
        savesystem.playerinfo.posz = player.transform.position.z;
        savesystem.playerinfo.Listadedialogos = listaDialogos.Listadedialogos;
        savesystem.playerinfo.saved = SceneManager.GetActiveScene().buildIndex; //isso serve pra posicao do player nao mudar caso ele salve em uma cena e vá para a outra
        savesystem.SaveState();
        Resume();
    }
    public void LoadGame()
    {
        scenetocontinue = savesystem.playerinfo.sceneindex;
        if (scenetocontinue != 0)
        {
            savesystem.LoadState();
            SceneManager.LoadScene(scenetocontinue);
            Resume();
        }
    }
    public void LoadMenu()
    {
        savesystem.playerinfo.sceneindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
