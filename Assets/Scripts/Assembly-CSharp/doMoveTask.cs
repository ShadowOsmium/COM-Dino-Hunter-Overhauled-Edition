using BehaviorTree;
using UnityEngine;

public class doMoveTask : Task
{
	protected Vector3 m_v3Dst;

	public doMoveTask(Node node)
		: base(node)
	{
	}

	public override void OnEnter(Object inputParam)
	{
		Geometry geometry = inputParam as Geometry;
		if (!(geometry == null))
		{
			m_v3Dst = geometry.v3DstPoint;
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		Geometry geometry = inputParam as Geometry;
		if (geometry == null)
		{
			return kTreeRunStatus.Failture;
		}
		Vector3 vector = m_v3Dst - geometry.v3Pos;
		vector.y = 0f;
		float num = geometry.fSpeed * deltaTime;
		if (num * num < vector.sqrMagnitude)
		{
			geometry.v3Pos += vector.normalized * num;
			return kTreeRunStatus.Executing;
		}
		geometry.v3Pos = m_v3Dst;
		return kTreeRunStatus.Success;
	}
}
