using System.Collections.Generic;

namespace BehaviorTree
{
	public class CompositeNode : Node
	{
		protected List<Node> m_ChildrenNode;

		public CompositeNode()
		{
			m_ChildrenNode = new List<Node>();
		}

		public void AddChild(Node node)
		{
			node.SetParent(this);
			m_ChildrenNode.Add(node);
		}

		public Node GetChild(int nIndex)
		{
			if (nIndex < 0 || nIndex >= m_ChildrenNode.Count)
			{
				return null;
			}
			return m_ChildrenNode[nIndex];
		}

		public int GetChildCount()
		{
			return m_ChildrenNode.Count;
		}
	}
}
