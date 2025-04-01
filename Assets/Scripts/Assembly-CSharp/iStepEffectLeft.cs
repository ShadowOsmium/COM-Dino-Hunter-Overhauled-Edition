using UnityEngine;

public class iStepEffectLeft : _iAnimEventGroundBase
{
	public int nPrefabID = -1;

	public void iStepEffectLeft_PlayEffect()
	{
		if (nPrefabID != -1)
		{
			PlayEffect(nPrefabID);
		}
	}

	protected override void TransformRefresh(GameObject o)
	{
		base.TransformRefresh(o);
		o.transform.forward = -base.transform.forward;
	}
}
