using BehaviorTree;
using UnityEngine;

public class doHurtUserTask : Task
{
	protected float m_fTime;

	protected float m_fTimeCount;

	public doHurtUserTask(Node node)
		: base(node)
	{
	}

	public override void OnEnter(Object inputParam)
	{
		CCharUser cCharUser = inputParam as CCharUser;
		if (!(cCharUser == null))
		{
			m_fTime = cCharUser.CrossAnimMix(kAnimEnum.Hurt, WrapMode.Once, 0.1f, 1f);
			m_fTimeCount = 0f;
			cCharUser.PlayAudio(kAudioEnum.Hurt);
		}
	}

	public override void OnExit(Object inputParam)
	{
		CCharUser cCharUser = inputParam as CCharUser;
		if (!(cCharUser == null))
		{
			cCharUser.PauseFire(false);
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharUser cCharUser = inputParam as CCharUser;
		if (cCharUser == null)
		{
			return kTreeRunStatus.Failture;
		}
		m_fTimeCount += deltaTime;
		if (m_fTimeCount < m_fTime)
		{
			return kTreeRunStatus.Executing;
		}
		cCharUser.m_bHurting = false;
		return kTreeRunStatus.Success;
	}
}
