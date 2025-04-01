using BehaviorTree;

public class doHoverToNode : Node
{
	public override Task CreateTask()
	{
		return new doHoverToTask(this);
	}
}
