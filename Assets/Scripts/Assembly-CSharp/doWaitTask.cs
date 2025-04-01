using BehaviorTree;
using UnityEngine;

public class doWaitTask : Task
{
	protected float m_fTime;

	public doWaitTask(Node node, float fTime)
		: base(node)
	{
		m_fTime = fTime;
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		m_fTime -= deltaTime;
		if (m_fTime > 0f)
		{
			return kTreeRunStatus.Executing;
		}
		return kTreeRunStatus.Success;
	}
}
