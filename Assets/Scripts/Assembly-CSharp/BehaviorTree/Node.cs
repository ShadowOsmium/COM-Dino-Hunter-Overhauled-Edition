namespace BehaviorTree
{
	public class Node
	{
		protected Node m_Parent;

		public Node()
		{
			m_Parent = null;
		}

		public virtual Task CreateTask()
		{
			return null;
		}

		public virtual void DestroyTask(Task task)
		{
		}

		public void SetParent(Node parent)
		{
			m_Parent = parent;
		}
	}
}
