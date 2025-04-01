using BehaviorTree;

public class lgHasTargetBuildingNode : UnSharedNode
{
	public lgHasTargetBuildingNode()
	{
		m_Task = new lgHasTargetBuildingTask(this);
	}
}
