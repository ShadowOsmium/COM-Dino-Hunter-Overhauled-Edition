using BehaviorTree;
using UnityEngine;

public class lgIsBumpingTask : Task
{
	public lgIsBumpingTask(Node node)
		: base(node)
	{
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharBase cCharBase = inputParam as CCharBase;
		if (cCharBase == null || !cCharBase.m_bBumping)
		{
			return kTreeRunStatus.Failture;
		}
		return kTreeRunStatus.Success;
	}
}
