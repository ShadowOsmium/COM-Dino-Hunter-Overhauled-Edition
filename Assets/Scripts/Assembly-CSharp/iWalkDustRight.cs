public class iWalkDustRight : _iAnimEventBase
{
	public void iWalkDustRight_PlayEffect(int nPrefabID)
	{
		PlayEffect(nPrefabID);
	}

	public void iWalkDustRight_Shake()
	{
		iGameSceneBase gameScene = iGameApp.GetInstance().m_GameScene;
		if (gameScene != null)
		{
			gameScene.ShakeCamera(0.2f);
		}
	}
}
