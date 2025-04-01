using System;
using EventCenter;
using UnityEngine;

public class iLoginManager : MonoBehaviour
{
	public enum kFailedType
	{
		None,
		VersionError,
		ServerMaintain,
		GameCenterChanged,
		FetchFailed,
		GMUsing,
		Timeout
	}

	public enum kState
	{
		None,
		StartApp,
		BackToApp,
		Success,
		Failed,
		Timeout
	}

	public delegate void OnSuccess();

	public delegate void OnFailed(kFailedType failedtype);

	public delegate void OnNetError();

	protected static iLoginManager m_Instance;

	protected kFailedType m_FailedType;

	protected OnSuccess m_OnSuccess;

	protected OnFailed m_OnFailed;

	protected OnNetError m_OnNetError;

	protected kState m_State;

	protected float m_fTimeout;

	protected float m_fTimeoutCount;

	protected string m_sGameCenterCache;

	protected iDataCenterNet m_DataCenterNet
	{
		get
		{
			if (iGameApp.GetInstance().m_GameData == null)
			{
				return null;
			}
			return iGameApp.GetInstance().m_GameData.m_DataCenterNet;
		}
	}

	public kState LoginState
	{
		get
		{
			return m_State;
		}
	}

	public static iLoginManager GetInstance()
	{
		if (m_Instance == null)
		{
			GameObject gameObject = new GameObject("_LoginManager");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			m_Instance = gameObject.AddComponent<iLoginManager>();
		}
		return m_Instance;
	}

	private void Awake()
	{
		m_State = kState.None;
		m_sGameCenterCache = string.Empty;
	}

	private void Start()
	{
		OnFetchConfigSuccess();
	}

	private void Update()
	{
		Update(Time.deltaTime);
	}

	public void StartApp(OnSuccess onsuccess, OnFailed onfailed, OnNetError onneterror)
	{
		m_State = kState.StartApp;
		m_OnSuccess = onsuccess;
		m_OnFailed = onfailed;
		m_OnNetError = onneterror;
		iServerVerify.GetInstance().ConnectServer("3.1.7a", OnVerifySuccess, OnVerifyFailed, OnVerifyNetError, 10f, 0f);
		m_fTimeout = 60f;
		m_fTimeoutCount = 0f;
		//global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ChangeText, "Connecting to the Internet..."));
	}

