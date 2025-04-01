using BehaviorTree;

public class lgIsTargetCloseNode : UnSharedNode
{
	public lgIsTargetCloseNode()
	{
		m_Task = new lgIsTargetCloseTask(this);
	}
}
