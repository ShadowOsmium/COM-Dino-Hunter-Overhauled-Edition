using System.Collections.Generic;
using UnityEngine;

public class TUIMappingInfo
{
	public class CTUIMaterialInfo
	{
		public int m_nID;

		public GoodsQualityType m_nQuality;

		public string m_sName = string.Empty;

		public TUIPriceInfo m_PurchasePrice;

		public CTUIMaterialInfo(int nID, GoodsQualityType nQuality, string sName, TUIPriceInfo purchaseprice)
		{
			m_nID = nID;
			m_nQuality = nQuality;
			m_sName = sName;
			m_PurchasePrice = purchaseprice;
		}
	}

	public delegate bool GetAvatarModel(int avatarid, int charid, ref GameObject modelprefab, ref Texture modeltexture);

	public delegate bool GetCharacterDefaultAvatar(int roleid, WeaponType type, ref int avatarid);

	public delegate void SwitchSceneStr(string m_next_scene);

	public delegate void SwitchSceneInt(int m_id);

	public delegate Transform FireEffect(int m_id);

	public delegate int GoldToCrystal(int m_gold);

	public delegate void DoNewHelpFunc(NewHelpState m_state);

	private static TUIMappingInfo instance = null;

	private static Vector3 current_angle = Vector3.zero;

	public Dictionary<int, string> stash_dictionary;

	public Dictionary<int, string> skill_dictionary;

	public Dictionary<int, string> weapon_dictionary;

	public Dictionary<int, string> prop_dictionary;

	public Dictionary<int, string> role_dictionary;

	public Dictionary<int, string> map_dictionary;

	public SwitchSceneStr switch_scene_function_str;

	public SwitchSceneInt switch_scene_function_int;

	public FireEffect fire_effect_function;

	public GoldToCrystal gold_to_crystal_function;

	public DoNewHelpFunc do_new_help_function;

	public NewHelpState help_state = NewHelpState.None;

	public GetAvatarModel m_GetAvatarModel;

	public GetCharacterDefaultAvatar m_GetCharacterDefaultAvatar;

	public object current_scene;

	public string m_sPathRootCustomTex = "Artist/Textrues";

	public string m_sPathRootCustomAtlas = "Artist/Atlas";

	public string m_sPathCustomSkillTex = "TUI/Skill";

	public string m_sPathCustomMaterialTex = "TUI/Goods";

	protected Dictionary<int, CTUIMaterialInfo> m_dictMaterialInfo;

	protected Dictionary<int, int> m_dictMaterialCount;

