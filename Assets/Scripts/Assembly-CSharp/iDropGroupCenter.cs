using System.Collections.Generic;
using System.Xml;

public class iDropGroupCenter : iBaseCenter
{
	protected Dictionary<int, CDropGroupInfo> m_dictDropGroupInfo;

	public iDropGroupCenter()
	{
		m_dictDropGroupInfo = new Dictionary<int, CDropGroupInfo>();
	}

	public CDropGroupInfo Get(int nID)
	{
		if (!m_dictDropGroupInfo.ContainsKey(nID))
		{
			return null;
		}
		return m_dictDropGroupInfo[nID];
	}

	protected override void LoadData(string content)
	{
		m_dictDropGroupInfo.Clear();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (childNode.Name != "group" || !MyUtils.GetAttribute(childNode, "id", ref value))
			{
				continue;
			}
			CDropGroupInfo cDropGroupInfo = new CDropGroupInfo();
			cDropGroupInfo.nID = int.Parse(value);
			if (MyUtils.GetAttribute(childNode, "item", ref value))
			{
				cDropGroupInfo.ltItem.Clear();
				string[] array = value.Split(',');
				for (int i = 0; i < array.Length; i++)
				{
					cDropGroupInfo.ltItem.Add(new CDropItem(int.Parse(array[i]), 0f));
				}
			}
			if (MyUtils.GetAttribute(childNode, "rate", ref value))
			{
				string[] array = value.Split(',');
				for (int j = 0; j < array.Length && j < cDropGroupInfo.ltItem.Count; j++)
				{
					cDropGroupInfo.ltItem[j].fRate = float.Parse(array[j]);
				}
			}
			m_dictDropGroupInfo.Add(cDropGroupInfo.nID, cDropGroupInfo);
		}
	}
}
