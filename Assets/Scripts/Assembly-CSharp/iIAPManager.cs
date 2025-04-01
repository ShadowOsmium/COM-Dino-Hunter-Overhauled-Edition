using System.Collections;
using System.Collections.Generic;
using Prime31;
using UnityEngine;

public class iIAPManager : MonoBehaviour
{
	protected enum kPingState
	{
		None,
		Pinging,
		Success,
		Fail
	}

	protected enum kPurchaseState
	{
		None,
		Ping,
		Purchase
	}

	public delegate void OnIAPPurchaseSuccess(string sIAPKey, string sIdentifier, string sReceipt, string sSignature);

	public delegate void OnEvent();

	protected static iIAPManager m_Instance;

	protected OnIAPPurchaseSuccess m_OnIAPPurchaseSuccess;

	protected OnEvent m_OnIAPPurchaseFailed;

	protected OnEvent m_OnIAPPurchaseCancel;

	protected OnEvent m_OnIAPPurchaseNetError;

	protected string m_sCurIAPKey = string.Empty;

	protected kPingState m_PingState;

	protected kPurchaseState m_PurchaseState;

	protected string m_sServerName = string.Empty;

	protected string m_sServerUrl = string.Empty;

	protected string m_sKey = string.Empty;

	protected float m_fTimeOut = -1f;

	private bool itemOwned;

	private string m_CurBuyingIapId = string.Empty;

	public static iIAPManager GetInstance()
	{
		if (m_Instance == null)
		{
			GameObject gameObject = new GameObject("_IAPManager");
			gameObject.transform.position = Vector3.zero;
			gameObject.transform.rotation = Quaternion.identity;
			Object.DontDestroyOnLoad(gameObject);
			m_Instance = gameObject.AddComponent<iIAPManager>();
		}
		return m_Instance;
	}

	private static void AwakeChartBoost()
	{
		ChartBoostAndroid.init("52bbc6ff9ddc3561b7bf2386", "139a2e6e6cce81e5acdd375f965f364d9bc114a6");
		ChartBoostAndroid.onStart();
		UnityEngine.Debug.Log("ChartBoostAndroid init");
	}

	private void Awake()
	{
		m_PingState = kPingState.None;
		m_PurchaseState = kPurchaseState.None;
	}

	private void OnEnable()
	{
		GoogleIABManager.billingSupportedEvent += billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent += billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent += purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent += purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
		string publicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAjyxJZM9WdlXom3tscL3uvAgVl3e13M3pCHIawMf5DSuqahavcO2UKKTxGmTFKuR0IxiSmQhdKxFbmBFM00pG0mC9YMPqNNhsCBCy97V4Mu/GrHwxo+a2DZTCekFvVEc4QbhIyUGHk3eJNilNscY7LhC9AjiMhcPhSlnKYL2zAy4g4TqMKXpKobAI2IxcpNKfrrOwgXIKXz5XiYP4GH0IY43/6p7W5iMaHs3wmMAyQLUGItNjdnPhn28FgB60EaZwf+VS9h1c+ZuDaUxme9Q8yrPRKw4FsJAoBx+X0cXpIMKzNxgKBgh4881+MkZUiiUgQh7/ucKFzXy3DGpw/eZrQwIDAQAB";
		GoogleIAB.init(publicKey);
		GoogleIAB.setAutoVerifySignatures(true);
	}

	private void OnDisable()
	{
		GoogleIABManager.billingSupportedEvent -= billingSupportedEvent;
		GoogleIABManager.billingNotSupportedEvent -= billingNotSupportedEvent;
		GoogleIABManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		GoogleIABManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		GoogleIABManager.purchaseCompleteAwaitingVerificationEvent += purchaseCompleteAwaitingVerificationEvent;
		GoogleIABManager.purchaseSucceededEvent -= purchaseSucceededEvent;
		GoogleIABManager.purchaseFailedEvent -= purchaseFailedEvent;
		GoogleIABManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
		GoogleIABManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
		GoogleIAB.unbindService();
	}