	public TUIMappingInfo()
	{
		m_dictMaterialInfo = new Dictionary<int, CTUIMaterialInfo>();
		m_dictMaterialCount = new Dictionary<int, int>();
		stash_dictionary = new Dictionary<int, string>();
		stash_dictionary[100001] = "bawanglong_BOSS_ya1";
		stash_dictionary[100002] = "shuangguanlong_BOSS_duzhi1";
		stash_dictionary[100003] = "shuangguanlong_gongyou_longpi1";
		stash_dictionary[100004] = "shuangguanlong_putong_duzhi1";
		stash_dictionary[100005] = "shuangguanlong_bianyi_duzhi1";
		stash_dictionary[100006] = "yilong_gongyou_longzhua1";
		stash_dictionary[100007] = "yilong_BOSS_yizhua1";
		stash_dictionary[100008] = "yilong_putong_yizhua1";
		stash_dictionary[100009] = "yilong_bianyi_yizhua1";
		stash_dictionary[100010] = "sanjiaolong_gongyou_linke1";
		stash_dictionary[100011] = "sanjiaolong_putong_touke1";
		stash_dictionary[100012] = "sanjiaolong_bianyi_touke1";
		stash_dictionary[100013] = "sanjiaolong_BOSS_touke1";
		stash_dictionary[100014] = "xunmenglong_BOSS_duanwei1";
		stash_dictionary[100015] = "xunmenglong_gongyou_longgu1";
		stash_dictionary[100016] = "xunmenglong_putong_duanwei1";
		stash_dictionary[100017] = "xunmenglong_bianyi_duanwei1";
		stash_dictionary[100018] = "jibeilong_BOSS_weici1";
		stash_dictionary[100019] = "jianlong_BOSS_beici1";
		stash_dictionary[100020] = "shuangqiyilong_boss_toujiao1";
		stash_dictionary[100021] = "shangu_gongyou_tiekuangshi1";
		stash_dictionary[100022] = "yulin_gongyou_caishuijing1";
		stash_dictionary[100023] = "yanjiang_gongyou_rongyankuai1";
		stash_dictionary[100024] = "bingchuan_gongyou_bingjiejin1";
		stash_dictionary[200001] = "shuangqiyilong_boss_toujiao3";
		stash_dictionary[200002] = "bawanglong_BOSS_ya3";
		stash_dictionary[200003] = "shuangguanlong_BOSS_duzhi3";
		stash_dictionary[200004] = "shuangguanlong_putong_duzhi3";
		stash_dictionary[200005] = "shuangguanlong_bianyi_duzhi3";
		stash_dictionary[200006] = "yilong_BOSS_yizhua";
		stash_dictionary[200007] = "yilong_putong_yizhua3";
		stash_dictionary[200008] = "yilong_bianyi_yizhua3";
		stash_dictionary[200009] = "sanjiaolong_BOSS_touke3";
		stash_dictionary[200010] = "sanjiaolong_putong_touke3";
		stash_dictionary[200011] = "sanjiaolong_bianyi_touke3";
		stash_dictionary[200012] = "xunmenglong_BOSS_duanwei3";
		stash_dictionary[200013] = "xunmenglong_putong_duanwei3";
		stash_dictionary[200014] = "xunmenglong_bianyi_duanwei3";
		stash_dictionary[200015] = "jibeilong_BOSS_weici3";
		stash_dictionary[200016] = "jianlong_BOSS_beici3";
		stash_dictionary[200017] = "shuangguanlong_gongyou_longpi3";
		stash_dictionary[200018] = "yilong_gongyou_longzhua3";
		stash_dictionary[200019] = "sanjiaolong_gongyou_linke3";
		stash_dictionary[200020] = "xunmenglong_gongyou_longgu3";
		stash_dictionary[300001] = "bawanglong_BOSS_beijia1";
		stash_dictionary[300002] = "shuangguanlong_BOSS_guan1";
		stash_dictionary[300003] = "shuangguanlong_putong_guan1";
		stash_dictionary[300004] = "shuangguanlong_bianyi_guan1";
		stash_dictionary[300005] = "yilong_BOSS_yimo1";
		stash_dictionary[300006] = "yilong_putong_yimo1";
		stash_dictionary[300007] = "yilong_bianyi_yimo1";
		stash_dictionary[300008] = "sanjiaolong_BOSS_jiao1";
		stash_dictionary[300009] = "sanjiaolong_putong_jiao1";
		stash_dictionary[300010] = "sanjiaolong_bianyi_jiao1";
		stash_dictionary[300011] = "xunmenglong_putong_tougu1";
		stash_dictionary[300012] = "xunmenglong_bianyi_tougu1";
		stash_dictionary[300013] = "xunmenglong_BOSS_tougu1";
		stash_dictionary[300014] = "jibeilong_BOSS_xiongmo1";
		stash_dictionary[300015] = "jianlong_boss_duanwei1";
		stash_dictionary[300016] = "shuangqiyilong_boss_tuijia1";
		stash_dictionary[300017] = "shangu_gongyou_tiekuangshi3";
		stash_dictionary[300018] = "yulin_gongyou_caishuijing3";
		stash_dictionary[300019] = "yanjiang_gongyou_rongyankuai3";
		stash_dictionary[300020] = "bingchuan_gongyou_bingjiejin3";
		stash_dictionary[400001] = "bawanglong_BOSS_beijia3";
		stash_dictionary[400002] = "shuangguanlong_BOSS_guan3";
		stash_dictionary[400003] = "shuangguanlong_putong_guan3";
		stash_dictionary[400004] = "shuangguanlong_bianyi_guan3";
		stash_dictionary[400005] = "yilong_BOSS_yimo3";
		stash_dictionary[400006] = "yilong_putong_yimo3";
		stash_dictionary[400007] = "yilong_bianyi_yimo3";
		stash_dictionary[400008] = "sanjiaolong_BOSS_jiao3";
		stash_dictionary[400009] = "sanjiaolong_putong_jiao3";
		stash_dictionary[400010] = "sanjiaolong_bianyi_jiao3";
		stash_dictionary[400011] = "xunmenglong_BOSS_tougu3";
		stash_dictionary[400012] = "xunmenglong_putong_tougu3";
		stash_dictionary[400013] = "xunmenglong_bianyi_tougu3";
		stash_dictionary[400014] = "jibeilong_BOSS_xiongmo3";
		stash_dictionary[400015] = "jianlong_boss_duanwei3";
		stash_dictionary[400016] = "shuangqiyilong_boss_tuijia3";
		stash_dictionary[500001] = "bawanglong_BOSS_touke1";
		stash_dictionary[500002] = "jibeilong_BOSS_gusui1";
		stash_dictionary[500003] = "jianlong_boss_tougu1";
		stash_dictionary[500004] = "shuangqiyilong_boss_tuijia1";
		stash_dictionary[600001] = "bawanglong_BOSS_touke3";
		stash_dictionary[600002] = "jibeilong_BOSS_gusui3";
		stash_dictionary[600003] = "jianlong_boss_tougu3";
		stash_dictionary[600004] = "shuangqiyilong_boss_ya3";
		stash_dictionary[600005] = "xiyou_lierenzhengming";
		stash_dictionary[700001] = "duihuanquan_1";
		stash_dictionary[700002] = "duihuanquan_2";
		stash_dictionary[700003] = "duihuanquan_3";
		stash_dictionary[700004] = "duihuanquan_4";
		stash_dictionary[700005] = "duihuanquan_5";
		skill_dictionary = new Dictionary<int, string>();
		skill_dictionary[2] = "chongfeng";
		skill_dictionary[4] = "dunxing";
		skill_dictionary[5] = "huti";
		skill_dictionary[1] = "kuangbao";
		skill_dictionary[3] = "zhiliao";
		skill_dictionary[6] = "kuangbao";
		skill_dictionary[99002] = "chongfeng2";
		skill_dictionary[99004] = "dunxing2";
		skill_dictionary[99005] = "huti2";
		skill_dictionary[99001] = "kuangbao2";
		skill_dictionary[99003] = "zhiliao2";
		skill_dictionary[99006] = "kuangbao2";
		skill_dictionary[1001] = "passiveskill_1001";
		skill_dictionary[1002] = "passiveskill_1002";
		skill_dictionary[1003] = "passiveskill_1003";
		skill_dictionary[1004] = "passiveskill_1004";
		skill_dictionary[1005] = "passiveskill_1005";
		skill_dictionary[1006] = "passiveskill_1006";
		skill_dictionary[1007] = "passiveskill_1007";
		skill_dictionary[2001] = "passiveskill_2001";
		skill_dictionary[2002] = "passiveskill_2002";
		skill_dictionary[2003] = "passiveskill_2003";
		skill_dictionary[2004] = "passiveskill_2004";
		skill_dictionary[2005] = "passiveskill_2005";
		skill_dictionary[2006] = "passiveskill_2006";
		skill_dictionary[2007] = "passiveskill_2007";
		skill_dictionary[2008] = "passiveskill_2008";
		skill_dictionary[2009] = "passiveskill_2009";
		skill_dictionary[3001] = "passiveskill_3001";
		skill_dictionary[3002] = "passiveskill_3002";
		skill_dictionary[3003] = "passiveskill_3003";
		skill_dictionary[3004] = "passiveskill_3004";
		skill_dictionary[3005] = "passiveskill_3005";
		skill_dictionary[3006] = "passiveskill_3006";
		skill_dictionary[3007] = "passiveskill_3007";
		skill_dictionary[3008] = "passiveskill_3008";
		skill_dictionary[3009] = "passiveskill_3009";
		skill_dictionary[4001] = "passiveskill_4001";
		skill_dictionary[4002] = "passiveskill_4002";
		skill_dictionary[4003] = "passiveskill_4003";
		skill_dictionary[4004] = "passiveskill_4004";
		skill_dictionary[4005] = "passiveskill_4005";
		skill_dictionary[4006] = "passiveskill_4006";
		skill_dictionary[4007] = "passiveskill_4007";
		skill_dictionary[4008] = "passiveskill_4008";
		skill_dictionary[5001] = "passiveskill_5001";
		skill_dictionary[5002] = "passiveskill_5002";
		skill_dictionary[5003] = "passiveskill_5003";
		skill_dictionary[5004] = "passiveskill_5004";
		skill_dictionary[5005] = "passiveskill_5005";
		skill_dictionary[5006] = "passiveskill_5006";
		skill_dictionary[5007] = "passiveskill_5007";
		skill_dictionary[5008] = "passiveskill_5008";
		skill_dictionary[5009] = "passiveskill_5009";
		skill_dictionary[6001] = "passiveskill_1001";
		skill_dictionary[6002] = "passiveskill_1002";
		skill_dictionary[6003] = "passiveskill_1003";
		skill_dictionary[6004] = "passiveskill_1004";
		skill_dictionary[6005] = "passiveskill_1005";
		skill_dictionary[6006] = "passiveskill_1006";
		skill_dictionary[6007] = "passiveskill_1007";
		weapon_dictionary = new Dictionary<int, string>();
		weapon_dictionary[1] = "Weapon001";
		weapon_dictionary[2] = "Weapon002";
		weapon_dictionary[3] = "Weapon003";
		weapon_dictionary[4] = "Weapon004";
		weapon_dictionary[5] = "Weapon005";
		weapon_dictionary[6] = "Weapon006";
		weapon_dictionary[7] = "Weapon007";
		weapon_dictionary[8] = "Weapon008";
		weapon_dictionary[9] = "Weapon009";
		weapon_dictionary[10] = "Weapon010";
		weapon_dictionary[11] = "Weapon011";
		weapon_dictionary[12] = "Weapon012";
		weapon_dictionary[13] = "Weapon013";
		weapon_dictionary[14] = "Weapon014";
		weapon_dictionary[15] = "Weapon015";
		weapon_dictionary[16] = "Weapon016";
		weapon_dictionary[17] = "Weapon017";
		weapon_dictionary[18] = "Weapon018";
		weapon_dictionary[19] = "Weapon019";
		weapon_dictionary[21] = "Weapon021";
		weapon_dictionary[23] = "Weapon023";
		weapon_dictionary[24] = "Weapon024";
		weapon_dictionary[25] = "Weapon025";
		weapon_dictionary[26] = "Weapon026";
		weapon_dictionary[27] = "Weapon027";
		weapon_dictionary[28] = "Weapon028";
		weapon_dictionary[29] = "Weapon029";
		weapon_dictionary[30] = "Weapon030";
		weapon_dictionary[31] = "Weapon031";
		weapon_dictionary[32] = "Weapon032";
		weapon_dictionary[33] = "Weapon033";
		weapon_dictionary[34] = "Weapon034";
		weapon_dictionary[20001] = "avatar_01_h";
		weapon_dictionary[20002] = "avatar_02_h";
		weapon_dictionary[20003] = "avatar_03_h";
		weapon_dictionary[20004] = "avatar_04_h";
		weapon_dictionary[20005] = "avatar_05_h";
		weapon_dictionary[20006] = "avatar_06_h";
		weapon_dictionary[30001] = "avatar_01_u";
		weapon_dictionary[30002] = "avatar_02_u";
		weapon_dictionary[30003] = "avatar_03_u";
		weapon_dictionary[30004] = "avatar_04_u";
		weapon_dictionary[30005] = "avatar_05_u";
		weapon_dictionary[30006] = "avatar_06_u";
		weapon_dictionary[40001] = "avatar_01_l";
		weapon_dictionary[40002] = "avatar_02_l";
		weapon_dictionary[40003] = "avatar_03_l";
		weapon_dictionary[40004] = "avatar_04_l";
		weapon_dictionary[40005] = "avatar_05_l";
		weapon_dictionary[40006] = "avatar_06_l";
		weapon_dictionary[10001] = "Stoneskin_001";
		weapon_dictionary[10002] = "Stoneskin_002";
		weapon_dictionary[10003] = "Stoneskin_003";
		weapon_dictionary[10004] = "Stoneskin_004";
		weapon_dictionary[10005] = "Stoneskin_005";
		weapon_dictionary[10006] = "Stoneskin_006";
		weapon_dictionary[10007] = "Stoneskin_007";
		weapon_dictionary[10008] = "Stoneskin_008";
		weapon_dictionary[50001] = "guanghuan_1";
		weapon_dictionary[50002] = "guanghuan_2";
		weapon_dictionary[50003] = "guanghuan_3";
		weapon_dictionary[50004] = "guanghuan_4";
		weapon_dictionary[50005] = "guanghuan_5";
		weapon_dictionary[60001] = "shouzhuo_1";
		weapon_dictionary[60002] = "shouzhuo_2";
		weapon_dictionary[60003] = "shouzhuo_3";
		weapon_dictionary[60004] = "shouzhuo_4";
		weapon_dictionary[60005] = "shouzhuo_5";
		weapon_dictionary[70001] = "xianglian_1";
		weapon_dictionary[70002] = "xianglian_2";
		weapon_dictionary[70003] = "xianglian_3";
		weapon_dictionary[70004] = "xianglian_4";
		weapon_dictionary[70005] = "xianglian_5";
		prop_dictionary = new Dictionary<int, string>();
		prop_dictionary[1] = "Abundance";
		prop_dictionary[2] = "Fury";
		role_dictionary = new Dictionary<int, string>();
		role_dictionary[1] = "avatar1";
		role_dictionary[2] = "avatar5";
		role_dictionary[3] = "avatar4";
		role_dictionary[4] = "avatar3";
		role_dictionary[5] = "avatar2";
		role_dictionary[6] = "avatar6";
		map_dictionary = new Dictionary<int, string>();
		map_dictionary[0] = string.Empty;
		map_dictionary[1] = "p7";
		map_dictionary[2] = "p4";
		map_dictionary[3] = "p3";
		map_dictionary[4] = "p5";
		map_dictionary[5] = "p8";
		map_dictionary[6] = "p2";
		current_angle = new Vector3(354.6f, 189.9f, 0f);
		SetSwitchSceneStr(DoSwitchSceneStr);
		SetSwitchSceneInt(DoSwitchSceneInt);
		SetFireEffect(DoFireEffect);
		SetGoldToCrystalFunc(DoGoldToCrystal);
		SetDoNewHelpFunc(DoNewHelp);
		SetGetAvatarModel(GetAvatarModelFunc);
		SetGetCharacterDefaultAvatar(GetCharacterDefaultAvatarFunc);
	}

