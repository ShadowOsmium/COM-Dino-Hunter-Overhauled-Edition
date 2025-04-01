using BehaviorTree;

public class lgHasShowTimeNode : UnSharedNode
{
	public lgHasShowTimeNode()
	{
		m_Task = new lgHasShowTimeTask(this);
	}
}
