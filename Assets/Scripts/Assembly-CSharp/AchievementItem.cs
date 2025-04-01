using System.Collections.Generic;
using UnityEngine;

public class AchievementItem : MonoBehaviour
{
	public AchievementStars achievement_stars;

	public TUILabel label_name;

	public TUILabel label_introduce;

	public AchievementBar achievement_bar;

	public AchievementRewardText achievement_reward_text_right;

	public TUIButtonClick btn_achievement;

	public GameObject effect_stars_prefab;

	private TUIOneAchievementInfo chievement_info;

	private AchievementLevelType star_level;

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

	public void DoCreate(TUIOneAchievementInfo m_chievement_info, GameObject m_go_invoke = null)
	{
		chievement_info = m_chievement_info;
		if (chievement_info == null)
		{
			Debug.Log("no info!");
			return;
		}
		if (btn_achievement != null && m_go_invoke != null)
		{
			btn_achievement.invokeObject = m_go_invoke;
		}
		Dictionary<int, string> name_list = chievement_info.name_list;
		Dictionary<int, string> introduce_list = chievement_info.introduce_list;
		Dictionary<int, TUIAchievementRewardInfo> reward_list = chievement_info.reward_list;
		Dictionary<int, int> progress_list = chievement_info.progress_list;
		Dictionary<int, bool> take_reward_list = chievement_info.take_reward_list;
		Dictionary<int, string> progress_text_list = chievement_info.progress_text_list;
		if (take_reward_list.ContainsKey(1) && !take_reward_list[1])
		{
			star_level = AchievementLevelType.Level0;
		}
		else if (take_reward_list.ContainsKey(2) && !take_reward_list[2])
		{
			star_level = AchievementLevelType.Level1;
		}
		else if (take_reward_list.ContainsKey(3) && !take_reward_list[3])
		{
			star_level = AchievementLevelType.Level2;
		}
		else if (take_reward_list.ContainsKey(3) && take_reward_list[3])
		{
			star_level = AchievementLevelType.Level3;
		}
		int num = 0;
		num = ((star_level != AchievementLevelType.Level3) ? progress_list[(int)(star_level + 1)] : progress_list[(int)star_level]);
		string text = string.Empty;
		if (name_list != null)
		{
			if (star_level == AchievementLevelType.Level3)
			{
				if (name_list.ContainsKey((int)star_level))
				{
					text = name_list[(int)star_level];
				}
			}
			else if (name_list.ContainsKey((int)(star_level + 1)))
			{
				text = name_list[(int)(star_level + 1)];
			}
		}
		string introduce = string.Empty;
		if (introduce_list != null)
		{
			if (star_level == AchievementLevelType.Level3)
			{
				if (introduce_list.ContainsKey((int)star_level))
				{
					introduce = introduce_list[(int)star_level];
				}
			}
			else if (introduce_list.ContainsKey((int)(star_level + 1)))
			{
				introduce = introduce_list[(int)(star_level + 1)];
			}
		}
		TUIAchievementRewardInfo tUIAchievementRewardInfo = null;
		if (reward_list != null)
		{
			if (star_level == AchievementLevelType.Level3)
			{
				if (reward_list.ContainsKey((int)star_level))
				{
					tUIAchievementRewardInfo = reward_list[(int)star_level];
				}
			}
			else if (reward_list.ContainsKey((int)(star_level + 1)))
			{
				tUIAchievementRewardInfo = reward_list[(int)(star_level + 1)];
			}
		}
		string progress_text = string.Empty;
		if (progress_text_list != null)
		{
			if (star_level == AchievementLevelType.Level3)
			{
				if (progress_text_list.ContainsKey((int)star_level))
				{
					progress_text = progress_text_list[(int)star_level];
				}
			}
			else if (progress_text_list.ContainsKey((int)(star_level + 1)))
			{
				progress_text = progress_text_list[(int)(star_level + 1)];
			}
		}
		bool flag = false;
		bool flag2 = false;
		int reward = 0;
		int reward2 = 0;
		UnitType unit = UnitType.Gold;
		UnitType unit2 = UnitType.Gold;
		if (tUIAchievementRewardInfo != null)
		{
			if (tUIAchievementRewardInfo.open_reward01)
			{
				flag = true;
				reward = tUIAchievementRewardInfo.reward_value01;
				unit = tUIAchievementRewardInfo.reward_unit01;
			}
			if (tUIAchievementRewardInfo.open_reward02)
			{
				flag2 = true;
				reward2 = tUIAchievementRewardInfo.reward_value02;
				unit2 = tUIAchievementRewardInfo.reward_unit02;
			}
		}
		if (flag && !flag2)
		{
			DoCreateEx(star_level, text, introduce, num, reward, unit, progress_text);
		}
		else if (flag && flag2)
		{
			DoCreateEx(star_level, text, introduce, num, reward, unit, reward2, unit2, progress_text);
		}
		else
		{
			Debug.Log("no reward?!Next Level:" + (int)(star_level + 1));
		}
	}

