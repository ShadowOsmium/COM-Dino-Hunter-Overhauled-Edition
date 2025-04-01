using BehaviorTree;

public class lgIsRoarNode : UnSharedNode
{
	public lgIsRoarNode(float fRoarRate)
	{
		m_Task = new lgIsRoarTask(this, fRoarRate);
	}
}
