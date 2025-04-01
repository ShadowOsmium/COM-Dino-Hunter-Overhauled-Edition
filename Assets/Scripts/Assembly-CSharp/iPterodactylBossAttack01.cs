using UnityEngine;

public class iPterodactylBossAttack01 : _iAnimEventBase
{
	public void iPterodactylBossAttack01_PlayEffect(int nPrefabID)
	{
		PlayEffect(nPrefabID);
	}

	protected override void TransformRefresh(GameObject o)
	{
		o.transform.forward = base.transform.root.forward;
	}
}
