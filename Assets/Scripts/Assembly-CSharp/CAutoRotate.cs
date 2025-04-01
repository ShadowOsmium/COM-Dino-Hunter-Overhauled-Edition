using UnityEngine;

public class CAutoRotate
{
	protected bool m_bRotate;

	protected Transform m_Agent;

	protected Vector3 m_v3SrcDir;

	protected Vector3 m_v3DstDir;

	protected Vector3 m_v3DstDirSave;

	protected float m_fRotateRate;

	protected float m_fRotateSpeed;

	protected bool m_bWave;

	protected float m_fWaveAngle;

	protected float m_fSrcWave;

	protected float m_fDstWave;

	protected float m_fWaveRate;

	protected float m_fWaveSpeed;

	protected bool m_bWaveRecover;

	public bool isRotate
	{
		get
		{
			return m_bRotate;
		}
		set
		{
			m_bRotate = value;
		}
	}

	public void Initialize()
	{
		m_bRotate = false;
		m_bWaveRecover = false;
		m_bWave = false;
	}

	public void Update(float deltaTime)
	{
		if (m_Agent == null)
		{
			return;
		}
		if (m_bWave)
		{
			m_fWaveRate += m_fWaveSpeed * deltaTime;
			Vector3 eulerAngles = m_Agent.eulerAngles;
			eulerAngles.z = MyUtils.Lerp(m_fSrcWave, m_fDstWave, m_fWaveRate);
			m_Agent.eulerAngles = eulerAngles;
			if (m_fWaveRate >= 1f)
			{
				m_fWaveRate = 1f;
				m_bWave = false;
			}
		}
		if (!m_bRotate)
		{
			return;
		}
		m_fRotateRate += m_fRotateSpeed * deltaTime;
		m_Agent.forward = Vector3.Lerp(m_v3SrcDir, m_v3DstDir, m_fRotateRate);
		Vector3 eulerAngles2 = m_Agent.eulerAngles;
		eulerAngles2.z = MyUtils.Lerp(m_fSrcWave, m_fDstWave, m_fWaveRate);
		m_Agent.eulerAngles = eulerAngles2;
		if (m_fRotateRate >= 1f)
		{
			if (m_v3DstDirSave != Vector3.zero)
			{
				m_v3SrcDir = m_Agent.forward;
				m_v3DstDir = m_v3DstDirSave;
				m_v3DstDirSave = Vector3.zero;
				m_fRotateRate = 0f;
			}
			else
			{
				m_bRotate = false;
			}
		}
	}

	public void Rotate(Transform tf, Vector3 v3DstDir, float speed = 1f)
	{
		m_Agent = tf;
		m_v3SrcDir = m_Agent.forward;
		m_v3DstDir = v3DstDir.normalized;
		m_v3DstDirSave = Vector3.zero;
		m_bRotate = true;
		m_fRotateRate = 0f;
		m_fRotateSpeed = speed;
	}

	public void Wave(float fAngle, float speed)
	{
		if (!(m_Agent == null))
		{
			m_bWave = true;
			m_bWaveRecover = false;
			m_fSrcWave = m_Agent.eulerAngles.z;
			m_fDstWave = fAngle;
			if (m_fSrcWave > 180f && m_fDstWave < 180f)
			{
				m_fDstWave += 360f;
			}
			else if (m_fSrcWave < 180f && m_fDstWave > 180f)
			{
				m_fSrcWave += 360f;
			}
			m_fWaveSpeed = speed;
			m_fWaveRate = 0f;
		}
	}

	public bool isClose()
	{
		if (m_v3DstDirSave == Vector3.zero && Vector3.Dot(m_v3SrcDir, m_v3DstDir) > 0.9f)
		{
			return true;
		}
		return false;
	}

	public Vector3 GetDstDir()
	{
		if (m_v3DstDirSave != Vector3.zero)
		{
			return m_v3DstDirSave;
		}
		return m_v3DstDir;
	}

	public void FinishWave()
	{
		if (!(m_Agent == null))
		{
			m_bWave = false;
			m_bWaveRecover = false;
			Vector3 eulerAngles = m_Agent.eulerAngles;
			eulerAngles.z = 0f;
			m_Agent.eulerAngles = eulerAngles;
		}
	}
}
