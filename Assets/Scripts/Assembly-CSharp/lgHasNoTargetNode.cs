using BehaviorTree;

public class lgHasNoTargetNode : UnSharedNode
{
	public lgHasNoTargetNode()
	{
		m_Task = new lgHasNoTargetTask(this);
	}
}
