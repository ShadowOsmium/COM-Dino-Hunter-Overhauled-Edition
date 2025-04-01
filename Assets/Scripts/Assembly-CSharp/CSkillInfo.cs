using System.Collections.Generic;

public class CSkillInfo
{
	public int nID;

	public Dictionary<int, CSkillInfoLevel> m_dictSkillInfoLevel;

	public int nUnlockLevel;

	public bool isCrystalUnlock;

	public int nUnlockPrice;

	public CSkillInfo()
	{
		m_dictSkillInfoLevel = new Dictionary<int, CSkillInfoLevel>();
	}

	public void Add(int nLevel, CSkillInfoLevel skillinfolevel)
	{
		if (!m_dictSkillInfoLevel.ContainsKey(nLevel))
		{
			m_dictSkillInfoLevel.Add(nLevel, skillinfolevel);
		}
	}

	public CSkillInfoLevel Get(int nLevel)
	{
		if (!m_dictSkillInfoLevel.ContainsKey(nLevel))
		{
			return null;
		}
		return m_dictSkillInfoLevel[nLevel];
	}
}