	public static TUIMappingInfo Instance()
	{
		if (instance == null)
		{
			instance = new TUIMappingInfo();
		}
		return instance;
	}

	public void SetMaterial(int nID, GoodsQualityType nQuality, string sName, TUIPriceInfo Price)
	{
		if (!m_dictMaterialInfo.ContainsKey(nID))
		{
			m_dictMaterialInfo.Add(nID, new CTUIMaterialInfo(nID, nQuality, sName, Price));
		}
		else
		{
			m_dictMaterialInfo[nID] = new CTUIMaterialInfo(nID, nQuality, sName, Price);
		}
	}

	public CTUIMaterialInfo GetMaterial(int nID)
	{
		if (!m_dictMaterialInfo.ContainsKey(nID))
		{
			return null;
		}
		return m_dictMaterialInfo[nID];
	}

	public void ClearMaterial()
	{
		m_dictMaterialInfo.Clear();
	}

	public void SetMaterialCount(int nID, int nCount)
	{
		if (!m_dictMaterialCount.ContainsKey(nID))
		{
			m_dictMaterialCount.Add(nID, nCount);
		}
		else
		{
			m_dictMaterialCount[nID] = nCount;
		}
	}

	public int GetMaterialCount(int nID)
	{
		if (!m_dictMaterialCount.ContainsKey(nID))
		{
			return 0;
		}
		return m_dictMaterialCount[nID];
	}

