using BehaviorTree;
using UnityEngine;

public class doRandomPointTask : Task
{
	public doRandomPointTask(Node node)
		: base(node)
	{
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		Geometry geometry = inputParam as Geometry;
		if (geometry == null)
		{
			return kTreeRunStatus.Failture;
		}
		geometry.v3DstPoint = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
		return kTreeRunStatus.Success;
	}
}
