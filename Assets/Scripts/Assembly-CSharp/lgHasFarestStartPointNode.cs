using BehaviorTree;

public class lgHasFarestStartPointNode : UnSharedNode
{
	public lgHasFarestStartPointNode()
	{
		m_Task = new lgHasFarestStartPointTask(this);
	}
}
