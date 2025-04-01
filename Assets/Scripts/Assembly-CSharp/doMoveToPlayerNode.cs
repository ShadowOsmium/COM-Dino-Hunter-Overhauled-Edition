using BehaviorTree;

public class doMoveToPlayerNode : Node
{
	public override Task CreateTask()
	{
		return new doMoveToPlayerTask(this);
	}
}
