using UnityEngine;

public class iSpawnBullet_Parabola : iSpawnBullet
{
	protected Vector3 m_v3Src;

	protected Vector3 m_v3VelocityCur = Vector3.forward;

	protected float m_fTime;

	protected bool m_bFalling;

	public new void Awake()
	{
		base.Awake();
		m_bFalling = false;
	}

	public override void SetForce(Vector3 v3Force)
	{
		base.SetForce(v3Force);
		m_v3VelocityCur = m_v3VelocityBase;
		m_bFalling = v3Force.y <= 0f;
	}

	public override bool IsCanHitFloor()
	{
		return m_bFalling;
	}

	protected override void OnInit()
	{
		m_v3Src = m_Transform.position;
		m_Transform.forward = m_v3VelocityCur;
		m_v3VelocityCur = m_v3VelocityBase;
		m_fTime = 0f;
		m_bFalling = m_v3VelocityBase.y <= 0f || fGravity == 0f;
		m_bActive = true;
	}

	protected override void OnUpdate(float deltaTime)
	{
		if (!(m_Transform == null))
		{
			m_fTime += deltaTime;
			m_v3VelocityCur.y = m_v3VelocityBase.y - fGravity * m_fTime;
			Vector3 position = m_Transform.position;
			position = m_v3Src + m_v3VelocityCur * m_fTime;
			if (!m_bFalling && m_v3VelocityBase.y - fGravity * m_fTime <= 0f)
			{
				m_bFalling = true;
			}
			position.y = m_v3Src.y + m_v3VelocityBase.y * m_fTime - 0.5f * fGravity * m_fTime * m_fTime;
			if (v3RotateSpeed == Vector3.zero)
			{
				m_Transform.forward = position - m_Transform.position;
			}
			m_Transform.position = position;
		}
	}
}
