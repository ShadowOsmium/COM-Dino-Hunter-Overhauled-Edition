using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnhancer : MonoBehaviour
{
    void Awake()
    {
        if (Application.isPlaying)
        {
            DontDestroyOnLoad(this.gameObject); // Persist between scene loads
        }
    }

    void OnEnable()
    {
        if (!Application.isPlaying) return;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        if (!Application.isPlaying) return;

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "SceneLoad" || currentScene == "Scene_mainmenu")
        {
            CallClampAndFix();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "SceneLoad" || scene.name == "Scene_mainmenu")
        {
            CallClampAndFix();
        }
    }

    void CallClampAndFix()
    {
        iDataCenter data = iGameApp.GetInstance().m_GameData.m_DataCenter;
        if (data != null)
        {
            data.ClampToLimits();
        }
        else
        {
            Debug.LogWarning("DataCenter is null!");
        }
    }
}