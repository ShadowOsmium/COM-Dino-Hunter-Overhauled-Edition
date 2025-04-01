using System.Collections.Generic;
using UnityEngine;

public class CHunterLevelInfo
{
	public int m_nID;

	public int m_nGroupID;

	public int m_nHunterExpWin;

	public int m_nHunterExpLose;

	public List<int> m_ltGameLevel;

	public int m_nMonsterLvlMin;

	public int m_nMonsterLvlMax;

	public int m_nGold;

	public int m_nExp;

	public CHunterLevelInfo()
	{
		m_ltGameLevel = new List<int>();
	}

	public int GetGameLevel()
	{
		if (m_ltGameLevel.Count < 1)
		{
			return -1;
		}
		return m_ltGameLevel[Random.Range(0, m_ltGameLevel.Count)];
	}
}
