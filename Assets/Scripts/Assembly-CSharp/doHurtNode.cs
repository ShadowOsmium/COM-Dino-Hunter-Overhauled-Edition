using BehaviorTree;

public class doHurtNode : Node
{
	public override Task CreateTask()
	{
		return new doHurtTask(this);
	}
}
