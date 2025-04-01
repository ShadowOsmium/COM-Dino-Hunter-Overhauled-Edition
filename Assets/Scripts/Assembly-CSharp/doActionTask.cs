using BehaviorTree;
using UnityEngine;

public class doActionTask : Task
{
	protected float m_fActionTime;

	protected float m_fActionTimeCount;

	public doActionTask(Node node)
		: base(node)
	{
	}

	public override void OnEnter(Object inputParam)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (!(cCharMob == null) && cCharMob.m_NetAnim != 0)
		{
			m_fActionTime = cCharMob.CrossAnim(cCharMob.m_NetAnim, WrapMode.ClampForever, 0.3f, 1f, 0f);
			m_fActionTimeCount = 0f;
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null || cCharMob.m_NetAnim == kAnimEnum.None)
		{
			return kTreeRunStatus.Failture;
		}
		m_fActionTimeCount += deltaTime;
		if (m_fActionTimeCount < m_fActionTime)
		{
			return kTreeRunStatus.Executing;
		}
		cCharMob.m_NetAnim = kAnimEnum.None;
		return kTreeRunStatus.Success;
	}
}
