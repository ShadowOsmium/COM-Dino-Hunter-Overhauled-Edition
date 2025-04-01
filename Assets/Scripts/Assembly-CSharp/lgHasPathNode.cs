using BehaviorTree;

public class lgHasPathNode : UnSharedNode
{
	public lgHasPathNode()
	{
		m_Task = new lgHasPathTask(this);
	}
}
