using UnityEngine;

public class iBillBoard : MonoBehaviour
{
	public bool x = true;

	public bool y = true;

	public bool z = true;

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
		Vector3 forward = Camera.main.transform.position - m_Transform.position;
		if (!x)
		{
			forward.x = 0f;
		}
		if (!y)
		{
			forward.y = 0f;
		}
		if (!z)
		{
			forward.z = 0f;
		}
		m_Transform.forward = forward;
	}
}
