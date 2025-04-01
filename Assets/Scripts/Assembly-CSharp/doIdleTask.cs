using BehaviorTree;
using UnityEngine;

public class doIdleTask : Task
{
	protected float m_fTime;

	protected float m_fTimeCount;

	public doIdleTask(Node node, float fTime)
		: base(node)
	{
		m_fTime = fTime;
		m_fTimeCount = 0f;
	}

	public override void OnEnter(Object inputParam)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (!(cCharMob == null))
		{
			cCharMob.SetCurTask(this);
			if (!cCharMob.IsActionPlaying(kAnimEnum.Idle))
			{
				cCharMob.CrossAnim(kAnimEnum.Idle, WrapMode.Loop, 0.3f, 1f, 0f);
			}
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		m_fTimeCount += deltaTime;
		if (m_fTimeCount < m_fTime)
		{
			return kTreeRunStatus.Executing;
		}
		return kTreeRunStatus.Success;
	}
}
