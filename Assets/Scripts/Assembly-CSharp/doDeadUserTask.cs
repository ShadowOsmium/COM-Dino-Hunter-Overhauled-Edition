using BehaviorTree;
using UnityEngine;

public class doDeadUserTask : Task
{
	protected float m_fFloorHeight;

	protected bool m_bFall;

	protected float m_fSpeedYInt;

	protected float m_fSpeedYAcc = -20f;

	public doDeadUserTask(Node node)
		: base(node)
	{
	}

	public override void OnEnter(Object inputParam)
	{
		CCharPlayer cCharPlayer = inputParam as CCharPlayer;
		if (!(cCharPlayer == null))
		{
			cCharPlayer.SetCurTask(this);
			cCharPlayer.StopAction(cCharPlayer.CurMixAnim);
			cCharPlayer.CrossAnim(kAnimEnum.Death, WrapMode.ClampForever, 0.3f, 1f, 0f);
			Vector3 pos = cCharPlayer.Pos;
			pos.y += 100f;
			RaycastHit hitInfo;
			if (Physics.Raycast(new Ray(pos, Vector3.down), out hitInfo, 1000f, 536870912))
			{
				m_fFloorHeight = hitInfo.point.y;
			}
			if (cCharPlayer.Pos.y > m_fFloorHeight)
			{
				m_bFall = true;
			}
			cCharPlayer.RemoveAllBuff();
			cCharPlayer.ClearAllStatus();
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharPlayer cCharPlayer = inputParam as CCharPlayer;
		if (cCharPlayer == null)
		{
			return kTreeRunStatus.Failture;
		}
		if (m_bFall)
		{
			cCharPlayer.Pos += new Vector3(0f, m_fSpeedYInt * deltaTime, 0f);
			m_fSpeedYInt += m_fSpeedYAcc * deltaTime;
			if (cCharPlayer.Pos.y <= m_fFloorHeight)
			{
				cCharPlayer.Pos = new Vector3(cCharPlayer.Pos.x, m_fFloorHeight, cCharPlayer.Pos.z);
				m_bFall = false;
			}
		}
		return kTreeRunStatus.Executing;
	}
}
