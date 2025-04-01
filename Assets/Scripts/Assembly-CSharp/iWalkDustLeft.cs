public class iWalkDustLeft : _iAnimEventBase
{
	public void iWalkDustLeft_PlayEffect(int nPrefabID)
	{
		PlayEffect(nPrefabID);
	}

	public void iWalkDustLeft_Shake()
	{
		iGameSceneBase gameScene = iGameApp.GetInstance().m_GameScene;
		if (gameScene != null)
		{
			gameScene.ShakeCamera(0.2f);
		}
	}
}
