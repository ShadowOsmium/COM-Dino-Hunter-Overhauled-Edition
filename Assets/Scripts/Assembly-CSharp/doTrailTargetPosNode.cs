using BehaviorTree;

public class doTrailTargetPosNode : UnSharedNode
{
	public doTrailTargetPosNode()
	{
		m_Task = new doTrailTargetPosTask(this);
	}
}
