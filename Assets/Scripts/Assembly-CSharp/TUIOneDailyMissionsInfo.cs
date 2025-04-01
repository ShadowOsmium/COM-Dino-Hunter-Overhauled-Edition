public class TUIOneDailyMissionsInfo
{
	public int id;

	public string name = string.Empty;

	public string introduce = string.Empty;

	public TUIAchievementRewardInfo reward_info;

	public bool take_reward;

	public int progress;

	public string progress_text = string.Empty;

	public TUIOneDailyMissionsInfo(int m_id, string m_name, string m_introduce, TUIAchievementRewardInfo m_reward_info, bool m_take_reward, int m_progress, string m_progress_text)
	{
		id = m_id;
		name = m_name;
		introduce = m_introduce;
		reward_info = m_reward_info;
		take_reward = m_take_reward;
		progress = m_progress;
		progress_text = m_progress_text;
	}
}
