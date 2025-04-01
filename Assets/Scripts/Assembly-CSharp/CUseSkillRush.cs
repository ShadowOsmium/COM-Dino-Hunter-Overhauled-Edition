using UnityEngine;

public class CUseSkillRush : CUseSkill
{
	protected Vector3 m_v3Dst;

	protected float m_fExpandDis;

	protected int m_nEffectID = -1;

	protected float m_fSpeed;

	protected float m_fTimePoint;

	protected float m_fTimePointCount;

	protected iRushEffect m_RushEffect;

	public override kUseSkillStatus OnEnter(CCharBase charbase)
	{
		m_pSkillInfoLevel.GetSkillModeValue(0, ref m_fTimePoint);
		m_pSkillInfoLevel.GetSkillModeValue(1, ref m_fExpandDis);
		m_pSkillInfoLevel.GetSkillModeValue(2, ref m_nEffectID);
		m_v3Dst = charbase.m_Target.Pos;
		Vector3 vector = m_v3Dst - charbase.Pos;
		float magnitude = vector.magnitude;
		Vector3 vector2 = vector / magnitude;
		if (magnitude + m_fExpandDis <= 0f)
		{
			return kUseSkillStatus.Failure;
		}
		int num = -1;
		CCharMob cCharMob = charbase as CCharMob;
		if (Physics.Raycast(layerMask: ((!(cCharMob != null) || cCharMob.MobType != 1) && cCharMob.MobType != 10) ? (-1874853888) : (-1879048192), origin: charbase.Pos, direction: vector2, maxDistance: magnitude + m_fExpandDis))
		{
			return kUseSkillStatus.Failure;
		}
		if (m_fTimePoint == 0f)
		{
			m_fSpeed = 1000f;
		}
		else
		{
			m_fSpeed = magnitude / m_fTimePoint;
		}
		m_v3Dst += vector2 * m_fExpandDis;
		m_fTimePointCount = 0f;
		bool flag = false;
		if (cCharMob != null)
		{
			if (cCharMob.MobType == 1)
			{
				if (cCharMob.IsBoss())
				{
					cCharMob.PlayAudio("Ani_Dive_Ptero_Boss");
				}
				else
				{
					cCharMob.PlayAudio("Ani_Dive_Ptero");
				}
			}
			CMobInfoLevel mobInfo = cCharMob.GetMobInfo();
			if (mobInfo != null)
			{
				if (m_pSkillInfoLevel.nAnim == 4)
				{
					charbase.CrossAnim((kAnimEnum)m_pSkillInfoLevel.nAnim, WrapMode.Loop, 0.3f, m_fSpeed * mobInfo.fMoveSpeedRate, 0f);
					flag = true;
				}
				else if (m_pSkillInfoLevel.nAnim == 2504)
				{
					charbase.CrossAnim((kAnimEnum)m_pSkillInfoLevel.nAnim, WrapMode.Loop, 0.3f, m_fSpeed * mobInfo.fRushSpeedRate, 0f);
					flag = true;
				}
			}
		}
		if (!flag)
		{
			if (m_pSkillInfoLevel.nAnimLoop == 1)
			{
				charbase.PlayAnim((kAnimEnum)m_pSkillInfoLevel.nAnim, WrapMode.Loop, 1f, 0f);
			}
			else
			{
				float actionLen = charbase.GetActionLen((kAnimEnum)m_pSkillInfoLevel.nAnim);
				if (actionLen > 0f)
				{
					float num2 = Vector3.Distance(m_v3Dst, charbase.Pos) / m_fSpeed;
					charbase.PlayAnim((kAnimEnum)m_pSkillInfoLevel.nAnim, WrapMode.ClampForever, actionLen / num2, 0f);
				}
			}
		}
		charbase.Dir3D = vector2;
		if (m_pSkillInfoLevel != null && m_pSkillInfoLevel.nAnimBuffID > 0)
		{
			charbase.AddBuff(m_pSkillInfoLevel.nAnimBuffID, 0f);
		}
		if (charbase.Entity != null)
		{
			m_RushEffect = charbase.Entity.GetComponent<iRushEffect>();
			if (m_RushEffect != null)
			{
				m_RushEffect.iRushEffect_PlayEffect(m_nEffectID);
			}
		}
		if (m_pSkillInfoLevel.sUseAudio.Length > 0)
		{
			charbase.PlayAudio(m_pSkillInfoLevel.sUseAudio);
		}
		return kUseSkillStatus.Success;
	}

	public override void OnExit(CCharBase charbase)
	{
		if (m_pSkillInfoLevel != null && m_pSkillInfoLevel.nAnimBuffID > 0)
		{
			charbase.DelBuffByID(m_pSkillInfoLevel.nAnimBuffID);
		}
		if (m_RushEffect != null)
		{
			m_RushEffect.iRushEffect_StopEffect(true);
		}
	}

	public override kUseSkillStatus OnUpdate(CCharBase charbase, float deltaTime)
	{
		m_fTimePointCount += deltaTime;
		if (m_fTimePointCount >= m_fTimePoint)
		{
			m_fTimePointCount = 0f;
			SkillEffect(charbase, m_Target);
		}
		float num = m_fSpeed * deltaTime;
		Vector3 vector = m_v3Dst - charbase.Pos;
		if (num * num < vector.sqrMagnitude)
		{
			charbase.Pos += vector.normalized * num;
			return kUseSkillStatus.Executing;
		}
		charbase.Pos = m_v3Dst;
		if (m_pSkillInfoLevel != null && m_pSkillInfoLevel.nAnimBuffID > 0)
		{
			charbase.DelBuffByID(m_pSkillInfoLevel.nAnimBuffID);
		}
		if (m_RushEffect != null)
		{
			m_RushEffect.iRushEffect_StopEffect();
		}
		return kUseSkillStatus.Success;
	}
}
