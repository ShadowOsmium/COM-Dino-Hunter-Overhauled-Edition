using UnityEngine;

namespace BehaviorTree
{
	public class Task
	{
		protected Node m_Node;

		public Task(Node node)
		{
			m_Node = node;
		}

		public virtual void OnEnter(Object inputParam)
		{
		}

		public virtual kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
		{
			return kTreeRunStatus.Invalid;
		}

		public virtual void OnExit(Object inputParam)
		{
		}
	}
}
