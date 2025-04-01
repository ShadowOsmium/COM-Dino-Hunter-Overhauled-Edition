using System.Collections.Generic;
using UnityEngine;

public class iUpdateHandleManager : MonoBehaviour
{
	protected class CEventInfo
	{
		public float m_fInterval;

		public float m_fCount;

		public OnEvent m_Event;

		public List<object> m_ltEventParam;

		public CEventInfo()
		{
			m_ltEventParam = new List<object>();
		}
	}

	public delegate bool OnEvent(List<object> ltEventParam);

	protected static iUpdateHandleManager m_Instance;

	protected List<CEventInfo> m_ltEventInfo;

	protected List<CEventInfo> m_ltEventInfoDestroy;

	public static iUpdateHandleManager GetInstance()
	{
		if (m_Instance == null)
		{
			GameObject gameObject = new GameObject("_iUpdateHandleManager");
			gameObject.transform.position = Vector3.zero;
			gameObject.transform.rotation = Quaternion.identity;
			m_Instance = gameObject.AddComponent<iUpdateHandleManager>();
		}
		return m_Instance;
	}

	private void Awake()
	{
		m_ltEventInfo = new List<CEventInfo>();
		m_ltEventInfoDestroy = new List<CEventInfo>();
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (m_ltEventInfo.Count < 1)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		foreach (CEventInfo item in m_ltEventInfo)
		{
			item.m_fCount += deltaTime;
			if (!(item.m_fCount < item.m_fInterval))
			{
				item.m_fCount = 0f;
				if (item.m_Event == null || item.m_Event(item.m_ltEventParam))
				{
					m_ltEventInfoDestroy.Add(item);
				}
			}
		}
		if (m_ltEventInfoDestroy.Count <= 0)
		{
			return;
		}
		foreach (CEventInfo item2 in m_ltEventInfoDestroy)
		{
			m_ltEventInfo.Remove(item2);
		}
	}

	public void AddEvent(OnEvent onevent, List<object> ltparam, float fInterval = 0f)
	{
		CEventInfo cEventInfo = new CEventInfo();
		cEventInfo.m_Event = onevent;
		cEventInfo.m_fInterval = fInterval;
		cEventInfo.m_fCount = 0f;
		cEventInfo.m_ltEventParam = ltparam;
		m_ltEventInfo.Add(cEventInfo);
	}
}
