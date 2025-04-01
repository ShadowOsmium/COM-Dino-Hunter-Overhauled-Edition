using System.Collections.Generic;
using TNetSdk;
using UnityEngine;

public class CGameNetSender
{
	protected static CGameNetSender m_Instance;

	protected iGameSceneBase m_GameScene
	{
		get
		{
			return iGameApp.GetInstance().m_GameScene;
		}
	}

	public static CGameNetSender GetInstance()
	{
		if (m_Instance == null)
		{
			m_Instance = new CGameNetSender();
			m_Instance.Initialize();
		}
		return m_Instance;
	}

	public void Initialize()
	{
	}

	public void SendMsg_LOBBY_OTHER_TO_PLAYER(int nId, Dictionary<int, CGameNetManager.CNetUserInfoBase> dictData)
	{
		nmsg_lobby_other_to_player nmsg_lobby_other_to_player2 = new nmsg_lobby_other_to_player();
		nmsg_lobby_other_to_player2.msghead = kGameNetEnum.LOBBY_OTHER_TO_PLAYER;
		nmsg_lobby_other_to_player2.m_dictNetUserInfoBase = dictData;
		TNetManager.GetInstance().SendMessage(nId, nmsg_lobby_other_to_player2);
	}

	public void SendMsg_LOBBY_PLAYER_TO_OTHER(CGameNetManager.CNetUserInfoBase netuserinfobase)
	{
		nmsg_lobby_player_to_other nmsg_lobby_player_to_other2 = new nmsg_lobby_player_to_other();
		nmsg_lobby_player_to_other2.msghead = kGameNetEnum.LOBBY_PLAYER_TO_OTHER;
		nmsg_lobby_player_to_other2.m_NetUserInfoBase = netuserinfobase;
		TNetManager.GetInstance().BroadcastMessage(nmsg_lobby_player_to_other2);
	}

	public void SendMsg_GAME_ENTER(int nGameLevelID, int nHunterLevelID, TNetRoom room)
	{
		nmsg_game_enter nmsg_game_enter2 = new nmsg_game_enter();
		nmsg_game_enter2.msghead = kGameNetEnum.GAME_ENTER;
		nmsg_game_enter2.m_nGameLevelID = nGameLevelID;
		nmsg_game_enter2.m_nHunterLevelID = nHunterLevelID;
		GameLevelInfo gameLevelInfo = iGameApp.GetInstance().m_GameData.GetGameLevelInfo(nGameLevelID);
		if (gameLevelInfo == null)
		{
			return;
		}
		nmsg_game_enter2.ltPlayerPos = new List<nmsg_game_enter.player_pos>();
		CStartPointManager cStartPointManager = new CStartPointManager();
		if (cStartPointManager != null)
		{
			cStartPointManager.Load("_Config/_StartPoint/StartPoint_" + gameLevelInfo.nBirthPos);
		}
		for (int i = 0; i < room.UserCount; i++)
		{
			nmsg_game_enter.player_pos player_pos = new nmsg_game_enter.player_pos();
			player_pos.m_nId = room.UserList[i].Id;
			Debug.Log(player_pos.m_nId);
			player_pos.m_nUID = MyUtils.GetUID();
			player_pos.m_v3Pos = new Vector3(0f, gameLevelInfo.fNavPlane, 0f);
			if (cStartPointManager != null)
			{
				CStartPoint random = cStartPointManager.GetRandom();
				if (random != null)
				{
					player_pos.m_v3Pos = random.GetRandom2D();
				}
			}
			player_pos.m_v3Dir = -player_pos.m_v3Pos.normalized;
			nmsg_game_enter2.ltPlayerPos.Add(player_pos);
		}
		TNetManager.GetInstance().BroadcastMessage(nmsg_game_enter2);
		TNetManager.GetInstance().StartRoom();
	}

	public void SendMsg_Game_START_REQUEST()
	{
		nmsg_game_start_request nmsg_game_start_request2 = new nmsg_game_start_request();
		nmsg_game_start_request2.msghead = kGameNetEnum.GAME_START_REQUEST;
		TNetManager.GetInstance().BroadcastMessage(nmsg_game_start_request2);
	}

	public void SendMsg_GAME_START()
	{
		nmsg_game_start nmsg_game_start2 = new nmsg_game_start();
		nmsg_game_start2.msghead = kGameNetEnum.GAME_START;
		TNetManager.GetInstance().BroadcastMessage(nmsg_game_start2);
	}

	public void SendMsg_GAME_OVER(bool bWin)
	{
		nmsg_game_over nmsg_game_over2 = new nmsg_game_over();
		nmsg_game_over2.msghead = kGameNetEnum.GAME_OVER;
		nmsg_game_over2.m_bWin = bWin;
		TNetManager.GetInstance().BroadcastMessage(nmsg_game_over2);
	}

