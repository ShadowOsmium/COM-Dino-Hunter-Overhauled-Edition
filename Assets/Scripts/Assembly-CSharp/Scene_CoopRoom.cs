using System.Collections.Generic;
using EventCenter;
using UnityEngine;

public class Scene_CoopRoom : MonoBehaviour
{
	public TUIFade m_fade;

	private float m_fade_in_time;

	private float m_fade_out_time;

	private bool do_fade_in;

	private bool is_fade_out;

	private bool do_fade_out;

	private string next_scene = string.Empty;

	public Top_Bar top_bar;

	private bool sfx_open_now = true;

	private bool music_open_now = true;

	public PopupRoomPlayerInfo role_player_info01;

	public PopupRoomPlayerInfo role_player_info02;

	public PopupRoomPlayerInfo role_player_info03;

	public PopupReciprocal popup_reciprocal;

	public PopupOnlyText popup_start_warning;

	public TUIButtonClick btn_enter;

	public TUIButtonClick btn_start;

	public TUIButtonClick btn_exit;

	private int scene_id;

	private PopupRoomPlayerInfo my_role_info;

	private Dictionary<int, string> title_list;

	private void Awake()
	{
		if (m_fade == null)
		{
			Debug.Log("error!no found m_fade!");
		}
		TUIDataServer.Instance().Initialize();
		TUIMappingInfo.Instance().SetCurrentScene(base.gameObject);
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.BackEvent_SceneCoopRoom>(TUIEvent_SetUIInfo);
	}

