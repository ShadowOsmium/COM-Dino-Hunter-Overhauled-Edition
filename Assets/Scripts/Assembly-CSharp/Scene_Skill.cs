using EventCenter;
using UnityEngine;

public class Scene_Skill : MonoBehaviour
{
	public TUIFade m_fade;

	private float m_fade_in_time;

	private float m_fade_out_time;

	private bool do_fade_in;

	private bool is_fade_out;

	private bool do_fade_out;

	private string next_scene = string.Empty;

	public PopupSkill popup_skill;

	private bool sfx_open_now = true;

	private bool music_open_now = true;

	public PopupNewHelp popup_new_help;

	private void Awake()
	{
		TUIDataServer.Instance().Initialize();
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.BackEvent_SceneSkill>(TUIEvent_SetUIInfo);
	}

	private void Start()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_TopBar));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_SkillInfo));
		DoNewHelp();
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
		global::EventCenter.EventCenter.Instance.Unregister<TUIEvent.BackEvent_SceneSkill>(TUIEvent_SetUIInfo);
	}

	public void TUIEvent_SetUIInfo(object sender, TUIEvent.BackEvent_SceneSkill m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_TopBar)
		{
			if (m_event.GetEventInfo().GetPlayerInfo() != null)
			{
				popup_skill.SetTopBarInfo(m_event.GetEventInfo().GetPlayerInfo());
			}
			else
			{
				Debug.Log("error!");
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_SkillInfo)
		{
			if (m_event.GetEventInfo() != null)
			{
				popup_skill.AddScrollList(m_event.GetEventInfo().all_skill_info, base.gameObject);
				popup_skill.SetLinkItem(m_event.GetEventInfo().all_skill_info);
			}
			else
			{
				Debug.Log("error!");
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_SkillUnlcok)
		{
			if (m_event.GetControlSuccess())
			{
				popup_skill.SkillUnlock();
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
				popup_skill.ShowPopupGoldToCrystal(wparam2, crystal);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseGoldToCrystal);
				break;
			}
			case BackEventFalseType.NoCrystalEnough:
			{
				int wparam = m_event.GetWparam();
				popup_skill.ShowPopupCrystalNoEnough(wparam);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseCrystalNoEnough);
				break;
			}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_SkillBuy)
		{
			if (m_event.GetControlSuccess())
			{
				popup_skill.SkillBuy();
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
				popup_skill.ShowPopupGoldToCrystal(wparam4, crystal2);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseGoldToCrystal);
				break;
			}
			case BackEventFalseType.NoCrystalEnough:
			{
				int wparam3 = m_event.GetWparam();
				popup_skill.ShowPopupCrystalNoEnough(wparam3);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseCrystalNoEnough);
				break;
			}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_SkillUpdate)
		{
			if (m_event.GetControlSuccess())
			{
				popup_skill.SkillUpdate();
				return;
			}
			switch (m_event.GetFalseType())
			{
			case BackEventFalseType.NoGoldEnough:
			{
				int wparam6 = m_event.GetWparam();
				int crystal3 = 0;
				TUIMappingInfo.GoldToCrystal goldToCrystalFunc3 = TUIMappingInfo.Instance().GetGoldToCrystalFunc();
				if (goldToCrystalFunc3 != null)
				{
					crystal3 = goldToCrystalFunc3(wparam6);
				}
				popup_skill.ShowPopupGoldToCrystal(wparam6, crystal3);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseGoldToCrystal);
				break;
			}
			case BackEventFalseType.NoCrystalEnough:
			{
				int wparam5 = m_event.GetWparam();
				popup_skill.ShowPopupCrystalNoEnough(wparam5);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseCrystalNoEnough);
				break;
			}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_Back)
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
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_NewMarkInfo)
		{
			if (m_event.GetEventInfo() != null)
			{
				popup_skill.UpdateNewMark(m_event.GetEventInfo().all_skill_info);
			}
			else
			{
				Debug.Log("error!");
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_GoldToCrystal)
		{
			if (m_event.GetControlSuccess())
			{
				popup_skill.DoGoldExchange();
				if (popup_skill.GetStateBtnSkill() == Btn_BuySkill.StateButtonSkill.State_Unlock)
				{
					popup_skill.ShowSkillUnlock();
					AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseSkillUnlock);
				}
				else if (popup_skill.GetStateBtnSkill() == Btn_BuySkill.StateButtonSkill.State_Learn)
				{
					popup_skill.ShowSkillBuy();
					AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseSkillBuy);
				}
				else if (popup_skill.GetStateBtnSkill() == Btn_BuySkill.StateButtonSkill.State_Update)
				{
					popup_skill.ShowSkillUpdate();
					AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseSkillUpdate);
				}
			}
			else
			{
				BackEventFalseType falseType = m_event.GetFalseType();
				if (falseType == BackEventFalseType.NoCrystalEnough)
				{
					int wparam7 = m_event.GetWparam();
					popup_skill.ShowPopupCrystalNoEnough(wparam7);
					AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseCrystalNoEnough);
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_EnterIAP)
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
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_EnterGold)
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
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_EnterIAPCrystalNoEnough)
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
		else if (m_event.GetEventName() == TUIEvent.SceneSkillEventType.TUIEvent_EnterGoEquip)
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
		else
		{
			if (m_event.GetEventName() != TUIEvent.SceneSkillEventType.TUIEvent_SkipTutorial)
			{
				return;
			}
			if (m_event.GetControlSuccess())
			{
				if (popup_new_help != null)
				{
					popup_new_help.Hide();
					popup_new_help.ShowSkipTutorial(false);
					AndroidReturnPlugin.instance.SetSkipTutorialFunc(null);
					AndroidReturnPlugin.instance.m_bSkipTutorial = false;
					AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseSkipTutorial);
				}
			}
			else if (popup_new_help != null)
			{
				popup_new_help.ShowSkipTutorial(false);
				AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseSkipTutorial);
				AndroidReturnPlugin.instance.m_bSkipTutorial = false;
			}
		}
	}

	public void TUIEvent_BtnRole(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 1)
		{
			popup_skill.ScrollListChoose(control);
		}
	}

	public void TUIEvent_OpenSkillUpdate(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_skill.GetStateBtnSkill() == Btn_BuySkill.StateButtonSkill.State_Unlock)
			{
				popup_skill.ShowSkillUnlock();
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseSkillUnlock);
			}
			else if (popup_skill.GetStateBtnSkill() == Btn_BuySkill.StateButtonSkill.State_Learn)
			{
				popup_skill.ShowSkillBuy();
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseSkillBuy);
			}
			else if (popup_skill.GetStateBtnSkill() == Btn_BuySkill.StateButtonSkill.State_Update)
			{
				popup_skill.ShowSkillUpdate();
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseSkillUpdate);
			}
		}
	}

	public void TUIEvent_SkillUpdate(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			int scrollListIndex = popup_skill.GetScrollListIndex();
			int skillID = popup_skill.GetSkillID();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_SkillUpdate, scrollListIndex, skillID));
			popup_skill.CloseSkillUpdate();
		}
	}

	public void TUIEvent_CloseSkillUpdate(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			popup_skill.CloseSkillUpdate();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseSkillUpdate);
		}
	}

	public void TUIEvent_SkillUnlock(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			int scrollListIndex = popup_skill.GetScrollListIndex();
			int skillID = popup_skill.GetSkillID();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_SkillUnlcok, scrollListIndex, skillID));
			popup_skill.CloseSkillUnlock();
		}
	}

	public void TUIEvent_CloseSkillUnlock(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			popup_skill.CloseSkillUnlock();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseSkillUnlock);
		}
	}

	public void TUIEvent_SkillBuy(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			int scrollListIndex = popup_skill.GetScrollListIndex();
			int skillID = popup_skill.GetSkillID();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_SkillBuy, scrollListIndex, skillID));
			popup_skill.CloseSkillBuy();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseSkillBuy);
		}
	}

	public void TUIEvent_CloseSkillBuy(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			popup_skill.CloseSkillBuy();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseSkillBuy);
		}
	}

	public void TUIEvent_HideUnlockBlink(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			popup_skill.CloseBlink();
			popup_skill.StarsBlink();
			popup_skill.ShowGoEquipAfterBuy(sfx_open_now);
			NewHelpState newHelpState = TUIMappingInfo.Instance().GetNewHelpState();
			if (newHelpState == NewHelpState.Help13_ClickSkillBuy)
			{
				TUIMappingInfo.Instance().NextNewHelpState();
				NewHelpState newHelpState2 = TUIMappingInfo.Instance().GetNewHelpState();
				popup_new_help.Show();
				popup_new_help.SetHelpState(newHelpState2, 0f);
			}
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_Back));
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_EnterIAP));
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_EnterGold));
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
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_GoldToCrystal, wparam2));
		popup_skill.HidePopupGoldToCrystal();
		AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseGoldToCrystal);
	}

	public void TUIEvent_CloseGoldToCrystal(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			popup_skill.HidePopupGoldToCrystal();
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
			popup_skill.HidePopupCrystalNoEnough();
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_EnterIAPCrystalNoEnough));
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
			popup_skill.HideGoEquip();
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_EnterGoEquip));
			popup_skill.HideGoEquip();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseGoEquip);
		}
	}

	public void TUIEvent_ClickNewHelp(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (control == popup_new_help.GetBtnMask())
		{
			popup_new_help.DoBtnMaskEvent();
			return;
		}
		switch (TUIMappingInfo.Instance().GetNewHelpState())
		{
		case NewHelpState.Help12_ClickOpenSkillBuy:
		{
			if (popup_skill.GetStateBtnSkill() == Btn_BuySkill.StateButtonSkill.State_Unlock)
			{
				popup_skill.ShowSkillUnlock();
			}
			else if (popup_skill.GetStateBtnSkill() == Btn_BuySkill.StateButtonSkill.State_Learn)
			{
				popup_skill.ShowSkillBuy();
			}
			else if (popup_skill.GetStateBtnSkill() == Btn_BuySkill.StateButtonSkill.State_Update)
			{
				popup_skill.ShowSkillUpdate();
			}
			TUIMappingInfo.Instance().NextNewHelpState();
			NewHelpState newHelpState = TUIMappingInfo.Instance().GetNewHelpState();
			popup_new_help.SetHelpState(newHelpState, 0f);
			break;
		}
		case NewHelpState.Help13_ClickSkillBuy:
		{
			int scrollListIndex = popup_skill.GetScrollListIndex();
			int skillID = popup_skill.GetSkillID();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_SkillBuy, scrollListIndex, skillID));
			popup_skill.CloseSkillBuy();
			popup_new_help.Hide();
			break;
		}
		case NewHelpState.Help14_ClickJumpToCamp:
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_EnterGoEquip));
			popup_skill.HideGoEquip();
			popup_new_help.Hide();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseGoEquip);
			TUIMappingInfo.Instance().NextNewHelpState();
			break;
		}
	}

	public void TUIEvent_OpenSkipTutorial(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_new_help != null)
			{
				popup_new_help.ShowSkipTutorial(true);
				AndroidReturnPlugin.instance.m_bSkipTutorial = true;
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseSkipTutorial);
			}
		}
	}

	public void TUIEvent_SkipTutorial(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_SkipTutorial));
		}
	}

	public void TUIEvent_CloseSkipTutorial(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_new_help != null)
			{
				popup_new_help.ShowSkipTutorial(false);
				AndroidReturnPlugin.instance.m_bSkipTutorial = false;
				AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseSkipTutorial);
			}
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

	private void DoNewHelp()
	{
		if (popup_new_help != null)
		{
			popup_new_help.ResetHelpState();
			NewHelpState newHelpState = TUIMappingInfo.Instance().GetNewHelpState();
			if (newHelpState != NewHelpState.Help_Over && newHelpState != NewHelpState.None)
			{
				popup_new_help.Show();
				popup_new_help.SetHelpState(newHelpState, 0f);
				AndroidReturnPlugin.instance.SetSkipTutorialFunc(TUIEvent_OpenSkipTutorial);
			}
		}
	}
}
