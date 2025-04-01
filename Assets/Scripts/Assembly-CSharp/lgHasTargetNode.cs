using BehaviorTree;

public class lgHasTargetNode : UnSharedNode
{
	public lgHasTargetNode()
	{
		m_Task = new lgHasTargetTask(this);
	}
}
