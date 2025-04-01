using UnityEngine;

public class DailyMissionsItem : MonoBehaviour
{
	public TUILabel label_name;

	public TUILabel label_introduce;

	public AchievementBar achievement_bar;

	public AchievementRewardText achievement_reward_text_right;

	public TUIButtonClick btn_achievement;

	public TUIMeshSprite img_texture;

	public TUIMeshSprite img_arrow;

	private TUIOneDailyMissionsInfo daily_missions_info;

	private void Start()
	{
		Transform[] componentsInChildren = base.gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].gameObject.layer = 9;
			TUIDrawSprite component = componentsInChildren[i].GetComponent<TUIDrawSprite>();
			if (component != null)
			{
				component.clippingType = TUIDrawSprite.Clipping.None;
			}
		}
	}

	private void Update()
	{
	}

	public void DoCreate(TUIOneDailyMissionsInfo m_daily_mission_info, GameObject m_go_invoke = null)
	{
		daily_missions_info = m_daily_mission_info;
		if (daily_missions_info == null)
		{
			Debug.Log("no info!");
			return;
		}
		if (btn_achievement != null && m_go_invoke != null)
		{
			btn_achievement.invokeObject = m_go_invoke;
		}
		string text = daily_missions_info.name;
		string introduce = daily_missions_info.introduce;
		TUIAchievementRewardInfo reward_info = daily_missions_info.reward_info;
		int progress = daily_missions_info.progress;
		bool take_reward = daily_missions_info.take_reward;
		string progress_text = daily_missions_info.progress_text;
		bool flag = false;
		bool flag2 = false;
		int reward = 0;
		int reward2 = 0;
		UnitType unit = UnitType.Gold;
		UnitType unit2 = UnitType.Gold;
		if (reward_info != null)
		{
			if (reward_info.open_reward01)
			{
				flag = true;
				reward = reward_info.reward_value01;
				unit = reward_info.reward_unit01;
			}
			if (reward_info.open_reward02)
			{
				flag2 = true;
				reward2 = reward_info.reward_value02;
				unit2 = reward_info.reward_unit02;
			}
		}
		if (flag && !flag2)
		{
			DoCreateEx(take_reward, text, introduce, progress, reward, unit, progress_text);
		}
		else if (flag && flag2)
		{
			DoCreateEx(take_reward, text, introduce, progress, reward, unit, reward2, unit2, progress_text);
		}
		else
		{
			Debug.Log("no reward?");
		}
	}

	public void DoCreateEx(bool m_take_reward, string m_name, string m_introduce, int m_progress, int m_reward01, UnitType m_unit01, string m_progress_text)
	{
		if (label_name != null)
		{
			label_name.Text = m_name;
		}
		if (label_introduce != null)
		{
			label_introduce.Text = m_introduce;
		}
		if (achievement_bar != null)
		{
			achievement_bar.Show(m_progress, m_progress_text);
		}
		if (img_arrow != null)
		{
			img_arrow.gameObject.SetActiveRecursively(false);
		}
		Btn_Achievement component = btn_achievement.GetComponent<Btn_Achievement>();
		if (m_progress == 100)
		{
			if (component != null)
			{
				if (m_take_reward)
				{
					component.SetStateText(Btn_Achievement.BtnAchievementState.State_Finish);
					btn_achievement.Disable(true);
					img_arrow.gameObject.SetActiveRecursively(true);
				}
				else
				{
					component.SetStateText(Btn_Achievement.BtnAchievementState.State_Normal);
					btn_achievement.Disable(false);
				}
			}
		}
		else
		{
			if (component != null)
			{
				component.SetStateText(Btn_Achievement.BtnAchievementState.State_Disable);
			}
			btn_achievement.Disable(true);
		}
		if (achievement_reward_text_right != null)
		{
			achievement_reward_text_right.Show(m_reward01, m_unit01);
		}
	}

	public void DoCreateEx(bool m_take_reward, string m_name, string m_introduce, int m_progress, int m_reward01, UnitType m_unit01, int m_reward02, UnitType m_unit02, string m_progress_text)
	{
		if (label_name != null)
		{
			label_name.Text = m_name;
		}
		if (label_introduce != null)
		{
			label_introduce.Text = m_introduce;
		}
		if (achievement_bar != null)
		{
			achievement_bar.Show(m_progress, m_progress_text);
		}
		if (img_arrow != null)
		{
			img_arrow.gameObject.SetActiveRecursively(false);
		}
		Btn_Achievement component = btn_achievement.GetComponent<Btn_Achievement>();
		if (m_progress == 100)
		{
			if (component != null)
			{
				if (m_take_reward)
				{
					component.SetStateText(Btn_Achievement.BtnAchievementState.State_Finish);
					btn_achievement.Disable(true);
					img_arrow.gameObject.SetActiveRecursively(true);
				}
				else
				{
					component.SetStateText(Btn_Achievement.BtnAchievementState.State_Normal);
					btn_achievement.Disable(false);
				}
			}
		}
		else
		{
			if (component != null)
			{
				component.SetStateText(Btn_Achievement.BtnAchievementState.State_Disable);
			}
			btn_achievement.Disable(true);
		}
		if (achievement_reward_text_right != null)
		{
			achievement_reward_text_right.Show(m_reward01, m_unit01, m_reward02, m_unit02);
		}
	}

	public void AfterTakeReward()
	{
		if (daily_missions_info == null)
		{
			Debug.Log("no info!");
			return;
		}
		string text = daily_missions_info.name;
		string introduce = daily_missions_info.introduce;
		TUIAchievementRewardInfo reward_info = daily_missions_info.reward_info;
		int progress = daily_missions_info.progress;
		daily_missions_info.take_reward = true;
		bool take_reward = daily_missions_info.take_reward;
		string progress_text = daily_missions_info.progress_text;
		bool flag = false;
		bool flag2 = false;
		int reward = 0;
		int reward2 = 0;
		UnitType unit = UnitType.Gold;
		UnitType unit2 = UnitType.Gold;
		if (reward_info != null)
		{
			if (reward_info.open_reward01)
			{
				flag = true;
				reward = reward_info.reward_value01;
				unit = reward_info.reward_unit01;
			}
			if (reward_info.open_reward02)
			{
				flag2 = true;
				reward2 = reward_info.reward_value02;
				unit2 = reward_info.reward_unit02;
			}
		}
		if (flag && !flag2)
		{
			DoCreateEx(true, text, introduce, progress, reward, unit, progress_text);
		}
		else if (flag && flag2)
		{
			DoCreateEx(true, text, introduce, progress, reward, unit, reward2, unit2, progress_text);
		}
		else
		{
			Debug.Log("no reward?!");
		}
	}

	public TUIAchievementRewardInfo TakeReward()
	{
		if (daily_missions_info == null)
		{
			Debug.Log("no info!");
			return null;
		}
		TUIAchievementRewardInfo reward_info = daily_missions_info.reward_info;
		if (reward_info != null)
		{
			return reward_info;
		}
		return null;
	}

	public int GetID()
	{
		if (daily_missions_info != null)
		{
			return daily_missions_info.id;
		}
		return 0;
	}
}
