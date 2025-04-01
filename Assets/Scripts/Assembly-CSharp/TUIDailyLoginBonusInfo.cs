using System.Collections.Generic;

public class TUIDailyLoginBonusInfo
{
	public List<TUIPriceInfo> daily_bonus_list;

	public int day = 1;

	public void AddItem(TUIPriceInfo m_info)
	{
		if (daily_bonus_list == null)
		{
			daily_bonus_list = new List<TUIPriceInfo>();
		}
		daily_bonus_list.Add(m_info);
	}

	public void SetDay(int m_day)
	{
		day = m_day;
	}
}
