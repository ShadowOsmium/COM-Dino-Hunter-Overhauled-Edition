using System.Collections.Generic;
using System.Xml;

public class iShopDisplayCenter : iBaseCenter
{
	public List<int> m_ltCharacter;

	public List<int> m_ltWeapon_Melee;

	public List<int> m_ltWeapon_Autorifle;

	public List<int> m_ltWeapon_Bow;

	public List<int> m_ltWeapon_Shotgun;

	public List<int> m_ltWeapon_Rocket;

	public List<int> m_ltWeapon_Holdgun;

	public List<int> m_ltAvatar_Armor;

	public List<int> m_ltAvatar_Accessory;

	public List<int> m_ltSellInBlack;

	public iShopDisplayCenter()
	{
		m_ltCharacter = new List<int>();
		m_ltWeapon_Melee = new List<int>();
		m_ltWeapon_Autorifle = new List<int>();
		m_ltWeapon_Bow = new List<int>();
		m_ltWeapon_Shotgun = new List<int>();
		m_ltWeapon_Rocket = new List<int>();
		m_ltWeapon_Holdgun = new List<int>();
		m_ltAvatar_Armor = new List<int>();
		m_ltAvatar_Accessory = new List<int>();
		m_ltSellInBlack = new List<int>();
	}

	public bool IsInSellInBlack(int nID)
	{
		foreach (int item in m_ltSellInBlack)
		{
			if (item == nID)
			{
				return true;
			}
		}
		return false;
	}

	public List<int> GetRangeList()
	{
		List<int> list = new List<int>();
		list.AddRange(m_ltWeapon_Autorifle);
		list.AddRange(m_ltWeapon_Bow);
		list.AddRange(m_ltWeapon_Shotgun);
		list.AddRange(m_ltWeapon_Rocket);
		list.AddRange(m_ltWeapon_Holdgun);
		return list;
	}

	protected override void LoadData(string content)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(content);
		string value = string.Empty;
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (childNode.Name == "shop_character")
			{
				if (MyUtils.GetAttribute(childNode, "display", ref value))
				{
					SetData(m_ltCharacter, value.Split(','));
				}
			}
			else if (childNode.Name == "shop_weapon_melee")
			{
				if (MyUtils.GetAttribute(childNode, "display", ref value))
				{
					SetData(m_ltWeapon_Melee, value.Split(','));
				}
			}
			else if (childNode.Name == "shop_weapon_autorifle")
			{
				if (MyUtils.GetAttribute(childNode, "display", ref value))
				{
					SetData(m_ltWeapon_Autorifle, value.Split(','));
				}
			}
			else if (childNode.Name == "shop_weapon_bow")
			{
				if (MyUtils.GetAttribute(childNode, "display", ref value))
				{
					SetData(m_ltWeapon_Bow, value.Split(','));
				}
			}
			else if (childNode.Name == "shop_weapon_shotgun")
			{
				if (MyUtils.GetAttribute(childNode, "display", ref value))
				{
					SetData(m_ltWeapon_Shotgun, value.Split(','));
				}
			}
			else if (childNode.Name == "shop_weapon_rocket")
			{
				if (MyUtils.GetAttribute(childNode, "display", ref value))
				{
					SetData(m_ltWeapon_Rocket, value.Split(','));
				}
			}
			else if (childNode.Name == "shop_weapon_holdgun")
			{
				if (MyUtils.GetAttribute(childNode, "display", ref value))
				{
					SetData(m_ltWeapon_Holdgun, value.Split(','));
				}
			}
			else if (childNode.Name == "shop_avatar_armor")
			{
				if (MyUtils.GetAttribute(childNode, "display", ref value))
				{
					SetData(m_ltAvatar_Armor, value.Split(','));
				}
			}
			else if (childNode.Name == "shop_avatar_accessory")
			{
				if (MyUtils.GetAttribute(childNode, "display", ref value))
				{
					SetData(m_ltAvatar_Accessory, value.Split(','));
				}
			}
			else if (childNode.Name == "shop_sellinblack" && MyUtils.GetAttribute(childNode, "itemlist", ref value))
			{
				SetData(m_ltSellInBlack, value.Split(','));
			}
		}
	}

	protected void SetData(List<int> ltList, string[] arrTemp)
	{
		if (arrTemp != null && arrTemp.Length >= 1)
		{
			ltList.Clear();
			for (int i = 0; i < arrTemp.Length; i++)
			{
				ltList.Add(int.Parse(arrTemp[i]));
			}
		}
	}
}
