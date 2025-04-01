using System.Collections;

public class CFlurryManager
{
	public class CEnterStageInfo
	{
		public string sCharID;

		public string sCharLevel;

		public string[] arrWeaponID;

		public string[] arrSkillID;

		public string sEquipStoneID;

		public string sLevelID;

		public int nLevelProccess;

		public int nLastStageArrive;
	}

	public class CPurchaseSkillInfo
	{
		public string sCharID;

		public string sCharLevel;

		public string sSkillID;
	}

	public class CUpgradeSkillInfo
	{
		public string sCharID;

		public string sCharLevel;

		public string sSkillID;

		public string sSkillLevel;
	}

	public class CPurchaseWeaponInfo
	{
		public string sCharID;

		public string sCharLevel;

		public string sWeaponID;

		public int nLevelProccess;
	}

	public class CUpgradeWeaponInfo
	{
		public string sCharID;

		public string sCharLevel;

		public string sWeaponID;

		public string sWeaponLevel;

		public int nLevelProccess;
	}

	public class CPurchaseStoneInfo
	{
		public string sCharID;

		public string sCharLevel;

		public string sStoneID;

		public int nLevelProccess;
	}

	public class CUpgradeStoneInfo
	{
		public string sCharID;

		public string sCharLevel;

		public string sStoneID;

		public string sStoneLevel;

		public int nLevelProccess;
	}

	public class CPurchaseCharInfo
	{
		public string sCharID;

		public int nLevelProccess;
	}

	public class CPurchaseBulletInfo
	{
		public string sCharID;

		public string sCharLevel;

		public string sLevelID;
	}

	public class CPurchaseIAPInfo
	{
		public string sCharID;

		public string sCharLevel;

		public string sIAP;

		public string sLevelID;
	}

	public enum kConsumeType
	{
		Weapon,
		Skill,
		Char,
		Stone,
		Material,
		StashSize,
		Revive,
		Bullet,
		Gold,
		Avatar
	}

	public class CReviveInfo
	{
		public string sCharID;

		public string sCharLevel;

		public string[] arrWeaponID;

		public string[] arrSkillID;

		public string sEquipStoneID;

		public string sLevelID;

		public int nLevelProccess;
	}

	public class CAchiInfo
	{
		public string sCharID;

		public string sCharLevel;

		public string sAchiID;

		public string sAchiLevel;
	}

	protected static CFlurryManager m_Instance;

	public static CFlurryManager GetInstance()
	{
		if (m_Instance == null)
		{
			m_Instance = new CFlurryManager();
		}
		return m_Instance;
	}

	public void Initialize(string sKey)
	{
		FlurryPlugin.StartSession(sKey);
	}

	public void Destroy()
	{
	}

	public void EnterApp()
	{
		FlurryPlugin.logEvent("EnterApp");
	}

	public void EnterStage(string sLevelID, CEnterStageInfo info)
	{
		string empty = string.Empty;
		Hashtable hashtable = new Hashtable();
		hashtable.Add("charid", info.sCharID);
		hashtable.Add("charlevel", info.sCharLevel);
		empty = MyUtils.CombinateStr(info.arrWeaponID);
		if (empty.Length > 0)
		{
			hashtable.Add("weapon", empty);
		}
		empty = MyUtils.CombinateStr(info.arrSkillID);
		if (empty.Length > 0)
		{
			hashtable.Add("skill", empty);
		}
		hashtable.Add("stone", info.sEquipStoneID);
		hashtable.Add("stageid", info.sLevelID);
		hashtable.Add("stageproccess", info.nLevelProccess.ToString());
		hashtable.Add("laststagearrive", info.nLastStageArrive.ToString());
		FlurryPlugin.logEvent("Fight_Normal_" + sLevelID, hashtable, true);
	}

