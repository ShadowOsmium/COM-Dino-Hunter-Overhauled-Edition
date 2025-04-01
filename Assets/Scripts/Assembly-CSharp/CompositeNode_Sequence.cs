using BehaviorTree;

public class CompositeNode_Sequence : CompositeNode
{
	public override Task CreateTask()
	{
		return new SequenceTask(this);
	}

	public override void DestroyTask(Task task)
	{
	}
}
