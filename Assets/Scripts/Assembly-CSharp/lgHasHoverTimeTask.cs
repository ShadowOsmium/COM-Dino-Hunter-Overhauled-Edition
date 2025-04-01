using BehaviorTree;
using UnityEngine;

public class lgHasHoverTimeTask : Task
{
	protected float m_fTime;

	public lgHasHoverTimeTask(Node node, float fTime)
		: base(node)
	{
		m_fTime = fTime;
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null)
		{
			return kTreeRunStatus.Failture;
		}
		if (cCharMob.m_fHoverTime >= m_fTime)
		{
			return kTreeRunStatus.Success;
		}
		return kTreeRunStatus.Failture;
	}
}
