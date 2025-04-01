using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class iBattleGroupCenter
{
	protected List<CBattleRegion> m_ltBattleRegion;

	public iBattleGroupCenter()
	{
		m_ltBattleRegion = new List<CBattleRegion>();
	}

	public List<CBattleRegion> GetData()
	{
		return m_ltBattleRegion;
	}

	public bool Load()
	{
		string content = string.Empty;
		if (MyUtils.isWindows)
		{
			if (!Utils.FileGetString("battlegroup.xml", ref content))
			{
				return false;
			}
		}
		else if (MyUtils.isIOS || MyUtils.isAndroid)
		{
			TextAsset textAsset = (TextAsset)Resources.Load(PrefabManager.GetPath(3012), typeof(TextAsset));
			if (textAsset == null)
			{
				return false;
			}
			content = textAsset.ToString();
		}
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (!(childNode.Name != "battlegroup"))
			{
				int nGroupID = 0;
				float fMin = 0f;
				float fMax = 0f;
				if (MyUtils.GetAttribute(childNode, "groupid", ref value))
				{
					nGroupID = int.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "min", ref value))
				{
					fMin = float.Parse(value);
				}
				if (MyUtils.GetAttribute(childNode, "max", ref value))
				{
					fMax = float.Parse(value);
				}
				m_ltBattleRegion.Add(new CBattleRegion(nGroupID, fMin, fMax));
			}
		}
		return true;
	}
}