	public void SendMsg_PLAYER_DEAD()
	{
		nmsg_player_dead nmsg_player_dead2 = new nmsg_player_dead();
		nmsg_player_dead2.msghead = kGameNetEnum.PLAYER_DEAD;
		TNetManager.GetInstance().BroadcastMessage(nmsg_player_dead2);
	}

	public void SendMsg_PLAYER_REVIVE_REQUEST()
	{
		nmsg_player_revive_request nmsg_player_revive_request2 = new nmsg_player_revive_request();
		nmsg_player_revive_request2.msghead = kGameNetEnum.PLAYER_REVIVE_REQUEST;
		TNetManager.GetInstance().BroadcastMessage(nmsg_player_revive_request2);
	}

	public void SendMsg_PLAYER_REVIVE(int nId)
	{
		nmsg_player_revive nmsg_player_revive2 = new nmsg_player_revive();
		nmsg_player_revive2.msghead = kGameNetEnum.PLAYER_REVIVE;
		nmsg_player_revive2.m_nPlayerId = nId;
		TNetManager.GetInstance().BroadcastMessage(nmsg_player_revive2);
	}

	public void SendMsg_PLAYER_MOVE(Vector3 v3Src, Vector3 v3Dst)
	{
		nmsg_player_move nmsg_player_move2 = new nmsg_player_move();
		nmsg_player_move2.msghead = kGameNetEnum.PLAYER_MOVE;
		nmsg_player_move2.m_ltPath.Add(v3Dst);
		nmsg_player_move2.m_fTime = 0.2f;
		TNetManager.GetInstance().BroadcastMessage(nmsg_player_move2);
	}

	public void SendMsg_PLAYER_MOVESTOP(Vector3 v3Dst)
	{
		nmsg_player_stopmove nmsg_player_stopmove2 = new nmsg_player_stopmove();
		nmsg_player_stopmove2.msghead = kGameNetEnum.PLAYER_MOVESTOP;
		nmsg_player_stopmove2.m_v3StopPoint = v3Dst;
		TNetManager.GetInstance().BroadcastMessage(nmsg_player_stopmove2);
	}

	public void SendMsg_PLAYER_AIM(Vector3 v3AimPoint)
	{
		nmsg_player_aim nmsg_player_aim2 = new nmsg_player_aim();
		nmsg_player_aim2.msghead = kGameNetEnum.PLAYER_AIM;
		nmsg_player_aim2.m_v3AimPoint = v3AimPoint;
		TNetManager.GetInstance().BroadcastMessage(nmsg_player_aim2);
	}

	public void SendMsg_PLAYER_SHOOT(bool bShoot)
	{
		nmsg_player_shoot nmsg_player_shoot2 = new nmsg_player_shoot();
		nmsg_player_shoot2.msghead = kGameNetEnum.PLAYER_SHOOT;
		nmsg_player_shoot2.m_bShoot = bShoot;
		TNetManager.GetInstance().BroadcastMessage(nmsg_player_shoot2);
	}

	public void SendMsg_PLAYER_SWITCHWEAPON(int nWeaponID, int nWeaponLevel)
	{
		nmsg_player_switchweapon nmsg_player_switchweapon2 = new nmsg_player_switchweapon();
		nmsg_player_switchweapon2.msghead = kGameNetEnum.PLAYER_SWITCHWEAPON;
		nmsg_player_switchweapon2.m_nWeaponID = nWeaponID;
		nmsg_player_switchweapon2.m_nWeaponLvl = nWeaponLevel;
		TNetManager.GetInstance().BroadcastMessage(nmsg_player_switchweapon2);
	}

	public void SendMsg_PLAYER_USESKILL(int nSkillID, int nSkillLevel)
	{
		nmsg_player_useskill nmsg_player_useskill2 = new nmsg_player_useskill();
		nmsg_player_useskill2.msghead = kGameNetEnum.PLAYER_SKILL;
		nmsg_player_useskill2.m_nSkillID = nSkillID;
		nmsg_player_useskill2.m_nSkillLvl = nSkillLevel;
		TNetManager.GetInstance().BroadcastMessage(nmsg_player_useskill2);
	}

	public void SendMsg_PLAYER_BEATBACK(Vector3 point)
	{
		nmsg_player_beatback nmsg_player_beatback2 = new nmsg_player_beatback();
		nmsg_player_beatback2.msghead = kGameNetEnum.PLAYER_BEATBACK;
		nmsg_player_beatback2.m_v3BeatBackPoint = point;
		TNetManager.GetInstance().BroadcastMessage(nmsg_player_beatback2);
	}

	public void SendMsg_PLAYER_LEVELUP(int nLevel)
	{
		nmsg_player_levelup nmsg_player_levelup2 = new nmsg_player_levelup();
		nmsg_player_levelup2.msghead = kGameNetEnum.PLAYER_LEVELUP;
		nmsg_player_levelup2.m_nLevel = nLevel;
		TNetManager.GetInstance().BroadcastMessage(nmsg_player_levelup2);
	}

