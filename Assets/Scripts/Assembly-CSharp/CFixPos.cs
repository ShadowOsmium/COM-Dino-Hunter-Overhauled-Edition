using UnityEngine;

public class CFixPos
{
	protected bool m_bFix;

	protected float m_fSpeed;

	protected Vector3 m_v3Src;

	protected Vector3 m_v3Dst;

	protected float m_fRate;

	public void LateUpdate(CCharBase charbase, float deltaTime)
	{
		if (m_bFix)
		{
			m_fRate += m_fSpeed * deltaTime;
			charbase.Pos = Vector3.Lerp(m_v3Src, m_v3Dst, m_fRate);
			if (m_fRate > 1f)
			{
				m_bFix = false;
			}
		}
	}

	public bool IsFixing()
	{
		return m_bFix;
	}

	public void FixPos(Vector3 v3Src, Vector3 v3Dst, float fSpeed = 1f)
	{
		m_v3Src = v3Src;
		m_v3Dst = v3Dst;
		m_bFix = true;
		m_fRate = 0f;
		m_fSpeed = fSpeed;
	}
}
