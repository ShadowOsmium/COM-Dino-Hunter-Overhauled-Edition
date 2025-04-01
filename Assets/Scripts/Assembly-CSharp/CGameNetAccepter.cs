using System.Collections.Generic;
using TNetSdk;
using UnityEngine;

public class CGameNetAccepter
{
	protected delegate void OnMsgFunc(TNetUser sender, SFSObject data);

	protected static CGameNetAccepter m_Instance;

	protected Dictionary<kGameNetEnum, OnMsgFunc> m_dictMsgCallBack;

	protected iGameSceneBase m_GameScene
	{
		get
		{
			return iGameApp.GetInstance().m_GameScene;
		}
	}

	protected iGameData m_GameData
	{
		get
		{
			return iGameApp.GetInstance().m_GameData;
		}
	}

	protected iGameState m_GameState
	{
		get
		{
			return iGameApp.GetInstance().m_GameState;
		}
	}

	protected iGameUIBase m_GameUI
	{
		get
		{
			if (m_GameScene == null)
			{
				return null;
			}
			return m_GameScene.GetGameUI();
		}
	}

	protected iDataCenter m_DataCenter
	{
		get
		{
			if (m_GameData == null)
			{
				return null;
			}
			return m_GameData.GetDataCenter();
		}
	}

	public static CGameNetAccepter GetInstance()
	{
		if (m_Instance == null)
		{
			m_Instance = new CGameNetAccepter();
		}
		return m_Instance;
	}

	public void Initialize()
	{
	}

	public void Destroy()
	{
		m_dictMsgCallBack.Clear();
	}

	public void OnCustomMsg(TNetUser sender, kGameNetEnum nmsg, SFSObject data)
	{
		if (sender != null && m_dictMsgCallBack != null && m_dictMsgCallBack.ContainsKey(nmsg))
		{
			m_dictMsgCallBack[nmsg](sender, data);
		}
	}

	public void Register()
	{
		if (m_dictMsgCallBack == null)
		{
			m_dictMsgCallBack = new Dictionary<kGameNetEnum, OnMsgFunc>();
		}
		m_dictMsgCallBack.Clear();
		m_dictMsgCallBack.Add(kGameNetEnum.LOBBY_ENTER, OnMsg_LOBBY_ENTER);
		m_dictMsgCallBack.Add(kGameNetEnum.LOBBY_LEAVE, OnMsg_LOBBY_LEAVE);
		m_dictMsgCallBack.Add(kGameNetEnum.LOBBY_ENTER_PLAYER, OnMsg_LOBBY_ENTER_PLAYER);
		m_dictMsgCallBack.Add(kGameNetEnum.LOBBY_LEAVE_PLAYER, OnMsg_LOBBY_LEAVE_PLAYER);
		m_dictMsgCallBack.Add(kGameNetEnum.LOBBY_START, null);
		m_dictMsgCallBack.Add(kGameNetEnum.LOBBY_OTHER_TO_PLAYER, OnMsg_LOBBY_OTHER_TO_PLAYER);
		m_dictMsgCallBack.Add(kGameNetEnum.LOBBY_PLAYER_TO_OTHER, OnMsg_LOBBY_PLAYER_TO_OTHER);
		m_dictMsgCallBack.Add(kGameNetEnum.GAME_ENTER, OnMsg_GAME_ENTER);
		m_dictMsgCallBack.Add(kGameNetEnum.GAME_START_REQUEST, OnMsg_GAME_START_REQUEST);
		m_dictMsgCallBack.Add(kGameNetEnum.GAME_START, OnMsg_GAME_START);
		m_dictMsgCallBack.Add(kGameNetEnum.GAME_OVER, OnMsg_GAME_OVER);
		m_dictMsgCallBack.Add(kGameNetEnum.PLAYER_DEAD, OnMsg_PLAYER_DEAD);
		m_dictMsgCallBack.Add(kGameNetEnum.PLAYER_REVIVE_REQUEST, OnMsg_PLAYER_REVIVE_REQUEST);
		m_dictMsgCallBack.Add(kGameNetEnum.PLAYER_REVIVE, OnMsg_PLAYER_REVIVE);
		m_dictMsgCallBack.Add(kGameNetEnum.PLAYER_MOVE, OnMsg_PLAYER_MOVE);
		m_dictMsgCallBack.Add(kGameNetEnum.PLAYER_MOVESTOP, OnMsg_PLAYER_MOVESTOP);
		m_dictMsgCallBack.Add(kGameNetEnum.PLAYER_AIM, OnMsg_PLAYER_AIM);
		m_dictMsgCallBack.Add(kGameNetEnum.PLAYER_SHOOT, OnMsg_PLAYER_SHOOT);
		m_dictMsgCallBack.Add(kGameNetEnum.PLAYER_SWITCHWEAPON, OnMsg_PLAYER_SWITCHWEAPON);
		m_dictMsgCallBack.Add(kGameNetEnum.PLAYER_SKILL, OnMsg_PLAYER_USESKILL);
		m_dictMsgCallBack.Add(kGameNetEnum.PLAYER_LEVELUP, OnMsg_PLAYER_LEVELUP);
		m_dictMsgCallBack.Add(kGameNetEnum.PLAYER_BEATBACK, OnMsg_PLAYER_BEATBACK);
		m_dictMsgCallBack.Add(kGameNetEnum.MOB_DEAD, OnMsg_MOB_DEAD);
		m_dictMsgCallBack.Add(kGameNetEnum.MOB_MOVE, OnMsg_MOB_MOVE);
		m_dictMsgCallBack.Add(kGameNetEnum.MOB_SKILL, OnMsg_MOB_SKILL);
		m_dictMsgCallBack.Add(kGameNetEnum.MOB_HOVER, OnMsg_MOB_HOVER);
		m_dictMsgCallBack.Add(kGameNetEnum.MOB_BEATBACK, OnMsg_MOB_BEATBACK);
		m_dictMsgCallBack.Add(kGameNetEnum.MGMANAGER_ADDMOB, OnMsg_MGMANAGER_ADDMOB);
		m_dictMsgCallBack.Add(kGameNetEnum.MGMANAGER_ADDMOB_SPECIAL, OnMsg_MGMANAGER_ADDMOB_SPECIAL);
		m_dictMsgCallBack.Add(kGameNetEnum.BATTLE_DAMAGE_MOB, OnMsg_BATTLE_DAMAGE_MOB);
		m_dictMsgCallBack.Add(kGameNetEnum.BATTLE_DAMAGE_PLAYER, OnMsg_BATTLE_DAMAGE_PLAYER);
		m_dictMsgCallBack.Add(kGameNetEnum.BATTLE_RESULT_PLAYERREWARDS, OnMsg_BATTLE_RESULT_PLAYERREWARDS);
		m_dictMsgCallBack.Add(kGameNetEnum.BATTLE_RESULT_ADMIRE, OnMsg_BATTLE_RESULT_ADMIRE);
		m_dictMsgCallBack.Add(kGameNetEnum.SOCIAL_ADDFRIEND, OnMsg_SOCIAL_ADDFRIEND);
	}

