using BehaviorTree;

public class doHurtUserNode : UnSharedNode
{
	public doHurtUserNode()
	{
		m_Task = new doHurtUserTask(this);
	}
}
