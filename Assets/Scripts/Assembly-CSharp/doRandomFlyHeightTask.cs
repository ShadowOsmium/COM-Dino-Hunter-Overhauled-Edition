using BehaviorTree;
using UnityEngine;

public class doRandomFlyHeightTask : Task
{
	protected iGameSceneBase m_GameScene;

	public doRandomFlyHeightTask(Node node)
		: base(node)
	{
	}

	public override void OnEnter(Object inputParam)
	{
		m_GameScene = iGameApp.GetInstance().m_GameScene;
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null)
		{
			return kTreeRunStatus.Failture;
		}
		bool flag = false;
		for (int num = 5; num > 0; num--)
		{
			Vector3 vector = cCharMob.Pos + new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
			vector.y = m_GameScene.m_fNavPlane;
			Vector3 vector2 = vector - cCharMob.Pos;
			float magnitude = vector2.magnitude;
			if (!Physics.Raycast(cCharMob.Pos, vector2 / magnitude, magnitude, int.MinValue))
			{
				cCharMob.m_v3BirthPos = vector;
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			CCharUser user = m_GameScene.GetUser();
			if (user != null)
			{
				Vector3 vector3 = user.Pos - cCharMob.Pos;
				if (vector3 != Vector3.zero)
				{
					cCharMob.m_v3BirthPos = cCharMob.Pos + vector3.normalized * 10f;
				}
			}
		}
		return kTreeRunStatus.Success;
	}
}
