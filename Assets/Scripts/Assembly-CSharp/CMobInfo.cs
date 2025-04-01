using System.Collections.Generic;

public class CMobInfo
{
	public int nID;

	public Dictionary<int, CMobInfoLevel> m_dictMobInfoLevel;

	public CMobInfo()
	{
		m_dictMobInfoLevel = new Dictionary<int, CMobInfoLevel>();
	}

	public void Add(int nLevel, CMobInfoLevel mobinfolevel)
	{
		if (!m_dictMobInfoLevel.ContainsKey(nLevel))
		{
			m_dictMobInfoLevel.Add(nLevel, mobinfolevel);
		}
	}

	public CMobInfoLevel Get(int nLevel)
	{
		if (!m_dictMobInfoLevel.ContainsKey(nLevel))
		{
			return null;
		}
		return m_dictMobInfoLevel[nLevel];
	}
}
