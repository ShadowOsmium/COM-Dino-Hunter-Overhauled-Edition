using BehaviorTree;
using UnityEngine;

public class lgIsFreezeTask : Task
{
	public lgIsFreezeTask(Node node)
		: base(node)
	{
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null || !cCharMob.m_bFreeze)
		{
			return kTreeRunStatus.Failture;
		}
		return kTreeRunStatus.Success;
	}
}
