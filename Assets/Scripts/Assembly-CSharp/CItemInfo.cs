using System.Collections.Generic;

public class CItemInfo
{
	public int nID;

	public Dictionary<int, CItemInfoLevel> m_dictItemInfoLevel;

	public int nUnLockLevel;

	public CItemInfo()
	{
		m_dictItemInfoLevel = new Dictionary<int, CItemInfoLevel>();
	}

	public void Add(int nLevel, CItemInfoLevel iteminfolevel)
	{
		if (!m_dictItemInfoLevel.ContainsKey(nLevel))
		{
			m_dictItemInfoLevel.Add(nLevel, iteminfolevel);
		}
	}

	public CItemInfoLevel Get(int nLevel)
	{
		if (!m_dictItemInfoLevel.ContainsKey(nLevel))
		{
			return null;
		}
		return m_dictItemInfoLevel[nLevel];
	}

	public int GetLvlCount()
	{
		return m_dictItemInfoLevel.Count;
	}
}
