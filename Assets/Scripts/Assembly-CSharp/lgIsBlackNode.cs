using BehaviorTree;

public class lgIsBlackNode : UnSharedNode
{
	public lgIsBlackNode()
	{
		m_Task = new lgIsBlackTask(this);
	}
}
