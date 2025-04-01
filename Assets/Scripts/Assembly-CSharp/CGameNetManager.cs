using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Boomlagoon.JSON;
using EventCenter;
using LitJson;
using TNetSdk;
using UnityEngine;

public class CGameNetManager : MonoBehaviour
{
	public enum kMutiplyState
	{
		None,
		ReadyToPlay,
		Gaming
	}

	public class CNetUserInfoBase
	{
		public int m_nId;

		public string m_sDeivceId;

		public string m_sName;

		public int m_nHunterLvl;

		public int m_nHunterExp;

		public int m_nTitle;

		public int m_nCharID;

		public int m_nCharLvl;

		public int m_nCharExp;

		public int m_nWeaponID;

		public int m_nWeaponLvl;

		public int m_nAvatarHead;

		public int m_nAvatarUpper;

		public int m_nAvatarLower;

		public int m_nAvatarHeadup;

		public int m_nAvatarNeck;

		public int m_nAvatarBracelet;

		public int m_nAvatarBadge;

		public int m_nAvatarStone;

		public int m_nCombatPower;

		public string m_sSignature;

		public int[] m_arrWeapon = new int[3] { -1, -1, -1 };

		public bool m_bLeaved;
	}

	public class CNetUserInfo : CNetUserInfoBase
	{
		public int m_nUID;

		public Vector3 m_v3Pos;

		public Vector3 m_v3Dir;

		public bool m_bLoaded;

		public bool m_bRevive;

		public float m_fReviveTime;

		public Texture2D m_Photo;

		public int m_nResultIndex;

		public int m_nCombatRatings;

		public int m_nGainCrystalInGame;

		public int m_nGainGoldInGame;

		public int m_nBeadmireCount;

		public int m_nCharLvlResult;

		public int m_nCharExpResult;

		public int m_nHunterLvlResult;

		public int m_nHunterExpResult;
	}

	public class CRoomVarInfo
	{
		public int m_nHunterLevel;

		public int m_nGameLevel;

		public bool m_bBlack;
	}

	protected class CFetchUUIDInfo
	{
		public string m_sGCAccount;

		public string m_sUUID;
	}

	public delegate void OnFetchUUIDList_S(List<string> ltUUIDList);

	protected static CGameNetManager m_Instance;

	protected kMutiplyState m_MutiplyState;

	protected bool m_bLogined;

	protected string m_sUserName = "TestAccount";

	protected TNetRoom m_curRoom;

	protected float m_fTimeInRoom;

	protected bool m_bGaming;

	public int m_nGroup;

	protected Dictionary<int, CNetUserInfo> m_dictNetUserInfo = new Dictionary<int, CNetUserInfo>();

	protected int m_nFetchUUIDIndex;

	protected List<CFetchUUIDInfo> m_ltFetchUUIDList;

	protected OnFetchUUIDList_S m_OnFetchUUIDList_S;

	public int m_nCurRoomGameLevelID { get; private set; }

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

	protected iDataCenterNet m_DataCenterNet
	{
		get
		{
			if (m_GameData == null)
			{
				return null;
			}
			return m_GameData.GetDataCenterNet();
		}
	}

	public kMutiplyState MutiplyState
	{
		get
		{
			return m_MutiplyState;
		}
		set
		{
			m_MutiplyState = value;
		}
	}

	public string UserName
	{
		get
		{
			return m_sUserName;
		}
		set
		{
			m_sUserName = value;
		}
	}

	public bool IsGaming
	{
		get
		{
			return true;
			//return m_bGaming;
		}
		set
		{
			m_bGaming = value;
		}
	}

	public static CGameNetManager GetInstance()
	{
		if (m_Instance == null)
		{
			GameObject gameObject = new GameObject("_GameNetManager");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			m_Instance = gameObject.AddComponent<CGameNetManager>();
		}
		return m_Instance;
	}

