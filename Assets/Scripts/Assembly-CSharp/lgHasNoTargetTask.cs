using BehaviorTree;
using UnityEngine;

public class lgHasNoTargetTask : Task
{
	public lgHasNoTargetTask(Node node)
		: base(node)
	{
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null)
		{
			return kTreeRunStatus.Failture;
		}
		if (cCharMob.m_Target != null && !cCharMob.m_Target.isDead && !cCharMob.m_Target.isStealth)
		{
			return kTreeRunStatus.Failture;
		}
		return kTreeRunStatus.Success;
	}
}