	public void ClearMaterialCount()
	{
		m_dictMaterialCount.Clear();
	}

	public string GetStashTexture(int id)
	{
		if (stash_dictionary != null && stash_dictionary.ContainsKey(id))
		{
			return stash_dictionary[id];
		}
		Debug.Log("error!" + id);
		return string.Empty;
	}

	public string GetSkillTexture(int id, bool m_active_skill_square = false)
	{
		if (m_active_skill_square)
		{
			if (skill_dictionary != null && skill_dictionary.ContainsKey(id + 99000))
			{
				return skill_dictionary[id + 99000];
			}
		}
		else if (skill_dictionary != null && skill_dictionary.ContainsKey(id))
		{
			return skill_dictionary[id];
		}
		Debug.Log("error! " + id + " " + m_active_skill_square);
		return string.Empty;
	}

	public string GetWeaponTexture(int id)
	{
		if (weapon_dictionary != null && weapon_dictionary.ContainsKey(id))
		{
			return weapon_dictionary[id];
		}
		return string.Empty;
	}

	public string GetPropTexture(int id)
	{
		if (prop_dictionary != null && prop_dictionary.ContainsKey(id))
		{
			return prop_dictionary[id];
		}
		Debug.Log("error!");
		return string.Empty;
	}

	public string GetRoleTexture(int id)
	{
		if (role_dictionary != null && role_dictionary.ContainsKey(id))
		{
			return role_dictionary[id];
		}
		Debug.Log("error!" + id);
		return string.Empty;
	}

