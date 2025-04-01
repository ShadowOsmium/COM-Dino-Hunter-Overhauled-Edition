using BehaviorTree;
using UnityEngine;

public class doFlyHeightTask : Task
{
	protected float m_fSpeed;

	protected Vector3 m_v3Dst;

	protected bool m_bRotateBody;

	protected Vector3 m_v3RotSrc;

	protected Vector3 m_v3RotDst;

	protected float m_fRotRate;

	public doFlyHeightTask(Node node, float fSpeed)
		: base(node)
	{
		m_fSpeed = fSpeed;
	}

	public override void OnEnter(Object inputParam)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (!(cCharMob == null))
		{
			cCharMob.SetCurTask(this);
			m_v3Dst = cCharMob.m_v3BirthPos;
			TurnRound(cCharMob, m_v3Dst - cCharMob.Pos);
			cCharMob.CrossAnim(kAnimEnum.MoveForward, WrapMode.Loop, 0.3f, 1f, 0f);
		}
	}

	public override kTreeRunStatus OnUpdate(Object inputParam, float deltaTime)
	{
		CCharMob cCharMob = inputParam as CCharMob;
		if (cCharMob == null)
		{
			return kTreeRunStatus.Failture;
		}
		float num = m_fSpeed;
		if (num == 0f)
		{
			num = cCharMob.Property.GetValue(kProEnum.MoveSpeed);
		}
		if (m_bRotateBody)
		{
			m_fRotRate += num * 0.5f * deltaTime;
			Vector3 vector = Vector3.Lerp(m_v3RotSrc, m_v3RotDst, m_fRotRate);
			if (vector != Vector3.zero)
			{
				cCharMob.Dir3D = vector;
			}
			if (m_fRotRate >= 1f)
			{
				m_bRotateBody = false;
			}
		}
		Vector3 vector2 = m_v3Dst - cCharMob.Pos;
		float num2 = num * deltaTime;
		float magnitude = vector2.magnitude;
		if (num2 < magnitude)
		{
			cCharMob.Pos += vector2 / magnitude * num2;
			return kTreeRunStatus.Executing;
		}
		cCharMob.Pos = m_v3Dst;
		return kTreeRunStatus.Success;
	}

	protected void TurnRound(CCharBase charbase, Vector3 v3Forward)
	{
		m_bRotateBody = true;
		m_v3RotSrc = charbase.Dir2D;
		m_v3RotDst = v3Forward;
		m_fRotRate = 0f;
	}
}
