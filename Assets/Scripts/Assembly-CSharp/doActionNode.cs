using BehaviorTree;

public class doActionNode : Node
{
	public override Task CreateTask()
	{
		return new doShowTimeTask(this);
	}
}
