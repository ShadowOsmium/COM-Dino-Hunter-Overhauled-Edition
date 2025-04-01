using UnityEngine;

public class iCameraController : MonoBehaviour
{
	protected Transform m_Transform;

	protected Vector3 m_v3Position;

	protected Quaternion m_qtRotation;

	protected bool m_bShake;

	protected Vector3 m_v3ShakeOffset;

	protected float m_fShakeTime;

	protected float m_fShakeTimeCount;

	protected float m_fShakeRange;

	protected float m_fShakeRangeDamping;

	protected GameObject m_AudioLister;

	public Vector3 Position
	{
		get
		{
			return m_v3Position;
		}
		set
		{
			m_v3Position = value;
		}
	}

	public Quaternion Rotation
	{
		get
		{
			return m_qtRotation;
		}
		set
		{
			m_qtRotation = value;
		}
	}

	private void Awake()
	{
		m_Transform = base.transform;
		m_bShake = false;
		m_AudioLister = new GameObject("Main Camera Listener");
		m_AudioLister.AddComponent<AudioListener>();
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		if (m_bShake)
		{
			m_fShakeTimeCount += deltaTime;
			if (m_fShakeTimeCount > m_fShakeTime)
			{
				m_v3ShakeOffset = Vector3.zero;
				m_bShake = false;
			}
			else
			{
				m_v3ShakeOffset = Random.onUnitSphere * m_fShakeRange;
			}
		}
	}

	private void LateUpdate()
	{
		if (m_AudioLister.active)
		{
			m_AudioLister.transform.position = m_v3Position;
		}
		m_Transform.position = m_v3Position + m_v3ShakeOffset;
		m_Transform.rotation = m_qtRotation;
	}

	public bool IsShake()
	{
		return m_bShake;
	}

	public void Shake(float fShakeTime = 0.5f, float fShakeRange = 0.05f)
	{
		m_bShake = true;
		m_fShakeTime = fShakeTime;
		m_fShakeRange = fShakeRange;
		m_fShakeTimeCount = 0f;
	}

	public void ActiveListener(bool bActive)
	{
		if (!(m_AudioLister == null))
		{
			m_AudioLister.active = bActive;
		}
	}
}
