using BehaviorTree;
using UnityEngine;

public class lgHasPurposePointTask : Task
{
	public lgHasPurposePointTask(Node node)
		: base(node)
	{
	}

	public override void OnEnter(Object inputParam)
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
