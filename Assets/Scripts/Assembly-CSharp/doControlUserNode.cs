using BehaviorTree;

public class doControlUserNode : UnSharedNode
{
	public doControlUserNode()
	{
		m_Task = new doControlUserTask(this);
	}
}
