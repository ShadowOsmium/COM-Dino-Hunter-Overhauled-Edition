using UnityEngine;
using UnityEngine.SceneManagement;

public class AvatarLevelEnforcer : MonoBehaviour
{
    private const int MaxAvatarLevel = 5;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SceneLoad")
        {
            EnforceAvatarLevelClamp();
        }
    }

    void EnforceAvatarLevelClamp()
    {
        var data = iGameApp.GetInstance().m_GameData.m_DataCenter;

        if (data == null)
        {
            return;
        }

        for (int avatarID = 0; avatarID <= 804; avatarID++)
        {
            // Skip Avatar Stone IDs (701 to 707)
            if (avatarID >= 701 && avatarID <= 707)
                continue;

            int avatarLevel = -1;

            // Call GetAvatar normally (uses int)
            if (data.GetAvatar(avatarID, ref avatarLevel))
            {
                if (avatarLevel > MaxAvatarLevel)
                {

                    // Pass clamped value back as int, conversion handled internally
                    data.SetAvatar(avatarID, MaxAvatarLevel);
                }
            }
        }
    }
}