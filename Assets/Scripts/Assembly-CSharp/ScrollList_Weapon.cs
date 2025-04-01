using System.Collections.Generic;
using UnityEngine;

public class ScrollList_Weapon : MonoBehaviour
{
	public ScrollList_WeaponItem item_prefab;

	public TUIGrid grid;

	protected TUIScrollListCircle scroll_list_ex;

	protected List<ScrollList_WeaponItem> ltScrollList_WeaponItem;

	protected int m_nCurIndex;

	protected Vector3 m_v3InitPos = Vector3.zero;

	private void Awake()
	{
		ltScrollList_WeaponItem = new List<ScrollList_WeaponItem>();
		m_nCurIndex = -1;
		scroll_list_ex = base.gameObject.GetComponent<TUIScrollListCircle>();
	}

	private void Start()
	{
	}

	private void Update()
	{
		CheckItemChoose();
	}

	private void CheckItemChoose()
	{
		GameObject nowItem = scroll_list_ex.GetNowItem();
		if (nowItem == null)
		{
			return;
		}
		ScrollList_WeaponItem component = nowItem.GetComponent<ScrollList_WeaponItem>();
		if (component == null)
		{
			return;
		}
		if (m_nCurIndex >= 0 && m_nCurIndex < ltScrollList_WeaponItem.Count)
		{
			ScrollList_WeaponItem scrollList_WeaponItem = ltScrollList_WeaponItem[m_nCurIndex];
			if (scrollList_WeaponItem != null)
			{
				scrollList_WeaponItem.OnUnselect();
			}
		}
		m_nCurIndex = -1;
		for (int i = 0; i < ltScrollList_WeaponItem.Count; i++)
		{
			if (ltScrollList_WeaponItem[i] == component)
			{
				m_nCurIndex = i;
				break;
			}
		}
		if (m_nCurIndex >= 0 && m_nCurIndex < ltScrollList_WeaponItem.Count)
		{
			ScrollList_WeaponItem scrollList_WeaponItem2 = ltScrollList_WeaponItem[m_nCurIndex];
			if (scrollList_WeaponItem2 != null)
			{
				scrollList_WeaponItem2.OnSelect();
			}
		}
	}

	public void SetItem(List<TUIWeaponAttributeInfo> ltWeaponAttributeInfo)
	{
		if (ltWeaponAttributeInfo == null)
		{
			return;
		}
		for (int i = 0; i < ltWeaponAttributeInfo.Count; i++)
		{
			if (ltWeaponAttributeInfo[i] != null)
			{
				ScrollList_WeaponItem scrollList_WeaponItem = (ScrollList_WeaponItem)Object.Instantiate(item_prefab);
				scrollList_WeaponItem.transform.parent = grid.transform;
				scrollList_WeaponItem.DoCreate(ltWeaponAttributeInfo[i]);
				ltScrollList_WeaponItem.Add(scrollList_WeaponItem);
				if (scroll_list_ex != null)
				{
					scroll_list_ex.Add(scrollList_WeaponItem.gameObject);
				}
			}
		}
		if (scroll_list_ex != null)
		{
			scroll_list_ex.ResetGrid();
			scroll_list_ex.SetItemList();
		}
		m_v3InitPos = base.transform.localPosition;
	}

	public void ResetPosition()
	{
		grid.repositionNow = true;
	}

	public void ResetPositionNow()
	{
		grid.Reposition();
	}

	public ScrollList_WeaponItem GetItemChoose()
	{
		if (m_nCurIndex >= 0 && m_nCurIndex < ltScrollList_WeaponItem.Count)
		{
			ScrollList_WeaponItem scrollList_WeaponItem = ltScrollList_WeaponItem[m_nCurIndex];
			if (scrollList_WeaponItem != null)
			{
				return scrollList_WeaponItem;
			}
		}
		return null;
	}

	public void Show()
	{
		base.transform.localPosition = m_v3InitPos;
	}

	public void Hide()
	{
		ResetPositionNow();
		base.transform.localPosition = m_v3InitPos + new Vector3(0f, 1000f, 0f);
	}

	public void RefreshMark()
	{
		foreach (ScrollList_WeaponItem item in ltScrollList_WeaponItem)
		{
			item.RefreshMark();
		}
	}

	public void SetItemChoose(int m_weapon_id)
	{
		if (ltScrollList_WeaponItem == null)
		{
			return;
		}
		for (int i = 0; i < ltScrollList_WeaponItem.Count; i++)
		{
			if (!(ltScrollList_WeaponItem[i] == null))
			{
				TUIWeaponAttributeInfo weaponAttributeInfo = ltScrollList_WeaponItem[i].GetWeaponAttributeInfo();
				if (weaponAttributeInfo != null && weaponAttributeInfo.m_nID == m_weapon_id)
				{
					m_nCurIndex = i;
					break;
				}
			}
		}
		grid.Reposition();
		grid.repositionStart = false;
		scroll_list_ex.SetNowItem(m_nCurIndex);
	}
}
