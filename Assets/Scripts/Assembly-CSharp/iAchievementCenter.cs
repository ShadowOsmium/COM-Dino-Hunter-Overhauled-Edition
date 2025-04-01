using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class iAchievementCenter : iBaseCenter
{
	protected Dictionary<int, CAchievementInfo> m_dictAchievementInfo;

	public iAchievementCenter()
	{
		m_dictAchievementInfo = new Dictionary<int, CAchievementInfo>();
	}

	public Dictionary<int, CAchievementInfo> GetData()
	{
		if (m_dictAchievementInfo.Count == 0)
		{
			LoadData(SpoofedData.LoadSpoof("achievement"));
		}
		return m_dictAchievementInfo;
	}

	public CAchievementInfo Get(int nID)
	{
		if (m_dictAchievementInfo.Count == 0)
		{
			LoadData(SpoofedData.LoadSpoof("achievement"));
		}
		if (!m_dictAchievementInfo.ContainsKey(nID))
		{
			return null;
		}
		return m_dictAchievementInfo[nID];
	}

	public List<CAchievementInfo> GetDailyAchievementList(int nType = -1)
	{
		if (m_dictAchievementInfo.Count == 0)
		{
			LoadData(SpoofedData.LoadSpoof("achievement"));
		}
		List<CAchievementInfo> list = new List<CAchievementInfo>();
		foreach (CAchievementInfo value in m_dictAchievementInfo.Values)
		{
			if ((nType == -1 || nType == value.nType) && value.isDaily)
			{
				list.Add(value);
			}
		}
		return list;
	}

	protected override void LoadData(string content)
	{
		m_dictAchievementInfo.Clear();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (childNode.Name != "achievement" || !MyUtils.GetAttribute(childNode, "id", ref value))
			{
				continue;
			}
			CAchievementInfo cAchievementInfo = new CAchievementInfo();
			cAchievementInfo.nID = int.Parse(value);
			if (MyUtils.GetAttribute(childNode, "daily", ref value))
			{
				cAchievementInfo.isDaily = bool.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "type", ref value))
			{
				cAchievementInfo.nType = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "param", ref value))
			{
				cAchievementInfo.ltParam.Clear();
				string[] array = value.Split(',');
				for (int i = 0; i < array.Length; i++)
				{
					cAchievementInfo.ltParam.Add(int.Parse(array[i]));
				}
			}
			if (MyUtils.GetAttribute(childNode, "key", ref value))
			{
				cAchievementInfo.sKey = value.Trim();
			}
			if (MyUtils.GetAttribute(childNode, "name", ref value))
			{
				cAchievementInfo.sName = value.Trim();
			}
			if (MyUtils.GetAttribute(childNode, "desc", ref value))
			{
				cAchievementInfo.sDesc = value.Trim();
			}
			cAchievementInfo.ltStep.Clear();
			string text = "step";
			for (int j = 1; j <= 3; j++)
			{
				if (MyUtils.GetAttribute(childNode, text + j, ref value))
				{
					string[] array = value.Split(',');
					if (array.Length == 3)
					{
						CAchievementStep cAchievementStep = new CAchievementStep();
						cAchievementStep.nStepPurpose = int.Parse(array[0]);
						cAchievementStep.nRewardType = int.Parse(array[1]);
						cAchievementStep.nRewardNumber = int.Parse(array[2]);
						cAchievementInfo.ltStep.Add(cAchievementStep);
					}
				}
			}
			m_dictAchievementInfo.Add(cAchievementInfo.nID, cAchievementInfo);
		}
	}
}
