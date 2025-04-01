using BehaviorTree;
using UnityEngine;

public class doAimToPlayerTask : Task
{
	protected Vector3 m_v3DirSrc;

	protected Vector3 m_v3DirDst;

	protected Vector3 m_v3AimPointCur;

	protected Vector3 m_v3AimPointSrc;

	protected Vector3 m_v3AimPointDst;

	protected float m_fRate;

	protected float m_fSpeed;

	public doAimToPlayerTask(Node node)
		: base(node)
	{
	}

	public override void OnEnter(Object inputParam)
	{
		CCharPlayer cCharPlayer = inputParam as CCharPlayer;
		if (!(cCharPlayer == null))
		{
			m_v3DirSrc = cCharPlayer.Dir2D;
			m_v3DirDst = cCharPlayer.m_v3NetAimPoint - cCharPlayer.Pos;
			m_v3DirDst.y = 0f;
			m_v3DirDst.Normalize();
			m_v3AimPointSrc = m_v3AimPointCur;
			m_v3AimPointDst = cCharPlayer.m_v3NetAimPoint - cCharPlayer.GetBone(2).position;
			m_fRate = 0f;
			m_fSpeed = 5f;
			cCharPlayer.m_bNetAimFresh = false;
		}
	}

	public override void OnExit(Object inputParam)
	{
		base.OnExit(inputParam);
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharPlayer cCharPlayer = inputParam as CCharPlayer;
		if (cCharPlayer == null)
		{
			return kTreeRunStatus.Failture;
		}
		if (cCharPlayer.m_bNetAimFresh)
		{
			m_v3DirSrc = cCharPlayer.Dir2D;
			m_v3DirDst = cCharPlayer.m_v3NetAimPoint - cCharPlayer.Pos;
			m_v3DirDst.y = 0f;
			m_v3DirDst.Normalize();
			m_v3AimPointSrc = m_v3AimPointCur;
			m_v3AimPointDst = cCharPlayer.m_v3NetAimPoint - cCharPlayer.GetBone(2).position;
			m_fRate = 0f;
			m_fSpeed = 5f;
			cCharPlayer.m_bNetAimFresh = false;
		}
		if (m_fRate < 1f)
		{
			m_fRate += m_fSpeed * deltaTime;
			cCharPlayer.Dir2D = Vector3.Lerp(m_v3DirSrc, m_v3DirDst, m_fRate);
			m_v3AimPointCur = Vector3.Lerp(m_v3AimPointSrc, m_v3AimPointDst, m_fRate);
			cCharPlayer.m_v3CurNetAimDir = m_v3AimPointCur;
			cCharPlayer.UpdateUpBody(m_v3AimPointCur);
			cCharPlayer.ShootDir = m_v3AimPointCur;
		}
		return kTreeRunStatus.Executing;
	}
}
