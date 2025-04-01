using BehaviorTree;

public class lgIsBumpingNode : UnSharedNode
{
	public lgIsBumpingNode()
	{
		m_Task = new lgIsBumpingTask(this);
	}
}
