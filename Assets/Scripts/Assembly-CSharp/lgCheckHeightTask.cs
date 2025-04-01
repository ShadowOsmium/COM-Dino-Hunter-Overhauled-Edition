using BehaviorTree;
using UnityEngine;

public class lgCheckHeightTask : Task
{
	protected iGameSceneBase m_GameScene;

	public lgCheckHeightTask(Node node)
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
		if (!m_GameScene.IsSkyScene())
		{
			return kTreeRunStatus.Failture;
		}
		float num = cCharMob.Pos.y - m_GameScene.m_fNavPlane;
		if (num < -0.2f || num > 0.2f)
		{
			return kTreeRunStatus.Success;
		}
		return kTreeRunStatus.Failture;
	}
}
