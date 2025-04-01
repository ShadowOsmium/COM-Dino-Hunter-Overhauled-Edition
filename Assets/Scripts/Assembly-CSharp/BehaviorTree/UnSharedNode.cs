namespace BehaviorTree
{
	public class UnSharedNode : Node
	{
		protected Task m_Task;

		public override Task CreateTask()
		{
			return m_Task;
		}

		public override void DestroyTask(Task task)
		{
		}
	}
}
