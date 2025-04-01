using BehaviorTree;
using UnityEngine;

public class doTrailTargetPosTask : Task
{
	protected iGameSceneBase m_GameScene;

	protected UnityEngine.AI.NavMeshPath m_NavPath;

	public doTrailTargetPosTask(Node node)
		: base(node)
	{
		m_NavPath = new UnityEngine.AI.NavMeshPath();
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
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null)
		{
			return kTreeRunStatus.Failture;
		}
		if (cCharMob.m_Target == null)
		{
			return kTreeRunStatus.Failture;
		}
		if (!UnityEngine.AI.NavMesh.CalculatePath(cCharMob.Pos, cCharMob.m_Target.Pos, -1, m_NavPath))
		{
			return kTreeRunStatus.Failture;
		}
		CMobInfoLevel mobInfo = cCharMob.GetMobInfo();
		if (mobInfo == null)
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
		if (cCharMob.m_ltPath.Count == 1)
		{
			Vector3 vector = cCharMob.m_Target.Pos - cCharMob.Pos;
			vector.y = 0f;
			vector.Normalize();
			cCharMob.m_ltPath[0] = cCharMob.m_Target.Pos - vector * mobInfo.fMeleeRange;
		}
		else
		{
			int num = cCharMob.m_ltPath.Count - 1;
			if (Vector3.Distance(cCharMob.m_ltPath[num], cCharMob.m_Target.Pos) < mobInfo.fMeleeRange)
			{
				Vector3 vector2 = cCharMob.m_ltPath[num - 1] - cCharMob.m_ltPath[num];
				vector2.y = 0f;
				vector2.Normalize();
				cCharMob.m_ltPath[num] = cCharMob.m_Target.Pos + vector2 * mobInfo.fMeleeRange;
			}
		}
		return kTreeRunStatus.Success;
	}
}
