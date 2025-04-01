using BehaviorTree;

public class doFreezeNode : Node
{
	public override Task CreateTask()
	{
		return new doFreezeTask(this);
	}
}
