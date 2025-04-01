using BehaviorTree;
using UnityEngine;

public class lgHasSkillTask : Task
{
	public lgHasSkillTask(Node node)
		: base(node)
	{
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null || cCharMob.m_pSkillComboInfo == null)
		{
			return kTreeRunStatus.Failture;
		}
		return kTreeRunStatus.Success;
	}
}
