using UnityEngine;

public class Popup_DailyMissions : MonoBehaviour
{
	public GameObject go_popup;

	public DailyMissionsScrollList daily_missions_scrolllist;

	public TUILabel label_text;

	private TUIControl btn_claim;

	public TUIMeshSprite img_scroll2d_bg;

	private void Start()
	{
		if (img_scroll2d_bg != null)
		{
			img_scroll2d_bg.gameObject.layer = 9;
		}
	}

	private void Update()
	{
	}

	public void DoCreateDailyMissions(TUIDailyMissionsInfo m_info, GameObject m_go_invoke)
	{
		if (daily_missions_scrolllist != null)
		{
			daily_missions_scrolllist.DoCreate(m_info, m_go_invoke);
		}
		if (m_info == null || m_info.daily_missions_list == null || m_info.daily_missions_list.Count == 0)
		{
			if (label_text != null)
			{
				label_text.gameObject.SetActiveRecursively(true);
			}
		}
		else if (label_text != null)
		{
			label_text.gameObject.SetActiveRecursively(false);
		}
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

	public void SetClaimBtn(TUIControl m_control)
	{
		btn_claim = m_control;
	}

	public void AfterTakeReward()
	{
		daily_missions_scrolllist.AfterTakeReward(btn_claim);
	}

	public TUIAchievementRewardInfo TakeReward()
	{
		return daily_missions_scrolllist.TakeReward(btn_claim);
	}
}
