using UnityEngine;

public class Popup_Achievement : MonoBehaviour
{
	public GameObject go_popup;

	public AchievementScrollList chievement_scrolllist;

	private TUIControl btn_take_achievement;

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

	public void DoCreateAchievement(TUIAchievementInfo m_info, GameObject m_go_invoke)
	{
		if (chievement_scrolllist != null)
		{
			chievement_scrolllist.DoCreate(m_info, m_go_invoke);
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

	public void SetTakeAchievementBtn(TUIControl m_control)
	{
		btn_take_achievement = m_control;
	}

	public void AfterTakeAchievement()
	{
		chievement_scrolllist.AfterTakeAchievement(btn_take_achievement);
	}

	public TUIAchievementRewardInfo TakeAchievement()
	{
		return chievement_scrolllist.TakeAchievement(btn_take_achievement);
	}
}
