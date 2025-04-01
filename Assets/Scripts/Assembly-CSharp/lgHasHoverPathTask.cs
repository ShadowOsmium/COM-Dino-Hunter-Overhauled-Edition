using BehaviorTree;
using UnityEngine;

public class lgHasHoverPathTask : Task
{
	public lgHasHoverPathTask(Node node)
		: base(node)
	{
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null)
		{
			return kTreeRunStatus.Failture;
		}
		if (cCharMob.m_ltPathHover.Count < 1 && cCharMob.m_v3DstHoverPoint == Vector3.zero)
		{
			return kTreeRunStatus.Failture;
		}
		if (cCharMob.m_v3DstHoverPoint == Vector3.zero)
		{
			cCharMob.m_v3DstHoverPoint = cCharMob.m_ltPathHover[0];
			cCharMob.m_ltPathHover.RemoveAt(0);
		}
		return kTreeRunStatus.Success;
	}
}
