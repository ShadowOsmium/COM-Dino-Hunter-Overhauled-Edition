using BehaviorTree;

public class doRandomPointNode : UnSharedNode
{
	public doRandomPointNode()
	{
		m_Task = new doRandomPointTask(this);
	}
}
