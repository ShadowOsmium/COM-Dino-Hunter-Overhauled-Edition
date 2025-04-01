using BehaviorTree;

public class doPurposePointNode : UnSharedNode
{
	public doPurposePointNode()
	{
		m_Task = new doPurposePointTask(this);
	}
}
