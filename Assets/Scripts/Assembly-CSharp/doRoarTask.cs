using BehaviorTree;
using UnityEngine;

public class doRoarTask : Task
{
	protected float m_fTime;

	protected float m_fTimeCount;

	public doRoarTask(Node node)
		: base(node)
	{
	}

	public override void OnEnter(Object inputParam)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (!(cCharMob == null))
		{
			m_fTime = cCharMob.CrossAnim(kAnimEnum.Mob_Roar, WrapMode.ClampForever, 0.3f, 1f, 0f);
			m_fTimeCount = 0f;
		}
	}

	public override void OnExit(Object inputParam)
	{
		base.OnExit(inputParam);
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null)
		{
			return kTreeRunStatus.Failture;
		}
		if (cCharMob.m_Target != null)
		{
			cCharMob.Dir2D = Vector3.Lerp(cCharMob.Dir2D, (cCharMob.m_Target.Pos - cCharMob.Pos).normalized, m_fTimeCount / m_fTime);
		}
		m_fTimeCount += deltaTime;
		if (m_fTimeCount < m_fTime)
		{
			return kTreeRunStatus.Executing;
		}
		return kTreeRunStatus.Success;
	}
}
