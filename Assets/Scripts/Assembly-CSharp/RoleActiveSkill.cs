using System.Collections.Generic;
using UnityEngine;

public class RoleActiveSkill : MonoBehaviour
{
	public List<BtnItem_Item> btn_active_skill_list;

	private List<TUIPopupInfo> active_skill_list;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetInfo(List<TUIPopupInfo> m_active_skill_list)
	{
		active_skill_list = m_active_skill_list;
		if (active_skill_list == null)
		{
			Debug.Log("warning no skill list!");
			return;
		}
		for (int i = 0; i < btn_active_skill_list.Count; i++)
		{
			if (btn_active_skill_list[i] != null)
			{
				btn_active_skill_list[i].gameObject.SetActiveRecursively(false);
			}
		}
		for (int j = 0; j < active_skill_list.Count; j++)
		{
			if (btn_active_skill_list[j] != null)
			{
				btn_active_skill_list[j].SetInfo(active_skill_list[j], false, true);
				btn_active_skill_list[j].gameObject.SetActiveRecursively(true);
				TUIButtonClick component = btn_active_skill_list[j].GetComponent<TUIButtonClick>();
				if (component != null)
				{
					component.Reset();
				}
			}
		}
	}
}
