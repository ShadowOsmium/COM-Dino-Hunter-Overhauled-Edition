using BehaviorTree;

public class doSkillPlayerNode : Node
{
	public override Task CreateTask()
	{
		return new doSkillPlayerTask(this);
	}
}
