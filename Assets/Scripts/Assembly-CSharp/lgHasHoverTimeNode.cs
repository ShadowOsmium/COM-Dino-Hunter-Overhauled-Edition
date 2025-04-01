using BehaviorTree;

public class lgHasHoverTimeNode : UnSharedNode
{
	public lgHasHoverTimeNode(float fTime)
	{
		m_Task = new lgHasHoverTimeTask(this, fTime);
	}
}
