using System;
using System.Collections;
using Boomlagoon.JSON;
using LitJson;
using UnityEngine;

public class iServerLogin : MonoBehaviour
{
	public delegate void OnSuccessFetch(string serverId, string serverUrl, string deviceId, string userId, string gamecenterId);

	public delegate void OnFailed(string code);

	protected static iServerLogin m_Instance;

	protected OnSuccessFetch m_OnSuccessFetch;

	protected OnFailed m_OnFailed;

	protected string m_sServerName = string.Empty;

	protected string m_sServerUrl = string.Empty;

	protected string m_sKey = string.Empty;

	protected float m_fTimeOut = -1f;

	public static iServerLogin GetInstance()
	{
		if (m_Instance == null)
		{
			GameObject gameObject = new GameObject("_ServerLogin");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			m_Instance = gameObject.AddComponent<iServerLogin>();
		}
		return m_Instance;
	}

	public void Initialize(string sServerName, string sServerUrl, string sKey, float fTimeOut)
	{
		m_sServerName = sServerName;
		m_sServerUrl = sServerUrl;
		m_sKey = sKey;
		m_fTimeOut = fTimeOut;
		m_fTimeOut = -1f;
		HttpClient.Instance().AddServer(m_sServerName, m_sServerUrl, m_fTimeOut, (m_sKey.Length >= 1) ? m_sKey : null);
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
		Update(Time.deltaTime);
	}

	public void Update(float deltaTime)
	{
		HttpClient.Instance().HandleResponse();
	}

	public void Login(string game, string version, string deviceId, string facebookId, string gamecenterId, OnSuccessFetch successfunc, OnFailed failedfunc)
	{
		UnityEngine.Debug.Log(game + " " + version + " " + deviceId + " " + gamecenterId);
		m_OnSuccessFetch = successfunc;
		m_OnFailed = failedfunc;
		Hashtable hashtable = new Hashtable();
		hashtable.Add("game", game);
		hashtable.Add("version", version);
		hashtable.Add("deviceId", deviceId);
		hashtable.Add("facebookId", facebookId);
		hashtable.Add("gamecenterId", gamecenterId);
		HttpClient.Instance().SendRequest(m_sServerName, "multiCsHandler.enter", JsonMapper.ToJson(hashtable), "_ServerLogin", "iServerLogin", "OnLoginResult", null);
	}

	protected void OnLoginResult(int taskId, int result, string server, string action, string response, string param)
	{
		UnityEngine.Debug.Log("OnLoginResult " + result + " " + action + " " + response + " " + param);
		if (result != 0)
		{
			return;
		}
		string text = string.Empty;
		try
		{
			JSONObject jSONObject = JSONObject.Parse(response);
			text = jSONObject.GetString("code");
			UnityEngine.Debug.Log(text);
			if (text == "0")
			{
				string @string = jSONObject.GetString("serverId");
				string string2 = jSONObject.GetString("serverUrl");
				JSONObject @object = jSONObject.GetObject("user");
				if (@object == null)
				{
					throw new Exception();
				}
				string string3 = @object.GetString("deviceId");
				string string4 = @object.GetString("userId");
				string string5 = @object.GetString("gamecenterId");
				UnityEngine.Debug.Log("serverId = " + @string);
				UnityEngine.Debug.Log("serverUrl = " + string2);
				UnityEngine.Debug.Log("deviceId = " + string3);
				UnityEngine.Debug.Log("userId = " + string4);
				UnityEngine.Debug.Log("gamecenterId = " + string5);
				if (m_OnSuccessFetch != null)
				{
					m_OnSuccessFetch(@string, string2, string3, string4, string5);
				}
			}
			else if (m_OnFailed != null)
			{
				m_OnFailed(text);
			}
		}
		catch
		{
			if (m_OnFailed != null)
			{
				UnityEngine.Debug.Log("exception");
				m_OnFailed(text);
			}
		}
	}
}
