using UnityEngine;

public class iRecoverSkillDeath : _iAnimEventBase
{
	public void iRecoverSkillDeath_PlayEffect(int nPrefabID)
	{
		PlayEffect(nPrefabID);
	}

	protected override void TransformRefresh(GameObject o)
	{
		base.TransformRefresh(o);
		o.transform.forward = base.transform.forward;
	}
}
