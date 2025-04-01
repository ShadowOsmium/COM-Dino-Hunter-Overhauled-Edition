using BehaviorTree;

public class lgCheckHeightNode : UnSharedNode
{
	public lgCheckHeightNode()
	{
		m_Task = new lgCheckHeightTask(this);
	}
}
