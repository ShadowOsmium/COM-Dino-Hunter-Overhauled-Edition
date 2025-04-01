using BehaviorTree;

public class lgTypeCheckNode : UnSharedNode
{
	public lgTypeCheckNode(GeometryType type)
	{
		m_Task = new lgTypeCheckTask(this, type);
	}
}
