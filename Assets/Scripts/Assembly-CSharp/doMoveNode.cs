using BehaviorTree;

public class doMoveNode : Node
{
	public override Task CreateTask()
	{
		return new doMoveTask(this);
	}
}
