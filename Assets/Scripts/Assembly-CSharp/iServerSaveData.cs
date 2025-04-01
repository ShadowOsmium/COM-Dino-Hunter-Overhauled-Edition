using System;
using System.Collections;
using LitJson;
using UnityEngine;

public class iServerSaveData : MonoBehaviour
{
	protected enum kState
	{
		None,
		Fetch,
		Successed,
		Failed
	}

	protected enum kStateUpload
	{
		None,
		Upload,
		Success,
		Failed
	}

	public delegate void OnEvent();

	public delegate void OnFailed(string code);

	public delegate string OnPackData();

	public delegate void OnDialog(string msg, OnEvent onok, OnEvent oncancel);

	public delegate bool OnUnPackData(string sData);

	protected static iServerSaveData m_Instance;

	protected OnEvent m_OnFetchSuccess;

	protected OnFailed m_OnFetchFailed;

	protected OnEvent m_OnUploadSuccess;

	protected OnFailed m_OnUploadFailed;

	protected OnPackData m_OnPackData;

	protected OnDialog m_OnDialog;

	protected OnUnPackData m_OnUnPackData;

	protected kState m_State;

	protected kStateUpload m_StateUpload;

	protected string m_sGameName = string.Empty;

	protected string m_sGameVersion = string.Empty;

	protected string m_sDeviceId = string.Empty;

	protected string m_sGameCenterId = string.Empty;

	protected string m_sCurDeviceId = string.Empty;

	protected string m_sCurGameCenterId = string.Empty;

	protected string m_sServerId = string.Empty;

	protected string m_sServerUrl = string.Empty;

	protected string m_sSaveData;

	protected bool m_bNeedUpload;

	protected float m_fCheckUploadTime = 60f;

	protected float m_fCheckUploadTimeCount;

	protected int m_nUploadFailedCur;

	protected int m_nUploadFailedMax = 3;

	protected bool m_bNeedNextUpload;

	protected bool m_bBackgroundUpload;

	protected bool m_bBackgroundBack;

	protected bool m_bBackgroundRelogin;

	protected float m_fGameInBackgroundTime;

	public bool m_bFirstRegister;

	public bool m_bCreateNewSave;

	protected iGameSceneBase m_GameScene
	{
		get
		{
			return iGameApp.GetInstance().m_GameScene;
		}
	}

	protected iGameData m_GameData
	{
		get
		{
			return iGameApp.GetInstance().m_GameData;
		}
	}

	protected iGameState m_GameState
	{
		get
		{
			return iGameApp.GetInstance().m_GameState;
		}
	}

	protected iDataCenter m_DataCenter
	{
		get
		{
			if (m_GameData == null)
			{
				return null;
			}
			return m_GameData.m_DataCenter;
		}
	}

	public string DeviceId
	{
		get
		{
			return m_sDeviceId;
		}
	}

	public string GameCenterId
	{
		get
		{
			return m_sGameCenterId;
		}
	}

	public string CurDeviceId
	{
		get
		{
			return m_sCurDeviceId;
		}
	}

	public string CurGameCenterId
	{
		get
		{
			return m_sCurGameCenterId;
		}
	}

	public bool IsBackgroundUpload
	{
		get
		{
			return m_bBackgroundUpload;
		}
		set
		{
			m_bBackgroundUpload = value;
		}
	}

	public bool IsBackgroundBack
	{
		get
		{
			return m_bBackgroundBack;
		}
		set
		{
			m_bBackgroundBack = value;
		}
	}

	public bool IsBackgroundRelogin
	{
		get
		{
			return m_bBackgroundRelogin;
		}
		set
		{
			m_bBackgroundRelogin = value;
		}
	}

	public static iServerSaveData GetInstance()
	{
		if (m_Instance == null)
		{
			GameObject gameObject = new GameObject("_ServerSaveData");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			m_Instance = gameObject.AddComponent<iServerSaveData>();
		}
		return m_Instance;
	}

	public bool IsFetchSuccess()
	{
		return m_State == kState.Successed;
	}

	public bool IsFetchFailed()
	{
		return m_State == kState.Failed;
	}

	public bool IsUploadSuccess()
	{
		return m_StateUpload == kStateUpload.Success;
	}

	public bool IsUploadFailed()
	{
		return m_StateUpload == kStateUpload.Failed;
	}

