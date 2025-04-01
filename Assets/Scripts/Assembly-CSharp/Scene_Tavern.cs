using EventCenter;
using UnityEngine;

public class Scene_Tavern : MonoBehaviour
{
	public TUIFade m_fade;

	private float m_fade_in_time;

	private float m_fade_out_time;

	private bool do_fade_in;

	private bool is_fade_out;

	private bool do_fade_out;

	private string next_scene = string.Empty;

	public PopupRole popup_role;

	private bool sfx_open_now = true;

	private bool music_open_now = true;

	public TUILabel label_role_name;

	private void Awake()
	{
		TUIDataServer.Instance().Initialize();
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.BackEvent_SceneTavern>(TUIEvent_SetUIInfo);
	}

	private void Start()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneTavern(TUIEvent.SceneTavernEventType.TUIEvent_TopBar));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneTavern(TUIEvent.SceneTavernEventType.TUIEvent_AllRoleInfo));
	}

	private void Update()
	{
		m_fade_in_time += Time.deltaTime;
		if (m_fade_in_time >= m_fade.fadeInTime && !do_fade_in)
		{
			do_fade_in = true;
		}
		if (!is_fade_out)
		{
			return;
		}
		m_fade_out_time += Time.deltaTime;
		if (m_fade_out_time >= m_fade.fadeOutTime && !do_fade_out)
		{
			do_fade_out = true;
			m_fade.SetFadeOutEnd();
			TUIMappingInfo.SwitchSceneStr switchSceneStr = TUIMappingInfo.Instance().GetSwitchSceneStr();
			if (switchSceneStr != null)
			{
				switchSceneStr(next_scene);
			}
		}
	}

	private void OnDestroy()
	{
		global::EventCenter.EventCenter.Instance.Unregister<TUIEvent.BackEvent_SceneTavern>(TUIEvent_SetUIInfo);
	}

	public void TUIEvent_SetUIInfo(object sender, TUIEvent.BackEvent_SceneTavern m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_TopBar)
		{
			TUIPlayerInfo playerInfo = m_event.GetEventInfo().GetPlayerInfo();
			if (playerInfo != null)
			{
				if (popup_role != null)
				{
					popup_role.SetTopBarInfo(playerInfo);
				}
				if (label_role_name != null)
				{
					label_role_name.Text = playerInfo.avatar_name;
				}
			}
			else
			{
				Debug.Log("error!");
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_AllRoleInfo)
		{
			if (m_event.GetEventInfo() != null)
			{
				popup_role.AddScrollListItem(m_event.GetEventInfo().all_role_info);
			}
			else
			{
				Debug.Log("error!");
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_RoleUnlock)
		{
			if (m_event.GetControlSuccess())
			{
				popup_role.SetRoleUnlock(sfx_open_now);
				return;
			}
			switch (m_event.GetFalseType())
			{
			case BackEventFalseType.NoGoldEnough:
			{
				int wparam2 = m_event.GetWparam();
				int crystal = 0;
				TUIMappingInfo.GoldToCrystal goldToCrystalFunc = TUIMappingInfo.Instance().GetGoldToCrystalFunc();
				if (goldToCrystalFunc != null)
				{
					crystal = goldToCrystalFunc(wparam2);
				}
				popup_role.ShowPopupGoldToCrystal(wparam2, crystal);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseGoldToCrystal);
				break;
			}
			case BackEventFalseType.NoCrystalEnough:
			{
				int wparam = m_event.GetWparam();
				popup_role.ShowPopupCrystalNoEnough(wparam);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseCrystalNoEnough);
				break;
			}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_RoleBuy)
		{
			if (m_event.GetControlSuccess())
			{
				popup_role.SetRoleBuy(sfx_open_now);
				return;
			}
			switch (m_event.GetFalseType())
			{
			case BackEventFalseType.NoGoldEnough:
			{
				int wparam4 = m_event.GetWparam();
				int crystal2 = 0;
				TUIMappingInfo.GoldToCrystal goldToCrystalFunc2 = TUIMappingInfo.Instance().GetGoldToCrystalFunc();
				if (goldToCrystalFunc2 != null)
				{
					crystal2 = goldToCrystalFunc2(wparam4);
				}
				popup_role.ShowPopupGoldToCrystal(wparam4, crystal2);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseGoldToCrystal);
				break;
			}
			case BackEventFalseType.NoCrystalEnough:
			{
				int wparam3 = m_event.GetWparam();
				popup_role.ShowPopupCrystalNoEnough(wparam3);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseCrystalNoEnough);
				break;
			}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_GetActiveRole)
		{
			if (m_event.GetControlSuccess())
			{
				popup_role.SetRoleBuy(sfx_open_now);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_Back)
		{
			if (m_event.GetControlSuccess())
			{
				DoSceneChange(m_event.GetWparam(), "Scene_MainMenu");
				return;
			}
			m_fade_in_time = 0f;
			do_fade_in = false;
			m_fade.FadeIn();
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_NewMarkInfo)
		{
			if (m_event.GetEventInfo() != null && m_event.GetEventInfo().all_role_info != null)
			{
				TUIAllRoleInfo all_role_info = m_event.GetEventInfo().all_role_info;
				if (all_role_info != null)
				{
					popup_role.UpdateNewMark(all_role_info.new_mark_list);
				}
			}
			else
			{
				Debug.Log("error!");
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_GoldToCrystal)
		{
			if (m_event.GetControlSuccess())
			{
				popup_role.DoGoldExchange();
				switch (popup_role.GetRoleBuyState())
				{
				case PopupRoleBtnBuy.PopupRoleBuyState.State_Unlock:
					popup_role.ShowPopupUnlock();
					AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseRoleUnlock);
					break;
				case PopupRoleBtnBuy.PopupRoleBuyState.State_Buy:
					popup_role.ShowPopupBuy();
					AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseRoleBuy);
					break;
				}
			}
			else
			{
				BackEventFalseType falseType = m_event.GetFalseType();
				if (falseType == BackEventFalseType.NoCrystalEnough)
				{
					int wparam5 = m_event.GetWparam();
					popup_role.ShowPopupCrystalNoEnough(wparam5);
					AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseCrystalNoEnough);
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_EnterIAP)
		{
			if (m_event.GetControlSuccess())
			{
				DoSceneChange(m_event.GetWparam(), "Scene_IAP");
				return;
			}
			m_fade_in_time = 0f;
			do_fade_in = false;
			m_fade.FadeIn();
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_EnterGold)
		{
			if (m_event.GetControlSuccess())
			{
				DoSceneChange(m_event.GetWparam(), "Scene_Gold");
				return;
			}
			m_fade_in_time = 0f;
			do_fade_in = false;
			m_fade.FadeIn();
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_EnterIAPCrystalNoEnough)
		{
			if (m_event.GetControlSuccess())
			{
				DoSceneChange(m_event.GetWparam(), "Scene_IAP");
				return;
			}
			m_fade_in_time = 0f;
			do_fade_in = false;
			m_fade.FadeIn();
		}
		else if (m_event.GetEventName() == TUIEvent.SceneTavernEventType.TUIEvent_EnterGoEquip)
		{
			if (m_event.GetControlSuccess())
			{
				DoSceneChange(m_event.GetWparam(), "Scene_Equip");
				return;
			}
			m_fade_in_time = 0f;
			do_fade_in = false;
			m_fade.FadeIn();
		}
	}

	public void TUIEvent_MoveScreen(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 2)
		{
			popup_role.SetRoleRotation(wparam, lparam);
		}
	}

	public void TUIEvent_BtnBuy(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			switch (popup_role.GetRoleBuyState())
			{
			case PopupRoleBtnBuy.PopupRoleBuyState.State_Unlock:
				popup_role.ShowPopupUnlock();
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseRoleUnlock);
				break;
			case PopupRoleBtnBuy.PopupRoleBuyState.State_Buy:
				popup_role.ShowPopupBuy();
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseRoleBuy);
				break;
			default:
				Debug.Log("error!");
				break;
			}
		}
	}

	public void TUIEvent_RoleUnlock(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			int roleChooseID = popup_role.GetRoleChooseID();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneTavern(TUIEvent.SceneTavernEventType.TUIEvent_RoleUnlock, roleChooseID));
			popup_role.ClosePopupUnlock();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseRoleUnlock);
		}
	}

	public void TUIEvent_RoleBuy(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			int roleChooseID = popup_role.GetRoleChooseID();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneTavern(TUIEvent.SceneTavernEventType.TUIEvent_RoleBuy, roleChooseID));
			popup_role.ClosePopupBuy();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseRoleBuy);
		}
	}

	public void TUIEvent_GetActiveRole(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			bool flag = popup_role.IsGetActiveRole();
			int roleChooseID = popup_role.GetRoleChooseID();
			if (flag)
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneTavern(TUIEvent.SceneTavernEventType.TUIEvent_GetActiveRole, roleChooseID));
			}
			popup_role.ClosePopupBuy();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseRoleBuy);
		}
	}

	public void TUIEvent_CloseRoleUnlock(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			popup_role.ClosePopupUnlock();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseRoleUnlock);
		}
	}

	public void TUIEvent_CloseRoleBuy(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			popup_role.ClosePopupBuy();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseRoleBuy);
		}
	}

	public void TUIEvent_Back(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneTavern(TUIEvent.SceneTavernEventType.TUIEvent_Back));
		}
	}

	public void TUIEvent_IAP(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneTavern(TUIEvent.SceneTavernEventType.TUIEvent_EnterIAP));
		}
	}

	public void TUIEvent_Gold(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneTavern(TUIEvent.SceneTavernEventType.TUIEvent_EnterGold));
		}
	}

	public void TUIEvent_BtnGoldToCrystal(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (control.transform.parent == null || control.transform.parent.parent == null)
		{
			Debug.Log("error!");
			return;
		}
		int wparam2 = 0;
		PopupGoldToCrystal component = control.transform.parent.parent.GetComponent<PopupGoldToCrystal>();
		if (component != null)
		{
			wparam2 = component.GetGoldExchangeCount();
		}
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneTavern(TUIEvent.SceneTavernEventType.TUIEvent_GoldToCrystal, wparam2));
		popup_role.HidePopupGoldToCrystal();
		AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseGoldToCrystal);
	}

	public void TUIEvent_HideUnlockBlink(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			popup_role.CloseBlink();
			popup_role.ShowGoEquipAfterBuy(sfx_open_now);
		}
	}

	public void TUIEvent_CloseGoldToCrystal(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			popup_role.HidePopupGoldToCrystal();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseGoldToCrystal);
		}
	}

	public void TUIEvent_CloseCrystalNoEnough(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			popup_role.HidePopupCrystalNoEnough();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseCrystalNoEnough);
		}
	}

	public void TUIEvent_BtnCrystalNoEnough(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneTavern(TUIEvent.SceneTavernEventType.TUIEvent_EnterIAPCrystalNoEnough));
		}
	}

	public void TUIEvent_ClickActiveSkill(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			popup_role.ShowPopupActiveSkill(control);
			AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseActiveSkill);
		}
	}

	public void TUIEvent_CloseActiveSkill(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			popup_role.HidePopupActiveSkill();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseActiveSkill);
		}
	}

	public void TUIEvent_CloseGoEquip(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			popup_role.HideGoEquip();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseGoEquip);
		}
	}

	public void TUIEvent_ClickGoEquip(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneTavern(TUIEvent.SceneTavernEventType.TUIEvent_EnterGoEquip));
			popup_role.HideGoEquip();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseGoEquip);
		}
	}

	public void DoSceneChange(int m_scene_id, string m_scene_normal)
	{
		string sceneName = TUIMappingInfo.Instance().GetSceneName(m_scene_id);
		if (sceneName != string.Empty)
		{
			next_scene = sceneName;
		}
		else
		{
			next_scene = m_scene_normal;
		}
		if (!is_fade_out)
		{
			is_fade_out = true;
			m_fade.FadeOut();
		}
	}

	public bool GetMusicOpen()
	{
		return music_open_now;
	}

	public bool GetSFXOpen()
	{
		return sfx_open_now;
	}
}
