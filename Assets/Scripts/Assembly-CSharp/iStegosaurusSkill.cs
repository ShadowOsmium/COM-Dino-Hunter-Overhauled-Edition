using UnityEngine;

public class iStegosaurusSkill : _iAnimEventGroundBase
{
	public void iStegosaurusSkill_PlayEffect(int nPrefabID)
	{
		PlayEffect(nPrefabID);
	}

	protected override void TransformRefresh(GameObject o)
	{
		base.TransformRefresh(o);
		o.transform.forward = base.transform.forward;
	}
}
