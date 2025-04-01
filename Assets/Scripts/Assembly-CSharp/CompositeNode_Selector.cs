using BehaviorTree;

public class CompositeNode_Selector : CompositeNode
{
	public override Task CreateTask()
	{
		return new SelectorTask(this);
	}

	public override void DestroyTask(Task task)
	{
	}
}
