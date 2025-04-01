using BehaviorTree;

public class doSelectFarestPointNode : UnSharedNode
{
	public doSelectFarestPointNode()
	{
		m_Task = new doSelectFarestPointTask(this);
	}
}