	private void Awake()
	{
		CGameNetAccepter.GetInstance().Register();
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (m_curRoom == null || IsRoomMaster())
		{
		}
		if (IsRoomMaster())
		{
			if (!m_bGaming)
			{
				bool flag = true;
				foreach (CNetUserInfo value in m_dictNetUserInfo.Values)
				{
					if (value.m_bLeaved || value.m_bLoaded)
					{
						continue;
					}
					flag = false;
					break;
				}
				if (flag)
				{
					CGameNetSender.GetInstance().SendMsg_GAME_START();
				}
			}
			else
			{
				foreach (CNetUserInfo value2 in m_dictNetUserInfo.Values)
				{
					if (!value2.m_bLeaved && value2.m_bRevive)
					{
						value2.m_fReviveTime -= Time.deltaTime;
						if (value2.m_fReviveTime <= 0f)
						{
							value2.m_bRevive = false;
						}
					}
				}
			}
		}
		m_DataCenterNet.Update(Time.deltaTime);
	}

	public Dictionary<int, CNetUserInfo> GetNetUserInfoData()
	{
		return m_dictNetUserInfo;
	}

	public CNetUserInfo GetNetUserInfo(int nId)
	{
		if (!m_dictNetUserInfo.ContainsKey(nId))
		{
			return null;
		}
		return m_dictNetUserInfo[nId];
	}

	public CNetUserInfo GetNetUserInfoByIndex(int nIndex)
	{
		if (nIndex < 0 || nIndex >= m_dictNetUserInfo.Count)
		{
			return null;
		}
		int num = 0;
		foreach (CNetUserInfo value in m_dictNetUserInfo.Values)
		{
			if (num == nIndex)
			{
				return value;
			}
			num++;
		}
		return null;
	}

	public CNetUserInfo GetNetUserInfoByResultIndex(int nIndex)
	{
		foreach (CNetUserInfo value in m_dictNetUserInfo.Values)
		{
			if (value.m_nResultIndex == nIndex)
			{
				return value;
			}
		}
		return null;
	}

	public CNetUserInfo GetNetUserInfo()
	{
		if (TNetManager.GetInstance().Connection == null)
		{
			return null;
		}
		return GetNetUserInfo(TNetManager.GetInstance().Connection.Myself.Id);
	}

	public void AddNetUserInfo(int nId, CNetUserInfo netuserinfo)
	{
		if (!m_dictNetUserInfo.ContainsKey(nId))
		{
			netuserinfo.m_nId = nId;
			m_dictNetUserInfo.Add(nId, netuserinfo);
		}
	}

	public void DelNetUserInfo(int nId)
	{
		if (m_dictNetUserInfo.ContainsKey(nId))
		{
			m_dictNetUserInfo.Remove(nId);
		}
	}

	public void ClearNetUserInfo()
	{
		m_dictNetUserInfo.Clear();
	}

	public bool connected { get; set; }

	public bool IsConnected()
	{
		return connected;
		//if (TNetManager.GetInstance() == null || TNetManager.GetInstance().Connection == null || TNetManager.GetInstance().Connection.GetStatus() != TNetObject.STATUS.kConnected)
		//{
		//	return false;
		//}
		//return true;
	}

	public bool IsLogin()
	{
		return m_bLogined;
	}

	public bool IsRoomMaster()
	{
		return true;
		if (TNetManager.GetInstance().Connection == null || TNetManager.GetInstance().Connection.CurRoom == null)
		{
			return true;
		}
		if (TNetManager.GetInstance().Connection.GetStatus() == TNetObject.STATUS.kClosed)
		{
			return true;
		}
		if (TNetManager.GetInstance().Connection.CurRoom.UserCount == 1)
		{
			return true;
		}
		if (TNetManager.GetInstance().Connection.Myself.Id == TNetManager.GetInstance().Connection.CurRoom.RoomMasterID)
		{
			return true;
		}
		return false;
	}

