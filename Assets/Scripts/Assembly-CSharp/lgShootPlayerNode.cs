using BehaviorTree;

public class lgShootPlayerNode : UnSharedNode
{
	public lgShootPlayerNode()
	{
		m_Task = new lgShootPlayerTask(this);
	}
}
