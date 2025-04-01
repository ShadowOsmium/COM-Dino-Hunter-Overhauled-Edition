using System;
using BehaviorTree;

public class iGameData
{
	public iWeaponCenter m_WeaponCenter { get; private set; }

	public iBuffCenter m_BuffCenter { get; private set; }

	public iSkillCenter m_SkillCenter { get; private set; }

	public iMobCenter m_MobCenter { get; private set; }

	public iMGCenter m_MGCenter { get; private set; }

	public iGameLevelCenter m_GameLevelCenter { get; private set; }

	public iBehaviorCenter m_BehaviorCenter { get; private set; }

	public iAICenter m_AICenter { get; private set; }

	public iTaskCenter m_TaskCenter { get; private set; }

	public iItemCenter m_ItemCenter { get; private set; }

	public iRecipeCenter m_RecipeCenter { get; private set; }

	public iCharacterCenter m_CharacterCenter { get; private set; }

	public iBattleGroupCenter m_BattleGroupCenter { get; private set; }

	public iDropGroupCenter m_DropGroupCenter { get; private set; }

	public iLoadTipCenter m_LoadTipCenter { get; private set; }

	public iIAPCenter m_IAPCenter { get; private set; }

	public iAchievementCenter m_AchievementCenter { get; private set; }

	public iDailyTaskCenter m_DailyTaskCenter { get; private set; }

	public iDailyRewardCenter m_DailyRewardCenter { get; private set; }

	public iStashCapacityCenter m_StashCapacityCenter { get; private set; }

	public iTitleCenter m_TitleCenter { get; private set; }

	public iHunterCenter m_HunterCenter { get; private set; }

	public iHunterLevelCenter m_HunterLevelCenter { get; private set; }

	public iDataCenter m_DataCenter { get; private set; }

	public iDataCenterNet m_DataCenterNet { get; private set; }

	public iAvatarCenter m_AvatarCenter { get; private set; }

	public iShopDisplayCenter m_ShopDisplayCenter { get; private set; }

	public iBlackMarketCenter m_BlackMarketCenter { get; private set; }

	public iGameData()
	{
		m_WeaponCenter = new iWeaponCenter();
		m_BuffCenter = new iBuffCenter();
		m_SkillCenter = new iSkillCenter();
		m_MobCenter = new iMobCenter();
		m_MGCenter = new iMGCenter();
		m_GameLevelCenter = new iGameLevelCenter();
		m_BehaviorCenter = new iBehaviorCenter();
		m_AICenter = new iAICenter();
		m_TaskCenter = new iTaskCenter();
		m_ItemCenter = new iItemCenter();
		m_CharacterCenter = new iCharacterCenter();
		m_BattleGroupCenter = new iBattleGroupCenter();
		m_DropGroupCenter = new iDropGroupCenter();
		m_LoadTipCenter = new iLoadTipCenter();
		m_IAPCenter = new iIAPCenter();
		m_AchievementCenter = new iAchievementCenter();
		m_DailyTaskCenter = new iDailyTaskCenter();
		m_DailyRewardCenter = new iDailyRewardCenter();
		m_StashCapacityCenter = new iStashCapacityCenter();
		m_TitleCenter = new iTitleCenter();
		m_HunterCenter = new iHunterCenter();
		m_HunterLevelCenter = new iHunterLevelCenter();
		m_DataCenter = new iDataCenter();
		m_DataCenterNet = new iDataCenterNet();
		m_AvatarCenter = new iAvatarCenter();
		m_ShopDisplayCenter = new iShopDisplayCenter();
		m_BlackMarketCenter = new iBlackMarketCenter();
	}

