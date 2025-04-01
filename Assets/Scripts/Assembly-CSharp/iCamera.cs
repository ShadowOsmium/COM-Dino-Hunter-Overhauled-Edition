using UnityEngine;

[RequireComponent(typeof(iCameraController))]
public class iCamera : MonoBehaviour
{
	public bool m_bActive;

	protected kCameraMode m_Mode;

	protected iCameraController m_CameraController;

	protected float m_fYaw;

	protected float m_fPitch;

	protected float m_fPitchMin;

	protected float m_fPitchMax;

	protected float m_fYawMin;

	protected float m_fYawMax;

	public bool Active
	{
		get
		{
			return m_bActive;
		}
		set
		{
			m_bActive = value;
		}
	}

	public void Awake()
	{
		m_Mode = kCameraMode.None;
		m_CameraController = GetComponent<iCameraController>();
		m_bActive = false;
	}

	public void Start()
	{
	}

	public void Update()
	{
	}

	public void LateUpdate()
	{
	}

	public void SetRotateLimit(float pitchmin, float pitchmax, float yawmin, float yawmax)
	{
		m_fPitchMin = pitchmin;
		m_fPitchMax = pitchmax;
		m_fYawMin = yawmin;
		m_fYawMax = yawmax;
	}

	public Ray ScreenPointToRay(Vector2 v2Point, float dis = 0f)
	{
		return base.GetComponent<Camera>().ScreenPointToRay(new Vector3(v2Point.x, v2Point.y, dis));
	}

	public iCameraController GetCameraController()
	{
		return m_CameraController;
	}
}
