using BehaviorTree;

public class lgHasStunNode : UnSharedNode
{
	public lgHasStunNode()
	{
		m_Task = new lgHasStunTask(this);
	}
}
