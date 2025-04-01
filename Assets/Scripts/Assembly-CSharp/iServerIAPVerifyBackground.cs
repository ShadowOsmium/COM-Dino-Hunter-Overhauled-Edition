using UnityEngine;

public class iServerIAPVerifyBackground : MonoBehaviour
{
	protected static iServerIAPVerifyBackground m_Instance;

	protected bool m_bAcitve;

	protected string m_sCurIAPKey = string.Empty;

	protected string m_sCurTID = string.Empty;

	protected string m_sCurReceipt = string.Empty;

	protected string m_sCurSignature = string.Empty;

	protected iGameData m_GameData
	{
		get
		{
			return iGameApp.GetInstance().m_GameData;
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
			return m_GameData.GetDataCenter();
		}
	}

	public static iServerIAPVerifyBackground GetInstance()
	{
		if (m_Instance == null)
		{
			GameObject gameObject = new GameObject("_ServerIAPVerifyBackground");
			gameObject.transform.position = Vector3.zero;
			gameObject.transform.rotation = Quaternion.identity;
			Object.DontDestroyOnLoad(gameObject);
			m_Instance = gameObject.AddComponent<iServerIAPVerifyBackground>();
		}
		return m_Instance;
	}

	private void Awake()
	{
		m_bAcitve = false;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (!m_bAcitve)
		{
			return;
		}
		string sRandom = string.Empty;
		int nRat = 0;
		int nRatA = 0;
		int nRatB = 0;
		if (m_DataCenter.GetIAPTransactionInfo(ref m_sCurIAPKey, ref m_sCurTID, ref m_sCurReceipt, ref m_sCurSignature, ref sRandom, ref nRat, ref nRatA, ref nRatB) && iServerIAPVerify.GetInstance().IsCanVerify())
		{
			if (sRandom.Length < 1 || nRat == 0 || nRatA == 0 || nRatB == 0)
			{
				UnityEngine.Debug.Log("commit verify request at the background");
				iServerIAPVerify.GetInstance().VerifyIAP(m_sCurIAPKey, m_sCurTID, m_sCurReceipt, m_sCurSignature, null, null, null, iGameApp.GetInstance().OnPurchaseIAP_InBackground, OnIAPWriteIdentifier, OnIAPDeleteIdentifier);
			}
			else
			{
				Debug.Log("check verify request at the background");
				iServerIAPVerify.GetInstance().VerifyIAPCheck(m_sCurIAPKey, m_sCurTID, m_sCurReceipt, sRandom, nRat, nRatA, nRatB, null, null, null, iGameApp.GetInstance().OnPurchaseIAP_InBackground, OnIAPWriteIdentifier, OnIAPDeleteIdentifier);
			}
		}
	}

	public void SetActive(bool bActive)
	{
		m_bAcitve = bActive;
	}

	protected void OnIAPWriteIdentifier(string sKey, string sIdentifier, string sReceipt, string sSignature, string sRandom, int nRat, int nRatA, int nRatB)
	{
		if (m_DataCenter != null)
		{
			m_DataCenter.AddIAPTransactionInfo(sKey, sIdentifier, sReceipt, sSignature, sRandom, nRat, nRatA, nRatB);
			iGameApp.GetInstance().SaveData();
		}
	}

	protected void OnIAPDeleteIdentifier(string sKey, string sIdentifier, string sReceipt)
	{
		if (m_DataCenter != null)
		{
			m_DataCenter.DelIAPTransactionInfo(sKey, sIdentifier, sReceipt);
			iGameApp.GetInstance().SaveData();
		}
	}
}
