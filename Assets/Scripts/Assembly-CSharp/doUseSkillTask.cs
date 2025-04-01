using BehaviorTree;
using UnityEngine;

public class doUseSkillTask : Task
{
	protected iGameData m_GameData;

	protected iGameSceneBase m_GameScene;

	protected iGameLogic m_GameLogic;

	protected CCharBase m_Target;

	protected CSkillInfoLevel m_pSkillInfoLevel;

	protected CUseSkill m_UseSkill;

	protected bool m_bSkillCompleted;

	protected bool m_bSkillInitCompeleted;

	public doUseSkillTask(Node node)
		: base(node)
	{
		m_bSkillCompleted = false;
	}

	protected bool InitSkill(CCharMob mob, int nComboIndex)
	{
		m_bSkillCompleted = false;
		if (mob == null || mob.m_pSkillComboInfo == null)
		{
			return false;
		}
		int skill = mob.m_pSkillComboInfo.GetSkill(nComboIndex);
		if (skill == -1)
		{
			m_bSkillCompleted = true;
			return false;
		}
		m_pSkillInfoLevel = m_GameData.GetSkillInfo(skill, 1);
		if (m_pSkillInfoLevel == null)
		{
			return false;
		}
		switch (m_pSkillInfoLevel.nSkillMode)
		{
		case 1:
			m_UseSkill = new CUseSkillOnce();
			break;
		case 2:
			m_UseSkill = new CUseSkillHold();
			break;
		case 4:
			m_UseSkill = new CUseSkillRush();
			break;
		case 3:
			m_UseSkill = new CUseSkillLock();
			break;
		case 5:
			m_UseSkill = new CUseSkillSpawn();
			break;
		case 6:
			m_UseSkill = new CUseSkillBump();
			break;
		case 7:
			m_UseSkill = new CUseSkillBoom();
			break;
		case 8:
			m_UseSkill = new CUseSkillDrift();
			break;
		case 9:
			m_UseSkill = new CUseSkillBumpTrack();
			break;
		}
		if (m_UseSkill == null)
		{
			return false;
		}
		m_UseSkill.Initialize(m_pSkillInfoLevel, m_Target);
		if (m_UseSkill.OnEnter(mob) == kUseSkillStatus.Failure)
		{
			return false;
		}
		return true;
	}

	protected kTreeRunStatus UpdateSkill(CCharMob mob, float deltaTime)
	{
		if (m_UseSkill == null || !m_bSkillInitCompeleted)
		{
			return kTreeRunStatus.Failture;
		}
		switch (m_UseSkill.OnUpdate(mob, deltaTime))
		{
		case kUseSkillStatus.Executing:
			return kTreeRunStatus.Executing;
		case kUseSkillStatus.Failure:
			return kTreeRunStatus.Failture;
		default:
			DestroySkill(mob);
			m_bSkillInitCompeleted = InitSkill(mob, mob.m_nCurComboIndex);
			if (!m_bSkillInitCompeleted)
			{
				if (m_bSkillCompleted)
				{
					float fTime = 1f;
					if (mob.m_pSkillComboInfo != null)
					{
						fTime = mob.m_pSkillComboInfo.fFreezeTime;
					}
					mob.SetFreeze(true, fTime);
					return kTreeRunStatus.Success;
				}
				return kTreeRunStatus.Failture;
			}
			mob.m_nCurComboIndex++;
			return kTreeRunStatus.Executing;
		}
	}

	protected void DestroySkill(CCharMob mob)
	{
		if (m_UseSkill != null)
		{
			m_UseSkill.OnExit(mob);
			m_UseSkill = null;
		}
	}

	public override void OnEnter(Object inputParam)
	{
		m_GameData = iGameApp.GetInstance().m_GameData;
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		if (m_GameScene != null)
		{
			m_GameLogic = m_GameScene.GetGameLogic();
		}
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null)
		{
			return;
		}
		cCharMob.SetCurTask(this);
		m_Target = cCharMob.m_Target;
		if (m_Target != null && Vector3.Distance(cCharMob.Pos, m_Target.Pos) <= 10f)
		{
			m_Target.AddMeleeAttacker(cCharMob.UID);
		}
		m_bSkillInitCompeleted = InitSkill(cCharMob, cCharMob.m_nCurComboIndex);
		if (m_bSkillInitCompeleted)
		{
			cCharMob.m_nCurComboIndex++;
			cCharMob.AddUseSkillRecord(cCharMob.m_pSkillComboInfo.nID);
			if (CGameNetManager.GetInstance().IsRoomMaster() && CGameNetManager.GetInstance().IsConnected())
			{
				CGameNetSender.GetInstance().SendMsg_MOB_SKILL(cCharMob.UID, cCharMob.m_Target.UID, cCharMob.m_pSkillComboInfo.nID, 0);
			}
		}
	}

	public override void OnExit(Object inputParam)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (!(cCharMob == null))
		{
			if (m_Target != null)
			{
				m_Target.DelMeleeAttacker(cCharMob.UID);
			}
			DestroySkill(cCharMob);
			cCharMob.m_nCurComboIndex = 0;
			cCharMob.m_pSkillComboInfo = null;
			cCharMob.m_fHoverTime = 0f;
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null)
		{
			return kTreeRunStatus.Failture;
		}
		return UpdateSkill(cCharMob, deltaTime);
	}
}
