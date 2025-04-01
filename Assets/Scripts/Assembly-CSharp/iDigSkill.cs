using UnityEngine;

public class iDigSkill : _iAnimEventGroundBase
{
	public void iDigSkill_PlayEffect(int nPrefabID)
	{
		PlayEffect(nPrefabID);
	}

	protected override void TransformRefresh(GameObject o)
	{
		base.TransformRefresh(o);
		o.transform.forward = base.transform.forward;
	}
}
