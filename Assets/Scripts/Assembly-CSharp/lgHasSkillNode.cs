using BehaviorTree;

public class lgHasSkillNode : UnSharedNode
{
	public lgHasSkillNode()
	{
		m_Task = new lgHasSkillTask(this);
	}
}
