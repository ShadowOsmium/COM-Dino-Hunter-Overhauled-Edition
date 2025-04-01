using BehaviorTree;
using UnityEngine;

public class doSelectFarestPointTask : Task
{
	protected iGameSceneBase m_GameScene;

	protected UnityEngine.AI.NavMeshPath m_NavPath;

	public doSelectFarestPointTask(Node node)
		: base(node)
	{
		m_NavPath = new UnityEngine.AI.NavMeshPath();
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
		CStartPointManager sPManagerGround = m_GameScene.GetSPManagerGround();
		if (sPManagerGround == null)
		{
			return kTreeRunStatus.Failture;
		}
		CStartPoint randomFarestPoint = sPManagerGround.GetRandomFarestPoint(cCharMob.Pos, 1);
		if (randomFarestPoint == null)
		{
			return kTreeRunStatus.Failture;
		}
		if (!UnityEngine.AI.NavMesh.CalculatePath(cCharMob.Pos, randomFarestPoint.v3Pos, -1, m_NavPath))
		{
			return kTreeRunStatus.Failture;
		}
		cCharMob.m_ltPath.Clear();
		for (int i = 0; i < m_NavPath.corners.Length; i++)
		{
			cCharMob.m_ltPath.Add(m_NavPath.corners[i]);
		}
		if (cCharMob.m_ltPath.Count == 0)
		{
			return kTreeRunStatus.Failture;
		}
		cCharMob.m_bHasPurposePoint = true;
		cCharMob.m_v3PurposePoint = randomFarestPoint.v3Pos;
		return kTreeRunStatus.Success;
	}
}
