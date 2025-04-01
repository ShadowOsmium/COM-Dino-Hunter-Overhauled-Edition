using System.Collections.Generic;
using System.Xml;

public class iHunterLevelCenter : iBaseCenter
{
	protected Dictionary<int, CHunterLevelInfo> m_dictHunterLevelInfo;

	public iHunterLevelCenter()
	{
		m_dictHunterLevelInfo = new Dictionary<int, CHunterLevelInfo>();
	}

	public int Count()
	{
		if (m_dictHunterLevelInfo == null)
		{
			return 0;
		}
		return m_dictHunterLevelInfo.Count;
	}

	public Dictionary<int, CHunterLevelInfo> GetData()
	{
		return m_dictHunterLevelInfo;
	}

	public CHunterLevelInfo Get(int nID)
	{
		if (!m_dictHunterLevelInfo.ContainsKey(nID))
		{
			return null;
		}
		return m_dictHunterLevelInfo[nID];
	}

	public bool IsSameGroup(int nID1, int nID2)
	{
		CHunterLevelInfo cHunterLevelInfo = Get(nID1);
		CHunterLevelInfo cHunterLevelInfo2 = Get(nID2);
		if (cHunterLevelInfo == null || cHunterLevelInfo2 == null)
		{
			return false;
		}
		if (cHunterLevelInfo.m_nGroupID != cHunterLevelInfo2.m_nGroupID)
		{
			return false;
		}
		return true;
	}

	protected override void LoadData(string content)
	{
		m_dictHunterLevelInfo.Clear();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (childNode.Name != "node" || !MyUtils.GetAttribute(childNode, "id", ref value))
			{
				continue;
			}
			int nID = int.Parse(value);
			CHunterLevelInfo cHunterLevelInfo = Get(nID);
			if (cHunterLevelInfo == null)
			{
				cHunterLevelInfo = new CHunterLevelInfo();
				cHunterLevelInfo.m_nID = nID;
				m_dictHunterLevelInfo.Add(cHunterLevelInfo.m_nID, cHunterLevelInfo);
			}
			if (MyUtils.GetAttribute(childNode, "groupid", ref value))
			{
				cHunterLevelInfo.m_nGroupID = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "hunterexp_win", ref value))
			{
				cHunterLevelInfo.m_nHunterExpWin = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "hunterexp_lose", ref value))
			{
				cHunterLevelInfo.m_nHunterExpLose = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "gamelevellist", ref value))
			{
				cHunterLevelInfo.m_ltGameLevel.Clear();
				string[] array = value.Split(',');
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						cHunterLevelInfo.m_ltGameLevel.Add(int.Parse(array[i]));
					}
				}
			}
			if (MyUtils.GetAttribute(childNode, "monsterlvl_min", ref value))
			{
				cHunterLevelInfo.m_nMonsterLvlMin = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "monsterlvl_max", ref value))
			{
				cHunterLevelInfo.m_nMonsterLvlMax = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "gold", ref value))
			{
				cHunterLevelInfo.m_nGold = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "exp", ref value))
			{
				cHunterLevelInfo.m_nExp = int.Parse(value);
			}
		}
	}
}
