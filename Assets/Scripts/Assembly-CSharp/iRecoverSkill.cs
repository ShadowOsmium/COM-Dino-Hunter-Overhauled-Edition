using UnityEngine;

public class iRecoverSkill : _iAnimEventBase
{
	public void iRecoverSkill_PlayEffect(int nPrefabID)
	{
		PlayEffect(nPrefabID);
	}

	protected override void TransformRefresh(GameObject o)
	{
		base.TransformRefresh(o);
		o.transform.forward = base.transform.forward;
	}
}