	public void StartGooglePurchase(string sku, OnIAPPurchaseSuccess onsuccess, OnEvent onfailed, OnEvent oncancel, OnEvent onneterror)
	{
		m_CurBuyingIapId = sku;
		m_OnIAPPurchaseSuccess = onsuccess;
		m_OnIAPPurchaseFailed = onfailed;
		m_OnIAPPurchaseCancel = oncancel;
		m_OnIAPPurchaseNetError = onneterror;
		GoogleIAB.purchaseProduct(sku);
	}

	private void billingSupportedEvent()
	{
		UnityEngine.Debug.Log("billingSupportedEvent");
	}

	private void billingNotSupportedEvent(string error)
	{
		UnityEngine.Debug.Log("billingNotSupportedEvent: " + error);
	}

	private void queryInventorySucceededEvent(List<GooglePurchase> purchases, List<GoogleSkuInfo> skus)
	{
		if (itemOwned)
		{
			UnityEngine.Debug.Log("consumeProduct : " + m_CurBuyingIapId);
			GoogleIAB.consumeProduct(m_CurBuyingIapId);
			itemOwned = false;
		}
		UnityEngine.Debug.Log(string.Format("queryInventorySucceededEvent. total purchases: {0}, total skus: {1}", purchases.Count, skus.Count));
		Prime31.Utils.logObject(purchases);
		Prime31.Utils.logObject(skus);
	}

	private void queryInventoryFailedEvent(string error)
	{
		if (itemOwned)
		{
			itemOwned = false;
		}
		UnityEngine.Debug.Log("queryInventoryFailedEvent: " + error);
	}

	private void purchaseCompleteAwaitingVerificationEvent(string purchaseData, string signature)
	{
		UnityEngine.Debug.Log("purchaseCompleteAwaitingVerificationEvent. purchaseData: " + purchaseData + ", signature: " + signature);
	}

	private void purchaseSucceededEvent(GooglePurchase purchase)
	{
		UnityEngine.Debug.Log("google purchaseSucceededEvent: " + purchase.ToString());
		GoogleIAB.consumeProduct(purchase.productId);
	}

	private void consumePurchaseSucceededEvent(GooglePurchase purchase)
	{
		UnityEngine.Debug.Log("google consumePurchaseSucceededEvent");
		GoogleOnPurchaseSuccess(purchase);
	}

	public void GoogleOnPurchaseSuccess(GooglePurchase purchase)
	{
		if (m_OnIAPPurchaseSuccess != null)
		{
			UnityEngine.Debug.Log("GoogleOnPurchaseSuccess purchase : " + purchase.ToString());
			m_OnIAPPurchaseSuccess(purchase.productId, purchase.orderId, purchase.originalJson, purchase.signature);
		}
		m_PurchaseState = kPurchaseState.None;
	}

	private void consumePurchaseFailedEvent(string error)
	{
		UnityEngine.Debug.Log("consumePurchaseFailedEvent: " + error);
		if (error.Substring(0, 14) == "Item not owned" || error.Substring(0, 14) == "Unable to buy ")
		{
			GoogleIAB.consumeProduct(m_CurBuyingIapId);
		}
	}

	private void purchaseFailedEvent(string reason)
	{
		UnityEngine.Debug.Log("---- purchaseFailedEvent: " + reason + " | iapid : " + m_CurBuyingIapId);
		int num = reason.IndexOf("response: ");
		string text = reason.Substring(num + 10);
		num = text.IndexOf(':');
		text = text.Substring(0, num);
		switch (text)
		{
		case "7":
			itemOwned = true;
			GoogleIAB.queryInventory(new string[1] { m_CurBuyingIapId });
			break;
		case "1":
		case "2":
		case "3":
		case "4":
		case "5":
		case "6":
		case "8":
		case "-1000":
		case "-1001":
		case "-1002":
		case "-1003":
		case "-1004":
		case "-1005":
		case "-1006":
		case "-1007":
		case "-1008":
		case "-1009":
		case "-1010":
			OnPurchaseFailed();
			break;
		}
		UnityEngine.Debug.LogWarning("reason id : " + text);
	}