	public void SendMsg_MOB_DEAD(int nMobUID)
	{
		nmsg_mob_dead nmsg_mob_dead2 = new nmsg_mob_dead();
		nmsg_mob_dead2.msghead = kGameNetEnum.MOB_DEAD;
		nmsg_mob_dead2.m_nMobUID = nMobUID;
		TNetManager.GetInstance().BroadcastMessage(nmsg_mob_dead2);
	}

	public void SendMsg_MOB_MOVE(int nMobUID, Vector3 v3Dst)
	{
		nmsg_mob_move nmsg_mob_move2 = new nmsg_mob_move();
		nmsg_mob_move2.msghead = kGameNetEnum.MOB_MOVE;
		nmsg_mob_move2.m_nMobUID = nMobUID;
		nmsg_mob_move2.m_v3Dst = v3Dst;
		TNetManager.GetInstance().BroadcastMessage(nmsg_mob_move2);
	}

	public void SendMsg_MOB_HOVER(int nMobUID, Vector3 v3HoverPoint)
	{
		nmsg_mob_hover nmsg_mob_hover2 = new nmsg_mob_hover();
		nmsg_mob_hover2.msghead = kGameNetEnum.MOB_HOVER;
		nmsg_mob_hover2.m_nMobUID = nMobUID;
		nmsg_mob_hover2.m_v3HoverPoint = v3HoverPoint;
		TNetManager.GetInstance().BroadcastMessage(nmsg_mob_hover2);
	}

	public void SendMsg_MOB_SKILL(int nMobUID, int nTargetUID, int nComboSkillID, int nComboSkillIndex)
	{
		nmsg_mob_skill nmsg_mob_skill2 = new nmsg_mob_skill();
		nmsg_mob_skill2.msghead = kGameNetEnum.MOB_SKILL;
		nmsg_mob_skill2.m_nMobUID = nMobUID;
		nmsg_mob_skill2.m_nTargetUID = nTargetUID;
		nmsg_mob_skill2.m_nComboSkillID = nComboSkillID;
		nmsg_mob_skill2.m_nComboSkillIndex = nComboSkillIndex;
		TNetManager.GetInstance().BroadcastMessage(nmsg_mob_skill2);
	}

	public void SendMsg_MOB_ACTION(int nMobUID, int nAction)
	{
		nmsg_mob_action nmsg_mob_action2 = new nmsg_mob_action();
		nmsg_mob_action2.msghead = kGameNetEnum.MOB_ACTION;
		nmsg_mob_action2.m_nMobUID = nMobUID;
		nmsg_mob_action2.m_nAction = nAction;
		TNetManager.GetInstance().BroadcastMessage(nmsg_mob_action2);
	}

	public void SendMsg_MOB_BEATBACK(int uid, Vector3 point)
	{
		nmsg_mob_beatback nmsg_mob_beatback2 = new nmsg_mob_beatback();
		nmsg_mob_beatback2.msghead = kGameNetEnum.MOB_BEATBACK;
		nmsg_mob_beatback2.m_nMobUID = uid;
		nmsg_mob_beatback2.m_v3BeatBackPoint = point;
		TNetManager.GetInstance().BroadcastMessage(nmsg_mob_beatback2);
	}

	public void SendMsg_BATTLE_DAMAGE_MOB(int nMobUID, float fDamage, bool bBlack = false)
	{
		nmsg_battle_damage_mob nmsg_battle_damage_mob2 = new nmsg_battle_damage_mob();
		nmsg_battle_damage_mob2.msghead = kGameNetEnum.BATTLE_DAMAGE_MOB;
		nmsg_battle_damage_mob2.m_nMobUID = nMobUID;
		nmsg_battle_damage_mob2.m_bBlack = bBlack;
		nmsg_battle_damage_mob2.m_fDamage = fDamage;
		TNetManager.GetInstance().BroadcastMessage(nmsg_battle_damage_mob2);
	}

	public void SendMsg_BATTLE_DAMAGE_PLAYER(int nPlayerUID, float fHP, float fMaxHP)
	{
		nmsg_battle_damage_player nmsg_battle_damage_player2 = new nmsg_battle_damage_player();
		nmsg_battle_damage_player2.msghead = kGameNetEnum.BATTLE_DAMAGE_PLAYER;
		nmsg_battle_damage_player2.m_nPlayerUID = nPlayerUID;
		nmsg_battle_damage_player2.m_fCurHP = fHP;
		nmsg_battle_damage_player2.m_fMaxHP = fMaxHP;
		TNetManager.GetInstance().BroadcastMessage(nmsg_battle_damage_player2);
	}

