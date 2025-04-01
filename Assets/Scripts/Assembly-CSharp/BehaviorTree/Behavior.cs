using UnityEngine;

namespace BehaviorTree
{
	public class Behavior
	{
		protected Node m_Node;

		protected Task m_Task;

		protected kTreeRunStatus m_kStatus;

		public Behavior()
		{
			m_Node = null;
			m_Task = null;
			m_kStatus = kTreeRunStatus.Invalid;
		}

		public Behavior(Node node)
		{
			m_Task = null;
			m_kStatus = kTreeRunStatus.Invalid;
			Install(node);
		}

		public bool HasInstalled()
		{
			return m_Task != null;
		}

		public virtual void Install(Node node)
		{
			Uninstall();
			m_Node = node;
			m_Task = m_Node.CreateTask();
		}

		public void Uninstall()
		{
			if (m_Node != null && m_Task != null)
			{
				m_Node.DestroyTask(m_Task);
				m_Node = null;
				m_Task = null;
				m_kStatus = kTreeRunStatus.Invalid;
			}
		}

		public void Reset()
		{
			Install(m_Node);
		}

		public kTreeRunStatus Update(Object inputParam, float deltaTime)
		{
			if (m_kStatus == kTreeRunStatus.Invalid)
			{
				m_Task.OnEnter(inputParam);
			}
			kTreeRunStatus kTreeRunStatus2 = m_Task.OnUpdate(inputParam, deltaTime);
			if (kTreeRunStatus2 != kTreeRunStatus.Executing)
			{
				m_Task.OnExit(inputParam);
				m_kStatus = kTreeRunStatus.Invalid;
			}
			else
			{
				m_kStatus = kTreeRunStatus2;
			}
			return kTreeRunStatus2;
		}
	}
}
