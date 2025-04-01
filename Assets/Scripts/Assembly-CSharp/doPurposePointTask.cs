using BehaviorTree;
using UnityEngine;

public class doPurposePointTask : Task
{
	protected iGameSceneBase m_GameScene;

	protected UnityEngine.AI.NavMeshPath m_Path;

	public doPurposePointTask(Node node)
		: base(node)
	{
		m_Path = new UnityEngine.AI.NavMeshPath();
	}

	public override void OnEnter(Object inputParam)
	{
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		CCharMob cCharMob = inputParam as CCharMob;
		if (!(cCharMob == null))
		{
			cCharMob.SetCurTask(this);
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		if (m_GameScene == null)
		{
			return kTreeRunStatus.Failture;
		}
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null)
		{
			return kTreeRunStatus.Failture;
		}
		CStartPointManager tPManagerEnd = m_GameScene.GetTPManagerEnd();
		if (tPManagerEnd == null)
		{
			return kTreeRunStatus.Failture;
		}
		CStartPoint random = tPManagerEnd.GetRandom();
		if (random == null)
		{
			return kTreeRunStatus.Failture;
		}
		cCharMob.m_bHasPurposePoint = true;
		cCharMob.m_v3PurposePoint = random.GetRandom();
		if (!UnityEngine.AI.NavMesh.CalculatePath(cCharMob.Pos, cCharMob.m_v3PurposePoint, -1, m_Path))
		{
			return kTreeRunStatus.Failture;
		}
		cCharMob.m_ltPath.Clear();
		for (int i = 0; i < m_Path.corners.Length; i++)
		{
			cCharMob.m_ltPath.Add(m_Path.corners[i]);
		}
		return kTreeRunStatus.Success;
	}
}
