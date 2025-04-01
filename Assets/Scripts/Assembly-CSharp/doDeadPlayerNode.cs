using BehaviorTree;

public class doDeadPlayerNode : Node
{
	public override Task CreateTask()
	{
		return new doDeadPlayerTask(this);
	}
}
