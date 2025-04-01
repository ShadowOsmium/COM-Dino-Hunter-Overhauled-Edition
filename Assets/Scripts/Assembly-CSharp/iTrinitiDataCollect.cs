using System.Collections;
using LitJson;
using UnityEngine;

public class iTrinitiDataCollect : MonoBehaviour
{
	protected static iTrinitiDataCollect m_Instance;

	protected string m_sServerName = string.Empty;

	protected string m_sServerUrl = string.Empty;

	protected string m_sKey = string.Empty;

	protected float m_fTimeOut = -1f;

	protected string m_sAppSymbol = string.Empty;

	protected string m_sUserSymbol = string.Empty;

	protected string m_sUserName = string.Empty;

	public static iTrinitiDataCollect GetInstance()
	{
		if (m_Instance == null)
		{
			GameObject gameObject = new GameObject("_TrinitiDataCollect");
			Object.DontDestroyOnLoad(gameObject);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			m_Instance = gameObject.AddComponent<iTrinitiDataCollect>();
		}
		return m_Instance;
	}

	public void Initialize(string sServerName, string sServerUrl, string sKey, float fTimeOut)
	{
		m_sServerName = sServerName;
		m_sServerUrl = sServerUrl;
		m_sKey = sKey;
		m_fTimeOut = fTimeOut;
		HttpClient.Instance().AddServer(m_sServerName, m_sServerUrl, m_fTimeOut, (m_sKey.Length >= 1) ? m_sKey : null);
	}

	public void SetAppSymbol(string sAppSymbol)
	{
		m_sAppSymbol = sAppSymbol;
	}

	public void SetUserSymbol(string sUserSymbol)
	{
		m_sUserSymbol = sUserSymbol;
	}

	public void SetUserName(string sUserName)
	{
		m_sUserName = sUserName;
	}

	public void logEvent(int eventname, Hashtable data)
	{
		if (m_sServerName.Length >= 1)
		{
			if (data == null)
			{
				data = new Hashtable();
			}
			data.Add("gamename", m_sAppSymbol);
			data.Add("uuid", m_sUserSymbol);
			data.Add("uname", m_sUserName);
			data.Add("action", eventname.ToString());
			HttpClient.Instance().SendRequest(m_sServerName, "logAllInfo", JsonMapper.ToJson(data), "_TrinitiDataCollect", "iTrinitiDataCollect", "OnRequest", null);
		}
	}

	public void logEvent(int eventname)
	{
		logEvent(eventname, new Hashtable());
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	protected void OnRequest(int taskId, int result, string server, string action, string response, string param)
	{
		if (result == 0)
		{
			UnityEngine.Debug.Log("TrinitiCollect OnRequest " + result + " " + action + " " + response + " " + param);
		}
	}
}
