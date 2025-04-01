using BehaviorTree;

public class doSelectTargetBuildingNode : Node
{
	public override Task CreateTask()
	{
		return new doSelectTargetBuildingTask(this);
	}
}
