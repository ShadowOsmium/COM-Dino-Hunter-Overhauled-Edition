using BehaviorTree;

public class doMoribundNode : Node
{
	public override Task CreateTask()
	{
		return new doMoribundTask(this);
	}
}
