using System.Collections.Generic;
using UnityEngine;

public class TUIWeaponInfo
{
	public Dictionary<kShopWeaponCategory, List<TUIWeaponAttributeInfo>> m_dictWeaponData;

	public kShopWeaponCategory m_nLinkCategory;

	public int m_nLinkID;

	public TUIWeaponInfo()
	{
		m_dictWeaponData = new Dictionary<kShopWeaponCategory, List<TUIWeaponAttributeInfo>>();
		m_nLinkCategory = kShopWeaponCategory.None;
		m_nLinkID = -1;
	}

	public List<TUIWeaponAttributeInfo> Get(kShopWeaponCategory weapontype)
	{
		if (!m_dictWeaponData.ContainsKey(weapontype))
		{
			return null;
		}
		return m_dictWeaponData[weapontype];
	}

	public void AddItem(kShopWeaponCategory nCategory, TUIWeaponAttributeInfo weaponattributeinfo)
	{
		if (weaponattributeinfo == null)
		{
			return;
		}
		List<TUIWeaponAttributeInfo> list = Get(nCategory);
		if (list == null)
		{
			list = new List<TUIWeaponAttributeInfo>();
			if (list == null)
			{
				return;
			}
			m_dictWeaponData.Add(nCategory, list);
		}
		list.Add(weaponattributeinfo);
	}

	public void SetLinkInfo(kShopWeaponCategory nLinkCategory, int nLinkID)
	{
		Debug.Log(string.Concat("SetLinkInfo ", m_nLinkCategory, " ", m_nLinkID));
		m_nLinkCategory = nLinkCategory;
		m_nLinkID = nLinkID;
	}

	public NewMarkType GetMark(kShopWeaponCategory nCategory)
	{
		List<TUIWeaponAttributeInfo> list = Get(nCategory);
		if (list == null)
		{
			return NewMarkType.None;
		}
		NewMarkType result = NewMarkType.None;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] != null)
			{
				if (list[i].m_Mark == NewMarkType.New)
				{
					return NewMarkType.New;
				}
				if (list[i].m_Mark == NewMarkType.Mark)
				{
					result = NewMarkType.Mark;
				}
			}
		}
		return result;
	}

	public void RefreshMark(Dictionary<int, NewMarkType> dictMarkData)
	{
		foreach (List<TUIWeaponAttributeInfo> value in m_dictWeaponData.Values)
		{
			foreach (TUIWeaponAttributeInfo item in value)
			{
				if (dictMarkData.ContainsKey(item.m_nID))
				{
					item.m_Mark = dictMarkData[item.m_nID];
				}
			}
		}
	}
}
