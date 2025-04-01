using BehaviorTree;
using UnityEngine;

public class doMoveToPlayerTask : Task
{
	protected Vector3 m_v3Src;

	protected Vector3 m_v3Dst;

	protected float m_fRate;

	protected bool m_bNeedNextPoint;

	public doMoveToPlayerTask(Node node)
		: base(node)
	{
		m_bNeedNextPoint = true;
	}

	public override void OnEnter(Object inputParam)
	{
		CCharPlayer cCharPlayer = inputParam as CCharPlayer;
		if (!(cCharPlayer == null))
		{
		}
	}

	public override void OnExit(Object inputParam)
	{
		CCharPlayer cCharPlayer = inputParam as CCharPlayer;
		if (!(cCharPlayer == null))
		{
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharPlayer cCharPlayer = inputParam as CCharPlayer;
		if (cCharPlayer == null)
		{
			return kTreeRunStatus.Failture;
		}
		if (cCharPlayer.m_ltNetPath.Count < 1)
		{
			return kTreeRunStatus.Executing;
		}
		if (m_bNeedNextPoint)
		{
			m_bNeedNextPoint = false;
			m_v3Src = cCharPlayer.Pos;
			m_v3Dst = cCharPlayer.m_ltNetPath[0];
			m_fRate = 0f;
		}
		m_fRate += 5f * deltaTime;
		cCharPlayer.Pos = Vector3.Lerp(m_v3Src, m_v3Dst, m_fRate);
		cCharPlayer.UpdateMoveAnim((m_v3Dst - m_v3Src).normalized, cCharPlayer.ShootDir);
		if (m_fRate >= 1f)
		{
			cCharPlayer.Pos = m_v3Dst;
			m_bNeedNextPoint = true;
			cCharPlayer.m_ltNetPath.RemoveAt(0);
			if (cCharPlayer.m_ltNetPath.Count < 1 && cCharPlayer.m_bFinalPath)
			{
				cCharPlayer.StopMoveAnim();
				return kTreeRunStatus.Success;
			}
		}
		return kTreeRunStatus.Executing;
	}
}
