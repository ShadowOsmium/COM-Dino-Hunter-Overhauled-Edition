using BehaviorTree;
using UnityEngine;

public class lgIsBlackTask : Task
{
	public lgIsBlackTask(Node node)
		: base(node)
	{
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharBoss cCharBoss = inputParam as CCharBoss;
		if (cCharBoss == null || !cCharBoss.m_bReadyToBlack)
		{
			return kTreeRunStatus.Failture;
		}
		return kTreeRunStatus.Success;
	}
}
