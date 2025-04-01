using BehaviorTree;

public class doSelectTargetNode : UnSharedNode
{
	public doSelectTargetNode()
	{
		m_Task = new doSelectTargetTask(this);
	}
}
