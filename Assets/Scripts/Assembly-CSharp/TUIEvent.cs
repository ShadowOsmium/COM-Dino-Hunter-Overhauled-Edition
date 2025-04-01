using System.Collections.Generic;

public class TUIEvent
{
	public enum SceneMainEventType
	{
		None,
		TUIEvent_OptionInfo,
		TUIEvent_EnterInfo,
		TUIEvent_EnterLevel,
		TUIEvent_ConnectResult,
		TUIEvent_HidePopupWarnning,
		TUIEvent_ShowPopupServer,
		TUIEvent_HidePopupServer,
		TUIEvent_ChangeText,
		TUIEvent_ConnectAgain,
		TUIEvent_GotoUpdate,
		TUIEvent_FetchFailed,
		TUIEvent_PopupServerOK,
		TUIEvent_PopupServerCancle,
		TUIEvent_ClickFullVersion,
		TUIEvent_GMUsing,
		TUIEvent_ServerMaintain,
		TUIEvent_UnkownError
	}

	public enum SceneMainMenuEventType
	{
		None,
		TUIEvent_TopBar,
		TUIEvent_OptionInfo,
		TUIEvent_AcheviementInfo,
		TUIEvent_SaleInfo,
		TUIEvent_EnterInfo,
		TUIEvent_TakeAchievement,
		TUIEvent_ChangeMusic,
		TUIEvent_ChangeSFX,
		TUIEvent_Forum,
		TUIEvent_EnterIAP,
		TUIEvent_EnterGold,
		TUIEvent_EnterEquip,
		TUIEvent_EnterForge,
		TUIEvent_EnterTavern,
		TUIEvent_EnterSkill,
		TUIEvent_EnterStash,
		TUIEvent_EnterMap,
		TUIEvent_ShowReview,
		TUIEvent_HadAchievementReward,
		TUIEvent_ShowSale,
		TUIEvent_EnterSale,
		TUIEvent_DailyLoginBonusInfo,
		TUIEvent_ShowDailyLoginBonus,
		TUIEvent_ClickDailyLoginBonus,
		TUIEvent_DailyMissionsInfo,
		TUIEvent_TakeDailyMissionsReward,
		TUIEvent_HadDailyMissionsReward,
		TUIEvent_ShowUnlockItem,
		TUIEvent_OpenSupportURL,
		TUIEvent_OpenReviewURL,
		TUIEvent_CloseReviewURL,
		TUIEvent_CloseSale,
		TUIEvent_ShowUDID,
		TUIEvent_SkipTutorial,
		TUIEvent_EnterCoop,
		TUIEvent_EnterBlackMarket
	}

	public enum SceneEquipEventType
	{
		None,
		TUIEvent_TopBar,
		TUIEvent_OptionInfo,
		TUIEvent_RoleSign,
		TUIEvent_SkillSign,
		TUIEvent_PropSign,
		TUIEvent_WeaponSign,
		TUIEvent_RoleEquip,
		TUIEvent_SkillEquip,
		TUIEvent_SkillUnEquip,
		TUIEvent_SkillExchange,
		TUIEvent_WeaponEquip,
		TUIEvent_WeaponUnEquip,
		TUIEvent_WeaponExchange,
		TUIEvent_Back,
		TUIEvent_RolesChoose,
		TUIEvent_WeaponChoose,
		TUIEvent_SkillChoose,
		TUIEvent_EnterIAP,
		TUIEvent_EnterGold,
		TUIEvent_EnterGoBuyWeapon,
		TUIEvent_EnterGoBuyWeaponInBlack,
		TUIEvent_EnterGoBuySkill,
		TUIEvent_RoleNewMarkInfo,
		TUIEvent_SkillNewMarkInfo,
		TUIEvent_WeaponNewMarkInfo,
		TUIEvent_SkipTutorial,
		TUIEvent_SetBattlePower
	}

