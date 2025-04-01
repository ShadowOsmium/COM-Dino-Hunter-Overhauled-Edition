using System.Collections.Generic;
using System.Xml;

public class iDailyTaskCenter : iBaseCenter
{
	protected Dictionary<int, CDailyTaskInfo> m_dictDailyTaskInfo;

	public iDailyTaskCenter()
	{
		m_dictDailyTaskInfo = new Dictionary<int, CDailyTaskInfo>();
	}

	public CDailyTaskInfo Get(int nWeekDay)
	{
		if (m_dictDailyTaskInfo.Count == 0)
		{
			LoadData(SpoofedData.LoadSpoof("dailytask"));
		}
		if (!m_dictDailyTaskInfo.ContainsKey(nWeekDay))
		{
			return null;
		}
		return m_dictDailyTaskInfo[nWeekDay];
	}

	protected override void LoadData(string content)
	{
		m_dictDailyTaskInfo.Clear();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (childNode.Name != "node" || !MyUtils.GetAttribute(childNode, "weekday", ref value))
			{
				continue;
			}
			CDailyTaskInfo cDailyTaskInfo = new CDailyTaskInfo();
			cDailyTaskInfo.nWeekDay = int.Parse(value);
			m_dictDailyTaskInfo.Add(cDailyTaskInfo.nWeekDay, cDailyTaskInfo);
			if (!MyUtils.GetAttribute(childNode, "tasktype", ref value))
			{
				continue;
			}
			string[] array = value.Split(',');
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					cDailyTaskInfo.ltTask.Add(int.Parse(array[i]));
				}
			}
		}
	}
}
