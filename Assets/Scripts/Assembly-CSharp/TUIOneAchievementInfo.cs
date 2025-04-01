using System.Collections.Generic;

public class TUIOneAchievementInfo
{
	public int id;

	public Dictionary<int, string> name_list;

	public Dictionary<int, string> introduce_list;

	public Dictionary<int, TUIAchievementRewardInfo> reward_list;

	public Dictionary<int, int> progress_list;

	public Dictionary<int, bool> take_reward_list;

	public int progress;

	public Dictionary<int, string> progress_text_list;

	public TUIOneAchievementInfo()
	{
	}

	public TUIOneAchievementInfo(int m_id, Dictionary<int, string> m_name_list, Dictionary<int, string> m_introduce_list, Dictionary<int, TUIAchievementRewardInfo> m_reward_list, Dictionary<int, int> m_progress_list, Dictionary<int, bool> m_take_reward_list, Dictionary<int, string> m_progress_text_list)
	{
		id = m_id;
		name_list = m_name_list;
		introduce_list = m_introduce_list;
		reward_list = m_reward_list;
		progress_list = m_progress_list;
		take_reward_list = m_take_reward_list;
		progress_text_list = m_progress_text_list;
	}
}
