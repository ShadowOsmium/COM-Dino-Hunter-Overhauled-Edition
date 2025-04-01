using UnityEngine;

public class CUseSkillOnce : CUseSkill
{
	protected float m_fTimeAnim;

	protected float m_fTimeAnimCount;

	protected float m_fTimePoint;

	protected float m_fTimePointCount;

	public override kUseSkillStatus OnEnter(CCharBase charbase)
	{
		kAnimEnum type = (kAnimEnum)m_pSkillInfoLevel.nAnim;
		if (charbase.GetActionLen(type) == 0f)
		{
			type = ((!charbase.IsMob() && !charbase.IsBoss()) ? kAnimEnum.Attack : kAnimEnum.Mob_Attack);
		}
		if (charbase.IsPlayer() || charbase.IsUser())
		{
			m_fTimeAnim = charbase.CrossAnimMix(type, WrapMode.Once, 0.3f, m_pSkillInfoLevel.fAnimSpeed);
		}
		else
		{
			m_fTimeAnim = charbase.CrossAnim(type, WrapMode.ClampForever, 0.3f, m_pSkillInfoLevel.fAnimSpeed, 0f);
		}
		float fValue = 0f;
		m_pSkillInfoLevel.GetSkillModeValue(0, ref fValue);
		m_fTimePoint = m_fTimeAnim * fValue;
		m_fTimeAnimCount = 0f;
		m_fTimePointCount = 0f;
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
			Debug.Log("invalid skill");
			return kUseSkillStatus.Failure;
		}
		if (m_fTimePointCount < m_fTimePoint)
		{
			m_fTimePointCount += deltaTime;
			if (m_fTimePointCount >= m_fTimePoint)
			{
				m_fTimePointCount = m_fTimePoint;
				if (m_pSkillInfoLevel.sUseAudio.Length > 0)
				{
					charbase.PlayAudio(m_pSkillInfoLevel.sUseAudio);
				}
				SkillEffect(charbase, m_Target);
			}
		}
		m_fTimeAnimCount += deltaTime;
		if (m_fTimeAnimCount >= m_fTimeAnim)
		{
			if (m_pSkillInfoLevel != null && m_pSkillInfoLevel.nAnimBuffID > 0)
			{
				charbase.DelBuffByID(m_pSkillInfoLevel.nAnimBuffID);
			}
			m_pSkillInfoLevel = null;
			return kUseSkillStatus.Success;
		}
		return kUseSkillStatus.Executing;
	}
}
