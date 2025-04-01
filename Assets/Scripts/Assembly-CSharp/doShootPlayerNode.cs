using BehaviorTree;

public class doShootPlayerNode : Node
{
	public override Task CreateTask()
	{
		return new doShootPlayerTask(this);
	}
}
