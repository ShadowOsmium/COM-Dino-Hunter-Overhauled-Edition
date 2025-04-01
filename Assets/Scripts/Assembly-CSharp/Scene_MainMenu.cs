using System.Collections.Generic;
using EventCenter;
using UnityEngine;

public class Scene_MainMenu : MonoBehaviour
{
	private enum CameraStopType
	{
		StopCamp,
		StopForge,
		StopSkills,
		StopStash,
		StopTavern,
		StopBlackMarket
	}

	public TUIFade m_fade;

	public Camera_Village camera_village;

	public Popup_Option popup_option;

	public Popup_Achievement popup_achievement;

	public Popup_DailyMissions popup_daily_missions;

	public Top_Bar top_bar;

	public Transform go_forge;

	public Transform go_forge_name;

	public Transform go_forge_new;

	public Transform go_forge_mark;

	public Transform go_tavern;

	public Transform go_tavern_name;

	public Transform go_tavern_new;

	public Transform go_tavern_mark;

	public Transform go_skill;

	public Transform go_skill_name;

	public Transform go_skill_new;

	public Transform go_skill_mark;

	public Transform go_stash;

	public Transform go_stash_name;

	public Transform go_stash_new;

	public Transform go_stash_mark;

	public Transform go_camp;

	public Transform go_camp_name;

	public Transform go_camp_new;

	public Transform go_camp_mark;

	public Transform go_blackmarket;

	public Transform go_blackmarket_name;

	public Transform go_blackmarket_new;

	public Transform go_blackmarket_mark;

	public UnlockBlink unlock_blink;

	public TUILabel label_finished;

	public TUIMeshSprite img_map_bg;

	public TUIMeshSprite img_coop_bg;

	public GameObject effect_achievement_bg;

	public GameObject effect_sale_bg;

	public GameObject effect_daily_missions_bg;

	public TUIMeshSprite img_arrow_left;

	public TUIMeshSprite img_arrow_right;

	public Popup_Credits popup_credits;

	public Popup_Review popup_review;

	public PopupNewHelp popup_new_help;

	private Transform go_control;

	private bool is_click;

	private float m_fade_in_time;

	private float m_fade_out_time;

	private bool do_fade_in;

	private bool is_fade_out;

	private bool do_fade_out;

	private string next_scene = string.Empty;

	private bool sfx_open_now = true;

	private bool music_open_now = true;

	private List<TUIUnlockInfo> unlock_list;

	public Popup_Sale popup_sale;

	public TUIButtonClick btn_sale;

	public DailyLoginBonus popup_daily_bonus;

	public Dictionary<int, string> title_list;

	public TUIButtonClick btn_coop;

	private void Awake()
	{
		CGameNetManager.GetInstance().connected = false;
		TUIDataServer.Instance().Initialize();
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.BackEvent_SceneMainMenu>(TUIEvent_SetUIInfo);
		camera_village.SetCurrentAngle(TUIMappingInfo.Instance().GetCurrentAngle());
		OpenMapBlink(true);
		OpenCoopBlink(true);
		if (img_arrow_left != null && img_arrow_left.GetComponent<Animation>() != null)
		{
			img_arrow_left.GetComponent<Animation>().wrapMode = WrapMode.Loop;
			img_arrow_left.GetComponent<Animation>().Play();
		}
		if (img_arrow_right != null && img_arrow_right.GetComponent<Animation>() != null)
		{
			img_arrow_right.GetComponent<Animation>().wrapMode = WrapMode.Loop;
			img_arrow_right.GetComponent<Animation>().Play();
		}
	}

