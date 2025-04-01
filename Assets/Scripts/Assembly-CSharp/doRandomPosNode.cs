using BehaviorTree;

public class doRandomPosNode : UnSharedNode
{
	public doRandomPosNode()
	{
		m_Task = new doRandomPosTask(this);
	}
}
