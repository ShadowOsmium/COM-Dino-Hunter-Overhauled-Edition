using System;

[Serializable]
public class SkillComboRateInfo
{
	public int m_nID;

	public float m_fRate;

	public SkillComboRateInfo(int nID, float fRate)
	{
		m_nID = nID;
		m_fRate = fRate;
	}
}
