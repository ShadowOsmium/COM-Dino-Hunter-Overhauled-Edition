using BehaviorTree;

public class ConditionNode_MaintainTimeNode : ConditionNode
{
	protected float m_fTime;

	public ConditionNode_MaintainTimeNode(float fTime)
	{
		m_fTime = fTime;
	}

	public override Task CreateTask()
	{
		return new ConditionNode_MaintainTimeTask(this, m_fTime);
	}
}
