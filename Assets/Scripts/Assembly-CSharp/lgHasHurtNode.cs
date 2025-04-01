using BehaviorTree;

public class lgHasHurtNode : UnSharedNode
{
	public lgHasHurtNode()
	{
		m_Task = new lgHasHurtTask(this);
	}
}