	public string GetMapTexture(int id)
	{
		if (map_dictionary != null && map_dictionary.ContainsKey(id))
		{
			return map_dictionary[id];
		}
		Debug.Log("error!" + id);
		return string.Empty;
	}

	public string GetTextureByID(PopupType nType, int nID, bool bSquare = true)
	{
		switch (nType)
		{
		case PopupType.Roles:
			return GetRoleTexture(nID);
		case PopupType.Skills:
			return GetSkillTexture(nID);
		case PopupType.Skills01:
			return GetSkillTexture(nID, bSquare);
		case PopupType.Weapons01:
		case PopupType.Weapons02:
		case PopupType.Armor_Head:
		case PopupType.Armor_Body:
		case PopupType.Armor_Bracelet:
		case PopupType.Armor_Leg:
		case PopupType.Accessory_Halo:
		case PopupType.Accessory_Necklace:
		case PopupType.Accessory_Badge:
		case PopupType.Accessory_Stoneskin:
			return GetWeaponTexture(nID);
		default:
			return string.Empty;
		}
	}

	public void SetStashTexture(int m_id, string m_name)
	{
		if (stash_dictionary == null)
		{
			stash_dictionary = new Dictionary<int, string>();
		}
		stash_dictionary[m_id] = m_name;
	}

