using EventCenter;
using UnityEngine;

public class Scene_CoopInputName : MonoBehaviour
{
	private const int namelength = 12;

	public TUIFade m_fade;

	private float m_fade_in_time;

	private float m_fade_out_time;

	private bool do_fade_in;

	private bool is_fade_out;

	private bool do_fade_out;

	private string next_scene = string.Empty;

	public Top_Bar top_bar;

	public TUILabel label_input;

	public PopupNewHelpState popup_new_help;

	public TUIButtonClick btn_continue;

	private string player_name = string.Empty;

	private bool sfx_open_now = true;

	private bool music_open_now = true;

	private void Awake()
	{
		if (m_fade == null)
		{
			Debug.Log("error!no found m_fade!");
		}
		TUIDataServer.Instance().Initialize();
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.BackEvent_SceneCoopInputName>(TUIEvent_SetUIInfo);
	}

	private void Start()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopInputName(TUIEvent.SceneCoopInputNameEventType.TUIEvent_TopBar));
		DoNewHelp();
		if (top_bar != null)
		{
			top_bar.SetBtnGoldShow(false);
			top_bar.SetBtnCrystalShow(false);
		}
		OpenBtnContinue(false);
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopInputName(TUIEvent.SceneCoopInputNameEventType.TUIEvent_Continue, "Test Account"));
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
		UpdateKeyboard();
	}

	private void OnDestroy()
	{
		global::EventCenter.EventCenter.Instance.Unregister<TUIEvent.BackEvent_SceneCoopInputName>(TUIEvent_SetUIInfo);
	}

	public void TUIEvent_SetUIInfo(object sender, TUIEvent.BackEvent_SceneCoopInputName m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneCoopInputNameEventType.TUIEvent_TopBar)
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
		else if (m_event.GetEventName() == TUIEvent.SceneCoopInputNameEventType.TUIEvent_Continue)
		{
			if (m_event.GetControlSuccess())
			{
				DoSceneChange(m_event.GetWparam(), "Scene_CoopMainMenu");
				return;
			}
			m_fade_in_time = 0f;
			do_fade_in = false;
			m_fade.FadeIn();
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopInputNameEventType.TUIEvent_Back)
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
	}

	public void TUIEvent_OpenInputName(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (label_input != null)
			{
				OpenKeyBoard(label_input.Text);
			}
		}
	}

	public void TUIEvent_ClickNewHelp(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3 && popup_new_help != null)
		{
			if (popup_new_help.IsTextOver())
			{
				popup_new_help.gameObject.SetActiveRecursively(false);
			}
			else
			{
				popup_new_help.ShowTextScroll();
			}
		}
	}

	public void TUIEvent_ClickBtnContinue(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopInputName(TUIEvent.SceneCoopInputNameEventType.TUIEvent_Continue, player_name));
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopInputName(TUIEvent.SceneCoopInputNameEventType.TUIEvent_Back));
		}
	}

	private void DoNewHelp()
	{
		if (popup_new_help != null)
		{
			popup_new_help.Show(0f);
		}
	}

	private void OpenBtnContinue(bool m_open)
	{
		if (btn_continue != null)
		{
			btn_continue.Disable(!m_open);
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

	public void OpenKeyBoard(string text)
	{
		IphoneInputPlugin.GetInstance().Open("Enter a name(max 12 letters or numbers)", text, 12, OnKeyBoardDone, TouchScreenKeyboardType.ASCIICapable);
	}

	protected void OnKeyBoardDone(string sValue)
	{
		if (sValue.Length >= 2)
		{
			player_name = sValue;
			label_input.Text = player_name;
			OpenBtnContinue(true);
		}
		else
		{
			OpenBtnContinue(false);
		}
	}

	private void UpdateKeyboard()
	{
		IphoneInputPlugin.GetInstance().Update(Time.deltaTime);
	}
}
