using BehaviorTree;

public class doRandomHoverPosNode : UnSharedNode
{
	public doRandomHoverPosNode()
	{
		m_Task = new doRandomHoverPosTask(this);
	}
}
