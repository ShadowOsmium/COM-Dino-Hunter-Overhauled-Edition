using System;
using System.Collections.Generic;
using UnityEngine;

public class iGameLogic
{
	public class HitInfo
	{
		public Vector3 v3HitDir = Vector3.zero;

		public Vector3 v3HitPos = Vector3.zero;

		public CWeaponInfoLevel weaponinfolevel;

		public bool isPlayerSkill;

		public int nFromSkill = -1;

		public bool isHurt;

		public bool ismutiply;

		public float mutiplyeff;

		public float mutiplyefftime;
	}

	public List<float> ltDamageInfo;

	public float m_fTotalDmg;

	protected iGameSceneBase m_GameScene;

	protected iGameUIBase m_GameUI;

	protected iGameState m_GameState;

	protected iGameData m_GameData;

	protected List<int> m_ltFunc;

	protected List<int> m_ltValueX;

	protected List<int> m_ltValueY;

	public void Initialize()
	{
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		m_GameUI = m_GameScene.GetGameUI();
		m_GameState = iGameApp.GetInstance().m_GameState;
		m_GameData = iGameApp.GetInstance().m_GameData;
		ltDamageInfo = new List<float>();
		m_ltFunc = new List<int>();
		m_ltValueX = new List<int>();
		m_ltValueY = new List<int>();
	}

