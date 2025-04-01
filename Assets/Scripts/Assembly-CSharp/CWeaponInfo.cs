using System.Collections.Generic;

public class CWeaponInfo
{
	public int nID;

	public Dictionary<int, CWeaponInfoLevel> m_dictWeaponLvlInfo;

	public int m_nUnlockStageID;

	public int m_nUnlockHunterLvl;

	public CWeaponInfo()
	{
		m_dictWeaponLvlInfo = new Dictionary<int, CWeaponInfoLevel>();
	}

	public void Add(int nLevel, CWeaponInfoLevel weaponinfolevel)
	{
		if (!m_dictWeaponLvlInfo.ContainsKey(nLevel))
		{
			m_dictWeaponLvlInfo.Add(nLevel, weaponinfolevel);
		}
	}

	public CWeaponInfoLevel Get(int nLevel)
	{
		if (!m_dictWeaponLvlInfo.ContainsKey(nLevel))
		{
			return null;
		}
		return m_dictWeaponLvlInfo[nLevel];
	}

	public int GetLvlCount()
	{
		return m_dictWeaponLvlInfo.Count;
	}

	public bool IsGift()
	{
		if (nID == 27)
		{
			return true;
		}
		return false;
	}
}
