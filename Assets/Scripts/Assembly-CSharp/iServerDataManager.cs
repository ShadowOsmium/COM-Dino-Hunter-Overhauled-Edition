using System.Collections;
using System.Collections.Generic;
using Boomlagoon.JSON;
using LitJson;
using UnityEngine;

public class iServerDataManager : MonoBehaviour
{
	public enum kDataType
	{
		None,
		GC,
		Data
	}

	public delegate void OnSuccessFetch(string sData);

	public delegate void OnSuccessUpload();

	public delegate void OnFailed(string sCode);

	protected static iServerDataManager m_Instance;

	protected OnSuccessFetch m_OnSuccessFetch;

	protected OnSuccessUpload m_OnSuccessUpload;

	protected OnFailed m_OnFailed;

	protected string m_sServerName = string.Empty;

	protected string m_sServerUrl = string.Empty;

	protected string m_sKey = string.Empty;

	protected float m_fTimeOut = -1f;

	protected Dictionary<string, string> m_dictRandom;

	public iServerDataManager()
	{
		m_dictRandom = new Dictionary<string, string>();
	}

	public static iServerDataManager GetInstance()
	{
		if (m_Instance == null)
		{
			GameObject gameObject = new GameObject("_ServerDataManager");
			Object.DontDestroyOnLoad(gameObject);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			m_Instance = gameObject.AddComponent<iServerDataManager>();
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
	}

	public void SendFetchSaveData(string userid, OnSuccessFetch successfunc, OnFailed failedfunc)
	{
		m_OnSuccessFetch = successfunc;
		m_OnFailed = failedfunc;
		Hashtable hashtable = new Hashtable();
		hashtable.Add("userId", userid);
		HttpClient.Instance().SendRequest(m_sServerName, "userHandler.loadProfile&json=" + WWW.EscapeURL(JsonMapper.ToJson(hashtable)), "nodata", "_ServerDataManager", "iServerDataManager", "OnFetchSaveDataResult", null);
	}

	protected void OnFetchSaveDataResult(int taskId, int result, string server, string action, string response, string param)
	{
		UnityEngine.Debug.Log("OnFetchSaveDataResult " + result + " " + action + " " + response + " " + param);
		if (result != 0)
		{
			return;
		}
		string text = string.Empty;
		try
		{
			JSONObject jSONObject = JSONObject.Parse(response);
			text = jSONObject.GetString("code");
			if (text == "0")
			{
				string @string = jSONObject.GetString("userId");
				string string2 = jSONObject.GetString("rand");
				string text2 = string.Empty;
				if (jSONObject["profile"].Type == JSONValueType.Object)
				{
					JSONObject @object = jSONObject.GetObject("profile");
					text2 = ((@object["data"] == null || @object["data"].Type != 0) ? string.Empty : @object["data"].Str);
				}
				Debug.Log(string2 + " " + text2);
				SetUserRandom(@string, string2);
				if (m_OnSuccessFetch != null)
				{
					m_OnSuccessFetch(text2);
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
				Debug.Log("exception");
				m_OnFailed(text);
			}
		}
	}

	public void SendUploadSaveData(string userid, string sData, OnSuccessUpload successfunc, OnFailed failedfunc)
	{
		m_OnSuccessUpload = successfunc;
		m_OnFailed = failedfunc;
		Hashtable hashtable = new Hashtable();
		hashtable.Add("userId", userid);
		hashtable.Add("rand", GetUserRandom(userid));
		HttpClient.Instance().SendRequest(m_sServerName, "userHandler.saveProfile&json=" + WWW.EscapeURL(JsonMapper.ToJson(hashtable)), sData, "_ServerDataManager", "iServerDataManager", "OnUploadSaveDataResult", null);
	}

	protected void OnUploadSaveDataResult(int taskId, int result, string server, string action, string response, string param)
	{
		UnityEngine.Debug.Log("OnUploadSaveDataResult " + result + " " + action + " " + response);
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
				string str = jSONObject["userId"].Str;
				string str2 = jSONObject["rand"].Str;
				UnityEngine.Debug.Log("upload sucess random = " + str2);
				if (m_OnSuccessUpload != null)
				{
					m_OnSuccessUpload();
				}
			}
			else if (m_OnFailed != null)
			{
				Debug.Log("code != 0 error failed");
				m_OnFailed(text);
			}
		}
		catch
		{
			if (m_OnFailed != null)
			{
				Debug.Log("exception error failed");
				m_OnFailed(text);
			}
		}
	}

	protected void SetUserRandom(string userid, string random)
	{
		if (!m_dictRandom.ContainsKey(userid))
		{
			m_dictRandom.Add(userid, random);
		}
		else
		{
			m_dictRandom[userid] = random;
		}
	}

	protected string GetUserRandom(string userid)
	{
		if (!m_dictRandom.ContainsKey(userid))
		{
			return "000";
		}
		return m_dictRandom[userid];
	}
}
