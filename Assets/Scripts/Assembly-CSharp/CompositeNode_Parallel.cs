using BehaviorTree;

public class CompositeNode_Parallel : CompositeNode
{
	public override Task CreateTask()
	{
		return new ParallelTask(this);
	}

	public override void DestroyTask(Task task)
	{
	}
}