	public enum SceneStashEventType
	{
		None,
		TUIEvent_TopBar,
		TUIEvent_OptionInfo,
		TUIEvent_StashInfo,
		TUIEvent_AddCapacity,
		TUIEvent_SellGoods,
		TUIEvent_Back,
		TUIEvent_SearchGoodsDrop,
		TUIEvent_GoldToCrystal,
		TUIEvent_EnterIAP,
		TUIEvent_EnterGold,
		TUIEvent_EnterIAPCrystalNoEnough
	}

	public enum SceneSkillEventType
	{
		None,
		TUIEvent_TopBar,
		TUIEvent_OptionInfo,
		TUIEvent_SkillInfo,
		TUIEvent_SkillUnlcok,
		TUIEvent_SkillBuy,
		TUIEvent_SkillUpdate,
		TUIEvent_Back,
		TUIEvent_SkillChoose,
		TUIEvent_GoldToCrystal,
		TUIEvent_EnterIAP,
		TUIEvent_EnterGold,
		TUIEvent_EnterIAPCrystalNoEnough,
		TUIEvent_EnterGoEquip,
		TUIEvent_NewMarkInfo,
		TUIEvent_SkipTutorial
	}

	public enum SceneForgeEventType
	{
		None,
		TUIEvent_TopBar,
		TUIEvent_OptionInfo,
		TUIEvent_WeaponInfo,
		TUIEvent_WeaponGoodsBuy,
		TUIEvent_Back,
		TUIEvent_SearchGoodsDrop,
		TUIEvent_WeaponChoose,
		TUIEvent_ClickUpgrade,
		TUIEvent_GetActiveWeapon,
		TUIEvent_ShowSupplement,
		TUIEvent_ClickSupplement,
		TUIEvent_GoldToCrystal,
		TUIEvent_EnterIAP,
		TUIEvent_EnterGold,
		TUIEvent_EnterIAPCrystalNoEnough,
		TUIEvent_EnterGoEquip,
		TUIEvent_NewMarkInfo,
		TUIEvent_SkipTutorial
	}

	public enum SceneTavernEventType
	{
		None,
		TUIEvent_TopBar,
		TUIEvent_OptionInfo,
		TUIEvent_AllRoleInfo,
		TUIEvent_RoleUnlock,
		TUIEvent_RoleBuy,
		TUIEvent_RoleChange,
		TUIEvent_Back,
		TUIEvent_RolesChoose,
		TUIEvent_GoldToCrystal,
		TUIEvent_EnterIAP,
		TUIEvent_EnterGold,
		TUIEvent_EnterIAPCrystalNoEnough,
		TUIEvent_EnterGoEquip,
		TUIEvent_NewMarkInfo,
		TUIEvent_GetActiveRole
	}

	public enum SceneMapEventType
	{
		None,
		TUIEvent_TopBar,
		TUIEvent_OptionInfo,
		TUIEvent_MapEnterInfo,
		TUIEvent_LevelInfo,
		TUIEvent_EnterLevel,
		TUIEvent_Back,
		TUIEvent_EnterRoleBuy,
		TUIEvent_EnterWeaponBuy,
		TUIEvent_EnterEquip,
		TUIEvent_EnterIAP,
		TUIEvent_EnterGold,
		TUIEvent_EnterVilliage,
		TUIEvent_ClickPopularize,
		TUIEvent_SkipTutorial,
		TUIEvent_EnterCoop
	}

	public enum SceneIAPEventType
	{
		None,
		TUIEvent_TopBar,
		TUIEvent_OptionInfo,
		TUIEvent_IAPBuy,
		TUIEvent_Back,
		TUIEvent_EnterGold,
		TUIEvent_IAPResult,
		TUIEvent_ServerResult,
		TUIEvent_IAPEnterInfo,
		TUIEvent_TapJoy
	}

	public enum SceneGoldEventType
	{
		None,
		TUIEvent_OptionInfo,
		TUIEvent_TopBar,
		TUIEvent_GoldBuy,
		TUIEvent_Back,
		TUIEvent_EnterIAP,
		TUIEvent_EnterIAPCrystalNoEnough,
		TUIEvent_GoldResult
	}

