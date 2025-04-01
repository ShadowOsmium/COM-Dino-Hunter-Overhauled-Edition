using EventCenter;
using UnityEngine;

public class Scene_IAP : MonoBehaviour
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

	public IAPItems iap_items;

	private void Awake()
	{
		TUIDataServer.Instance().Initialize();
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.BackEvent_SceneIAP>(TUIEvent_SetUIInfo);
	}

	private void Start()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneIAP(TUIEvent.SceneIAPEventType.TUIEvent_TopBar));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneIAP(TUIEvent.SceneIAPEventType.TUIEvent_IAPEnterInfo));
		if (top_bar != null)
		{
			top_bar.SetBtnCrystalShow(false);
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
		global::EventCenter.EventCenter.Instance.Unregister<TUIEvent.BackEvent_SceneIAP>(TUIEvent_SetUIInfo);
	}

	public void TUIEvent_SetUIInfo(object sender, TUIEvent.BackEvent_SceneIAP m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneIAPEventType.TUIEvent_TopBar)
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
		else if (m_event.GetEventName() == TUIEvent.SceneIAPEventType.TUIEvent_IAPEnterInfo)
		{
			if (m_event.GetEventInfo() != null)
			{
				TUIIAPInfo iAPInfo = m_event.GetEventInfo().GetIAPInfo();
				if (iAPInfo == null || iap_items == null)
				{
					Debug.Log("error!");
				}
				iap_items.DoCreate(iAPInfo, base.gameObject);
			}
			else
			{
				Debug.Log("error!");
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneIAPEventType.TUIEvent_IAPResult)
		{
			if (m_event.GetControlSuccess())
			{
				popup_iap.Hide();
				AndroidReturnPlugin.instance.ClearFunc(TUIEvent_HidePopup);
				popup_iap.ShowPopupWaiting("Please wait while the system is verifying your purchase...");
				return;
			}
			switch ((IAPFailType)m_event.GetWparam())
			{
			case IAPFailType.Cancel:
				popup_iap.Hide();
				AndroidReturnPlugin.instance.ClearFunc(TUIEvent_HidePopup);
				break;
			case IAPFailType.Failed:
			case IAPFailType.ServerFaild:
				popup_iap.ShowPopupYes(string.Empty);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_HidePopup);
				if (sfx_open_now)
				{
					CUISound.GetInstance().Play("UI_Button");
				}
				break;
			default:
				popup_iap.Hide();
				AndroidReturnPlugin.instance.ClearFunc(TUIEvent_HidePopup);
				break;
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneIAPEventType.TUIEvent_ServerResult)
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
			switch ((IAPFailType)m_event.GetWparam())
			{
			case IAPFailType.Cancel:
				popup_iap.Hide();
				AndroidReturnPlugin.instance.ClearFunc(TUIEvent_HidePopup);
				break;
			case IAPFailType.Failed:
			case IAPFailType.ServerFaild:
				popup_iap.ShowPopupYes(string.Empty);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_HidePopup);
				if (sfx_open_now)
				{
					CUISound.GetInstance().Play("UI_Button");
				}
				break;
			default:
				popup_iap.Hide();
				AndroidReturnPlugin.instance.ClearFunc(TUIEvent_HidePopup);
				break;
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneIAPEventType.TUIEvent_Back)
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
		else if (m_event.GetEventName() == TUIEvent.SceneIAPEventType.TUIEvent_EnterGold)
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
	}

	public void TUIEvent_IAPBuy(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			IAPItem component = control.transform.parent.GetComponent<IAPItem>();
			if (component != null)
			{
				int iD = component.GetID();
				popup_iap.ShowPopupWaiting("Loading...");
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneIAP(TUIEvent.SceneIAPEventType.TUIEvent_IAPBuy, iD));
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneIAP(TUIEvent.SceneIAPEventType.TUIEvent_Back));
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneIAP(TUIEvent.SceneIAPEventType.TUIEvent_EnterGold));
		}
	}

	public void TUIEvent_TapJoy(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneIAP(TUIEvent.SceneIAPEventType.TUIEvent_TapJoy));
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
