using BehaviorTree;
using UnityEngine;

public class lgTypeCheckTask : Task
{
	protected GeometryType m_GeometryType;

	public lgTypeCheckTask(Node node, GeometryType type)
		: base(node)
	{
		m_GeometryType = type;
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		Geometry geometry = inputParam as Geometry;
		if (geometry == null)
		{
			return kTreeRunStatus.Failture;
		}
		if (geometry.Type == m_GeometryType)
		{
			return kTreeRunStatus.Success;
		}
		return kTreeRunStatus.Failture;
	}
}
