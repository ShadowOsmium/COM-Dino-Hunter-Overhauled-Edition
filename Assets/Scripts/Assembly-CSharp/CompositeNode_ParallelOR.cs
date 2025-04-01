using BehaviorTree;

public class CompositeNode_ParallelOR : CompositeNode
{
	public override Task CreateTask()
	{
		return new ParallelORTask(this);
	}

	public override void DestroyTask(Task task)
	{
	}
}