	public void SetSkillTexture(int m_id, string m_name)
	{
		if (skill_dictionary == null)
		{
			skill_dictionary = new Dictionary<int, string>();
		}
		skill_dictionary[m_id] = m_name;
	}

	public void SetWeaponTexture(int m_id, string m_name)
	{
		if (weapon_dictionary == null)
		{
			weapon_dictionary = new Dictionary<int, string>();
		}
		weapon_dictionary[m_id] = m_name;
	}

	public void SetPropTexture(int m_id, string m_name)
	{
		if (prop_dictionary == null)
		{
			prop_dictionary = new Dictionary<int, string>();
		}
		prop_dictionary[m_id] = m_name;
	}

	public void SetRoleTexture(int m_id, string m_name)
	{
		if (role_dictionary == null)
		{
			role_dictionary = new Dictionary<int, string>();
		}
		role_dictionary[m_id] = m_name;
	}

	public void SetMapTexture(int m_id, string m_name)
	{
		if (map_dictionary == null)
		{
			map_dictionary = new Dictionary<int, string>();
		}
		map_dictionary[m_id] = m_name;
	}

	public Vector3 GetCurrentAngle()
	{
		return current_angle;
	}

	public void SetCurrentAngle(Vector3 m_angle)
	{
		current_angle = m_angle;
	}

	public string GetSceneName(int m_id)
	{
		string result = string.Empty;
		switch ((TUISceneType)m_id)
		{
		case TUISceneType.Scene_Equip:
			result = "Scene_Equip";
			break;
		case TUISceneType.Scene_Forge:
			result = "Scene_Forge";
			break;
		case TUISceneType.Scene_Gold:
			result = "Scene_Gold";
			break;
		case TUISceneType.Scene_IAP:
			result = "Scene_IAP";
			break;
		case TUISceneType.Scene_Main:
			result = "Scene_Main";
			break;
		case TUISceneType.Scene_MainMenu:
			result = "Scene_MainMenu";
			break;
		case TUISceneType.Scene_Map:
			result = "Scene_Map";
			break;
		case TUISceneType.Scene_Skill:
			result = "Scene_Skill";
			break;
		case TUISceneType.Scene_Stash:
			result = "Scene_Stash";
			break;
		case TUISceneType.Scene_Tavern:
			result = "Scene_Tavern";
			break;
		case TUISceneType.Scene_CoopInputName:
			result = "Scene_CoopInputName";
			break;
		case TUISceneType.Scene_CoopMainMenu:
			result = "Scene_CoopMainMenu";
			break;
		case TUISceneType.Scene_CoopRoom:
			result = "Scene_CoopRoom";
			break;
		case TUISceneType.Scene_BlackMarket:
			result = "Scene_BlackMarket";
			break;
		}
		return result;
	}

