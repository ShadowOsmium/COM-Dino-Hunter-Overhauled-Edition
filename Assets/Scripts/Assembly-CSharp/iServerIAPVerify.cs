using System.Collections;
using LitJson;
using UnityEngine;

public class iServerIAPVerify : MonoBehaviour
{
	protected enum kVerifyState
	{
		None,
		Send,
		WaitResult
	}

	public delegate void OnWriteIdentifierFunc(string sKey, string sIdentifier, string sReceipt, string sSignature, string sRandom, int nRat, int nRatA, int nRatB);

	public delegate void OnDeleteIdentifierFunc(string sKey, string sIdentifier, string sReceipt);

	public delegate void OnVerifySuccessFunc(string sKey, string sIdentifier, string sReceipt);

	public delegate void OnVerifyFailedFunc(string sKey, string sIdentifier, string sReceipt);

	public delegate void OnAddCrystalFunc(string sKey, string sIdentifier, string sRecepit);

	public delegate void OnEventFunc(string sIAPKey);

	protected static iServerIAPVerify m_Instance;

	protected OnWriteIdentifierFunc m_OnWriteIdentifier;

	protected OnDeleteIdentifierFunc m_OnDeleteIdentifier;

	protected OnVerifySuccessFunc m_OnVerifyIAP_S;

	protected OnVerifyFailedFunc m_OnVerifyIAP_F;

	protected OnAddCrystalFunc m_OnAddCrystalFunc;

	protected OnEventFunc m_OnNetError;

	protected kVerifyState m_VerifyState;

	protected string m_sCurIAPKey = string.Empty;

	protected string m_sCurTID = string.Empty;

	protected string m_sCurReceipt = string.Empty;

	protected string m_sCurSignature = string.Empty;

	protected string m_sRandom = string.Empty;

	protected int m_nRat;

	protected int m_nRatA;

	protected int m_nRatB;

	protected float m_fVerifyTime = 5f;

	protected float m_fVerifyTimeCount;

	protected string m_sServerName = string.Empty;

	protected string m_sServerUrl = string.Empty;

	protected string m_sKey = string.Empty;

	protected float m_fTimeOut = -1f;

	public static iServerIAPVerify GetInstance()
	{
		if (m_Instance == null)
		{
			GameObject gameObject = new GameObject("_ServerIAPVerify");
			gameObject.transform.position = Vector3.zero;
			gameObject.transform.rotation = Quaternion.identity;
			Object.DontDestroyOnLoad(gameObject);
			m_Instance = gameObject.AddComponent<iServerIAPVerify>();
		}
		return m_Instance;
	}

	private void Awake()
	{
		m_VerifyState = kVerifyState.None;
	}

	private void Start()
	{
	}

	private void Update()
	{
		Update(Time.deltaTime);
	}

	public bool IsCanVerify()
	{
		return m_VerifyState == kVerifyState.None;
	}

	public void Initialize(string sServerName, string sServerUrl, string sKey, float fTimeOut)
	{
		m_sServerName = sServerName;
		m_sServerUrl = sServerUrl;
		m_sKey = sKey;
		m_fTimeOut = fTimeOut;
		HttpClient.Instance().AddServer(m_sServerName, m_sServerUrl, m_fTimeOut, (m_sKey.Length >= 1) ? m_sKey : null);
	}

	public void SetPurchaseCallBack()
	{
	}

	public void VerifyIAP(string iapkey, string identifier, string receipt, string signature, OnVerifySuccessFunc onsuccess, OnVerifyFailedFunc onfailed, OnEventFunc onneterror, OnAddCrystalFunc onaddcrystal, OnWriteIdentifierFunc onwriteidentifier, OnDeleteIdentifierFunc ondeleteidentifier)
	{
		if (m_VerifyState == kVerifyState.None && iapkey.Length >= 1)
		{
			m_sCurIAPKey = iapkey;
			m_sCurTID = identifier;
			m_sCurReceipt = receipt;
			m_sCurSignature = signature;
			m_OnVerifyIAP_S = onsuccess;
			m_OnVerifyIAP_F = onfailed;
			m_OnNetError = onneterror;
			m_OnAddCrystalFunc = onaddcrystal;
			m_OnWriteIdentifier = onwriteidentifier;
			m_OnDeleteIdentifier = ondeleteidentifier;
			m_sRandom = string.Empty + Random.Range(0f, 1f) + "-" + Random.Range(0f, 1f) + "-" + Random.Range(0f, 1f) + "-" + Random.Range(0f, 1f);
			m_nRat = Random.Range(0, 255);
			SendIAPVerifyRequest(m_sCurIAPKey, m_sCurTID, m_sCurReceipt, m_sRandom, m_nRat);
		}
	}

