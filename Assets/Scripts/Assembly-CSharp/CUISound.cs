using UnityEngine;

public class CUISound
{
	protected static CUISound m_Instance;

	protected TAudioController m_AudioController;

	protected TAudioController AudioController
	{
		get
		{
			if (m_AudioController == null)
			{
				GameObject gameObject = new GameObject("UIAudioController");
				if (gameObject != null)
				{
					Object.DontDestroyOnLoad(gameObject);
					m_AudioController = gameObject.AddComponent<TAudioController>();
				}
			}
			return m_AudioController;
		}
	}

	public static CUISound GetInstance()
	{
		if (m_Instance == null)
		{
			m_Instance = new CUISound();
		}
		return m_Instance;
	}

	public void Destroy()
	{
		if (m_AudioController != null)
		{
			Object.Destroy(m_AudioController.gameObject);
			m_AudioController = null;
		}
	}

	public void Play(string sName)
	{
		if (!(AudioController == null))
		{
			AudioController.PlayAudio(sName);
		}
	}

	public void Stop(string sName)
	{
		if (!(AudioController == null))
		{
			AudioController.StopAudio(sName);
		}
	}
}
