using EventCenter;
using UnityEngine;

public class Scene_Main : MonoBehaviour
{
	public TUIFade m_fade;

	private float m_fade_in_time;

	private float m_fade_out_time;

	private bool do_fade_in;

	private bool is_fade_out;

	private bool do_fade_out;

	private string next_scene = "Scene_MainMenu";

	private int next_scene_id;

	private bool is_enter_level_scene;

	private bool sfx_open_now = true;

	private bool music_open_now = true;

	public TUILabel label_text;

	public PopupIAP popup_warning;

	private bool connect_success;

	private ServerConnectFailType server_connect_fail;

	public GameObject prefab_popup_server;

	private PopupServer popup_server;

	public Transform tui_control;

	private bool didTheThing;

	private void Awake()
	{
		Application.targetFrameRate = 240;
		TUIDataServer.Instance().Initialize();
		//global::EventCenter.EventCenter.Instance.Register<TUIEvent.BackEvent_SceneMain>(TUIEvent_SetUIInfo);
		label_text.Text = "Touch to play";
	}

	private void Start()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_EnterInfo));
		if (music_open_now)
		{
			CUISound.GetInstance().Play("BGM_theme");
		}
	}

	private void Update()
	{
		if (Input2.touchCount > 0 && !didTheThing)
		{
			is_fade_out = true;
			m_fade.FadeOut();
			next_scene = "Scene_MainMenu";
			CUISound.GetInstance().Play("UI_Entergame");
			didTheThing = true;
		}
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
		if (!(m_fade_out_time >= m_fade.fadeOutTime) || do_fade_out)
		{
			return;
		}
		do_fade_out = true;
		m_fade.SetFadeOutEnd();
		if (is_enter_level_scene)
		{
			CUISound.GetInstance().Stop("BGM_theme");
		}
		if (is_enter_level_scene)
		{
			TUIMappingInfo.SwitchSceneInt switchSceneInt = TUIMappingInfo.Instance().GetSwitchSceneInt();
			if (switchSceneInt != null)
			{
				switchSceneInt(next_scene_id);
			}
		}
		else
		{
			TUIMappingInfo.SwitchSceneStr switchSceneStr = TUIMappingInfo.Instance().GetSwitchSceneStr();
			if (switchSceneStr != null)
			{
				switchSceneStr(next_scene);
			}
		}
	}

	private void OnDestroy()
	{
		global::EventCenter.EventCenter.Instance.Unregister<TUIEvent.BackEvent_SceneMain>(TUIEvent_SetUIInfo);
	}

	public void TUIEvent_SetUIInfo(object sender, TUIEvent.BackEvent_SceneMain m_event)
	{
		is_enter_level_scene = m_event.GetControlSuccess();
		if (m_event.GetControlSuccess())
		{
			int wparam = m_event.GetWparam();
			next_scene_id = wparam;
			if (!is_fade_out)
			{
				is_fade_out = true;
				m_fade.FadeOut();
			}
			return;
		}
		int wparam2 = m_event.GetWparam();
		string sceneName = TUIMappingInfo.Instance().GetSceneName(wparam2);
		if (sceneName != string.Empty)
		{
			next_scene = sceneName;
		}
		else
		{
			next_scene = "Scene_MainMenu";
		}
		if (!is_fade_out)
		{
			is_fade_out = true;
			m_fade.FadeOut();
		}
	}

	public void TUIEvent_Enter(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3 && connect_success)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Entergame");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_EnterLevel));
		}
	}

	public void TUIEvent_CloseWarnning(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_warning != null)
			{
				popup_warning.Hide();
			}
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseWarnning);
			if (server_connect_fail == ServerConnectFailType.NeedNet)
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ConnectAgain));
			}
			else if (server_connect_fail == ServerConnectFailType.NeedUpdate)
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_GotoUpdate));
			}
			else if (server_connect_fail == ServerConnectFailType.FetchFailed)
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_FetchFailed));
			}
			else if (server_connect_fail == ServerConnectFailType.GMUsing)
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_GMUsing));
			}
			else if (server_connect_fail == ServerConnectFailType.ServerMaintain)
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ServerMaintain));
			}
			else
			{
				Debug.Log("error!");
			}
		}
	}

	public void TUIEvent_PopupServerOK(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_PopupServerOK));
		}
	}

	public void TUIEvent_PopupServerCancle(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_PopupServerCancle));
		}
	}
}
