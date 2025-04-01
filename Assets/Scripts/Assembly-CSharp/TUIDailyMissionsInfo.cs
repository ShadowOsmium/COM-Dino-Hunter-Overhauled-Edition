using System.Collections.Generic;

public class TUIDailyMissionsInfo
{
	public List<TUIOneDailyMissionsInfo> daily_missions_list;

	public TUIDailyMissionsInfo()
	{
	}

	public TUIDailyMissionsInfo(List<TUIOneDailyMissionsInfo> m_daily_missions_list)
	{
		daily_missions_list = m_daily_missions_list;
	}

	public void AddDailyMissionsInfo(TUIOneDailyMissionsInfo m_info)
	{
		if (daily_missions_list == null)
		{
			daily_missions_list = new List<TUIOneDailyMissionsInfo>();
		}
		daily_missions_list.Add(m_info);
	}
}