	public string GetDeviceInfoDesc()
	{
		string text = m_sDeviceId;
		if (m_sCurGameCenterId.Length > 0 && m_sCurGameCenterId == m_sGameCenterId)
		{
			text = "*" + m_sDeviceId;
		}
		if (m_sCurDeviceId.Length > 0 && m_sCurDeviceId != m_sDeviceId)
		{
			text = text + " " + m_sCurDeviceId;
		}
		return text;
	}

	public string GetDeviceInfoTitle()
	{
		return "ANDROID VERSOIN: " + DevicePlugin.GetSysVersion();
	}

	protected void OnFetchDataSuccess()
	{
		m_State = kState.Successed;
		if (m_OnFetchSuccess != null)
		{
			m_OnFetchSuccess();
		}
	}

	protected void OnFetchDataFailed(string sCode)
	{
		m_State = kState.Failed;
		if (m_OnFetchFailed != null)
		{
			m_OnFetchFailed(sCode);
		}
	}

	protected void OnUploadDataSuccess()
	{
		m_StateUpload = kStateUpload.Success;
		if (m_OnUploadSuccess != null)
		{
			m_OnUploadSuccess();
		}
		Debug.Log("upload success");
	}

	protected void OnUploadDataFailed(string code)
	{
		m_StateUpload = kStateUpload.Failed;
		if (m_OnUploadFailed != null)
		{
			m_OnUploadFailed(code);
		}
		Debug.Log("upload failed");
	}

	protected bool PackData(ref string sData)
	{
		if (m_OnPackData == null)
		{
			return false;
		}
		try
		{
			sData = m_OnPackData();
			return true;
		}
		catch
		{
			return false;
		}
	}

	protected bool UnPackData(string sData)
	{
		if (m_OnUnPackData == null)
		{
			return false;
		}
		try
		{
			return m_OnUnPackData(sData);
		}
		catch
		{
			return false;
		}
	}

	protected void ShowDialog(string msg, OnEvent onok, OnEvent oncancel)
	{
		if (m_OnDialog == null)
		{
			return;
		}
		try
		{
			m_OnDialog(msg, onok, oncancel);
		}
		catch
		{
			Debug.Log("exception ShowDialog " + msg);
		}
	}

	public void SetCurGCAccount(string sAccount)
	{
		m_sGameCenterId = sAccount;
	}

	public bool CheckGCAccountIsValid(string sAccount)
	{
		return m_sGameCenterId == sAccount;
	}

	public void SetDialog(OnDialog ondialog)
	{
		m_OnDialog = ondialog;
	}

	public void Fetch(string sGameName, string sGameVersion, string sDeviceId, string sGameCenterId, OnEvent onsuccess, OnFailed onfailed, OnPackData onpack, OnUnPackData onunpack)
	{
		UnityEngine.Debug.Log("start to fetch");
		m_sGameName = sGameName;
		m_sGameVersion = sGameVersion;
		m_sDeviceId = sDeviceId;
		m_sGameCenterId = sGameCenterId;
		m_OnFetchSuccess = onsuccess;
		m_OnFetchFailed = onfailed;
		m_OnPackData = onpack;
		m_OnUnPackData = onunpack;
		m_State = kState.Fetch;
		iServerLogin.GetInstance().Login(m_sGameName, m_sGameVersion, m_sDeviceId, string.Empty, m_sGameCenterId, Step_Login_S, Step_Login_F);
	}

