using System.Collections.Generic;

public class TUICoopFriendsInfo
{
	public Dictionary<string, TUICoopPlayerInfo> m_dictFriends;

	public TUICoopFriendsInfo()
	{
		m_dictFriends = new Dictionary<string, TUICoopPlayerInfo>();
	}

	public void Add(string sID, TUICoopPlayerInfo coopplayerinfo)
	{
		if (!m_dictFriends.ContainsKey(sID))
		{
			m_dictFriends.Add(sID, coopplayerinfo);
		}
	}
}
