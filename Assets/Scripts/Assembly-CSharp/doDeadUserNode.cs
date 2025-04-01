using BehaviorTree;

public class doDeadUserNode : Node
{
	public override Task CreateTask()
	{
		return new doDeadUserTask(this);
	}
}
