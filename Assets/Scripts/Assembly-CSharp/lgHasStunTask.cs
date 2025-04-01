using BehaviorTree;
using UnityEngine;

public class lgHasStunTask : Task
{
	public lgHasStunTask(Node node)
		: base(node)
	{
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharBase cCharBase = inputParam as CCharBase;
		if (cCharBase == null || !cCharBase.isStun)
		{
			return kTreeRunStatus.Failture;
		}
		return kTreeRunStatus.Success;
	}
}
