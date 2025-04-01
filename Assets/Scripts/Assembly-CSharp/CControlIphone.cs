using UnityEngine;

public class CControlIphone : CControlBase
{
	protected float m_fSlashSpeed;

	protected Vector2 m_v2Slash;

	public CControlIphone()
	{
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		m_GameUI = m_GameScene.GetGameUI();
		m_GameUI.RegisterEvent();
	}

	public override void Initialize()
	{
		base.Initialize();
		m_fSlashSpeed = ((!Utils.IsPad()) ? 3f : 6f);
		m_v2Slash = Vector2.zero;
	}

	public override void Update(float deltaTime)
	{
		if (m_GameScene != null && !(m_User == null) && m_User.IsCanAim() && (m_v2Slash.x != 0f || m_v2Slash.y != 0f))
		{
			Ray ray = m_Camera.ScreenPointToRay(m_GameState.ScreenCenter, 0f);
			m_User.LookAt(ray.GetPoint(1000f));
		}
	}

	public override void LateUpdate(float deltaTime)
	{
		if (m_GameScene == null || m_User == null || !(m_v2Slash != Vector2.zero))
		{
			return;
		}
		if (m_v2Slash.x != 0f)
		{
			m_Camera.Yaw(m_v2Slash.x * m_fSlashSpeed / 2f * Time.deltaTime);
			if (m_User.IsCanAim())
			{
				m_User.SetYaw(m_Camera.GetYaw());
			}
		}
		if (m_v2Slash.y != 0f)
		{
			m_Camera.Pitch(m_v2Slash.y * m_fSlashSpeed * Time.deltaTime);
		}
		m_v2Slash = Vector2.zero;
	}
}
