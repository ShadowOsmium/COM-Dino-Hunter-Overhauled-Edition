using UnityEngine;

public class iSpawnBullet_Twist : iSpawnBullet
{
	public float fTwistWidth = 5f;

	public float fTwistSpeed = 1f;

	protected Vector3 m_v3CurPos;

	protected float m_fTwistMin;

	protected float m_fTwistMax;

	protected float m_fTwistCur;

	protected float m_fTwistSpeedCur;

	protected int m_nDir;

	protected override void OnInit()
	{
		base.OnInit();
		m_v3CurPos = m_Transform.position;
		m_fTwistMax = fTwistWidth / 2f;
		m_fTwistMin = 0f - m_fTwistMax;
		m_fTwistCur = 0f;
		m_fTwistSpeedCur = fTwistSpeed * (1f - Mathf.Abs(m_fTwistCur) / m_fTwistMax);
		m_nDir = 1;
	}

	protected override void OnUpdate(float deltaTime)
	{
		if (!(m_Transform == null))
		{
			m_fTwistCur += m_fTwistSpeedCur * (float)m_nDir * deltaTime;
			m_fTwistSpeedCur = fTwistSpeed * Mathf.Clamp(1f - Mathf.Abs(m_fTwistCur) / m_fTwistMax, 0.3f, 1f);
			if (m_fTwistCur > m_fTwistMax)
			{
				m_fTwistCur = m_fTwistMax;
				m_nDir = -1;
			}
			else if (m_fTwistCur < m_fTwistMin)
			{
				m_fTwistCur = m_fTwistMin;
				m_nDir = 1;
			}
			m_v3CurPos += m_v3VelocityBase * Time.deltaTime;
			m_Transform.position = m_v3CurPos + m_Transform.right * m_fTwistCur;
		}
	}
}
