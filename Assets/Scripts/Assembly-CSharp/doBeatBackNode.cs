using BehaviorTree;

public class doBeatBackNode : Node
{
	public override Task CreateTask()
	{
		return new doBeatBackTask(this);
	}
}
