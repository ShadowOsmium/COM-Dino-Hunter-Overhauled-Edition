using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorLock : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleCursorLock();
        }

        // Auto-lock cursor when unpausing
        if (pauseMenu != null && !pauseMenu.activeSelf && Cursor.lockState != CursorLockMode.Locked)
        {
            LockCursor();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SceneForest" || scene.name == "SceneGorge" || scene.name == "SceneIce" ||
            scene.name == "SceneLava" || scene.name == "SceneLava2" || scene.name == "SceneSnow")
        {
            LockCursor();
        }
    }

    private void ToggleCursorLock()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            LockCursor();
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}