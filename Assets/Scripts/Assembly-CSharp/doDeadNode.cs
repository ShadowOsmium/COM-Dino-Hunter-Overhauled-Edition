using BehaviorTree;

public class doDeadNode : Node
{
	public override Task CreateTask()
	{
		return new doDeadTask(this);
	}
}
