using System.Collections.Generic;
using UnityEngine;

public class iLocalNotification : MonoBehaviour
{
	public class CLocalNotifyInfo
	{
		public string alerbody;

		public float time;
	}

	protected List<CLocalNotifyInfo> m_ltLocalNotifyInfo;

	private void Awake()
	{
		m_ltLocalNotifyInfo = new List<CLocalNotifyInfo>();
	}

	private void Update()
	{
	}

	private void OnApplicationPause(bool bPause)
	{
		if (bPause)
		{
			Register();
		}
		else
		{
			UnRegister();
		}
	}

	private void OnApplicationQuit()
	{
		Register();
	}

	public void Register()
	{
		foreach (CLocalNotifyInfo item in m_ltLocalNotifyInfo)
		{
			Register(item.alerbody, item.time);
		}
	}

	public void UnRegister()
	{
	}

	public void Register(string str, float time)
	{
		Debug.Log("register local notification :" + str + "  lefttime:" + MyUtils.TimeToString(time));
	}

	public void Clear()
	{
		m_ltLocalNotifyInfo.Clear();
	}

	public void Add(string alertBody, float fTime)
	{
		CLocalNotifyInfo cLocalNotifyInfo = new CLocalNotifyInfo();
		cLocalNotifyInfo.alerbody = alertBody;
		cLocalNotifyInfo.time = fTime;
		m_ltLocalNotifyInfo.Add(cLocalNotifyInfo);
	}
}
