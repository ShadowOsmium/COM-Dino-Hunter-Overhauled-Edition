using System;
using UnityEngine;

public class iMacroDefine
{
	public const string sGameVersion = "3.1.7a";

	public const int CarryPassiveSkillMax = 3;

	public const int CarryWeaponMax = 3;

	public const int SkillFuncMax = 3;

	public const int SkillLimitMax = 3;

	public const int SkillNum = 1;

	public const int SkillPassiveNum = 3;

	public const int BuffFuncMax = 3;

	public const int BuffNumMax = 10;

	public const int FuncMaxNum = 3;

	public const int MeleeDis = 5;

	public const float MeleeAngle = 15f;

	public const int NetRoomMaxNumber = 2;

	public static float MeleeAngleCos = Mathf.Cos((float)Math.PI / 12f);

	public static float SlipRateWidth = 0.002f;

	public static float SlipRateHeight = 0.01f;

	public static int GainMaterialFromGameMax = 8;

	public static int GainMaterialFromTaskMax = 2;

	public static int SecondsOneDay = 86400;

	public static float ReloginInBackgroundTime = 15f;

	public static float LoginGameServerTimeout = 30f;

	public static string BundleID = "com.trinitigame.callofminidinohunter";

	public static string AddressForItunes = "https://play.google.com/store/apps/details?id=com.trinitigame.android.callofminidinohunter";

	public static string SaveExpandedName = ".txt";

	public static string CompanyAccountURL = "http://account.trinitigame.com/game/CoMDHAndroid";

	public static float RankInfo_RefreshTime = 10f;

	public static float RankInfoFriends_RefreshTime = 10f;

	public static float Photo_RefreshTime = 30f;

	public static float NameCard_RefreshTime = 10f;

	public static string ServerUrl_CoopData = string.Empty;

	public static float m_fMonsterPower_Life = 2f;

	public static float m_fMonsterPower_Damage = 2f;

	public static float m_fMonsterPower_Lvl = 1f;

	public static float m_fMonsterReward_Gold = 2f;

	public static float m_fMonsterReward_Exp = 2f;

	public static float m_fMonsterReward_Lvl = 1f;

	public static float m_fStageReward_Gold = 1f;

	public static float m_fStageReward_Exp = 1f;

	public static float m_fStageReward_Lvl = 1f;

	public static string path_model_root = "Artist/Model/CharacterAvatar/Model";

	public static string path_texture_root = "Artist/Model/CharacterAvatar/Texture";

	public static string path_effect_root = "Artist/Effect/Skill/halo";
}
