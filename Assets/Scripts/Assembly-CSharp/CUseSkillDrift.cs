using UnityEngine;

public class CUseSkillDrift : CUseSkill
{
	protected float m_fTimeHold;

	protected float m_fTimeHoldCount;

	protected Vector3 m_v3DstPoint;

	protected Vector3 m_v3SrcPoint;

	public override kUseSkillStatus OnEnter(CCharBase charbase)
	{
		float fValue = 0f;
		float fValue2 = 0f;
		m_pSkillInfoLevel.GetSkillModeValue(2, ref fValue);
		m_pSkillInfoLevel.GetSkillModeValue(3, ref fValue2);
		m_v3DstPoint = charbase.Pos + charbase.Dir2D * fValue;
		m_v3DstPoint += charbase.Transform.right * fValue2;
		Vector3 vector = m_v3DstPoint - charbase.Pos;
		float magnitude = vector.magnitude;
		vector /= magnitude;
		RaycastHit hitInfo;
		if (Physics.Raycast(charbase.Pos, vector, out hitInfo, magnitude + 2f, -1874853888))
		{
			m_v3DstPoint = hitInfo.point - vector * 2f;
		}
		m_v3SrcPoint = charbase.Pos;
		m_pSkillInfoLevel.GetSkillModeValue(0, ref m_fTimeHold);
		m_fTimeHold *= Mathf.Clamp01(magnitude / 5f);
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
		if (m_pSkillInfoLevel != null && m_pSkillInfoLevel.nAnimBuffID > 0)
		{
			charbase.AddBuff(m_pSkillInfoLevel.nAnimBuffID, 0f, 0);
		}
		return kUseSkillStatus.Success;
	}

	public override kUseSkillStatus OnUpdate(CCharBase charbase, float deltaTime)
	{
		m_fTimeHoldCount += deltaTime;
		charbase.Pos = Vector3.Lerp(m_v3SrcPoint, m_v3DstPoint, m_fTimeHoldCount / m_fTimeHold);
		if (m_fTimeHoldCount >= m_fTimeHold)
		{
			SkillEffect(charbase, m_Target);
			if (m_pSkillInfoLevel != null && m_pSkillInfoLevel.nAnimBuffID > 0)
			{
				charbase.DelBuffByID(m_pSkillInfoLevel.nAnimBuffID);
			}
			return kUseSkillStatus.Success;
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
