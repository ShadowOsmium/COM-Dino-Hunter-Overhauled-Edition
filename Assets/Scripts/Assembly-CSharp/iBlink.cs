using UnityEngine;

public class iBlink : _iAnimEventGroundBase
{
	public void iBlink_PlayEffect(int nPrefabID)
	{
		PlayEffect(1407);
		PlayEffect(nPrefabID);
	}

	protected override void TransformRefresh(GameObject o)
	{
		base.TransformRefresh(o);
		o.transform.forward = base.transform.forward;
	}
}
