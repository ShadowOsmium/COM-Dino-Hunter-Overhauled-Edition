using BehaviorTree;
using UnityEngine;

public class lgShootPlayerTask : Task
{
	public lgShootPlayerTask(Node node)
		: base(node)
	{
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharPlayer cCharPlayer = inputParam as CCharPlayer;
		if (cCharPlayer == null || !cCharPlayer.m_bNetShoot)
		{
			return kTreeRunStatus.Failture;
		}
		return kTreeRunStatus.Success;
	}
}
