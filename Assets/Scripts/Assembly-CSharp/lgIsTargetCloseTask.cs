using BehaviorTree;
using UnityEngine;

public class lgIsTargetCloseTask : Task
{
	public lgIsTargetCloseTask(Node node)
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
		if (cCharMob.m_Target == null)
		{
			return kTreeRunStatus.Failture;
		}
		CMobInfoLevel mobInfo = cCharMob.GetMobInfo();
		if (mobInfo == null)
		{
			return kTreeRunStatus.Failture;
		}
		if (Vector3.Distance(cCharMob.Pos, cCharMob.m_Target.Pos) <= mobInfo.fMeleeRange * 1.5f)
		{
			return kTreeRunStatus.Success;
		}
		return kTreeRunStatus.Failture;
	}
}