	public void SendMsg_MGMANAGER_ADDMOB(int nMobID, int nMobLvl, int nMobUID, int nWaveID, int nSeq, Vector3 v3Pos, Vector3 v3Dir)
	{
		nmsg_mgmanager_addmob nmsg_mgmanager_addmob2 = new nmsg_mgmanager_addmob();
		nmsg_mgmanager_addmob2.msghead = kGameNetEnum.MGMANAGER_ADDMOB;
		nmsg_mgmanager_addmob2.m_nMobID = nMobID;
		nmsg_mgmanager_addmob2.m_nMobLvl = nMobLvl;
		nmsg_mgmanager_addmob2.m_nMobUID = nMobUID;
		nmsg_mgmanager_addmob2.m_nWaveID = nWaveID;
		nmsg_mgmanager_addmob2.m_nSequence = nSeq;
		nmsg_mgmanager_addmob2.m_v3Pos = v3Pos;
		nmsg_mgmanager_addmob2.m_v3Dir = v3Dir;
		TNetManager.GetInstance().BroadcastMessage(nmsg_mgmanager_addmob2);
	}

	public void SendMsg_MGMANAGER_ADDMOB_SPECIAL(int nMobID, int nMobLvl, int nMobUID, Vector3 v3Pos, Vector3 v3Dir)
	{
		nmsg_mgmanager_addmob_special nmsg_mgmanager_addmob_special2 = new nmsg_mgmanager_addmob_special();
		nmsg_mgmanager_addmob_special2.msghead = kGameNetEnum.MGMANAGER_ADDMOB_SPECIAL;
		nmsg_mgmanager_addmob_special2.m_nMobID = nMobID;
		nmsg_mgmanager_addmob_special2.m_nMobLvl = nMobLvl;
		nmsg_mgmanager_addmob_special2.m_nMobUID = nMobUID;
		nmsg_mgmanager_addmob_special2.m_v3Pos = v3Pos;
		nmsg_mgmanager_addmob_special2.m_v3Dir = v3Dir;
		TNetManager.GetInstance().BroadcastMessage(nmsg_mgmanager_addmob_special2);
	}

	public void SendMsg_BATTLE_RESULT_PLAYERREWARDS(int nTitle, int nCombatRatings, int nGainCrystalInGame, int nGainGoldInGame, int nBeadmireCount, int nCharLvlResult, int nCharExpResult, int nHunterLvlResult, int nHunterExpResult)
	{
		nmsg_battle_result_playerrewards nmsg_battle_result_playerrewards2 = new nmsg_battle_result_playerrewards();
		nmsg_battle_result_playerrewards2.msghead = kGameNetEnum.BATTLE_RESULT_PLAYERREWARDS;
		nmsg_battle_result_playerrewards2.m_nTitle = nTitle;
		nmsg_battle_result_playerrewards2.m_nCombatRatings = nCombatRatings;
		nmsg_battle_result_playerrewards2.m_nGainCrystalInGame = nGainCrystalInGame;
		nmsg_battle_result_playerrewards2.m_nGainGoldInGame = nGainGoldInGame;
		nmsg_battle_result_playerrewards2.m_nBeadmireCount = nBeadmireCount;
		nmsg_battle_result_playerrewards2.m_nCharLvlResult = nCharLvlResult;
		nmsg_battle_result_playerrewards2.m_nCharExpResult = nCharExpResult;
		nmsg_battle_result_playerrewards2.m_nHunterLvlResult = nHunterLvlResult;
		nmsg_battle_result_playerrewards2.m_nHunterExpResult = nHunterExpResult;
		TNetManager.GetInstance().BroadcastMessage(nmsg_battle_result_playerrewards2);
	}

	public void SendMsg_BATTLE_RESULT_ADMIRE(int nPlayerId)
	{
		nmsg_battle_result_admire nmsg_battle_result_admire2 = new nmsg_battle_result_admire();
		nmsg_battle_result_admire2.msghead = kGameNetEnum.BATTLE_RESULT_ADMIRE;
		nmsg_battle_result_admire2.m_nId = nPlayerId;
		TNetManager.GetInstance().BroadcastMessage(nmsg_battle_result_admire2);
	}

	public void SendMsg_SOCIAL_ADDFRIEND(string sDeviceId, int nid = -1)
	{
		nmsg_social_addfriend nmsg_social_addfriend2 = new nmsg_social_addfriend();
		nmsg_social_addfriend2.msghead = kGameNetEnum.SOCIAL_ADDFRIEND;
		nmsg_social_addfriend2.m_sId = sDeviceId;
		if (nid == -1)
		{
			TNetManager.GetInstance().BroadcastMessage(nmsg_social_addfriend2);
		}
		else
		{
			TNetManager.GetInstance().SendMessage(nid, nmsg_social_addfriend2);
		}
	}
}
