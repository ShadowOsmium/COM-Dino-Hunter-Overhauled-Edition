using UnityEngine;

public class iStepEffectRight : _iAnimEventGroundBase
{
	public int nPrefabID = -1;

	public void iStepEffectRight_PlayEffect()
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
