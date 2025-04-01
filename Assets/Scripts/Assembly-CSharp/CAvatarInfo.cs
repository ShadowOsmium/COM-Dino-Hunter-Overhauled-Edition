using System.Collections.Generic;

public class CAvatarInfo
{
	public int m_nID;

	public int m_nType;

	public string m_sIcon = string.Empty;

	public string m_sModel = string.Empty;

	public string m_sTexture = string.Empty;

	public string m_sEffect = string.Empty;

	public string m_sName = string.Empty;

	public bool m_bLinkChar;

	public int m_nUnlockStageID;

	public int m_nUnlockHunterLvl;

	public Dictionary<int, CAvatarInfoLevel> m_dictAvatarInfoLevel;

	public CAvatarInfo()
	{
		m_dictAvatarInfoLevel = new Dictionary<int, CAvatarInfoLevel>();
	}

	public CAvatarInfoLevel Get(int nLevel)
	{
		if (!m_dictAvatarInfoLevel.ContainsKey(nLevel))
		{
			return null;
		}
		return m_dictAvatarInfoLevel[nLevel];
	}

	public int GetCount()
	{
		if (m_dictAvatarInfoLevel == null)
		{
			return 0;
		}
		return m_dictAvatarInfoLevel.Count;
	}
}
