namespace BehaviorTree
{
	public class ConditionNode : Node
	{
		protected Node m_Child;

		public void AddChild(Node node)
		{
			node.SetParent(this);
			m_Child = node;
		}

		public Node GetChild()
		{
			return m_Child;
		}
	}
}
