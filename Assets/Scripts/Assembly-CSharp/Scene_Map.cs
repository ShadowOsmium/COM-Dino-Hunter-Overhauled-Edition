using EventCenter;
using UnityEngine;

public class Scene_Map : MonoBehaviour
{
	public TUIFade m_fade;

	private float m_fade_in_time;

	private float m_fade_out_time;

	private bool do_fade_in;

	private bool is_fade_out;

	private bool do_fade_out;

	private string next_scene = "Scene_MainMenu";

	private int next_scene_id;

	public Top_Bar top_bar;

	public LevelMap level_map;

	public PopupLevel popup_level_map;

	public Camera tui_camera;

	public TUIButtonClick btn_villiage;

	private Transform level_point;

	private bool sfx_open_now = true;

	private bool music_open_now = true;

	public PopupNewHelp popup_new_help;

	public Transform level02_point;

	public Transform level03_point;

	public TUIButtonClick btn_popularize;

	public PopupPopularize popup_popularize;

	private void Awake()
	{
		CGameNetManager.GetInstance().connected = false;
		TUIDataServer.Instance().Initialize();
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.BackEvent_SceneMap>(TUIEvent_SetUIInfo);
		TUISelfAdaptiveAnchorGroup component = base.transform.GetComponent<TUISelfAdaptiveAnchorGroup>();
		if (component != null)
		{
			component.Anchor();
		}
	}

