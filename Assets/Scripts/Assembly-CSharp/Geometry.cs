using BehaviorTree;
using UnityEngine;

public class Geometry : MonoBehaviour
{
	public GeometryType Type;

	public Vector3 v3DstPoint;

	public float fSpeed = 1f;

	public Behavior m_Behavior;

	public Vector3 v3Pos
	{
		get
		{
			return base.transform.position;
		}
		set
		{
			base.transform.position = value;
		}
	}

	public Vector3 v3Dir
	{
		get
		{
			return base.transform.forward;
		}
		set
		{
			value.y = 0f;
			base.transform.forward = value;
		}
	}

	public void SetAI(Node node)
	{
		m_Behavior = new Behavior();
		m_Behavior.Install(node);
	}

	private void Update()
	{
		if (m_Behavior != null)
		{
			m_Behavior.Update(this, Time.deltaTime);
		}
	}
}
