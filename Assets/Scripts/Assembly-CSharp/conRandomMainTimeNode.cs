using BehaviorTree;

public class conRandomMainTimeNode : ConditionNode
{
	protected float m_fMin;

	protected float m_fMax;

	public conRandomMainTimeNode(float min, float max)
	{
		m_fMin = min;
		m_fMax = max;
	}

	public override Task CreateTask()
	{
		return new conRandomMainTimeTask(this, m_fMin, m_fMax);
	}
}