	public void CaculateFunc(CCharBase actor, CCharBase target, int[] arrFunc, int[] arrValueX, int[] arrValueY, ref HitInfo hitinfo)
	{
		ltDamageInfo.Clear();
		m_fTotalDmg = 0f;
		for (int i = 0; i < arrFunc.Length; i++)
		{
			int num = arrFunc[i];
			int num2 = arrValueX[i];
			int num3 = arrValueY[i];
			if (num > 0)
			{
			}
			switch (num)
			{
			case 2:
				if (target != null && (target.Property == null || !(target.Property.GetValue(kProEnum.Invincible) > 0f)))
				{
					float num14 = num2;
					float num15 = actor.CalcCritical(hitinfo.weaponinfolevel);
					float num16 = actor.CalcCriticalDmg(hitinfo.weaponinfolevel);
					bool bCritical3 = false;
					if (num15 > UnityEngine.Random.Range(1f, 100f))
					{
						num14 *= 1f + num16 / 100f;
						bCritical3 = true;
					}
					float num17 = target.CalcProtect();
					num14 *= 1f - num17 / 100f;
					if (num14 < 1f)
					{
						num14 = 1f;
					}
					target.OnHit(0f - num14, hitinfo.weaponinfolevel, string.Empty);
					hitinfo.isHurt = true;
					if (m_GameScene.IsMyself(actor))
					{
						m_GameScene.AddDamageText(num14, hitinfo.v3HitPos, bCritical3);
					}
					else if (m_GameScene.IsMyself(target))
					{
						m_GameScene.AddDamageText(num14, hitinfo.v3HitPos, Color.red, bCritical3);
					}
					m_GameScene.AddHitEffect(hitinfo.v3HitPos, Vector3.forward, 1115);
					ltDamageInfo.Add(num14);
					m_fTotalDmg += num14;
				}
				break;
			case 5:
				if (target != null && (target.Property == null || !(target.Property.GetValue(kProEnum.Invincible) > 0f)))
				{
					float num19 = actor.Property.GetValue(kProEnum.Damage) * (float)MyUtils.Low32(num2) / 100f + (float)MyUtils.High32(num2);
					float num20 = actor.CalcCritical(hitinfo.weaponinfolevel);
					float num21 = actor.CalcCriticalDmg(hitinfo.weaponinfolevel);
					bool bCritical4 = false;
					if (num20 > UnityEngine.Random.Range(1f, 100f))
					{
						num19 *= 1f + num21 / 100f;
						bCritical4 = true;
					}
					float num22 = target.CalcProtect();
					num19 *= 1f - num22 / 100f;
					if (num19 < 1f)
					{
						num19 = 1f;
					}
					target.OnHit(0f - num19, hitinfo.weaponinfolevel, string.Empty);
					hitinfo.isHurt = true;
					if (m_GameScene.IsMyself(actor))
					{
						m_GameScene.AddDamageText(num19, hitinfo.v3HitPos, bCritical4);
					}
					else if (m_GameScene.IsMyself(target))
					{
						m_GameScene.AddDamageText(num19, hitinfo.v3HitPos, Color.red, bCritical4);
					}
					m_GameScene.AddHitEffect(hitinfo.v3HitPos, Vector3.forward, 1115);
					ltDamageInfo.Add(num19);
					m_fTotalDmg += num19;
				}
				break;
			case 7:
			{
				if (target.Property != null && target.Property.GetValue(kProEnum.Invincible) > 0f)
				{
					break;
				}
				float num9 = num2;
				float num10 = actor.CalcCritical(hitinfo.weaponinfolevel);
				float num11 = actor.CalcCriticalDmg(hitinfo.weaponinfolevel);
				foreach (CCharMob item in m_GameScene.GetMobEnumerator())
				{
					if (!(item == actor) && !item.isDead && !(Vector3.Distance(item.Pos, hitinfo.v3HitPos) > (float)num3))
					{
						float num12 = num9;
						bool bCritical2 = false;
						if (num10 > UnityEngine.Random.Range(1f, 100f))
						{
							num12 *= 1f + num11 / 100f;
							bCritical2 = true;
						}
						float num13 = item.CalcProtect();
						num12 *= 1f - num13 / 100f;
						if (num12 < 1f)
						{
							num12 = 1f;
						}
						target.OnHit(0f - num12, hitinfo.weaponinfolevel, string.Empty);
						hitinfo.isHurt = true;
						m_GameScene.AddDamageText(num12, item.GetBone(1).position, bCritical2);
						m_GameScene.AddHitEffect(item.GetBone(1).position, Vector3.forward, 1115);
					}
				}
				break;
			}
			case 8:
			{
				if (target.Property != null && target.Property.GetValue(kProEnum.Invincible) > 0f)
				{
					break;
				}
				float num4 = actor.Property.GetValue(kProEnum.Damage) * (float)MyUtils.Low32(num2) / 100f + (float)MyUtils.High32(num2);
				float num5 = actor.CalcCritical(hitinfo.weaponinfolevel);
				float num6 = actor.CalcCriticalDmg(hitinfo.weaponinfolevel);
				foreach (CCharMob item2 in m_GameScene.GetMobEnumerator())
				{
					if (!(item2 == actor) && !item2.isDead && !(Vector3.Distance(item2.Pos, hitinfo.v3HitPos) > (float)num3))
					{
						float num7 = num4;
						bool bCritical = false;
						if (num5 > UnityEngine.Random.Range(1f, 100f))
						{
							num7 *= 1f + num6 / 100f;
							bCritical = true;
						}
						float num8 = item2.CalcProtect();
						num7 *= 1f - num8 / 100f;
						if (num7 < 1f)
						{
							num7 = 1f;
						}
						target.OnHit(0f - num7, hitinfo.weaponinfolevel, string.Empty);
						hitinfo.isHurt = true;
						m_GameScene.AddDamageText(num7, item2.GetBone(1).position, bCritical);
						m_GameScene.AddHitEffect(item2.GetBone(1).position, Vector3.forward, 1115);
					}
				}
				break;
			}
			case 3:
				if (target == null)
				{
					return;
				}
				target.AddBuff(num2, num3, hitinfo.nFromSkill);
				UnityEngine.Debug.Log("add buff " + target.UID + " " + num3);
				if (!hitinfo.ismutiply)
				{
					break;
				}
				foreach (CCharPlayer item3 in m_GameScene.GetPlayerEnumerator())
				{
					if (!(item3 == target) && !item3.isDead)
					{
						item3.AddBuff(num2, (float)num3 * (1f - hitinfo.mutiplyefftime / 100f), hitinfo.nFromSkill);
						UnityEngine.Debug.Log("add buff " + item3.UID + " " + (float)num3 * (1f - hitinfo.mutiplyefftime / 100f) + " " + hitinfo.mutiplyefftime);
					}
				}
				break;
			case 4:
			{
				if (!(target != null) || (target.Property != null && target.Property.GetValue(kProEnum.Invincible) > 0f) || target.m_bMoribund)
				{
					break;
				}
				float num23 = num2;
				float value3 = target.Property.GetValue(kProEnum.ResistBeatBack);
				if (value3 > 0f)
				{
					float num24 = num3;
					if (actor.IsPlayer() && hitinfo.weaponinfolevel != null)
					{
						num24 = (float)num3 + ((CCharPlayer)actor).CalcWeaponBeatBack(hitinfo.weaponinfolevel);
					}
					if (value3 < num24)
					{
						num23 = (float)num2 * ((num24 - value3) / num24);
					}
				}
				target.BeatBack(hitinfo.v3HitDir, num23);
				if (!CGameNetManager.GetInstance().IsConnected())
				{
					break;
				}
				if (target.IsMonster())
				{
					if (m_GameScene.IsMyself(actor))
					{
						CGameNetSender.GetInstance().SendMsg_MOB_BEATBACK(target.UID, target.Pos + hitinfo.v3HitDir * num23);
					}
				}
				else if (target.IsUser())
				{
					CGameNetSender.GetInstance().SendMsg_PLAYER_BEATBACK(target.Pos + hitinfo.v3HitDir * num23);
				}
				break;
			}
			case 9:
			{
				if (!(target != null))
				{
					break;
				}
				float num18 = target.Property.GetValue(kProEnum.HPMax) * (float)num2 / 100f + (float)num3;
				target.AddHP(num18);
				if (hitinfo.ismutiply)
				{
					foreach (CCharPlayer item4 in m_GameScene.GetPlayerEnumerator())
					{
						if (!(item4 == target) && !item4.isDead)
						{
							item4.AddHP(num18 * (1f - hitinfo.mutiplyeff / 100f));
						}
					}
				}
				if (m_GameScene.IsMyself(actor) || m_GameScene.IsMyself(target))
				{
					m_GameScene.AddHealText(num18, target.GetBone(0).position);
				}
				break;
			}
			case 101:
			{
				CCharUser cCharUser3 = target as CCharUser;
				if (cCharUser3 != null)
				{
					float value = cCharUser3.Property.GetValue(kProEnum.Char_IncreaseExp);
					if (value > 0f)
					{
						num2 = (int)((float)num2 * (1f + value / 100f));
					}
					cCharUser3.AddExp(num2);
					m_GameScene.AddExpText(num2, cCharUser3.GetBone(0).position);
				}
				break;
			}
			case 10:
				target.SetStealth(true, num2);
				UnityEngine.Debug.Log("add Stealth " + target.UID + " " + num2);
				if (!hitinfo.ismutiply)
				{
					break;
				}
				foreach (CCharPlayer item5 in m_GameScene.GetPlayerEnumerator())
				{
					if (!(item5 == target) && !item5.isDead)
					{
						item5.SetStealth(true, (float)num2 * (1f - hitinfo.mutiplyefftime / 100f));
						UnityEngine.Debug.Log("add Stealth " + item5.UID + " " + (float)num2 * (1f - hitinfo.mutiplyefftime / 100f) + " " + hitinfo.mutiplyefftime);
					}
				}
				break;
			case 11:
				if (target.Property == null || (!(target.Property.GetValue(kProEnum.Invincible) > 0f) && !(target.Property.GetValue(kProEnum.AntiStun) > 0f)))
				{
					target.SetStun(true, num2);
				}
				break;
			case 100:
			{
				CCharUser cCharUser4 = target as CCharUser;
				if (cCharUser4 != null)
				{
					float value2 = cCharUser4.Property.GetValue(kProEnum.Char_IncreaseGold);
					if (value2 > 0f)
					{
						num2 = (int)((float)num2 * (1f + value2 / 100f));
					}
					m_GameState.AddGold(num2);
					m_GameScene.AddGoldText(num2, cCharUser4.GetBone(1).position);
				}
				break;
			}
			case 103:
			{
				CCharUser cCharUser2 = target as CCharUser;
				if (cCharUser2 != null)
				{
					UnityEngine.Debug.Log("kSkillFunc.AddCrystal " + num2);
					m_GameState.AddCrystal(num2);
					m_GameScene.AddCrystalText(num2, cCharUser2.GetBone(1).position);
				}
				break;
			}
			case 102:
			{
				CCharUser cCharUser = target as CCharUser;
				if (cCharUser != null)
				{
					CItemInfoLevel itemInfo = m_GameData.GetItemInfo(num2, 1);
					if (itemInfo != null)
					{
						m_GameScene.AddMaterial(cCharUser.GetBone(1).position, itemInfo.sIcon, num3);
					}
				}
				m_GameState.AddMaterial(num2, num3);
				break;
			}
			case 104:
			{
				CCharBoss cCharBoss2 = actor as CCharBoss;
				if (cCharBoss2 != null)
				{
					cCharBoss2.ChangeAI = num2;
				}
				break;
			}
			case 13:
			{
				CCharBoss cCharBoss = actor as CCharBoss;
				if (cCharBoss != null)
				{
					cCharBoss.SetReadyToBlack(true, (float)num2 * cCharBoss.MaxHP / 100f);
				}
				break;
			}
			}
		}
	}

