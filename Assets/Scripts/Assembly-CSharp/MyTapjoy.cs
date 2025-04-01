using UnityEngine;

[RequireComponent(typeof(GotTapPointsMono))]
public class MyTapjoy : MonoBehaviour
{
	private const string methodName = "GotTapPoints";

	protected static MyTapjoy m_Instance;

	public string androidAppId = "9df285ac-947b-451a-8bb7-b4ff9c71a999";

	public string androidSecretKey = "2RKhea6rrUgveHRa7SUZ";

	public string iphoneAppId = "9bc5b206-b3e9-4695-9270-e0b954fc1c92";

	public string iphoneSecretKey = "gW4MJkglbp1jqoPqdOIB";

	private int tapPoints;

	public static MyTapjoy GetInstance()
	{
		if (m_Instance == null)
		{
			GameObject gameObject = new GameObject("_MyTapjoy");
			Object.DontDestroyOnLoad(gameObject);
			m_Instance = gameObject.AddComponent<MyTapjoy>();
		}
		return m_Instance;
	}

	public void Show()
	{
		//TapjoyPlugin.ShowOffers();
	}

	private void Awake()
	{
	}

	private void Start()
	{
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	AndroidJNI.AttachCurrentThread();
		//}
		//TapjoyPlugin.EnableLogging(true);
		//TapjoyPlugin.SetCallbackHandler(base.gameObject.name);
		//if (Application.platform == RuntimePlatform.Android)
		//{
		//	TapjoyPlugin.RequestTapjoyConnect(androidAppId, androidSecretKey);
		//}
		//else if (Application.platform == RuntimePlatform.IPhonePlayer)
		//{
		//	TapjoyPlugin.RequestTapjoyConnect(iphoneAppId, iphoneSecretKey);
		//}
		//TapjoyPlugin.GetTapPoints();
	}

	public void TapjoyConnectSuccess(string message)
	{
		//MonoBehaviour.print(message);
	}

	public void TapjoyConnectFail(string message)
	{
		//MonoBehaviour.print(message);
	}

	public void TapPointsLoaded(string message)
	{
		//MonoBehaviour.print("TapPointsLoaded: " + message);
		//UsedAllTapPoints();
	}

	public void TapPointsLoadedError(string message)
	{
		//MonoBehaviour.print("TapPointsLoadedError: " + message);
	}

	public void TapPointsSpent(string message)
	{
		//MonoBehaviour.print("TapPointsSpent: " + message);
		//SpendSuccessful();
	}

	public void TapPointsSpendError(string message)
	{
		//MonoBehaviour.print("TapPointsSpendError: " + message);
		//tapPoints = 0;
	}

	public void TapPointsAwarded(string message)
	{
		//MonoBehaviour.print("TapPointsAwarded: " + message);
		//UsedAllTapPoints();
	}

	public void TapPointsAwardError(string message)
	{
		//MonoBehaviour.print("TapPointsAwardError: " + message);
	}

	public void CurrencyEarned(string message)
	{
		//MonoBehaviour.print("CurrencyEarned: " + message);
		//TapjoyPlugin.ShowDefaultEarnedCurrencyAlert();
		//TapjoyPlugin.GetTapPoints();
	}

	public void FullScreenAdLoaded(string message)
	{
		//MonoBehaviour.print("FullScreenAdLoaded: " + message);
		//TapjoyPlugin.ShowFullScreenAd();
	}

	public void FullScreenAdError(string message)
	{
		//MonoBehaviour.print("FullScreenAdError: " + message);
	}

	public void DailyRewardAdLoaded(string message)
	{
		//MonoBehaviour.print("DailyRewardAd: " + message);
		//TapjoyPlugin.ShowDailyRewardAd();
	}

	public void DailyRewardAdError(string message)
	{
		//MonoBehaviour.print("DailyRewardAd: " + message);
	}

	public void DisplayAdLoaded(string message)
	{
		//MonoBehaviour.print("DisplayAdLoaded: " + message);
		//TapjoyPlugin.ShowDisplayAd();
	}

	public void DisplayAdError(string message)
	{
		//MonoBehaviour.print("DisplayAdError: " + message);
	}

	public void VideoAdStart(string message)
	{
		//MonoBehaviour.print("VideoAdStart: " + message);
	}

	public void VideoAdError(string message)
	{
		//MonoBehaviour.print("VideoAdError: " + message);
	}

	public void VideoAdComplete(string message)
	{
		//MonoBehaviour.print("VideoAdComplete: " + message);
	}

	private void UsedAllTapPoints()
	{
		//tapPoints = TapjoyPlugin.QueryTapPoints();
		//if (tapPoints > 0)
		//{
		//	TapjoyPlugin.SpendTapPoints(tapPoints);
		//}
	}

	private void SpendSuccessful()
	{
		//if (tapPoints > 0)
		//{
		//	SendMessage("GotTapPoints", tapPoints);
		//}
	}
}
