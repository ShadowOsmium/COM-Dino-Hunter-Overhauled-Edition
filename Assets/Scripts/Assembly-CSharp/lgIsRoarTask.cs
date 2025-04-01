using BehaviorTree;
using UnityEngine;

public class lgIsRoarTask : Task
{
	protected float m_fRoarRate;

	public lgIsRoarTask(Node node, float fRoarRate)
		: base(node)
	{
		m_fRoarRate = fRoarRate;
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null || cCharMob.GetActionLen(kAnimEnum.Mob_Roar) == 0f)
		{
			return kTreeRunStatus.Failture;
		}
		if (cCharMob.m_Target == null)
		{
			return kTreeRunStatus.Failture;
		}
		Vector3 lhs = cCharMob.m_Target.Pos - cCharMob.Pos;
		lhs.Normalize();
		if (Vector3.Dot(lhs, cCharMob.Dir2D) < 0.7f)
		{
			return kTreeRunStatus.Failture;
		}
		if ((float)Random.Range(1, 100) < m_fRoarRate)
		{
			return kTreeRunStatus.Success;
		}
		return kTreeRunStatus.Failture;
	}
}
