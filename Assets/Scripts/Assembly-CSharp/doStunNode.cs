using BehaviorTree;

public class doStunNode : UnSharedNode
{
	public doStunNode()
	{
		m_Task = new doStunTask(this);
	}
}
