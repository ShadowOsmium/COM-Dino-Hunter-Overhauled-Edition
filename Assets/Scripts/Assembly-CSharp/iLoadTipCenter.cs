using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class iLoadTipCenter : iBaseCenter
{
	protected List<CLoadTipInfo> m_ltLoadTipInfo;

	public iLoadTipCenter()
	{
		m_ltLoadTipInfo = new List<CLoadTipInfo>();
	}

	public CLoadTipInfo GetRandom()
	{
		if (m_ltLoadTipInfo == null || m_ltLoadTipInfo.Count == 0)
		{
			LoadData(SpoofedData.LoadSpoof("loadtip"));
			//return null;
		}
		return m_ltLoadTipInfo[Random.Range(0, m_ltLoadTipInfo.Count)];
	}

	protected override void LoadData(string content)
	{
		m_ltLoadTipInfo.Clear();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (!(childNode.Name != "tip"))
			{
				CLoadTipInfo cLoadTipInfo = new CLoadTipInfo();
				if (MyUtils.GetAttribute(childNode, "icon", ref value))
				{
					cLoadTipInfo.sIcon = value;
				}
				if (MyUtils.GetAttribute(childNode, "desc", ref value))
				{
					cLoadTipInfo.sDesc = value;
				}
				m_ltLoadTipInfo.Add(cLoadTipInfo);
			}
		}
	}
}