	public bool IsMe(int nId)
	{
		if (TNetManager.GetInstance().Connection == null)
		{
			return true;
		}
		return TNetManager.GetInstance().Connection.Myself.Id == nId;
	}

	public TNetRoom GetCurRoom()
	{
		return m_curRoom;
	}

	public int RoomateCount()
	{
		if (m_dictNetUserInfo == null)
		{
			return 0;
		}
		return m_dictNetUserInfo.Count;
	}

	public TNetUser GetMe()
	{
		if (TNetManager.GetInstance().Connection == null)
		{
			return null;
		}
		return TNetManager.GetInstance().Connection.Myself;
	}

	public void Connect(string ip, int port, int group)
	{
		if (TNetManager.GetInstance().Connection == null)
		{
			if (group >= 9000 && group < 10000)
			{
				m_nGroup = group;
			}
			else
			{
				m_nGroup = 9000;
			}
			if (m_nGroup == 9000)
			{
				m_nGroup = 9500;
			}
			TNetManager.GetInstance().Connect(ip, port);
			TNetManager.GetInstance().m_OnConnectSuccess = OnConnectSuccess;
			TNetManager.GetInstance().m_OnConnectFailed = OnConnectFailed;
			TNetManager.GetInstance().m_OnConnectTimeout = OnConnectTimeout;
			TNetManager.GetInstance().m_OnConnectDisconnect = OnConnectDisconnect;
			TNetManager.GetInstance().m_OnLoginSuccess = OnLoginSuccess;
			TNetManager.GetInstance().m_OnLoginFailed = OnLoginFailed;
			TNetManager.GetInstance().m_OnEnterRoom = OnEnterRoom;
			TNetManager.GetInstance().m_OnLeaveRoom = OnLeaveRoom;
			TNetManager.GetInstance().m_OnRoomList = OnRoomList;
			TNetManager.GetInstance().m_OnCreateRoom = OnCreateRoom;
			TNetManager.GetInstance().m_OnRoomMasterChange = OnRoomMasterChange;
			TNetManager.GetInstance().m_OnUserEnterRoom = OnUserEnterRoom;
			TNetManager.GetInstance().m_OnUserLeaveRoom = OnUserLeaveRoom;
			TNetManager.GetInstance().m_OnCustomMsgFunc = OnCustomMsg;
		}
	}

	public void Login()
	{
		TNetManager.GetInstance().Login(m_sUserName, string.Empty);
	}

	public void SearchRoom()
	{
		TNetManager.GetInstance().ApplyRoomList(m_nGroup, 1, 15, RoomDragListCmd.ListType.not_full_not_game);
	}

	protected void OnConnectSuccess()
	{
		Login();
		CTrinitiCollectManager.GetInstance().SendEnterCoop();
	}

	protected void OnConnectFailed()
	{
		m_bLogined = false;
		ClearNetUserInfo();
		if (m_GameState.CurScene != kGameSceneEnum.Home && m_GameState.CurScene != kGameSceneEnum.MutipyHome && !m_GameState.m_bInIAPScene)
		{
			CMessageBoxScript.GetInstance().MessageBox(string.Empty, "Connection failed!", null, null, "OK");
			iGameApp.GetInstance().EnterScene(kGameSceneEnum.MutipyHome);
		}
		CTrinitiCollectManager.GetInstance().SendQuitCoop();
	}

	protected void OnConnectTimeout()
	{
		m_bLogined = false;
		ClearNetUserInfo();
		if (m_GameState.CurScene != kGameSceneEnum.Home && m_GameState.CurScene != kGameSceneEnum.MutipyHome && !m_GameState.m_bInIAPScene)
		{
			CMessageBoxScript.GetInstance().MessageBox(string.Empty, "Connection lost!", null, null, "OK");
			iGameApp.GetInstance().EnterScene(kGameSceneEnum.MutipyHome);
		}
		CTrinitiCollectManager.GetInstance().SendQuitCoop();
	}

