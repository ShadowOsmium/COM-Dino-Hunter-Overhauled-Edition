using BehaviorTree;
using UnityEngine;

public class doHurtPlayerTask : Task
{
	protected float m_fTime;

	protected float m_fTimeCount;

	public doHurtPlayerTask(Node node)
		: base(node)
	{
	}

	public override void OnEnter(Object inputParam)
	{
		CCharPlayer cCharPlayer = inputParam as CCharPlayer;
		if (!(cCharPlayer == null))
		{
			m_fTime = cCharPlayer.CrossAnimMix(kAnimEnum.Hurt, WrapMode.Once, 0.1f, 1f);
			m_fTimeCount = 0f;
			cCharPlayer.PlayAudio(kAudioEnum.Hurt);
		}
	}

	public override void OnExit(Object inputParam)
	{
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharPlayer cCharPlayer = inputParam as CCharPlayer;
		if (cCharPlayer == null)
		{
			return kTreeRunStatus.Failture;
		}
		m_fTimeCount += deltaTime;
		if (m_fTimeCount < m_fTime)
		{
			return kTreeRunStatus.Executing;
		}
		cCharPlayer.m_bHurting = false;
		return kTreeRunStatus.Success;
	}
}
