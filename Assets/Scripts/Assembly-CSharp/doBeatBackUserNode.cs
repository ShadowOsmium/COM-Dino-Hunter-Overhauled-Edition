using BehaviorTree;

public class doBeatBackUserNode : Node
{
	public override Task CreateTask()
	{
		return new doBeatBackUserTask(this);
	}
}
