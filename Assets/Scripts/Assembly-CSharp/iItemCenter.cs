using System.Collections.Generic;
using System.Xml;

public class iItemCenter : iBaseCenter
{
	protected Dictionary<int, CItemInfo> m_dictItemInfo;

	public iItemCenter()
	{
		m_dictItemInfo = new Dictionary<int, CItemInfo>();
	}

	public Dictionary<int, CItemInfo> GetData()
	{
		return m_dictItemInfo;
	}

	public CItemInfo Get(int nID)
	{
		if (!m_dictItemInfo.ContainsKey(nID))
		{
			return null;
		}
		return m_dictItemInfo[nID];
	}

	public CItemInfoLevel Get(int nID, int nLevel)
	{
		CItemInfo cItemInfo = Get(nID);
		if (cItemInfo == null)
		{
			return null;
		}
		return cItemInfo.Get(nLevel);
	}

	protected override void LoadData(string content)
	{
		m_dictItemInfo.Clear();
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (childNode.Name != "item" || !MyUtils.GetAttribute(childNode, "id", ref value))
			{
				continue;
			}
			int num = int.Parse(value);
			int nLevel = 1;
			if (MyUtils.GetAttribute(childNode, "lvl", ref value))
			{
				nLevel = int.Parse(value);
			}
			CItemInfo cItemInfo = Get(num);
			if (cItemInfo == null)
			{
				cItemInfo = new CItemInfo();
				cItemInfo.nID = num;
				m_dictItemInfo.Add(num, cItemInfo);
			}
			CItemInfoLevel cItemInfoLevel = cItemInfo.Get(nLevel);
			if (cItemInfoLevel == null)
			{
				cItemInfoLevel = new CItemInfoLevel();
				cItemInfoLevel.nID = num;
				cItemInfoLevel.nLevel = nLevel;
				cItemInfo.Add(nLevel, cItemInfoLevel);
			}
			if (MyUtils.GetAttribute(childNode, "unlockstage", ref value))
			{
				cItemInfo.nUnLockLevel = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "type", ref value))
			{
				cItemInfoLevel.nType = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "rare", ref value))
			{
				cItemInfoLevel.nRare = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "model", ref value))
			{
				cItemInfoLevel.nModel = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "name", ref value))
			{
				cItemInfoLevel.sName = value;
			}
			else
			{
				cItemInfoLevel.sName = "Item " + cItemInfoLevel.nID;
			}
			if (MyUtils.GetAttribute(childNode, "desc", ref value))
			{
				cItemInfoLevel.sDesc = value;
			}
			else
			{
				cItemInfoLevel.sDesc = "This is desc of Item " + cItemInfoLevel.nID;
			}
			if (MyUtils.GetAttribute(childNode, "icon", ref value))
			{
				cItemInfoLevel.sIcon = value;
			}
			if (MyUtils.GetAttribute(childNode, "func", ref value))
			{
				string[] array = value.Split(',');
				for (int i = 0; i < array.Length && i < cItemInfoLevel.arrFunc.Length; i++)
				{
					cItemInfoLevel.arrFunc[i] = int.Parse(array[i]);
				}
			}
			if (MyUtils.GetAttribute(childNode, "valuex", ref value))
			{
				string[] array = value.Split(',');
				for (int j = 0; j < array.Length && j < cItemInfoLevel.arrValueX.Length; j++)
				{
					cItemInfoLevel.arrValueX[j] = int.Parse(array[j]);
				}
			}
			if (MyUtils.GetAttribute(childNode, "valuey", ref value))
			{
				string[] array = value.Split(',');
				for (int k = 0; k < array.Length && k < cItemInfoLevel.arrValueY.Length; k++)
				{
					cItemInfoLevel.arrValueY[k] = int.Parse(array[k]);
				}
			}
			if (MyUtils.GetAttribute(childNode, "takenbuff", ref value))
			{
				cItemInfoLevel.nTakenBuff = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "iscrystalpurchase", ref value))
			{
				cItemInfoLevel.isCrystalPurchase = bool.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "purchaseprice", ref value))
			{
				cItemInfoLevel.nPurchasePrice = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "iscrystalsell", ref value))
			{
				cItemInfoLevel.isCrystalSell = bool.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "sellprice", ref value))
			{
				cItemInfoLevel.nSellPrice = int.Parse(value);
			}
			if (MyUtils.GetAttribute(childNode, "materials", ref value))
			{
				string[] array = value.Split(',');
				for (int l = 0; l < array.Length; l++)
				{
					cItemInfoLevel.ltMaterials.Add(int.Parse(array[l]));
				}
			}
			if (MyUtils.GetAttribute(childNode, "materialscount", ref value))
			{
				string[] array = value.Split(',');
				for (int m = 0; m < array.Length; m++)
				{
					cItemInfoLevel.ltMaterialsCount.Add(int.Parse(array[m]));
				}
			}
			if (MyUtils.GetAttribute(childNode, "levelupdesc", ref value))
			{
				cItemInfoLevel.sLevelUpDesc = value;
			}
		}
	}
}