	public void RestartApp()
	{
		m_State = kState.StartApp;
		m_fTimeout = 60f;
		m_fTimeoutCount = 0f;
		//iServerVerify.GetInstance().ConnectServer("3.1.7a", OnVerifySuccess, OnVerifyFailed, OnVerifyNetError, 10f, 0f);
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ChangeText, "Connecting to the Internet..."));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_HidePopupWarnning));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_HidePopupServer));
	}

	public void BackToApp(OnSuccess onsuccess, OnFailed onfailed, OnNetError onneterror)
	{
		m_State = kState.BackToApp;
		m_OnSuccess = onsuccess;
		m_OnFailed = onfailed;
		m_OnNetError = onneterror;
		m_fTimeout = 60f;
		m_fTimeoutCount = 0f;
		iServerVerify.GetInstance().ConnectServer("3.1.7a", OnVerifySuccess, OnVerifyFailed, OnVerifyNetError, 10f, 0f);
	}

	protected void Update(float deltaTime)
	{
		if (m_State == kState.StartApp || m_State == kState.BackToApp)
		{
			m_fTimeoutCount += deltaTime;
			if (m_fTimeoutCount >= m_fTimeout)
			{
				OnLoginFailed(kFailedType.Timeout);
				m_State = kState.Timeout;
			}
		}
	}

	protected void OnLoginSuccess()
	{
		if (m_State != kState.Timeout)
		{
			m_State = kState.Success;
			if (m_OnSuccess != null)
			{
				m_OnSuccess();
			}
			iServerIAPVerifyBackground.GetInstance().SetActive(true);
		}
	}

	protected void OnLoginFailed(kFailedType type)
	{
		if (m_State != kState.Timeout)
		{
			m_State = kState.Failed;
			if (m_OnFailed != null)
			{
				m_OnFailed(type);
			}
		}
	}

	protected void OnLoginNetError()
	{
		if (m_State != kState.Timeout)
		{
			m_State = kState.Failed;
			if (m_OnNetError != null)
			{
				m_OnNetError();
			}
		}
	}

	protected void OnVerifySuccess()
	{
		m_DataCenterNet.Save();
		if (m_State == kState.Timeout)
		{
			return;
		}
		iServerVerify.CServerConfigInfo serverConfigInfo = iServerVerify.GetInstance().GetServerConfigInfo();
		if (serverConfigInfo == null)
		{
			return;
		}
		if (serverConfigInfo.m_ServerState == 0)
		{
			iServerVerify.CServerConfigInfo.CServerInfo cServerInfo = null;
			cServerInfo = serverConfigInfo.GetServerInfo("dataserver");
			if (cServerInfo != null)
			{
				iServerLogin.GetInstance().Initialize(cServerInfo.sName, cServerInfo.sUrl, cServerInfo.sKey, cServerInfo.fTimeOut);
				if (m_DataCenterNet != null && m_DataCenterNet.m_sGameCenterCache.Length > 0)
				{
					m_sGameCenterCache = m_DataCenterNet.m_sGameCenterCache;
					OnGameCenterLoginCache(m_DataCenterNet.m_sGameCenterCache);
				}
				else
				{
					m_sGameCenterCache = string.Empty;
				}
				iGameCenter.GetInstance().Login(OnGameCenterLoginSuccess, OnGameCenterLoginFailed);
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ChangeText, "Connecting to Game Center..."));
			}
			cServerInfo = serverConfigInfo.GetServerInfo("iapserver");
			if (cServerInfo != null)
			{
				iIAPManager.GetInstance().Initialize(cServerInfo.sName, cServerInfo.sUrl, cServerInfo.sKey, cServerInfo.fTimeOut);
				iServerIAPVerify.GetInstance().Initialize(cServerInfo.sName, cServerInfo.sUrl, cServerInfo.sKey, cServerInfo.fTimeOut);
				iServerTime.GetInstance().Initialize("servertime", cServerInfo.sUrl, cServerInfo.sKey, cServerInfo.fTimeOut);
			}
			cServerInfo = serverConfigInfo.GetServerInfo("collectserver");
			if (cServerInfo != null)
			{
				iTrinitiDataCollect.GetInstance().Initialize(cServerInfo.sName, cServerInfo.sUrl, cServerInfo.sKey, cServerInfo.fTimeOut);
				CFlurryManager.GetInstance().EnterApp();
			}
		}
		else if (serverConfigInfo.m_ServerState == 1)
		{
			m_FailedType = kFailedType.ServerMaintain;
			OnLoginFailed(m_FailedType);
		}
	}

	protected void OnVerifyFailed()
	{
		if (m_State != kState.Timeout)
		{
			m_FailedType = kFailedType.VersionError;
			OnLoginFailed(m_FailedType);
		}
	}

	protected void OnVerifyNetError()
	{
		if (m_State != kState.Timeout)
		{
			OnLoginNetError();
		}
	}

	protected void OnGameCenterLoginCache(string sAccount)
	{
		if (m_State != kState.Timeout)
		{
			Debug.Log(string.Concat("OnGameCenterLoginCache ", m_State, " account = ", sAccount));
			if (m_State == kState.BackToApp)
			{
				OnFetchSuccess();
			}
			else
			{
				iServerSaveData.GetInstance().Fetch("dinohunter", "3.1.7a", MyUtils.GetUUID(), sAccount, OnFetchSuccess, OnFetchFailed, iGameApp.GetInstance().PackSaveData, iGameApp.GetInstance().UnPackSaveData);
			}
		}
	}

	protected void OnGameCenterLoginSuccess(string sAccount)
	{
		if (m_State == kState.Timeout)
		{
			return;
		}
		Debug.Log(string.Concat("OnGameCenterLoginSuccess ", m_State, " account = ", sAccount, " cacheaccount = ", m_sGameCenterCache));
		if (m_DataCenterNet != null)
		{
			m_DataCenterNet.m_sGameCenterCache = sAccount;
			m_DataCenterNet.Save();
		}
		if ((m_sGameCenterCache.Length > 0 && m_sGameCenterCache != sAccount) || (m_State == kState.BackToApp && !iServerSaveData.GetInstance().CheckGCAccountIsValid(sAccount)))
		{
			Debug.Log("change gc");
			m_FailedType = kFailedType.GameCenterChanged;
			OnLoginFailed(m_FailedType);
		}
		else if (m_sGameCenterCache.Length < 1)
		{
			if (m_State == kState.BackToApp)
			{
				OnFetchSuccess();
				return;
			}
			iServerSaveData.GetInstance().Fetch("dinohunter", "3.1.7a", MyUtils.GetUUID(), sAccount, OnFetchSuccess, OnFetchFailed, iGameApp.GetInstance().PackSaveData, iGameApp.GetInstance().UnPackSaveData);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ChangeText, "Loading save files..."));
		}
	}

	protected void OnGameCenterLoginFailed()
	{
		if (m_State == kState.Timeout)
		{
			return;
		}
		Debug.Log(string.Concat("OnGameCenterLoginFailed ", m_State, " cacheaccount = ", m_sGameCenterCache));
		if (m_sGameCenterCache.Length > 0 || (m_State == kState.BackToApp && !iServerSaveData.GetInstance().CheckGCAccountIsValid(string.Empty)))
		{
			m_FailedType = kFailedType.GameCenterChanged;
			OnLoginFailed(m_FailedType);
		}
		else if (m_sGameCenterCache.Length < 1)
		{
			if (m_State == kState.BackToApp)
			{
				OnFetchSuccess();
				return;
			}
			iServerSaveData.GetInstance().Fetch("dinohunter", "3.1.7a", MyUtils.GetUUID(), string.Empty, OnFetchSuccess, OnFetchFailed, iGameApp.GetInstance().PackSaveData, iGameApp.GetInstance().UnPackSaveData);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ChangeText, "Loading save files..."));
		}
	}

	protected void OnFetchSuccess()
	{
		if (m_State != kState.Timeout)
		{
			iServerConfigData.GetInstance().Fetch(OnFetchConfigSuccess, OnFetchConfigFailed);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ChangeText, "Loading config files..."));
		}
	}

	protected void OnFetchFailed(string code)
	{
		if (m_State != kState.Timeout)
		{
			if (code == "gmOperating")
			{
				m_FailedType = kFailedType.GMUsing;
			}
			else
			{
				m_FailedType = kFailedType.FetchFailed;
			}
			OnLoginFailed(m_FailedType);
		}
	}

	protected void OnFetchConfigSuccess()
	{
		//if (m_State != kState.Timeout)
		//{
			iServerTime.GetInstance().GetServerTime(OnGetServerTime_S, OnGetServerTime_F);
		//}
	}

	protected void OnFetchConfigFailed()
	{
		if (m_State != kState.Timeout)
		{
			Debug.Log("OnFetchConfigFailed");
			m_FailedType = kFailedType.FetchFailed;
			OnLoginFailed(m_FailedType);
		}
	}

	protected void OnGetServerTime_S(double millionseconds)
	{
		//if (m_State == kState.Timeout)
		//{
		//	return;
		//}
		iGameData gameData = iGameApp.GetInstance().m_GameData;
		if (gameData == null)
		{
			return;
		}
		iGameState gameState = iGameApp.GetInstance().m_GameState;
		if (gameState == null)
		{
			return;
		}
		iDataCenter dataCenter = gameData.GetDataCenter();
		if (dataCenter != null)
		{
			DateTime dateTime = new DateTime(1970, 1, 1).AddMilliseconds(millionseconds);
			dataCenter.RefreshServerDateTime(dateTime);
			if (gameData.m_BlackMarketCenter != null)
			{
				gameData.m_BlackMarketCenter.InitBlackItem(dateTime);
			}
			OnLoginSuccess();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ChangeText, "Touch to play"));
		}
	}

	protected void OnGetServerTime_F()
	{
		if (m_State != kState.Timeout)
		{
			m_FailedType = kFailedType.None;
			OnLoginFailed(m_FailedType);
		}
	}
}
