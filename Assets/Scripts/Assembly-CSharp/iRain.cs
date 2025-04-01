using UnityEngine;

public class iRain : MonoBehaviour
{
	public Vector3 offset;

	protected Transform m_Transform;

	protected Camera m_Camera;

	private void Awake()
	{
		m_Transform = base.transform;
		m_Camera = Camera.main;
	}

	private void Start()
	{
	}

	private void Update()
	{
		m_Transform.position = m_Camera.transform.position + offset;
	}
}
