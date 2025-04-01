using BehaviorTree;

public class doHurtPlayerNode : UnSharedNode
{
	public doHurtPlayerNode()
	{
		m_Task = new doHurtPlayerTask(this);
	}
}
