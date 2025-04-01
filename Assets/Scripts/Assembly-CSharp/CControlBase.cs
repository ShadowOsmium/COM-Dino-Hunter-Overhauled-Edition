public abstract class CControlBase
{
	protected iGameSceneBase m_GameScene;

	protected iGameUIBase m_GameUI;

	protected iGameState m_GameState;

	protected CCharUser m_User;

	protected iCameraTrail m_Camera;

	public virtual void Initialize()
	{
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		m_GameUI = m_GameScene.GetGameUI();
		m_GameState = iGameApp.GetInstance().m_GameState;
		m_User = m_GameScene.GetUser();
		m_Camera = m_GameScene.GetCamera();
	}

	public virtual void Destroy()
	{
		m_User = null;
		m_Camera = null;
	}

	public abstract void Update(float deltaTime);

	public abstract void LateUpdate(float deltaTime);
}
