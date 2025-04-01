using BehaviorTree;

public class lgHasPurposePointNode : UnSharedNode
{
	public lgHasPurposePointNode()
	{
		m_Task = new lgHasPurposePointTask(this);
	}
}
