using UnityEngine;

public class CSoundScene
{
	protected static CSoundScene m_Instance;

	protected TAudioController m_AudioController;

	protected string m_sBGM;

	protected string m_sBGMInterlude;

	protected string m_sAmbienceBGM;

	protected string m_sAmbience;

	protected TAudioController AudioController
	{
		get
		{
			if (m_AudioController == null)
			{
				GameObject gameObject = new GameObject("SceneAudioController");
				if (gameObject != null)
				{
					m_AudioController = gameObject.AddComponent<TAudioController>();
				}
			}
			return m_AudioController;
		}
	}

	public static CSoundScene GetInstance()
	{
		if (m_Instance == null)
		{
			m_Instance = new CSoundScene();
			m_Instance.Initialize();
		}
		return m_Instance;
	}

	public void Initialize()
	{
		Clear();
	}

	public void Clear()
	{
		m_sBGM = string.Empty;
		m_sBGMInterlude = string.Empty;
		m_sAmbienceBGM = string.Empty;
		m_sAmbience = string.Empty;
	}

	public void Destroy()
	{
		if (m_AudioController != null)
		{
			if (m_sBGM.Length > 0)
			{
				m_AudioController.StopAudio(m_sBGM);
			}
			if (m_sBGMInterlude.Length > 0)
			{
				m_AudioController.StopAudio(m_sBGMInterlude);
			}
			if (m_sAmbienceBGM.Length > 0)
			{
				m_AudioController.StopAudio(m_sAmbienceBGM);
			}
			if (m_sAmbience.Length > 0)
			{
				m_AudioController.StopAudio(m_sAmbience);
			}
			Object.Destroy(m_AudioController.gameObject);
			m_AudioController = null;
		}
		Clear();
	}

	public void PlayBGM(string sBGM = "")
	{
		if (AudioController == null)
		{
			return;
		}
		if (sBGM.Length > 0)
		{
			if (m_sBGM.Length > 0)
			{
				AudioController.StopAudio(m_sBGM);
			}
			m_sBGM = sBGM;
		}
		if (m_sBGM.Length > 0)
		{
			AudioController.PlayAudio(m_sBGM);
		}
	}

	public void StopBGM()
	{
		if (m_sBGM.Length > 0)
		{
			AudioController.StopAudio(m_sBGM);
		}
	}

	public void PlayAmbienceBGM(string sAmbienceBGM = "")
	{
		if (AudioController == null)
		{
			return;
		}
		if (sAmbienceBGM.Length > 0)
		{
			if (m_sAmbienceBGM.Length > 0)
			{
				AudioController.StopAudio(m_sAmbienceBGM);
			}
			m_sAmbienceBGM = sAmbienceBGM;
		}
		if (m_sAmbienceBGM.Length > 0)
		{
			AudioController.PlayAudio(m_sAmbienceBGM);
		}
	}

	public void StopAmbienceBGM()
	{
		if (m_sAmbienceBGM.Length > 0)
		{
			AudioController.StopAudio(m_sAmbienceBGM);
		}
	}

	public void PlayBGMInterlude(string sBGMInterlude)
	{
		if (!(AudioController == null))
		{
			if (m_sBGM.Length > 0)
			{
				AudioController.StopAudio(m_sBGM);
			}
			if (m_sBGMInterlude.Length > 0)
			{
				AudioController.StopAudio(m_sBGMInterlude);
			}
			m_sBGMInterlude = sBGMInterlude;
			if (m_sBGMInterlude.Length > 0)
			{
				AudioController.PlayAudio(m_sBGMInterlude);
			}
		}
	}

	public void StopBGMInterlude()
	{
		if (!(AudioController == null))
		{
			if (m_sBGMInterlude.Length > 0)
			{
				AudioController.StopAudio(m_sBGMInterlude);
			}
			m_sBGMInterlude = string.Empty;
			if (m_sBGM.Length > 0)
			{
				AudioController.PlayAudio(m_sBGM);
			}
		}
	}

	public void PlayAmbience(string sAmbiance)
	{
		if (!(AudioController == null))
		{
			if (m_sAmbience.Length < 1)
			{
				AudioController.StopAudio(m_sAmbience);
			}
			m_sAmbience = sAmbiance;
			AudioController.PlayAudio(m_sAmbience);
		}
	}

	public void StopAmbience()
	{
		if (!(AudioController == null))
		{
			if (m_sAmbience.Length < 1)
			{
				AudioController.StopAudio(m_sAmbience);
			}
			m_sAmbience = string.Empty;
		}
	}
}
