using BehaviorTree;
using UnityEngine;

public class lgHasPathTask : Task
{
	public lgHasPathTask(Node node)
		: base(node)
	{
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null || cCharMob.m_ltPath.Count < 1)
		{
			return kTreeRunStatus.Failture;
		}
		return kTreeRunStatus.Success;
	}
}
