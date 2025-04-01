using System.Collections.Generic;
using System.Xml;

public class iTaskCenter : iBaseCenter
{
	protected Dictionary<int, CTaskInfo> m_dictTaskInfo;

	public iTaskCenter()
	{
		m_dictTaskInfo = new Dictionary<int, CTaskInfo>();
	}

	public CTaskInfo Get(int nID)
	{
		if (!m_dictTaskInfo.ContainsKey(nID))
		{
			return null;
		}
		return m_dictTaskInfo[nID];
	}

	protected override void LoadData(string content)
	{
		m_dictTaskInfo.Clear();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (childNode.Name != "task" || !MyUtils.GetAttribute(childNode, "id", ref value))
			{
				continue;
			}
			int nID = int.Parse(value);
			if (!MyUtils.GetAttribute(childNode, "type", ref value))
			{
				continue;
			}
			int num = int.Parse(value);
			CTaskInfo cTaskInfo = null;
			switch (num)
			{
			case 1:
				cTaskInfo = LoadAttributeCollect(nID, childNode);
				break;
			case 2:
				cTaskInfo = LoadAttributeHunter(nID, childNode);
				break;
			case 3:
				cTaskInfo = LoadAttributeDefence(nID, childNode);
				break;
			case 4:
				cTaskInfo = LoadAttributeSurvival(nID, childNode);
				break;
			case 5:
				cTaskInfo = LoadAttributeButcher(nID, childNode);
				break;
			case 6:
				cTaskInfo = LoadAttributeInfinite(nID, childNode);
				break;
			}
			if (cTaskInfo == null)
			{
				continue;
			}
			if (MyUtils.GetAttribute(childNode, "name", ref value))
			{
				cTaskInfo.sName = value;
			}
			else
			{
				cTaskInfo.sName = "This is task " + cTaskInfo.nID;
			}
			if (MyUtils.GetAttribute(childNode, "desc", ref value))
			{
				cTaskInfo.sDesc = value;
			}
			else
			{
				cTaskInfo.sDesc = "This is desc of task " + cTaskInfo.nID;
			}
			if (MyUtils.GetAttribute(childNode, "FinishList", ref value))
			{
				string[] array = value.Split(',');
				for (int i = 0; i < array.Length && i < cTaskInfo.arrFail.Length; i++)
				{
					cTaskInfo.arrFail[i] = int.Parse(array[i]);
				}
			}
			if (MyUtils.GetAttribute(childNode, "FinishValueList", ref value))
			{
				string[] array = value.Split(',');
				for (int j = 0; j < array.Length && j < cTaskInfo.arrFailValue.Length; j++)
				{
					cTaskInfo.arrFailValue[j] = int.Parse(array[j]);
				}
			}
			m_dictTaskInfo.Add(cTaskInfo.nID, cTaskInfo);
		}
	}

	protected CTaskInfoCollect LoadAttributeCollect(int nID, XmlNode node)
	{
		string value = string.Empty;
		CTaskInfoCollect cTaskInfoCollect = new CTaskInfoCollect();
		cTaskInfoCollect.nID = nID;
		cTaskInfoCollect.nType = 1;
		if (MyUtils.GetAttribute(node, "IDList", ref value))
		{
			string[] array = value.Split(',');
			for (int i = 0; i < array.Length; i++)
			{
				cTaskInfoCollect.ltCollectInfo.Add(new CTaskInfoCollect.CCollectInfo(int.Parse(array[i]), 1));
			}
		}
		if (MyUtils.GetAttribute(node, "NUMList", ref value))
		{
			string[] array = value.Split(',');
			for (int j = 0; j < array.Length && j < cTaskInfoCollect.ltCollectInfo.Count; j++)
			{
				cTaskInfoCollect.ltCollectInfo[j].nMaxCount = int.Parse(array[j]);
			}
		}
		if (MyUtils.GetAttribute(node, "EggLife", ref value))
		{
			cTaskInfoCollect.fLife = float.Parse(value);
		}
		return cTaskInfoCollect;
	}

	protected CTaskInfoHunter LoadAttributeHunter(int nID, XmlNode node)
	{
		string value = string.Empty;
		CTaskInfoHunter cTaskInfoHunter = new CTaskInfoHunter();
		cTaskInfoHunter.nID = nID;
		cTaskInfoHunter.nType = 2;
		if (MyUtils.GetAttribute(node, "IDList", ref value))
		{
			string[] array = value.Split(',');
			for (int i = 0; i < array.Length; i++)
			{
				cTaskInfoHunter.ltHunterInfo.Add(new CTaskInfoHunter.CHunterInfo(int.Parse(array[i]), 1));
			}
		}
		if (MyUtils.GetAttribute(node, "NUMList", ref value))
		{
			string[] array = value.Split(',');
			for (int j = 0; j < array.Length && j < cTaskInfoHunter.ltHunterInfo.Count; j++)
			{
				cTaskInfoHunter.ltHunterInfo[j].nMaxCount = int.Parse(array[j]);
			}
		}
		return cTaskInfoHunter;
	}

	protected CTaskInfoDefence LoadAttributeDefence(int nID, XmlNode node)
	{
		string value = string.Empty;
		CTaskInfoDefence cTaskInfoDefence = new CTaskInfoDefence();
		cTaskInfoDefence.nID = nID;
		cTaskInfoDefence.nType = 3;
		if (MyUtils.GetAttribute(node, "WaveCount", ref value))
		{
			cTaskInfoDefence.nWaveCount = int.Parse(value);
		}
		if (MyUtils.GetAttribute(node, "ZhaLanLife", ref value))
		{
			cTaskInfoDefence.fLife = float.Parse(value);
		}
		return cTaskInfoDefence;
	}

	protected CTaskInfoSurvival LoadAttributeSurvival(int nID, XmlNode node)
	{
		CTaskInfoSurvival cTaskInfoSurvival = new CTaskInfoSurvival();
		cTaskInfoSurvival.nID = nID;
		cTaskInfoSurvival.nType = 4;
		return cTaskInfoSurvival;
	}

	protected CTaskInfoButcher LoadAttributeButcher(int nID, XmlNode node)
	{
		CTaskInfoButcher cTaskInfoButcher = new CTaskInfoButcher();
		cTaskInfoButcher.nID = nID;
		cTaskInfoButcher.nType = 5;
		return cTaskInfoButcher;
	}

	protected CTaskInfoInfinite LoadAttributeInfinite(int nID, XmlNode node)
	{
		CTaskInfoInfinite cTaskInfoInfinite = new CTaskInfoInfinite();
		cTaskInfoInfinite.nID = nID;
		cTaskInfoInfinite.nType = 6;
		return cTaskInfoInfinite;
	}
}
