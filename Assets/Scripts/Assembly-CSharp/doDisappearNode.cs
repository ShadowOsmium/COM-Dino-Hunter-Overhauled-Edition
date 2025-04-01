using BehaviorTree;

public class doDisappearNode : UnSharedNode
{
	public doDisappearNode()
	{
		m_Task = new doDisappearTask(this);
	}
}