	public void VerifyIAPCheck(string iapkey, string identifier, string receipt, string sRandom, int nRat, int nRatA, int nRatB, OnVerifySuccessFunc onsuccess, OnVerifyFailedFunc onfailed, OnEventFunc onneterror, OnAddCrystalFunc onaddcrystal, OnWriteIdentifierFunc onwriteidentifier, OnDeleteIdentifierFunc ondeleteidentifier)
	{
		if (m_VerifyState == kVerifyState.None && iapkey.Length >= 1)
		{
			m_sCurIAPKey = iapkey;
			m_sCurTID = identifier;
			m_sCurReceipt = receipt;
			m_sRandom = sRandom;
			m_nRat = nRat;
			m_nRatA = nRatA;
			m_nRatB = nRatB;
			m_OnVerifyIAP_S = onsuccess;
			m_OnVerifyIAP_F = onfailed;
			m_OnNetError = onneterror;
			m_OnAddCrystalFunc = onaddcrystal;
			m_OnWriteIdentifier = onwriteidentifier;
			m_OnDeleteIdentifier = ondeleteidentifier;
			SendIAPVerifyResultRequest(m_sCurTID, m_sCurReceipt, m_sRandom);
		}
	}

	protected void Update(float deltaTime)
	{
		if (m_VerifyState == kVerifyState.WaitResult)
		{
			m_fVerifyTimeCount += deltaTime;
			if (!(m_fVerifyTimeCount < m_fVerifyTime))
			{
				m_fVerifyTimeCount = 0f;
				SendIAPVerifyResultRequest(m_sCurTID, m_sCurReceipt, m_sRandom);
			}
		}
	}

	protected void OnSuccess(string sKey, string sIdentifier, string sReceipt)
	{
		if (m_OnAddCrystalFunc != null)
		{
			m_OnAddCrystalFunc(sKey, sIdentifier, sReceipt);
		}
		if (m_OnVerifyIAP_S != null)
		{
			m_OnVerifyIAP_S(sKey, sIdentifier, sReceipt);
		}
		m_VerifyState = kVerifyState.None;
	}

	protected void OnFailed(string sKey, string sIdentifier, string sReceipt)
	{
		if (m_OnVerifyIAP_F != null)
		{
			m_OnVerifyIAP_F(sKey, sIdentifier, sReceipt);
		}
		m_VerifyState = kVerifyState.None;
	}

	protected void OnNetError(string sKey)
	{
		if (m_OnNetError != null)
		{
			m_OnNetError(sKey);
		}
		m_VerifyState = kVerifyState.None;
	}

	protected void SendIAPVerifyRequest(string iapkey, string tid, string receipt, string sRandom, int nRat)
	{
		m_VerifyState = kVerifyState.Send;
		UnityEngine.Debug.Log("SendIAPVerifyRequest " + iapkey + " " + tid + " " + receipt);
		Hashtable hashtable = new Hashtable();
		hashtable["cmd"] = "purchase/android/UserPurchaseBuy";
		hashtable["aid"] = iMacroDefine.BundleID;
		hashtable["uuid"] = iServerSaveData.GetInstance().CurDeviceId;
		hashtable["pid"] = iapkey;
		m_sCurTID = tid;
		hashtable["tid"] = m_sCurTID;
		hashtable["info"] = receipt;
		hashtable["signature"] = m_sCurSignature;
		hashtable["rand"] = m_sRandom;
		hashtable["rat"] = m_nRat;
		string text = JsonMapper.ToJson(hashtable);
		UnityEngine.Debug.Log(text);
		if (m_OnWriteIdentifier != null)
		{
			m_OnWriteIdentifier(iapkey, tid, receipt, m_sCurSignature, sRandom, nRat, 0, 0);
		}
		HttpClient.Instance().SendRequest(m_sServerName, "groovy", text, "_ServerIAPVerify", "iServerIAPVerify", "OnIAPVerifyRequest", null);
	}

