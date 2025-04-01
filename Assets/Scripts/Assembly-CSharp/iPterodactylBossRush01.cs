using UnityEngine;

public class iPterodactylBossRush01 : _iAnimEventBase
{
	public void iPterodactylBossRush01_PlayEffect(int nPrefabID)
	{
		PlayEffect(nPrefabID);
	}

	protected override void TransformRefresh(GameObject o)
	{
		base.TransformRefresh(o);
		o.transform.forward = base.transform.root.forward;
	}
}
