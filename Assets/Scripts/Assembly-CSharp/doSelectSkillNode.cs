using BehaviorTree;

public class doSelectSkillNode : UnSharedNode
{
	public doSelectSkillNode()
	{
		m_Task = new doSelectSkillTask(this);
	}
}
