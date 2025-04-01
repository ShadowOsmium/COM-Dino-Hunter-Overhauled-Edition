using BehaviorTree;

public class doRandomFlyHeightNode : UnSharedNode
{
	public doRandomFlyHeightNode()
	{
		m_Task = new doRandomFlyHeightTask(this);
	}
}
