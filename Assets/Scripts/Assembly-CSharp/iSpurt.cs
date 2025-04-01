using UnityEngine;

public class iSpurt : _iAnimEventBase
{
	public void iSpurt_PlayEffect(int nPrefabID)
	{
		PlayEffect(nPrefabID);
	}

	protected override void TransformRefresh(GameObject o)
	{
		base.TransformRefresh(o);
		o.transform.forward = m_Node.up;
		o.transform.position = o.transform.position + o.transform.forward * 0.2f;
	}
}
