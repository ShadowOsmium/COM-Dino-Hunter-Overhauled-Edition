using System.Collections.Generic;

public class TUIAchievementInfo
{
	public List<TUIOneAchievementInfo> achievement_list;

	public TUIAchievementInfo()
	{
	}

	public TUIAchievementInfo(List<TUIOneAchievementInfo> m_achievement_list)
	{
		achievement_list = m_achievement_list;
	}

	public void AddAchievementInfo(TUIOneAchievementInfo m_info)
	{
		if (achievement_list == null)
		{
			achievement_list = new List<TUIOneAchievementInfo>();
		}
		achievement_list.Add(m_info);
	}
}