	protected void OnMsg_LOBBY_ENTER(TNetUser sender, SFSObject data)
	{
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null || dataCenter.GetCharacter(dataCenter.CurCharID) == null)
		{
			TNetManager.GetInstance().LeaveRoom();
			return;
		}
		CGameNetManager.CNetUserInfo cNetUserInfo = new CGameNetManager.CNetUserInfo();
		cNetUserInfo.m_sDeivceId = iServerSaveData.GetInstance().CurDeviceId;
		cNetUserInfo.m_sName = dataCenter.NickName;
		cNetUserInfo.m_nHunterLvl = dataCenter.HunterLvl;
		cNetUserInfo.m_nHunterExp = dataCenter.HunterExp;
		cNetUserInfo.m_nCharID = dataCenter.CurCharID;
		cNetUserInfo.m_nCharLvl = dataCenter.GetCharacter(dataCenter.CurCharID).nLevel;
		cNetUserInfo.m_nCharExp = dataCenter.GetCharacter(dataCenter.CurCharID).nExp;
		cNetUserInfo.m_nWeaponID = dataCenter.GetSelectWeapon(1);
		dataCenter.GetWeaponLevel(cNetUserInfo.m_nWeaponID, ref cNetUserInfo.m_nWeaponLvl);
		cNetUserInfo.m_nTitle = dataCenter.Title;
		cNetUserInfo.m_sSignature = dataCenter.Signature;
		cNetUserInfo.m_nCombatPower = dataCenter.CombatPower;
		for (int i = 0; i < cNetUserInfo.m_arrWeapon.Length; i++)
		{
			cNetUserInfo.m_arrWeapon[i] = dataCenter.GetSelectWeapon(i);
		}
		cNetUserInfo.m_nAvatarHead = dataCenter.AvatarHead;
		cNetUserInfo.m_nAvatarUpper = dataCenter.AvatarUpper;
		cNetUserInfo.m_nAvatarLower = dataCenter.AvatarLower;
		cNetUserInfo.m_nAvatarHeadup = dataCenter.AvatarHeadup;
		cNetUserInfo.m_nAvatarNeck = dataCenter.AvatarNeck;
		cNetUserInfo.m_nAvatarBracelet = dataCenter.AvatarWrist;
		cNetUserInfo.m_nAvatarBadge = dataCenter.AvatarBadge;
		cNetUserInfo.m_nAvatarStone = dataCenter.AvatarStone;
		CGameNetManager.GetInstance().AddNetUserInfo(sender.Id, cNetUserInfo);
		CGameNetSender.GetInstance().SendMsg_LOBBY_PLAYER_TO_OTHER(cNetUserInfo);
		iGameApp.GetInstance().EnterScene("Scene_CoopRoom");
	}

	protected void OnMsg_LOBBY_LEAVE(TNetUser sender, SFSObject data)
	{
		CGameNetManager.GetInstance().ClearNetUserInfo();
		iGameApp.GetInstance().EnterScene(kGameSceneEnum.MutipyHome);
	}

	protected void OnMsg_LOBBY_ENTER_PLAYER(TNetUser sender, SFSObject data)
	{
		if (!CGameNetManager.GetInstance().IsRoomMaster())
		{
			return;
		}
		Dictionary<int, CGameNetManager.CNetUserInfoBase> dictionary = new Dictionary<int, CGameNetManager.CNetUserInfoBase>();
		Dictionary<int, CGameNetManager.CNetUserInfo> netUserInfoData = CGameNetManager.GetInstance().GetNetUserInfoData();
		foreach (KeyValuePair<int, CGameNetManager.CNetUserInfo> item in netUserInfoData)
		{
			dictionary.Add(item.Key, item.Value);
		}
		CGameNetSender.GetInstance().SendMsg_LOBBY_OTHER_TO_PLAYER(sender.Id, dictionary);
	}

	protected void OnMsg_LOBBY_LEAVE_PLAYER(TNetUser sender, SFSObject data)
	{
		CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo(sender.Id);
		if (netUserInfo == null)
		{
			return;
		}
		Debug.Log(sender.Id + " leave room");
		if (m_GameScene == null)
		{
			CGameNetManager.GetInstance().DelNetUserInfo(netUserInfo.m_nId);
			TUIDataServer.Instance().PlayerLeaveRoom(netUserInfo.m_sDeivceId);
			return;
		}
		netUserInfo.m_bLeaved = true;
		if (!sender.IsItMe)
		{
			Debug.Log("uid = " + netUserInfo.m_nUID);
			m_GameScene.RemovePlayer(netUserInfo.m_nUID);
		}
	}

	protected void OnMsg_LOBBY_OTHER_TO_PLAYER(TNetUser sender, SFSObject data)
	{
		if (sender.IsItMe)
		{
			return;
		}
		nmsg_lobby_other_to_player nmsg_lobby_other_to_player2 = new nmsg_lobby_other_to_player();
		nmsg_lobby_other_to_player2.UnPack(data);
		foreach (KeyValuePair<int, CGameNetManager.CNetUserInfoBase> item in nmsg_lobby_other_to_player2.m_dictNetUserInfoBase)
		{
			int key = item.Key;
			CGameNetManager.CNetUserInfoBase value = item.Value;
			CGameNetManager.CNetUserInfo cNetUserInfo = CGameNetManager.GetInstance().GetNetUserInfo(key);
			if (cNetUserInfo == null)
			{
				cNetUserInfo = new CGameNetManager.CNetUserInfo();
				CGameNetManager.GetInstance().AddNetUserInfo(key, cNetUserInfo);
			}
			cNetUserInfo.m_sDeivceId = value.m_sDeivceId;
			cNetUserInfo.m_sName = value.m_sName;
			cNetUserInfo.m_nHunterLvl = value.m_nHunterLvl;
			cNetUserInfo.m_nHunterExp = value.m_nHunterExp;
			cNetUserInfo.m_nCharID = value.m_nCharID;
			cNetUserInfo.m_nCharLvl = value.m_nCharLvl;
			cNetUserInfo.m_nCharExp = value.m_nCharExp;
			cNetUserInfo.m_nWeaponID = value.m_nWeaponID;
			cNetUserInfo.m_nWeaponLvl = value.m_nWeaponLvl;
			cNetUserInfo.m_nTitle = value.m_nTitle;
			cNetUserInfo.m_nCombatPower = value.m_nCombatPower;
			cNetUserInfo.m_sSignature = value.m_sSignature;
			cNetUserInfo.m_arrWeapon = value.m_arrWeapon;
			cNetUserInfo.m_nAvatarHead = value.m_nAvatarHead;
			cNetUserInfo.m_nAvatarUpper = value.m_nAvatarUpper;
			cNetUserInfo.m_nAvatarLower = value.m_nAvatarLower;
			cNetUserInfo.m_nAvatarHeadup = value.m_nAvatarHeadup;
			cNetUserInfo.m_nAvatarNeck = value.m_nAvatarNeck;
			cNetUserInfo.m_nAvatarBracelet = value.m_nAvatarBracelet;
			cNetUserInfo.m_nAvatarBadge = value.m_nAvatarBadge;
			cNetUserInfo.m_nAvatarStone = value.m_nAvatarStone;
			TUIDataServer.Instance().PlayerEnterRoom(false, cNetUserInfo.m_sDeivceId, cNetUserInfo.m_sName, cNetUserInfo.m_nTitle, cNetUserInfo.m_nHunterLvl, cNetUserInfo.m_nCharID, cNetUserInfo.m_nCharLvl, cNetUserInfo.m_nCombatPower, cNetUserInfo.m_sSignature, cNetUserInfo.m_arrWeapon, cNetUserInfo.m_nAvatarHead, cNetUserInfo.m_nAvatarUpper, cNetUserInfo.m_nAvatarLower, cNetUserInfo.m_nAvatarHeadup, cNetUserInfo.m_nAvatarNeck, cNetUserInfo.m_nAvatarBracelet);
		}
	}

	protected void OnMsg_LOBBY_PLAYER_TO_OTHER(TNetUser sender, SFSObject data)
	{
		if (!sender.IsItMe)
		{
			nmsg_lobby_player_to_other nmsg_lobby_player_to_other2 = new nmsg_lobby_player_to_other();
			nmsg_lobby_player_to_other2.UnPack(data);
			CGameNetManager.CNetUserInfo cNetUserInfo = CGameNetManager.GetInstance().GetNetUserInfo(sender.Id);
			if (cNetUserInfo == null)
			{
				cNetUserInfo = new CGameNetManager.CNetUserInfo();
				CGameNetManager.GetInstance().AddNetUserInfo(sender.Id, cNetUserInfo);
			}
			cNetUserInfo.m_sDeivceId = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_sDeivceId;
			cNetUserInfo.m_sName = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_sName;
			cNetUserInfo.m_nHunterLvl = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_nHunterLvl;
			cNetUserInfo.m_nHunterExp = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_nHunterExp;
			cNetUserInfo.m_nCharID = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_nCharID;
			cNetUserInfo.m_nCharLvl = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_nCharLvl;
			cNetUserInfo.m_nCharExp = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_nCharExp;
			cNetUserInfo.m_nWeaponID = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_nWeaponID;
			cNetUserInfo.m_nWeaponLvl = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_nWeaponLvl;
			cNetUserInfo.m_nTitle = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_nTitle;
			cNetUserInfo.m_nCombatPower = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_nCombatPower;
			cNetUserInfo.m_sSignature = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_sSignature;
			cNetUserInfo.m_arrWeapon = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_arrWeapon;
			cNetUserInfo.m_nAvatarHead = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_nAvatarHead;
			cNetUserInfo.m_nAvatarUpper = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_nAvatarUpper;
			cNetUserInfo.m_nAvatarLower = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_nAvatarLower;
			cNetUserInfo.m_nAvatarHeadup = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_nAvatarHeadup;
			cNetUserInfo.m_nAvatarNeck = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_nAvatarNeck;
			cNetUserInfo.m_nAvatarBracelet = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_nAvatarBracelet;
			cNetUserInfo.m_nAvatarBadge = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_nAvatarBadge;
			cNetUserInfo.m_nAvatarStone = nmsg_lobby_player_to_other2.m_NetUserInfoBase.m_nAvatarStone;
			TUIDataServer.Instance().PlayerEnterRoom(false, cNetUserInfo.m_sDeivceId, cNetUserInfo.m_sName, cNetUserInfo.m_nTitle, cNetUserInfo.m_nHunterLvl, cNetUserInfo.m_nCharID, cNetUserInfo.m_nCharLvl, cNetUserInfo.m_nCombatPower, cNetUserInfo.m_sSignature, cNetUserInfo.m_arrWeapon, cNetUserInfo.m_nAvatarHead, cNetUserInfo.m_nAvatarUpper, cNetUserInfo.m_nAvatarLower, cNetUserInfo.m_nAvatarHeadup, cNetUserInfo.m_nAvatarNeck, cNetUserInfo.m_nAvatarBracelet);
		}
	}

	protected void OnMsg_GAME_ENTER(TNetUser sender, SFSObject data)
	{
		nmsg_game_enter nmsg_game_enter2 = new nmsg_game_enter();
		nmsg_game_enter2.UnPack(data);
		foreach (nmsg_game_enter.player_pos ltPlayerPo in nmsg_game_enter2.ltPlayerPos)
		{
			CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo(ltPlayerPo.m_nId);
			if (netUserInfo != null)
			{
				netUserInfo.m_nUID = ltPlayerPo.m_nUID;
				netUserInfo.m_v3Pos = ltPlayerPo.m_v3Pos;
				netUserInfo.m_v3Dir = ltPlayerPo.m_v3Dir;
				if (!sender.IsItMe && ltPlayerPo.m_nUID > MyUtils.UIDCount)
				{
					MyUtils.UIDCount = ltPlayerPo.m_nUID;
				}
			}
		}
		Debug.Log(nmsg_game_enter2.m_nGameLevelID);
		m_GameState.GameLevel = nmsg_game_enter2.m_nGameLevelID;
		m_GameState.m_nCurHunterLevelID = nmsg_game_enter2.m_nHunterLevelID;
		iGameApp.GetInstance().EnterScene(kGameSceneEnum.Game);
	}

	protected void OnMsg_GAME_START_REQUEST(TNetUser sender, SFSObject data)
	{
		CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo(sender.Id);
		if (netUserInfo != null)
		{
			netUserInfo.m_bLoaded = true;
		}
	}

	protected void OnMsg_GAME_START(TNetUser sender, SFSObject data)
	{
		CGameNetManager.GetInstance().IsGaming = true;
	}

	protected void OnMsg_GAME_OVER(TNetUser sender, SFSObject data)
	{
		nmsg_game_over nmsg_game_over2 = new nmsg_game_over();
		nmsg_game_over2.UnPack(data);
		if (!sender.IsItMe && m_GameScene != null)
		{
			if (m_GameScene.isWaitingRevive)
			{
				m_GameScene.FinishRevive(false);
			}
			m_GameScene.FinishGame(nmsg_game_over2.m_bWin);
		}
	}

	protected void OnMsg_PLAYER_DEAD(TNetUser sender, SFSObject data)
	{
		if (m_GameScene == null || sender.IsItMe)
		{
			return;
		}
		CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo(sender.Id);
		if (netUserInfo == null)
		{
			return;
		}
		CCharPlayer player = m_GameScene.GetPlayer(netUserInfo.m_nUID);
		if (!(player == null))
		{
			nmsg_player_dead nmsg_player_dead2 = new nmsg_player_dead();
			nmsg_player_dead2.UnPack(data);
			player.AddHP(0f - player.CurHP);
			player.isDead = true;
			player.ResetAI();
			iGameUIBase gameUI = m_GameScene.GetGameUI();
			if (gameUI != null)
			{
				gameUI.SetProtraitDeathFlag(true, player.UID);
			}
		}
	}

	protected void OnMsg_PLAYER_REVIVE_REQUEST(TNetUser sender, SFSObject data)
	{
		if (m_GameScene == null)
		{
			return;
		}
		CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo(sender.Id);
		if (netUserInfo != null)
		{
			netUserInfo.m_bRevive = true;
			netUserInfo.m_fReviveTime = 20f;
			if (CGameNetManager.GetInstance().IsRoomMaster())
			{
				CGameNetSender.GetInstance().SendMsg_PLAYER_REVIVE(sender.Id);
			}
		}
	}

	protected void OnMsg_PLAYER_REVIVE(TNetUser sender, SFSObject data)
	{
		if (m_GameScene == null)
		{
			return;
		}
		nmsg_player_revive nmsg_player_revive2 = new nmsg_player_revive();
		nmsg_player_revive2.UnPack(data);
		CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo(nmsg_player_revive2.m_nPlayerId);
		if (netUserInfo == null)
		{
			return;
		}
		netUserInfo.m_bRevive = false;
		CCharPlayer player = m_GameScene.GetPlayer(netUserInfo.m_nUID);
		if (!(player == null))
		{
			if (m_GameScene.IsMyself(player))
			{
				m_GameScene.FinishRevive();
			}
			else
			{
				player.Revive(player.MaxHP);
			}
			iGameUIBase gameUI = m_GameScene.GetGameUI();
			if (gameUI != null)
			{
				gameUI.SetProtraitDeathFlag(false, player.UID);
			}
		}
	}

	protected void OnMsg_PLAYER_MOVE(TNetUser sender, SFSObject data)
	{
		if (m_GameScene == null || sender.IsItMe)
		{
			return;
		}
		CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo(sender.Id);
		if (netUserInfo != null)
		{
			CCharPlayer player = m_GameScene.GetPlayer(netUserInfo.m_nUID);
			if (!(player == null) && !player.isDead)
			{
				nmsg_player_move nmsg_player_move2 = new nmsg_player_move();
				nmsg_player_move2.UnPack(data);
				player.MoveTo(nmsg_player_move2.m_ltPath);
			}
		}
	}

	protected void OnMsg_PLAYER_MOVESTOP(TNetUser sender, SFSObject data)
	{
		if (m_GameScene == null || sender.IsItMe)
		{
			return;
		}
		CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo(sender.Id);
		if (netUserInfo != null)
		{
			CCharPlayer player = m_GameScene.GetPlayer(netUserInfo.m_nUID);
			if (!(player == null) && !player.isDead)
			{
				nmsg_player_stopmove nmsg_player_stopmove2 = new nmsg_player_stopmove();
				nmsg_player_stopmove2.UnPack(data);
				player.MoveStop(nmsg_player_stopmove2.m_v3StopPoint);
			}
		}
	}

	protected void OnMsg_PLAYER_AIM(TNetUser sender, SFSObject data)
	{
		if (m_GameScene == null || sender.IsItMe)
		{
			return;
		}
		CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo(sender.Id);
		if (netUserInfo != null)
		{
			CCharPlayer player = m_GameScene.GetPlayer(netUserInfo.m_nUID);
			if (!(player == null) && !player.isDead)
			{
				nmsg_player_aim nmsg_player_aim2 = new nmsg_player_aim();
				nmsg_player_aim2.UnPack(data);
				player.AimTo(nmsg_player_aim2.m_v3AimPoint);
			}
		}
	}

	protected void OnMsg_PLAYER_SHOOT(TNetUser sender, SFSObject data)
	{
		if (m_GameScene == null || sender.IsItMe)
		{
			return;
		}
		CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo(sender.Id);
		if (netUserInfo != null)
		{
			CCharPlayer player = m_GameScene.GetPlayer(netUserInfo.m_nUID);
			if (!(player == null) && !player.isDead)
			{
				nmsg_player_shoot nmsg_player_shoot2 = new nmsg_player_shoot();
				nmsg_player_shoot2.UnPack(data);
				player.Shoot(nmsg_player_shoot2.m_bShoot);
			}
		}
	}

	protected void OnMsg_PLAYER_SWITCHWEAPON(TNetUser sender, SFSObject data)
	{
		if (m_GameScene == null || sender.IsItMe)
		{
			return;
		}
		CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo(sender.Id);
		if (netUserInfo != null)
		{
			CCharPlayer player = m_GameScene.GetPlayer(netUserInfo.m_nUID);
			if (!(player == null) && !player.isDead)
			{
				nmsg_player_switchweapon nmsg_player_switchweapon2 = new nmsg_player_switchweapon();
				nmsg_player_switchweapon2.UnPack(data);
				player.Shoot(false);
				player.UnEquipWeapon();
				player.EquipWeapon(nmsg_player_switchweapon2.m_nWeaponID, nmsg_player_switchweapon2.m_nWeaponLvl);
			}
		}
	}

	protected void OnMsg_PLAYER_USESKILL(TNetUser sender, SFSObject data)
	{
		if (sender.IsItMe || m_GameScene == null)
		{
			return;
		}
		CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo(sender.Id);
		if (netUserInfo != null)
		{
			CCharPlayer player = m_GameScene.GetPlayer(netUserInfo.m_nUID);
			if (!(player == null) && !player.isDead)
			{
				nmsg_player_useskill nmsg_player_useskill2 = new nmsg_player_useskill();
				nmsg_player_useskill2.UnPack(data);
				player.UseSkill(nmsg_player_useskill2.m_nSkillID, nmsg_player_useskill2.m_nSkillLvl);
			}
		}
	}

	protected void OnMsg_PLAYER_BEATBACK(TNetUser sender, SFSObject data)
	{
		if (sender.IsItMe || m_GameScene == null)
		{
			return;
		}
		CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo(sender.Id);
		if (netUserInfo != null)
		{
			CCharPlayer player = m_GameScene.GetPlayer(netUserInfo.m_nUID);
			if (!(player == null) && !player.isDead)
			{
				nmsg_player_beatback nmsg_player_beatback2 = new nmsg_player_beatback();
				nmsg_player_beatback2.UnPack(data);
				player.BeatBack(nmsg_player_beatback2.m_v3BeatBackPoint);
			}
		}
	}

	protected void OnMsg_PLAYER_LEVELUP(TNetUser sender, SFSObject data)
	{
		if (sender.IsItMe || m_GameScene == null)
		{
			return;
		}
		CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo(sender.Id);
		if (netUserInfo != null)
		{
			CCharPlayer player = m_GameScene.GetPlayer(netUserInfo.m_nUID);
			if (!(player == null) && !player.isDead)
			{
				nmsg_player_levelup nmsg_player_levelup2 = new nmsg_player_levelup();
				nmsg_player_levelup2.UnPack(data);
				player.LevelUp(nmsg_player_levelup2.m_nLevel);
			}
		}
	}

	protected void OnMsg_MOB_DEAD(TNetUser sender, SFSObject data)
	{
		if (m_GameScene != null && !sender.IsItMe)
		{
			nmsg_mob_dead nmsg_mob_dead2 = new nmsg_mob_dead();
			nmsg_mob_dead2.UnPack(data);
			CCharMob mob = m_GameScene.GetMob(nmsg_mob_dead2.m_nMobUID);
			if (!(mob == null) && !mob.isDead)
			{
				mob.OnDead(kDeadMode.Normal);
			}
		}
	}

	protected void OnMsg_MOB_MOVE(TNetUser sender, SFSObject data)
	{
		if (m_GameScene != null && !sender.IsItMe)
		{
			nmsg_mob_move nmsg_mob_move2 = new nmsg_mob_move();
			nmsg_mob_move2.UnPack(data);
			CCharMob mob = m_GameScene.GetMob(nmsg_mob_move2.m_nMobUID);
			if (!(mob == null))
			{
				mob.MoveTo(nmsg_mob_move2.m_v3Dst);
			}
		}
	}

	protected void OnMsg_MOB_HOVER(TNetUser sender, SFSObject data)
	{
		if (m_GameScene != null && !sender.IsItMe)
		{
			nmsg_mob_hover nmsg_mob_hover2 = new nmsg_mob_hover();
			nmsg_mob_hover2.UnPack(data);
			CCharMob mob = m_GameScene.GetMob(nmsg_mob_hover2.m_nMobUID);
			if (!(mob == null))
			{
				mob.m_ltPathHover.Add(nmsg_mob_hover2.m_v3HoverPoint);
			}
		}
	}

	protected void OnMsg_MOB_SKILL(TNetUser sender, SFSObject data)
	{
		if (m_GameScene == null || sender.IsItMe)
		{
			return;
		}
		nmsg_mob_skill nmsg_mob_skill2 = new nmsg_mob_skill();
		nmsg_mob_skill2.UnPack(data);
		CCharMob mob = m_GameScene.GetMob(nmsg_mob_skill2.m_nMobUID);
		if (!(mob == null) && !mob.isDead)
		{
			mob.m_Target = m_GameScene.GetPlayer(nmsg_mob_skill2.m_nTargetUID);
			mob.m_pSkillComboInfo = m_GameData.GetSkillComboInfo(nmsg_mob_skill2.m_nComboSkillID);
			mob.m_nCurComboIndex = nmsg_mob_skill2.m_nComboSkillIndex;
			if (mob.TaskStr == "doIdleTask")
			{
				mob.ResetAI();
			}
		}
	}

	protected void OnMsg_MOB_ACTION(TNetUser sender, SFSObject data)
	{
		if (m_GameScene == null || sender.IsItMe)
		{
			return;
		}
		nmsg_mob_action nmsg_mob_action2 = new nmsg_mob_action();
		nmsg_mob_action2.UnPack(data);
		CCharMob mob = m_GameScene.GetMob(nmsg_mob_action2.m_nMobUID);
		if (!(mob == null))
		{
			mob.m_NetAnim = (kAnimEnum)nmsg_mob_action2.m_nAction;
			if (mob.TaskStr == "doIdleTask")
			{
				mob.ResetAI();
			}
		}
	}

	protected void OnMsg_MOB_BEATBACK(TNetUser sender, SFSObject data)
	{
		if (m_GameScene != null && !sender.IsItMe)
		{
			nmsg_mob_beatback nmsg_mob_beatback2 = new nmsg_mob_beatback();
			nmsg_mob_beatback2.UnPack(data);
			CCharMob mob = m_GameScene.GetMob(nmsg_mob_beatback2.m_nMobUID);
			if (!(mob == null))
			{
				mob.BeatBack(nmsg_mob_beatback2.m_v3BeatBackPoint);
			}
		}
	}

	protected void OnMsg_MGMANAGER_ADDMOB(TNetUser sender, SFSObject data)
	{
		if (m_GameScene != null && !sender.IsItMe)
		{
			nmsg_mgmanager_addmob nmsg_mgmanager_addmob2 = new nmsg_mgmanager_addmob();
			nmsg_mgmanager_addmob2.UnPack(data);
			m_GameScene.AddMobByWave(nmsg_mgmanager_addmob2.m_nMobID, nmsg_mgmanager_addmob2.m_nMobLvl, nmsg_mgmanager_addmob2.m_nMobUID, nmsg_mgmanager_addmob2.m_nWaveID, nmsg_mgmanager_addmob2.m_nSequence, nmsg_mgmanager_addmob2.m_v3Pos, nmsg_mgmanager_addmob2.m_v3Dir);
			if (nmsg_mgmanager_addmob2.m_nMobUID > MyUtils.UIDCount)
			{
				MyUtils.UIDCount = nmsg_mgmanager_addmob2.m_nMobUID;
			}
		}
	}

	protected void OnMsg_MGMANAGER_ADDMOB_SPECIAL(TNetUser sender, SFSObject data)
	{
		if (m_GameScene != null && !sender.IsItMe)
		{
			nmsg_mgmanager_addmob_special nmsg_mgmanager_addmob_special2 = new nmsg_mgmanager_addmob_special();
			nmsg_mgmanager_addmob_special2.UnPack(data);
			CCharMob cCharMob = m_GameScene.AddMob(nmsg_mgmanager_addmob_special2.m_nMobID, nmsg_mgmanager_addmob_special2.m_nMobLvl, nmsg_mgmanager_addmob_special2.m_nMobUID, nmsg_mgmanager_addmob_special2.m_v3Pos, nmsg_mgmanager_addmob_special2.m_v3Dir);
			if (cCharMob != null)
			{
				cCharMob.m_bShowTime = false;
				m_GameScene.AddEffect(cCharMob.GetBone(0).position, Vector3.forward, 2f, 1950);
			}
			if (nmsg_mgmanager_addmob_special2.m_nMobUID > MyUtils.UIDCount)
			{
				MyUtils.UIDCount = nmsg_mgmanager_addmob_special2.m_nMobUID;
			}
		}
	}

	protected void OnMsg_BATTLE_DAMAGE_MOB(TNetUser sender, SFSObject data)
	{
		if (m_GameScene == null || sender.IsItMe)
		{
			return;
		}
		nmsg_battle_damage_mob nmsg_battle_damage_mob2 = new nmsg_battle_damage_mob();
		nmsg_battle_damage_mob2.UnPack(data);
		CCharMob mob = m_GameScene.GetMob(nmsg_battle_damage_mob2.m_nMobUID);
		if (mob == null || mob.isDead)
		{
			return;
		}
		mob.SetLifeBarParam(1f);
		CCharBoss cCharBoss = mob as CCharBoss;
		if (cCharBoss != null && cCharBoss.isInBlack)
		{
			if (nmsg_battle_damage_mob2.m_bBlack)
			{
				cCharBoss.AddBlackDmg(0f - nmsg_battle_damage_mob2.m_fDamage);
			}
		}
		else
		{
			mob.OnHit(0f - nmsg_battle_damage_mob2.m_fDamage, null, string.Empty);
		}
	}

	protected void OnMsg_BATTLE_DAMAGE_PLAYER(TNetUser sender, SFSObject data)
	{
		if (m_GameScene != null && !sender.IsItMe)
		{
			nmsg_battle_damage_player nmsg_battle_damage_player2 = new nmsg_battle_damage_player();
			nmsg_battle_damage_player2.UnPack(data);
			CCharPlayer player = m_GameScene.GetPlayer(nmsg_battle_damage_player2.m_nPlayerUID);
			if (!(player == null) && !player.isDead)
			{
				player.SetHP(nmsg_battle_damage_player2.m_fCurHP, nmsg_battle_damage_player2.m_fMaxHP);
			}
		}
	}

	protected void OnMsg_BATTLE_RESULT_PLAYERREWARDS(TNetUser sender, SFSObject data)
	{
		if (m_GameScene != null && !sender.IsItMe)
		{
			nmsg_battle_result_playerrewards nmsg_battle_result_playerrewards2 = new nmsg_battle_result_playerrewards();
			nmsg_battle_result_playerrewards2.UnPack(data);
			CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo(sender.Id);
			if (netUserInfo != null)
			{
				netUserInfo.m_nTitle = nmsg_battle_result_playerrewards2.m_nTitle;
				netUserInfo.m_nCombatRatings = nmsg_battle_result_playerrewards2.m_nCombatRatings;
				netUserInfo.m_nGainCrystalInGame = nmsg_battle_result_playerrewards2.m_nGainCrystalInGame;
				netUserInfo.m_nGainGoldInGame = nmsg_battle_result_playerrewards2.m_nGainGoldInGame;
				netUserInfo.m_nBeadmireCount = nmsg_battle_result_playerrewards2.m_nBeadmireCount;
				netUserInfo.m_nCharLvlResult = nmsg_battle_result_playerrewards2.m_nCharLvlResult;
				netUserInfo.m_nCharExpResult = nmsg_battle_result_playerrewards2.m_nCharExpResult;
				netUserInfo.m_nHunterLvlResult = nmsg_battle_result_playerrewards2.m_nHunterLvlResult;
				netUserInfo.m_nHunterExpResult = nmsg_battle_result_playerrewards2.m_nHunterExpResult;
			}
		}
	}

	protected void OnMsg_BATTLE_RESULT_ADMIRE(TNetUser sender, SFSObject data)
	{
		if (sender.IsItMe)
		{
			return;
		}
		nmsg_battle_result_admire nmsg_battle_result_admire2 = new nmsg_battle_result_admire();
		nmsg_battle_result_admire2.UnPack(data);
		if (sender.Id == nmsg_battle_result_admire2.m_nId)
		{
			return;
		}
		CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo(nmsg_battle_result_admire2.m_nId);
		if (netUserInfo == null)
		{
			return;
		}
		netUserInfo.m_nBeadmireCount++;
		if (m_GameUI != null)
		{
			gyUIPlayerRewards panelMissionSuccessMutiply_PlayerRewards = m_GameUI.GetPanelMissionSuccessMutiply_PlayerRewards(netUserInfo.m_nResultIndex);
			if (panelMissionSuccessMutiply_PlayerRewards != null && panelMissionSuccessMutiply_PlayerRewards.m_AdmireCount != null)
			{
				panelMissionSuccessMutiply_PlayerRewards.m_AdmireCount.Value = panelMissionSuccessMutiply_PlayerRewards.m_AdmireCount.Value + 1;
			}
		}
		TNetUser me = CGameNetManager.GetInstance().GetMe();
		if (me != null && me.Id == nmsg_battle_result_admire2.m_nId && m_DataCenter != null)
		{
			m_DataCenter.BeAdmire++;
		}
	}

	protected void OnMsg_SOCIAL_ADDFRIEND(TNetUser sender, SFSObject data)
	{
		if (!sender.IsItMe)
		{
			nmsg_social_addfriend nmsg_social_addfriend2 = new nmsg_social_addfriend();
			nmsg_social_addfriend2.UnPack(data);
			if (m_DataCenter != null && !m_DataCenter.IsFriend(nmsg_social_addfriend2.m_sId))
			{
				m_DataCenter.AddFriend(nmsg_social_addfriend2.m_sId);
				CGameNetSender.GetInstance().SendMsg_SOCIAL_ADDFRIEND(iServerSaveData.GetInstance().CurDeviceId, sender.Id);
			}
		}
	}
}
