using BehaviorTree;

public class doIdleNode : Node
{
	protected float m_fTime;

	public doIdleNode(float fTime)
	{
		m_fTime = fTime;
	}

	public override Task CreateTask()
	{
		return new doIdleTask(this, m_fTime);
	}
}
