using UnityEngine;

public class _iAnimEventGroundBase : _iAnimEventBase
{
	protected override void TransformRefresh(GameObject o)
	{
		if (!(m_Node == null))
		{
			Vector3 position = m_Node.position;
			RaycastHit hitInfo;
			if (Physics.Raycast(new Ray(m_Node.position + new Vector3(0f, 50f, 0f), Vector3.down), out hitInfo, 100f, 536870912))
			{
				position.y = hitInfo.point.y + 0.01f;
			}
			o.transform.position = position;
		}
	}
}
