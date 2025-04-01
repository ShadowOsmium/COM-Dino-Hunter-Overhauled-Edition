using BehaviorTree;

public class doUseSkillNode : Node
{
	public override Task CreateTask()
	{
		return new doUseSkillTask(this);
	}
}
