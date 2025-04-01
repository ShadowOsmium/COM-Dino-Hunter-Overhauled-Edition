using UnityEngine;

public class CControlWindows : CControlBase
{
	public CControlWindows()
	{
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		m_GameUI = m_GameScene.GetGameUI();
		m_GameUI.RegisterEvent_Windows();
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	public override void Update(float deltaTime)
	{
		if (m_GameScene == null)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.T))
		{
		}
		if (m_User == null || (m_GameScene.GameStatus != iGameSceneBase.kGameStatus.Gameing && m_GameScene.GameStatus != iGameSceneBase.kGameStatus.GameOver_ShowTime))
		{
			return;
		}
		Vector2 zero = Vector2.zero;
		if (m_User.IsCanMove())
		{
			if (Input.GetKey(KeyCode.W))
			{
				zero.y += 1f;
			}
			if (Input.GetKey(KeyCode.S))
			{
				zero.y += -1f;
			}
			if (Input.GetKey(KeyCode.A))
			{
				zero.x += -1f;
			}
			if (Input.GetKey(KeyCode.D))
			{
				zero.x += 1f;
			}
		}
		if (zero == Vector2.zero)
		{
			m_User.MoveStop();
		}
		else
		{
			m_User.MoveByCompass(zero.x, zero.y);
			Ray ray = m_Camera.ScreenPointToRay(m_GameState.ScreenCenter, 0f);
			m_User.LookAt(ray.GetPoint(1000f));
		}
		if (Screen.lockCursor)
		{
			float axis = Input.GetAxis("Mouse X");
			if (axis != 0f)
			{
				m_Camera.Yaw(axis * 270f * Time.deltaTime);
				if (m_User.IsCanAim())
				{
					m_User.SetYaw(m_Camera.GetYaw());
				}
			}
			float axis2 = Input.GetAxis("Mouse Y");
			if (axis2 != 0f)
			{
				m_Camera.Pitch(axis2 * 270f * Time.deltaTime);
			}
			if (Input.GetMouseButton(1))
			{
				if (m_User.IsCanAim() && (axis != 0f || axis2 != 0f))
				{
					Ray ray2 = m_Camera.ScreenPointToRay(m_GameState.ScreenCenter, 0f);
					m_User.LookAt(ray2.GetPoint(1000f));
				}
				if (Mathf.Abs(axis) > 0.1f || Mathf.Abs(axis2) > 0.1f)
				{
					m_GameScene.AssistAim_Stop();
				}
				else if (m_User.IsFire() && !m_GameScene.IsAssistAim())
				{
					m_GameScene.AssistAim_Start();
				}
			}
			else
			{
				if (!m_User.IsFire() && m_GameScene.IsAssistAim())
				{
					m_GameScene.AssistAim_Stop();
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			if (m_User.IsCanAttack() && !m_User.IsSkillCD())
			{
				m_User.UseSkill(m_User.SkillID, m_User.SkillLevel);
			}
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{
			if (m_GameScene.CurGameLevelInfo.m_bLimitMelee)
			{
				return;
			}
			int curWeaponIndex = m_User.CurWeaponIndex;
			int num = curWeaponIndex - 1;
			while (num != curWeaponIndex && m_GameState.GetWeapon(num) == null)
			{
				num--;
				if (num < 0)
				{
					num = 2;
				}
			}
			m_User.SwitchWeapon(num);
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			if (m_GameScene.CurGameLevelInfo.m_bLimitMelee)
			{
				return;
			}
			int curWeaponIndex2 = m_User.CurWeaponIndex;
			int num2 = curWeaponIndex2 + 1;
			while (num2 != curWeaponIndex2 && m_GameState.GetWeapon(num2) == null)
			{
				num2++;
				if (num2 >= 3)
				{
					num2 = 0;
				}
			}
			m_User.SwitchWeapon(num2);
		}
		if (Input.GetKeyDown(KeyCode.Alpha9))
		{
			Debug.Log("press 9 key");
			m_GameScene.FinishGame(true);
			if (CGameNetManager.GetInstance().IsConnected())
			{
				CGameNetSender.GetInstance().SendMsg_GAME_OVER(true);
			}
		}
		if (!Input.GetKeyDown(KeyCode.Alpha0))
		{
			return;
		}
		Debug.Log("press 0 key");
		foreach (CCharMob item in m_GameScene.GetMobEnumerator())
		{
			CCharBoss cCharBoss = item as CCharBoss;
			if (cCharBoss != null)
			{
				cCharBoss.SetReadyToBlack(true, 2000f);
			}
		}
	}

	public override void LateUpdate(float deltaTime)
	{
	}
}
