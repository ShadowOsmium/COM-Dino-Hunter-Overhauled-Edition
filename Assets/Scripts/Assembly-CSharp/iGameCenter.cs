using System.Collections.Generic;
using UnityEngine;

public class iGameCenter : MonoBehaviour
{
	public enum kState
	{
		None,
		Login,
		Success,
		Failed
	}

	public delegate void OnFailed();

	public delegate void OnSuccess(string sLoginAccount);

	protected static iGameCenter m_Instance;

	protected OnFailed m_OnLoginFail;

	protected OnSuccess m_OnLoginSuccess;

	protected kState m_State;

	public static iGameCenter GetInstance()
	{
		if (m_Instance == null)
		{
			GameObject gameObject = new GameObject("_GameCenter");
			Object.DontDestroyOnLoad(gameObject);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			m_Instance = gameObject.AddComponent<iGameCenter>();
		}
		return m_Instance;
	}

	public bool IsSuccess()
	{
		return m_State == kState.Success;
	}

	public bool IsFailed()
	{
		return m_State == kState.Failed;
	}

	public string GetAccount()
	{
		return MyUtils.GetGCAccount();
	}

	public bool IsLogin()
	{
		return GameCenterPlugin.IsLogin();
	}

	public void Login(OnSuccess onsuccess, OnFailed onfailed)
	{
		Debug.Log("start login game center");
		if (GameCenterPlugin.IsLogin())
		{
			OnLogin(true);
			return;
		}
		m_OnLoginSuccess = onsuccess;
		m_OnLoginFail = onfailed;
		GameCenterPlugin.Login();
		m_State = kState.Login;
	}

	public bool GetFriends(ref List<string> ltFriends)
	{
		return true;
	}

	private void Awake()
	{
		m_State = kState.None;
		GameCenterPlugin.Initialize();
	}

	private void Start()
	{
	}

	private void Update()
	{
		Update(Time.deltaTime);
	}

	protected void Update(float deltaTime)
	{
		if (m_State == kState.Login)
		{
			OnLogin(false);
		}
	}

	protected void OnLogin(bool bSuccess)
	{
		if (bSuccess)
		{
			Debug.Log("GameCenter Login successed");
			m_State = kState.Success;
			return;
		}
		Debug.Log("GameCenter Login failed");
		if (m_OnLoginFail != null)
		{
			m_OnLoginFail();
		}
		m_State = kState.Failed;
	}
}
