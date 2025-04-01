using UnityEngine;

public class _iAnimEventFollowBase : _iAnimEventBase
{
	protected override void TransformRefresh(GameObject o)
	{
		if (!(m_Node == null))
		{
			o.transform.parent = m_Node;
			o.transform.localPosition = Vector3.zero;
			o.transform.localRotation = Quaternion.identity;
		}
	}
}
