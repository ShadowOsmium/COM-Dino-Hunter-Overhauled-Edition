using System.Collections.Generic;

public class CSkillPro
{
	public int nID;

	public float fRemainTime;

	public float fBuffUp;

	public float fDamageUp;

	public float fBeatBack;

	public float fCDDown;

	public List<int> ltBuffUpID;

	public float fHealUp;

	public CSkillPro()
	{
		ltBuffUpID = new List<int>();
	}
}
