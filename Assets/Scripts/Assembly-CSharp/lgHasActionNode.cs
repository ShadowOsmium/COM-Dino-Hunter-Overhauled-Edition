using BehaviorTree;

public class lgHasActionNode : UnSharedNode
{
	public lgHasActionNode()
	{
		m_Task = new lgHasActionTask(this);
	}
}
