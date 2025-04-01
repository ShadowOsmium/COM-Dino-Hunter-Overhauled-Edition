using BehaviorTree;

public class doMoveToNode : Node
{
	protected float m_fSpeed;

	public doMoveToNode()
	{
		m_fSpeed = 0f;
	}

	public doMoveToNode(float fSpeed)
	{
		m_fSpeed = fSpeed;
	}

	public override Task CreateTask()
	{
		return new doMoveToTask(this, m_fSpeed);
	}
}
