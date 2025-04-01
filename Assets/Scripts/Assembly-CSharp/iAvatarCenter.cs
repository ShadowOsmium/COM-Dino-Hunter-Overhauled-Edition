using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class iAvatarCenter : iBaseCenter
{
	protected Dictionary<int, CAvatarInfo> m_dictAvatar;

	public iAvatarCenter()
	{
		m_dictAvatar = new Dictionary<int, CAvatarInfo>();
	}

	public CAvatarInfo Get(int nID)
	{
		if (!m_dictAvatar.ContainsKey(nID))
		{
			return null;
		}
		return m_dictAvatar[nID];
	}

	public CAvatarInfoLevel Get(int nID, int nLevel)
	{
		CAvatarInfo cAvatarInfo = Get(nID);
		if (cAvatarInfo == null)
		{
			return null;
		}
		return cAvatarInfo.Get(nLevel);
	}

	public Dictionary<int, CAvatarInfo> GetData()
	{
		return m_dictAvatar;
	}

	public CAvatarInfo GetRandomByType(int nType)
	{
		List<CAvatarInfo> list = new List<CAvatarInfo>();
		foreach (CAvatarInfo value in m_dictAvatar.Values)
		{
			if (value.m_nType == nType)
			{
				list.Add(value);
			}
		}
		if (list.Count < 1)
		{
			return null;
		}
		return list[Random.Range(0, list.Count)];
	}

	protected override void LoadData(string content)
	{
		m_dictAvatar.Clear();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (childNode.Name != "avatar" || !MyUtils.GetAttribute(childNode, "id", ref value))
			{
				continue;
			}
			int nID = int.Parse(value);
			int nLevel = 1;
			if (MyUtils.GetAttribute(childNode, "level", ref value))
			{
				nLevel = int.Parse(value);
			}
			CAvatarInfo cAvatarInfo = Get(nID);
			if (cAvatarInfo == null)
			{
				cAvatarInfo = new CAvatarInfo();
				cAvatarInfo.m_nID = nID;
				m_dictAvatar.Add(cAvatarInfo.m_nID, cAvatarInfo);
			}
			CAvatarInfoLevel cAvatarInfoLevel = cAvatarInfo.Get(nLevel);
			if (cAvatarInfoLevel == null)
			{
				cAvatarInfoLevel = new CAvatarInfoLevel();
				cAvatarInfoLevel.m_nLevel = nLevel;
				cAvatarInfo.m_dictAvatarInfoLevel.Add(cAvatarInfoLevel.m_nLevel, cAvatarInfoLevel);
			}
			if (MyUtils.GetAttribute(childNode, "type", ref value))
			{
				cAvatarInfo.m_nType = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "icon", ref value))
			{
				cAvatarInfo.m_sIcon = value.Trim();
			}
			if (MyUtils.GetAttribute(childNode, "name", ref value))
			{
				cAvatarInfo.m_sName = value.Trim();
			}
			if (MyUtils.GetAttribute(childNode, "model", ref value))
			{
				cAvatarInfo.m_sModel = value;
				cAvatarInfo.m_sTexture = value;
			}
			if (MyUtils.GetAttribute(childNode, "texture", ref value))
			{
				cAvatarInfo.m_sTexture = value;
			}
			if (MyUtils.GetAttribute(childNode, "effect", ref value))
			{
				cAvatarInfo.m_sEffect = value;
			}
			if (MyUtils.GetAttribute(childNode, "islinkchar", ref value))
			{
				cAvatarInfo.m_bLinkChar = bool.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "unlockstage", ref value))
			{
				cAvatarInfo.m_nUnlockStageID = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "unlockhunterlvl", ref value))
			{
				cAvatarInfo.m_nUnlockHunterLvl = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "func", ref value))
			{
				string[] array = value.Split(',');
				for (int i = 0; i < array.Length && i < cAvatarInfoLevel.arrFunc.Length; i++)
				{
					cAvatarInfoLevel.arrFunc[i] = int.Parse(array[i]);
				}
			}
			if (MyUtils.GetAttribute(childNode, "valuex", ref value))
			{
				string[] array = value.Split(',');
				for (int j = 0; j < array.Length && j < cAvatarInfoLevel.arrValueX.Length; j++)
				{
					cAvatarInfoLevel.arrValueX[j] = int.Parse(array[j]);
				}
			}
			if (MyUtils.GetAttribute(childNode, "valuey", ref value))
			{
				string[] array = value.Split(',');
				for (int k = 0; k < array.Length && k < cAvatarInfoLevel.arrValueY.Length; k++)
				{
					cAvatarInfoLevel.arrValueY[k] = int.Parse(array[k]);
				}
			}
			if (MyUtils.GetAttribute(childNode, "iscrystalpurchase", ref value))
			{
				cAvatarInfoLevel.isCrystalPurchase = bool.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "purchaseprice", ref value))
			{
				cAvatarInfoLevel.nPurchasePrice = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "desc", ref value))
			{
				cAvatarInfoLevel.sDesc = value;
			}
			if (MyUtils.GetAttribute(childNode, "levelupdesc", ref value))
			{
				cAvatarInfoLevel.sLevelUpDesc = value;
			}
			if (MyUtils.GetAttribute(childNode, "materials", ref value))
			{
				string[] array = value.Split(',');
				for (int l = 0; l < array.Length; l++)
				{
					cAvatarInfoLevel.ltMaterials.Add(int.Parse(array[l]));
				}
			}
			if (MyUtils.GetAttribute(childNode, "materialscount", ref value))
			{
				string[] array = value.Split(',');
				for (int m = 0; m < array.Length && m < cAvatarInfoLevel.ltMaterials.Count; m++)
				{
					cAvatarInfoLevel.ltMaterialsCount.Add(int.Parse(array[m]));
				}
			}
		}
	}
}
