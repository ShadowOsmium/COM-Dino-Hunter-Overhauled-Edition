using UnityEngine;

public class iCameraReveal : iCamera
{
	public float fPitch;

	public float fDistance;

	public float fRotateSpeed;

	protected Transform mTransform;

	protected GameObject m_FocusTarget;

	protected float m_fRate;

	protected Vector3 m_v3RealPos;

	private new void Awake()
	{
		base.Awake();
		mTransform = base.transform;
	}

	private new void Start()
	{
	}

	private new void Update()
	{
		if (!m_bActive || m_FocusTarget == null)
		{
			return;
		}
		Vector3 vector = m_v3RealPos - m_FocusTarget.transform.position;
		vector = Quaternion.Euler(0f, 30f * Time.deltaTime, 0f) * vector;
		m_v3RealPos = m_FocusTarget.transform.position + vector;
		if (m_fRate < 1f)
		{
			m_fRate += Time.deltaTime;
			if (m_fRate > 1f)
			{
				m_fRate = 1f;
			}
		}
		m_CameraController.Position = Vector3.Lerp(mTransform.position, m_v3RealPos, m_fRate);
		mTransform.forward = m_FocusTarget.transform.position - m_CameraController.Position;
		m_CameraController.Rotation = mTransform.rotation;
	}

	public void Go(GameObject target, float fPitch, float fDistance, float fRotateSpeed)
	{
		m_bActive = true;
		m_FocusTarget = target;
		this.fPitch = fPitch;
		this.fDistance = fDistance;
		this.fRotateSpeed = fRotateSpeed;
		Vector3 vector = mTransform.position - m_FocusTarget.transform.position;
		vector.Normalize();
		vector.y = (Quaternion.Euler(0f - fPitch, 0f, 0f) * Vector3.forward).y;
		vector *= fDistance;
		m_v3RealPos = m_FocusTarget.transform.position + vector;
		if (m_v3RealPos.y <= 0f)
		{
			m_v3RealPos.y = 0.1f;
		}
		m_fRate = 0f;
	}

	public void Stop()
	{
		m_bActive = false;
	}
}
