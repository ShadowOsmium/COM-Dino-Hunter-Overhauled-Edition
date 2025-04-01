using UnityEngine;

public class iSlashTail : _iAnimEventGroundBase
{
	public void iSlashTail_PlayEffect(int nPrefabID)
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
		o.transform.position = base.transform.position;
		o.transform.forward = base.transform.forward;
	}
}
