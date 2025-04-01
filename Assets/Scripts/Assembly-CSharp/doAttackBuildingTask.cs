using BehaviorTree;
using gyTaskSystem;
using UnityEngine;

public class doAttackBuildingTask : Task
{
	protected enum kState
	{
		Idle,
		Attack
	}

	protected iGameSceneBase m_GameScene;

	protected iGameData m_GameData;

	protected kState m_State;

	protected float m_fTime;

	protected float m_fTimeCount;

	protected float m_fRotTime;

	public doAttackBuildingTask(Node node)
		: base(node)
	{
	}

	public override void OnEnter(Object inputParam)
	{
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		m_GameData = iGameApp.GetInstance().m_GameData;
		CCharMob cCharMob = inputParam as CCharMob;
		if (!(cCharMob == null) && !(cCharMob.m_TargetBuilding == null) && !cCharMob.m_TargetBuilding.IsBroken)
		{
			cCharMob.SetCurTask(this);
			m_State = kState.Idle;
			m_fTime = 0.5f;
			m_fTimeCount = 0f;
			cCharMob.CrossAnim(kAnimEnum.Idle, WrapMode.Loop, 0.3f, 1f, 0f);
			m_fRotTime = 0f;
		}
	}

	public override void OnExit(Object inputParam)
	{
		base.OnExit(inputParam);
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null || cCharMob.m_TargetBuilding == null || cCharMob.m_TargetBuilding.IsBroken)
		{
			return kTreeRunStatus.Failture;
		}
		CMobInfoLevel mobInfo = m_GameData.GetMobInfo(cCharMob.ID, cCharMob.Level);
		if (mobInfo == null)
		{
			return kTreeRunStatus.Failture;
		}
		if (cCharMob.m_TargetBuilding.IsBroken)
		{
			cCharMob.m_TargetBuilding = null;
			return kTreeRunStatus.Success;
		}
		m_fRotTime += deltaTime;
		cCharMob.Dir2D = Vector3.Lerp(cCharMob.Dir2D, cCharMob.m_TargetBuilding.transform.position - cCharMob.Pos, m_fRotTime);
		switch (m_State)
		{
		case kState.Idle:
			m_fTimeCount += deltaTime;
			if (m_fTimeCount < m_fTime)
			{
				return kTreeRunStatus.Executing;
			}
			m_State = kState.Attack;
			m_fTime = cCharMob.CrossAnim(kAnimEnum.Mob_Attack, WrapMode.ClampForever, 0.3f, 1f, 0f);
			m_fTimeCount = 0f;
			break;
		case kState.Attack:
			m_fTimeCount += deltaTime;
			if (m_fTimeCount < m_fTime)
			{
				return kTreeRunStatus.Executing;
			}
			m_State = kState.Idle;
			m_fTime = 0.5f;
			m_fTimeCount = 0f;
			cCharMob.CrossAnim(kAnimEnum.Idle, WrapMode.Loop, 0.3f, 1f, 0f);
			cCharMob.m_TargetBuilding.AddHP(0f - mobInfo.fDamage);
			cCharMob.m_TargetBuilding.PlayAudio("Mat_Fence_crash");
			if (m_GameScene.m_TaskManager != null)
			{
				CTaskDefence cTaskDefence = m_GameScene.m_TaskManager.GetTask() as CTaskDefence;
				if (cTaskDefence != null)
				{
					cTaskDefence.AddDamage(0f - mobInfo.fDamage);
				}
			}
			break;
		}
		return kTreeRunStatus.Executing;
	}
}
