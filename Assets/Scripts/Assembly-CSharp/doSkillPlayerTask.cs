using BehaviorTree;
using UnityEngine;

public class doSkillPlayerTask : Task
{
	protected CUseSkill m_UseSkill;

	public doSkillPlayerTask(Node node)
		: base(node)
	{
	}

	public override void OnEnter(Object inputParam)
	{
		CCharPlayer cCharPlayer = inputParam as CCharPlayer;
		if (cCharPlayer == null)
		{
			return;
		}
		iGameData gameData = iGameApp.GetInstance().m_GameData;
		if (gameData == null)
		{
			return;
		}
		CSkillInfoLevel skillInfo = gameData.GetSkillInfo(cCharPlayer.m_nNetSkillID, 1);
		if (skillInfo != null)
		{
			switch (skillInfo.nSkillMode)
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
			}
			if (m_UseSkill != null)
			{
				m_UseSkill.Initialize(skillInfo, cCharPlayer.m_pNetSkillTarget);
				m_UseSkill.OnEnter(cCharPlayer);
				cCharPlayer.SetCurTask(this);
			}
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharPlayer cCharPlayer = inputParam as CCharPlayer;
		if (cCharPlayer == null || m_UseSkill == null)
		{
			return kTreeRunStatus.Failture;
		}
		kUseSkillStatus kUseSkillStatus2 = m_UseSkill.OnUpdate(cCharPlayer, deltaTime);
		if (kUseSkillStatus2 == kUseSkillStatus.Executing)
		{
			return kTreeRunStatus.Executing;
		}
		m_UseSkill.OnExit(cCharPlayer);
		cCharPlayer.m_bNetSkill = false;
		if (kUseSkillStatus2 == kUseSkillStatus.Success)
		{
			return kTreeRunStatus.Success;
		}
		return kTreeRunStatus.Failture;
	}
}
