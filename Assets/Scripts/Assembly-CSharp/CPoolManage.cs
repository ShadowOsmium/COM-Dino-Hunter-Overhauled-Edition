using System.Collections.Generic;
using UnityEngine;

public class CPoolManage
{
	protected Transform m_NodeFree;

	protected Transform m_NodePlay;

	protected GameObject m_Prefab;

	protected List<gyUIPoolObject> m_ltCache;

	public CPoolManage()
	{
		m_ltCache = new List<gyUIPoolObject>();
	}

	public void Initialize(string str, Transform nodefree, Transform nodeplay, int nCount = 0)
	{
		if (m_Prefab == null)
		{
			m_Prefab = (GameObject)Resources.Load(str);
		}
		m_NodeFree = nodefree;
		m_NodePlay = nodeplay;
		Resize(nCount);
	}

	public void Resize(int nCount)
	{
		if (m_Prefab == null)
		{
			return;
		}
		nCount -= m_ltCache.Count;
		if (nCount <= 0)
		{
			return;
		}
		for (int i = 0; i < nCount; i++)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(m_Prefab);
			if (gameObject == null)
			{
				continue;
			}
			gyUIPoolObject gyUIPoolObject2 = gameObject.GetComponent<gyUIPoolObject>();
			if (gyUIPoolObject2 == null)
			{
				gyUIPoolObject2 = gameObject.AddComponent<gyUIPoolObject>();
				if (gyUIPoolObject2 == null)
				{
					continue;
				}
			}
			gyUIPoolObject2.Initialize(m_NodeFree, m_NodePlay);
			m_ltCache.Add(gyUIPoolObject2);
		}
	}

	public void Destroy()
	{
		foreach (gyUIPoolObject item in m_ltCache)
		{
			if (item != null && item.gameObject != null)
			{
				Object.Destroy(item.gameObject);
			}
		}
		m_ltCache.Clear();
	}

	public GameObject Get(float fTime = 0f)
	{
		if (m_Prefab == null)
		{
			return null;
		}
		foreach (gyUIPoolObject item in m_ltCache)
		{
			if (item != null && item.isFree && item.gameObject != null)
			{
				item.GiveOut(fTime);
				return item.gameObject;
			}
		}
		GameObject gameObject = (GameObject)Object.Instantiate(m_Prefab);
		if (gameObject == null)
		{
			return null;
		}
		gyUIPoolObject gyUIPoolObject2 = gameObject.GetComponent<gyUIPoolObject>();
		if (gyUIPoolObject2 == null)
		{
			gyUIPoolObject2 = gameObject.AddComponent<gyUIPoolObject>();
			if (gyUIPoolObject2 == null)
			{
				return null;
			}
		}
		gyUIPoolObject2.Initialize(m_NodeFree, m_NodePlay);
		gyUIPoolObject2.GiveOut(fTime);
		m_ltCache.Add(gyUIPoolObject2);
		return gameObject;
	}
}
