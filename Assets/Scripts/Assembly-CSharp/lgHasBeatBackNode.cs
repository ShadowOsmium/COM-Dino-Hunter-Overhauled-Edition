using BehaviorTree;

public class lgHasBeatBackNode : UnSharedNode
{
	public lgHasBeatBackNode()
	{
		m_Task = new lgHasBeatBackTask(this);
	}
}
