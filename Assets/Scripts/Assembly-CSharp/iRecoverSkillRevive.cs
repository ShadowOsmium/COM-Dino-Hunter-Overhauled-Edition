using UnityEngine;

public class iRecoverSkillRevive : _iAnimEventBase
{
	public void iRecoverSkillRevive_PlayEffect(int nPrefabID)
	{
		PlayEffect(nPrefabID);
	}

	protected override void TransformRefresh(GameObject o)
	{
		base.TransformRefresh(o);
		o.transform.forward = base.transform.forward;
	}
}