	public void DoCreateEx(AchievementLevelType m_level, string m_name, string m_introduce, int m_progress, int m_reward01, UnitType m_unit01, string m_progress_text)
	{
		if (achievement_stars != null)
		{
			achievement_stars.SetInfo(m_level);
		}
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
		Btn_Achievement component = btn_achievement.GetComponent<Btn_Achievement>();
		if (m_progress == 100)
		{
			if (component != null)
			{
				component.SetStateText(Btn_Achievement.BtnAchievementState.State_Normal);
			}
			btn_achievement.Disable(false);
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
		if (component != null && m_level == AchievementLevelType.Level3)
		{
			component.SetStateText(Btn_Achievement.BtnAchievementState.State_Finish);
			btn_achievement.Disable(true);
		}
	}

	public void DoCreateEx(AchievementLevelType m_level, string m_name, string m_introduce, int m_progress, int m_reward01, UnitType m_unit01, int m_reward02, UnitType m_unit02, string m_progress_text)
	{
		if (achievement_stars != null)
		{
			achievement_stars.SetInfo(m_level);
		}
		if (label_name != null)
		{
			label_name.Text = m_name;
		}
		if (label_introduce != null)
		{
			label_introduce.Text = m_introduce;
		}
		Btn_Achievement component = btn_achievement.GetComponent<Btn_Achievement>();
		if (m_progress == 100)
		{
			if (component != null)
			{
				component.SetStateText(Btn_Achievement.BtnAchievementState.State_Normal);
			}
			btn_achievement.Disable(false);
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
		if (achievement_bar != null)
		{
			achievement_bar.Show(m_progress, m_progress_text);
		}
		if (component != null && m_level == AchievementLevelType.Level3)
		{
			component.SetStateText(Btn_Achievement.BtnAchievementState.State_Finish);
			btn_achievement.Disable(true);
		}
	}

	public void AfterTakeAchievement()
	{
		if (chievement_info == null)
		{
			Debug.Log("no info!");
			return;
		}
		Dictionary<int, string> name_list = chievement_info.name_list;
		Dictionary<int, string> introduce_list = chievement_info.introduce_list;
		Dictionary<int, TUIAchievementRewardInfo> reward_list = chievement_info.reward_list;
		Dictionary<int, int> progress_list = chievement_info.progress_list;
		Dictionary<int, string> progress_text_list = chievement_info.progress_text_list;
		if (star_level + 1 > AchievementLevelType.Level3)
		{
			Debug.Log("error!");
			return;
		}
		star_level++;
		chievement_info.take_reward_list[(int)(star_level + 1)] = true;
		int progress = 0;
		if (chievement_info != null)
		{
			progress = ((star_level != AchievementLevelType.Level3) ? progress_list[(int)(star_level + 1)] : progress_list[(int)star_level]);
		}
		string text = string.Empty;
		if (name_list != null)
		{
			if (star_level == AchievementLevelType.Level3)
			{
				if (name_list.ContainsKey((int)star_level))
				{
					text = name_list[(int)star_level];
				}
			}
			else if (name_list.ContainsKey((int)(star_level + 1)))
			{
				text = name_list[(int)(star_level + 1)];
			}
		}
		string introduce = string.Empty;
		if (introduce_list != null)
		{
			if (star_level == AchievementLevelType.Level3)
			{
				if (introduce_list.ContainsKey((int)star_level))
				{
					introduce = introduce_list[(int)star_level];
				}
			}
			else if (introduce_list.ContainsKey((int)(star_level + 1)))
			{
				introduce = introduce_list[(int)(star_level + 1)];
			}
		}
		TUIAchievementRewardInfo tUIAchievementRewardInfo = null;
		if (reward_list != null)
		{
			if (star_level == AchievementLevelType.Level3)
			{
				if (reward_list.ContainsKey((int)star_level))
				{
					tUIAchievementRewardInfo = reward_list[(int)star_level];
				}
			}
			else if (reward_list.ContainsKey((int)(star_level + 1)))
			{
				tUIAchievementRewardInfo = reward_list[(int)(star_level + 1)];
			}
		}
		string progress_text = string.Empty;
		if (progress_text_list != null)
		{
			if (star_level == AchievementLevelType.Level3)
			{
				if (progress_text_list.ContainsKey((int)star_level))
				{
					progress_text = progress_text_list[(int)star_level];
				}
			}
			else if (progress_text_list.ContainsKey((int)(star_level + 1)))
			{
				progress_text = progress_text_list[(int)(star_level + 1)];
			}
		}
		bool flag = false;
		bool flag2 = false;
		int reward = 0;
		int reward2 = 0;
		UnitType unit = UnitType.Gold;
		UnitType unit2 = UnitType.Gold;
		if (tUIAchievementRewardInfo != null)
		{
			if (tUIAchievementRewardInfo.open_reward01)
			{
				flag = true;
				reward = tUIAchievementRewardInfo.reward_value01;
				unit = tUIAchievementRewardInfo.reward_unit01;
			}
			if (tUIAchievementRewardInfo.open_reward02)
			{
				flag2 = true;
				reward2 = tUIAchievementRewardInfo.reward_value02;
				unit2 = tUIAchievementRewardInfo.reward_unit02;
			}
		}
		if (flag && !flag2)
		{
			DoCreateEx(star_level, text, introduce, progress, reward, unit, progress_text);
		}
		else if (flag && flag2)
		{
			DoCreateEx(star_level, text, introduce, progress, reward, unit, reward2, unit2, progress_text);
		}
		else
		{
			Debug.Log("no reward?!Next Level:" + (int)(star_level + 1));
		}
		if (effect_stars_prefab != null)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(effect_stars_prefab);
			gameObject.transform.parent = achievement_stars.transform;
			gameObject.transform.localPosition = achievement_stars.GetStarPos(star_level) + new Vector3(0f, 0f, 0.5f);
			Object.Destroy(gameObject, 1f);
		}
	}

	public TUIAchievementRewardInfo TakeAchievement()
	{
		if (chievement_info == null)
		{
			Debug.Log("no info!");
			return null;
		}
		Dictionary<int, TUIAchievementRewardInfo> reward_list = chievement_info.reward_list;
		if (reward_list.ContainsKey((int)GetAchievementLevel()))
		{
			return reward_list[(int)GetAchievementLevel()];
		}
		return null;
	}

	public int GetID()
	{
		if (chievement_info != null)
		{
			return chievement_info.id;
		}
		return 0;
	}

	public AchievementLevelType GetStarLevel()
	{
		return star_level;
	}

	public AchievementLevelType GetAchievementLevel()
	{
		if (star_level + 1 <= AchievementLevelType.Level3)
		{
			return star_level + 1;
		}
		return AchievementLevelType.Level3;
	}
}
