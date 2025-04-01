using System;
using System.IO;
using System.Net;
using UnityEngine;

public class IPAddressPlugin : MonoBehaviour
{
	protected static IPAddressPlugin m_Instance;

	public static IPAddressPlugin GetInstance()
	{
		if (m_Instance == null)
		{
			GameObject gameObject = new GameObject("_IPAddressPlugin");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			m_Instance = gameObject.AddComponent<IPAddressPlugin>();
		}
		return m_Instance;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SendIPReqest()
	{
		try
		{
			Uri requestUri = new Uri("http://iframe.ip138.com/ic.asp");
			HttpWebRequest httpWebRequest = WebRequest.Create(requestUri) as HttpWebRequest;
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/x-www-form-urlencoded";
			httpWebRequest.ContentLength = 0L;
			httpWebRequest.CookieContainer = new CookieContainer();
			httpWebRequest.GetRequestStream().Write(new byte[0], 0, 0);
			HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
			StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
			string message = streamReader.ReadToEnd();
			streamReader.Close();
			httpWebRequest.Abort();
			httpWebResponse.Close();
			Debug.Log(message);
		}
		catch
		{
			Debug.Log("SendIPReqest exception");
		}
	}
}
