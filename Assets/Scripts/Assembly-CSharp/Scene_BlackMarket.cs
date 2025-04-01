using System.Collections.Generic;
using EventCenter;
using UnityEngine;

public class Scene_BlackMarket : MonoBehaviour
{
	public TUIFade m_fade;

	private float m_fade_in_time;

	private float m_fade_out_time;

	private bool do_fade_in;

	private bool is_fade_out;

	private bool do_fade_out;

	private string next_scene = string.Empty;

	private bool sfx_open_now = true;

	private bool music_open_now = true;

	public PopupBlackMarket popup_blackmarket;

	public Dictionary<int, string> title_list;

	private void Awake()
	{
		TUIDataServer.Instance().Initialize();
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.BackEvent_SceneBlackMarket>(TUIEvent_SetUIInfo);
	}

	private void Start()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneBlackMarket(TUIEvent.SceneBlackMarketEventType.TUIEvent_TopBar));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneBlackMarket(TUIEvent.SceneBlackMarketEventType.TUIEvent_GoodsInfo));
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
		global::EventCenter.EventCenter.Instance.Unregister<TUIEvent.BackEvent_SceneBlackMarket>(TUIEvent_SetUIInfo);
	}

	public void TUIEvent_SetUIInfo(object sender, TUIEvent.BackEvent_SceneBlackMarket m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneBlackMarketEventType.TUIEvent_TopBar)
		{
			if (popup_blackmarket != null)
			{
				TUIGameInfo eventInfo = m_event.GetEventInfo();
				if (eventInfo != null)
				{
					TUIPlayerInfo playerInfo = eventInfo.GetPlayerInfo();
					popup_blackmarket.SetTopBarInfo(playerInfo);
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneBlackMarketEventType.TUIEvent_GoodsInfo)
		{
			TUIGameInfo eventInfo2 = m_event.GetEventInfo();
			if (eventInfo2 != null)
			{
				TUIBlackMarketInfo blackmarket_info = eventInfo2.blackmarket_info;
				if (blackmarket_info != null)
				{
					popup_blackmarket.SetGoodsInfo(blackmarket_info);
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneBlackMarketEventType.TUIEvent_EnterIAP)
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
		else if (m_event.GetEventName() == TUIEvent.SceneBlackMarketEventType.TUIEvent_EnterGold)
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
		else if (m_event.GetEventName() == TUIEvent.SceneBlackMarketEventType.TUIEvent_Back)
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
		else if (m_event.GetEventName() == TUIEvent.SceneBlackMarketEventType.TUIEvent_ClickBtnBuy)
		{
			if (m_event.GetControlSuccess())
			{
				if (popup_blackmarket != null)
				{
					popup_blackmarket.OnBuyItem();
				}
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
				if (popup_blackmarket != null)
				{
					popup_blackmarket.ShowPopupGoldToCrystal(wparam2, crystal);
					AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseGoldToCrystal);
				}
				break;
			}
			case BackEventFalseType.NoCrystalEnough:
			{
				int wparam = m_event.GetWparam();
				if (popup_blackmarket != null)
				{
					popup_blackmarket.ShowPopupCrystalNoEnough(wparam);
					AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseCrystalNoEnough);
				}
				break;
			}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneBlackMarketEventType.TUIEvent_GoldToCrystal)
		{
			if (m_event.GetControlSuccess())
			{
				if (popup_blackmarket != null)
				{
					popup_blackmarket.DoGoldExchange();
				}
				return;
			}
			BackEventFalseType falseType = m_event.GetFalseType();
			if (falseType == BackEventFalseType.NoCrystalEnough)
			{
				int wparam3 = m_event.GetWparam();
				if (popup_blackmarket != null)
				{
					popup_blackmarket.ShowPopupCrystalNoEnough(wparam3);
					AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseCrystalNoEnough);
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneBlackMarketEventType.TUIEvent_EnterIAPCrystalNoEnough)
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
		else if (m_event.GetEventName() == TUIEvent.SceneBlackMarketEventType.TUIEvent_EnterGoEquip)
		{
			if (m_event.GetControlSuccess())
			{
				Debug.Log(m_event.GetWparam());
				DoSceneChange(m_event.GetWparam(), "Scene_Equip");
			}
			else
			{
				m_fade_in_time = 0f;
				do_fade_in = false;
				m_fade.FadeIn();
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneBlackMarket(TUIEvent.SceneBlackMarketEventType.TUIEvent_Back));
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneBlackMarket(TUIEvent.SceneBlackMarketEventType.TUIEvent_EnterIAP));
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneBlackMarket(TUIEvent.SceneBlackMarketEventType.TUIEvent_EnterGold));
		}
	}

	public void TUIEvent_MoveScreen(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 2)
		{
			popup_blackmarket.SetRoleRotation(wparam, lparam);
		}
	}

	public void TUIEvent_ChangeGoodsItem(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != TUIScrollListCircle.CommandChange || !(popup_blackmarket != null))
		{
			return;
		}
		TUIScrollListCircle component = control.GetComponent<TUIScrollListCircle>();
		if (!(component != null))
		{
			return;
		}
		GameObject nowItem = component.GetNowItem();
		if (!(nowItem != null))
		{
			return;
		}
		BlackMarketItem component2 = nowItem.GetComponent<BlackMarketItem>();
		if (component2 != null)
		{
			TUIBlackMarketItem info = component2.GetInfo();
			if (info != null)
			{
				popup_blackmarket.ChangeNowItemInfo(info.m_nBlackMarketID);
			}
		}
	}

	public void TUIEvent_ClickBtnBuy(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (popup_blackmarket != null)
		{
			TUIBlackMarketItem nowItem = popup_blackmarket.GetNowItem();
			if (nowItem != null)
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneBlackMarket(TUIEvent.SceneBlackMarketEventType.TUIEvent_ClickBtnBuy, nowItem.m_nBlackMarketID));
			}
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
		if (popup_blackmarket == null)
		{
			return;
		}
		TUIBlackMarketItem nowItem = popup_blackmarket.GetNowItem();
		if (nowItem != null)
		{
			PopupType popupType = PopupType.None;
			switch (nowItem.m_WeaponType)
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
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneBlackMarket(TUIEvent.SceneBlackMarketEventType.TUIEvent_EnterGoEquip, (int)popupType));
			}
			popup_blackmarket.HideGoEquip();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseGoEquip);
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
			popup_blackmarket.HideGoEquip();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseGoEquip);
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
			if (popup_blackmarket != null)
			{
				popup_blackmarket.CloseUnlockBlink();
				popup_blackmarket.ShowGoEquipAfterBuy(sfx_open_now);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseGoEquip);
			}
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
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneBlackMarket(TUIEvent.SceneBlackMarketEventType.TUIEvent_GoldToCrystal, wparam2));
		if (popup_blackmarket != null)
		{
			popup_blackmarket.HidePopupGoldToCrystal();
		}
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
			if (popup_blackmarket != null)
			{
				popup_blackmarket.HidePopupGoldToCrystal();
			}
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseGoldToCrystal);
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneBlackMarket(TUIEvent.SceneBlackMarketEventType.TUIEvent_EnterIAPCrystalNoEnough));
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
			if (popup_blackmarket != null)
			{
				popup_blackmarket.HidePopupCrystalNoEnough();
			}
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseCrystalNoEnough);
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
