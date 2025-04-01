using BehaviorTree;
using UnityEngine;

public class conRandomMainTimeTask : ConditionNode_MaintainTimeTask
{
	protected float m_fMin;

	protected float m_fMax;

	public conRandomMainTimeTask(Node node, float min, float max)
		: base(node, 0f)
	{
		m_fMin = min;
		m_fMax = max;
	}

	public override void OnEnter(Object inputParam)
	{
		m_fTime = Random.Range(m_fMin, m_fMax);
	}
}