	public bool Load()
	{
		try
		{
			iGameApp.GetInstance().ScreenLog("Loading m_WeaponCenter");
			m_WeaponCenter.Load("weapon");
			iServerConfigData.GetInstance().AddConfigInfo(m_WeaponCenter.sFileName, m_WeaponCenter.sMD5, m_WeaponCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_BuffCenter");
			m_BuffCenter.Load("buff");
			iServerConfigData.GetInstance().AddConfigInfo(m_BuffCenter.sFileName, m_BuffCenter.sMD5, m_BuffCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_SkillCenter Monster");
			m_SkillCenter.Load_Monster("skillmonster");
			iServerConfigData.GetInstance().AddConfigInfo(m_SkillCenter.sFileNameMonster, m_SkillCenter.sMD5Monster, m_SkillCenter.OnFetchMonsterData);
			iGameApp.GetInstance().ScreenLog("Loading m_SkillCenter Player");
			m_SkillCenter.Load_Player("skillplayer");
			iServerConfigData.GetInstance().AddConfigInfo(m_SkillCenter.sFileNamePlayer, m_SkillCenter.sMD5Player, m_SkillCenter.OnFetchPlayerData);
			iGameApp.GetInstance().ScreenLog("Loading m_MobCenter");
			m_MobCenter.Load("mob");
			iServerConfigData.GetInstance().AddConfigInfo(m_MobCenter.sFileName, m_MobCenter.sMD5, m_MobCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_MGCenter");
			m_MGCenter.Load("gamewave");
			iServerConfigData.GetInstance().AddConfigInfo(m_MGCenter.sFileName, m_MGCenter.sMD5, m_MGCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_GameLevelCenter");
			m_GameLevelCenter.Load("gamelevel");
			iServerConfigData.GetInstance().AddConfigInfo(m_GameLevelCenter.sFileName, m_GameLevelCenter.sMD5, m_GameLevelCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_BehaviorCenter");
			m_BehaviorCenter.Load();
			iGameApp.GetInstance().ScreenLog("Loading m_AICenter");
			m_AICenter.Load("ai");
			iServerConfigData.GetInstance().AddConfigInfo(m_AICenter.sFileName, m_AICenter.sMD5, m_AICenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_TaskCenter");
			m_TaskCenter.Load("task");
			iServerConfigData.GetInstance().AddConfigInfo(m_TaskCenter.sFileName, m_TaskCenter.sMD5, m_TaskCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_ItemCenter");
			m_ItemCenter.Load("item");
			iServerConfigData.GetInstance().AddConfigInfo(m_ItemCenter.sFileName, m_ItemCenter.sMD5, m_ItemCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_CharacterCenter");
			m_CharacterCenter.Load("character");
			iServerConfigData.GetInstance().AddConfigInfo(m_CharacterCenter.sFileName, m_CharacterCenter.sMD5, m_CharacterCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_BattleGroupCenter");
			m_BattleGroupCenter.Load();
			iGameApp.GetInstance().ScreenLog("Loading m_DropGroupCenter");
			m_DropGroupCenter.Load("dropgroup");
			iServerConfigData.GetInstance().AddConfigInfo(m_DropGroupCenter.sFileName, m_DropGroupCenter.sMD5, m_DropGroupCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_LoadTipCenter");
			m_LoadTipCenter.Load("loadtip");
			iServerConfigData.GetInstance().AddConfigInfo(m_LoadTipCenter.sFileName, m_LoadTipCenter.sMD5, m_LoadTipCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_IAPCenter");
			m_IAPCenter.Load("iap");
			m_IAPCenter.LoadCrystal2Gold();
			iServerConfigData.GetInstance().AddConfigInfo(m_IAPCenter.sFileName, m_IAPCenter.sMD5, m_IAPCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_AchievementCenter");
			m_AchievementCenter.Load("achievement");
			iServerConfigData.GetInstance().AddConfigInfo(m_AchievementCenter.sFileName, m_AchievementCenter.sMD5, m_AchievementCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_DailyTaskCenter");
			m_DailyTaskCenter.Load("dailytask");
			iServerConfigData.GetInstance().AddConfigInfo(m_DailyTaskCenter.sFileName, m_DailyTaskCenter.sMD5, m_DailyTaskCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_DailyRewardCenter");
			m_DailyRewardCenter.Load("dailyreward");
			iServerConfigData.GetInstance().AddConfigInfo(m_DailyRewardCenter.sFileName, m_DailyRewardCenter.sMD5, m_DailyRewardCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_StashCapacityCenter");
			m_StashCapacityCenter.Load("stashcapacity");
			iServerConfigData.GetInstance().AddConfigInfo(m_StashCapacityCenter.sFileName, m_StashCapacityCenter.sMD5, m_StashCapacityCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_TitleCenter");
			m_TitleCenter.Load("title");
			iServerConfigData.GetInstance().AddConfigInfo(m_TitleCenter.sFileName, m_TitleCenter.sMD5, m_TitleCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_HunterCenter");
			m_HunterCenter.Load("hunter");
			iServerConfigData.GetInstance().AddConfigInfo(m_HunterCenter.sFileName, m_HunterCenter.sMD5, m_HunterCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_HunterLevelCenter");
			m_HunterLevelCenter.Load("hunterlevel");
			iServerConfigData.GetInstance().AddConfigInfo(m_HunterLevelCenter.sFileName, m_HunterLevelCenter.sMD5, m_HunterLevelCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_AvatarCenter");
			m_AvatarCenter.Load("avatar");
			iServerConfigData.GetInstance().AddConfigInfo(m_AvatarCenter.sFileName, m_AvatarCenter.sMD5, m_AvatarCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_ShopDisplayCenter");
			m_ShopDisplayCenter.Load("shopdisplay");
			iServerConfigData.GetInstance().AddConfigInfo(m_ShopDisplayCenter.sFileName, m_ShopDisplayCenter.sMD5, m_ShopDisplayCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_BlackMarketCenter");
			m_BlackMarketCenter.Load("blackmarket");
			iServerConfigData.GetInstance().AddConfigInfo(m_BlackMarketCenter.sFileName, m_BlackMarketCenter.sMD5, m_BlackMarketCenter.OnFetch);
			iGameApp.GetInstance().ScreenLog("Loading m_DataCenter");
			m_DataCenter.Load();
			iGameApp.GetInstance().ScreenLog("Loading m_DataCenterNet");
			m_DataCenterNet.Load();
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError(ex);
			iGameApp.GetInstance().ScreenLog("error msg: " + ex.Message);
		}
		iGameApp.GetInstance().ClearScreenLog();
		return true;
	}

	public iTaskCenter GetTaskCenter()
	{
		return m_TaskCenter;
	}

	public iDataCenter GetDataCenter()
	{
		return m_DataCenter;
	}

	public iDataCenterNet GetDataCenterNet()
	{
		return m_DataCenterNet;
	}

	public iCharacterCenter GetCharacterCenter()
	{
		return m_CharacterCenter;
	}

	public iWeaponCenter GetWeaponCenter()
	{
		return m_WeaponCenter;
	}

	public iSkillCenter GetSkillCenter()
	{
		return m_SkillCenter;
	}

	public iItemCenter GetItemCenter()
	{
		return m_ItemCenter;
	}

	public iBattleGroupCenter GetBattleGroupCenter()
	{
		return m_BattleGroupCenter;
	}

	public iGameLevelCenter GetGameLevelCenter()
	{
		return m_GameLevelCenter;
	}

	public iStashCapacityCenter GetStashCapacityCenter()
	{
		return m_StashCapacityCenter;
	}

	public iAchievementCenter GetAchievementCenter()
	{
		return m_AchievementCenter;
	}

	public iDailyRewardCenter GetDailyRewardCenter()
	{
		return m_DailyRewardCenter;
	}

	public iDailyTaskCenter GetDailyTaskCenter()
	{
		return m_DailyTaskCenter;
	}

	public iIAPCenter GetIAPCenter()
	{
		return m_IAPCenter;
	}

	public CCharacterInfo GetCharacterInfo(int nID)
	{
		if (m_CharacterCenter == null)
		{
			return null;
		}
		return m_CharacterCenter.Get(nID);
	}

	public CCharacterInfoLevel GetCharacterInfo(int nID, int nLevel)
	{
		if (m_CharacterCenter == null)
		{
			return null;
		}
		return m_CharacterCenter.Get(nID, nLevel);
	}

	public CWeaponInfo GetWeaponInfo(int nID)
	{
		if (m_WeaponCenter == null)
		{
			return null;
		}
		return m_WeaponCenter.Get(nID);
	}

	public CWeaponInfoLevel GetWeaponInfo(int nID, int nLevel)
	{
		if (m_WeaponCenter == null)
		{
			UnityEngine.Debug.LogError("oopsy!!");
			return null;
		}
		return m_WeaponCenter.Get(nID, nLevel);
	}

	public CBuffInfo GetBuffInfo(int nID)
	{
		if (m_BuffCenter == null)
		{
			return null;
		}
		return m_BuffCenter.GetBuffInfo(nID);
	}

	public CSkillInfo GetSkillInfo(int nID)
	{
		if (m_SkillCenter == null)
		{
			return null;
		}
		return m_SkillCenter.GetSkillInfo(nID);
	}

	public CSkillInfoLevel GetSkillInfo(int nID, int nLevel)
	{
		if (m_SkillCenter == null)
		{
			return null;
		}
		return m_SkillCenter.GetSkillInfo(nID, nLevel);
	}

	public CSkillComboInfo GetSkillComboInfo(int nComboID)
	{
		if (m_SkillCenter == null)
		{
			return null;
		}
		return m_SkillCenter.GetSkillComboInfo(nComboID);
	}

	public CMobInfoLevel GetMobInfo(int nID, int nLevel)
	{
		if (m_MobCenter == null)
		{
			return null;
		}
		return m_MobCenter.Get(nID, nLevel);
	}

	public Node GetBehavior(int nID)
	{
		if (m_BehaviorCenter == null)
		{
			return null;
		}
		return m_BehaviorCenter.GetBehavior(nID);
	}

	public CAIInfo GetAIInfo(int nID)
	{
		if (m_AICenter == null)
		{
			return null;
		}
		return m_AICenter.GetAIInfo(nID);
	}

	public CAIManagerInfo GetAIManagerInfo(int nID)
	{
		if (m_AICenter == null)
		{
			return null;
		}
		return m_AICenter.GetAIManagerInfo(nID);
	}

	public WaveInfo GetWaveInfo(int nID)
	{
		if (m_MGCenter == null)
		{
			return null;
		}
		return m_MGCenter.Get(nID);
	}

	public GameLevelInfo GetGameLevelInfo(int nID)
	{
		if (m_GameLevelCenter == null)
		{
			return null;
		}
		return m_GameLevelCenter.Get(nID);
	}

	public GameLevelGroupInfo GetGameLevelGroupInfo(int nID)
	{
		if (m_GameLevelCenter == null)
		{
			return null;
		}
		return m_GameLevelCenter.GetGroupInfo(nID);
	}

	public CTaskInfo GetTaskInfo(int nID)
	{
		if (m_TaskCenter == null)
		{
			return null;
		}
		return m_TaskCenter.Get(nID);
	}

	public CItemInfo GetItemInfo(int nID)
	{
		if (m_ItemCenter == null)
		{
			return null;
		}
		return m_ItemCenter.Get(nID);
	}

	public CItemInfoLevel GetItemInfo(int nID, int nLevel)
	{
		if (m_ItemCenter == null)
		{
			return null;
		}
		return m_ItemCenter.Get(nID, nLevel);
	}

	public CRecipeInfo GetRecipeInfo(int nID)
	{
		if (m_RecipeCenter == null)
		{
			return null;
		}
		return m_RecipeCenter.Get(nID);
	}

	public CDropGroupInfo GetDropGrouInfo(int nID)
	{
		if (m_DropGroupCenter == null)
		{
			return null;
		}
		return m_DropGroupCenter.Get(nID);
	}

	public CLoadTipInfo GetLoadTipInfoRandom()
	{
		if (m_LoadTipCenter == null)
		{
			return null;
		}
		return m_LoadTipCenter.GetRandom();
	}

	public CStashCapacity GetStashCapacity(int nLevel)
	{
		if (m_StashCapacityCenter == null)
		{
			return null;
		}
		return m_StashCapacityCenter.Get(nLevel - 1);
	}

	public CIAPInfo GetIAPInfo(int nIAPID)
	{
		if (m_IAPCenter == null)
		{
			return null;
		}
		return m_IAPCenter.Get(nIAPID);
	}

	public CIAPInfo GetIAPInfoBySeq(int nIndex)
	{
		if (m_IAPCenter == null)
		{
			return null;
		}
		return m_IAPCenter.GetBySeq(nIndex);
	}

	public CIAPInfo GetIAPInfoByKey(string key)
	{
		if (m_IAPCenter == null)
		{
			return null;
		}
		return m_IAPCenter.GetByKey(key);
	}

	public CCrystal2GoldInfo GetCrystal2GoldInfo(int nID)
	{
		if (m_IAPCenter == null)
		{
			return null;
		}
		return m_IAPCenter.GetCrystal2GoldInfo(nID);
	}

	public CAchievementInfo GetAchievementInfo(int nID)
	{
		if (m_AchievementCenter == null)
		{
			return null;
		}
		return m_AchievementCenter.Get(nID);
	}
}
