using UnityEngine;

public class iRotate : MonoBehaviour
{
	public float m_fSpeed = 1f;

	public bool x;

	public bool y = true;

	public bool z;

	protected Transform m_Transform;

	private void Awake()
	{
		m_Transform = base.transform;
	}

	private void Start()
	{
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		Vector3 eulerAngles = m_Transform.eulerAngles;
		if (x)
		{
			eulerAngles.x += m_fSpeed * deltaTime;
		}
		if (y)
		{
			eulerAngles.y += m_fSpeed * deltaTime;
		}
		if (z)
		{
			eulerAngles.z += m_fSpeed * deltaTime;
		}
		m_Transform.eulerAngles = eulerAngles;
	}
}
