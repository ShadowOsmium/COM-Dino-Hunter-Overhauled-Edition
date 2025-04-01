using BehaviorTree;

public class doShowTimeNode : Node
{
	public override Task CreateTask()
	{
		return new doShowTimeTask(this);
	}
}
