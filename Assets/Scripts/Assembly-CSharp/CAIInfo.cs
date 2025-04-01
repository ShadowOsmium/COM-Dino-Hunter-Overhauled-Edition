using System.Collections.Generic;

public class CAIInfo
{
	public int nID;

	public List<SkillComboRateInfo> ltSkill;

	public List<int> ltSkillPassive;

	public int nBehavior;

	public List<int> ltEffect;

	public List<int> ltEffectBone;

	public CAIInfo()
	{
		ltSkill = new List<SkillComboRateInfo>();
		ltSkillPassive = new List<int>();
		ltEffect = new List<int>();
		ltEffectBone = new List<int>();
	}

	public bool GetBone(int nIndex, ref int nBone)
	{
		if (nIndex < 0 || nIndex >= ltEffectBone.Count)
		{
			return false;
		}
		nBone = ltEffectBone[nIndex];
		return true;
	}
}
