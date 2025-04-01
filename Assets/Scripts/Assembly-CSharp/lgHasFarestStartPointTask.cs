using BehaviorTree;
using UnityEngine;

public class lgHasFarestStartPointTask : Task
{
	public lgHasFarestStartPointTask(Node node)
		: base(node)
	{
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null || !cCharMob.m_bHasPurposePoint)
		{
			return kTreeRunStatus.Failture;
		}
		return kTreeRunStatus.Success;
	}
}
