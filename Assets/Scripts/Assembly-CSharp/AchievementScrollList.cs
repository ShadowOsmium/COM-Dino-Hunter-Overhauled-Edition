using System.Collections.Generic;
using UnityEngine;

public class AchievementScrollList : MonoBehaviour
{
	public TUIScrollList scrollist;

	public AchievementItem prefab_item;

	public TUIRect rect_show;

	private List<TUIOneAchievementInfo> achievement_list;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void DoCreate(TUIAchievementInfo m_info, GameObject m_go_invoke)
	{
		if (scrollist == null || prefab_item == null)
		{
			Debug.Log("error!");
			return;
		}
		achievement_list = m_info.achievement_list;
		if (achievement_list == null)
		{
			Debug.Log("error! no info!");
			return;
		}
		for (int i = 0; i < achievement_list.Count; i++)
		{
			AchievementItem achievementItem = (AchievementItem)Object.Instantiate(prefab_item);
			if (achievementItem != null)
			{
				achievementItem.transform.parent = base.transform;
				achievementItem.transform.localPosition = new Vector3(0f, 0f, 0f);
				achievementItem.DoCreate(achievement_list[i], m_go_invoke);
				scrollist.Add(achievementItem.GetComponent<TUIControl>());
			}
			else
			{
				Debug.Log("errror!");
			}
		}
	}

	public void AfterTakeAchievement(TUIControl m_control)
	{
		if (!(m_control == null) && !(m_control.transform.parent == null))
		{
			AchievementItem component = m_control.transform.parent.GetComponent<AchievementItem>();
			if (!(component == null))
			{
				component.AfterTakeAchievement();
			}
		}
	}

	public TUIAchievementRewardInfo TakeAchievement(TUIControl m_control)
	{
		AchievementItem component = m_control.transform.parent.GetComponent<AchievementItem>();
		if (component == null)
		{
			return null;
		}
		return component.TakeAchievement();
	}
}
