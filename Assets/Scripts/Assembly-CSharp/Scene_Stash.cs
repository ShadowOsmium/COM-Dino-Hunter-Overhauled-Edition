using EventCenter;
using UnityEngine;

public class Scene_Stash : MonoBehaviour
{
	public TUIFade m_fade;

	private float m_fade_in_time;

	private float m_fade_out_time;

	private bool do_fade_in;

	private bool is_fade_out;

	private bool do_fade_out;

	private string next_scene = string.Empty;

	public Popup_Stash popup_stash;

	private float m_click_time_gap;

	private bool m_plus_down;

	private bool m_substract_down;

	private int m_plus_substract_change;

	private bool m_have_play_count;

	private bool sfx_open_now = true;

	private bool music_open_now = true;

	private void Awake()
	{
		m_plus_down = false;
		m_substract_down = false;
		m_click_time_gap = 0f;
		TUIDataServer.Instance().Initialize();
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.BackEvent_SceneStash>(TUIEvent_SetUIInfo);
	}

	private void Start()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneStash(TUIEvent.SceneStashEventType.TUIEvent_TopBar));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneStash(TUIEvent.SceneStashEventType.TUIEvent_StashInfo));
	}

	private void Update()
	{
		m_fade_in_time += Time.deltaTime;
		if (m_fade_in_time >= m_fade.fadeInTime && !do_fade_in)
		{
			do_fade_in = true;
		}
		if (is_fade_out)
		{
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
		UpdateSellButton();
	}

	private void OnDestroy()
	{
		global::EventCenter.EventCenter.Instance.Unregister<TUIEvent.BackEvent_SceneStash>(TUIEvent_SetUIInfo);
	}

	public void TUIEvent_SetUIInfo(object sender, TUIEvent.BackEvent_SceneStash m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_TopBar)
		{
			if (m_event.GetEventInfo().GetPlayerInfo() != null)
			{
				popup_stash.SetTopBarInfo(m_event.GetEventInfo().GetPlayerInfo());
			}
			else
			{
				Debug.Log("error!");
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_StashInfo)
		{
			if (m_event.GetEventInfo() != null)
			{
				popup_stash.SetInfo(m_event.GetEventInfo().stash_info, base.gameObject);
			}
			else
			{
				Debug.Log("error!");
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_AddCapacity)
		{
			if (m_event.GetControlSuccess())
			{
				popup_stash.AddCapacity(sfx_open_now);
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
				popup_stash.ShowPopupGoldToCrystal(wparam2, crystal);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseGoldToCrystal);
				break;
			}
			case BackEventFalseType.NoCrystalEnough:
			{
				int wparam = m_event.GetWparam();
				popup_stash.ShowPopupCrystalNoEnough(wparam);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseCrystalNoEnough);
				break;
			}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_SellGoods)
		{
			if (m_event.GetControlSuccess())
			{
				popup_stash.UpdateSellGoods(sfx_open_now);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_Back)
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
		else if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_SearchGoodsDrop)
		{
			if (!is_fade_out)
			{
				next_scene = "Scene_Map";
				is_fade_out = true;
				m_fade.FadeOut();
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_GoldToCrystal)
		{
			if (m_event.GetControlSuccess())
			{
				popup_stash.DoGoldExchange();
				return;
			}
			BackEventFalseType falseType = m_event.GetFalseType();
			if (falseType == BackEventFalseType.NoCrystalEnough)
			{
				int wparam3 = m_event.GetWparam();
				popup_stash.ShowPopupCrystalNoEnough(wparam3);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseCrystalNoEnough);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_EnterIAP)
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
		else if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_EnterGold)
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
		else if (m_event.GetEventName() == TUIEvent.SceneStashEventType.TUIEvent_EnterIAPCrystalNoEnough)
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
	}

	public void TUIEvent_OpenCapacityAdd(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			popup_stash.ShowCapacityAdd();
			AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseCapacityAdd);
		}
	}

	public void TUIEvent_CapacityAdd(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneStash(TUIEvent.SceneStashEventType.TUIEvent_AddCapacity));
			popup_stash.CloseCapacityAdd();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseCapacityAdd);
		}
	}

	public void TUIEvent_CloseCapacityAdd(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			popup_stash.CloseCapacityAdd();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseCapacityAdd);
		}
	}

	public void TUIEvent_ChooseGoods(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 1)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Drag");
			}
			Btn_Select_Stash component = control.GetComponent<Btn_Select_Stash>();
			if (component == null)
			{
				Debug.Log("no goods control!");
			}
			else
			{
				popup_stash.SetGoodsControl(component);
			}
		}
	}

	public void TUIEvent_SearchGoodsDrop(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			Btn_Select_Stash goodsControl = popup_stash.GetGoodsControl();
			if (goodsControl == null || goodsControl.GetGoodsInfo() == null)
			{
				Debug.Log("error! no goods control!");
				return;
			}
			int id = goodsControl.GetGoodsInfo().id;
			int quality = (int)goodsControl.GetGoodsInfo().quality;
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneStash(TUIEvent.SceneStashEventType.TUIEvent_SearchGoodsDrop, id, quality));
		}
	}

	public void TUIEvent_OpenSell(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			popup_stash.ShowSell();
			AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseSell);
		}
	}

	public void TUIEvent_SellPlus(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		switch (event_type)
		{
		case 3:
			if (m_plus_substract_change <= 1 && sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Click");
			}
			m_plus_down = false;
			m_click_time_gap = 0.1f;
			popup_stash.SetSellParamPlus(1);
			break;
		case 1:
			m_plus_down = true;
			break;
		case 2:
			CUISound.GetInstance().Stop("UI_Count");
			m_plus_down = false;
			m_click_time_gap = 0.1f;
			m_plus_substract_change = 0;
			m_have_play_count = false;
			break;
		}
	}

	public void TUIEvent_SellSubstract(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		switch (event_type)
		{
		case 3:
			m_substract_down = false;
			m_click_time_gap = 0.1f;
			popup_stash.SetSellParamSubstract(1);
			if (m_plus_substract_change <= 1 && sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Click");
			}
			break;
		case 1:
			m_substract_down = true;
			break;
		case 2:
			CUISound.GetInstance().Stop("UI_Count");
			m_substract_down = false;
			m_click_time_gap = 0.1f;
			m_plus_substract_change = 0;
			m_have_play_count = false;
			break;
		}
	}

	public void TUIEvent_Sell(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			Btn_Select_Stash goodsControl = popup_stash.GetGoodsControl();
			if (goodsControl == null || goodsControl.GetGoodsInfo() == null)
			{
				Debug.Log("error! no goods control!");
				return;
			}
			int id = goodsControl.GetGoodsInfo().id;
			int quality = (int)goodsControl.GetGoodsInfo().quality;
			int sellCount = popup_stash.GetSellCount();
			Debug.Log("goods_id:" + id + " goods_quality:" + quality.ToString() + " sell_count:" + sellCount);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneStash(TUIEvent.SceneStashEventType.TUIEvent_SellGoods, id, quality, sellCount));
			popup_stash.HideSell();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseSell);
		}
	}

	public void TUIEvent_CloseSell(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			popup_stash.HideSell();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseSell);
		}
	}

	public void TUIEvent_PageFrame(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		//if (event_type == 1)
		//{
		//	popup_stash.SetGoodsControl(null);
		//}
	}

	public void TUIEvent_Back(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneStash(TUIEvent.SceneStashEventType.TUIEvent_Back));
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
			if (!is_fade_out)
			{
				next_scene = "Scene_IAP";
				is_fade_out = true;
				m_fade.FadeOut();
			}
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
			if (!is_fade_out)
			{
				next_scene = "Scene_Gold";
				is_fade_out = true;
				m_fade.FadeOut();
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
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneStash(TUIEvent.SceneStashEventType.TUIEvent_GoldToCrystal, wparam2));
		popup_stash.HidePopupGoldToCrystal();
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
			popup_stash.HidePopupGoldToCrystal();
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
			popup_stash.HidePopupCrystalNoEnough();
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneStash(TUIEvent.SceneStashEventType.TUIEvent_EnterIAPCrystalNoEnough));
		}
	}

	public void UpdateSellButton()
	{
		if (m_plus_down)
		{
			m_click_time_gap -= Time.deltaTime;
			if (m_click_time_gap <= 0f)
			{
				m_click_time_gap = 0.1f;
				popup_stash.SetSellParamPlus(1);
				m_plus_substract_change++;
			}
		}
		if (m_substract_down)
		{
			m_click_time_gap -= Time.deltaTime;
			if (m_click_time_gap <= 0f)
			{
				m_click_time_gap = 0.1f;
				popup_stash.SetSellParamSubstract(1);
				m_plus_substract_change++;
			}
		}
		if (m_plus_substract_change > 1 && sfx_open_now && !m_have_play_count)
		{
			m_have_play_count = true;
			CUISound.GetInstance().Play("UI_Count");
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
}
