using EventCenter;
using UnityEngine;

public class Scene_Forge : MonoBehaviour
{
	public TUIFade m_fade;

	private float m_fade_in_time;

	private float m_fade_out_time;

	private bool do_fade_in;

	private bool is_fade_out;

	private bool do_fade_out;

	private string next_scene = string.Empty;

	public PopupWeapon popup_weapon;

	public PopupNewHelp popup_new_help;

	private bool sfx_open_now = true;

	private bool music_open_now = true;

	private void Awake()
	{
		TUIDataServer.Instance().Initialize();
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.BackEvent_SceneForge>(TUIEvent_SetUIInfo);
	}

	private void Start()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_TopBar));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_WeaponInfo));
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
		global::EventCenter.EventCenter.Instance.Unregister<TUIEvent.BackEvent_SceneForge>(TUIEvent_SetUIInfo);
	}

	public void TUIEvent_SetUIInfo(object sender, TUIEvent.BackEvent_SceneForge m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_TopBar)
		{
			if (m_event.GetEventInfo().GetPlayerInfo() != null)
			{
				popup_weapon.SetTopBarInfo(m_event.GetEventInfo().GetPlayerInfo());
			}
			else
			{
				Debug.Log("error!");
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_WeaponInfo)
		{
			if (m_event.GetEventInfo() != null)
			{
				popup_weapon.SetWeaponInfo(m_event.GetEventInfo().weapon_info);
			}
			else
			{
				Debug.Log("error!");
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_WeaponGoodsBuy)
		{
			if (m_event.GetControlSuccess())
			{
				int wparam = m_event.GetWparam();
				int lparam = m_event.GetLparam();
				popup_weapon.OnGoodsBuy(wparam, lparam);
				return;
			}
			switch (m_event.GetFalseType())
			{
			case BackEventFalseType.NoGoldEnough:
			{
				int wparam3 = m_event.GetWparam();
				int crystal = 0;
				TUIMappingInfo.GoldToCrystal goldToCrystalFunc = TUIMappingInfo.Instance().GetGoldToCrystalFunc();
				if (goldToCrystalFunc != null)
				{
					crystal = goldToCrystalFunc(wparam3);
				}
				popup_weapon.ShowPopupGoldToCrystal(wparam3, crystal);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseGoldToCrystal);
				break;
			}
			case BackEventFalseType.NoCrystalEnough:
			{
				int wparam2 = m_event.GetWparam();
				popup_weapon.ShowPopupCrystalNoEnough(wparam2);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseCrystalNoEnough);
				break;
			}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_ClickSupplement)
		{
			if (m_event.GetControlSuccess())
			{
				popup_weapon.OnSupplementSuccess();
				popup_weapon.OpenWeaponUpdate();
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseWeaponUpdate);
				return;
			}
			switch (m_event.GetFalseType())
			{
			case BackEventFalseType.NoGoldEnough:
			{
				int wparam5 = m_event.GetWparam();
				int crystal2 = 0;
				TUIMappingInfo.GoldToCrystal goldToCrystalFunc2 = TUIMappingInfo.Instance().GetGoldToCrystalFunc();
				if (goldToCrystalFunc2 != null)
				{
					crystal2 = goldToCrystalFunc2(wparam5);
				}
				popup_weapon.ShowPopupGoldToCrystal(wparam5, crystal2);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseGoldToCrystal);
				break;
			}
			case BackEventFalseType.NoCrystalEnough:
			{
				int wparam4 = m_event.GetWparam();
				popup_weapon.ShowPopupCrystalNoEnough(wparam4);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseCrystalNoEnough);
				break;
			}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_ClickUpgrade)
		{
			if (m_event.GetControlSuccess())
			{
				int wparam6 = m_event.GetWparam();
				int lparam2 = m_event.GetLparam();
				popup_weapon.OnUpgradeWeapon(wparam6, lparam2);
				popup_weapon.CloseWeaponUpdate();
				AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseWeaponUpdate);
				return;
			}
			switch (m_event.GetFalseType())
			{
			case BackEventFalseType.NoGoldEnough:
			{
				int wparam8 = m_event.GetWparam();
				int crystal3 = 0;
				TUIMappingInfo.GoldToCrystal goldToCrystalFunc3 = TUIMappingInfo.Instance().GetGoldToCrystalFunc();
				if (goldToCrystalFunc3 != null)
				{
					crystal3 = goldToCrystalFunc3(wparam8);
				}
				popup_weapon.ShowPopupGoldToCrystal(wparam8, crystal3);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseGoldToCrystal);
				break;
			}
			case BackEventFalseType.NoCrystalEnough:
			{
				int wparam7 = m_event.GetWparam();
				popup_weapon.ShowPopupCrystalNoEnough(wparam7);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseCrystalNoEnough);
				break;
			}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_GetActiveWeapon)
		{
			if (m_event.GetControlSuccess())
			{
				int wparam9 = m_event.GetWparam();
				int lparam3 = m_event.GetLparam();
				popup_weapon.OnUpgradeWeapon(wparam9, lparam3);
				popup_weapon.CloseWeaponUpdate();
				AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseWeaponUpdate);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_NewMarkInfo)
		{
			if (m_event.GetMarkData() != null)
			{
				popup_weapon.RefreshMark(m_event.GetMarkData());
			}
			else
			{
				Debug.Log("error!");
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_ShowSupplement)
		{
			TUISupplementInfo supplementInfo = m_event.GetSupplementInfo();
			if (supplementInfo != null)
			{
				popup_weapon.OpenWeaponSupplement(supplementInfo);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseWeaponSupplement);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_GoldToCrystal)
		{
			if (m_event.GetControlSuccess())
			{
				popup_weapon.DoGoldExchange();
				popup_weapon.OpenWeaponUpdate();
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseWeaponUpdate);
				return;
			}
			BackEventFalseType falseType = m_event.GetFalseType();
			if (falseType == BackEventFalseType.NoCrystalEnough)
			{
				int wparam10 = m_event.GetWparam();
				popup_weapon.ShowPopupCrystalNoEnough(wparam10);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseCrystalNoEnough);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_EnterIAP)
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
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_EnterGold)
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
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_SearchGoodsDrop)
		{
			if (m_event.GetControlSuccess())
			{
				DoSceneChange(m_event.GetWparam(), "Scene_Map");
				return;
			}
			m_fade_in_time = 0f;
			do_fade_in = false;
			m_fade.FadeIn();
		}
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_Back)
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
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_EnterIAPCrystalNoEnough)
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
		else if (m_event.GetEventName() == TUIEvent.SceneForgeEventType.TUIEvent_EnterGoEquip)
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
			if (m_event.GetEventName() != TUIEvent.SceneForgeEventType.TUIEvent_SkipTutorial)
			{
				return;
			}
			if (m_event.GetControlSuccess())
			{
				if (popup_new_help != null)
				{
					popup_new_help.Hide();
					popup_new_help.ShowSkipTutorial(false);
				}
				AndroidReturnPlugin.instance.SetSkipTutorialFunc(null);
				AndroidReturnPlugin.instance.m_bSkipTutorial = false;
				AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseSkipTutorial);
			}
			else
			{
				if (popup_new_help != null)
				{
					popup_new_help.ShowSkipTutorial(false);
				}
				AndroidReturnPlugin.instance.m_bSkipTutorial = false;
				AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseSkipTutorial);
			}
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_SkipTutorial));
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

	public void TUIEvent_WeaponKindItem(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 1)
		{
			return;
		}
		if (popup_weapon == null)
		{
			Debug.Log("error!");
			return;
		}
		WeaponKindItemBtn component = control.gameObject.GetComponent<WeaponKindItemBtn>();
		if (component != null)
		{
			popup_weapon.SetWeaponKindItem(component.m_nCategory);
		}
	}

	public void TUIEvent_MoveScreen(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 2)
		{
			popup_weapon.SetRoleRotation(wparam, lparam);
		}
	}

	public void TUIEvent_OpenWeaponUpdate(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			PopupWeaponBuy.PopupWeaponBuyState state = control.GetComponent<PopupWeaponBuy>().GetState();
			if (state == PopupWeaponBuy.PopupWeaponBuyState.State_Update || state == PopupWeaponBuy.PopupWeaponBuyState.State_Craft)
			{
				popup_weapon.OpenWeaponUpdate();
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseWeaponUpdate);
			}
		}
	}

	public void TUIEvent_CloseWeaponUpdate(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			popup_weapon.CloseWeaponUpdate();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseWeaponUpdate);
		}
	}

	public void TUIEvent_WeaponGoodsBuy(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (popup_weapon == null || popup_weapon.m_curWeaponAttributeInfo == null)
		{
			return;
		}
		TUIWeaponLevelInfo next = popup_weapon.m_curWeaponAttributeInfo.GetNext();
		if (next == null)
		{
			return;
		}
		GoodsNeedItemBuy component = control.GetComponent<GoodsNeedItemBuy>();
		if (component.m_nIndex >= 0 && component.m_nIndex < next.m_ltGoodsNeed.Count)
		{
			int nID = next.m_ltGoodsNeed[component.m_nIndex].m_nID;
			TUIMappingInfo.CTUIMaterialInfo material = TUIMappingInfo.Instance().GetMaterial(nID);
			int lparam2 = 0;
			if (material != null)
			{
				lparam2 = (int)material.m_nQuality;
			}
			int num = next.m_ltGoodsNeed[component.m_nIndex].m_nNeedCount - TUIMappingInfo.Instance().GetMaterialCount(nID);
			if (num < 0)
			{
				num = 0;
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_WeaponGoodsBuy, nID, lparam2, num));
		}
	}

	public void TUIEvent_WeaponUpdate(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (!(popup_weapon == null) && popup_weapon.m_curWeaponAttributeInfo != null)
			{
				int nID = popup_weapon.m_curWeaponAttributeInfo.m_nID;
				int weaponType = (int)popup_weapon.m_curWeaponAttributeInfo.m_WeaponType;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_ClickUpgrade, nID, weaponType));
				popup_weapon.CloseWeaponUpdate();
				AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseWeaponUpdate);
			}
		}
	}

	public void TUIEvent_GetActiveWeapon(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (!(popup_weapon == null) && popup_weapon.m_curWeaponAttributeInfo != null)
		{
			if (popup_weapon.m_curWeaponAttributeInfo.m_bActive && popup_weapon.m_curWeaponAttributeInfo.m_bActiveCanGet)
			{
				int nID = popup_weapon.m_curWeaponAttributeInfo.m_nID;
				int weaponType = (int)popup_weapon.m_curWeaponAttributeInfo.m_WeaponType;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_GetActiveWeapon, nID, weaponType));
			}
			popup_weapon.CloseWeaponUpdate();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseWeaponUpdate);
		}
	}

	public void TUIEvent_ClickSupplement(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			TUISupplementInfo supplementInfo = popup_weapon.GetSupplementInfo();
			if (supplementInfo == null)
			{
				Debug.Log("error!");
				return;
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_ClickSupplement, supplementInfo));
			popup_weapon.CloseWeaponSupplement();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseWeaponSupplement);
		}
	}

	public void TUIEvent_CloseWeaponSupplement(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			popup_weapon.CloseWeaponSupplement();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseWeaponSupplement);
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
			popup_weapon.CloseBlink();
			popup_weapon.StarsBlink();
			popup_weapon.OpenValueAnimation();
			popup_weapon.ShowGoEquipAfterBuy(sfx_open_now);
			NewHelpState newHelpState = TUIMappingInfo.Instance().GetNewHelpState();
			if (newHelpState == NewHelpState.Help03_ClickWeaponMake || newHelpState == NewHelpState.Help24_ClickWeaponUpgrade)
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_Back));
		}
	}

	public void TUIEvent_SearchGoodsDrop(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (popup_weapon == null || popup_weapon.m_curWeaponAttributeInfo == null)
		{
			return;
		}
		TUIWeaponLevelInfo next = popup_weapon.m_curWeaponAttributeInfo.GetNext();
		if (next == null)
		{
			return;
		}
		GoodsNeedItem component = control.transform.parent.GetComponent<GoodsNeedItem>();
		if (!(component == null) && component.m_nIndex >= 0 && component.m_nIndex < next.m_ltGoodsNeed.Count)
		{
			int nID = next.m_ltGoodsNeed[component.m_nIndex].m_nID;
			TUIMappingInfo.CTUIMaterialInfo material = TUIMappingInfo.Instance().GetMaterial(nID);
			int lparam2 = 0;
			if (material != null)
			{
				lparam2 = (int)material.m_nQuality;
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_SearchGoodsDrop, nID, lparam2));
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_EnterIAP));
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_EnterGold));
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
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_GoldToCrystal, wparam2));
		popup_weapon.HidePopupGoldToCrystal();
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
			popup_weapon.HidePopupGoldToCrystal();
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
			popup_weapon.HidePopupCrystalNoEnough();
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_EnterIAPCrystalNoEnough));
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
			popup_weapon.HideGoEquip();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseGoEquip);
		}
	}

	public void TUIEvent_ClickGoEquip(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (!(popup_weapon == null) && popup_weapon.m_curWeaponAttributeInfo != null)
		{
			PopupType popupType = PopupType.None;
			switch (popup_weapon.m_curWeaponAttributeInfo.m_WeaponType)
			{
			case WeaponType.CloseWeapon:
				popupType = PopupType.Weapons01;
				break;
			case WeaponType.Crossbow:
			case WeaponType.LiquidFireGun:
			case WeaponType.MachineGun:
			case WeaponType.RPG:
			case WeaponType.ViolenceGun:
				popupType = PopupType.Weapons02;
				break;
			case WeaponType.Armor_Head:
				popupType = PopupType.Armor_Head;
				break;
			case WeaponType.Armor_Body:
				popupType = PopupType.Armor_Body;
				break;
			case WeaponType.Armor_Leg:
				popupType = PopupType.Armor_Leg;
				break;
			case WeaponType.Armor_Bracelet:
				popupType = PopupType.Armor_Bracelet;
				break;
			case WeaponType.Accessory_Halo:
				popupType = PopupType.Accessory_Halo;
				break;
			case WeaponType.Accessory_Necklace:
				popupType = PopupType.Accessory_Necklace;
				break;
			case WeaponType.Accessory_Badge:
				popupType = PopupType.Accessory_Badge;
				break;
			case WeaponType.Accessory_Stoneskin:
				popupType = PopupType.Accessory_Stoneskin;
				break;
			}
			if (popupType != 0)
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_EnterGoEquip, (int)popupType));
			}
			popup_weapon.HideGoEquip();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseGoEquip);
		}
	}

	public void TUIEvent_ShowGoodsTips(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 1)
		{
			popup_weapon.ShowTips(control);
		}
		else
		{
			popup_weapon.HideTips();
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
		case NewHelpState.Help02_ClickOpenWeaponMake:
		{
			popup_weapon.OpenWeaponUpdate();
			TUIMappingInfo.Instance().NextNewHelpState();
			NewHelpState newHelpState3 = TUIMappingInfo.Instance().GetNewHelpState();
			popup_new_help.SetHelpState(newHelpState3, 0f);
			break;
		}
		case NewHelpState.Help03_ClickWeaponMake:
			if (popup_weapon.m_curWeaponAttributeInfo != null)
			{
				int nID3 = popup_weapon.m_curWeaponAttributeInfo.m_nID;
				int weaponType2 = (int)popup_weapon.m_curWeaponAttributeInfo.m_WeaponType;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_ClickUpgrade, nID3, weaponType2));
			}
			popup_weapon.CloseWeaponUpdate();
			popup_new_help.Hide();
			break;
		case NewHelpState.Help04_ClickJumpToCamp:
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_EnterGoEquip));
			popup_weapon.HideGoEquip();
			TUIMappingInfo.Instance().NextNewHelpState();
			popup_new_help.Hide();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseGoEquip);
			break;
		case NewHelpState.Help22_ClickOpenWeaponUpgrade:
		{
			popup_weapon.OpenWeaponUpdate();
			TUIMappingInfo.Instance().NextNewHelpState();
			NewHelpState newHelpState = TUIMappingInfo.Instance().GetNewHelpState();
			popup_new_help.SetHelpState(newHelpState, 0f);
			break;
		}
		case NewHelpState.Help23_ClickGoodsSupplement:
		{
			if (popup_weapon.m_curWeaponAttributeInfo != null)
			{
				TUIWeaponLevelInfo next = popup_weapon.m_curWeaponAttributeInfo.GetNext();
				if (next != null && next.m_ltGoodsNeed[0] != null)
				{
					int nID2 = next.m_ltGoodsNeed[0].m_nID;
					TUIMappingInfo.CTUIMaterialInfo material = TUIMappingInfo.Instance().GetMaterial(nID2);
					int lparam2 = 0;
					if (material != null)
					{
						lparam2 = (int)material.m_nQuality;
					}
					int num = next.m_ltGoodsNeed[0].m_nNeedCount - TUIMappingInfo.Instance().GetMaterialCount(nID2);
					if (num < 0)
					{
						num = 0;
					}
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_WeaponGoodsBuy, nID2, lparam2, num));
				}
			}
			TUIMappingInfo.Instance().NextNewHelpState();
			NewHelpState newHelpState2 = TUIMappingInfo.Instance().GetNewHelpState();
			popup_new_help.SetHelpState(newHelpState2, 0f);
			break;
		}
		case NewHelpState.Help24_ClickWeaponUpgrade:
			if (popup_weapon.m_curWeaponAttributeInfo != null)
			{
				int nID = popup_weapon.m_curWeaponAttributeInfo.m_nID;
				int weaponType = (int)popup_weapon.m_curWeaponAttributeInfo.m_WeaponType;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_ClickUpgrade, nID, weaponType));
			}
			popup_weapon.CloseWeaponUpdate();
			popup_new_help.Hide();
			break;
		case NewHelpState.Help25_ClickBackToVillage:
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_Back));
			popup_weapon.HideGoEquip();
			TUIMappingInfo.Instance().NextNewHelpState();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseGoEquip);
			break;
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