	private void Start()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_TopBar));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_OptionInfo));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_AcheviementInfo));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_SaleInfo));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_DailyLoginBonusInfo));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_DailyMissionsInfo));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_EnterInfo));
		DoNewHelp();
	}

	private void Update()
	{
		LookAtCamera();
		UpdateArrowControl();
		if (m_fade == null)
		{
			Debug.Log("error! no found m_fade!");
			return;
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
			TUIMappingInfo.Instance().SetCurrentAngle(camera_village.GetCurrentAngle());
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
		global::EventCenter.EventCenter.Instance.Unregister<TUIEvent.BackEvent_SceneMainMenu>(TUIEvent_SetUIInfo);
	}

	public void TUIEvent_SetUIInfo(object sender, TUIEvent.BackEvent_SceneMainMenu m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_TopBar)
		{
			if (m_event.GetEventInfo().player_info != null)
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
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_OptionInfo)
		{
			if (m_event.GetEventInfo() != null && m_event.GetEventInfo().option_info != null)
			{
				bool music_open = m_event.GetEventInfo().option_info.music_open;
				bool sfx_open = m_event.GetEventInfo().option_info.sfx_open;
				popup_option.SetOption(music_open, sfx_open);
				sfx_open_now = sfx_open;
				music_open_now = music_open;
			}
			else
			{
				Debug.Log("error!");
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_AcheviementInfo)
		{
			if (m_event.GetEventInfo() != null && popup_achievement != null)
			{
				popup_achievement.DoCreateAchievement(m_event.GetEventInfo().achievement_info, base.gameObject);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_TakeAchievement)
		{
			if (m_event.GetControlSuccess())
			{
				if (sfx_open_now)
				{
					CUISound.GetInstance().Play("UI_Levelup");
				}
				TUIAchievementRewardInfo tUIAchievementRewardInfo = popup_achievement.TakeAchievement();
				if (tUIAchievementRewardInfo == null)
				{
					Debug.Log("error!");
					return;
				}
				TakeAchievement(tUIAchievementRewardInfo, top_bar);
				popup_achievement.AfterTakeAchievement();
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterInfo)
		{
			if (m_event.GetEventInfo() != null && m_event.GetEventInfo().villiage_enter_info != null)
			{
				TUIVilliageEnterInfo villiage_enter_info = m_event.GetEventInfo().villiage_enter_info;
				TUICoopTitleListInfo coop_title_list_info = m_event.GetEventInfo().coop_title_list_info;
				if (coop_title_list_info != null)
				{
					title_list = coop_title_list_info.title_list;
				}
				unlock_list = villiage_enter_info.unlock_list;
				if (label_finished != null)
				{
					label_finished.Text = villiage_enter_info.finished_text;
				}
				if (villiage_enter_info.equip_sign == NewMarkType.New)
				{
					go_camp_new.gameObject.SetActiveRecursively(true);
					go_camp_mark.gameObject.SetActiveRecursively(false);
				}
				else if (villiage_enter_info.equip_sign == NewMarkType.Mark)
				{
					go_camp_new.gameObject.SetActiveRecursively(false);
					go_camp_mark.gameObject.SetActiveRecursively(true);
				}
				else
				{
					go_camp_new.gameObject.SetActiveRecursively(false);
					go_camp_mark.gameObject.SetActiveRecursively(false);
				}
				if (villiage_enter_info.forge_sign == NewMarkType.New)
				{
					go_forge_new.gameObject.SetActiveRecursively(true);
					go_forge_mark.gameObject.SetActiveRecursively(false);
				}
				else if (villiage_enter_info.forge_sign == NewMarkType.Mark)
				{
					go_forge_new.gameObject.SetActiveRecursively(false);
					go_forge_mark.gameObject.SetActiveRecursively(true);
				}
				else
				{
					go_forge_new.gameObject.SetActiveRecursively(false);
					go_forge_mark.gameObject.SetActiveRecursively(false);
				}
				if (villiage_enter_info.skill_sign == NewMarkType.New)
				{
					go_skill_new.gameObject.SetActiveRecursively(true);
					go_skill_mark.gameObject.SetActiveRecursively(false);
				}
				else if (villiage_enter_info.skill_sign == NewMarkType.Mark)
				{
					go_skill_new.gameObject.SetActiveRecursively(false);
					go_skill_mark.gameObject.SetActiveRecursively(true);
				}
				else
				{
					go_skill_new.gameObject.SetActiveRecursively(false);
					go_skill_mark.gameObject.SetActiveRecursively(false);
				}
				if (villiage_enter_info.tavern_sign == NewMarkType.New)
				{
					go_tavern_new.gameObject.SetActiveRecursively(true);
					go_tavern_mark.gameObject.SetActiveRecursively(false);
				}
				else if (villiage_enter_info.tavern_sign == NewMarkType.Mark)
				{
					go_tavern_new.gameObject.SetActiveRecursively(false);
					go_tavern_mark.gameObject.SetActiveRecursively(true);
				}
				else
				{
					go_tavern_new.gameObject.SetActiveRecursively(false);
					go_tavern_mark.gameObject.SetActiveRecursively(false);
				}
				if (villiage_enter_info.stash_sign == NewMarkType.New)
				{
					go_stash_new.gameObject.SetActiveRecursively(true);
					go_stash_mark.gameObject.SetActiveRecursively(false);
				}
				else if (villiage_enter_info.stash_sign == NewMarkType.Mark)
				{
					go_stash_new.gameObject.SetActiveRecursively(false);
					go_stash_mark.gameObject.SetActiveRecursively(true);
				}
				else
				{
					go_stash_new.gameObject.SetActiveRecursively(false);
					go_stash_mark.gameObject.SetActiveRecursively(false);
				}
				if (villiage_enter_info.blackmarket_sign == NewMarkType.New)
				{
					go_blackmarket_new.gameObject.SetActiveRecursively(true);
					go_blackmarket_mark.gameObject.SetActiveRecursively(false);
				}
				else if (villiage_enter_info.blackmarket_sign == NewMarkType.Mark)
				{
					go_blackmarket_new.gameObject.SetActiveRecursively(false);
					go_blackmarket_mark.gameObject.SetActiveRecursively(true);
				}
				else
				{
					go_blackmarket_new.gameObject.SetActiveRecursively(false);
					go_blackmarket_mark.gameObject.SetActiveRecursively(false);
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_ChangeMusic)
		{
			if (m_event.GetControlSuccess())
			{
				popup_option.SetMusicNow();
				music_open_now = popup_option.GetMusicNow();
				CUISound.GetInstance().Stop("BGM_theme");
				if (music_open_now)
				{
					CUISound.GetInstance().Play("BGM_theme");
				}
			}
			else
			{
				popup_option.RestoreOption();
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_ChangeSFX)
		{
			if (m_event.GetControlSuccess())
			{
				popup_option.SetSFXNow();
				sfx_open_now = popup_option.GetSFXNow();
			}
			else
			{
				popup_option.RestoreOption();
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterIAP)
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
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterGold)
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
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterEquip)
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
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterForge)
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
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterTavern)
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
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterSkill)
		{
			if (m_event.GetControlSuccess())
			{
				DoSceneChange(m_event.GetWparam(), "Scene_Skill");
				return;
			}
			m_fade_in_time = 0f;
			do_fade_in = false;
			m_fade.FadeIn();
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterStash)
		{
			if (m_event.GetControlSuccess())
			{
				DoSceneChange(m_event.GetWparam(), "Scene_Stash");
				return;
			}
			m_fade_in_time = 0f;
			do_fade_in = false;
			m_fade.FadeIn();
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterBlackMarket)
		{
			if (m_event.GetControlSuccess())
			{
				DoSceneChange(m_event.GetWparam(), "Scene_BlackMarket");
				return;
			}
			m_fade_in_time = 0f;
			do_fade_in = false;
			m_fade.FadeIn();
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterMap)
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
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_ShowReview)
		{
			if (popup_review != null)
			{
				popup_review.Show();
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseReview);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_HadAchievementReward)
		{
			if (m_event.GetControlSuccess())
			{
				OpenAchievementBlink(true);
			}
			else
			{
				OpenAchievementBlink(false);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_SaleInfo)
		{
			if (m_event.GetEventInfo() == null || !(popup_sale != null))
			{
				return;
			}
			TUIAllSaleInfo all_sale_info = m_event.GetEventInfo().all_sale_info;
			popup_sale.DoCreate(all_sale_info, base.gameObject);
			if (all_sale_info != null && all_sale_info.all_sale_info != null && all_sale_info.all_sale_info.Count > 0)
			{
				if (btn_sale != null)
				{
					btn_sale.gameObject.SetActiveRecursively(true);
					btn_sale.Reset();
				}
				OpenSaleBlink(true);
			}
			else if (btn_sale != null)
			{
				btn_sale.gameObject.SetActiveRecursively(false);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_ShowSale)
		{
			if (m_event.GetControlSuccess())
			{
				if (popup_sale != null)
				{
					popup_sale.Show();
					AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseSale);
				}
			}
			else if (popup_sale != null)
			{
				popup_sale.Hide();
				AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseSale);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterSale)
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
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_DailyLoginBonusInfo)
		{
			if (m_event.GetEventInfo() != null && popup_daily_bonus != null)
			{
				popup_daily_bonus.SetInfo(m_event.GetEventInfo().daily_bonus_info);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_ShowDailyLoginBonus)
		{
			if (m_event.GetControlSuccess())
			{
				if (popup_daily_bonus != null)
				{
					popup_daily_bonus.Show();
				}
			}
			else if (popup_daily_bonus != null)
			{
				popup_daily_bonus.Hide();
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_ClickDailyLoginBonus)
		{
			if (m_event.GetControlSuccess())
			{
				if (sfx_open_now)
				{
					CUISound.GetInstance().Play("UI_Levelup");
				}
				if (popup_daily_bonus != null)
				{
					popup_daily_bonus.Hide();
					top_bar.AddGoldOrCrystal(popup_daily_bonus.GetDailyLoginBonusInfo(), sfx_open_now);
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_DailyMissionsInfo)
		{
			if (m_event.GetEventInfo() != null)
			{
				popup_daily_missions.DoCreateDailyMissions(m_event.GetEventInfo().daily_missions_info, base.gameObject);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_TakeDailyMissionsReward)
		{
			if (m_event.GetControlSuccess() && popup_daily_missions != null)
			{
				TUIAchievementRewardInfo tUIAchievementRewardInfo2 = popup_daily_missions.TakeReward();
				if (tUIAchievementRewardInfo2 == null)
				{
					Debug.Log("error!");
					return;
				}
				TakeDailyMissionsReward(tUIAchievementRewardInfo2, top_bar);
				popup_daily_missions.AfterTakeReward();
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_HadDailyMissionsReward)
		{
			if (m_event.GetControlSuccess())
			{
				OpenDailyMissionsBlink(true);
			}
			else
			{
				OpenDailyMissionsBlink(false);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_ShowUnlockItem)
		{
			ChangeUnlockItem();
		}
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_SkipTutorial)
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
		else if (m_event.GetEventName() == TUIEvent.SceneMainMenuEventType.TUIEvent_EnterCoop)
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

	public void TUIEvent_CameraMove(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		switch (event_type)
		{
		case 1:
		{
			if (go_control != null)
			{
				break;
			}
			camera_village.DoBegin();
			Vector2 clickPosition2 = control.GetComponent<TUIMoveEx>().GetClickPosition();
			Ray ray2 = camera_village.GetComponent<Camera>().ScreenPointToRay(new Vector3(clickPosition2.x, clickPosition2.y, 0f));
			Debug.DrawRay(ray2.origin, ray2.direction * 600f, Color.green);
			RaycastHit hitInfo2;
			if (Physics.Raycast(ray2, out hitInfo2))
			{
				Debug.Log("you hit: " + hitInfo2.transform.name);
				if (hitInfo2.transform == go_camp)
				{
					next_scene = "Scene_Equip";
					go_control = go_camp_name;
					PlayForwardAnimation(go_camp_name);
				}
				else if (hitInfo2.transform == go_forge)
				{
					next_scene = "Scene_Forge";
					go_control = go_forge_name;
					PlayForwardAnimation(go_forge_name);
				}
				else if (hitInfo2.transform == go_tavern)
				{
					next_scene = "Scene_Tavern";
					go_control = go_tavern_name;
					PlayForwardAnimation(go_tavern_name);
				}
				else if (hitInfo2.transform == go_skill)
				{
					next_scene = "Scene_Skill";
					go_control = go_skill_name;
					PlayForwardAnimation(go_skill_name);
				}
				else if (hitInfo2.transform == go_stash)
				{
					next_scene = "Scene_Stash";
					go_control = go_stash_name;
					PlayForwardAnimation(go_stash_name);
				}
				else if (hitInfo2.transform == go_blackmarket)
				{
					next_scene = "Scene_Stash";
					go_control = go_blackmarket_name;
					PlayForwardAnimation(go_blackmarket_name);
				}
			}
			break;
		}
		case 2:
			if (!is_click)
			{
				PlayBackwardAnimation(go_control);
				go_control = null;
				camera_village.DoMoveBegin();
			}
			break;
		case 3:
			if (!is_click)
			{
				camera_village.DoMove(wparam);
			}
			break;
		case 4:
			if (!is_click)
			{
				camera_village.DoMoveEnd();
			}
			break;
		case 5:
		{
			PlayBackwardAnimation(go_control);
			Vector2 clickPosition = control.GetComponent<TUIMoveEx>().GetClickPosition();
			Ray ray = camera_village.GetComponent<Camera>().ScreenPointToRay(new Vector3(clickPosition.x, clickPosition.y, 0f));
			Debug.DrawRay(ray.origin, ray.direction * 300f, Color.green);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo))
			{
				if (hitInfo.transform == go_camp)
				{
					camera_village.SetCloser(go_camp);
					is_click = true;
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_EnterEquip));
				}
				else if (hitInfo.transform == go_forge)
				{
					camera_village.SetCloser(go_forge);
					is_click = true;
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_EnterForge));
				}
				else if (hitInfo.transform == go_tavern)
				{
					camera_village.SetCloser(go_tavern);
					is_click = true;
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_EnterTavern));
				}
				else if (hitInfo.transform == go_skill)
				{
					camera_village.SetCloser(go_skill);
					is_click = true;
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_EnterSkill));
				}
				else if (hitInfo.transform == go_stash)
				{
					camera_village.SetCloser(go_stash);
					is_click = true;
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_EnterStash));
				}
				else if (hitInfo.transform == go_blackmarket)
				{
					camera_village.SetCloser(go_blackmarket);
					is_click = true;
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_EnterBlackMarket));
				}
			}
			break;
		}
		}
	}

	public void TUIEvent_Acheviement(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_achievement != null)
			{
				popup_achievement.Show();
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseAchievement);
			}
		}
	}

	public void TUIEvent_DailyMissions(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if ((bool)popup_daily_missions)
			{
				popup_daily_missions.Show();
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseDailyMissions);
			}
		}
	}

	public void TUIEvent_Option(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			popup_option.Show();
			AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseOption);
		}
	}

	public void TUIEvent_BtnMusic(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 1 || event_type == 2)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_ChangeMusic));
		}
	}

	public void TUIEvent_BtnSFX(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 1 || event_type == 2)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_ChangeSFX));
		}
	}

	public void TUIEvent_BtnForum(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_Forum));
		}
	}

	public void TUIEvent_TakeAchievement(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
			CUISound.GetInstance().Play("UI_Coin_get");
		}
		if (control.transform.parent == null)
		{
			Debug.Log("error!");
			return;
		}
		AchievementItem component = control.transform.parent.GetComponent<AchievementItem>();
		if (component == null)
		{
			Debug.Log("error!");
			return;
		}
		popup_achievement.SetTakeAchievementBtn(control);
		int iD = component.GetID();
		int achievementLevel = (int)component.GetAchievementLevel();
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_TakeAchievement, iD, achievementLevel));
	}

	public void TUIEvent_TakeDailyMissionsAward(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
			CUISound.GetInstance().Play("UI_Coin_get");
		}
		if (control.transform.parent == null)
		{
			Debug.Log("error!");
			return;
		}
		DailyMissionsItem component = control.transform.parent.GetComponent<DailyMissionsItem>();
		if (component == null)
		{
			Debug.Log("error!");
			return;
		}
		popup_daily_missions.SetClaimBtn(control);
		int iD = component.GetID();
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_TakeDailyMissionsReward, iD));
	}

	public void TUIEvent_CloseAchievement(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			if (popup_achievement != null)
			{
				popup_achievement.Hide();
			}
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseAchievement);
		}
	}

	public void TUIEvent_CloseDailyMissions(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			if (popup_daily_missions != null)
			{
				popup_daily_missions.Hide();
			}
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseDailyMissions);
		}
	}

	public void TUIEvent_CloseOption(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			popup_option.Hide();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseOption);
		}
	}

	public void TUIEvent_OpenCredits(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_credits != null)
			{
				popup_credits.Show();
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseCredits);
			}
		}
	}

	public void TUIEvent_CloseCredits(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			if (popup_credits != null)
			{
				popup_credits.Hide();
			}
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseCredits);
		}
	}

	public void TUIEvent_OpenSupport(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_OpenSupportURL));
		}
	}

	public void TUIEvent_CloseReview(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			if (popup_review != null)
			{
				popup_review.Hide();
			}
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseReview);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_CloseReviewURL));
		}
	}

	public void TUIEvent_ClickReview(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_OpenReviewURL));
			popup_review.Hide();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseReview);
		}
	}

	public void TUIEvent_OpenSale(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_sale != null)
			{
				popup_sale.Show();
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseSale);
			}
		}
	}

	public void TUIEvent_CloseSale(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			if (popup_sale != null)
			{
				popup_sale.Hide();
			}
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseSale);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_CloseSale));
		}
	}

	public void TUIEvent_ClickSaleLink(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (control.transform.parent == null)
		{
			return;
		}
		Popup_Sale_Item component = control.transform.parent.GetComponent<Popup_Sale_Item>();
		if (component != null)
		{
			TUISingleSaleInfo saleInfo = component.GetSaleInfo();
			if (saleInfo != null)
			{
				OnSaleType sale_type = saleInfo.sale_type;
				int id = saleInfo.id;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_EnterSale, (int)sale_type, id));
			}
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_EnterIAP));
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_EnterGold));
		}
	}

	public void TUIEvent_Map(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Entergame");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_EnterMap));
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
			unlock_blink.CloseBlink();
			ChangeUnlockItem();
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
		if (popup_new_help == null)
		{
			Debug.Log("error!");
			return;
		}
		if (control == popup_new_help.GetBtnMask())
		{
			popup_new_help.DoBtnMaskEvent();
			return;
		}
		switch (TUIMappingInfo.Instance().GetNewHelpState())
		{
		case NewHelpState.Help01_ClickEnterForge:
			camera_village.SetCloser(go_forge);
			TUIMappingInfo.Instance().NextNewHelpState();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_EnterForge));
			break;
		case NewHelpState.Help08_ClickMap:
			TUIMappingInfo.Instance().NextNewHelpState();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_EnterMap));
			break;
		case NewHelpState.Help11_ClickEnterSkills:
			camera_village.SetCloser(go_skill);
			TUIMappingInfo.Instance().NextNewHelpState();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_EnterSkill));
			break;
		case NewHelpState.Help18_ClickMap:
			TUIMappingInfo.Instance().NextNewHelpState();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_EnterMap));
			break;
		case NewHelpState.Help21_ClickEnterForge:
			camera_village.SetCloser(go_forge);
			TUIMappingInfo.Instance().NextNewHelpState();
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_EnterForge));
			break;
		}
	}

	public void TUIEvent_ClickDailyLoginBonus(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_ClickDailyLoginBonus));
		}
	}

	public void TUIEvent_ShowUDID(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_ShowUDID));
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_SkipTutorial));
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

	public void TUIEvent_ClickBtnCoop(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneMainMenu(TUIEvent.SceneMainMenuEventType.TUIEvent_EnterCoop));
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

	private void PlayForwardAnimation(Transform go)
	{
		if (!(go == null))
		{
			AnimationState animationState = go.GetComponent<Animation>()[go.GetComponent<Animation>().clip.name];
			animationState.speed = 1f;
			animationState.normalizedTime = 0f;
			go.GetComponent<Animation>().Play(animationState.name, PlayMode.StopAll);
		}
	}

	private void PlayBackwardAnimation(Transform go)
	{
		if (!(go == null))
		{
			AnimationState animationState = go.GetComponent<Animation>()[go.GetComponent<Animation>().clip.name];
			animationState.speed = -1f;
			animationState.normalizedTime = 1f;
			go.GetComponent<Animation>().Play(animationState.name, PlayMode.StopAll);
		}
	}

	private void LookAtCamera()
	{
		if (camera_village == null)
		{
			Debug.Log("error!");
			return;
		}
		Vector3 eulerAngles = camera_village.transform.eulerAngles + new Vector3(-90f, 0f, 180f);
		if (go_forge_name != null)
		{
			go_forge_name.transform.eulerAngles = eulerAngles;
		}
		if (go_tavern_name != null)
		{
			go_tavern_name.transform.eulerAngles = eulerAngles;
		}
		if (go_skill_name != null)
		{
			go_skill_name.transform.eulerAngles = eulerAngles;
		}
		if (go_stash_name != null)
		{
			go_stash_name.transform.eulerAngles = eulerAngles;
		}
		if (go_camp_name != null)
		{
			go_camp_name.transform.eulerAngles = eulerAngles;
		}
		if (go_blackmarket_name != null)
		{
			go_blackmarket_name.transform.eulerAngles = eulerAngles;
		}
	}

	private void TakeAchievement(TUIAchievementRewardInfo m_reward_info, Top_Bar m_top_bar)
	{
		if (m_reward_info == null || m_top_bar == null)
		{
			Debug.Log("error!");
			return;
		}
		if (m_reward_info.open_reward01)
		{
			m_top_bar.AddGoldOrCrystal(m_reward_info.reward_unit01, m_reward_info.reward_value01, true);
		}
		if (m_reward_info.open_reward02)
		{
			m_top_bar.AddGoldOrCrystal(m_reward_info.reward_unit02, m_reward_info.reward_value02, true);
		}
	}

	private void TakeDailyMissionsReward(TUIAchievementRewardInfo m_reward_info, Top_Bar m_top_bar)
	{
		if (m_reward_info == null || m_top_bar == null)
		{
			Debug.Log("error!");
			return;
		}
		if (m_reward_info.open_reward01)
		{
			m_top_bar.AddGoldOrCrystal(m_reward_info.reward_unit01, m_reward_info.reward_value01, true);
		}
		if (m_reward_info.open_reward02)
		{
			m_top_bar.AddGoldOrCrystal(m_reward_info.reward_unit02, m_reward_info.reward_value02, true);
		}
	}

	private void UpdateArrowControl()
	{
		if (img_arrow_left == null || img_arrow_right == null || camera_village == null)
		{
			Debug.Log("error!");
			return;
		}
		float num = 0.37f;
		float num2 = 0.65f;
		float persentAngle = camera_village.GetPersentAngle();
		if (persentAngle > num)
		{
			img_arrow_left.gameObject.SetActiveRecursively(true);
		}
		else
		{
			img_arrow_left.gameObject.SetActiveRecursively(false);
		}
		if (persentAngle < num2)
		{
			img_arrow_right.gameObject.SetActiveRecursively(true);
		}
		else
		{
			img_arrow_right.gameObject.SetActiveRecursively(false);
		}
	}

	private void OpenMapBlink(bool m_open)
	{
		if (m_open)
		{
			if (img_map_bg != null && img_map_bg.GetComponent<Animation>() != null)
			{
				img_map_bg.gameObject.SetActiveRecursively(true);
				img_map_bg.GetComponent<Animation>().wrapMode = WrapMode.Loop;
				img_map_bg.GetComponent<Animation>().Play();
			}
		}
		else if (img_map_bg != null && img_map_bg.GetComponent<Animation>() != null)
		{
			img_map_bg.gameObject.SetActiveRecursively(false);
		}
	}

	private void OpenCoopBlink(bool m_open)
	{
		if (m_open)
		{
			if (img_coop_bg != null && img_coop_bg.GetComponent<Animation>() != null)
			{
				img_coop_bg.gameObject.SetActiveRecursively(true);
				img_coop_bg.GetComponent<Animation>().wrapMode = WrapMode.Loop;
				img_coop_bg.GetComponent<Animation>().Play();
			}
		}
		else if (img_coop_bg != null && img_coop_bg.GetComponent<Animation>() != null)
		{
			img_coop_bg.gameObject.SetActiveRecursively(false);
		}
	}

	private void OpenAchievementBlink(bool m_open)
	{
		if (m_open)
		{
			if (effect_achievement_bg != null)
			{
				effect_achievement_bg.SetActiveRecursively(true);
			}
		}
		else if (effect_achievement_bg != null)
		{
			effect_achievement_bg.SetActiveRecursively(false);
		}
	}

	private void OpenSaleBlink(bool m_open)
	{
		if (m_open)
		{
			if (effect_sale_bg != null)
			{
				effect_sale_bg.SetActiveRecursively(true);
			}
		}
		else if (effect_sale_bg != null)
		{
			effect_sale_bg.SetActiveRecursively(false);
		}
	}

	private void OpenDailyMissionsBlink(bool m_open)
	{
		if (m_open)
		{
			if (effect_daily_missions_bg != null)
			{
				effect_daily_missions_bg.SetActiveRecursively(true);
			}
		}
		else if (effect_daily_missions_bg != null)
		{
			effect_daily_missions_bg.SetActiveRecursively(false);
		}
	}

	private void SetCurrentAngle(CameraStopType m_stop_type)
	{
		if (camera_village != null)
		{
			switch (m_stop_type)
			{
			case CameraStopType.StopForge:
				SetCurrentAngleEx(go_forge);
				break;
			case CameraStopType.StopCamp:
				SetCurrentAngleEx(go_camp);
				break;
			case CameraStopType.StopSkills:
				SetCurrentAngleEx(go_skill);
				break;
			case CameraStopType.StopStash:
				SetCurrentAngleEx(go_stash);
				break;
			case CameraStopType.StopTavern:
				SetCurrentAngleEx(go_tavern);
				break;
			case CameraStopType.StopBlackMarket:
				SetCurrentAngleEx(go_blackmarket);
				break;
			}
		}
	}

	private void SetCurrentAngleEx(Transform m_go)
	{
		if (m_go != null)
		{
			float num = Quaternion.FromToRotation(camera_village.transform.forward, m_go.transform.position - camera_village.transform.position).eulerAngles.y;
			if (num > 180f)
			{
				num -= 360f;
			}
			num += camera_village.transform.eulerAngles.y;
			camera_village.SetCurrentAngle(new Vector3(camera_village.transform.eulerAngles.x, num, camera_village.transform.eulerAngles.z));
		}
	}

	private void DoNewHelp()
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
			popup_new_help.SetHelpState(newHelpState, 0f);
			switch (newHelpState)
			{
			case NewHelpState.Help01_ClickEnterForge:
			case NewHelpState.Help21_ClickEnterForge:
				SetCurrentAngle(CameraStopType.StopForge);
				break;
			case NewHelpState.Help11_ClickEnterSkills:
				SetCurrentAngle(CameraStopType.StopSkills);
				break;
			}
			if (btn_coop != null)
			{
				btn_coop.gameObject.SetActiveRecursively(false);
			}
			AndroidReturnPlugin.instance.SetSkipTutorialFunc(TUIEvent_OpenSkipTutorial);
		}
		else if (btn_coop != null)
		{
			btn_coop.Reset();
		}
	}

	private void ChangeUnlockItem()
	{
		if (unlock_list == null || unlock_list.Count == 0)
		{
			return;
		}
		TUIUnlockInfo tUIUnlockInfo = unlock_list[0];
		if (tUIUnlockInfo == null)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Unlocked_weapon");
		}
		if (tUIUnlockInfo.unlock_type == UnlockType.Weapon)
		{
			unlock_blink.OpenBlinkWeapon(tUIUnlockInfo.item_id, "New Equip Unlocked For Purchase!", true);
		}
		else if (tUIUnlockInfo.unlock_type == UnlockType.Role)
		{
			unlock_blink.OpenBlinkRole(tUIUnlockInfo.item_id, "New Character Unlocked For Purchase!");
		}
		else if (tUIUnlockInfo.unlock_type == UnlockType.Skill)
		{
			unlock_blink.OpenBlinkSkill(tUIUnlockInfo.item_id, "New Skill Unlocked For Purchase!", true);
		}
		else if (tUIUnlockInfo.unlock_type == UnlockType.Title)
		{
			string title_name = string.Empty;
			if (title_list != null && title_list.ContainsKey(tUIUnlockInfo.item_id))
			{
				title_name = title_list[tUIUnlockInfo.item_id];
			}
			unlock_blink.OpenBlinkTitle(title_name, "New Title Unlocked!");
		}
		else if (tUIUnlockInfo.unlock_type == UnlockType.Avatar)
		{
			unlock_blink.OpenBlinkWeapon("New Avatar Unlocked For Purchase!", true, tUIUnlockInfo.m_sPath);
		}
		unlock_list.Remove(tUIUnlockInfo);
	}
}
