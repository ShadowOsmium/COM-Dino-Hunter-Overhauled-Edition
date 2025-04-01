using System.Collections.Generic;

public class CSkillComboInfo
{
	public int nID;

	public bool isIgnoreComboLimit;

	public float fCoolDown;

	public List<int> ltSkill;

	public float fFreezeTime;

	public int nUseCount;

	public CSkillComboInfo()
	{
		ltSkill = new List<int>();
		isIgnoreComboLimit = true;
	}

	public int GetSkill(int nIndex)
	{
		if (nIndex < 0 || nIndex >= ltSkill.Count)
		{
			return -1;
		}
		return ltSkill[nIndex];
	}
}
