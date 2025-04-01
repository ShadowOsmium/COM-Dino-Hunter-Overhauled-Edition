using BehaviorTree;

public class doFlyHeightNode : Node
{
	protected float m_fSpeed;

	public doFlyHeightNode()
	{
		m_fSpeed = 0f;
	}

	public doFlyHeightNode(float fSpeed)
	{
		m_fSpeed = fSpeed;
	}

	public override Task CreateTask()
	{
		return new doFlyHeightTask(this, m_fSpeed);
	}
}
