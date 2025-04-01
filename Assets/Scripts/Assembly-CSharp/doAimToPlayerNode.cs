using BehaviorTree;

public class doAimToPlayerNode : Node
{
	public override Task CreateTask()
	{
		return new doAimToPlayerTask(this);
	}
}