	public enum SceneCoopInputNameEventType
	{
		None,
		TUIEvent_TopBar,
		TUIEvent_HelpClick,
		TUIEvent_InputName,
		TUIEvent_Continue,
		TUIEvent_Back
	}

	public enum SceneCoopMainMenuEventType
	{
		None,
		TUIEvent_TopBar,
		TUIEvent_EnterInfo,
		TUIEvent_Equip,
		TUIEvent_Start,
		TUIEvent_Friends,
		TUIEvent_AddFriends,
		TUIEvent_TitleList,
		TUIEvent_AllRanking,
		TUIEvent_FriendsRanking,
		TUIEvent_AddAllRanking,
		TUIEvent_AddFriendsRanking,
		TUIEvent_InfoCard,
		TUIEvent_Back,
		TUIEvent_UpdatePlayerTexture,
		TUIEvent_TitleChange,
		TUIEvent_ShowUnlockItem,
		TUIEvent_StatusChange,
		TUIEvent_ShowLoading,
		TUIEvent_EnterIAP,
		TUIEvent_EnterGold
	}

	public enum SceneCoopRoomEventType
	{
		None,
		TUIEvent_TopBar,
		TUIEvent_EnterInfo,
		TUIEvent_IamEnter,
		TUIEvent_PlayerEnter,
		TUIEvent_PlayerExit,
		TUIEvent_GameStartBtn,
		TUIEvent_GameStartYes,
		TUIEvent_GameStartCancel,
		TUIEvent_GameStart,
		TUIEvent_Back,
		TUIEvent_ShowBtnStart,
		TUIEvent_ShowStartWarning,
		TUIEvent_LastRoleSpeedOver
	}

	public enum SceneBlackMarketEventType
	{
		None,
		TUIEvent_TopBar,
		TUIEvent_Back,
		TUIEvent_EnterIAP,
		TUIEvent_EnterGold,
		TUIEvent_GoodsInfo,
		TUIEvent_ClickBtnBuy,
		TUIEvent_EnterIAPCrystalNoEnough,
		TUIEvent_GoldToCrystal,
		TUIEvent_EnterGoEquip
	}

	public class SendEvent_SceneMain
	{
		private SceneMainEventType name;

		private int wparam;

		private int lparam;

		public SendEvent_SceneMain(SceneMainEventType m_name, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public SceneMainEventType GetEventName()
		{
			return name;
		}

		public int GetWParam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}
	}

	public class BackEvent_SceneMain
	{
		private SceneMainEventType name;

		private TUIGameInfo info;

		private bool control_success;

		private int wparam;

		private int lparam;

		private string str = string.Empty;

		public BackEvent_SceneMain(SceneMainEventType m_name, bool m_success, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			info = null;
			control_success = m_success;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public BackEvent_SceneMain(SceneMainEventType m_name, bool m_success, int m_wparam, string m_str)
		{
			name = m_name;
			info = null;
			control_success = m_success;
			wparam = m_wparam;
			str = m_str;
		}

		public BackEvent_SceneMain(SceneMainEventType m_name, bool m_success, TUIGameInfo m_info)
		{
			name = m_name;
			info = m_info;
			control_success = m_success;
		}

		public BackEvent_SceneMain(SceneMainEventType m_name)
		{
			name = m_name;
		}

		public BackEvent_SceneMain(SceneMainEventType m_name, string m_str)
		{
			name = m_name;
			str = m_str;
		}

		public BackEvent_SceneMain(SceneMainEventType m_name, TUIGameInfo m_info)
		{
			name = m_name;
			info = m_info;
		}

		public SceneMainEventType GetEventName()
		{
			return name;
		}

		public TUIGameInfo GetEventInfo()
		{
			return info;
		}

		public bool GetControlSuccess()
		{
			return control_success;
		}

		public int GetWparam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}

