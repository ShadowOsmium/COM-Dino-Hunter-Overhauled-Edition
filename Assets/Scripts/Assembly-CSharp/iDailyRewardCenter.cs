using System.Collections.Generic;
using System.Xml;

public class iDailyRewardCenter : iBaseCenter
{
	protected Dictionary<int, CDailyRewardInfo> m_dictDailyRewardInfo;

	public iDailyRewardCenter()
	{
		m_dictDailyRewardInfo = new Dictionary<int, CDailyRewardInfo>();
	}

	public CDailyRewardInfo Get(int nLoginCount)
	{
		if (m_dictDailyRewardInfo.Count == 0)
		{
			LoadData(SpoofedData.LoadSpoof("dailyreward"));
		}
		if (!m_dictDailyRewardInfo.ContainsKey(nLoginCount))
		{
			return null;
		}
		return m_dictDailyRewardInfo[nLoginCount];
	}

	protected override void LoadData(string content)
	{
		m_dictDailyRewardInfo.Clear();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (!(childNode.Name != "node") && MyUtils.GetAttribute(childNode, "logincount", ref value))
			{
				CDailyRewardInfo cDailyRewardInfo = new CDailyRewardInfo();
				cDailyRewardInfo.nLoginCount = int.Parse(value);
				m_dictDailyRewardInfo.Add(cDailyRewardInfo.nLoginCount, cDailyRewardInfo);
				if (MyUtils.GetAttribute(childNode, "iscrystal", ref value))
				{
					cDailyRewardInfo.isCrystal = bool.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "value", ref value))
				{
					cDailyRewardInfo.nValue = int.Parse(value);
				}
			}
		}
	}
}