	public void Upload(OnEvent onsuccess, OnFailed onfailed, OnPackData onpack)
	{
		m_OnUploadSuccess = onsuccess;
		m_OnUploadFailed = onfailed;
		m_OnPackData = onpack;
		m_OnUnPackData = null;
		if (PackData(ref m_sSaveData))
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("data", m_sSaveData);
			iServerDataManager.GetInstance().SendUploadSaveData(m_sCurDeviceId, JsonMapper.ToJson(hashtable), OnUploadDataSuccess, OnUploadDataFailed);
			m_StateUpload = kStateUpload.Upload;
			m_fCheckUploadTimeCount = 0f;
		}
	}

	public void SetActive(bool bActive)
	{
		m_bNeedUpload = bActive;
	}

	public void UploadImmidately()
	{
		m_StateUpload = kStateUpload.None;
		m_bNeedNextUpload = true;
	}

	private void Awake()
	{
		string sysVersion = DevicePlugin.GetSysVersion();
		Debug.Log(sysVersion);
		m_State = kState.None;
	}

	private void Start()
	{
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		Update(deltaTime);
		CTrinitiCollectManager.GetInstance().Update(deltaTime);
		if (m_GameData != null && m_GameData.m_BlackMarketCenter != null)
		{
			m_GameData.m_BlackMarketCenter.Update(deltaTime);
		}
	}

	private void OnApplicationPause(bool bPause)
	{
		if (bPause)
		{
			CTrinitiCollectManager.GetInstance().SendLeaveGame();
			if (CGameNetManager.GetInstance().IsConnected())
			{
				CTrinitiCollectManager.GetInstance().SendQuitCoop();
			}
			if (m_GameState.CurScene != kGameSceneEnum.Game && CGameNetManager.GetInstance().IsConnected())
			{
				TNetManager.GetInstance().DisConnect();
			}
			if (m_GameScene != null)
			{
				m_GameScene.LeaveMutiplyPunish();
			}
			m_fGameInBackgroundTime = Time.realtimeSinceStartup;
			if (IsBackgroundUpload)
			{
				iGameData gameData = iGameApp.GetInstance().m_GameData;
				if (gameData != null)
				{
					iDataCenter dataCenter = gameData.GetDataCenter();
					if (dataCenter != null)
					{
						dataCenter.Save();
					}
				}
				Upload(OnUploadSuccess, OnUploadFailed, m_OnPackData);
			}
			float fTime = -1f;
			if (m_GameData.m_BlackMarketCenter.CheckBlackMarketRefreshTime(ref fTime))
			{
				CLocalNotification.GetInstance().Register("Check new arrivals in blackmarket for the limited time only!", fTime);
			}
			return;
		}
		if (CGameNetManager.GetInstance().IsConnected())
		{
			CTrinitiCollectManager.GetInstance().SendEnterCoop();
		}
		if (Time.realtimeSinceStartup - m_fGameInBackgroundTime >= iMacroDefine.ReloginInBackgroundTime)
		{
			if (IsBackgroundBack)
			{
				iLoginManager.GetInstance().BackToApp(OnBackToAppSuccess, OnBackToAppFailed, OnBackToAppNetError);
			}
			else if (IsBackgroundRelogin)
			{
				iLoginManager.GetInstance().RestartApp();
			}
		}
		else
		{
			CTrinitiCollectManager.GetInstance().SendLogin();
		}
	}

	private void OnApplicationQuit()
	{
		CTrinitiCollectManager.GetInstance().SendLeaveGame();
		if (CGameNetManager.GetInstance().IsConnected())
		{
			CTrinitiCollectManager.GetInstance().SendQuitCoop();
		}
		float fTime = -1f;
		if (m_GameData.m_BlackMarketCenter.CheckBlackMarketRefreshTime(ref fTime))
		{
			CLocalNotification.GetInstance().Register("Check new arrivals in blackmarket for the limited time only!", fTime);
		}
	}

	protected void OnBackToAppSuccess()
	{
		CTrinitiCollectManager.GetInstance().SendLogin();
	}

	protected void OnBackToAppFailed(iLoginManager.kFailedType type)
	{
		Debug.Log("OnBackToAppFailed " + type);
		string empty = string.Empty;
		switch (type)
		{
		case iLoginManager.kFailedType.VersionError:
			empty = "An updated version is required. Download it now to continue playing.";
			break;
		case iLoginManager.kFailedType.ServerMaintain:
			empty = "Our server is down for maintenance. Please retry later!";
			break;
		case iLoginManager.kFailedType.GameCenterChanged:
			empty = "Game Center ID changed. Please re-login.";
			break;
		default:
			empty = "Connection error. Please retry later.";
			break;
		}
		CMessageBoxScript.GetInstance().MessageBox(string.Empty, empty, OnOK, null, "OK");
	}

	protected void OnBackToAppNetError()
	{
		CMessageBoxScript.GetInstance().MessageBox(string.Empty, "Connection error. Please retry later.", OnOK, null, "OK");
	}

	protected void OnOK()
	{
		iGameApp.GetInstance().DestroyScene();
		iGameApp.GetInstance().EnterScene("Scene_Main");
	}

	protected void Update(float deltaTime)
	{
		if (m_StateUpload != kStateUpload.Upload && m_bNeedNextUpload)
		{
			m_bNeedNextUpload = false;
			Upload(OnUploadSuccess, OnUploadFailed, m_OnPackData);
		}
		if (!m_bNeedUpload)
		{
			return;
		}
		m_fCheckUploadTimeCount += deltaTime;
		if (m_fCheckUploadTimeCount >= m_fCheckUploadTime && IsBackgroundUpload)
		{
			m_fCheckUploadTimeCount = 0f;
			if (m_StateUpload != kStateUpload.Upload)
			{
				Upload(OnUploadSuccess, OnUploadFailed, m_OnPackData);
			}
		}
	}

	protected void OnUploadSuccess()
	{
		m_nUploadFailedCur = 0;
	}

	protected void OnUploadFailed(string code)
	{
		Debug.Log(code);
		if (code == "gmOperating")
		{
			CMessageBoxScript.GetInstance().MessageBox(string.Empty, "Your account is under maintenance. Please try again later.", OnOK, null, "OK");
			iGameSceneBase gameScene = iGameApp.GetInstance().m_GameScene;
			if (gameScene != null)
			{
				gameScene.SetPause(true);
			}
			return;
		}
		if (code == "invalidRand")
		{
			CMessageBoxScript.GetInstance().MessageBox(string.Empty, "This account was logged in elsewhere. Please re-login.", OnOK, null, "OK");
			iGameSceneBase gameScene2 = iGameApp.GetInstance().m_GameScene;
			if (gameScene2 != null)
			{
				gameScene2.SetPause(true);
			}
			return;
		}
		m_nUploadFailedCur++;
		if (m_nUploadFailedCur >= m_nUploadFailedMax)
		{
			m_nUploadFailedCur = 0;
			CMessageBoxScript.GetInstance().MessageBox(string.Empty, "Connection error. Please re-login.", OnOK, null, "OK");
			iGameSceneBase gameScene3 = iGameApp.GetInstance().m_GameScene;
			if (gameScene3 != null)
			{
				gameScene3.SetPause(true);
			}
		}
		else
		{
			m_fCheckUploadTimeCount = 0f;
			Upload(OnUploadSuccess, OnUploadFailed, m_OnPackData);
		}
	}

	protected void Step_Login_S(string serverId, string serverUrl, string deviceId, string userId, string gamecenterId)
	{
		UnityEngine.Debug.Log("Step_Login_S " + serverId + " " + serverUrl + " " + deviceId + " " + userId + " " + gamecenterId);
		m_sCurDeviceId = deviceId;
		m_sCurGameCenterId = gamecenterId;
		Step_Fetch_Data(serverId, serverUrl, deviceId);
		iMacroDefine.ServerUrl_CoopData = serverUrl;
	}

	protected void Step_Login_F(string sCode)
	{
		if (m_OnFetchFailed != null)
		{
			m_OnFetchFailed(sCode);
		}
	}

	protected void Step_Fetch_Data(string serverId, string serverUrl, string deviceId)
	{
		m_sServerId = serverId;
		m_sServerUrl = serverUrl;
		iServerDataManager.GetInstance().Initialize(serverId, serverUrl, string.Empty, -1f);
		iServerDataManager.GetInstance().SendFetchSaveData(deviceId, Step_Fetch_Data_S, Step_Fetch_Data_F);
		m_bFirstRegister = false;
	}

	protected void Step_Fetch_Data_S(string sData)
	{
		try
		{
			if (sData.Length < 1)
			{
				m_bFirstRegister = true;
				m_bCreateNewSave = true;
				throw new Exception();
			}
			if (!UnPackData(sData))
			{
				throw new Exception();
			}
			Debug.Log("save data is ok");
		}
		catch
		{
			Debug.Log("no data on server, upload local savedata");
			if (!PackData(ref m_sSaveData))
			{
				OnFetchDataFailed(string.Empty);
				return;
			}
			Hashtable hashtable = new Hashtable();
			hashtable.Add("data", m_sSaveData);
			iServerDataManager.GetInstance().SendUploadSaveData(m_sDeviceId, JsonMapper.ToJson(hashtable), null, null);
		}
		if (m_OnFetchSuccess != null)
		{
			m_OnFetchSuccess();
		}
	}

	protected void Step_Fetch_Data_F(string sCode)
	{
		Debug.Log("failed " + sCode);
		if (m_OnFetchFailed != null)
		{
			m_OnFetchFailed(sCode);
		}
	}
}
