using gyAchievementSystem;
using gyEvent;
using gyTaskSystem;
using UnityEngine;

public class CUseSkillBoom : CUseSkillOnce
{
	public override kUseSkillStatus OnUpdate(CCharBase charbase, float deltaTime)
	{
		if (!IsSkillValid())
		{
			Debug.Log("invalid skill");
			return kUseSkillStatus.Failure;
		}
		CCharMob cCharMob = charbase as CCharMob;
		if (cCharMob == null)
		{
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
				CMobInfoLevel mobInfo = cCharMob.GetMobInfo();
				if (mobInfo != null)
				{
					float fValue = 0f;
					m_pSkillInfoLevel.GetSkillModeValue(1, ref fValue);
					m_GameScene.Boom(charbase.GetBone(2).position, fValue, new int[1] { 5 }, new int[1] { 100 }, new int[1], "Fx_Explosion_RPG", -1, null, charbase);
				}
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
			charbase.isNeedDestroy = true;
			charbase.isDead = true;
			m_GameScene.AddMonsterNumLimit(cCharMob.ID, cCharMob.MobType, cCharMob.MobBehavior, -1);
			m_GameScene.AddWaveMobNumber(cCharMob.GenerateWaveID, -1);
			if (m_GameScene.m_MGManager != null)
			{
				CEventManager eventManager = m_GameScene.m_MGManager.GetEventManager();
				if (eventManager != null)
				{
					eventManager.Trigger(new EventCondition_MobByWave(cCharMob.GenerateWaveID, cCharMob.GenerateSequence, 1));
					eventManager.Trigger(new EventCondition_MobByID(cCharMob.ID, 1));
					int generateWaveID = cCharMob.GenerateWaveID;
					if (!m_GameScene.m_MGManager.IsWaveProcess(generateWaveID))
					{
						int waveMobNumber = m_GameScene.GetWaveMobNumber(cCharMob.GenerateWaveID);
						eventManager.Trigger(new EventCondition_WaveNumberLeft(generateWaveID, waveMobNumber));
						if (m_GameScene.m_TaskManager != null)
						{
							CTaskBase task = m_GameScene.m_TaskManager.GetTask();
							if (task != null)
							{
								CTaskInfo taskInfo = task.GetTaskInfo();
								if (taskInfo != null && taskInfo.nType == 3 && waveMobNumber == 0)
								{
									CAchievementManager.GetInstance().AddAchievement(8);
								}
							}
						}
					}
				}
			}
			if (m_GameScene.m_TaskManager != null)
			{
				m_GameScene.m_TaskManager.OnKillMonster(cCharMob.ID);
				if (cCharMob.IsBoss())
				{
					iGameApp.GetInstance().m_GameState.LastKillBoss = cCharMob.UID;
				}
				if (m_GameScene.m_MGManager != null && m_GameScene.m_MGManager.IsWaveCompleted() && m_GameScene.GetMobAliveCount() == 0)
				{
					m_GameScene.m_TaskManager.OnKillAllMonsters();
				}
			}
			return kUseSkillStatus.Success;
		}
		return kUseSkillStatus.Executing;
	}
}
