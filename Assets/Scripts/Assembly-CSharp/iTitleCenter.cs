using System.Collections.Generic;
using System.Xml;

public class iTitleCenter : iBaseCenter
{
	protected Dictionary<int, CTitleInfo> m_dictTitleInfo;

	public iTitleCenter()
	{
		m_dictTitleInfo = new Dictionary<int, CTitleInfo>();
	}

	public Dictionary<int, CTitleInfo> GetData()
	{
		return m_dictTitleInfo;
	}

	public CTitleInfo Get(int nID)
	{
		if (!m_dictTitleInfo.ContainsKey(nID))
		{
			return null;
		}
		return m_dictTitleInfo[nID];
	}

	protected override void LoadData(string content)
	{
		m_dictTitleInfo.Clear();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (!(childNode.Name != "title") && MyUtils.GetAttribute(childNode, "id", ref value))
			{
				int nID = int.Parse(value);
				CTitleInfo cTitleInfo = Get(nID);
				if (cTitleInfo == null)
				{
					cTitleInfo = new CTitleInfo();
					cTitleInfo.nID = nID;
					m_dictTitleInfo.Add(cTitleInfo.nID, cTitleInfo);
				}
				if (MyUtils.GetAttribute(childNode, "name", ref value))
				{
					cTitleInfo.sName = value;
				}
				if (MyUtils.GetAttribute(childNode, "conditiontype", ref value))
				{
					cTitleInfo.nConditionType = int.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "conditionvaluex", ref value))
				{
					cTitleInfo.nConditionValueX = int.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "conditionvaluey", ref value))
				{
					cTitleInfo.nConditionValueY = int.Parse(value);
				}
			}
		}
	}
}
