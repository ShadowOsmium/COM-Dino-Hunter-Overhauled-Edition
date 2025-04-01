using UnityEngine;

public class CLocalNotification
{
	protected static CLocalNotification m_Instance;

	protected iLocalNotification m_LocalNotification;

	public CLocalNotification()
	{
		GameObject gameObject = new GameObject("_LocalNotification");
		if (gameObject != null)
		{
			Object.DontDestroyOnLoad(gameObject);
			m_LocalNotification = gameObject.AddComponent<iLocalNotification>();
		}
	}

	public static CLocalNotification GetInstance()
	{
		if (m_Instance == null)
		{
			m_Instance = new CLocalNotification();
			m_Instance.Initialize();
		}
		return m_Instance;
	}

	public void Initialize()
	{
		if (m_LocalNotification != null)
		{
			m_LocalNotification.UnRegister();
		}
		ClearLocalNotification();
	}

	public void ClearLocalNotification()
	{
		if (!(m_LocalNotification == null))
		{
			m_LocalNotification.Clear();
		}
	}

	public void AddLocalNotification(string alertBody, float fTime)
	{
		if (!(m_LocalNotification == null))
		{
			m_LocalNotification.Add(alertBody, fTime);
		}
	}

	public void Register(string alertBody, float fTime)
	{
		if (!(m_LocalNotification == null))
		{
			m_LocalNotification.Register(alertBody, fTime);
		}
	}
}
