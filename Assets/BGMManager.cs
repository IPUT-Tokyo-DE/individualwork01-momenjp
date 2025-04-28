using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip titleBGM;
    public AudioClip gameBGM;

    private static BGMManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ← これ！シーンを超えて生き残る！
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        PlayBGMForCurrentScene();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGMForCurrentScene();
    }

    void PlayBGMForCurrentScene()
    {
        if (audioSource == null) return;

        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "TitleScene")
        {
            if (audioSource.clip != titleBGM)
            {
                audioSource.clip = titleBGM;
                audioSource.Play();
            }
        }
        else if (sceneName == "GameScene")
        {
            if (audioSource.clip != gameBGM)
            {
                audioSource.clip = gameBGM;
                audioSource.Play();
            }
        }
    }
}
