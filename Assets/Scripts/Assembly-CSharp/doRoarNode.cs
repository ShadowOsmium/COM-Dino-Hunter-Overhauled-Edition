using BehaviorTree;

public class doRoarNode : Node
{
	public override Task CreateTask()
	{
		return new doRoarTask(this);
	}
}
