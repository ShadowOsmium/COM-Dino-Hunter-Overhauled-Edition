using System.Collections.Generic;
using UnityEngine;

public class CUseSkillBump : CUseSkill
{
	protected int m_nEffectID = -1;

	protected float m_fBumpDis;

	protected float m_fBumpTime = 1f;

	protected float m_fBumpTimeCount;

	protected float m_fBumpFuncTime;

	protected float m_fBumpFuncTimeCount;

	protected Vector3 m_v3Src;

	protected Vector3 m_v3Dst;

	protected float m_fSpeed;

	protected iRushEffect m_RushEffect;

	public override kUseSkillStatus OnEnter(CCharBase charbase)
	{
		charbase.m_bBumping = true;
		m_pSkillInfoLevel.GetSkillModeValue(0, ref m_fBumpDis);
		m_pSkillInfoLevel.GetSkillModeValue(1, ref m_fBumpTime);
		m_pSkillInfoLevel.GetSkillModeValue(2, ref m_fBumpFuncTime);
		m_pSkillInfoLevel.GetSkillModeValue(3, ref m_nEffectID);
		m_fBumpFuncTimeCount = 0f;
		m_v3Src = charbase.Pos;
		m_v3Dst = charbase.Pos + charbase.Dir2D * m_fBumpDis;
		Ray ray = new Ray(charbase.Pos, charbase.Dir2D);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo, m_fBumpDis, -1874853888))
		{
			m_fBumpDis = Vector3.Distance(ray.origin, hitInfo.point) - 2f;
			m_v3Dst = charbase.Pos + charbase.Dir2D * m_fBumpDis;
		}
		m_fSpeed = m_fBumpDis / m_fBumpTime;
		float speed = 1f;
		switch (charbase.CharType)
		{
		case kCharType.Mob:
		case kCharType.Boss:
		{
			CCharMob cCharMob = charbase as CCharMob;
			if (!(cCharMob != null))
			{
				break;
			}
			CMobInfoLevel mobInfo = cCharMob.GetMobInfo();
			if (mobInfo != null)
			{
				if (m_pSkillInfoLevel.nAnim == 4)
				{
					speed = m_fSpeed * mobInfo.fMoveSpeedRate;
				}
				else if (m_pSkillInfoLevel.nAnim == 2504)
				{
					speed = m_fSpeed * mobInfo.fRushSpeedRate;
				}
			}
			break;
		}
		case kCharType.Player:
			if (m_pSkillInfoLevel.nAnim == 4)
			{
				speed = m_fSpeed * 0.3f;
			}
			break;
		case kCharType.User:
		{
			CCharUser cCharUser = charbase as CCharUser;
			if (!(cCharUser != null))
			{
				break;
			}
			if (m_pSkillInfoLevel.nAnim == 4)
			{
				speed = m_fSpeed * 0.3f;
			}
			cCharUser.MoveStop();
			cCharUser.SetFire(false);
			CWeaponInfoLevel curWeaponLvlInfo = cCharUser.GetCurWeaponLvlInfo();
			if (curWeaponLvlInfo != null && curWeaponLvlInfo.nType != 1)
			{
				iCameraTrail camera = m_GameScene.GetCamera();
				if (camera != null)
				{
					camera.SetViewMelee(true);
				}
			}
			break;
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
				Debug.Log(actionLen + " " + m_fBumpTime);
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
		if (m_pSkillInfoLevel.sUseAudio.Length > 0)
		{
			charbase.PlayAudio(m_pSkillInfoLevel.sUseAudio);
		}
		return kUseSkillStatus.Success;
	}

	public override void OnExit(CCharBase charbase)
	{
		charbase.m_bBumping = false;
		if (m_pSkillInfoLevel != null && m_pSkillInfoLevel.nAnimBuffID > 0)
		{
			charbase.DelBuffByID(m_pSkillInfoLevel.nAnimBuffID);
		}
		if (m_RushEffect != null)
		{
			m_RushEffect.iRushEffect_StopEffect(true);
		}
		if (!charbase.IsPlayer() && !charbase.IsUser())
		{
			return;
		}
		charbase.CrossAnim(kAnimEnum.Idle, WrapMode.Loop, 0.3f, 1f, 0f);
		CCharUser cCharUser = charbase as CCharUser;
		if (!(cCharUser != null))
		{
			return;
		}
		CWeaponInfoLevel curWeaponLvlInfo = cCharUser.GetCurWeaponLvlInfo();
		if (curWeaponLvlInfo != null && curWeaponLvlInfo.nType != 1)
		{
			iCameraTrail camera = m_GameScene.GetCamera();
			if (camera != null)
			{
				camera.SetViewMelee(false);
			}
		}
	}

	public override kUseSkillStatus OnUpdate(CCharBase charbase, float deltaTime)
	{
		m_fBumpFuncTimeCount += deltaTime;
		if (m_fBumpFuncTimeCount >= m_fBumpFuncTime)
		{
			m_fBumpFuncTimeCount = 0f;
			SkillEffect(charbase, m_Target);
		}
		if (m_fBumpTimeCount < m_fBumpTime)
		{
			m_fBumpTimeCount += deltaTime;
			charbase.Pos = Vector3.Lerp(m_v3Src, m_v3Dst, m_fBumpTimeCount / m_fBumpTime);
			if (m_fBumpTimeCount >= m_fBumpTime)
			{
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
