using BehaviorTree;
using UnityEngine;

public class lgHasTargetBuildingTask : Task
{
	public lgHasTargetBuildingTask(Node node)
		: base(node)
	{
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null || cCharMob.m_TargetBuilding == null)
		{
			return kTreeRunStatus.Failture;
		}
		return kTreeRunStatus.Failture;
	}
}
