using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public Animator transition;
    public PlayerInfo playerInfo;
    private static SceneControl _instance;
    public static SceneControl Instance { get { return _instance; } }
    public float transitionTime = 1f;
    public bool derrota;

    private static SceneControl instance;
    public static SceneControl GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        derrota = SaveSystem.GetInstance().playerinfo.derrota;
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
                derrota = false;
            }
        }
    }
    public void LoadScene(string sceneToLoad)
    {
        StartCoroutine(LoadLevel(sceneToLoad));
    }
    IEnumerator LoadLevel(string sceneToLoad)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        transition.SetTrigger("End");
        SceneManager.LoadScene(sceneToLoad);
    }
    public void TriggerDerrota(bool estado)
    {
        derrota = estado;
    }
    public string getscene()
    {
        return SceneManager.GetActiveScene().name;
    }
}