		public string GetStr()
		{
			return str;
		}
	}

	public class SendEvent_SceneMainMenu
	{
		private SceneMainMenuEventType name;

		private int wparam;

		private int lparam;

		public SendEvent_SceneMainMenu(SceneMainMenuEventType m_name, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public SceneMainMenuEventType GetEventName()
		{
			return name;
		}

		public int GetWParam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}
	}

	public class BackEvent_SceneMainMenu
	{
		private SceneMainMenuEventType name;

		private TUIGameInfo info;

		private int wparam;

		private int lparam;

		private bool control_success;

		private string str = string.Empty;

		public BackEvent_SceneMainMenu(SceneMainMenuEventType m_name, TUIGameInfo m_info, bool m_success = false)
		{
			name = m_name;
			info = m_info;
			control_success = m_success;
		}

		public BackEvent_SceneMainMenu(SceneMainMenuEventType m_name, bool m_success = false, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			info = null;
			control_success = m_success;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public BackEvent_SceneMainMenu(SceneMainMenuEventType m_name, string m_str)
		{
			name = m_name;
			str = m_str;
		}

		public SceneMainMenuEventType GetEventName()
		{
			return name;
		}

		public TUIGameInfo GetEventInfo()
		{
			return info;
		}

		public bool GetControlSuccess()
		{
			return control_success;
		}

		public int GetWparam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}

		public string GetStr()
		{
			return str;
		}
	}

	public class SendEvent_SceneEquip
	{
		private SceneEquipEventType name;

		private int wparam;

		private int lparam;

		protected PopupType m_EquipType;

		public SendEvent_SceneEquip(SceneEquipEventType m_name, int m_wparam = 0, int m_lparam = 0, PopupType nType = PopupType.None)
		{
			name = m_name;
			wparam = m_wparam;
			lparam = m_lparam;
			m_EquipType = nType;
		}

		public SceneEquipEventType GetEventName()
		{
			return name;
		}

		public int GetWParam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}

