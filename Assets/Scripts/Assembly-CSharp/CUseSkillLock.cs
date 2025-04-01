using UnityEngine;

public class CUseSkillLock : CUseSkill
{
	protected float m_fNeedLockTarget;

	protected float m_fTimeHold;

	protected float m_fTimeHoldCount;

	public override kUseSkillStatus OnEnter(CCharBase charbase)
	{
		m_pSkillInfoLevel.GetSkillModeValue(0, ref m_fTimeHold);
		m_fTimeHoldCount = 0f;
		if (m_pSkillInfoLevel.nAnimLoop == 1)
		{
			charbase.CrossAnim((kAnimEnum)m_pSkillInfoLevel.nAnim, WrapMode.Loop, 0.3f, 1f, 0f);
		}
		else
		{
			float actionLen = charbase.GetActionLen((kAnimEnum)m_pSkillInfoLevel.nAnim);
			if (actionLen > 0f)
			{
				charbase.CrossAnim((kAnimEnum)m_pSkillInfoLevel.nAnim, WrapMode.ClampForever, 0.3f, actionLen / m_fTimeHold, 0f);
			}
		}
		m_fNeedLockTarget = 1f;
		m_pSkillInfoLevel.GetSkillModeValue(2, ref m_fNeedLockTarget);
		if (m_pSkillInfoLevel != null && m_pSkillInfoLevel.nAnimBuffID > 0)
		{
			charbase.AddBuff(m_pSkillInfoLevel.nAnimBuffID, 0f);
		}
		return kUseSkillStatus.Success;
	}

	public override kUseSkillStatus OnUpdate(CCharBase charbase, float deltaTime)
	{
		m_fTimeHoldCount += deltaTime;
		if (m_fTimeHoldCount >= m_fTimeHold)
		{
			if (m_pSkillInfoLevel != null && m_pSkillInfoLevel.nAnimBuffID > 0)
			{
				charbase.DelBuffByID(m_pSkillInfoLevel.nAnimBuffID);
			}
			return kUseSkillStatus.Success;
		}
		if (m_fNeedLockTarget > 0f && charbase.m_Target != null)
		{
			charbase.Dir3D = Vector3.Lerp(charbase.Dir3D, (charbase.m_Target.Pos - charbase.Pos).normalized, m_fTimeHoldCount / m_fTimeHold);
		}
		return kUseSkillStatus.Executing;
	}

	public override void OnExit(CCharBase charbase)
	{
		if (m_pSkillInfoLevel != null && m_pSkillInfoLevel.nAnimBuffID > 0)
		{
			charbase.DelBuffByID(m_pSkillInfoLevel.nAnimBuffID);
		}
	}
}
