using System;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponMelee : CWeaponBase
{
	protected float m_fHitPoint;

	protected float m_fHitPointCount;

	protected override void OnEquip(CCharPlayer player)
	{
		RefreshBulletUI(player);
	}

	protected override void OnFire(CCharPlayer player)
	{
		if (!player.IsCanAttack())
		{
			return;
		}
		if (base.m_GameScene.IsMyself(player))
		{
			iGameUIBase gameUI = base.m_GameScene.GetGameUI();
			if (gameUI != null)
			{
				gameUI.ExpandAimCross();
			}
		}
		float actionLen = player.GetActionLen(kAnimEnum.Attack);
		actionLen = ((!(actionLen > m_fFireInterval)) ? player.PlayAnimMix(kAnimEnum.Attack, WrapMode.ClampForever, 1f) : player.PlayAnimMix(kAnimEnum.Attack, WrapMode.ClampForever, actionLen / m_fFireInterval));
		float fValue = 0f;
		if (m_pWeaponLvlInfo.GetAtkModeValue(2, ref fValue))
		{
			m_fHitPoint = actionLen * fValue;
			m_fHitPointCount = 0f;
			if (player.CurMixAnim == kAnimEnum.Melee_Attack1 || player.CurMixAnim == kAnimEnum.Melee_Attack1_Back || player.CurMixAnim == kAnimEnum.Melee_Attack1_Forward || player.CurMixAnim == kAnimEnum.Melee_Attack1_Left || player.CurMixAnim == kAnimEnum.Melee_Attack1_Right)
			{
				player.PlayAudio(m_pWeaponLvlInfo.sAudioFire + "_f");
			}
			else if (player.CurMixAnim == kAnimEnum.Melee_Attack2 || player.CurMixAnim == kAnimEnum.Melee_Attack2_Back || player.CurMixAnim == kAnimEnum.Melee_Attack2_Forward || player.CurMixAnim == kAnimEnum.Melee_Attack2_Left || player.CurMixAnim == kAnimEnum.Melee_Attack2_Right)
			{
				player.PlayAudio(m_pWeaponLvlInfo.sAudioFire + "_s");
			}
			else
			{
				player.PlayAudio(m_pWeaponLvlInfo.sAudioFire);
			}
		}
	}

	protected override void OnUpdate(CCharPlayer player, float deltaTime)
	{
		UpdateMelee(player, deltaTime);
		if (m_fFireIntervalCount < m_fFireInterval)
		{
			m_fFireIntervalCount += deltaTime;
			if (m_fFireIntervalCount < m_fFireInterval)
			{
				return;
			}
		}
		if (m_bFire)
		{
			m_fFireIntervalCount = 0f;
			OnFire(player);
		}
	}

	protected override void OnStop(CCharPlayer player)
	{
	}

	protected void UpdateMelee(CCharPlayer player, float deltaTime)
	{
		if (m_fHitPoint <= 0f)
		{
			return;
		}
		m_fHitPointCount += deltaTime;
		if (m_fHitPointCount < m_fHitPoint)
		{
			return;
		}
		m_fHitPointCount = 0f;
		m_fHitPoint = 0f;
		float fValue = 0f;
		float fValue2 = 0f;
		if (!m_pWeaponLvlInfo.GetAtkModeValue(0, ref fValue) || !m_pWeaponLvlInfo.GetAtkModeValue(1, ref fValue2))
		{
			return;
		}
		Dictionary<int, CCharMob> mobData = base.m_GameScene.GetMobData();
		foreach (CCharMob value in mobData.Values)
		{
			if (value.isDead)
			{
				continue;
			}
			Vector3 vector = value.Pos - player.Pos;
			if (vector.sqrMagnitude > fValue * fValue)
			{
				continue;
			}
			if (fValue < 2f)
			{
				if (fValue2 > 0f)
				{
					vector.y = 0f;
					if (Vector3.Dot(player.Dir2D, vector.normalized) <= 0f)
					{
						continue;
					}
				}
			}
			else if (fValue2 > 0f)
			{
				vector.y = 0f;
				if (Vector3.Dot(player.Dir2D, vector.normalized) < Mathf.Cos(fValue2 * ((float)Math.PI / 180f) / 2f))
				{
					continue;
				}
			}
			Vector3 vector2 = value.Pos - player.Pos;
			Vector3 bloodPos = value.GetBloodPos(player.GetUpBodyPos() + new Vector3(0f, 0.7f, 0f), vector2);
			base.m_GameScene.AddHitEffect(bloodPos, vector2, m_pWeaponLvlInfo.nHit);
			base.m_GameScene.ShakeCamera(0.2f, 0.1f);
			if (!base.isNetPlayerShoot)
			{
				OnHitMob(player, value, bloodPos, vector2, string.Empty);
			}
			value.PlayAudio(kAudioEnum.HitBody);
			switch (m_pWeaponLvlInfo.nElementType)
			{
			case 1:
				value.PlayAudio("Fx_Impact_fire");
				break;
			case 3:
				value.PlayAudio("Fx_Impact_freeze");
				break;
			case 2:
				value.PlayAudio("Fx_Impact_electric");
				break;
			}
		}
	}

	protected override void OnHitMob(CCharPlayer player, CCharMob mob, Vector3 hitpos, Vector3 hitdir, string sBodyPart = "")
	{
		mob.SetLifeBarParam(1f);
		float num = player.CalcWeaponDamage(m_pWeaponLvlInfo);
		float num2 = player.CalcCritical(m_pWeaponLvlInfo);
		float num3 = player.CalcCriticalDmg(m_pWeaponLvlInfo);
		bool bCritical = false;
		if (num2 > UnityEngine.Random.Range(1f, 100f))
		{
			num *= 1f + num3 / 100f;
			bCritical = true;
		}
		float elementValue = m_pWeaponLvlInfo.GetElementValue(mob.ID);
		if (elementValue != 0f)
		{
			num *= 1f + elementValue / 100f;
		}
		CCharBoss cCharBoss = mob as CCharBoss;
		if (cCharBoss != null && cCharBoss.isInBlack)
		{
			cCharBoss.AddBlackDmg(0f - num);
			base.m_GameScene.AddDamageText(num, hitpos, bCritical);
			if (CGameNetManager.GetInstance().IsConnected() && base.m_GameScene.IsMyself(player))
			{
				CGameNetSender.GetInstance().SendMsg_BATTLE_DAMAGE_MOB(mob.UID, num, true);
			}
			return;
		}
		float num4 = mob.CalcProtect();
		num *= 1f - num4 / 100f;
		if (num < 1f)
		{
			num = 1f;
		}
		base.m_GameScene.AddMyDamage(num, mob.CurHP);
		mob.OnHit(0f - num, m_pWeaponLvlInfo, string.Empty);
		base.m_GameScene.AddDamageText(num, hitpos, bCritical);
		base.m_GameScene.AddHitEffect(hitpos, Vector3.forward, 1116);
		iGameLogic.HitInfo hitinfo = new iGameLogic.HitInfo();
		hitinfo.v3HitDir = hitdir;
		hitinfo.v3HitPos = hitpos;
		m_GameLogic = base.m_GameScene.GetGameLogic();
		if (m_GameLogic != null)
		{
			m_GameLogic.CaculateFunc(player, mob, m_pWeaponLvlInfo.arrFunc, m_pWeaponLvlInfo.arrValueX, m_pWeaponLvlInfo.arrValueY, ref hitinfo);
			m_GameLogic.ltDamageInfo.Add(num);
			m_GameLogic.m_fTotalDmg += num;
		}
		if (CGameNetManager.GetInstance().IsConnected() && base.m_GameScene.IsMyself(player))
		{
			CGameNetSender.GetInstance().SendMsg_BATTLE_DAMAGE_MOB(mob.UID, m_GameLogic.m_fTotalDmg);
		}
		if (!mob.isDead)
		{
			return;
		}
		CMobInfoLevel mobInfo = mob.GetMobInfo();
		if (mobInfo != null)
		{
			int num5 = 0;
			num5 = ((!base.m_GameScene.m_bMutiplyGame) ? mobInfo.nExp : MyUtils.formula_monsterexp(mobInfo.nExp, mob.Level));
			float value = player.Property.GetValue(kProEnum.Char_IncreaseExp);
			if (value > 0f)
			{
				num5 = (int)((float)num5 * (1f + value / 100f));
			}
			player.AddExp(num5);
			base.m_GameScene.AddExpText(num5, hitinfo.v3HitPos);
		}
	}
}
