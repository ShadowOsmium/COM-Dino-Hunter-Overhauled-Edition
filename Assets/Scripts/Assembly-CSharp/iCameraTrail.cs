using UnityEngine;

public class iCameraTrail : iCamera
{
	public CCharBase m_Target;

	public Vector3 camera_offset_normal;

	public Vector3 camera_offset_shoot;

	public Vector3 camera_offset_melee;

	public Vector3 camera_offset_block;

	public Vector3 camera_lookat;

	protected AudioListener m_AudioListenerCamera;

	protected AudioListener m_AudioListenerTarget;

	protected Vector3 m_v3Camera_Offset_Near;

	protected Vector3 m_v3Camera_Offset_Far;

	protected Vector3 m_v3Camera_Offset_Cur;

	protected float m_fSmoothSpeed;

	protected float m_fCurCameraDis;

	protected float m_fMaxCameraDis;

	protected float m_fSrcYaw;

	protected float m_fDstYaw;

	protected float m_fSrcPitch;

	protected float m_fDstPitch;

	protected float m_fRateYaw;

	protected float m_fRatePitch;

	public new void Awake()
	{
		base.Awake();
		m_AudioListenerCamera = GetComponent<AudioListener>();
		m_AudioListenerCamera.enabled = false;
		m_fSrcYaw = 0f;
		m_fDstYaw = m_fSrcYaw;
		m_fSrcPitch = 0f;
		m_fDstPitch = m_fSrcPitch;
		m_fRateYaw = 1f;
		m_fRatePitch = 1f;
		base.enabled = false;
		m_CameraController.enabled = false;
		base.enabled = true;
		m_CameraController.enabled = true;
	}

	public void Initialize(CCharBase target, bool bMeleeView = false)
	{
		if (m_AudioListenerTarget == null)
		{
			m_AudioListenerTarget = target.gameObject.AddComponent<AudioListener>();
		}
		SwitchToTargetListener();
		ShootMode(false);
		SetViewMelee(bMeleeView);
		SetPitch(28f);
		m_v3Camera_Offset_Near = camera_offset_block;
		m_v3Camera_Offset_Cur = camera_offset_normal;
		m_Target = target;
		m_fMaxCameraDis = Vector3.Distance(camera_offset_normal, camera_offset_block);
		m_fCurCameraDis = m_fMaxCameraDis;
		m_fSmoothSpeed = 1f;
		Quaternion quaternion = Quaternion.Euler(0f - m_fPitch, m_fYaw, 0f);
		m_CameraController.Position = m_Target.Pos + quaternion * camera_offset_normal;
	}

	public void ResetOffset()
	{
		m_v3Camera_Offset_Near = camera_offset_block;
		m_v3Camera_Offset_Far = camera_offset_normal;
	}

	public void SetOffset(Vector3 near, Vector3 far)
	{
		m_v3Camera_Offset_Near = near;
		m_v3Camera_Offset_Far = far;
	}

	public void Destroy()
	{
		m_Target = null;
	}

	public new void LateUpdate()
	{
		if (!m_bActive || m_Target == null)
		{
			return;
		}
		if (m_fRateYaw < 1f)
		{
			m_fYaw = MyUtils.Lerp(m_fYaw, m_fDstYaw, m_fRateYaw);
			m_fRateYaw += 2f * Time.deltaTime;
		}
		if (m_fRatePitch < 1f)
		{
			m_fPitch = MyUtils.Lerp(m_fPitch, m_fDstPitch, m_fRatePitch);
			m_fRatePitch += 2f * Time.deltaTime;
		}
		Quaternion quaternion = Quaternion.Euler(0f - m_fPitch, m_fYaw, 0f);
		m_v3Camera_Offset_Cur = Vector3.Lerp(m_v3Camera_Offset_Cur, m_v3Camera_Offset_Far, m_fSmoothSpeed * Time.deltaTime);
		m_fMaxCameraDis = Vector3.Distance(m_v3Camera_Offset_Cur, m_v3Camera_Offset_Near);
		Vector3 vector = m_Target.Pos + quaternion * camera_lookat;
		Vector3 vector2 = m_Target.Pos + quaternion * m_v3Camera_Offset_Near;
		Vector3 vector3 = m_Target.Pos + quaternion * m_v3Camera_Offset_Cur;
		float num = Vector3.Distance(vector2, vector3);
		Vector3 direction = (vector3 - vector2) / num;
		RaycastHit hitInfo;
		if (Physics.Raycast(vector2, direction, out hitInfo, num + 0.3f, -1610612736))
		{
			m_fCurCameraDis = Vector3.Distance(vector2, hitInfo.point) - 0.3f;
		}
		else
		{
			m_fCurCameraDis += m_fSmoothSpeed * Time.deltaTime;
			if (m_fCurCameraDis > m_fMaxCameraDis)
			{
				m_fCurCameraDis = m_fMaxCameraDis;
			}
		}
		Vector3 vector4 = Vector3.Lerp(vector2, vector3, m_fCurCameraDis / m_fMaxCameraDis);
		Vector3 forward = base.transform.forward;
		base.transform.forward = vector - vector4;
		m_CameraController.Rotation = base.transform.rotation;
		m_CameraController.Position = vector4;
	}

	public void Yaw(float angle)
	{
		m_fDstYaw += angle;
		if (MyUtils.LimitAngle(ref m_fDstYaw, m_fYawMin, m_fYawMax))
		{
			m_fYaw = m_fDstYaw;
			m_fRateYaw = 1f;
		}
		else
		{
			m_fRateYaw = 0f;
		}
	}

	public void SetYaw(float angle)
	{
		m_fDstYaw = angle;
		if (MyUtils.LimitAngle(ref m_fDstYaw, m_fYawMin, m_fYawMax))
		{
			m_fYaw = m_fDstYaw;
			m_fRateYaw = 1f;
		}
		else
		{
			m_fRateYaw = 0f;
		}
	}

	public void Pitch(float angle)
	{
		m_fDstPitch += angle;
		MyUtils.LimitAngle(ref m_fDstPitch, m_fPitchMin, m_fPitchMax);
		m_fPitch = m_fDstPitch;
		m_fRatePitch = 1f;
	}

	public void SetPitch(float angle)
	{
		m_fDstPitch = angle;
		MyUtils.LimitAngle(ref m_fDstPitch, m_fPitchMin, m_fPitchMax);
		m_fPitch = m_fDstPitch;
		m_fRatePitch = 1f;
	}

	public float GetYaw()
	{
		return m_fDstYaw;
	}

	public float GetPitch()
	{
		return m_fDstPitch;
	}

	public void SetViewMelee(bool on)
	{
		m_fSmoothSpeed = 5f;
		if (on)
		{
			m_v3Camera_Offset_Far = camera_offset_melee;
		}
		else
		{
			m_v3Camera_Offset_Far = camera_offset_normal;
		}
	}

	public void ShootMode(bool on)
	{
		if (on)
		{
			m_fSmoothSpeed = 8f;
			m_v3Camera_Offset_Far = camera_offset_shoot;
		}
		else
		{
			m_fSmoothSpeed = 5f;
			m_v3Camera_Offset_Far = camera_offset_normal;
		}
	}

	public void SwitchToTargetListener()
	{
		if (m_AudioListenerTarget != null)
		{
			m_AudioListenerTarget.enabled = true;
		}
		m_CameraController.ActiveListener(false);
	}

	public void SwitchToCameraListener()
	{
		if (m_AudioListenerTarget != null)
		{
			m_AudioListenerTarget.enabled = false;
		}
		m_CameraController.ActiveListener(true);
	}
}
