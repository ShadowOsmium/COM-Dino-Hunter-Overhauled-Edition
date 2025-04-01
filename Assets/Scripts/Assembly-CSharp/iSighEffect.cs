using UnityEngine;

public class iSighEffect : _iAnimEventBase
{
	public void iSighEffect_PlayEffect(int nPrefab)
	{
		PlayEffect(nPrefab);
	}

	protected override void TransformRefresh(GameObject o)
	{
		base.TransformRefresh(o);
		Vector3 vector = m_Node.up + m_Node.right;
		o.transform.up = -vector;
		o.transform.position = o.transform.position + o.transform.up * 1.5f;
	}
}
