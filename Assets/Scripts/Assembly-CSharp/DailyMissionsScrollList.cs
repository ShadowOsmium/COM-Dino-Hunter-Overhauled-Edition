using System.Collections.Generic;
using UnityEngine;

public class DailyMissionsScrollList : MonoBehaviour
{
	public TUIScrollList scrollist;

	public DailyMissionsItem prefab_item;

	public TUIRect rect_show;

	private List<TUIOneDailyMissionsInfo> daily_missions_list;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void DoCreate(TUIDailyMissionsInfo m_info, GameObject m_go_invoke)
	{
		if (scrollist == null || prefab_item == null)
		{
			Debug.Log("error!");
			return;
		}
		daily_missions_list = m_info.daily_missions_list;
		if (daily_missions_list == null)
		{
			Debug.Log("error! no info!");
			return;
		}
		for (int i = 0; i < daily_missions_list.Count; i++)
		{
			DailyMissionsItem dailyMissionsItem = (DailyMissionsItem)Object.Instantiate(prefab_item);
			if (dailyMissionsItem != null)
			{
				dailyMissionsItem.transform.parent = base.transform;
				dailyMissionsItem.transform.localPosition = new Vector3(0f, 0f, 0f);
				dailyMissionsItem.DoCreate(daily_missions_list[i], m_go_invoke);
				scrollist.Add(dailyMissionsItem.GetComponent<TUIControl>());
			}
			else
			{
				Debug.Log("errror!");
			}
		}
	}

	public void AfterTakeReward(TUIControl m_control)
	{
		if (!(m_control == null) && !(m_control.transform.parent == null))
		{
			DailyMissionsItem component = m_control.transform.parent.GetComponent<DailyMissionsItem>();
			if (!(component == null))
			{
				component.AfterTakeReward();
			}
		}
	}

	public TUIAchievementRewardInfo TakeReward(TUIControl m_control)
	{
		DailyMissionsItem component = m_control.transform.parent.GetComponent<DailyMissionsItem>();
		if (component == null)
		{
			return null;
		}
		return component.TakeReward();
	}
}