	public void Skill(int nSkillID, CCharBase attacker, CCharBase defender, ref HitInfo hitinfo)
	{
		CSkillInfoLevel skillInfo = m_GameData.GetSkillInfo(nSkillID, 1);
		if (skillInfo != null)
		{
			Skill(skillInfo, attacker, defender, ref hitinfo);
		}
	}

	public void Skill(CSkillInfoLevel skillinfolevel, CCharBase attacker, CCharBase defender, ref HitInfo hitinfo)
	{
		if (skillinfolevel != null && !(attacker == null) && !(defender == null))
		{
			hitinfo.nFromSkill = skillinfolevel.nID;
			hitinfo.ismutiply = skillinfolevel.m_bMutiply;
			hitinfo.mutiplyeff = skillinfolevel.m_fMutiplyEff;
			hitinfo.mutiplyefftime = skillinfolevel.m_fMutiplyEffTime;
			UnityEngine.Debug.Log(skillinfolevel.nID + " " + skillinfolevel.m_bMutiply + " " + skillinfolevel.m_fMutiplyEff + " " + skillinfolevel.m_fMutiplyEffTime);
			Vector3 normalized = (defender.Pos - attacker.Pos).normalized;
			CCharPlayer cCharPlayer = attacker as CCharPlayer;
			if (cCharPlayer != null && cCharPlayer.Property != null && cCharPlayer.Property.GetSkillPro(skillinfolevel.nID) != null)
			{
				m_ltFunc.Clear();
				m_ltValueX.Clear();
				m_ltValueY.Clear();
				cCharPlayer.Property.CaculateSkillFuncBySkillPro(skillinfolevel, ref m_ltFunc, ref m_ltValueX, ref m_ltValueY);
				CaculateFunc(attacker, defender, m_ltFunc.ToArray(), m_ltValueX.ToArray(), m_ltValueY.ToArray(), ref hitinfo);
			}
			else
			{
				CaculateFunc(attacker, defender, skillinfolevel.arrFunc, skillinfolevel.arrValueX, skillinfolevel.arrValueY, ref hitinfo);
			}
		}
	}

