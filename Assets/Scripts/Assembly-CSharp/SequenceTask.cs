using BehaviorTree;
using UnityEngine;

public class SequenceTask : Task
{
	protected int m_nCurChild;

	protected Behavior m_curBehavior;

	public SequenceTask(Node node)
		: base(node)
	{
		m_nCurChild = 0;
		m_curBehavior = new Behavior();
	}

	private CompositeNode GetCompositeNode()
	{
		return (CompositeNode)m_Node;
	}

	public override void OnEnter(Object inputParam)
	{
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CompositeNode compositeNode = GetCompositeNode();
		if (compositeNode.GetChildCount() == 0)
		{
			return BehaviorTree.kTreeRunStatus.Failture;
		}
		if (!m_curBehavior.HasInstalled())
		{
			m_nCurChild = 0;
			m_curBehavior.Install(compositeNode.GetChild(m_nCurChild));
		}
		kTreeRunStatus kTreeRunStatus;
		while (true)
		{
			kTreeRunStatus = m_curBehavior.Update(inputParam, deltaTime);
			if (kTreeRunStatus != kTreeRunStatus.Success)
			{
				break;
			}
			m_nCurChild++;
			if (m_nCurChild >= compositeNode.GetChildCount())
			{
				return kTreeRunStatus;
			}
			m_curBehavior.Install(compositeNode.GetChild(m_nCurChild));
		}
		return kTreeRunStatus;
	}

	public override void OnExit(Object inputParam)
	{
		m_curBehavior.Uninstall();
	}
}
