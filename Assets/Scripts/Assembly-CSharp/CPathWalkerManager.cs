using System.Collections.Generic;
using UnityEngine;

public class CPathWalkerManager
{
	protected Dictionary<int, List<iRoadSignPath>> m_dictPathWalker;

	public CPathWalkerManager()
	{
		m_dictPathWalker = new Dictionary<int, List<iRoadSignPath>>();
	}

	public void Initialize(int nCount)
	{
	}

	public void Go(int nID, List<Vector3> ltPath)
	{
		if (ltPath.Count < 1)
		{
			return;
		}
		Object @object = PrefabManager.Get(4000);
		if (@object == null)
		{
			return;
		}
		GameObject gameObject = (GameObject)Object.Instantiate(@object, ltPath[0], Quaternion.identity);
		if (gameObject == null)
		{
			return;
		}
		iRoadSignPath component = gameObject.GetComponent<iRoadSignPath>();
		if (component == null)
		{
			return;
		}
		Vector3[] array = iTween.PathGenerate(ltPath.ToArray());
		int num = 1;
		if (array.Length > 1)
		{
			num = array.Length / 20;
		}
		ltPath.Clear();
		Vector3 vector = Vector3.zero;
		for (int i = 0; i < array.Length; i++)
		{
			if (!(vector != Vector3.zero) || !(Vector3.Distance(vector, array[i]) < 4f) || i == 0 || i == array.Length - 1)
			{
				vector = array[i];
				ltPath.Add(array[i]);
			}
		}
		component.Initialize(ltPath);
		component.Go();
		AddPath(nID, component);
	}

	public void Stop(int nID, float delaytime = 0f)
	{
		if (!m_dictPathWalker.ContainsKey(nID))
		{
			return;
		}
		foreach (iRoadSignPath item in m_dictPathWalker[nID])
		{
			item.DestroyWhenFinish(delaytime);
		}
		m_dictPathWalker[nID].Clear();
		m_dictPathWalker.Remove(nID);
	}

	public void StopAll()
	{
		foreach (List<iRoadSignPath> value in m_dictPathWalker.Values)
		{
			foreach (iRoadSignPath item in value)
			{
				item.DestroyWhenFinish(0f);
			}
			value.Clear();
		}
		m_dictPathWalker.Clear();
	}

	protected void AddPath(int nID, iRoadSignPath path)
	{
		if (!m_dictPathWalker.ContainsKey(nID))
		{
			m_dictPathWalker.Add(nID, new List<iRoadSignPath>());
		}
		m_dictPathWalker[nID].Add(path);
	}
}