	public void LoseStage(string sLevelID, CEnterStageInfo info)
	{
		string empty = string.Empty;
		Hashtable hashtable = new Hashtable();
		hashtable.Add("charid", info.sCharID);
		hashtable.Add("charlevel", info.sCharLevel);
		empty = MyUtils.CombinateStr(info.arrWeaponID);
		if (empty.Length > 0)
		{
			hashtable.Add("weapon", empty);
		}
		empty = MyUtils.CombinateStr(info.arrSkillID);
		if (empty.Length > 0)
		{
			hashtable.Add("skill", empty);
		}
		hashtable.Add("stone", info.sEquipStoneID);
		hashtable.Add("stageid", info.sLevelID);
		hashtable.Add("stageproccess", info.nLevelProccess.ToString());
		hashtable.Add("laststagearrive", info.nLastStageArrive.ToString());
		FlurryPlugin.logEvent("Fight_Lose_" + sLevelID, hashtable);
		FlurryPlugin.endTimedEvent("Fight_Normal_" + sLevelID);
	}

	public void WinStage(string sLevelID, CEnterStageInfo info)
	{
		string empty = string.Empty;
		Hashtable hashtable = new Hashtable();
		hashtable.Add("charid", info.sCharID);
		hashtable.Add("charlevel", info.sCharLevel);
		empty = MyUtils.CombinateStr(info.arrWeaponID);
		if (empty.Length > 0)
		{
			hashtable.Add("weapon", empty);
		}
		empty = MyUtils.CombinateStr(info.arrSkillID);
		if (empty.Length > 0)
		{
			hashtable.Add("skill", empty);
		}
		hashtable.Add("stone", info.sEquipStoneID);
		hashtable.Add("stage", info.sLevelID);
		hashtable.Add("stageproccess", info.nLevelProccess.ToString());
		hashtable.Add("laststagearrive", info.nLastStageArrive.ToString());
		FlurryPlugin.logEvent("Fight_Win_" + sLevelID, hashtable);
		FlurryPlugin.endTimedEvent("Fight_Normal_" + sLevelID);
	}

	public void QuitStage(string sLevelID, CEnterStageInfo info)
	{
		string empty = string.Empty;
		Hashtable hashtable = new Hashtable();
		hashtable.Add("charid", info.sCharID);
		hashtable.Add("charlevel", info.sCharLevel);
		empty = MyUtils.CombinateStr(info.arrWeaponID);
		if (empty.Length > 0)
		{
			hashtable.Add("weapon", empty);
		}
		empty = MyUtils.CombinateStr(info.arrSkillID);
		if (empty.Length > 0)
		{
			hashtable.Add("skill", empty);
		}
		hashtable.Add("stone", info.sEquipStoneID);
		hashtable.Add("stage", info.sLevelID);
		hashtable.Add("stageproccess", info.nLevelProccess.ToString());
		FlurryPlugin.logEvent("Fight_Quit_" + sLevelID, hashtable);
		FlurryPlugin.endTimedEvent("Fight_Normal_" + sLevelID);
	}

