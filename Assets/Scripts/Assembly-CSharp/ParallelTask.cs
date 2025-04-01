using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class ParallelTask : Task
{
	protected List<Behavior> m_BehaviorList;

	public ParallelTask(Node node)
		: base(node)
	{
		m_BehaviorList = new List<Behavior>();
	}

	private CompositeNode GetCompositeNode()
	{
		return (CompositeNode)m_Node;
	}

	public override void OnEnter(Object inputParam)
	{
		CompositeNode compositeNode = GetCompositeNode();
		for (int i = 0; i < compositeNode.GetChildCount(); i++)
		{
			Behavior behavior = new Behavior();
			behavior.Install(compositeNode.GetChild(i));
			m_BehaviorList.Add(behavior);
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		int num = 0;
		kTreeRunStatus result = kTreeRunStatus.Executing;
		for (int i = 0; i < m_BehaviorList.Count; i++)
		{
			kTreeRunStatus kTreeRunStatus = m_BehaviorList[i].Update(inputParam, deltaTime);
			if (kTreeRunStatus != kTreeRunStatus.Executing)
			{
				num++;
				result = kTreeRunStatus;
			}
		}
		if (num == m_BehaviorList.Count)
		{
			return result;
		}
		return kTreeRunStatus.Executing;
	}

	public override void OnExit(Object inputParam)
	{
		for (int i = 0; i < m_BehaviorList.Count; i++)
		{
			m_BehaviorList[i].Uninstall();
		}
		m_BehaviorList.Clear();
	}
}
