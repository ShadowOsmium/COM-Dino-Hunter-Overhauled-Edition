using UnityEngine;

public class iGame : MonoBehaviour
{
	private void Start()
	{
		iGameState gameState = iGameApp.GetInstance().m_GameState;
		if (gameState.CurScene == kGameSceneEnum.None)
		{
			gameState.CurScene = kGameSceneEnum.Game;
		}
		iGameApp.GetInstance().CreateScene();
	}

	private void Update()
	{
		iGameApp.GetInstance().Update(Time.deltaTime);
	}

	private void FixedUpdate()
	{
		iGameApp.GetInstance().FixedUpdate(Time.deltaTime);
	}

	private void LateUpdate()
	{
		iGameApp.GetInstance().LateUpdate(Time.deltaTime);
	}

	private void OnApplicationPause(bool bPause)
	{
		if (bPause && iGameApp.GetInstance().m_GameScene != null && !iGameApp.GetInstance().m_GameScene.isTutorialStage && TNetManager.GetInstance().Connection == null)
		{
			iGameApp.GetInstance().m_GameScene.SetGamePause(true);
		}
	}
}
