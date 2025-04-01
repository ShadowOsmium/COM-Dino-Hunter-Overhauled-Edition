using BehaviorTree;

public class lgHasHoverPathNode : UnSharedNode
{
	public lgHasHoverPathNode()
	{
		m_Task = new lgHasHoverPathTask(this);
	}
}
