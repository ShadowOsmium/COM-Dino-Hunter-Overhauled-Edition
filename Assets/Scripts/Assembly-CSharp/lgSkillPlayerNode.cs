using BehaviorTree;

public class lgSkillPlayerNode : UnSharedNode
{
	public lgSkillPlayerNode()
	{
		m_Task = new lgSkillPlayerTask(this);
	}
}
