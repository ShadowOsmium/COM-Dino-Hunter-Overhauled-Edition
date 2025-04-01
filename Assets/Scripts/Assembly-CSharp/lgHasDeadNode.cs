using BehaviorTree;

public class lgHasDeadNode : UnSharedNode
{
	public lgHasDeadNode()
	{
		m_Task = new lgHasDeadTask(this);
	}
}
