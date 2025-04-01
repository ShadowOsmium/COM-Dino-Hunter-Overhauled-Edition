using UnityEngine;

public class iServerFile : MonoBehaviour
{
	public delegate void OnSuccess(string sFileData);

	public delegate void OnFailed();

	protected static iServerFile m_Instance;

	protected OnSuccess m_OnSuccess;

	protected OnFailed m_OnFailed;

	protected string m_sUrl = string.Empty;

	protected WWW m_www;

	protected float m_fTimeout;

	protected float m_fTimeoutCount;

	public static iServerFile Instance
	{
		get
		{
			if (m_Instance == null)
			{
				GameObject gameObject = new GameObject("_ServerFile");
				Object.DontDestroyOnLoad(gameObject);
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				m_Instance = gameObject.AddComponent<iServerFile>();
				DevicePlugin.InitAndroidPlatform();
				gameObject.AddComponent<TrinitiAdAndroidPlugin>();
				gameObject.AddComponent<AndroidQuit>();
			}
			return m_Instance;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (m_www == null)
		{
			return;
		}
		if (m_www.isDone)
		{
			if (m_www.error != null)
			{
				if (m_OnFailed != null)
				{
					m_OnFailed();
				}
			}
			else if (m_OnSuccess != null)
			{
				m_OnSuccess(m_www.text);
			}
			m_www = null;
		}
		else
		{
			if (!(m_fTimeout > 0f))
			{
				return;
			}
			m_fTimeoutCount += Time.deltaTime;
			if (m_fTimeoutCount >= m_fTimeout)
			{
				if (m_OnFailed != null)
				{
					m_OnFailed();
				}
				m_www = null;
			}
		}
	}

	public void Visit(string url, OnSuccess onsuccess, OnFailed onfailed, float timeout = -1f)
	{
		m_sUrl = url;
		m_OnSuccess = onsuccess;
		m_OnFailed = onfailed;
		m_fTimeout = timeout;
		m_fTimeoutCount = 0f;
		m_www = new WWW(m_sUrl + "?rand=" + Random.Range(10, 99999));
	}
}
