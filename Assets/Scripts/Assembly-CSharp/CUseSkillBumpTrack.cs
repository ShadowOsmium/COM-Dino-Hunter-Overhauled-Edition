using System.Collections.Generic;
using UnityEngine;

public class CUseSkillBumpTrack : CUseSkill
{
	protected int m_nEffectID = -1;

	protected float m_fBumpTime = 1f;

	protected float m_fBumpTimeCount;

	protected float m_fBumpFuncTime;

	protected float m_fBumpFuncTimeCount;

	protected float m_fBumpSpeed;

	protected float m_fRotRate;

	protected float m_fRotSpeed;

	protected Vector3 m_v3RotSrc;

	protected Vector3 m_v3RotDst;

	protected float m_fCheckColliderTime = 0.2f;

	protected float m_fCheckColliderTimeCount;

	protected iRushEffect m_RushEffect;

	public override kUseSkillStatus OnEnter(CCharBase charbase)
	{
		charbase.m_bBumping = true;
		m_pSkillInfoLevel.GetSkillModeValue(0, ref m_fBumpTime);
		m_pSkillInfoLevel.GetSkillModeValue(1, ref m_fBumpSpeed);
		m_pSkillInfoLevel.GetSkillModeValue(2, ref m_fRotSpeed);
		m_pSkillInfoLevel.GetSkillModeValue(3, ref m_fBumpFuncTime);
		m_pSkillInfoLevel.GetSkillModeValue(4, ref m_nEffectID);
		m_fBumpFuncTimeCount = 0f;
		if (m_Target != null)
		{
			m_v3RotSrc = charbase.Dir2D;
			m_v3RotDst = (m_Target.Pos - charbase.Pos).normalized;
			m_v3RotDst.y = 0f;
			m_fRotRate = 0f;
		}
		float speed = 1f;
		CCharMob cCharMob = charbase as CCharMob;
		if (cCharMob != null)
		{
			CMobInfoLevel mobInfo = cCharMob.GetMobInfo();
			if (mobInfo != null)
			{
				if (m_pSkillInfoLevel.nAnim == 4)
				{
					speed = m_fBumpSpeed * mobInfo.fMoveSpeedRate;
				}
				else if (m_pSkillInfoLevel.nAnim == 2504)
				{
					speed = m_fBumpSpeed * mobInfo.fRushSpeedRate;
				}
			}
		}
		if (m_pSkillInfoLevel.nAnimLoop == 1)
		{
			charbase.PlayAnim((kAnimEnum)m_pSkillInfoLevel.nAnim, WrapMode.Loop, speed, 0f);
		}
		else
		{
			float actionLen = charbase.GetActionLen((kAnimEnum)m_pSkillInfoLevel.nAnim);
			if (actionLen > 0f)
			{
				charbase.PlayAnim((kAnimEnum)m_pSkillInfoLevel.nAnim, WrapMode.ClampForever, actionLen / m_fBumpTime, 0f);
			}
		}
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
		Debug.Log(charbase.UID + " start bump state");
		if (m_pSkillInfoLevel.sUseAudio.Length > 0)
		{
			charbase.PlayAudio(m_pSkillInfoLevel.sUseAudio);
		}
		return kUseSkillStatus.Success;
	}

	public override void OnExit(CCharBase charbase)
	{
		Debug.Log(charbase.UID + " out of the bump state");
		charbase.m_bBumping = false;
		if (m_pSkillInfoLevel != null && m_pSkillInfoLevel.nAnimBuffID > 0)
		{
			charbase.DelBuffByID(m_pSkillInfoLevel.nAnimBuffID);
		}
		if (m_RushEffect != null)
		{
			Debug.Log(charbase.UID + " stop effect");
			m_RushEffect.iRushEffect_StopEffect(true);
		}
	}

