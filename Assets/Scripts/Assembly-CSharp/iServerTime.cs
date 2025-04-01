using UnityEngine;
using System;

public class iServerTime : MonoBehaviour
{
    public delegate void OnServerTimeSuccess(double ticks);

    public delegate void OnServerTimeFailed();

    protected static iServerTime m_Instance;

    protected OnServerTimeSuccess m_OnServerTimeSuccess;

    protected OnServerTimeFailed m_OnServerTimeFailed;

    public static iServerTime GetInstance()
    {
        if (m_Instance == null)
        {
            GameObject gameObject = new GameObject("_iServerTime");
            gameObject.transform.position = Vector3.zero;
            gameObject.transform.rotation = Quaternion.identity;
            UnityEngine.Object.DontDestroyOnLoad(gameObject);
            m_Instance = gameObject.AddComponent<iServerTime>();
        }
        return m_Instance;
    }

	public void Initialize(string s, string s2, string s3, float f)
	{
	}

    public void GetServerTime(OnServerTimeSuccess onsuccess, OnServerTimeFailed onfailed)
    {
        m_OnServerTimeSuccess = onsuccess;
        m_OnServerTimeFailed = onfailed;

        double serverTimeTicks = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

        OnSuccess(serverTimeTicks);
    }

    protected void OnSuccess(double ticks)
    {
        if (m_OnServerTimeSuccess != null)
        {
            m_OnServerTimeSuccess(ticks);
        }
    }

    protected void OnFailed()
    {
        if (m_OnServerTimeFailed != null)
        {
            m_OnServerTimeFailed();
        }
    }
}