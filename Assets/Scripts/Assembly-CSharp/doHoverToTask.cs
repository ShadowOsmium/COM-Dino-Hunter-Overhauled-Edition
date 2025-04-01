using BehaviorTree;
using UnityEngine;

public class doHoverToTask : Task
{
	protected enum kHoverState
	{
		Move,
		Hover
	}

	protected kHoverState m_HoverState;

	protected Vector3 m_v3DstHoverPoint;

	protected CAutoRotate m_AutoRot;

	protected float m_fRotSpeed;

	public doHoverToTask(Node node)
		: base(node)
	{
		m_AutoRot = new CAutoRotate();
		m_AutoRot.Initialize();
	}

	public override void OnEnter(Object inputParam)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (!(cCharMob == null))
		{
			cCharMob.SetCurTask(this);
			m_HoverState = kHoverState.Hover;
			m_v3DstHoverPoint = cCharMob.m_v3DstHoverPoint;
			Vector3 vector = m_v3DstHoverPoint - cCharMob.Pos;
			float magnitude = vector.magnitude;
			m_fRotSpeed = 1f / magnitude;
			if (m_fRotSpeed < 1f)
			{
				m_fRotSpeed = 1f;
			}
			m_AutoRot.Rotate(cCharMob.Transform, vector / magnitude, m_fRotSpeed);
			Vector3 lhs = Vector3.Cross(cCharMob.Dir3D, (m_v3DstHoverPoint - cCharMob.Pos).normalized);
			if (Vector3.Dot(lhs, Vector3.up) >= 0f)
			{
				m_AutoRot.Wave(330f, 2f);
			}
			else
			{
				m_AutoRot.Wave(30f, 2f);
			}
			cCharMob.m_fHoverTime = 0f;
			cCharMob.CrossAnim(kAnimEnum.Mob_Glide, WrapMode.Loop, 0.3f, 1f, 0f);
		}
	}

	public override void OnExit(Object inputParam)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (!(cCharMob == null))
		{
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null || cCharMob.Property == null)
		{
			return kTreeRunStatus.Failture;
		}
		if (Vector3.Distance(m_v3DstHoverPoint, cCharMob.Pos) >= 300f)
		{
			bool flag = false;
			iGameSceneBase gameScene = iGameApp.GetInstance().m_GameScene;
			if (gameScene != null)
			{
				CStartPointManager sPManagerSky = gameScene.GetSPManagerSky();
				if (sPManagerSky != null)
				{
					CStartPoint random = sPManagerSky.GetRandom();
					if (random != null)
					{
						cCharMob.Pos = random.GetRandom();
						cCharMob.Dir3D = Vector3.forward;
						flag = true;
					}
				}
			}
			if (!flag)
			{
				cCharMob.Pos = m_v3DstHoverPoint;
				cCharMob.Dir3D = Vector3.forward;
				cCharMob.m_v3DstHoverPoint = Vector3.zero;
			}
			cCharMob.ResetAI();
			return kTreeRunStatus.Success;
		}
		if (m_AutoRot != null)
		{
			m_AutoRot.Update(deltaTime);
		}
		cCharMob.m_fHoverTime += deltaTime;
		switch (m_HoverState)
		{
		case kHoverState.Move:
		{
			if (!m_AutoRot.isRotate && !cCharMob.IsActionPlaying(kAnimEnum.MoveForward))
			{
				cCharMob.CrossAnim(kAnimEnum.MoveForward, WrapMode.Loop, 0.3f, 1f, 0f);
			}
			float num = cCharMob.Property.GetValue(kProEnum.MoveSpeed) * deltaTime;
			if (num * num >= (m_v3DstHoverPoint - cCharMob.Pos).sqrMagnitude)
			{
				cCharMob.Pos = m_v3DstHoverPoint;
				cCharMob.m_v3DstHoverPoint = Vector3.zero;
				return kTreeRunStatus.Success;
			}
			cCharMob.Pos += m_AutoRot.GetDstDir() * num;
			break;
		}
		case kHoverState.Hover:
		{
			Vector3 vector = m_v3DstHoverPoint - cCharMob.Pos;
			if (vector == Vector3.zero)
			{
				cCharMob.m_v3DstHoverPoint = Vector3.zero;
				return kTreeRunStatus.Success;
			}
			if (m_AutoRot.isClose())
			{
				m_HoverState = kHoverState.Move;
				cCharMob.CrossAnim(kAnimEnum.MoveForward, WrapMode.Loop, 0.3f, 1f, 0f);
				m_AutoRot.Rotate(cCharMob.Transform, (m_v3DstHoverPoint - cCharMob.Pos).normalized, m_fRotSpeed);
				m_AutoRot.Wave(0f, 2f);
			}
			else
			{
				m_AutoRot.Rotate(cCharMob.Transform, (m_v3DstHoverPoint - cCharMob.Pos).normalized, m_fRotSpeed);
			}
			float num = cCharMob.Property.GetValue(kProEnum.MoveSpeed) * 0.7f * deltaTime;
			cCharMob.Pos += cCharMob.Dir3D * num;
			break;
		}
		}
		return kTreeRunStatus.Executing;
	}
}
