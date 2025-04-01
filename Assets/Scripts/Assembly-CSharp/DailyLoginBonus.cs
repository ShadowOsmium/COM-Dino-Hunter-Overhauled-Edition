using System.Collections.Generic;
using UnityEngine;

public class DailyLoginBonus : MonoBehaviour
{
	public GameObject go_popup;

	public List<DailyLoginBonusItem> daily_bonus_item_list;

	public TUIPriceInfo price_info;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Show()
	{
		base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		if (go_popup != null && go_popup.GetComponent<Animation>() != null)
		{
			go_popup.GetComponent<Animation>().Play();
		}
	}

	public void Hide()
	{
		base.transform.localPosition = new Vector3(0f, -1000f, base.transform.localPosition.z);
	}

	public void SetInfo(TUIDailyLoginBonusInfo m_info)
	{
		if (m_info == null)
		{
			Debug.Log("error!");
			return;
		}
		if (daily_bonus_item_list == null || daily_bonus_item_list.Count != 7)
		{
			Debug.Log("error!");
			return;
		}
		List<TUIPriceInfo> daily_bonus_list = m_info.daily_bonus_list;
		int day = m_info.day;
		for (int i = 0; i < daily_bonus_list.Count; i++)
		{
			TUIPriceInfo tUIPriceInfo = daily_bonus_list[i];
			DailyLoginBonusItem dailyLoginBonusItem = daily_bonus_item_list[i];
			if (dailyLoginBonusItem != null)
			{
				if (tUIPriceInfo != null)
				{
					dailyLoginBonusItem.SetInfo(tUIPriceInfo);
				}
				if (i == day - 1)
				{
					dailyLoginBonusItem.SetGrayStyle(false);
					price_info = tUIPriceInfo;
				}
				else
				{
					dailyLoginBonusItem.SetGrayStyle(true);
				}
			}
		}
	}

	public TUIPriceInfo GetDailyLoginBonusInfo()
	{
		return price_info;
	}
}