	public void PurchaseSkill(string sSkillID, CPurchaseSkillInfo info)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("charid", info.sCharID);
		hashtable.Add("charlevel", info.sCharLevel);
		hashtable.Add("skillid", info.sSkillID);
		FlurryPlugin.logEvent("Shop_BuySkill_" + sSkillID, hashtable);
	}

	public void UpgradeSkill(string sSkillID, CUpgradeSkillInfo info)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("charid", info.sCharID);
		hashtable.Add("charlevel", info.sCharLevel);
		hashtable.Add("skillid", info.sSkillID);
		hashtable.Add("skilllevel", info.sSkillLevel);
		FlurryPlugin.logEvent("Shop_UpgradeSkill_" + sSkillID, hashtable);
	}

	public void PurchaseWeapon(string sWeaponID, CPurchaseWeaponInfo info)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("charid", info.sCharID);
		hashtable.Add("charlevel", info.sCharLevel);
		hashtable.Add("weaponid", info.sWeaponID);
		hashtable.Add("stageproccess", info.nLevelProccess.ToString());
		FlurryPlugin.logEvent("Shop_BuyWeapon", hashtable);
	}

	public void UpgradeWeapon(string sWeaponID, CUpgradeWeaponInfo info)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("charid", info.sCharID);
		hashtable.Add("charlevel", info.sCharLevel);
		hashtable.Add("weaponid", info.sWeaponID);
		hashtable.Add("weaponlevel", info.sWeaponLevel);
		hashtable.Add("stageproccess", info.nLevelProccess.ToString());
		FlurryPlugin.logEvent("Shop_UpgradeWeapon_" + sWeaponID, hashtable);
	}

	public void PurchaseStone(string sStoneID, CPurchaseStoneInfo info)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("charid", info.sCharID);
		hashtable.Add("charlevel", info.sCharLevel);
		hashtable.Add("stone", info.sStoneID);
		hashtable.Add("stageproccess", info.nLevelProccess.ToString());
		FlurryPlugin.logEvent("Shop_BuyStone_" + sStoneID, hashtable);
	}

	public void UpgradeStone(string sStoneID, CUpgradeStoneInfo info)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("charid", info.sCharID);
		hashtable.Add("charlevel", info.sCharLevel);
		hashtable.Add("stone", info.sStoneID);
		hashtable.Add("stonelevel", info.sStoneLevel);
		hashtable.Add("stageproccess", info.nLevelProccess.ToString());
		FlurryPlugin.logEvent("Shop_UpgradeStone_" + sStoneID, hashtable);
	}

	public void PurchaseChar(string sCharID, CPurchaseCharInfo info)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("charid", info.sCharID);
		hashtable.Add("stageproccess", info.nLevelProccess.ToString());
		FlurryPlugin.logEvent("Shot_BuyAvatar_" + sCharID, hashtable);
	}

	public void PurchaseBullet(string sLevelID, CPurchaseBulletInfo info)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("charid", info.sCharID);
		hashtable.Add("charlevel", info.sCharLevel);
		hashtable.Add("stage", info.sLevelID);
		FlurryPlugin.logEvent("Shot_BuyAmmo_" + sLevelID, hashtable);
	}

	public void PurchaseIAP(string sIAPID, CPurchaseIAPInfo info)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("charid", info.sCharID);
		hashtable.Add("charlevel", info.sCharLevel);
		hashtable.Add("stage", info.sLevelID);
		FlurryPlugin.logEvent("IAP_Buy_" + sIAPID, hashtable);
	}

	public void ConsumeGold(kConsumeType type)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("ConsumeType", type.ToString());
		FlurryPlugin.logEvent("Shop_Gold", hashtable);
	}

	public void ConsumeCrystal(kConsumeType type)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("ConsumeType", type.ToString());
		FlurryPlugin.logEvent("Shop_tCrystal", hashtable);
	}

	public void CharRevive(string sLevelID, CReviveInfo info)
	{
		string empty = string.Empty;
		Hashtable hashtable = new Hashtable();
		hashtable.Add("charid", info.sCharID);
		hashtable.Add("charlevel", info.sCharLevel);
		empty = MyUtils.CombinateStr(info.arrWeaponID);
		if (empty.Length > 0)
		{
			hashtable.Add("weapon", empty);
		}
		empty = MyUtils.CombinateStr(info.arrSkillID);
		if (empty.Length > 0)
		{
			hashtable.Add("skill", empty);
		}
		hashtable.Add("stone", info.sEquipStoneID);
		hashtable.Add("stage", info.sLevelID);
		hashtable.Add("stageproccess", info.nLevelProccess.ToString());
		FlurryPlugin.logEvent("Death_Alert_Item_" + sLevelID, hashtable);
	}

	public void GainAchi(string sAchiID, CAchiInfo info)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("charid", info.sCharID);
		hashtable.Add("charlevel", info.sCharLevel);
		hashtable.Add("achiid", info.sAchiID);
		hashtable.Add("achilevel", info.sAchiLevel);
		FlurryPlugin.logEvent("Achi_Gain_" + sAchiID, hashtable);
	}
}
