using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class doSelectSkillTask : Task
{
	protected iGameSceneBase m_GameScene;

	protected iGameLogic m_GameLogic;

	protected iGameData m_GameData;

	protected List<SkillComboUserInfo> m_ltTemp;

	public doSelectSkillTask(Node node)
		: base(node)
	{
		m_ltTemp = new List<SkillComboUserInfo>();
	}

	public override void OnEnter(Object inputParam)
	{
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		if (m_GameScene != null)
		{
			m_GameLogic = m_GameScene.GetGameLogic();
		}
		m_GameData = iGameApp.GetInstance().m_GameData;
		CCharMob cCharMob = inputParam as CCharMob;
		if (!(cCharMob == null))
		{
			cCharMob.SetCurTask(this);
		}
	}

	public override void OnExit(Object inputParam)
	{
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		if (m_GameData == null || m_GameLogic == null)
		{
			return kTreeRunStatus.Failture;
		}
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null)
		{
			return kTreeRunStatus.Failture;
		}
		SelectValidSkill(cCharMob, cCharMob.m_Target, ref m_ltTemp);
		if (m_ltTemp.Count < 1)
		{
			return kTreeRunStatus.Failture;
		}
		if (m_ltTemp.Count == 1)
		{
			cCharMob.m_pSkillComboInfo = m_GameData.GetSkillComboInfo(m_ltTemp[0].m_nID);
			if (cCharMob.m_pSkillComboInfo == null)
			{
				return kTreeRunStatus.Failture;
			}
			m_ltTemp[0].ResetCoolDown();
			return kTreeRunStatus.Success;
		}
		float[] array = new float[m_ltTemp.Count];
		for (int i = 0; i < m_ltTemp.Count; i++)
		{
			if (i == 0)
			{
				array[i] = m_ltTemp[i].m_fRate;
			}
			else
			{
				array[i] = array[i - 1] + m_ltTemp[i].m_fRate;
			}
		}
		float num = Random.Range(0f, array[m_ltTemp.Count - 1]);
		for (int j = 0; j < m_ltTemp.Count; j++)
		{
			if (num <= array[j])
			{
				CSkillComboInfo skillComboInfo = m_GameData.GetSkillComboInfo(m_ltTemp[j].m_nID);
				if (skillComboInfo != null)
				{
					m_ltTemp[j].ResetCoolDown();
					cCharMob.m_pSkillComboInfo = skillComboInfo;
					return kTreeRunStatus.Success;
				}
			}
		}
		return kTreeRunStatus.Failture;
	}

	protected void SelectValidSkill(CCharMob mob, CCharBase target, ref List<SkillComboUserInfo> ltSkill)
	{
		if (mob == null)
		{
			return;
		}
		CMobInfoLevel mobInfo = m_GameData.GetMobInfo(mob.ID, mob.Level);
		if (mobInfo == null)
		{
			return;
		}
		List<SkillComboUserInfo> list = new List<SkillComboUserInfo>();
		foreach (SkillComboUserInfo item in mob.GetSkillEnumerator())
		{
			if (item.IsCoolDown())
			{
				CSkillComboInfo skillComboInfo = m_GameData.GetSkillComboInfo(item.m_nID);
				if (skillComboInfo != null && (skillComboInfo.nUseCount <= 0 || skillComboInfo.nUseCount > mob.GetUserSkillCount(skillComboInfo.nID)))
				{
					list.Add(item);
				}
			}
		}
		foreach (SkillComboUserInfo item2 in mob.GetAISkillEnumerator())
		{
			if (item2.IsCoolDown())
			{
				CSkillComboInfo skillComboInfo2 = m_GameData.GetSkillComboInfo(item2.m_nID);
				if (skillComboInfo2 != null && (skillComboInfo2.nUseCount <= 0 || skillComboInfo2.nUseCount > mob.GetUserSkillCount(skillComboInfo2.nID)))
				{
					list.Add(item2);
				}
			}
		}
		ltSkill.Clear();
		foreach (SkillComboUserInfo item3 in list)
		{
			if (m_GameLogic.IsComboCanUse(mob, target, item3.m_nID) && m_GameData.m_SkillCenter != null && (!m_GameData.m_SkillCenter.IsContainBlackComboSkill(item3.m_nID) || (m_GameScene.m_bMutiplyGame && !(mob.CurHP / mob.MaxHP > 0.5f))))
			{
				ltSkill.Add(item3);
			}
		}
	}
}
