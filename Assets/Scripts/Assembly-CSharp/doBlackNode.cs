using BehaviorTree;

public class doBlackNode : Node
{
	public override Task CreateTask()
	{
		return new doBlackTask(this);
	}
}
