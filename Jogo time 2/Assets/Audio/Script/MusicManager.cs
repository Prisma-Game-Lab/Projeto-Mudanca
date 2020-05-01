using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Essa classe guarda as músicas que devem ser tocadas em cada cena
/// </summary>
[System.Serializable]
public class MusicData{
    [Tooltip("AudioSources que devem ser tocado nas cena")]public List<AudioSource> music;
    [Tooltip("Cena que devem tocar os audiosources (coloque o nome do arquivo .unity, sem o .unity)")]public string scene; 
}

public class MusicManager : MonoBehaviour
{
    /// <summary>
    /// Usado para referência e pra checar duplicatas
    /// </summary>
    public static MusicManager instance;

    [Tooltip("Listagem das músicas usadas em cada cena. Necessário 1 elemento por cena")] public List<MusicData> audiosInScene;
    [Tooltip("Duração dos fades, em segundos")] public float duration;
    List<AudioSource> audioSources;

    /// <summary>
    /// No Awake, checaremos se esse prefab já existe (veio de outra cena). Se existir, este é destruído (pra não ter duplicação).
    /// </summary>
    public void Awake(){
        if(instance != null) Destroy(gameObject); //já existe

        //É o único
        else{
            instance = this;
            DontDestroyOnLoad(gameObject); //seta para não ser destruído entre cenas

            //coloca audiosources na lista
            audioSources = new List<AudioSource>();
            foreach(MusicData data in audiosInScene){
                foreach(AudioSource source in data.music){
                    if(!audioSources.Contains(source)){
                        audioSources.Add(source);
                    }
                }
            }

            //como acabou de ser criado, faz o primeiro fade in dos áudios
            StartCoroutine(FadeAudios(SceneManager.GetActiveScene())); 

            //seta para sempre que a cena mudar, fazer fade
            SceneManager.sceneLoaded += new UnityEngine.Events.UnityAction<Scene, LoadSceneMode>((scene, mode) => StartCoroutine(FadeAudios(scene)));
        }
    }

    /// <summary>
    /// Baseado no método 1 desse cara aqui: https://johnleonardfrench.com/articles/how-to-fade-audio-in-unity-i-tested-every-method-this-ones-the-best/
    /// </summary>
    public IEnumerator FadeAudios(Scene scene){
        Dictionary<AudioSource, float> targetVolumes = new Dictionary<AudioSource, float>(); //correlação entre áudio e volume final

        float currentTime = 0; //usado para iterar
        string currentSceneName = scene.name; //pega nome da cena atual

        //catando audioSources
        foreach(MusicData data in audiosInScene){
            //primeiro passo: checar se é o data que queremos
            if(data.scene == currentSceneName){
                //se é, todos os listados aqui devem ter o volume 1 no final
                foreach(AudioSource source in data.music){ 
                    if(source.volume != 1){//checa se o volume já está em 1
                        targetVolumes.Add(source, 1); //caso contrário, seta fade in
                    }
                }

                //para cada um que não estiver, devemos setar o volume alvo como 0
                foreach(AudioSource source in audioSources){
                    if(!data.music.Contains(source)){
                        if(source.volume != 0){
                            targetVolumes.Add(source, 0);
                        }
                    }
                }
            }
        }

        //fades
        while(currentTime < duration){
            currentTime += Time.deltaTime; //atualiza o tempo

            //itera sobre cada elemento do targetVolumes
            foreach(KeyValuePair<AudioSource, float> element in targetVolumes){
                //element.Key é o audioSource, e element.Value é o volume alvo

                //podemos assumir que o volume inicial é o contrário do final
                float start = (element.Value == 0) ? 1 : 0;

                //fade
                element.Key.volume = Mathf.Lerp(start, element.Value, (currentTime/duration));
            }

            yield return new WaitForEndOfFrame(); //espera próximo frame para continuar o fade
        }
    }
}