	public void SetSwitchSceneStr(SwitchSceneStr m_function)
	{
		switch_scene_function_str = m_function;
	}

	public void SetSwitchSceneInt(SwitchSceneInt m_function)
	{
		switch_scene_function_int = m_function;
	}

	public SwitchSceneStr GetSwitchSceneStr()
	{
		return switch_scene_function_str;
	}

	public SwitchSceneInt GetSwitchSceneInt()
	{
		return switch_scene_function_int;
	}

	public void DoSwitchSceneStr(string m_next_scene)
	{
		iGameApp.GetInstance().EnterScene(m_next_scene);
	}

	public void DoSwitchSceneInt(int m_scene_id)
	{
		iGameApp.GetInstance().EnterScene((kGameSceneEnum)m_scene_id);
	}

	public void SetFireEffect(FireEffect m_function)
	{
		fire_effect_function = m_function;
	}

	public void SetGetAvatarModel(GetAvatarModel func)
	{
		m_GetAvatarModel = func;
	}

	public void SetGetCharacterDefaultAvatar(GetCharacterDefaultAvatar func)
	{
		m_GetCharacterDefaultAvatar = func;
	}

	public FireEffect GetFireEffect()
	{
		return fire_effect_function;
	}

	public Transform DoFireEffect(int m_id)
	{
		iGameData gameData = iGameApp.GetInstance().m_GameData;
		if (gameData == null)
		{
			return null;
		}
		CWeaponInfoLevel weaponInfo = gameData.GetWeaponInfo(m_id, 1);
		if (weaponInfo == null)
		{
			return null;
		}
		GameObject gameObject = PrefabManager.Get(weaponInfo.nFire);
		if (gameObject == null)
		{
			return null;
		}
		GameObject gameObject2 = Object.Instantiate(gameObject) as GameObject;
		if (gameObject2 == null)
		{
			return null;
		}
		return gameObject2.transform;
	}

	public bool GetAvatarModelFunc(int avatarid, int charid, ref GameObject modelprefab, ref Texture modeltexture)
	{
		iGameData gameData = iGameApp.GetInstance().m_GameData;
		if (gameData == null || gameData.m_AvatarCenter == null)
		{
			return false;
		}
		CAvatarInfo cAvatarInfo = gameData.m_AvatarCenter.Get(avatarid);
		if (cAvatarInfo == null)
		{
			return false;
		}
		if (cAvatarInfo.m_sModel.Length > 0)
		{
			string sPath = iMacroDefine.path_model_root + "/" + cAvatarInfo.m_sModel;
			string empty = string.Empty;
			empty = ((!cAvatarInfo.m_bLinkChar) ? (iMacroDefine.path_texture_root + "/" + cAvatarInfo.m_sTexture + "_m") : (iMacroDefine.path_texture_root + "/" + cAvatarInfo.m_sTexture + "_" + charid.ToString("d2") + "_m"));
			modelprefab = PrefabManager.Get(sPath);
			modeltexture = PrefabManager.GetObject(empty) as Texture;
		}
		if (cAvatarInfo.m_sEffect.Length > 0)
		{
			string sPath2 = iMacroDefine.path_effect_root + "/" + cAvatarInfo.m_sEffect;
			modelprefab = PrefabManager.Get(sPath2);
		}
		return true;
	}

