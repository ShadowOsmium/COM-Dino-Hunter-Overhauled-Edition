using System.Collections.Generic;

public class CCharacterInfoLevel
{
	public bool isMale;

	public string sName;

	public string sDesc;

	public string sIcon;

	public int nModel;

	public float fLifeBase;

	public int nSkill;

	public float fSkillCD;

	public List<int> ltSkillPassive;

	public int nExp;

	public int nAvatarHead;

	public int nAvatarUpper;

	public int nAvatarLower;

	public CCharacterInfoLevel()
	{
		ltSkillPassive = new List<int>();
	}
}