	private void Start()
	{
	}

	private void Update()
	{
		Update(Time.deltaTime);
	}

	public bool IsCanPurchase()
	{
		return m_PurchaseState == kPurchaseState.None;
	}

	public void Initialize(string sServerName, string sServerUrl, string sKey, float fTimeOut)
	{
		m_sServerName = sServerName;
		m_sServerUrl = sServerUrl;
		m_sKey = sKey;
		m_fTimeOut = fTimeOut;
		HttpClient.Instance().AddServer(m_sServerName, m_sServerUrl, m_fTimeOut, (m_sKey.Length >= 1) ? m_sKey : null);
	}

	public void Purchase(string sIAPKey, OnIAPPurchaseSuccess onsuccess, OnEvent onfailed, OnEvent oncancel, OnEvent onneterror)
	{
		if (m_PurchaseState == kPurchaseState.None)
		{
			UnityEngine.Debug.Log("purchase iap " + sIAPKey);
			m_PurchaseState = kPurchaseState.Ping;
			m_sCurIAPKey = sIAPKey;
			m_OnIAPPurchaseSuccess = onsuccess;
			m_OnIAPPurchaseFailed = onfailed;
			m_OnIAPPurchaseCancel = oncancel;
			m_OnIAPPurchaseNetError = onneterror;
			StartCoroutine(TestPingApple());
		}
	}

	protected IEnumerator TestPingApple()
	{
		m_PingState = kPingState.Pinging;
		WWW www = new WWW("http://www.apple.com/?rand=" + Random.Range(10, 99999));
		yield return www;
		if (!www.isDone || www.error != null)
		{
			Debug.Log("test ping failed " + www.error);
			m_PingState = kPingState.Fail;
		}
		else
		{
			Debug.Log("test ping successed ");
			m_PingState = kPingState.Success;
		}
	}

	protected void Update(float deltaTime)
	{
		if (m_PurchaseState == kPurchaseState.None)
		{
			return;
		}
		if (m_PurchaseState == kPurchaseState.Ping)
		{
			if (m_PingState != kPingState.Pinging)
			{
				if (m_PingState == kPingState.Success)
				{
					m_PingState = kPingState.None;
					IAPPlugin.NowPurchaseProduct(m_sCurIAPKey, "1");
					m_PurchaseState = kPurchaseState.Purchase;
				}
				else if (m_PingState == kPingState.Fail)
				{
					m_PingState = kPingState.None;
					OnPurchaseNetError();
				}
			}
		}
		else
		{
			if (m_PurchaseState != kPurchaseState.Purchase)
			{
				return;
			}
			int purchaseStatus = IAPPlugin.GetPurchaseStatus();
			if (purchaseStatus != 0)
			{
				if (purchaseStatus == 1)
				{
					OnPurchaseSuccess(m_sCurIAPKey);
				}
				else if (purchaseStatus == -2)
				{
					OnPurchaseCancel();
				}
				else if (purchaseStatus < 0)
				{
					OnPurchaseFailed();
				}
			}
		}
	}

	public void OnPurchaseSuccess(string sIAPKey)
	{
		if (m_OnIAPPurchaseSuccess != null)
		{
			m_OnIAPPurchaseSuccess(sIAPKey, IAPPlugin.GetTransactionIdentifier(), IAPPlugin.GetTransactionReceipt(), string.Empty);
		}
		m_PurchaseState = kPurchaseState.None;
	}

	public void OnPurchaseFailed()
	{
		if (m_OnIAPPurchaseFailed != null)
		{
			m_OnIAPPurchaseFailed();
		}
		m_PurchaseState = kPurchaseState.None;
	}

	public void OnPurchaseCancel()
	{
		if (m_OnIAPPurchaseCancel != null)
		{
			m_OnIAPPurchaseCancel();
		}
		m_PurchaseState = kPurchaseState.None;
	}

	public void OnPurchaseNetError()
	{
		if (m_OnIAPPurchaseNetError != null)
		{
			m_OnIAPPurchaseNetError();
		}
		m_PurchaseState = kPurchaseState.None;
	}
}