	public bool GetCharacterDefaultAvatarFunc(int nRoleID, WeaponType nWeaponType, ref int nAvatarID)
	{
		iGameData gameData = iGameApp.GetInstance().m_GameData;
		if (gameData == null || gameData.m_CharacterCenter == null)
		{
			return false;
		}
		CCharacterInfoLevel cCharacterInfoLevel = gameData.m_CharacterCenter.Get(nRoleID, 1);
		switch (nWeaponType)
		{
		case WeaponType.Armor_Head:
			if (cCharacterInfoLevel == null)
			{
				return false;
			}
			nAvatarID = ((gameData.m_DataCenter.AvatarHead <= 0) ? cCharacterInfoLevel.nAvatarHead : gameData.m_DataCenter.AvatarHead);
			break;
		case WeaponType.Armor_Body:
			if (cCharacterInfoLevel == null)
			{
				return false;
			}
			nAvatarID = ((gameData.m_DataCenter.AvatarUpper <= 0) ? cCharacterInfoLevel.nAvatarUpper : gameData.m_DataCenter.AvatarUpper);
			break;
		case WeaponType.Armor_Leg:
			if (cCharacterInfoLevel == null)
			{
				return false;
			}
			nAvatarID = ((gameData.m_DataCenter.AvatarLower <= 0) ? cCharacterInfoLevel.nAvatarLower : gameData.m_DataCenter.AvatarLower);
			break;
		case WeaponType.Accessory_Halo:
			nAvatarID = ((gameData.m_DataCenter.AvatarHeadup <= 0) ? (-1) : gameData.m_DataCenter.AvatarHeadup);
			break;
		case WeaponType.Accessory_Necklace:
			nAvatarID = ((gameData.m_DataCenter.AvatarNeck <= 0) ? (-1) : gameData.m_DataCenter.AvatarNeck);
			break;
		case WeaponType.Armor_Bracelet:
			nAvatarID = ((gameData.m_DataCenter.AvatarWrist <= 0) ? (-1) : gameData.m_DataCenter.AvatarWrist);
			break;
		default:
			return false;
		}
		return true;
	}

	public void SetGoldToCrystalFunc(GoldToCrystal m_function)
	{
		gold_to_crystal_function = m_function;
	}

	public GoldToCrystal GetGoldToCrystalFunc()
	{
		return gold_to_crystal_function;
	}

	public int DoGoldToCrystal(int m_gold)
	{
		float num = 0.01904f;
		float p = 0.8f;
		float num2 = -3f;
		int num3 = Mathf.CeilToInt(num * Mathf.Pow(m_gold * 10, p) + num2);
		if (num3 < 1)
		{
			num3 = 1;
		}
		return num3;
	}

	public void SetDoNewHelpFunc(DoNewHelpFunc m_func)
	{
		do_new_help_function = m_func;
	}

	public DoNewHelpFunc GetDoNewHelpFunc()
	{
		return do_new_help_function;
	}

	public void DoNewHelp(NewHelpState m_state)
	{
		Debug.Log("I'm fangkuai!");
		iDataCenter iDataCenter2 = null;
		if (iGameApp.GetInstance().m_GameData != null)
		{
			iDataCenter2 = iGameApp.GetInstance().m_GameData.GetDataCenter();
		}
		switch (m_state)
		{
		case NewHelpState.Help03_ClickWeaponMake:
		case NewHelpState.Help06_ClickWeaponEquip:
		case NewHelpState.Help13_ClickSkillBuy:
		case NewHelpState.Help16_ClickSkillEquip:
		case NewHelpState.Help23_ClickGoodsSupplement:
		case NewHelpState.Help24_ClickWeaponUpgrade:
		case NewHelpState.Help25_ClickBackToVillage:
			if (iDataCenter2 != null)
			{
				iDataCenter2.nTutorialVillageState = (int)(++m_state);
				iDataCenter2.Save();
				iServerSaveData.GetInstance().UploadImmidately();
			}
			break;
		default:
			if (iDataCenter2 != null)
			{
				iDataCenter2.nTutorialVillageState = (int)(++m_state);
			}
			break;
		}
	}

	public void SetNewHelpState(NewHelpState m_state)
	{
		help_state = m_state;
	}

	public void NextNewHelpState()
	{
		if (do_new_help_function != null)
		{
			do_new_help_function(help_state);
		}
		if (help_state < NewHelpState.Help_Over && help_state > NewHelpState.None)
		{
			help_state++;
			if (help_state == NewHelpState.Help_Over)
			{
				AndroidReturnPlugin.instance.SetSkipTutorialFunc(null);
			}
		}
	}

	public NewHelpState GetNewHelpState()
	{
		return help_state;
	}

	public void SetCurrentScene(object m_scene)
	{
		current_scene = m_scene;
	}

	public object GetCurrentScene()
	{
		return current_scene;
	}
}
