using UnityEngine;

public class iThrowStone : _iAnimEventGroundBase
{
	public void iThrowStone_PlayEffect(int nPrefabID)
	{
		PlayEffect(nPrefabID);
		iGameSceneBase gameScene = iGameApp.GetInstance().m_GameScene;
		if (gameScene != null)
		{
			gameScene.ShakeCamera(0.5f, 0.2f);
		}
	}

	protected override void TransformRefresh(GameObject o)
	{
		base.TransformRefresh(o);
		o.transform.forward = base.transform.forward;
	}
}
