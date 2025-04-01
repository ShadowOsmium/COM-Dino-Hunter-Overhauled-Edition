using EventCenter;
using UnityEngine;

public class Scene_Gold : MonoBehaviour
{
	public TUIFade m_fade;

	private float m_fade_in_time;

	private float m_fade_out_time;

	private bool do_fade_in;

	private bool is_fade_out;

	private bool do_fade_out;

	private string next_scene = string.Empty;

	public Top_Bar top_bar;

	public PopupIAP popup_iap;

	private bool sfx_open_now = true;

	private bool music_open_now = true;

	public PopupCrystalNoEnough popup_crystal_no_enough;

	private void Awake()
	{
		TUIDataServer.Instance().Initialize();
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.BackEvent_SceneGold>(TUIEvent_SetUIInfo);
	}

	private void Start()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneGold(TUIEvent.SceneGoldEventType.TUIEvent_TopBar));
		if (top_bar != null)
		{
			top_bar.SetBtnGoldShow(false);
		}
	}

	private void Update()
	{
		if (m_fade == null)
		{
			Debug.Log("error!no found m_fade!");
		}
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
		global::EventCenter.EventCenter.Instance.Unregister<TUIEvent.BackEvent_SceneGold>(TUIEvent_SetUIInfo);
	}

	public void TUIEvent_SetUIInfo(object sender, TUIEvent.BackEvent_SceneGold m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneGoldEventType.TUIEvent_TopBar)
		{
			if (m_event.GetEventInfo() != null && m_event.GetEventInfo().GetPlayerInfo() != null)
			{
				int role_id = m_event.GetEventInfo().player_info.role_id;
				int level = m_event.GetEventInfo().player_info.level;
				int exp = m_event.GetEventInfo().player_info.exp;
				int level_exp = m_event.GetEventInfo().player_info.level_exp;
				int gold = m_event.GetEventInfo().player_info.gold;
				int crystal = m_event.GetEventInfo().player_info.crystal;
				top_bar.SetAllValue(level, exp, level_exp, gold, crystal, role_id);
			}
			else
			{
				Debug.Log("error!");
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneGoldEventType.TUIEvent_GoldResult)
		{
			if (m_event.GetControlSuccess())
			{
				if (sfx_open_now)
				{
					CUISound.GetInstance().Play("UI_Crystal");
				}
				popup_iap.Hide();
				AndroidReturnPlugin.instance.ClearFunc(TUIEvent_HidePopup);
				if (m_event.GetEventInfo() != null && m_event.GetEventInfo().GetPlayerInfo() != null)
				{
					int gold2 = m_event.GetEventInfo().player_info.gold;
					int crystal2 = m_event.GetEventInfo().player_info.crystal;
					top_bar.SetGoldValue(gold2);
					top_bar.SetCrystalValue(crystal2);
				}
				else
				{
					Debug.Log("error! no info!");
				}
				return;
			}
			BackEventFalseType falseType = m_event.GetFalseType();
			if (falseType == BackEventFalseType.NoCrystalEnough)
			{
				int wparam = m_event.GetWparam();
				ShowPopupCrystalNoEnough(wparam);
				popup_iap.Hide();
				AndroidReturnPlugin.instance.ClearFunc(TUIEvent_HidePopup);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseCrystalNoEnough);
			}
			else
			{
				popup_iap.ShowPopupYes(string.Empty);
				if (sfx_open_now)
				{
					CUISound.GetInstance().Play("UI_Button");
				}
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_HidePopup);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneGoldEventType.TUIEvent_Back)
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
		else if (m_event.GetEventName() == TUIEvent.SceneGoldEventType.TUIEvent_EnterIAP)
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
		else if (m_event.GetEventName() == TUIEvent.SceneGoldEventType.TUIEvent_EnterIAPCrystalNoEnough)
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

	public void TUIEvent_GoldBuy(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			GoldItem component = control.transform.parent.GetComponent<GoldItem>();
			if (component != null)
			{
				int iD = component.GetID();
				int crystalValue = top_bar.GetCrystalValue();
				popup_iap.ShowPopupWaiting(string.Empty);
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneGold(TUIEvent.SceneGoldEventType.TUIEvent_GoldBuy, iD));
			}
		}
	}

	public void TUIEvent_HidePopup(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			popup_iap.Hide();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_HidePopup);
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneGold(TUIEvent.SceneGoldEventType.TUIEvent_Back));
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneGold(TUIEvent.SceneGoldEventType.TUIEvent_EnterIAP));
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
			HidePopupCrystalNoEnough();
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneGold(TUIEvent.SceneGoldEventType.TUIEvent_EnterIAPCrystalNoEnough));
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

	public void ShowPopupCrystalNoEnough(int m_crystal)
	{
		if (popup_crystal_no_enough != null)
		{
			string title = "you're " + m_crystal + " crystals short";
			string introduce = "Get more now?";
			popup_crystal_no_enough.Show();
			popup_crystal_no_enough.SetInfo(title, introduce, m_crystal, "OK");
		}
	}

	public void HidePopupCrystalNoEnough()
	{
		if (popup_crystal_no_enough != null)
		{
			popup_crystal_no_enough.Hide();
		}
	}

	public int GetCrystalNoEnoughCount()
	{
		if (popup_crystal_no_enough != null)
		{
			return popup_crystal_no_enough.GetCrystalNoEnoughCount();
		}
		return 0;
	}
}
