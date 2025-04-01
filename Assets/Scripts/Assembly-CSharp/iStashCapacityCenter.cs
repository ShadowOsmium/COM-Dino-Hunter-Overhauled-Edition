using System.Collections.Generic;
using System.Xml;

public class iStashCapacityCenter : iBaseCenter
{
	protected List<CStashCapacity> m_ltStashCapacity;

	public iStashCapacityCenter()
	{
		m_ltStashCapacity = new List<CStashCapacity>();
	}

	public List<CStashCapacity> GetData()
	{
		return m_ltStashCapacity;
	}

	public CStashCapacity Get(int nIndex)
	{
		if (nIndex < 0 || nIndex >= m_ltStashCapacity.Count)
		{
			return null;
		}
		return m_ltStashCapacity[nIndex];
	}

	protected override void LoadData(string content)
	{
		m_ltStashCapacity.Clear();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (!(childNode.Name != "stash") && MyUtils.GetAttribute(childNode, "level", ref value))
			{
				CStashCapacity cStashCapacity = new CStashCapacity();
				cStashCapacity.nLevel = int.Parse(value);
				if (MyUtils.GetAttribute(childNode, "capacity", ref value))
				{
					cStashCapacity.nCapacity = int.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "iscrystal", ref value))
				{
					cStashCapacity.isCrystalPurchase = bool.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "price", ref value))
				{
					cStashCapacity.nPrice = int.Parse(value);
				}
				cStashCapacity.sLevelUpDesc = "Add capacity to " + cStashCapacity.nCapacity;
				m_ltStashCapacity.Add(cStashCapacity);
			}
		}
	}
}
