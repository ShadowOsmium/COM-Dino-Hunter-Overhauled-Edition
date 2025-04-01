using BehaviorTree;
using UnityEngine;

public class doDisappearTask : Task
{
	protected iGameSceneBase m_GameScene;

	public doDisappearTask(Node node)
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
		m_GameScene.RemoveMob(cCharMob);
		return kTreeRunStatus.Success;
	}
}