	public override kUseSkillStatus OnUpdate(CCharBase charbase, float deltaTime)
	{
		m_fCheckColliderTimeCount += deltaTime;
		if (m_fCheckColliderTimeCount >= m_fCheckColliderTime)
		{
			m_fCheckColliderTimeCount = 0f;
			if (Physics.Raycast(new Ray(charbase.Pos, charbase.Dir2D), 5f, -1874853888))
			{
				charbase.m_bBumping = false;
				if (m_pSkillInfoLevel != null && m_pSkillInfoLevel.nAnimBuffID > 0)
				{
					charbase.DelBuffByID(m_pSkillInfoLevel.nAnimBuffID);
				}
				if (m_RushEffect != null)
				{
					Debug.Log(charbase.UID + " stop effect");
					m_RushEffect.iRushEffect_StopEffect(true);
				}
				return kUseSkillStatus.Success;
			}
		}
		m_fBumpFuncTimeCount += deltaTime;
		if (m_fBumpFuncTimeCount >= m_fBumpFuncTime)
		{
			m_fBumpFuncTimeCount = 0f;
			SkillEffect(charbase, m_Target);
		}
		if (m_Target != null)
		{
			m_fRotRate += m_fRotSpeed * deltaTime;
			if (m_fRotRate > 1f)
			{
				m_v3RotSrc = charbase.Dir2D;
				m_v3RotDst = (m_Target.Pos - charbase.Pos).normalized;
				m_v3RotDst.y = 0f;
				m_fRotRate = 0f;
			}
			charbase.Dir2D = Vector3.Lerp(charbase.Dir2D, m_v3RotDst, m_fRotRate);
		}
		charbase.Pos += charbase.Dir2D * m_fBumpSpeed * deltaTime;
		m_fBumpTimeCount += deltaTime;
		if (m_fBumpTimeCount >= m_fBumpTime)
		{
			charbase.m_bBumping = false;
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
		return kUseSkillStatus.Executing;
	}

	protected override void SkillEffect(CCharBase actor, CCharBase target = null)
	{
		switch (m_pSkillInfoLevel.nRangeType)
		{
		case 0:
			if (!(target == null) && !target.isDead && m_GameLogic.IsSkillCanUse(actor, target, m_pSkillInfoLevel))
			{
				Vector3 bloodPos2 = m_Target.GetBloodPos(actor.GetBone(1).position, target.Pos - actor.Pos);
				iGameLogic.HitInfo hitinfo2 = new iGameLogic.HitInfo();
				hitinfo2.v3HitDir = (m_Target.Pos - actor.Pos).normalized;
				hitinfo2.v3HitPos = bloodPos2;
				m_GameLogic.Skill(m_pSkillInfoLevel, actor, target, ref hitinfo2);
				m_Target.PlayAudio(kAudioEnum.HitBody);
			}
			break;
		case 1:
		{
			int num = 0;
			int nValue = 0;
			m_pSkillInfoLevel.GetSkillRangeValue(3, ref nValue);
			List<CCharBase> unitList = m_GameScene.GetUnitList();
			for (int i = 0; i < unitList.Count; i++)
			{
				target = unitList[i];
				if (actor.IsAlly(target))
				{
					continue;
				}
				if (nValue > 0 && num >= nValue)
				{
					break;
				}
				if (target.isDead || !m_GameLogic.IsSkillCanUse(actor, target, m_pSkillInfoLevel))
				{
					continue;
				}
				Vector3 bloodPos = target.GetBloodPos(actor.GetBone(1).position, target.Pos - actor.Pos);
				iGameLogic.HitInfo hitinfo = new iGameLogic.HitInfo();
				hitinfo.v3HitDir = (target.Pos - actor.Pos).normalized;
				hitinfo.v3HitPos = bloodPos;
				m_GameLogic.Skill(m_pSkillInfoLevel, actor, target, ref hitinfo);
				if (target.isDead)
				{
					CCharMob cCharMob = target as CCharMob;
					if (cCharMob != null)
					{
						cCharMob.m_fDeadDistance = 10f;
						if (Vector3.Dot(hitinfo.v3HitDir, actor.Dir2D) > 0f)
						{
							cCharMob.m_v3DeadDirection = hitinfo.v3HitDir;
						}
						else
						{
							cCharMob.m_v3DeadDirection = hitinfo.v3HitDir + actor.Dir2D;
						}
						cCharMob.OnDead(kDeadMode.HitFly);
					}
				}
				target.PlayAudio(kAudioEnum.HitBody);
			}
			break;
		}
		}
	}
}
