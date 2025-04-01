public class iRoar : _iAnimEventFollowBase
{
	public void iRoar_PlayEffect(int nPrefabID)
	{
		PlayEffect(nPrefabID);
	}

	public void iRoar_PlaySound(string sound)
	{
		PlaySound(sound);
	}

	public void iRoar_PlayShakeCamera()
	{
		iGameSceneBase gameScene = iGameApp.GetInstance().m_GameScene;
		if (gameScene != null)
		{
			gameScene.ShakeCamera(1f, 0.1f);
		}
	}
}
