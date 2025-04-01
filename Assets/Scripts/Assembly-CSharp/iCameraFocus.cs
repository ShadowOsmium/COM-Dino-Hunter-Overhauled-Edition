using UnityEngine;

public class iCameraFocus : iCamera
{
	public float fDistance;

	public float fTime;

	protected Transform mTransform;

	protected GameObject m_FocusTarget;

	protected float m_fRate;

	protected float m_fSpeed;

	protected Vector3 m_v3DstForward;

	protected Vector3 m_v3DstPosition;

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
		if (!m_bActive)
		{
			return;
		}
		if (m_FocusTarget != null)
		{
			m_v3DstForward = m_FocusTarget.transform.position - mTransform.position;
			m_v3DstForward.Normalize();
			m_v3DstPosition = m_FocusTarget.transform.position - m_v3DstForward * fDistance;
			RaycastHit hitInfo;
			if (Physics.Raycast(m_v3DstPosition + Vector3.up * 5f, Vector3.down, out hitInfo, 10f, 536870912))
			{
				m_v3DstPosition = hitInfo.point + Vector3.up;
			}
		}
		if (m_fRate < 1f)
		{
			m_fRate += m_fSpeed * Time.deltaTime;
			if (m_fRate > 1f)
			{
				m_fRate = 1f;
			}
		}
		m_CameraController.Position = Vector3.Lerp(m_CameraController.Position, m_v3DstPosition, m_fRate);
		mTransform.forward = Vector3.Lerp(mTransform.forward, m_v3DstForward, m_fRate);
		m_CameraController.Rotation = mTransform.rotation;
	}

	public void Go(GameObject target, float fDistance, float fTime)
	{
		m_bActive = true;
		m_FocusTarget = target;
		this.fDistance = fDistance;
		this.fTime = fTime;
		m_fSpeed = 1f / fTime;
		m_fRate = 0f;
		m_v3DstForward = m_FocusTarget.transform.position - mTransform.position;
		m_v3DstForward.Normalize();
		m_v3DstPosition = m_FocusTarget.transform.position - m_v3DstForward * fDistance;
	}

	public void Stop()
	{
		m_bActive = false;
	}
}
