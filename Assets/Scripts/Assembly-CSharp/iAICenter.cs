using System.Collections.Generic;
using System.Xml;

public class iAICenter : iBaseCenter
{
	protected Dictionary<int, CAIInfo> m_dictAIInfo;

	protected Dictionary<int, CAIManagerInfo> m_dictAIManagerInfo;

	public iAICenter()
	{
		m_dictAIInfo = new Dictionary<int, CAIInfo>();
		m_dictAIManagerInfo = new Dictionary<int, CAIManagerInfo>();
	}

	public CAIInfo GetAIInfo(int nID)
	{
		if (!m_dictAIInfo.ContainsKey(nID))
		{
			return null;
		}
		return m_dictAIInfo[nID];
	}

	public CAIManagerInfo GetAIManagerInfo(int nID)
	{
		if (!m_dictAIManagerInfo.ContainsKey(nID))
		{
			return null;
		}
		return m_dictAIManagerInfo[nID];
	}

	protected override void LoadData(string content)
	{
		m_dictAIInfo.Clear();
		m_dictAIManagerInfo.Clear();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (childNode.Name == "aiinfo")
			{
				LoadAIInfo(childNode);
			}
			else if (childNode.Name == "aimanagerinfo")
			{
				LoadAIManagerInfo(childNode);
			}
		}
	}

	protected void LoadAIInfo(XmlNode root)
	{
		string value = string.Empty;
		foreach (XmlNode childNode in root.ChildNodes)
		{
			if (childNode.Name != "ai" || !MyUtils.GetAttribute(childNode, "id", ref value))
			{
				continue;
			}
			CAIInfo cAIInfo = new CAIInfo();
			cAIInfo.nID = int.Parse(value);
			if (MyUtils.GetAttribute(childNode, "skill", ref value))
			{
				string[] array = value.Split(',');
				for (int i = 0; i < array.Length; i++)
				{
					cAIInfo.ltSkill.Add(new SkillComboRateInfo(int.Parse(array[i]), 100f));
				}
			}
			if (MyUtils.GetAttribute(childNode, "skillrate", ref value))
			{
				string[] array = value.Split(',');
				for (int j = 0; j < array.Length && j < cAIInfo.ltSkill.Count; j++)
				{
					cAIInfo.ltSkill[j].m_fRate = float.Parse(array[j]);
				}
			}
			if (MyUtils.GetAttribute(childNode, "skillpassive", ref value))
			{
				string[] array = value.Split(',');
				for (int k = 0; k < array.Length; k++)
				{
					cAIInfo.ltSkillPassive.Add(int.Parse(array[k]));
				}
			}
			if (MyUtils.GetAttribute(childNode, "behavior", ref value))
			{
				cAIInfo.nBehavior = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "effectlist", ref value))
			{
				string[] array = value.Split(',');
				for (int l = 0; l < array.Length; l++)
				{
					cAIInfo.ltEffect.Add(int.Parse(array[l]));
				}
			}
			if (MyUtils.GetAttribute(childNode, "effectbonelist", ref value))
			{
				string[] array = value.Split(',');
				for (int m = 0; m < array.Length; m++)
				{
					cAIInfo.ltEffectBone.Add(int.Parse(array[m]));
				}
			}
			m_dictAIInfo.Add(cAIInfo.nID, cAIInfo);
		}
	}

	protected void LoadAIManagerInfo(XmlNode root)
	{
		string value = string.Empty;
		foreach (XmlNode childNode in root.ChildNodes)
		{
			if (childNode.Name != "aimanager" || !MyUtils.GetAttribute(childNode, "id", ref value))
			{
				continue;
			}
			CAIManagerInfo cAIManagerInfo = new CAIManagerInfo();
			cAIManagerInfo.nID = int.Parse(value);
			if (MyUtils.GetAttribute(childNode, "ai", ref value))
			{
				cAIManagerInfo.nAI = int.Parse(value);
			}
			foreach (XmlNode childNode2 in childNode.ChildNodes)
			{
				if (!(childNode2.Name != "trigger"))
				{
					CAITriggerInfo cAITriggerInfo = new CAITriggerInfo();
					if (MyUtils.GetAttribute(childNode2, "type", ref value))
					{
						cAITriggerInfo.nType = int.Parse(value);
					}
					if (MyUtils.GetAttribute(childNode2, "oprate", ref value))
					{
						cAITriggerInfo.nOprate = int.Parse(value);
					}
					if (MyUtils.GetAttribute(childNode2, "value", ref value))
					{
						cAITriggerInfo.nValue = int.Parse(value);
					}
					if (MyUtils.GetAttribute(childNode2, "ai", ref value))
					{
						cAITriggerInfo.nAI = int.Parse(value);
					}
					if (MyUtils.GetAttribute(childNode2, "priority", ref value))
					{
						cAITriggerInfo.nPriority = int.Parse(value);
					}
					cAIManagerInfo.ltAITrigger.Add(cAITriggerInfo);
				}
			}
			m_dictAIManagerInfo.Add(cAIManagerInfo.nID, cAIManagerInfo);
		}
	}
}
