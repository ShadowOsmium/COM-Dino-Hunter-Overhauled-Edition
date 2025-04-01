using System.Collections.Generic;

public class CProMob : CProBase
{
	public CProMob()
	{
		RegisterPro(kProEnum.FreezeTimeMinuseRate);
		RegisterPro(kProEnum.Mob_Gold_Carry);
		RegisterPro(kProEnum.Mob_Crystal_Carry);
		RegisterPro(kProEnum.Skill_MoribundTime);
		RegisterPro(kProEnum.Skill_MoribundDefence);
		RegisterPro(kProEnum.Skill_DeathBoomDmg);
		RegisterPro(kProEnum.Skill_DeathBoomRng);
		RegisterPro(kProEnum.Skill_DeathSplitID);
		RegisterPro(kProEnum.Skill_DeathSplitCount);
	}

	public override void Initialize(int nID, int nLevel, bool bnetwork = false)
	{
		CMobInfoLevel mobInfo = m_GameData.GetMobInfo(nID, nLevel);
		if (mobInfo != null)
		{
			if (bnetwork)
			{
				SetValueBase(kProEnum.HPMax, MyUtils.formula_monsterlife(mobInfo.fLife, nLevel));
				SetValueBase(kProEnum.Damage, MyUtils.formula_monsterdamage(mobInfo.fDamage, nLevel));
			}
			else
			{
				SetValueBase(kProEnum.HPMax, mobInfo.fLife);
				SetValueBase(kProEnum.Damage, mobInfo.fDamage);
			}
			SetValueBase(kProEnum.MoveSpeed, mobInfo.fMoveSpeed);
			SetValueBase(kProEnum.ResistBeatBack, 0f);
			SetValueBase(kProEnum.FreezeTimeMinuseRate, 0f);
		}
	}

	public override void UpdateSkill(CCharBase charbase)
	{
		if (!charbase.IsMob() && !charbase.IsBoss())
		{
			return;
		}
		CCharMob cCharMob = charbase as CCharMob;
		if (cCharMob == null)
		{
			return;
		}
		foreach (CProValue value in m_dictPro.Values)
		{
			value.m_fValueAffectFromSkill = 0f;
			value.UpdateValue();
		}
		List<int> ltSkillPassive = new List<int>();
		if (!cCharMob.GetSkillPassiveList(ref ltSkillPassive) || ltSkillPassive == null)
		{
			return;
		}
		foreach (int item in ltSkillPassive)
		{
			CSkillInfoLevel skillInfo = m_GameData.GetSkillInfo(item, 1);
			if (skillInfo == null || skillInfo.nType != 1)
			{
				continue;
			}
			for (int i = 0; i < 3; i++)
			{
				int num = skillInfo.arrFunc[i];
				int nValue = skillInfo.arrValueX[i];
				int nValue2 = skillInfo.arrValueY[i];
				int num2 = num;
				if (num2 == 1)
				{
					ProFuncSkill((kProEnum)MyUtils.Low32(nValue), MyUtils.Low32(nValue2), MyUtils.High32(nValue), MyUtils.High32(nValue2));
				}
			}
		}
	}

	public override void UpdateBuff(CCharBase charbase)
	{
		foreach (CProValue value in m_dictPro.Values)
		{
			value.m_fValueAffectFromBuff = 0f;
			value.UpdateValue();
		}
		for (int i = 0; i < 10; i++)
		{
			iBuffData buffBySlot = charbase.GetBuffBySlot(i);
			if (buffBySlot == null || buffBySlot.m_nID == 0)
			{
				continue;
			}
			CBuffInfo buffInfo = m_GameData.GetBuffInfo(buffBySlot.m_nID);
			if (buffInfo == null)
			{
				continue;
			}
			for (int j = 0; j < 3; j++)
			{
				int num = buffInfo.arrFunc[j];
				int nValue = buffInfo.arrValueX[j];
				int nValue2 = buffInfo.arrValueY[j];
				int num2 = num;
				if (num2 == 1)
				{
					ProFuncBuff((kProEnum)MyUtils.Low32(nValue), MyUtils.Low32(nValue2), MyUtils.High32(nValue), MyUtils.High32(nValue2));
				}
			}
		}
	}

	public override void UpdateEquip(CCharBase charbase)
	{
	}
}
