using BehaviorTree;
using UnityEngine;

public class doMoveToTask : Task
{
	protected iGameSceneBase m_GameScene;

	protected float m_fSpeed;

	protected float m_fSpeedLast;

	protected bool m_bNeedNextPoint;

	protected Vector3 m_v3Dst;

	protected bool m_bRotateBody;

	protected Vector3 m_v3RotSrc;

	protected Vector3 m_v3RotDst;

	protected float m_fRotRate;

	protected bool m_bWaitRotateBody;

	protected float m_fMoveSpeedRate;

	public doMoveToTask(Node node, float fSpeed)
		: base(node)
	{
		m_fSpeed = fSpeed;
		m_fSpeedLast = fSpeed;
		m_bNeedNextPoint = true;
		m_bWaitRotateBody = false;
	}

	public override void OnEnter(Object inputParam)
	{
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null)
		{
			return;
		}
		cCharMob.SetCurTask(this);
		iGameData gameData = iGameApp.GetInstance().m_GameData;
		if (gameData != null)
		{
			CMobInfoLevel mobInfo = gameData.GetMobInfo(cCharMob.ID, cCharMob.Level);
			if (mobInfo != null)
			{
				m_fMoveSpeedRate = mobInfo.fMoveSpeedRate;
			}
		}
		if (!cCharMob.IsActionPlaying(kAnimEnum.MoveForward))
		{
			float num = m_fSpeed;
			if (m_fSpeed == 0f)
			{
				num = cCharMob.Property.GetValue(kProEnum.MoveSpeed);
			}
			m_fSpeedLast = num;
			cCharMob.CrossAnim(kAnimEnum.MoveForward, WrapMode.Loop, 0.3f, num * m_fMoveSpeedRate, 0f);
		}
		if (CGameNetManager.GetInstance().IsConnected() && CGameNetManager.GetInstance().IsRoomMaster())
		{
			CGameNetSender.GetInstance().SendMsg_MOB_MOVE(cCharMob.UID, cCharMob.m_ltPath[cCharMob.m_ltPath.Count - 1]);
		}
		m_bNeedNextPoint = true;
	}

	public override void OnExit(Object inputParam)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (!(cCharMob == null))
		{
			cCharMob.StopAction(kAnimEnum.TurnLeft);
			cCharMob.StopAction(kAnimEnum.TurnRight);
			cCharMob.m_bHasPurposePoint = false;
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null)
		{
			return kTreeRunStatus.Failture;
		}
		if (cCharMob.m_ltPath.Count < 1)
		{
			return kTreeRunStatus.Failture;
		}
		if (m_bNeedNextPoint)
		{
			m_bNeedNextPoint = false;
			if (cCharMob.m_ltPath.Count > 1 && Vector3.Distance(cCharMob.Pos, cCharMob.m_ltPath[0]) < 0.2f)
			{
				cCharMob.m_ltPath.RemoveAt(0);
			}
			m_v3Dst = cCharMob.m_ltPath[0];
			TurnRound(cCharMob, (m_v3Dst - cCharMob.Pos).normalized);
		}
		float num = m_fSpeed;
		if (m_fSpeed == 0f)
		{
			num = cCharMob.Property.GetValue(kProEnum.MoveSpeed);
		}
		if (num != m_fSpeedLast)
		{
			m_fSpeedLast = num;
			cCharMob.SetActionSpeed(kAnimEnum.MoveForward, num * m_fMoveSpeedRate);
		}
		if (m_bRotateBody)
		{
			m_fRotRate += num * 0.5f * deltaTime;
			Vector3 vector = Vector3.Lerp(m_v3RotSrc, m_v3RotDst, m_fRotRate);
			if (vector != Vector3.zero)
			{
				cCharMob.Dir2D = vector;
			}
			if (m_fRotRate >= 1f)
			{
				m_bRotateBody = false;
			}
			if (m_bRotateBody && m_bWaitRotateBody)
			{
				return kTreeRunStatus.Executing;
			}
		}
		if (!cCharMob.IsActionPlaying(kAnimEnum.MoveForward))
		{
			cCharMob.CrossAnim(kAnimEnum.MoveForward, WrapMode.Loop, 0.3f, num * m_fMoveSpeedRate, 0f);
		}
		Vector3 vector2 = m_v3Dst - cCharMob.Pos;
		float num2 = num * deltaTime;
		float magnitude = vector2.magnitude;
		if (num2 < magnitude)
		{
			cCharMob.Pos += vector2 / magnitude * num2;
		}
		else
		{
			cCharMob.Pos = m_v3Dst;
			m_bNeedNextPoint = true;
			cCharMob.m_ltPath.RemoveAt(0);
			if (cCharMob.m_ltPath.Count < 1)
			{
				return kTreeRunStatus.Success;
			}
		}
		return kTreeRunStatus.Executing;
	}

	protected void TurnRound(CCharBase charbase, Vector3 v3Forward)
	{
		if (v3Forward == Vector3.zero)
		{
			return;
		}
		m_bRotateBody = true;
		m_v3RotSrc = charbase.Dir2D;
		m_v3RotDst = v3Forward;
		m_fRotRate = 0f;
		m_bWaitRotateBody = false;
		float num = Vector3.Dot(m_v3RotSrc, m_v3RotDst);
		if (num >= 0.8f)
		{
			return;
		}
		CCharMob cCharMob = charbase as CCharMob;
		if (cCharMob == null)
		{
			return;
		}
		CMobInfoLevel mobInfo = cCharMob.GetMobInfo();
		if (mobInfo != null && mobInfo.isWaitRot)
		{
			m_bWaitRotateBody = true;
			if (Vector3.Cross(charbase.Dir2D, v3Forward).y > 0f)
			{
				charbase.CrossAnim(kAnimEnum.TurnRight, WrapMode.ClampForever, 0.3f, 1f, 0f);
			}
			else
			{
				charbase.CrossAnim(kAnimEnum.TurnLeft, WrapMode.ClampForever, 0.3f, 1f, 0f);
			}
		}
	}
}