	protected void OnIAPVerifyRequest(int taskId, int result, string server, string action, string response, string param)
	{
		if (result != 0 || m_VerifyState != kVerifyState.Send)
		{
			return;
		}
		UnityEngine.Debug.Log("OnIAPVerifyRequest result = " + result);
		if (result != 0)
		{
			OnNetError(m_sCurIAPKey);
			return;
		}
		try
		{
			UnityEngine.Debug.Log(response);
			JsonData jsonData = JsonMapper.ToObject(response);
			if ((int)jsonData["code"] != 0)
			{
				OnFailed(m_sCurIAPKey, m_sCurTID, m_sCurReceipt);
				return;
			}
			m_nRatA = (int)jsonData["rata"];
			m_nRatB = (int)jsonData["ratb"];
			if (m_OnWriteIdentifier != null)
			{
				m_OnWriteIdentifier(m_sCurIAPKey, m_sCurTID, m_sCurReceipt, m_sCurSignature, m_sRandom, m_nRat, m_nRatA, m_nRatB);
			}
			SendIAPVerifyResultRequest(m_sCurTID, m_sCurReceipt, m_sRandom);
		}
		catch
		{
			UnityEngine.Debug.Log("OnIAPVerifyRequest error " + action + " " + response);
			OnFailed(m_sCurIAPKey, m_sCurTID, m_sCurReceipt);
		}
	}

	protected void SendIAPVerifyResultRequest(string tid, string receipt, string sRandom)
	{
		m_VerifyState = kVerifyState.WaitResult;
		m_fVerifyTimeCount = 0f;
		UnityEngine.Debug.Log("SendIAPVerifyResultRequest " + tid + " " + receipt);
		Hashtable hashtable = new Hashtable();
		hashtable["cmd"] = "purchase/android/GetPurchaseVerify";
		hashtable["transactionId"] = m_sCurTID;
		hashtable["randPara"] = m_sRandom;
		string text = JsonMapper.ToJson(hashtable);
		UnityEngine.Debug.Log(text);
		HttpClient.Instance().SendRequest(m_sServerName, "groovy", text, "_ServerIAPVerify", "iServerIAPVerify", "OnIAPVerifyResultRequest", null);
	}

	protected void OnIAPVerifyResultRequest(int taskId, int result, string server, string action, string response, string param)
	{
		if (result != 0 || m_VerifyState != kVerifyState.WaitResult)
		{
			return;
		}
		UnityEngine.Debug.Log("OnIAPVerifyResultRequest result = " + result);
		if (result != 0)
		{
			OnNetError(m_sCurIAPKey);
			return;
		}
		try
		{
			JsonData jsonData = JsonMapper.ToObject(response);
			int num = (int)jsonData["code"];
			UnityEngine.Debug.Log("OnIAPVerifyResultRequest code = " + num);
			if (num != 0)
			{
				OnFailed(m_sCurIAPKey, m_sCurTID, m_sCurReceipt);
				return;
			}
			int num2 = (int)jsonData["sta"];
			UnityEngine.Debug.Log("OnIAPVerifyResultRequest sta = " + num2);
			switch (num2)
			{
			case -1:
				break;
			case 0:
			{
				string text = (string)jsonData["pid"];
				int num3 = (int)jsonData["ratresult"];
				string text2 = (string)jsonData["aid"];
				UnityEngine.Debug.Log("ratersult = " + num3);
				if (text2 == iMacroDefine.BundleID && num3 == m_nRat * m_nRatA / 9 + m_nRatB - 3)
				{
					OnSuccess(m_sCurIAPKey, m_sCurTID, m_sCurReceipt);
				}
				else
				{
					OnFailed(m_sCurIAPKey, m_sCurTID, m_sCurReceipt);
				}
				if (m_OnDeleteIdentifier != null)
				{
					m_OnDeleteIdentifier(m_sCurIAPKey, m_sCurTID, m_sCurReceipt);
				}
				break;
			}
			default:
				OnFailed(m_sCurIAPKey, m_sCurTID, m_sCurReceipt);
				if (m_OnDeleteIdentifier != null)
				{
					m_OnDeleteIdentifier(m_sCurIAPKey, m_sCurTID, m_sCurReceipt);
				}
				break;
			}
		}
		catch
		{
			OnFailed(m_sCurIAPKey, m_sCurTID, m_sCurReceipt);
		}
	}
}
