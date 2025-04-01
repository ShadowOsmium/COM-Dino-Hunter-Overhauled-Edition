using System.Collections.Generic;
using EventCenter;
using UnityEngine;

public class Scene_CoopMainMenu : MonoBehaviour
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

	public PopupPlayerInfo popup_player_info;

	private TUILabel label_status;

	private int status_length = 80;

	private string status_input_text = string.Empty;

	private Dictionary<TUIEvent.SceneCoopMainMenuEventType, TUIGameInfo> backup_info;

	private List<TUIUnlockInfo> unlock_list;

	public UnlockBlink unlock_blink;

	public Dictionary<int, string> title_list;

	public PopupOnlyText popup_loading;

	private bool change_drag;

	protected Dictionary<string, TUICoopPlayerInfo> m_dictData;

	protected List<string> m_ltFriends;

	public void SetData(string sID, TUICoopPlayerInfo coopplayerinfo)
	{
		if (m_dictData.ContainsKey(sID))
		{
			m_dictData[sID] = coopplayerinfo;
		}
		else
		{
			m_dictData.Add(sID, coopplayerinfo);
		}
	}

	public TUICoopPlayerInfo GetData(string sID)
	{
		if (!m_dictData.ContainsKey(sID))
		{
			return null;
		}
		return m_dictData[sID];
	}

	private void Awake()
	{
		backup_info = new Dictionary<TUIEvent.SceneCoopMainMenuEventType, TUIGameInfo>();
		m_dictData = new Dictionary<string, TUICoopPlayerInfo>();
		m_ltFriends = new List<string>();
		if (m_fade == null)
		{
			Debug.Log("error!no found m_fade!");
		}
		TUIDataServer.Instance().Initialize();
		TUIMappingInfo.Instance().SetCurrentScene(base.gameObject);
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.BackEvent_SceneCoopMainMenu>(TUIEvent_SetUIInfo);
	}

	private void Start()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_TopBar));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_EnterInfo));
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
		CheckBackupInfo();
		UpdateKeyboard();
	}

	private void OnDestroy()
	{
		global::EventCenter.EventCenter.Instance.Unregister<TUIEvent.BackEvent_SceneCoopMainMenu>(TUIEvent_SetUIInfo);
	}

	public void TUIEvent_SetUIInfo(object sender, TUIEvent.BackEvent_SceneCoopMainMenu m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_TopBar)
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
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_EnterInfo)
		{
			TUIGameInfo eventInfo = m_event.GetEventInfo();
			if (eventInfo != null)
			{
				SetData(eventInfo.coop_player_info.id, eventInfo.coop_player_info);
				popup_player_info.SetTitleList(eventInfo.coop_title_list_info);
				popup_player_info.SetInfo(eventInfo.coop_player_info);
				TUICoopTitleListInfo coop_title_list_info = eventInfo.coop_title_list_info;
				if (coop_title_list_info != null)
				{
					title_list = coop_title_list_info.title_list;
				}
				TUIVilliageEnterInfo villiage_enter_info = eventInfo.villiage_enter_info;
				if (villiage_enter_info != null)
				{
					unlock_list = villiage_enter_info.unlock_list;
					Debug.Log("unlock count " + unlock_list.Count);
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_Friends)
		{
			if (m_event.GetEventInfo() != null)
			{
				if (m_event.GetControlSuccess())
				{
					popup_player_info.SetFriendsEmtpyStr("You don't have any friends yet.");
				}
				else
				{
					popup_player_info.SetFriendsEmtpyStr("Loading...");
				}
				backup_info[TUIEvent.SceneCoopMainMenuEventType.TUIEvent_Friends] = m_event.GetEventInfo();
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_AddFriends)
		{
			if (m_event.GetEventInfo() != null)
			{
				backup_info[TUIEvent.SceneCoopMainMenuEventType.TUIEvent_Friends] = m_event.GetEventInfo();
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_AllRanking)
		{
			if (m_event.GetEventInfo() != null)
			{
				backup_info[TUIEvent.SceneCoopMainMenuEventType.TUIEvent_AllRanking] = m_event.GetEventInfo();
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_AddAllRanking)
		{
			if (m_event.GetEventInfo() != null)
			{
				backup_info[TUIEvent.SceneCoopMainMenuEventType.TUIEvent_AddAllRanking] = m_event.GetEventInfo();
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_FriendsRanking)
		{
			if (m_event.GetEventInfo() != null)
			{
				TUICoopRankingInfo coop_ranking_info = m_event.GetEventInfo().coop_ranking_info;
				if (coop_ranking_info != null)
				{
					Dictionary<string, TUICoopPlayerInfo> ranking_list = coop_ranking_info.ranking_list;
					popup_player_info.SetRankingList(RankingType.Friends_All, ranking_list, base.gameObject);
					TUICoopPlayerInfo my_ranking_list = coop_ranking_info.my_ranking_list;
					popup_player_info.SetRankingList(RankingType.Friends_Mine, my_ranking_list, base.gameObject);
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_AddFriendsRanking)
		{
			if (m_event.GetEventInfo() == null)
			{
				return;
			}
			TUICoopRankingInfo coop_ranking_info2 = m_event.GetEventInfo().coop_ranking_info;
			if (coop_ranking_info2 != null)
			{
				Dictionary<string, TUICoopPlayerInfo> ranking_list2 = coop_ranking_info2.ranking_list;
				if (ranking_list2 != null)
				{
					popup_player_info.AddRankingList(RankingType.Friends_All, ranking_list2, base.gameObject);
				}
				TUICoopPlayerInfo my_ranking_list2 = coop_ranking_info2.my_ranking_list;
				if (my_ranking_list2 != null)
				{
					popup_player_info.SetRankingList(RankingType.Friends_Mine, my_ranking_list2, base.gameObject);
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_InfoCard)
		{
			if (m_event.GetEventInfo() != null)
			{
				popup_player_info.SetInfoCard(m_event.GetEventInfo().coop_player_info);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_UpdatePlayerTexture)
		{
			if (m_event.GetEventInfo() != null && popup_player_info != null)
			{
				popup_player_info.UpdatePlayerTexture(m_event.GetEventInfo().player_texture_info);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_Equip)
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
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_Start)
		{
			CGameNetManager.GetInstance().connected = true;
			CGameNetManager.GetInstance().IsGaming = true;
			//if (m_event.GetControlSuccess())
			//{
				DoSceneChange(m_event.GetWparam(), "Scene_CoopRoom");
				return;
			//}
			//m_fade_in_time = 0f;
			//do_fade_in = false;
			//m_fade.FadeIn();
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_Back)
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
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_ShowUnlockItem)
		{
			ChangeUnlockItem();
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_ShowLoading)
		{
			bool controlSuccess = m_event.GetControlSuccess();
			string str = m_event.GetStr();
			if (popup_loading != null)
			{
				if (controlSuccess)
				{
					popup_loading.SetInfo(str);
					popup_loading.Show();
				}
				else
				{
					popup_loading.Hide();
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_EnterIAP)
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
		else if (m_event.GetEventName() == TUIEvent.SceneCoopMainMenuEventType.TUIEvent_EnterGold)
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

	public void TUIEvent_CloseInfoCard(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_player_info != null)
			{
				popup_player_info.ShowInfoCard(false);
				popup_player_info.ShowTitleList(false);
			}
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseInfoCard);
		}
	}

	public void TUIEvent_ClickTitleList(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_player_info != null)
			{
				popup_player_info.ShowTitleList(true);
			}
		}
	}

	public void TUIEvent_CloseTitleList(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (popup_player_info != null)
		{
			popup_player_info.ShowTitleList(false);
		}
		if (!(control.transform.parent != null))
		{
			return;
		}
		PopupTitleList component = control.transform.parent.GetComponent<PopupTitleList>();
		if (component != null)
		{
			int titleID = component.GetTitleID();
			if (popup_player_info != null)
			{
				popup_player_info.UpdateTitle(titleID);
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_TitleChange, titleID));
			}
		}
	}

	public void TUIEvent_ClickBtnEquip(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_Equip));
		}
	}

	public void TUIEvent_ClickBtnFriends(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_player_info != null)
			{
				popup_player_info.ShowFriendsList(true);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_ClosePopupFriends);
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_Friends));
		}
	}

	public void TUIEvent_ClosePopupFriends(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_player_info != null)
			{
				popup_player_info.ShowFriendsList(false);
			}
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_ClosePopupFriends);
		}
	}

	public void TUIEvent_ClickBtnRanking(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_player_info != null)
			{
				popup_player_info.ShowRankingList(true);
				popup_player_info.ChangeRanking(true);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_ClosePopupRanking);
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_AllRanking));
		}
	}

	public void TUIEvent_ClickAllRanking(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_player_info != null)
			{
				popup_player_info.ChangeRanking(true);
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_AllRanking));
		}
	}

	public void TUIEvent_ClickFriendsRanking(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_player_info != null)
			{
				popup_player_info.ChangeRanking(false);
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_FriendsRanking));
		}
	}

	public void TUIEvent_ClosePopupRanking(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_player_info != null)
			{
				popup_player_info.ShowRankingList(false);
			}
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_ClosePopupRanking);
		}
	}

	public void TUIEvent_ClickBtnStart(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			CGameNetManager.GetInstance().connected = true;
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_GameStartYes));
			//DoSceneChange(0, "Scene_CoopRoom");
			//global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_Start));
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_Back));
		}
	}

	public void TUIEvent_MoveScreen(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 2 && popup_player_info != null)
		{
			popup_player_info.SetRoleRotation(wparam, lparam);
		}
	}

	public void TUIEvent_AllRankingDrag(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == TUIScrollList.CommandDown)
		{
			change_drag = false;
		}
		else if (event_type == TUIScrollList.CommandMove)
		{
			TUIScrollList component = control.GetComponent<TUIScrollList>();
			if (component != null && component.scrollPos > 1f)
			{
				change_drag = true;
			}
		}
		else if (event_type == TUIScrollList.CommandUp && change_drag)
		{
			change_drag = false;
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_AddAllRanking));
		}
	}

	public void TUIEvent_FriendsRankingDrag(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == TUIScrollList.CommandDown)
		{
			change_drag = false;
		}
		else if (event_type == TUIScrollList.CommandMove)
		{
			TUIScrollList component = control.GetComponent<TUIScrollList>();
			if (component != null && component.scrollPos > 1f)
			{
				change_drag = true;
			}
		}
		else if (event_type == TUIScrollList.CommandUp && change_drag)
		{
			change_drag = false;
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_AddFriendsRanking));
		}
	}

	public void TUIEvent_FriendsListDrag(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == TUIScrollList.CommandDown)
		{
			change_drag = false;
		}
		else if (event_type == TUIScrollList.CommandMove)
		{
			TUIScrollList component = control.GetComponent<TUIScrollList>();
			if (component != null && component.scrollPos > 1f)
			{
				change_drag = true;
			}
		}
		else if (event_type == TUIScrollList.CommandUp && change_drag)
		{
			change_drag = false;
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_AddFriends));
		}
	}

	public void TUIEvent_TitleChange(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != TUIScrollListEx.CommandChange)
		{
		}
	}

	public void TUIEvent_Start(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			CGameNetManager.GetInstance().connected = true;
			DoSceneChange(0, "Scene_CoopRoom");
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_Start));
		}
	}

	public void TUIEvent_ClickStatus(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		status_input_text = string.Empty;
		PopupInfoCard popupInfoCard = null;
		if (control.transform.parent != null && control.transform.parent.parent != null && control.transform.parent.parent.parent != null)
		{
			popupInfoCard = control.transform.parent.parent.parent.GetComponent<PopupInfoCard>();
			if (popupInfoCard != null && popup_player_info != null && popup_player_info.GetPlayerID() == popupInfoCard.GetPlayerID())
			{
				label_status = popupInfoCard.GetStatusLabel();
				if (label_status != null)
				{
					status_input_text = label_status.Text;
				}
			}
		}
		OpenKeyBoard(status_input_text);
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

	public void TUIEvent_ClickBtnInfoCard(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (!(control.transform.parent != null))
		{
			return;
		}
		PopupPlayerInfo component = control.transform.parent.GetComponent<PopupPlayerInfo>();
		if (!(component != null))
		{
			return;
		}
		TUICoopPlayerInfo playerInfo = component.GetPlayerInfo();
		if (playerInfo != null)
		{
			if (popup_player_info != null)
			{
				popup_player_info.ShowInfoCard(true, true, playerInfo);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseInfoCard);
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_InfoCard, playerInfo.id));
		}
	}

	public void TUIEvent_FriendsCardInfo(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (!(control.transform.parent != null))
		{
			return;
		}
		PopupFriendsItem component = control.transform.parent.GetComponent<PopupFriendsItem>();
		if (!(component != null))
		{
			return;
		}
		TUICoopPlayerInfo playerInfo = component.GetPlayerInfo();
		if (playerInfo != null)
		{
			if (popup_player_info != null)
			{
				popup_player_info.ShowInfoCard(true, false, playerInfo);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseInfoCard);
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_InfoCard, playerInfo.id));
		}
	}

	public void TUIEvent_RankingInfoCard(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (!(control.transform.parent != null))
		{
			return;
		}
		PopupRankingItem component = control.transform.parent.GetComponent<PopupRankingItem>();
		if (!(component != null))
		{
			return;
		}
		TUICoopPlayerInfo playerInfo = component.GetPlayerInfo();
		if (playerInfo != null)
		{
			if (popup_player_info != null)
			{
				popup_player_info.ShowInfoCard(true, false, playerInfo);
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseInfoCard);
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_InfoCard, playerInfo.id));
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_EnterIAP));
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
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_EnterGold));
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

	public bool GetRankingShow()
	{
		if (popup_player_info != null)
		{
			return popup_player_info.GetRankingShow();
		}
		return false;
	}

	public bool GetFriendsListShow()
	{
		if (popup_player_info != null)
		{
			return popup_player_info.GetFriendsListShow();
		}
		return false;
	}

	public bool GetInfoCardShow()
	{
		if (popup_player_info != null)
		{
			return popup_player_info.GetInfoCardShow();
		}
		return false;
	}

	private void CheckBackupInfo()
	{
		if (backup_info == null)
		{
			return;
		}
		List<TUIEvent.SceneCoopMainMenuEventType> list = new List<TUIEvent.SceneCoopMainMenuEventType>();
		foreach (KeyValuePair<TUIEvent.SceneCoopMainMenuEventType, TUIGameInfo> item in backup_info)
		{
			TUIGameInfo value = item.Value;
			if (value == null)
			{
				continue;
			}
			switch (item.Key)
			{
			case TUIEvent.SceneCoopMainMenuEventType.TUIEvent_AllRanking:
			{
				if (!popup_player_info.GetRankingAniStop())
				{
					break;
				}
				TUICoopRankingInfo coop_ranking_info2 = value.coop_ranking_info;
				if (coop_ranking_info2 != null)
				{
					Dictionary<string, TUICoopPlayerInfo> ranking_list2 = coop_ranking_info2.ranking_list;
					if (ranking_list2 != null)
					{
						popup_player_info.SetRankingList(RankingType.All_All, ranking_list2, base.gameObject);
					}
					TUICoopPlayerInfo my_ranking_list2 = coop_ranking_info2.my_ranking_list;
					if (my_ranking_list2 != null)
					{
						popup_player_info.SetRankingList(RankingType.All_Mine, my_ranking_list2, base.gameObject);
					}
				}
				list.Add(item.Key);
				break;
			}
			case TUIEvent.SceneCoopMainMenuEventType.TUIEvent_AddAllRanking:
			{
				if (!popup_player_info.GetRankingAniStop())
				{
					break;
				}
				TUICoopRankingInfo coop_ranking_info = value.coop_ranking_info;
				if (coop_ranking_info != null)
				{
					Dictionary<string, TUICoopPlayerInfo> ranking_list = coop_ranking_info.ranking_list;
					if (ranking_list != null)
					{
						popup_player_info.AddRankingList(RankingType.All_All, ranking_list, base.gameObject);
					}
					TUICoopPlayerInfo my_ranking_list = coop_ranking_info.my_ranking_list;
					if (my_ranking_list != null)
					{
						popup_player_info.AddRankingList(RankingType.All_Mine, my_ranking_list, base.gameObject);
					}
				}
				list.Add(item.Key);
				break;
			}
			case TUIEvent.SceneCoopMainMenuEventType.TUIEvent_Friends:
				if (popup_player_info.GetFriendsAniStop())
				{
					TUICoopFriendsInfo coop_friends_info = value.coop_friends_info;
					if (coop_friends_info != null)
					{
						popup_player_info.SetFriendsList(coop_friends_info.m_dictFriends, base.gameObject);
					}
					list.Add(item.Key);
				}
				break;
			case TUIEvent.SceneCoopMainMenuEventType.TUIEvent_AddFriends:
				if (popup_player_info.GetFriendsAniStop())
				{
					TUICoopFriendsInfo coop_friends_info2 = value.coop_friends_info;
					if (coop_friends_info2 != null)
					{
						popup_player_info.AddFriendsList(coop_friends_info2.m_dictFriends, base.gameObject);
					}
					list.Add(item.Key);
				}
				break;
			case TUIEvent.SceneCoopMainMenuEventType.TUIEvent_InfoCard:
				if (popup_player_info.GetFriendsAniStop())
				{
					if (value != null)
					{
						popup_player_info.SetInfoCard(value.coop_player_info);
					}
					list.Add(item.Key);
				}
				break;
			}
		}
		for (int i = 0; i < list.Count; i++)
		{
			if (backup_info.ContainsKey(list[i]))
			{
				backup_info[list[i]] = null;
			}
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
			if (title_list.ContainsKey(tUIUnlockInfo.item_id))
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

	public void OpenKeyBoard(string text)
	{
		Debug.Log(text);
		IphoneInputPlugin.GetInstance().Open("Enter your status(max 40 letters or numbers)", text, 40, OnKeyBoardDone, TouchScreenKeyboardType.ASCIICapable, "^[\\w\\?\\,\\.\\!\\@\\#\\$\\%\\^&\\*\\(\\)]${0,40}", false, true, true);
	}

	protected void OnKeyBoardDone(string sValue)
	{
		Debug.Log(sValue);
		status_input_text = sValue;
		if (label_status != null)
		{
			label_status.Text = sValue;
		}
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_StatusChange, sValue));
	}

	private void UpdateKeyboard()
	{
		IphoneInputPlugin.GetInstance().Update(Time.deltaTime);
	}
}