	protected void OnConnectDisconnect()
	{
		m_bLogined = false;
		ClearNetUserInfo();
		if (m_GameState.CurScene != kGameSceneEnum.Home && m_GameState.CurScene != kGameSceneEnum.MutipyHome)
		{
			iGameApp.GetInstance().EnterScene(kGameSceneEnum.MutipyHome);
		}
		CTrinitiCollectManager.GetInstance().SendQuitCoop();
	}

	protected void OnLoginSuccess()
	{
		m_bLogined = true;
		SearchRoom();
	}

	protected void OnLoginFailed()
	{
		m_bLogined = false;
	}

	protected void OnEnterRoom(TNetRoom room)
	{
		if (room.IsGaming)
		{
			SearchRoom();
			return;
		}
		Debug.Log("i enter room " + room.Name);
		foreach (TNetUser user in room.UserList)
		{
			Debug.Log(user.Name);
		}
		m_curRoom = room;
		m_fTimeInRoom = 5f;
		m_nCurRoomGameLevelID = 0;
		CRoomVarInfo roomvarinfo = new CRoomVarInfo();
		if (UnPackComment(room.Commnet, ref roomvarinfo))
		{
			m_nCurRoomGameLevelID = roomvarinfo.m_nGameLevel;
		}
		CGameNetAccepter.GetInstance().OnCustomMsg(TNetManager.GetInstance().Connection.Myself, kGameNetEnum.LOBBY_ENTER, new SFSObject());
	}

	protected void OnLeaveRoom(TNetRoom room)
	{
		if (room != null)
		{
			Debug.Log("i leave room " + room.Name);
		}
		m_curRoom = null;
		m_fTimeInRoom = 0f;
		CGameNetAccepter.GetInstance().OnCustomMsg(TNetManager.GetInstance().Connection.Myself, kGameNetEnum.LOBBY_LEAVE, new SFSObject());
	}

	protected void OnRoomList(List<TNetRoom> ltRoom)
	{
		if (ltRoom.Count > 0)
		{
			foreach (TNetRoom item in ltRoom)
			{
				if (item.UserCount != item.MaxUsers)
				{
					string commnet = item.Commnet;
					CRoomVarInfo roomvarinfo = new CRoomVarInfo();
					if (UnPackComment(commnet, ref roomvarinfo) && m_GameData.m_HunterLevelCenter != null && (roomvarinfo.m_nHunterLevel == 0 || m_GameData.m_HunterLevelCenter.IsSameGroup(m_DataCenter.HunterLvl, roomvarinfo.m_nHunterLevel)) && roomvarinfo.m_bBlack == (!m_DataCenter.m_bInWhiteName && m_DataCenter.m_bInBlackName))
					{
						TNetManager.GetInstance().JoinRoom(item.Id, string.Empty);
						return;
					}
				}
			}
		}
		if (m_GameData.m_HunterLevelCenter != null)
		{
			CHunterLevelInfo cHunterLevelInfo = m_GameData.m_HunterLevelCenter.Get(m_DataCenter.HunterLvl);
			if (cHunterLevelInfo != null)
			{
				m_nCurRoomGameLevelID = cHunterLevelInfo.GetGameLevel();
			}
		}
		CRoomVarInfo cRoomVarInfo = new CRoomVarInfo();
		cRoomVarInfo.m_bBlack = !m_DataCenter.m_bInWhiteName && m_DataCenter.m_bInBlackName;
		cRoomVarInfo.m_nGameLevel = m_nCurRoomGameLevelID;
		cRoomVarInfo.m_nHunterLevel = m_DataCenter.HunterLvl;
		string comment = PackComment(cRoomVarInfo);
		string sRoomName = m_sUserName + "' Room";
		TNetManager.GetInstance().CreateRoom(sRoomName, string.Empty, m_nGroup, 3, RoomCreateCmd.RoomType.open, RoomCreateCmd.RoomSwitchMasterType.Auto, comment);
	}

