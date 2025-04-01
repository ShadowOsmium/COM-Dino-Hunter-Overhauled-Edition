using UnityEngine;

public class CSlipAssistant
{
	public float m_fCurFrameYaw;

	public float m_fCurFramePitch;

	protected float m_fTransTime;

	protected float m_fCurRateSpeed;

	protected float m_fCurRate;

	protected float m_fResetTime;

	protected float m_fResetTimeCount;

	protected float m_fLastSlipTime;

	protected float m_fResetDistance;

	public void Init(float fTransTime, float fResetTime, float fResetDistance)
	{
		m_fTransTime = fTransTime;
		m_fResetTime = fResetTime;
		m_fResetDistance = fResetDistance;
		m_fCurRateSpeed = 1f / m_fTransTime;
	}

	public bool Slip(Vector2 v2Delta)
	{
		float deltaTime = Time.deltaTime;
		if (v2Delta.magnitude <= m_fResetDistance)
		{
			m_fResetTimeCount += deltaTime;
			if (m_fResetTimeCount >= m_fResetTime)
			{
				m_fCurRate = 0.2f;
			}
		}
		else
		{
			m_fResetTimeCount = 0f;
		}
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if (realtimeSinceStartup - m_fLastSlipTime > m_fResetTime)
		{
			m_fCurRate = 0f;
		}
		m_fLastSlipTime = realtimeSinceStartup;
		if (m_fCurRate < 1f)
		{
			m_fCurRate += m_fCurRateSpeed * deltaTime;
			if (m_fCurRate > 1f)
			{
				m_fCurRate = 1f;
			}
		}
		float num = Mathf.Abs(v2Delta.x / (float)Screen.width);
		float num2 = Mathf.Abs(v2Delta.y / (float)Screen.height);
		m_fCurFrameYaw = m_fCurRate * num * 720f;
		m_fCurFramePitch = m_fCurRate * num2 * 120f;
		if (v2Delta.x < 0f)
		{
			m_fCurFrameYaw *= -1f;
		}
		if (v2Delta.y < 0f)
		{
			m_fCurFramePitch *= -1f;
		}
		return true;
	}
}
