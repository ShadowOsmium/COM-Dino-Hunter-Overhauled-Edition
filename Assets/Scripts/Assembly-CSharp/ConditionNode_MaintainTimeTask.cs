using BehaviorTree;
using UnityEngine;

public class ConditionNode_MaintainTimeTask : Task
{
	protected float m_fTime;

	protected float m_fTimeCount;

	protected Behavior m_Behavior;

	public ConditionNode_MaintainTimeTask(Node node, float fTime)
		: base(node)
	{
		m_fTime = fTime;
		m_fTimeCount = 0f;
		m_Behavior = new Behavior();
	}

	public ConditionNode GetConditionNode()
	{
		return (ConditionNode)m_Node;
	}

	public override void OnEnter(Object inputParam)
	{
	}

	public override void OnExit(Object inputParam)
	{
		m_Behavior.Uninstall();
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		ConditionNode conditionNode = GetConditionNode();
		Node child = conditionNode.GetChild();
		if (child == null)
		{
			return BehaviorTree.kTreeRunStatus.Failture;
		}
		if (!m_Behavior.HasInstalled())
		{
			m_Behavior.Install(child);
		}
		kTreeRunStatus kTreeRunStatus = m_Behavior.Update(inputParam, deltaTime);
		if (kTreeRunStatus == kTreeRunStatus.Executing)
		{
			m_fTimeCount += deltaTime;
			if (m_fTimeCount >= m_fTime)
			{
				return kTreeRunStatus.Success;
			}
		}
		return kTreeRunStatus;
	}
}
