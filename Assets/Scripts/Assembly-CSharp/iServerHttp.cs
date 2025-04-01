using UnityEngine;

public class iServerHttp : MonoBehaviour
{
	public delegate void OnSuccess(string response);

	public delegate void OnFailed(int nResult);

	public static uint m_nServerHttpObjCount = 1u;

	public OnSuccess m_OnSuccess;

	public OnFailed m_OnFailed;

	public static void SendRequest(string sServerUrl, float fTimeOut, string sKey, string sAction, string sData, OnSuccess onsuccess = null, OnFailed onfailed = null)
	{
		if (sServerUrl == null || sServerUrl.Length < 1)
		{
			return;
		}
		GameObject gameObject = new GameObject("_iServerHttp" + m_nServerHttpObjCount);
		if (!(gameObject == null))
		{
			m_nServerHttpObjCount++;
			if (m_nServerHttpObjCount > 4200000000u)
			{
				m_nServerHttpObjCount = 0u;
			}
			Object.DontDestroyOnLoad(gameObject);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			iServerHttp iServerHttp2 = gameObject.AddComponent<iServerHttp>();
			if (iServerHttp2 == null)
			{
				Object.Destroy(gameObject);
				return;
			}
			iServerHttp2.m_OnSuccess = onsuccess;
			iServerHttp2.m_OnFailed = onfailed;
			string server = sServerUrl + sAction;
			HttpClient.Instance().SetServer(server, sServerUrl, fTimeOut, sKey);
			UnityEngine.Debug.Log(sData);
			HttpClient.Instance().SendRequest(server, sAction, sData, gameObject.name, "iServerHttp", "OnRequestResult", string.Empty);
		}
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

	protected void OnRequestResult(int taskId, int result, string server, string action, string response, string param)
	{
		UnityEngine.Debug.Log("OnRequestResult " + result + " " + action + " " + response + " " + param);
		if (result == 0)
		{
			if (m_OnSuccess != null)
			{
				m_OnSuccess(response);
			}
		}
		else if (m_OnFailed != null)
		{
			m_OnFailed(result);
		}
		Object.Destroy(base.gameObject);
	}
}