		public new PopupType GetType()
		{
			return m_EquipType;
		}
	}

	public class BackEvent_SceneEquip
	{
		private SceneEquipEventType name;

		private TUIGameInfo info;

		private int wparam;

		private int lparam;

		private bool control_success;

		public BackEvent_SceneEquip(SceneEquipEventType m_name, TUIGameInfo m_info, bool m_success = false)
		{
			name = m_name;
			info = m_info;
			control_success = m_success;
		}

		public BackEvent_SceneEquip(SceneEquipEventType m_name, bool m_success = false, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			info = null;
			control_success = m_success;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public SceneEquipEventType GetEventName()
		{
			return name;
		}

		public TUIGameInfo GetEventInfo()
		{
			return info;
		}

		public bool GetControlSuccess()
		{
			return control_success;
		}

		public int GetWparam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}
	}

	public class SendEvent_SceneStash
	{
		private SceneStashEventType name;

		private int wparam;

		private int lparam;

		private int rparam;

		public SendEvent_SceneStash(SceneStashEventType m_name, int m_wparam = 0, int m_lparam = 0, int m_rparam = 0)
		{
			name = m_name;
			wparam = m_wparam;
			lparam = m_lparam;
			rparam = m_rparam;
		}

		public SceneStashEventType GetEventName()
		{
			return name;
		}

		public int GetWParam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}

		public int GetRparam()
		{
			return rparam;
		}
	}

	public class BackEvent_SceneStash
	{
		private SceneStashEventType name;

		private TUIGameInfo info;

		private int wparam;

		private int lparam;

		private bool control_success;

		private BackEventFalseType false_type;

		public BackEvent_SceneStash(SceneStashEventType m_name, TUIGameInfo m_info, bool m_success = false)
		{
			name = m_name;
			info = m_info;
			control_success = m_success;
		}

		public BackEvent_SceneStash(SceneStashEventType m_name, bool m_success = false, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			info = null;
			control_success = m_success;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public BackEvent_SceneStash(SceneStashEventType m_name, bool m_success, BackEventFalseType m_false_type, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			control_success = m_success;
			false_type = m_false_type;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public SceneStashEventType GetEventName()
		{
			return name;
		}

		public TUIGameInfo GetEventInfo()
		{
			return info;
		}

		public bool GetControlSuccess()
		{
			return control_success;
		}

		public int GetWparam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}

		public BackEventFalseType GetFalseType()
		{
			return false_type;
		}
	}

	public class SendEvent_SceneSkill
	{
		private SceneSkillEventType name;

		private int wparam;

		private int lparam;

		public SendEvent_SceneSkill(SceneSkillEventType m_name, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public SceneSkillEventType GetEventName()
		{
			return name;
		}

		public int GetWParam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}
	}

	public class BackEvent_SceneSkill
	{
		private SceneSkillEventType name;

		private TUIGameInfo info;

		private int wparam;

		private int lparam;

		private bool control_success;

		private BackEventFalseType false_type;

		public BackEvent_SceneSkill(SceneSkillEventType m_name, TUIGameInfo m_info, bool m_success = false)
		{
			name = m_name;
			info = m_info;
			control_success = m_success;
		}

		public BackEvent_SceneSkill(SceneSkillEventType m_name, bool m_success = false, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			info = null;
			control_success = m_success;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public BackEvent_SceneSkill(SceneSkillEventType m_name, bool m_success, BackEventFalseType m_false_type, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			control_success = m_success;
			false_type = m_false_type;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public SceneSkillEventType GetEventName()
		{
			return name;
		}

		public TUIGameInfo GetEventInfo()
		{
			return info;
		}

		public bool GetControlSuccess()
		{
			return control_success;
		}

		public int GetWparam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}

		public BackEventFalseType GetFalseType()
		{
			return false_type;
		}
	}

	public class SendEvent_SceneForge
	{
		private SceneForgeEventType name;

		private int wparam;

		private int lparam;

		private int rparam;

		private TUISupplementInfo supplement_info;

		public SendEvent_SceneForge(SceneForgeEventType m_name, int m_wparam = 0, int m_lparam = 0, int m_rparam = 0)
		{
			name = m_name;
			wparam = m_wparam;
			lparam = m_lparam;
			rparam = m_rparam;
		}

		public SendEvent_SceneForge(SceneForgeEventType m_name, TUISupplementInfo m_supplement_info)
		{
			name = m_name;
			supplement_info = m_supplement_info;
		}

		public SceneForgeEventType GetEventName()
		{
			return name;
		}

		public int GetWParam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}

		public int GetRparam()
		{
			return rparam;
		}

		public TUISupplementInfo GetSupplementInfo()
		{
			return supplement_info;
		}
	}

	public class BackEvent_SceneForge
	{
		private SceneForgeEventType name;

		private TUIGameInfo info;

		private bool control_success;

		private int wparam;

		private int lparam;

		private TUISupplementInfo m_SupplementInfo;

		private BackEventFalseType false_type;

		protected Dictionary<int, NewMarkType> m_dictMarkData;

		public BackEvent_SceneForge(SceneForgeEventType m_name, TUIGameInfo m_info, bool m_success = false)
		{
			name = m_name;
			info = m_info;
			control_success = m_success;
			wparam = 0;
			lparam = 0;
		}

		public BackEvent_SceneForge(SceneForgeEventType m_name, bool m_success = false, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			info = null;
			control_success = m_success;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public BackEvent_SceneForge(SceneForgeEventType m_name, bool m_success, BackEventFalseType m_false_type, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			control_success = m_success;
			false_type = m_false_type;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public BackEvent_SceneForge(SceneForgeEventType m_name, TUISupplementInfo supplementinfo)
		{
			name = m_name;
			m_SupplementInfo = supplementinfo;
		}

		public BackEvent_SceneForge(SceneForgeEventType m_name, Dictionary<int, NewMarkType> dictMarkData)
		{
			name = m_name;
			m_dictMarkData = dictMarkData;
		}

		public SceneForgeEventType GetEventName()
		{
			return name;
		}

		public TUIGameInfo GetEventInfo()
		{
			return info;
		}

		public bool GetControlSuccess()
		{
			return control_success;
		}

		public int GetWparam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}

		public TUISupplementInfo GetSupplementInfo()
		{
			return m_SupplementInfo;
		}

		public BackEventFalseType GetFalseType()
		{
			return false_type;
		}

		public Dictionary<int, NewMarkType> GetMarkData()
		{
			return m_dictMarkData;
		}
	}

	public class SendEvent_SceneTavern
	{
		private SceneTavernEventType name;

		private int wparam;

		private int lparam;

		public SendEvent_SceneTavern(SceneTavernEventType m_name, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public SceneTavernEventType GetEventName()
		{
			return name;
		}

		public int GetWParam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}
	}

	public class BackEvent_SceneTavern
	{
		private SceneTavernEventType name;

		private TUIGameInfo info;

		private int wparam;

		private int lparam;

		private bool control_success;

		private BackEventFalseType false_type;

		public BackEvent_SceneTavern(SceneTavernEventType m_name, TUIGameInfo m_info, bool m_success = false)
		{
			name = m_name;
			info = m_info;
			control_success = m_success;
		}

		public BackEvent_SceneTavern(SceneTavernEventType m_name, bool m_success, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			control_success = m_success;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public BackEvent_SceneTavern(SceneTavernEventType m_name, bool m_success, BackEventFalseType m_false_type, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			control_success = m_success;
			false_type = m_false_type;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public BackEvent_SceneTavern(SceneTavernEventType m_name, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public SceneTavernEventType GetEventName()
		{
			return name;
		}

		public TUIGameInfo GetEventInfo()
		{
			return info;
		}

		public bool GetControlSuccess()
		{
			return control_success;
		}

		public int GetWparam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}

		public BackEventFalseType GetFalseType()
		{
			return false_type;
		}
	}

	public class SendEvent_SceneMap
	{
		private SceneMapEventType name;

		private int wparam;

		private int lparam;

		public SendEvent_SceneMap(SceneMapEventType m_name, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public SceneMapEventType GetEventName()
		{
			return name;
		}

		public int GetWParam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}
	}

	public class BackEvent_SceneMap
	{
		private SceneMapEventType name;

		private TUIGameInfo info;

		private int wparam;

		private int lparam;

		private bool control_success;

		private string str = string.Empty;

		public BackEvent_SceneMap(SceneMapEventType m_name, TUIGameInfo m_info, bool m_success = false)
		{
			name = m_name;
			info = m_info;
			control_success = m_success;
		}

		public BackEvent_SceneMap(SceneMapEventType m_name, bool m_success = false, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			info = null;
			control_success = m_success;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public BackEvent_SceneMap(SceneMapEventType m_name, string m_str)
		{
			name = m_name;
			str = m_str;
		}

		public SceneMapEventType GetEventName()
		{
			return name;
		}

		public TUIGameInfo GetEventInfo()
		{
			return info;
		}

		public bool GetControlSuccess()
		{
			return control_success;
		}

		public int GetWparam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}

		public string GetStr()
		{
			return str;
		}
	}

	public class SendEvent_SceneIAP
	{
		private SceneIAPEventType name;

		private int wparam;

		private int lparam;

		public SendEvent_SceneIAP(SceneIAPEventType m_name, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public SceneIAPEventType GetEventName()
		{
			return name;
		}

		public int GetWParam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}
	}

	public class BackEvent_SceneIAP
	{
		private SceneIAPEventType name;

		private TUIGameInfo info;

		private int wparam;

		private int lparam;

		private bool control_success;

		public BackEvent_SceneIAP(SceneIAPEventType m_name, TUIGameInfo m_info, bool m_success = false, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			info = m_info;
			control_success = m_success;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public BackEvent_SceneIAP(SceneIAPEventType m_name, bool m_success = false, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			info = null;
			control_success = m_success;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public SceneIAPEventType GetEventName()
		{
			return name;
		}

		public TUIGameInfo GetEventInfo()
		{
			return info;
		}

		public bool GetControlSuccess()
		{
			return control_success;
		}

		public int GetWparam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}
	}

	public class SendEvent_SceneGold
	{
		private SceneGoldEventType name;

		private int wparam;

		private int lparam;

		public SendEvent_SceneGold(SceneGoldEventType m_name, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public SceneGoldEventType GetEventName()
		{
			return name;
		}

		public int GetWParam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}
	}

	public class BackEvent_SceneGold
	{
		private SceneGoldEventType name;

		private TUIGameInfo info;

		private int wparam;

		private int lparam;

		private bool control_success;

		private BackEventFalseType false_type;

		public BackEvent_SceneGold(SceneGoldEventType m_name, TUIGameInfo m_info, bool m_success = false)
		{
			name = m_name;
			info = m_info;
			control_success = m_success;
		}

		public BackEvent_SceneGold(SceneGoldEventType m_name, bool m_success = false, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			info = null;
			control_success = m_success;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public BackEvent_SceneGold(SceneGoldEventType m_name, bool m_success, BackEventFalseType m_false_type, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			control_success = m_success;
			false_type = m_false_type;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public SceneGoldEventType GetEventName()
		{
			return name;
		}

		public TUIGameInfo GetEventInfo()
		{
			return info;
		}

		public bool GetControlSuccess()
		{
			return control_success;
		}

		public int GetWparam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}

		public BackEventFalseType GetFalseType()
		{
			return false_type;
		}
	}

	public class SendEvent_SceneCoopInputName
	{
		private SceneCoopInputNameEventType name;

		private int wparam;

		private int lparam;

		private string str = string.Empty;

		public SendEvent_SceneCoopInputName(SceneCoopInputNameEventType m_name, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public SendEvent_SceneCoopInputName(SceneCoopInputNameEventType m_name, string m_str)
		{
			name = m_name;
			str = m_str;
		}

		public SceneCoopInputNameEventType GetEventName()
		{
			return name;
		}

		public int GetWParam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}

		public string GetStr()
		{
			return str;
		}
	}

	public class BackEvent_SceneCoopInputName
	{
		private SceneCoopInputNameEventType name;

		private int wparam;

		private int lparam;

		private TUIGameInfo info;

		private bool control_success;

		public BackEvent_SceneCoopInputName(SceneCoopInputNameEventType m_name, TUIGameInfo m_info, bool m_success = false)
		{
			name = m_name;
			info = m_info;
			control_success = m_success;
		}

		public BackEvent_SceneCoopInputName(SceneCoopInputNameEventType m_name, bool m_success = false, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			info = null;
			control_success = m_success;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public SceneCoopInputNameEventType GetEventName()
		{
			return name;
		}

		public TUIGameInfo GetEventInfo()
		{
			return info;
		}

		public bool GetControlSuccess()
		{
			return control_success;
		}

		public int GetWparam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}
	}

	public class SendEvent_SceneCoopMainMenu
	{
		private SceneCoopMainMenuEventType name;

		private int wparam;

		private int lparam;

		private string str = string.Empty;

		public SendEvent_SceneCoopMainMenu(SceneCoopMainMenuEventType m_name, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public SendEvent_SceneCoopMainMenu(SceneCoopMainMenuEventType m_name, string m_str)
		{
			name = m_name;
			str = m_str;
		}

		public string GetStr()
		{
			return str;
		}

		public SceneCoopMainMenuEventType GetEventName()
		{
			return name;
		}

		public int GetWParam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}
	}

	public class BackEvent_SceneCoopMainMenu
	{
		private SceneCoopMainMenuEventType name;

		private int wparam;

		private int lparam;

		private TUIGameInfo info;

		private bool control_success;

		private string str = string.Empty;

		public BackEvent_SceneCoopMainMenu(SceneCoopMainMenuEventType m_name, TUIGameInfo m_info, bool m_success = false)
		{
			name = m_name;
			info = m_info;
			control_success = m_success;
		}

		public BackEvent_SceneCoopMainMenu(SceneCoopMainMenuEventType m_name, bool m_success = false, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			info = null;
			control_success = m_success;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public BackEvent_SceneCoopMainMenu(SceneCoopMainMenuEventType m_name, bool m_success, string m_str)
		{
			name = m_name;
			control_success = m_success;
			str = m_str;
		}

		public SceneCoopMainMenuEventType GetEventName()
		{
			return name;
		}

		public TUIGameInfo GetEventInfo()
		{
			return info;
		}

		public bool GetControlSuccess()
		{
			return control_success;
		}

		public int GetWparam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}

		public string GetStr()
		{
			return str;
		}
	}

	public class SendEvent_SceneCoopRoom
	{
		private SceneCoopRoomEventType name;

		private int wparam;

		private int lparam;

		public SendEvent_SceneCoopRoom(SceneCoopRoomEventType m_name, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public SceneCoopRoomEventType GetEventName()
		{
			return name;
		}

		public int GetWParam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}
	}

	public class BackEvent_SceneCoopRoom
	{
		private SceneCoopRoomEventType name;

		private int wparam;

		private int lparam;

		private TUIGameInfo info;

		private bool control_success;

		private string str = string.Empty;

		public BackEvent_SceneCoopRoom(SceneCoopRoomEventType m_name, TUIGameInfo m_info, bool m_success = false)
		{
			name = m_name;
			info = m_info;
			control_success = m_success;
		}

		public BackEvent_SceneCoopRoom(SceneCoopRoomEventType m_name, bool m_success = false, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			info = null;
			control_success = m_success;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public BackEvent_SceneCoopRoom(SceneCoopRoomEventType m_name, string m_str)
		{
			name = m_name;
			str = m_str;
		}

		public SceneCoopRoomEventType GetEventName()
		{
			return name;
		}

		public TUIGameInfo GetEventInfo()
		{
			return info;
		}

		public bool GetControlSuccess()
		{
			return control_success;
		}

		public int GetWparam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}

		public string GetStr()
		{
			return str;
		}
	}

	public class SendEvent_SceneBlackMarket
	{
		private SceneBlackMarketEventType name;

		private int wparam;

		private int lparam;

		public SendEvent_SceneBlackMarket(SceneBlackMarketEventType m_name, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public SceneBlackMarketEventType GetEventName()
		{
			return name;
		}

		public int GetWParam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}
	}

	public class BackEvent_SceneBlackMarket
	{
		private SceneBlackMarketEventType name;

		private int wparam;

		private int lparam;

		private TUIGameInfo info;

		private bool control_success;

		private BackEventFalseType false_type;

		public BackEvent_SceneBlackMarket(SceneBlackMarketEventType m_name, TUIGameInfo m_info, bool m_success = false)
		{
			name = m_name;
			info = m_info;
			control_success = m_success;
		}

		public BackEvent_SceneBlackMarket(SceneBlackMarketEventType m_name, bool m_success = false, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			control_success = m_success;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public BackEvent_SceneBlackMarket(SceneBlackMarketEventType m_name, bool m_success, BackEventFalseType m_false_type, int m_wparam = 0, int m_lparam = 0)
		{
			name = m_name;
			control_success = m_success;
			false_type = m_false_type;
			wparam = m_wparam;
			lparam = m_lparam;
		}

		public bool GetControlSuccess()
		{
			return control_success;
		}

		public TUIGameInfo GetEventInfo()
		{
			return info;
		}

		public SceneBlackMarketEventType GetEventName()
		{
			return name;
		}

		public int GetWparam()
		{
			return wparam;
		}

		public int GetLparam()
		{
			return lparam;
		}

		public BackEventFalseType GetFalseType()
		{
			return false_type;
		}
	}
}
