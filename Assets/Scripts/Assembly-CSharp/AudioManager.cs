using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource audioSource;

    public float currentVolume = 1f;
    internal float volume;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>(); // Assumes the AudioSource is attached to this GameObject
        LoadVolumeSettings();
    }

    public void SetVolume(float volume)
    {
        currentVolume = Mathf.Clamp(volume, 0f, 1f);
        audioSource.volume = currentVolume;
        PlayerPrefs.SetFloat("AudioVolume", currentVolume);
    }

    public void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("AudioVolume"))
            SetVolume(PlayerPrefs.GetFloat("AudioVolume"));
    }
}
