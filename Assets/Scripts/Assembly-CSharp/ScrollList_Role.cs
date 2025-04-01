using System.Collections.Generic;
using UnityEngine;

public class ScrollList_Role : MonoBehaviour
{
	public ScrollList_RoleItem item_prefab;

	public TUIGrid grid;

	private TUIScrollListCircle scroll_list_ex;

	private ScrollList_RoleItem item_choose;

	private void Awake()
	{
	}

	private void Start()
	{
		scroll_list_ex = base.gameObject.GetComponent<TUIScrollListCircle>();
	}

	private void Update()
	{
		CheckItemChoose();
	}

	public void AddScrollListItem(TUIAllRoleInfo m_info)
	{
		if (m_info == null || m_info.role_list == null)
		{
			Debug.Log("no roll_list!");
			return;
		}
		if (scroll_list_ex == null)
		{
			scroll_list_ex = base.gameObject.GetComponent<TUIScrollListCircle>();
		}
		for (int i = 0; i < m_info.role_list.Length; i++)
		{
			ScrollList_RoleItem scrollList_RoleItem = (ScrollList_RoleItem)Object.Instantiate(item_prefab);
			scrollList_RoleItem.transform.parent = grid.transform;
			scrollList_RoleItem.DoCreate(m_info.role_list[i], m_info.new_mark_list);
			scroll_list_ex.Add(scrollList_RoleItem.gameObject);
		}
		scroll_list_ex.ResetGrid();
		scroll_list_ex.SetItemList();
		if (!m_info.open_link)
		{
			return;
		}
		int role_link_id = m_info.role_link_id;
		List<GameObject> itemsList = scroll_list_ex.GetItemsList();
		if (itemsList == null)
		{
			return;
		}
		for (int j = 0; j < itemsList.Count; j++)
		{
			ScrollList_RoleItem component = itemsList[j].GetComponent<ScrollList_RoleItem>();
			if (component != null)
			{
				TUIRoleInfo roleInfo = component.GetRoleInfo();
				if (roleInfo != null && roleInfo.id == role_link_id)
				{
					grid.repositionStart = false;
					scroll_list_ex.SetNowItem(itemsList[j]);
				}
			}
		}
	}

	public void UpdateNewMark(Dictionary<int, NewMarkType> m_new_mark_list)
	{
		if (m_new_mark_list == null || scroll_list_ex == null)
		{
			Debug.Log("error!");
			return;
		}
		List<GameObject> itemsList = scroll_list_ex.GetItemsList();
		if (itemsList == null)
		{
			Debug.Log("error!");
			return;
		}
		for (int i = 0; i < itemsList.Count; i++)
		{
			ScrollList_RoleItem component = itemsList[i].GetComponent<ScrollList_RoleItem>();
			if (component != null)
			{
				component.UpdateNewMark(m_new_mark_list);
			}
		}
	}

	public void CheckItemChoose()
	{
		GameObject nowItem = scroll_list_ex.GetNowItem();
		if (nowItem == null)
		{
			return;
		}
		ScrollList_RoleItem component = nowItem.GetComponent<ScrollList_RoleItem>();
		if (item_choose == null)
		{
			if (component != null)
			{
				item_choose = component;
				item_choose.DoChoose();
			}
		}
		else if (item_choose != component)
		{
			item_choose.DoUnChoose();
			item_choose = component;
			item_choose.DoChoose();
		}
	}

	public void ResetPosition()
	{
		grid.repositionNow = true;
	}

	public void ResetPositionNow()
	{
		grid.Reposition();
	}

	public ScrollList_RoleItem GetItemChoose()
	{
		return item_choose;
	}
}
