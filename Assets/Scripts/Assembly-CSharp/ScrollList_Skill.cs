using System.Collections.Generic;
using UnityEngine;

public class ScrollList_Skill : MonoBehaviour
{
	public ScrollList_SkillItem item_prefab;

	public TUIScrollListEx scroll_list_ex;

	public TUIGrid grid;

	private int index;

	private int id;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void AddItem(TUISkillListInfo m_skill_list_info, int m_index)
	{
		if (m_skill_list_info == null)
		{
			Debug.Log("no skill!");
			return;
		}
		TUISkillInfo[] skill_list_info = m_skill_list_info.skill_list_info;
		int iD = m_skill_list_info.id;
		Dictionary<int, NewMarkType> new_mark_list = m_skill_list_info.new_mark_list;
		SetIndex(m_index);
		SetID(iD);
		if (skill_list_info != null)
		{
			for (int i = 0; i < skill_list_info.Length; i++)
			{
				ScrollList_SkillItem scrollList_SkillItem = (ScrollList_SkillItem)Object.Instantiate(item_prefab);
				scrollList_SkillItem.transform.parent = grid.transform;
				scrollList_SkillItem.DoCreate(skill_list_info[i], new_mark_list);
				scroll_list_ex.Add(scrollList_SkillItem.gameObject);
			}
		}
		ResetPositionNow();
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
			return;
		}
		for (int i = 0; i < itemsList.Count; i++)
		{
			ScrollList_SkillItem component = itemsList[i].GetComponent<ScrollList_SkillItem>();
			if (component != null)
			{
				component.UpdateNewMark(m_new_mark_list);
			}
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

	public void SetIndex(int m_index)
	{
		index = m_index;
	}

	public int GetIndex()
	{
		return index;
	}

	public void SetID(int m_id)
	{
		id = m_id;
	}

	public int GetID()
	{
		return id;
	}

	public void SetItemChoose(int m_index)
	{
		if (scroll_list_ex == null)
		{
			Debug.Log("error!");
			return;
		}
		grid.repositionStart = false;
		scroll_list_ex.SetNowItem(m_index);
	}
}
