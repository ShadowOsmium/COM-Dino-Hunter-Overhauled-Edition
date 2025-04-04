using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorLock : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;
    private bool isManuallyUnlocked = false;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        // On startup, lock the cursor if required.
        if (IsSceneThatRequiresCursorLock(SceneManager.GetActiveScene().name))
            LockCursor();
        else
            UnlockCursor();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();

        if (Input.GetKeyDown(KeyCode.F1))
            ToggleCursorLock();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!isPaused && !isManuallyUnlocked && IsSceneThatRequiresCursorLock(scene.name))
            LockCursor();
        else
            UnlockCursor();
    }

    private bool IsSceneThatRequiresCursorLock(string sceneName)
    {
        return sceneName == "SceneForest" || sceneName == "SceneGorge" || 
               sceneName == "SceneIce" || sceneName == "SceneLava" || 
               sceneName == "SceneLava2" || sceneName == "SceneSnow";
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (pauseMenu != null)
            pauseMenu.SetActive(isPaused);

        if (isPaused)
        {
            UnlockCursor(); // Show cursor when paused.
        }
        else
        {
            // On unpause, force-lock the cursor (if the scene requires it) and clear manual override.
            if (IsSceneThatRequiresCursorLock(SceneManager.GetActiveScene().name))
            {
                LockCursor();
                isManuallyUnlocked = false;
            }
        }
    }

    private void ToggleCursorLock()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            UnlockCursor();
            isManuallyUnlocked = true;
        }
        else
        {
            LockCursor();
            isManuallyUnlocked = false;
        }
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}