	protected void OnCreateRoom(int nRoomID)
	{
		TNetManager.GetInstance().JoinRoom(nRoomID, string.Empty);
	}

	protected void OnRoomMasterChange(TNetUser roommaster)
	{
		Debug.Log("new master is " + roommaster.Name);
		if (m_GameScene == null)
		{
			if (roommaster.IsItMe)
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_ShowBtnStart, true));
			}
			else
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopRoom(TUIEvent.SceneCoopRoomEventType.TUIEvent_ShowBtnStart));
			}
		}
		else
		{
			if (!roommaster.IsItMe)
			{
				return;
			}
			foreach (CCharMob item in m_GameScene.GetMobEnumerator())
			{
				if (!(item == null) && !item.isDead)
				{
					item.SetBehavior(item.MobBehavior);
				}
			}
		}
	}

	protected void OnUserEnterRoom(TNetUser user)
	{
		iGameApp.GetInstance().ScreenLog(user.Name + " enter room, count = " + m_curRoom.UserCount);
		if (m_curRoom != null && m_curRoom.IsGaming)
		{
			iGameApp.GetInstance().ScreenLog(user.Name + " enter failed, game is already start!");
		}
		else
		{
			CGameNetAccepter.GetInstance().OnCustomMsg(user, kGameNetEnum.LOBBY_ENTER_PLAYER, new SFSObject());
		}
	}

	protected void OnUserLeaveRoom(TNetUser user)
	{
		iGameApp.GetInstance().ScreenLog(user.Name + " leave room, count = " + (m_curRoom.UserCount - 1));
		CGameNetAccepter.GetInstance().OnCustomMsg(user, kGameNetEnum.LOBBY_LEAVE_PLAYER, new SFSObject());
	}

	protected void OnCustomMsg(TNetUser netuser, kGameNetEnum nmsg, SFSObject data)
	{
		CGameNetAccepter.GetInstance().OnCustomMsg(netuser, nmsg, data);
	}

	public void SetRoomVar(int nHunterLvl, bool bLimitHunterLvl)
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutInt("hunterlvl", nHunterLvl);
		sFSObject.PutBool("limithunterlvl", bLimitHunterLvl);
		TNetManager.GetInstance().SetRoomVar(TNetRoomVarType.RoomRight, sFSObject);
	}

	public string PackComment(CRoomVarInfo roomvarinfo)
	{
		return roomvarinfo.m_nHunterLevel + "," + roomvarinfo.m_bBlack + "," + roomvarinfo.m_nGameLevel;
	}

	public bool UnPackComment(string comment, ref CRoomVarInfo roomvarinfo)
	{
		if (comment == null || comment.Length < 1)
		{
			return false;
		}
		string[] array = comment.Split(',');
		if (array == null || array.Length != 3)
		{
			return false;
		}
		roomvarinfo.m_nHunterLevel = int.Parse(array[0]);
		roomvarinfo.m_bBlack = bool.Parse(array[1]);
		roomvarinfo.m_nGameLevel = int.Parse(array[2]);
		return true;
	}

	public void SetRoomComment(string sComment)
	{
		if (m_curRoom != null && sComment != null)
		{
			TNetManager.GetInstance().SetRoomComment(m_curRoom.Id, sComment);
		}
	}

	public bool GetRoomVar(TNetRoom room, ref int nHunterLvl, ref bool bLimitHunterLvl)
	{
		if (room == null)
		{
			return false;
		}
		SFSObject variable = room.GetVariable(TNetRoomVarType.RoomRight);
		nHunterLvl = variable.GetInt("hunterlvl");
		bLimitHunterLvl = variable.GetBool("limithunterlvl");
		return true;
	}

	public void UploadNameCard(string userId, string nickname, int title, CNCPack ncpack)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("userId", userId);
		hashtable.Add("nickName", nickname);
		hashtable.Add("title", title);
		try
		{
			byte[] array = MyUtils.Serialize(ncpack);
			string @string = Encoding.UTF8.GetString(array);
			Debug.Log(@string);
			Debug.Log("namecard npcpack length = " + Convert.ToBase64String(array).Length);
			hashtable.Add("exts", Convert.ToBase64String(array));
			iServerHttp.SendRequest(iMacroDefine.ServerUrl_CoopData, -1f, null, "userHandler.saveUser", JsonMapper.ToJson(hashtable));
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
			Debug.Log(ex.StackTrace);
		}
	}

	public void UploadRankData(string userId, string nickname, int combatpower, int totalexp, int hunterlvl, int gold, int crystal, int applause)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("userId", userId);
		hashtable.Add("nickName", nickname);
		hashtable.Add("combatpower", combatpower);
		hashtable.Add("exp", totalexp);
		hashtable.Add("hunterLv", hunterlvl);
		hashtable.Add("crystal", crystal);
		hashtable.Add("gold", gold);
		hashtable.Add("applause", applause);
		iServerHttp.SendRequest(iMacroDefine.ServerUrl_CoopData, -1f, null, "userHandler.insertLeaderboard", JsonMapper.ToJson(hashtable));
	}

	public void FetchNameCard(string sId, iServerHttp.OnSuccess onsuccess, iServerHttp.OnFailed onfail)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("userId", sId);
		iServerHttp.SendRequest(iMacroDefine.ServerUrl_CoopData, -1f, null, "userHandler.loadUser", JsonMapper.ToJson(hashtable), onsuccess, onfail);
	}

	public void FetchNameCards(string[] arrId, iServerHttp.OnSuccess onsuccess, iServerHttp.OnFailed onfail)
	{
		if (arrId != null && arrId.Length >= 1)
		{
			string text = string.Empty;
			for (int i = 0; i < arrId.Length; i++)
			{
				text = ((i != 0) ? (text + "," + arrId[i]) : arrId[i]);
			}
			Hashtable hashtable = new Hashtable();
			hashtable.Add("userIds", text);
			iServerHttp.SendRequest(iMacroDefine.ServerUrl_CoopData, -1f, null, "userHandler.listUsers", JsonMapper.ToJson(hashtable), onsuccess, onfail);
		}
	}

	public void FetchRank(string sId, int nHunterExpTotal, iServerHttp.OnSuccess onsuccess, iServerHttp.OnFailed onfail)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("userId", sId);
		hashtable.Add("exp", nHunterExpTotal);
		hashtable.Add("page", 1);
		hashtable.Add("count", 200);
		iServerHttp.SendRequest(iMacroDefine.ServerUrl_CoopData, -1f, null, "userHandler.listLeaderboard", JsonMapper.ToJson(hashtable), onsuccess, onfail);
	}

	public void FetchRankByUsers(string[] arrId, iServerHttp.OnSuccess onsuccess, iServerHttp.OnFailed onfail)
	{
		string text = string.Empty;
		for (int i = 0; i < arrId.Length; i++)
		{
			text = ((i != 0) ? (text + "," + arrId[i]) : arrId[i]);
		}
		Hashtable hashtable = new Hashtable();
		hashtable.Add("userIds", text);
		iServerHttp.SendRequest(iMacroDefine.ServerUrl_CoopData, -1f, null, "userHandler.listFriendLeaderboard", JsonMapper.ToJson(hashtable), onsuccess, onfail);
	}

	public void FetchPhoto(string sDeviceId)
	{
		HttpClient.ServerInfo server = HttpClient.Instance().GetServer("dataserver");
		if (server != null)
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("game", "dinohunter");
			hashtable.Add("version", "3.1.7a");
			hashtable.Add("deviceId", sDeviceId);
			hashtable.Add("facebookId", string.Empty);
			hashtable.Add("gamecenterId", string.Empty);
			iServerHttp.SendRequest(server.url, server.timeout, server.key, "multiCsHandler.enter", JsonMapper.ToJson(hashtable), OnFetchGCAccount_S);
		}
	}

	protected void OnFetchGCAccount_S(string response)
	{
		try
		{
			JSONObject jSONObject = JSONObject.Parse(response);
			string @string = jSONObject.GetString("code");
			if (!(@string == "0"))
			{
				return;
			}
			string string2 = jSONObject.GetString("serverId");
			string string3 = jSONObject.GetString("serverUrl");
			JSONObject @object = jSONObject.GetObject("user");
			if (@object == null)
			{
				throw new Exception();
			}
			string string4 = @object.GetString("deviceId");
			string string5 = @object.GetString("userId");
			string string6 = @object.GetString("gamecenterId");
			if (m_DataCenterNet == null)
			{
				return;
			}
			CNameCardInfo nameCardInfo = m_DataCenterNet.GetNameCardInfo(string4);
			if (nameCardInfo != null)
			{
				nameCardInfo.m_sGCAccount = string6;
				if (string6.Length > 0)
				{
					FetchPhoto(string4, string6);
				}
			}
		}
		catch
		{
			Debug.Log("OnFetchGCAccount_S parse error!");
		}
	}

	public void FetchPhoto(string sDeviceId, string sGCAccount)
	{
		Debug.Log(sDeviceId + " " + sGCAccount);
		if (sDeviceId != null && sDeviceId.Length >= 1)
		{
			if (sDeviceId == iServerSaveData.GetInstance().CurDeviceId)
			{
				sGCAccount = iServerSaveData.GetInstance().GameCenterId;
			}
			if (sGCAccount != null && sGCAccount.Length >= 1)
			{
				sGCAccount = MyUtils.TranslateGCAccount(sGCAccount, true);
				GameCenterPlugin.LoadPhoto(sGCAccount);
				Debug.Log(sGCAccount);
				iUpdateHandleManager.GetInstance().AddEvent(OnEvent_FetchPhotoInBackground, new List<object> { sDeviceId, sGCAccount }, 5f);
			}
		}
	}

	protected bool OnEvent_FetchPhotoInBackground(List<object> ltParam)
	{
		if (ltParam.Count < 0)
		{
			return true;
		}
		string text = ltParam[0] as string;
		string text2 = ltParam[1] as string;
		int photoState = GameCenterPlugin.GetPhotoState(text2);
		if (photoState == 1)
		{
			return false;
		}
		Debug.Log("fetch photo " + text);
		CNameCardInfo nameCardInfo = m_DataCenterNet.GetNameCardInfo(text);
		if (nameCardInfo == null)
		{
			return true;
		}
		Debug.Log("fetch photo photostate " + photoState);
		nameCardInfo.m_nPhotoState = photoState;
		if (photoState == 2)
		{
			int photoSize = GameCenterPlugin.GetPhotoSize(text2);
			Debug.Log("fetch photo size " + photoSize + " " + text2);
			if (photoSize > 0)
			{
				byte[] array = new byte[photoSize];
				if (GameCenterPlugin.GetPhoto(text2, array))
				{
					Debug.Log("fetch photo 5");
					Debug.Log("photo exist " + array);
					nameCardInfo.SetPhoto(array);
					nameCardInfo.ResetPhotoTime();
					if (m_DataCenter != null)
					{
						m_DataCenter.SetPhoto(array);
					}
					GameCenterPlugin.ReleasePhoto(text2);
					TUIGameInfo tUIGameInfo = new TUIGameInfo();
					tUIGameInfo.player_texture_info = new TUIUpdatePlayerTextureInfo(nameCardInfo.m_sID, nameCardInfo.GetPhoto());
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneCoopMainMenu(TUIEvent.SceneCoopMainMenuEventType.TUIEvent_UpdatePlayerTexture, tUIGameInfo));
				}
				else
				{
					Debug.Log("photo does not exist ");
				}
			}
			return true;
		}
		return true;
	}

	public void FetchUUIDList(List<string> ltGCAccount, OnFetchUUIDList_S onfetch_s)
	{
		if (ltGCAccount == null || ltGCAccount.Count < 1)
		{
			return;
		}
		if (m_ltFetchUUIDList == null)
		{
			m_ltFetchUUIDList = new List<CFetchUUIDInfo>();
		}
		m_ltFetchUUIDList.Clear();
		foreach (string item in ltGCAccount)
		{
			CFetchUUIDInfo cFetchUUIDInfo = new CFetchUUIDInfo();
			cFetchUUIDInfo.m_sGCAccount = MyUtils.TranslateGCAccount(item, false);
			cFetchUUIDInfo.m_sUUID = string.Empty;
			m_ltFetchUUIDList.Add(cFetchUUIDInfo);
		}
		m_nFetchUUIDIndex = 0;
		FetchUUID(m_ltFetchUUIDList[m_nFetchUUIDIndex].m_sGCAccount);
		m_OnFetchUUIDList_S = onfetch_s;
	}

	public void FetchUUID(string gcaccount)
	{
		UnityEngine.Debug.Log("gc friends ready to fetch uuid " + gcaccount);
		HttpClient.ServerInfo server = HttpClient.Instance().GetServer("dataserver");
		if (server != null)
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("game", "dinohunter");
			hashtable.Add("version", "3.1.7a");
			hashtable.Add("deviceId", iServerSaveData.GetInstance().CurDeviceId);
			hashtable.Add("facebookId", string.Empty);
			hashtable.Add("gamecenterId", gcaccount);
			iServerHttp.SendRequest(server.url, server.timeout, server.key, "multiCsHandler.enter", JsonMapper.ToJson(hashtable), OnFetchUUID_S);
		}
	}

	protected void OnFetchUUID_S(string response)
	{
		try
		{
			JSONObject jSONObject = JSONObject.Parse(response);
			string @string = jSONObject.GetString("code");
			if (@string == "0")
			{
				string string2 = jSONObject.GetString("serverId");
				string string3 = jSONObject.GetString("serverUrl");
				JSONObject @object = jSONObject.GetObject("user");
				if (@object == null)
				{
					throw new Exception();
				}
				string string4 = @object.GetString("deviceId");
				string string5 = @object.GetString("userId");
				string string6 = @object.GetString("gamecenterId");
				if (string4.Length > 0 && m_ltFetchUUIDList != null && m_nFetchUUIDIndex >= 0 && m_nFetchUUIDIndex < m_ltFetchUUIDList.Count && iServerSaveData.GetInstance().CurDeviceId != string4)
				{
					m_ltFetchUUIDList[m_nFetchUUIDIndex].m_sUUID = string4;
					UnityEngine.Debug.Log(m_ltFetchUUIDList[m_nFetchUUIDIndex].m_sGCAccount + " = " + string4);
				}
			}
		}
		catch
		{
			Debug.Log("OnFetchGCAccount_S parse error!");
		}
		finally
		{
			if (m_nFetchUUIDIndex == m_ltFetchUUIDList.Count - 1)
			{
				if (m_OnFetchUUIDList_S != null)
				{
					List<string> list = new List<string>();
					foreach (CFetchUUIDInfo ltFetchUUID in m_ltFetchUUIDList)
					{
						if (ltFetchUUID.m_sUUID != null && ltFetchUUID.m_sUUID.Length > 0)
						{
							list.Add(ltFetchUUID.m_sUUID);
						}
					}
					m_OnFetchUUIDList_S(list);
				}
			}
			else
			{
				m_nFetchUUIDIndex++;
				FetchUUID(m_ltFetchUUIDList[m_nFetchUUIDIndex].m_sGCAccount);
			}
		}
	}
}
