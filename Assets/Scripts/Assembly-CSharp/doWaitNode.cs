using BehaviorTree;

public class doWaitNode : Node
{
	public float m_fTime;

	public doWaitNode(float fTime)
	{
		m_fTime = fTime;
	}

	public override Task CreateTask()
	{
		return new doWaitTask(this, m_fTime);
	}
}
