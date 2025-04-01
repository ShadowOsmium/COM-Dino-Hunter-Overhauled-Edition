using BehaviorTree;
using UnityEngine;

public class lgHasBeatBackTask : Task
{
	public lgHasBeatBackTask(Node node)
		: base(node)
	{
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharBase cCharBase = inputParam as CCharBase;
		if (cCharBase == null || !cCharBase.m_bBeatBack)
		{
			return kTreeRunStatus.Failture;
		}
		return kTreeRunStatus.Success;
	}
}
