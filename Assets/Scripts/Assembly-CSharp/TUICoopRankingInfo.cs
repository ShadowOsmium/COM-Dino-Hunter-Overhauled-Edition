using System.Collections.Generic;
using UnityEngine;

public class TUICoopRankingInfo
{
	public Dictionary<string, TUICoopPlayerInfo> ranking_list;

	public TUICoopPlayerInfo my_ranking_list;

	public void AddRanking(TUICoopPlayerInfo m_info)
	{
		if (ranking_list == null)
		{
			ranking_list = new Dictionary<string, TUICoopPlayerInfo>();
		}
		if (m_info == null)
		{
			Debug.Log("error!");
		}
		else
		{
			ranking_list[m_info.id] = m_info;
		}
	}

	public void SetMyself(TUICoopPlayerInfo m_info)
	{
		my_ranking_list = m_info;
	}
}
