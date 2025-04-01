using BehaviorTree;

public class lgIsFreezeNode : UnSharedNode
{
	public lgIsFreezeNode()
	{
		m_Task = new lgIsFreezeTask(this);
	}
}
