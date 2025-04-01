using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class CStartPointManager
{
	public class CClosePoint
	{
		public float fDis;

		public CStartPoint point;

		public CClosePoint(float dis, CStartPoint point)
		{
			fDis = dis;
			this.point = point;
		}
	}

	protected int m_nID;

	protected Color m_Color;

	protected Dictionary<int, CStartPoint> m_dictStartPoint;

	protected List<CClosePoint> m_ltClosePoint;

	public int ID
	{
		get
		{
			return m_nID;
		}
		set
		{
			m_nID = value;
		}
	}

	public Color GizmosColor
	{
		get
		{
			return m_Color;
		}
		set
		{
			m_Color = value;
		}
	}

	public CStartPointManager()
	{
		m_dictStartPoint = new Dictionary<int, CStartPoint>();
		m_ltClosePoint = new List<CClosePoint>();
	}

	public CStartPoint GetRandom()
	{
		int num = Random.Range(0, m_dictStartPoint.Count);
		foreach (CStartPoint value in m_dictStartPoint.Values)
		{
			num--;
			if (num < 0)
			{
				return value;
			}
		}
		return null;
	}

	public CStartPoint GetRandomClosePoint(Vector3 v3Pos, int nCount = 3, float closest = 10f)
	{
		if (m_dictStartPoint.Count <= nCount)
		{
			return GetRandom();
		}
		m_ltClosePoint.Clear();
		foreach (CStartPoint value in m_dictStartPoint.Values)
		{
			bool flag = false;
			float num = Vector3.Distance(v3Pos, value.v3Pos);
			if (num <= closest)
			{
				continue;
			}
			for (int i = 0; i < m_ltClosePoint.Count; i++)
			{
				if (m_ltClosePoint[i].fDis == 0f || num < m_ltClosePoint[i].fDis)
				{
					m_ltClosePoint.Insert(i, new CClosePoint(num, value));
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				m_ltClosePoint.Add(new CClosePoint(num, value));
			}
		}
		if (m_ltClosePoint.Count < 1)
		{
			return GetRandom();
		}
		if (m_ltClosePoint.Count <= nCount)
		{
			return m_ltClosePoint[Random.Range(0, m_ltClosePoint.Count)].point;
		}
		return m_ltClosePoint[Random.Range(0, nCount)].point;
	}

	public CStartPoint GetRandomFarestPoint(Vector3 v3Pos, int nCount = 3)
	{
		if (m_dictStartPoint.Count <= nCount)
		{
			return GetRandom();
		}
		m_ltClosePoint.Clear();
		foreach (CStartPoint value in m_dictStartPoint.Values)
		{
			bool flag = false;
			float num = Vector3.Distance(v3Pos, value.v3Pos);
			for (int i = 0; i < m_ltClosePoint.Count; i++)
			{
				if (m_ltClosePoint[i].fDis == 0f || num > m_ltClosePoint[i].fDis)
				{
					m_ltClosePoint.Insert(i, new CClosePoint(num, value));
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				m_ltClosePoint.Add(new CClosePoint(num, value));
			}
		}
		return m_ltClosePoint[Random.Range(0, nCount)].point;
	}

	public bool IsInside2D(Vector3 v3Pos)
	{
		foreach (CStartPoint value in m_dictStartPoint.Values)
		{
			if (value.IsInside2D(v3Pos))
			{
				return true;
			}
		}
		return false;
	}

	public bool IsInside3D(Vector3 v3Pos)
	{
		foreach (CStartPoint value in m_dictStartPoint.Values)
		{
			if (value.IsInside3D(v3Pos))
			{
				return true;
			}
		}
		return false;
	}

	public bool IsInside2D(int nID, Vector3 v3Pos)
	{
		if (!m_dictStartPoint.ContainsKey(nID))
		{
			return false;
		}
		return m_dictStartPoint[nID].IsInside2D(v3Pos);
	}

	public bool IsInside3D(int nID, Vector3 v3Pos)
	{
		if (!m_dictStartPoint.ContainsKey(nID))
		{
			return false;
		}
		return m_dictStartPoint[nID].IsInside3D(v3Pos);
	}

	public CStartPoint Get(int nID)
	{
		if (!m_dictStartPoint.ContainsKey(nID))
		{
			return null;
		}
		return m_dictStartPoint[nID];
	}

	public void Set(int nID, CStartPoint point)
	{
		if (!m_dictStartPoint.ContainsKey(nID))
		{
			m_dictStartPoint.Add(nID, point);
		}
		else
		{
			m_dictStartPoint[nID] = point;
		}
	}

	public void Del(int nID)
	{
		if (m_dictStartPoint.ContainsKey(nID))
		{
			m_dictStartPoint.Remove(nID);
		}
	}

	public Dictionary<int, CStartPoint> GetData()
	{
		return m_dictStartPoint;
	}

	public bool Load(string sPath)
	{
		string empty = string.Empty;
		TextAsset textAsset = (TextAsset)Resources.Load(sPath, typeof(TextAsset));
		if (textAsset == null)
		{
			return false;
		}
		empty = textAsset.ToString();
		if (empty.Length < 1)
		{
			return false;
		}
		ParseXml(empty);
		return true;
	}

	public void ParseXml(string context)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(context);
		string empty = string.Empty;
		m_dictStartPoint.Clear();
		XmlNode documentElement = xmlDocument.DocumentElement;
		empty = documentElement.Attributes["id"].Value;
		if (empty.Length < 1)
		{
			return;
		}
		m_nID = int.Parse(empty);
		empty = documentElement.Attributes["color"].Value;
		if (empty.Length < 1)
		{
			return;
		}
		string[] array = empty.Split(',');
		if (array.Length < 4)
		{
			return;
		}
		m_Color.r = float.Parse(array[0]);
		m_Color.g = float.Parse(array[1]);
		m_Color.b = float.Parse(array[2]);
		m_Color.a = float.Parse(array[3]);
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (childNode.Name != "Point")
			{
				continue;
			}
			empty = childNode.Attributes["id"].Value;
			if (empty.Length < 1)
			{
				continue;
			}
			int nID = int.Parse(empty);
			CStartPoint cStartPoint = new CStartPoint();
			empty = childNode.Attributes["pos"].Value;
			if (empty.Length > 0)
			{
				array = empty.Split(',');
				if (array.Length >= 2)
				{
					cStartPoint.v3Pos = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
				}
			}
			empty = childNode.Attributes["size"].Value;
			if (empty.Length > 0)
			{
				array = empty.Split(',');
				if (array.Length >= 2)
				{
					cStartPoint.v3Size = new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
				}
			}
			Set(nID, cStartPoint);
		}
	}
}
