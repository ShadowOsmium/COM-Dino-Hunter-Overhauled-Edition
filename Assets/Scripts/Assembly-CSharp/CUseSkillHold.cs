using UnityEngine;

public class CUseSkillHold : CUseSkill
{
	protected float m_fTime;

	protected float m_fTimeCount;

	protected float m_fInterval;

	protected float m_fIntervalCount;

	public override kUseSkillStatus OnEnter(CCharBase charbase)
	{
		charbase.CrossAnim((kAnimEnum)m_pSkillInfoLevel.nAnim, WrapMode.Loop, 0.3f, 1f, 0f);
		m_pSkillInfoLevel.GetSkillModeValue(0, ref m_fTime);
		m_pSkillInfoLevel.GetSkillModeValue(1, ref m_fInterval);
		m_fTimeCount = 0f;
		m_fIntervalCount = 0f;
		if (m_pSkillInfoLevel.nTargetLimit == 3)
		{
			m_Target = charbase;
		}
		if (m_pSkillInfoLevel != null && m_pSkillInfoLevel.nAnimBuffID > 0)
		{
			charbase.AddBuff(m_pSkillInfoLevel.nAnimBuffID, 0f);
		}
		return kUseSkillStatus.Success;
	}

	public override void OnExit(CCharBase charbase)
	{
		if (m_pSkillInfoLevel != null && m_pSkillInfoLevel.nAnimBuffID > 0)
		{
			charbase.DelBuffByID(m_pSkillInfoLevel.nAnimBuffID);
		}
	}

	public override kUseSkillStatus OnUpdate(CCharBase charbase, float deltaTime)
	{
		if (!IsSkillValid())
		{
			return kUseSkillStatus.Failure;
		}
		m_fTimeCount += deltaTime;
		if (m_fTimeCount >= m_fTime)
		{
			return kUseSkillStatus.Success;
		}
		m_fIntervalCount += deltaTime;
		if (m_fIntervalCount >= m_fInterval)
		{
			m_fIntervalCount = 0f;
			SkillEffect(charbase, m_Target);
			if (m_pSkillInfoLevel != null && m_pSkillInfoLevel.nAnimBuffID > 0)
			{
				charbase.DelBuffByID(m_pSkillInfoLevel.nAnimBuffID);
			}
		}
		return kUseSkillStatus.Executing;
	}
}
