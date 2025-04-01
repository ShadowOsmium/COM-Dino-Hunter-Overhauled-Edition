using BehaviorTree;

public class lgIsMoribundNode : UnSharedNode
{
	public lgIsMoribundNode()
	{
		m_Task = new lgIsMoribundTask(this);
	}
}