	public void Item(int nItemID, int nItemLevel, CCharBase actor, CCharBase target)
	{
		CItemInfoLevel itemInfo = m_GameData.GetItemInfo(nItemID, nItemLevel);
		if (itemInfo != null)
		{
			Item(itemInfo, actor, target);
		}
	}

	public void Item(CItemInfoLevel iteminfolevel, CCharBase actor, CCharBase target)
	{
		if (iteminfolevel != null && !(actor == null))
		{
			HitInfo hitinfo = new HitInfo();
			CaculateFunc(actor, target, iteminfolevel.arrFunc, iteminfolevel.arrValueX, iteminfolevel.arrValueY, ref hitinfo);
		}
	}

	public bool IsSkillCanUse(CCharBase actor, CCharBase target, int nSkillID)
	{
		CSkillInfoLevel skillInfo = m_GameData.GetSkillInfo(nSkillID, 1);
		if (skillInfo == null)
		{
			return false;
		}
		return IsSkillCanUse(actor, target, skillInfo);
	}

	public bool IsSkillCanUse(CCharBase actor, CCharBase target, CSkillInfoLevel skillinfolevel)
	{
		if (skillinfolevel == null || target == null)
		{
			return false;
		}
		switch (skillinfolevel.nRangeType)
		{
		case 0:
		{
			float fValue4 = 0f;
			float fValue5 = 0f;
			if (skillinfolevel.GetSkillRangeValue(0, ref fValue4) && skillinfolevel.GetSkillRangeValue(1, ref fValue5))
			{
				float num2 = Vector3.Distance(actor.Pos, target.Pos);
				if (num2 < fValue4 || num2 > fValue5)
				{
					return false;
				}
			}
			break;
		}
		case 1:
		{
			float fValue = 0f;
			float fValue2 = 0f;
			float fValue3 = 0f;
			skillinfolevel.GetSkillRangeValue(0, ref fValue);
			skillinfolevel.GetSkillRangeValue(1, ref fValue2);
			skillinfolevel.GetSkillRangeValue(2, ref fValue3);
			float num = Vector3.Distance(actor.Pos, target.Pos);
			if (num < fValue || num > fValue2)
			{
				return false;
			}
			if (fValue3 != 0f)
			{
				Vector3 vector = target.Pos - actor.Pos;
				vector.y = 0f;
				if (Vector3.Dot(actor.Dir2D, vector.normalized) < Mathf.Cos(fValue3 * ((float)Math.PI / 180f) / 2f))
				{
					return false;
				}
			}
			break;
		}
		}
		return true;
	}

	public bool IsComboCanUse(CCharBase actor, CCharBase target, int nComboID)
	{
		CSkillComboInfo skillComboInfo = m_GameData.GetSkillComboInfo(nComboID);
		if (skillComboInfo == null || skillComboInfo.ltSkill.Count < 1)
		{
			return false;
		}
		int num = skillComboInfo.ltSkill[0];
		if (num == -1)
		{
			return false;
		}
		return IsSkillCanUse(actor, target, num);
	}
}
