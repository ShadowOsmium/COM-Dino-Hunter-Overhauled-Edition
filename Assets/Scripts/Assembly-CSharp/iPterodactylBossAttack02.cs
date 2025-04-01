using UnityEngine;

public class iPterodactylBossAttack02 : _iAnimEventGroundBase
{
	public void iPterodactylBossAttack02_PlayEffect(int nPrefabID)
	{
		PlayEffect(nPrefabID);
	}

	protected override void TransformRefresh(GameObject o)
	{
		base.TransformRefresh(o);
		o.transform.forward = base.transform.forward;
	}
}