	private void Start()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_TopBar));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_EnterInfo));
		if (top_bar != null)
		{
			top_bar.SetBtnGoldShow(false);
			top_bar.SetBtnCrystalShow(false);
		}
		if (popup_reciprocal != null)
		{
			popup_reciprocal.StartReciprocal(false);
		}
		if (btn_enter != null)
		{
			btn_enter.gameObject.SetActiveRecursively(false);
		}
		if (btn_exit != null)
		{
			btn_exit.gameObject.SetActiveRecursively(false);
		}
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
					TUIMappingInfo.Instance().SetCurrentScene(base.gameObject);
					switchSceneStr(next_scene);
				}
			}
		}
		CheckReciprocal();
		CheckRoleSpeedOver();
	}

	private void OnDestroy()
	{
		global::EventCenter.EventCenter.Instance.Unregister<TUIEvent.BackEvent_SceneCoopRoom>(TUIEvent_SetUIInfo);
	}

	public void TUIEvent_SetUIInfo(object sender, TUIEvent.BackEvent_SceneCoopRoom m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneCoopRoomEventType.TUIEvent_TopBar)
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
		else if (m_event.GetEventName() == TUIEvent.SceneCoopRoomEventType.TUIEvent_EnterInfo)
		{
			if (m_event.GetEventInfo() != null)
			{
				TUICoopTitleListInfo coop_title_list_info = m_event.GetEventInfo().coop_title_list_info;
				if (coop_title_list_info != null)
				{
					title_list = coop_title_list_info.title_list;
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopRoomEventType.TUIEvent_IamEnter)
		{
			if (m_event.GetEventInfo() != null)
			{
				TUICoopPlayerInfo coop_player_info = m_event.GetEventInfo().coop_player_info;
				DoPlayerEnter(coop_player_info, true);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopRoomEventType.TUIEvent_PlayerEnter)
		{
			if (m_event.GetEventInfo() != null)
			{
				TUICoopPlayerInfo coop_player_info2 = m_event.GetEventInfo().coop_player_info;
				DoPlayerEnter(coop_player_info2);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopRoomEventType.TUIEvent_PlayerExit)
		{
			string str = m_event.GetStr();
			DoPlayerExit(str);
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopRoomEventType.TUIEvent_GameStart)
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
		else if (m_event.GetEventName() == TUIEvent.SceneCoopRoomEventType.TUIEvent_Back)
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
		else if (m_event.GetEventName() == TUIEvent.SceneCoopRoomEventType.TUIEvent_ShowBtnStart)
		{
			if (btn_start != null)
			{
				btn_start.gameObject.SetActiveRecursively(m_event.GetControlSuccess());
				if (m_event.GetControlSuccess())
				{
					btn_start.Reset();
				}
			}
		}
		else
		{
			if (m_event.GetEventName() != TUIEvent.SceneCoopRoomEventType.TUIEvent_ShowStartWarning)
			{
				return;
			}
			bool controlSuccess = m_event.GetControlSuccess();
			if (popup_start_warning != null)
			{
				if (controlSuccess)
				{
					popup_start_warning.Show();
					AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_GameStartCancel);
				}
				else
				{
					popup_start_warning.Hide();
					AndroidReturnPlugin.instance.ClearFunc(TUIEvent_GameStartCancel);
				}
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_Back));
		}
	}

	public void TUIEvent_GameStartBtn(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			int num = 0;
			if (role_player_info01 != null && role_player_info01.GetPlayerInfoExist())
			{
				num++;
			}
			if (role_player_info02 != null && role_player_info02.GetPlayerInfoExist())
			{
				num++;
			}
			if (role_player_info03 != null && role_player_info03.GetPlayerInfoExist())
			{
				num++;
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_GameStartBtn));
		}
	}

	public void TUIEvent_PlayerEnter(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
		}
	}

	public void TUIEvent_PlayerExit(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
		}
	}

	public void TUIEvent_MoveScreen(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 2 && my_role_info != null)
		{
			my_role_info.SetRoleRotation(wparam, lparam);
		}
	}

	public void TUIEvent_GameStartYes(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_start_warning != null)
			{
				popup_start_warning.Hide();
			}
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_GameStartCancel);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_GameStartYes));
		}
	}

	public void TUIEvent_GameStartCancel(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_start_warning != null)
			{
				popup_start_warning.Hide();
			}
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_GameStartCancel);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_GameStartCancel));
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

	public void DoPlayerEnter(TUICoopPlayerInfo m_info, bool m_mine = false)
	{
		if (!(role_player_info01 != null) || !(role_player_info02 != null) || !(role_player_info03 != null))
		{
			return;
		}
		if (!role_player_info01.GetPlayerInfoExist())
		{
			role_player_info01.SetInfo(m_info, title_list, 1);
			if (m_mine)
			{
				my_role_info = role_player_info01;
			}
		}
		else if (!role_player_info02.GetPlayerInfoExist())
		{
			role_player_info02.SetInfo(m_info, title_list, 2);
			if (m_mine)
			{
				my_role_info = role_player_info02;
			}
		}
		else if (!role_player_info03.GetPlayerInfoExist())
		{
			role_player_info03.SetInfo(m_info, title_list, 3);
			if (m_mine)
			{
				my_role_info = role_player_info03;
			}
		}
	}

	public void DoPlayerExit(string m_id)
	{
		if (role_player_info01 != null && role_player_info02 != null && role_player_info03 != null)
		{
			if (role_player_info01.GetPlayerID() == m_id)
			{
				role_player_info01.SetPlayerNull(1);
			}
			else if (role_player_info02.GetPlayerID() == m_id)
			{
				role_player_info02.SetPlayerNull(2);
			}
			else if (role_player_info03.GetPlayerID() == m_id)
			{
				role_player_info03.SetPlayerNull(3);
			}
		}
	}

	public void CheckReciprocal()
	{
		if (popup_reciprocal != null && popup_reciprocal.GetReciprocalOver())
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Entergame");
			}
			DoSceneChange(scene_id, "Scene_MainMenu");
		}
	}

	public void CheckRoleSpeedOver()
	{
		if (role_player_info01 != null && role_player_info01.Event_TextShowOver())
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_LastRoleSpeedOver));
		}
		if (role_player_info02 != null && role_player_info02.Event_TextShowOver())
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_LastRoleSpeedOver));
		}
		if (role_player_info03 != null && role_player_info03.Event_TextShowOver())
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_LastRoleSpeedOver));
		}
	}
}
