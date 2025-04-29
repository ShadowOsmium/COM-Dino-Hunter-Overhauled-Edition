using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TAudioManager : MonoBehaviour
{
    private class AudioInfo
    {
        public ITAudioEvent audioEvt;
        public bool loop;
        public bool sfx;
        public float volume;
    }

    private Dictionary<AudioSource, AudioInfo> m_playAudios = new Dictionary<AudioSource, AudioInfo>();
    private Dictionary<string, List<ITAudioRule>> m_audio_rules = new Dictionary<string, List<ITAudioRule>>();
    private bool m_isMusicOn = true;
    private bool m_isSoundOn = true;
    private float m_musicVolume = 0.5f;
    private float m_soundVolume = 0.5f;
    private AudioListener audioListener;
    private static TAudioManager s_instance;

    public static TAudioManager instance
    
    {
        get
        {
            if (s_instance == null && Application.isPlaying)
            {
                GameObject target = new GameObject("TAudioManager", typeof(TAudioManager));
                s_instance = target.GetComponent<TAudioManager>();  // <-- this line is missing
                DontDestroyOnLoad(target);
            }
            return s_instance;
        }
    }

    public static bool checkInstance
    {
        get { return s_instance != null; }
    }

    public bool isMusicOn
    {
        get { return m_isMusicOn; }
        set
        {
            if (m_isMusicOn == value) return;
            m_isMusicOn = value;

            Dictionary<AudioSource, AudioInfo>.Enumerator enumerator = m_playAudios.GetEnumerator();
            while (enumerator.MoveNext())
            {
                KeyValuePair<AudioSource, AudioInfo> playAudio = enumerator.Current;
                if (!playAudio.Value.sfx)
                {
                    if (m_isMusicOn)
                        playAudio.Key.Play();
                    else
                        playAudio.Key.Pause();
                }
            }

            PlayerPrefs.SetInt("MusicOff", (!m_isMusicOn) ? 1 : 0);
        }
    }

    public bool isSoundOn
    {
        get { return m_isSoundOn; }
        set
        {
            if (m_isSoundOn == value) return;
            m_isSoundOn = value;

            Dictionary<AudioSource, AudioInfo>.Enumerator enumerator = m_playAudios.GetEnumerator();
            while (enumerator.MoveNext())
            {
                KeyValuePair<AudioSource, AudioInfo> playAudio = enumerator.Current;
                AudioInfo info = playAudio.Value;
                if (info.sfx)
                {
                    if (m_isSoundOn)
                    {
                        if (info.loop && info.audioEvt != null)
                            info.audioEvt.Trigger();
                    }
                    else
                    {
                        playAudio.Key.Stop();
                    }
                }
            }

            PlayerPrefs.SetInt("SoundOff", (!m_isSoundOn) ? 1 : 0);
        }
    }

    public float musicVolume
    {
        get { return m_musicVolume; }
        set
        {
            m_musicVolume = Mathf.Clamp01(value);
            foreach (var pair in m_playAudios)
            {
                if (!pair.Value.sfx)
                    pair.Key.volume = pair.Value.volume * m_musicVolume;
            }
            PlayerPrefs.SetFloat("MusicVolume", m_musicVolume);
            PlayerPrefs.Save();
        }
    }

    public float soundVolume
    {
        get { return m_soundVolume; }
        set
        {
            m_soundVolume = Mathf.Clamp01(value);
            foreach (var pair in m_playAudios)
            {
                if (pair.Value.sfx)
                    pair.Key.volume = pair.Value.volume * m_soundVolume;
            }
            PlayerPrefs.SetFloat("SFXVolume", m_soundVolume);
            PlayerPrefs.Save();
        }
    }

    private void Awake()
    {
        m_isMusicOn = PlayerPrefs.GetInt("MusicOff", 0) == 0;
        m_isSoundOn = PlayerPrefs.GetInt("SoundOff", 0) == 0;

        m_musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        m_soundVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        if (s_instance != null)
            Destroy(s_instance.gameObject);

        AudioListener listener = FindObjectOfType<AudioListener>();
        if (listener == null)
        {
            GameObject go = new GameObject("AudioListener", typeof(AudioListener));
            DontDestroyOnLoad(go);
            listener = go.GetComponent<AudioListener>();
        }

        audioListener = listener;
        s_instance = this;

        // Apply the saved volumes to all currently tracked audio sources
        //foreach (var pair in m_playAudios)
       //{
       //     var info = pair.Value;
       //     pair.Key.volume = info.volume * (info.sfx ? m_soundVolume : m_musicVolume);
       // }
    }

    private void OnDestroy()
    {
        s_instance = null;
    }

    private void Update()
    {
        List<AudioSource> toRemove = new List<AudioSource>();
        Dictionary<AudioSource, AudioInfo>.Enumerator enumerator = m_playAudios.GetEnumerator();
        while (enumerator.MoveNext())
        {
            KeyValuePair<AudioSource, AudioInfo> pair = enumerator.Current;
            if (pair.Value.audioEvt != null && !pair.Value.audioEvt.isPlaying)
                toRemove.Add(pair.Key);
        }

        for (int i = 0; i < toRemove.Count; i++)
            m_playAudios.Remove(toRemove[i]);
    }

    public void Pause(AudioSource audio)
    {
        audio.Pause();
    }

    public void PlaySound(AudioSource audio, AudioClip clip, bool loop)
    {
        PlaySound(audio, clip, loop, false);
    }

    public void PlaySound(AudioSource audio, AudioClip clip, bool loop, bool cutoff)
    {
        if (!TryPlay(audio.gameObject, clip.length / audio.pitch))
            return;

        AudioInfo info;
        if (!m_playAudios.TryGetValue(audio, out info))
        {
            info = new AudioInfo();
            info.audioEvt = audio.GetComponent<ITAudioEvent>();
            info.loop = loop;
            info.sfx = true;
            info.volume = audio.volume;
            m_playAudios.Add(audio, info);
        }
        else
        {
            info.volume = audio.volume;
        }

        audio.volume *= m_soundVolume;
        audio.loop = loop;
        audio.clip = clip;

        if (m_isSoundOn)
        {
            if (loop || cutoff)
                audio.Play();
            else
                audio.PlayOneShot(clip);
        }
    }

    public void StopSound(AudioSource audio)
    {
        audio.Stop();
        TryStop(audio.gameObject);
        m_playAudios.Remove(audio);
    }

    public void PlayMusic(AudioSource audio, AudioClip clip, bool loop)
    {
        PlayMusic(audio, clip, loop, false);
    }

    public void PlayMusic(AudioSource audio, AudioClip clip, bool loop, bool cutoff)
    {
        if (!TryPlay(audio.gameObject, clip.length / audio.pitch))
            return;

        AudioInfo info;
        if (!m_playAudios.TryGetValue(audio, out info))
        {
            info = new AudioInfo();
            info.audioEvt = audio.GetComponent<ITAudioEvent>();
            info.loop = loop;
            info.sfx = false;
            info.volume = audio.volume;
            m_playAudios.Add(audio, info);
        }
        else
        {
            info.volume = audio.volume;
        }

        audio.volume *= m_musicVolume;
        audio.loop = loop;
        audio.clip = clip;

        if (m_isMusicOn)
        {
            if (loop || cutoff)
                audio.Play();
            else
                audio.PlayOneShot(clip);
        }
    }

    public void StopMusic(AudioSource audio)
    {
        audio.Stop();
        TryStop(audio.gameObject);
        m_playAudios.Remove(audio);
    }

    public void StopAll()
    {
        List<AudioInfo> allAudios = new List<AudioInfo>();
        Dictionary<AudioSource, AudioInfo>.Enumerator enumerator = m_playAudios.GetEnumerator();
        while (enumerator.MoveNext())
        {
            allAudios.Add(enumerator.Current.Value);
        }
        for (int i = 0; i < allAudios.Count; i++)
        {
            if (allAudios[i].audioEvt != null)
                allAudios[i].audioEvt.Stop();
        }
        m_playAudios.Clear();
    }

    private bool TryPlay(GameObject go, float length)
    {
        string name = go.name;
        int cloneIndex = name.IndexOf("(Clone)");
        if (cloneIndex >= 0)
            name = name.Substring(0, cloneIndex);

        List<ITAudioRule> rules;
        if (!m_audio_rules.TryGetValue(name, out rules))
            return true;

        float overTime = Time.realtimeSinceStartup + length;
        for (int i = 0; i < rules.Count; i++)
        {
            if (!rules[i].Try(name, go, overTime))
                return false;
        }
        return true;
    }

    private void TryStop(GameObject go)
    {
        string name = go.name;
        int cloneIndex = name.IndexOf("(Clone)");
        if (cloneIndex >= 0)
            name = name.Substring(0, cloneIndex);

        List<ITAudioRule> rules;
        if (!m_audio_rules.TryGetValue(name, out rules))
            return;

        for (int i = 0; i < rules.Count; i++)
        {
            rules[i].Stop(go);
        }
    }

    public void RegistRule(string name, ITAudioRule rule)
    {
        List<ITAudioRule> rules;
        if (!m_audio_rules.TryGetValue(name, out rules))
        {
            rules = new List<ITAudioRule>();
            m_audio_rules.Add(name, rules);
        }
        rules.Add(rule);
    }

    public void ClearRegistRule()
    {
        m_audio_rules.Clear();
    }

    // NEW VOLUME CONTROL METHODS (for UI buttons or hotkeys)
    public void AdjustMusicVolume(float delta)
    {
        musicVolume = Mathf.Clamp01(musicVolume + delta);
    }

    public void AdjustSoundVolume(float delta)
    {
        soundVolume = Mathf.Clamp01(soundVolume + delta);
    }

    public void ApplySavedVolumesToAllSources()
    {
        foreach (var pair in m_playAudios)
        {
            AudioSource source = pair.Key;
            var info = pair.Value;

            if (info.sfx)
                source.volume = info.volume * m_soundVolume;
            else
                source.volume = info.volume * m_musicVolume;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplySavedVolumesToAllSources();
    }
}
