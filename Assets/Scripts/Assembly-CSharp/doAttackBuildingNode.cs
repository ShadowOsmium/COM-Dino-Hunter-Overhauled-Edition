using BehaviorTree;

public class doAttackBuildingNode : Node
{
	public override Task CreateTask()
	{
		return new doAttackBuildingTask(this);
	}
}