	private void Start()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMap(TUIEvent.SceneMapEventType.TUIEvent_TopBar));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMap(TUIEvent.SceneMapEventType.TUIEvent_MapEnterInfo));
		if (TUIMappingInfo.Instance().GetNewHelpState() == NewHelpState.Help09_ClickLevel02)
		{
			DoNewHelp(level02_point);
		}
		else if (TUIMappingInfo.Instance().GetNewHelpState() == NewHelpState.Help19_ClickLevel03)
		{
			DoNewHelp(level03_point);
		}
		if (TUIMappingInfo.Instance().GetNewHelpState() != NewHelpState.None && TUIMappingInfo.Instance().GetNewHelpState() != NewHelpState.Help_Over && btn_popularize != null)
		{
			btn_popularize.gameObject.SetActiveRecursively(false);
		}
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
		if (!(m_fade_out_time >= m_fade.fadeOutTime) || do_fade_out)
		{
			return;
		}
		do_fade_out = true;
		m_fade.SetFadeOutEnd();
		if (next_scene_id != 0)
		{
			TUIMappingInfo.SwitchSceneInt switchSceneInt = TUIMappingInfo.Instance().GetSwitchSceneInt();
			if (switchSceneInt != null)
			{
				switchSceneInt(next_scene_id);
			}
			CUISound.GetInstance().Stop("BGM_theme");
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
		global::EventCenter.EventCenter.Instance.Unregister<TUIEvent.BackEvent_SceneMap>(TUIEvent_SetUIInfo);
	}

	public void TUIEvent_SetUIInfo(object sender, TUIEvent.BackEvent_SceneMap m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_TopBar)
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
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_MapEnterInfo)
		{
			if (m_event.GetEventInfo() == null)
			{
				return;
			}
			if (m_event.GetEventInfo().map_info == null || top_bar == null)
			{
				Debug.Log("error!");
				return;
			}
			level_map.SetMapEnterInfo(m_event.GetEventInfo().map_info);
			MapEnterType map_enter_type = m_event.GetEventInfo().map_info.map_enter_type;
			if (map_enter_type == MapEnterType.SearchGoods)
			{
				top_bar.SetBtnBackShow(true);
				if (btn_villiage != null)
				{
					btn_villiage.gameObject.SetActiveRecursively(false);
				}
				return;
			}
			top_bar.SetBtnBackShow(false);
			if (btn_villiage != null)
			{
				btn_villiage.gameObject.SetActiveRecursively(true);
				btn_villiage.Show();
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_LevelInfo)
		{
			if (m_event.GetEventInfo() == null)
			{
				return;
			}
			TUIMapInfo map_info = m_event.GetEventInfo().map_info;
			if (map_info != null)
			{
				TUIMainLevelInfo level_info = map_info.level_info;
				if (level_info != null)
				{
					level_map.SetMainLevelInfo(level_info);
					popup_level_map.SetInfo(level_info);
					popup_level_map.Show();
					AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_ClosePopup);
				}
				else
				{
					Debug.Log("error! no map info!");
				}
			}
			else
			{
				Debug.Log("error! no map info!");
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_EnterLevel)
		{
			if (m_event.GetControlSuccess())
			{
				int wparam = m_event.GetWparam();
				if (wparam != 0)
				{
					next_scene_id = wparam;
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
			else
			{
				m_fade_in_time = 0f;
				do_fade_in = false;
				m_fade.FadeIn();
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_Back)
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
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_EnterIAP)
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
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_EnterGold)
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
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_EnterEquip)
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
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_EnterWeaponBuy)
		{
			if (m_event.GetControlSuccess())
			{
				DoSceneChange(m_event.GetWparam(), "Scene_Forge");
				return;
			}
			m_fade_in_time = 0f;
			do_fade_in = false;
			m_fade.FadeIn();
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_EnterRoleBuy)
		{
			if (m_event.GetControlSuccess())
			{
				DoSceneChange(m_event.GetWparam(), "Scene_Tavern");
				return;
			}
			m_fade_in_time = 0f;
			do_fade_in = false;
			m_fade.FadeIn();
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_EnterVilliage)
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
		else
		{
			if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_ClickPopularize)
			{
				return;
			}
			if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_SkipTutorial)
			{
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
			else if (m_event.GetEventName() == TUIEvent.SceneMapEventType.TUIEvent_EnterCoop)
			{
				if (m_event.GetControlSuccess())
				{
					DoSceneChange(m_event.GetWparam(), "Scene_CoopInputName");
					return;
				}
				m_fade_in_time = 0f;
				do_fade_in = false;
				m_fade.FadeIn();
			}
		}
	}

	public void TUIEvent_MoveScreen(TUIControl control, int event_type, float wparam, float lparam, object obj)
	{
		level_map.MoveScreen(wparam, 0f);
	}

	public void TUIEvent_ShowPopup(TUIControl control, int event_type, float wparam, float lparam, object obj)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		level_point = control.transform.parent.transform;
		LevelPoint component = level_point.GetComponent<LevelPoint>();
		if (component != null)
		{
			TUIMainLevelInfo levelInfo = component.GetLevelInfo();
			if (levelInfo == null)
			{
				int levelID = component.GetLevelID();
				Debug.Log("Open MainLevel:" + levelID);
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMap(TUIEvent.SceneMapEventType.TUIEvent_LevelInfo, levelID));
			}
			else
			{
				popup_level_map.SetInfo(component.GetLevelInfo());
				popup_level_map.Show();
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_ClosePopup);
			}
		}
	}

	public void TUIEvent_EnterLevel(TUIControl control, int event_type, float wparam, float lparam, object obj)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		popup_level_map.Hide();
		AndroidReturnPlugin.instance.ClearFunc(TUIEvent_ClosePopup);
		if (level_point == null)
		{
			Debug.Log("error!");
			return;
		}
		PopupLevel_Item choose = popup_level_map.GetChoose();
		if (!(choose == null))
		{
			int iD = choose.GetID();
			Debug.Log("Level:" + iD);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMap(TUIEvent.SceneMapEventType.TUIEvent_EnterLevel, iD));
		}
	}

	public void TUIEvent_ClickRecommend(TUIControl control, int event_type, float wparam, float lparam, object obj)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		PopupLevel_Recommend component = control.transform.parent.GetComponent<PopupLevel_Recommend>();
		PopupLevel_Recommend.RecommendBtnState recommendBtnState = component.GetRecommendBtnState();
		Debug.Log("m_btn_state:" + recommendBtnState);
		switch (recommendBtnState)
		{
		case PopupLevel_Recommend.RecommendBtnState.RoleBuy:
		{
			TUIRecommendRoleInfo recommendRoleInfo = component.GetRecommendRoleInfo();
			if (recommendRoleInfo == null)
			{
				Debug.Log("error!");
			}
			int id2 = recommendRoleInfo.id;
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMap(TUIEvent.SceneMapEventType.TUIEvent_EnterRoleBuy, id2));
			break;
		}
		case PopupLevel_Recommend.RecommendBtnState.WeaponBuy:
		{
			TUIRecommendWeaponInfo recommendWeaponInfo = component.GetRecommendWeaponInfo();
			if (recommendWeaponInfo == null)
			{
				Debug.Log("error!");
			}
			int id = recommendWeaponInfo.id;
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMap(TUIEvent.SceneMapEventType.TUIEvent_EnterWeaponBuy, id));
			break;
		}
		case PopupLevel_Recommend.RecommendBtnState.RoleEquip:
		case PopupLevel_Recommend.RecommendBtnState.WeaponEquip:
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMap(TUIEvent.SceneMapEventType.TUIEvent_EnterEquip));
			break;
		}
		if (!is_fade_out)
		{
			is_fade_out = true;
			m_fade.FadeOut();
		}
	}

	public void TUIEvent_ClosePopup(TUIControl control, int event_type, float wparam, float lparam, object obj)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			popup_level_map.Hide();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_ClosePopup);
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMap(TUIEvent.SceneMapEventType.TUIEvent_Back));
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMap(TUIEvent.SceneMapEventType.TUIEvent_EnterIAP));
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMap(TUIEvent.SceneMapEventType.TUIEvent_EnterGold));
		}
	}

	public void TUIEvent_EnterVilliage(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMap(TUIEvent.SceneMapEventType.TUIEvent_EnterVilliage));
		}
	}

	public void TUIEvent_ShowGoodsTips(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 1)
		{
			popup_level_map.ShowTips(control);
		}
		else
		{
			popup_level_map.HideTips();
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
		switch (TUIMappingInfo.Instance().GetNewHelpState())
		{
		case NewHelpState.Help09_ClickLevel02:
		{
			int wparam2 = level_map.FindSecondaryLevelInMainLevel(1002);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMap(TUIEvent.SceneMapEventType.TUIEvent_LevelInfo, wparam2));
			TUIMappingInfo.Instance().NextNewHelpState();
			NewHelpState newHelpState = TUIMappingInfo.Instance().GetNewHelpState();
			popup_new_help.SetHelpState(newHelpState, 0f);
			break;
		}
		case NewHelpState.Help10_ClickPlayLevel02:
		{
			PopupLevel_Item choose2 = popup_level_map.GetChoose();
			if (choose2 == null)
			{
				Debug.Log("error!");
				break;
			}
			int iD2 = choose2.GetID();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMap(TUIEvent.SceneMapEventType.TUIEvent_EnterLevel, iD2));
			TUIMappingInfo.Instance().NextNewHelpState();
			popup_new_help.Hide();
			popup_level_map.Hide();
			break;
		}
		case NewHelpState.Help19_ClickLevel03:
		{
			int wparam3 = level_map.FindSecondaryLevelInMainLevel(1003);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMap(TUIEvent.SceneMapEventType.TUIEvent_LevelInfo, wparam3));
			TUIMappingInfo.Instance().NextNewHelpState();
			NewHelpState newHelpState2 = TUIMappingInfo.Instance().GetNewHelpState();
			popup_new_help.SetHelpState(newHelpState2, 0f);
			break;
		}
		case NewHelpState.Help20_ClickPlayLevel03:
		{
			PopupLevel_Item choose = popup_level_map.GetChoose();
			if (choose == null)
			{
				Debug.Log("error!");
				break;
			}
			int iD = choose.GetID();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMap(TUIEvent.SceneMapEventType.TUIEvent_EnterLevel, iD));
			TUIMappingInfo.Instance().NextNewHelpState();
			popup_new_help.Hide();
			popup_level_map.Hide();
			break;
		}
		}
	}

	public void TUIEvent_OpenPopularize(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_popularize != null)
			{
				popup_popularize.Show(true);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_ClosePopularize);
			}
		}
	}

	public void TUIEvent_ClosePopularize(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_popularize != null)
			{
				popup_popularize.Show(false);
			}
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_ClosePopularize);
		}
	}

	public void TUIEvent_ClickPopularize(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (control != null && control.transform.parent != null)
		{
			PopupPopularizeItem component = control.transform.parent.GetComponent<PopupPopularizeItem>();
			if (component != null)
			{
				PopularizeType popularizeType = component.GetPopularizeType();
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMap(TUIEvent.SceneMapEventType.TUIEvent_ClickPopularize, (int)popularizeType));
			}
		}
	}

	public void TUIEvent_ClickSecondaryLevel(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 1)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (!(control == null))
			{
				popup_level_map.SetChoose(control.GetComponent<PopupLevel_Item>());
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMap(TUIEvent.SceneMapEventType.TUIEvent_SkipTutorial));
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

	public void TUIEvent_CoopMainMenu(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMap(TUIEvent.SceneMapEventType.TUIEvent_EnterCoop));
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

	private void DoNewHelp(Transform m_level_point)
	{
		if (!(popup_new_help != null))
		{
			return;
		}
		popup_new_help.ResetHelpState();
		NewHelpState newHelpState = TUIMappingInfo.Instance().GetNewHelpState();
		if (newHelpState != NewHelpState.Help_Over && newHelpState != NewHelpState.None)
		{
			popup_new_help.Show();
			if (m_level_point != null)
			{
				popup_new_help.SetHelpState(newHelpState, m_level_point.position.x);
			}
			else
			{
				popup_new_help.SetHelpState(newHelpState, 0f);
			}
			if (level_map != null)
			{
				level_map.ShowLevelCoop(false);
			}
			AndroidReturnPlugin.instance.SetSkipTutorialFunc(TUIEvent_OpenSkipTutorial);
		}
	}
}
