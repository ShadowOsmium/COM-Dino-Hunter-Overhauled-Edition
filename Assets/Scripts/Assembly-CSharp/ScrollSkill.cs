using UnityEngine;

public class ScrollSkill : MonoBehaviour
{
	public TUIScrollListEx scroll_list_ex_prefab;

	private TUIScrollListEx[] scroll_list_ex_list;

	private TUIScrollListEx scroll_list_ex_now;

	private ScrollList_SkillItem item_choose;

	private void Start()
	{
	}

	private void Update()
	{
		CheckItemChoose();
	}

	public void AddScrollList(TUIAllSkillInfo m_all_role_skill_info)
	{
		if (m_all_role_skill_info == null || m_all_role_skill_info.all_role_skill_list == null)
		{
			Debug.Log("no role skill!");
			return;
		}
		scroll_list_ex_list = new TUIScrollListEx[m_all_role_skill_info.all_role_skill_list.Length];
		TUISkillListInfo[] all_role_skill_list = m_all_role_skill_info.all_role_skill_list;
		for (int i = 0; i < scroll_list_ex_list.Length; i++)
		{
			scroll_list_ex_list[i] = (TUIScrollListEx)Object.Instantiate(scroll_list_ex_prefab);
			scroll_list_ex_list[i].transform.parent = base.gameObject.transform;
			scroll_list_ex_list[i].transform.localPosition = new Vector3(0f, 1000f, 0f);
			scroll_list_ex_list[i].GetComponent<ScrollList_Skill>().AddItem(all_role_skill_list[i], i + 1);
		}
	}

	public void UpdateNewMark(TUIAllSkillInfo m_role_skill_info)
	{
		if (scroll_list_ex_list == null || m_role_skill_info == null)
		{
			Debug.Log("error!");
			return;
		}
		TUISkillListInfo[] all_role_skill_list = m_role_skill_info.all_role_skill_list;
		if (all_role_skill_list == null)
		{
			return;
		}
		for (int i = 0; i < all_role_skill_list.Length; i++)
		{
			for (int j = 0; j < scroll_list_ex_list.Length; j++)
			{
				ScrollList_Skill component = scroll_list_ex_list[j].GetComponent<ScrollList_Skill>();
				if (all_role_skill_list[i].id == component.GetID())
				{
					component.UpdateNewMark(all_role_skill_list[i].new_mark_list);
				}
			}
		}
	}

	public void ScrollListChoose(int m_index, int m_skill_index = -1)
	{
		if (m_index < 0 || m_index > scroll_list_ex_list.Length)
		{
			Debug.Log("error!");
			return;
		}
		if (scroll_list_ex_now != null)
		{
			scroll_list_ex_now.transform.localPosition = new Vector3(0f, 1000f, 0f);
		}
		scroll_list_ex_now = scroll_list_ex_list[m_index];
		scroll_list_ex_now.transform.localPosition = new Vector3(0f, 0f, 0f);
		item_choose = null;
		if (m_skill_index >= 0 && scroll_list_ex_now != null)
		{
			ScrollList_Skill component = scroll_list_ex_now.GetComponent<ScrollList_Skill>();
			if (component != null)
			{
				component.SetItemChoose(m_skill_index);
			}
		}
	}

	private void CheckItemChoose()
	{
		GameObject nowItem = scroll_list_ex_now.GetNowItem();
		if (nowItem == null)
		{
			return;
		}
		ScrollList_SkillItem component = nowItem.GetComponent<ScrollList_SkillItem>();
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

	public TUIScrollListEx GetScrollListChoose()
	{
		return scroll_list_ex_now;
	}

	public ScrollList_SkillItem GetItemChoose()
	{
		return item_choose;
	}
}
