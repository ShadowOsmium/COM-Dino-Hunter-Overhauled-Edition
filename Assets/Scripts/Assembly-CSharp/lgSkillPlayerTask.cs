using BehaviorTree;
using UnityEngine;

public class lgSkillPlayerTask : Task
{
	public lgSkillPlayerTask(Node node)
		: base(node)
	{
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharPlayer cCharPlayer = inputParam as CCharPlayer;
		if (cCharPlayer == null || !cCharPlayer.m_bNetSkill)
		{
			return kTreeRunStatus.Failture;
		}
		return kTreeRunStatus.Success;
	}
}
