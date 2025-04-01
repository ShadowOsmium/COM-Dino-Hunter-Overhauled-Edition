using System.Collections.Generic;
using TNetSdk;
using UnityEngine;

public class TNetManager : MonoBehaviour
{
	public delegate void OnCommonFunc();

	public delegate void OnEnterRoomFunc(TNetRoom room);

	public delegate void OnRoomListFunc(List<TNetRoom> ltRoom);

	public delegate void OnCreateRoomFunc(int nRoomID);

	public delegate void OnRoomMasterChangeFunc(TNetUser roommaster);

	public delegate void OnUserEnterRoomFunc(TNetUser user);

	public delegate void OnCustomMsgFunc(TNetUser netuser, kGameNetEnum nmsg, SFSObject data);

	protected static TNetManager m_Instance;

	protected TNetObject m_Connection;

	public OnCommonFunc m_OnConnectSuccess;

	public OnCommonFunc m_OnConnectFailed;

	public OnCommonFunc m_OnConnectTimeout;

	public OnCommonFunc m_OnConnectDisconnect;

	public OnCommonFunc m_OnLoginSuccess;

	public OnCommonFunc m_OnLoginFailed;

	public OnEnterRoomFunc m_OnEnterRoom;

	public OnEnterRoomFunc m_OnLeaveRoom;

	public OnRoomListFunc m_OnRoomList;

	public OnCreateRoomFunc m_OnCreateRoom;

	public OnRoomMasterChangeFunc m_OnRoomMasterChange;

	public OnUserEnterRoomFunc m_OnUserEnterRoom;

	public OnUserEnterRoomFunc m_OnUserLeaveRoom;

	public OnCustomMsgFunc m_OnCustomMsgFunc;

	public TNetObject Connection
	{
		get
		{
			if (m_Connection == null || !m_Connection.IsContected())
			{
				return null;
			}
			return m_Connection;
		}
	}

	public TNetObject NetObject
	{
		get
		{
			return m_Connection;
		}
	}

	public static TNetManager GetInstance()
	{
		if (m_Instance == null)
		{
			GameObject gameObject = new GameObject("TNetManager");
			if (gameObject == null)
			{
				return null;
			}
			m_Instance = gameObject.AddComponent<TNetManager>();
		}
		return m_Instance;
	}

	private void Awake()
	{
		m_Connection = null;
		Object.DontDestroyOnLoad(base.gameObject);
	}

	private void Update()
	{
	}

	private void FixedUpdate()
	{
		if (m_Connection != null)
		{
			m_Connection.Update(Time.deltaTime);
		}
	}

	public void SetCustomMsgFunc(OnCustomMsgFunc func)
	{
		UnityEngine.Debug.Log("SetCustomMsgFunc " + func.ToString());
		m_OnCustomMsgFunc = func;
	}

	public void ClearCustomMsgFunc()
	{
		m_OnCustomMsgFunc = null;
	}

	protected void OnConnectSuccess(TNetEventData tEvent)
	{
		Debug.Log("OnConnectSuccess");
		if (m_OnConnectSuccess != null)
		{
			m_OnConnectSuccess();
		}
	}

	protected void OnConnectKilled(TNetEventData tEvent)
	{
		Debug.Log("OnConnectKilled");
		if (m_OnConnectFailed != null)
		{
			m_OnConnectFailed();
		}
	}

	protected void OnConnectTimeout(TNetEventData tEvent)
	{
		Debug.Log("OnConnectTimeout");
		if (m_OnConnectTimeout != null)
		{
			m_OnConnectTimeout();
		}
	}

	protected void OnConnectDisconnect(TNetEventData tEvent)
	{
		Debug.Log("OnDisConnect");
		if (m_OnConnectDisconnect != null)
		{
			m_OnConnectDisconnect();
		}
	}

	protected void OnLoginSuccess(TNetEventData tEvent)
	{
		Debug.Log("OnLoginSuccess");
		if (m_OnLoginSuccess != null)
		{
			m_OnLoginSuccess();
		}
	}

	protected void OnLoginFailed(TNetEventData tEvent)
	{
		Debug.Log("OnLoginFailed");
		if (m_OnLoginFailed != null)
		{
			m_OnLoginFailed();
		}
	}

	protected void OnHeartTimeout(TNetEventData tEvent)
	{
		Debug.Log("OnHeartTimeout");
	}

	protected void OnHeartWaiting(TNetEventData tEvent)
	{
		Debug.Log("OnHeartWaiting");
	}

	protected void OnHearRenew(TNetEventData tEvent)
	{
		Debug.Log("OnHearRenew");
	}

	protected void OnHearBeat(TNetEventData tEvent)
	{
		UnityEngine.Debug.Log("OnHearBeat servertime = " + tEvent.data["serverTime"]);
	}

	protected void OnRoomList(TNetEventData tEvent)
	{
		ushort num = (ushort)tEvent.data["curPage"];
		ushort num2 = (ushort)tEvent.data["pageSum"];
		RoomDragListCmd.ListType listType = (RoomDragListCmd.ListType)(int)tEvent.data["roomListType"];
		List<TNetRoom> ltRoom = (List<TNetRoom>)tEvent.data["roomList"];
		UnityEngine.Debug.Log("OnRoomList page = " + num + " pagenum = " + num2 + "  listtype = " + listType);
		if (m_OnRoomList != null)
		{
			m_OnRoomList(ltRoom);
		}
	}

	protected void OnJoinRoom(TNetEventData tEvent)
	{
		RoomJoinResCmd.Result result = (RoomJoinResCmd.Result)(int)tEvent.data["result"];
		if (result != 0)
		{
			UnityEngine.Debug.LogError("OnJoinRoom failed result = " + result);
			CGameNetManager.GetInstance().SearchRoom();
			return;
		}
		TNetRoom tNetRoom = (TNetRoom)tEvent.data["room"];
		UnityEngine.Debug.Log("OnJoinRoom " + tNetRoom.Name);
		if (m_OnEnterRoom != null)
		{
			m_OnEnterRoom(tNetRoom);
		}
	}

	protected void OnLeaveRoom(TNetEventData tEvent)
	{
		TNetRoom tNetRoom = (TNetRoom)tEvent.data["room"];
		UnityEngine.Debug.Log("OnLeaveRoom " + tNetRoom.Name);
		if (m_OnLeaveRoom != null)
		{
			m_OnLeaveRoom(tNetRoom);
		}
	}

	protected void OnUserEnterRoom(TNetEventData tEvent)
	{
		Debug.Log("OnUserEnterRoom");
		TNetUser tNetUser = (TNetUser)tEvent.data["user"];
		if (tNetUser != null && m_OnUserEnterRoom != null)
		{
			m_OnUserEnterRoom(tNetUser);
		}
	}

	protected void OnUserExitRoom(TNetEventData tEvent)
	{
		Debug.Log("OnUserExitRoom");
		TNetUser tNetUser = (TNetUser)tEvent.data["user"];
		if (tNetUser != null)
		{
			if (m_OnUserLeaveRoom != null)
			{
				m_OnUserLeaveRoom(tNetUser);
			}
			if (tNetUser.IsItMe && m_OnLeaveRoom != null)
			{
				m_OnLeaveRoom(m_Connection.CurRoom);
			}
		}
	}

	protected void OnCreateRoom(TNetEventData tEvent)
	{
		Debug.Log("OnCreateRoom");
		if ((int)tEvent.data["result"] != 0)
		{
			Debug.LogError("cant create room!");
			return;
		}
		ushort nRoomID = (ushort)tEvent.data["roomId"];
		if (m_OnCreateRoom != null)
		{
			m_OnCreateRoom(nRoomID);
		}
	}

	protected void OnRoomMasterChange(TNetEventData tEvent)
	{
		Debug.Log("OnRoomMasterChange");
		TNetUser roommaster = tEvent.data["user"] as TNetUser;
		if (m_OnRoomMasterChange != null)
		{
			m_OnRoomMasterChange(roommaster);
		}
	}

	protected void OnRoomVarChange(TNetEventData tEvent)
	{
		TNetUser tNetUser = tEvent.data["user"] as TNetUser;
		TNetRoomVarType tNetRoomVarType = (TNetRoomVarType)(int)tEvent.data["key"];
		SFSObject variable = Connection.CurRoom.GetVariable(tNetRoomVarType);
		int @int = variable.GetInt("hunterlvl");
		bool @bool = variable.GetBool("limithunterlvl");
		UnityEngine.Debug.Log(string.Concat("OnRoomVarChange ", tNetUser.Id, " ", tNetUser.Name, " ", tNetRoomVarType, " ", @int, " ", @bool));
	}

	protected void OnRoomStart(TNetEventData tEvent)
	{
	}

	protected void OnCustomMsg(TNetEventData tEvent)
	{
		TNetUser netuser = (TNetUser)tEvent.data["user"];
		SFSObject sFSObject = (SFSObject)tEvent.data["message"];
		kGameNetEnum @int = (kGameNetEnum)sFSObject.GetInt("msghead");
		SFSObject data = (SFSObject)sFSObject.GetSFSObject("data");
		if (m_OnCustomMsgFunc != null)
		{
			m_OnCustomMsgFunc(netuser, @int, data);
		}
	}

	public void Register()
	{
		m_Connection.AddEventListener(TNetEventSystem.CONNECTION, OnConnectSuccess);
		m_Connection.AddEventListener(TNetEventSystem.CONNECTION_KILLED, OnConnectKilled);
		m_Connection.AddEventListener(TNetEventSystem.CONNECTION_TIMEOUT, OnConnectTimeout);
		m_Connection.AddEventListener(TNetEventSystem.DISCONNECT, OnConnectDisconnect);
		m_Connection.AddEventListener(TNetEventSystem.LOGIN, OnLoginSuccess);
		m_Connection.AddEventListener(TNetEventSystem.REVERSE_HEART_TIMEOUT, OnHeartTimeout);
		m_Connection.AddEventListener(TNetEventRoom.GET_ROOM_LIST, OnRoomList);
		m_Connection.AddEventListener(TNetEventRoom.ROOM_CREATION, OnCreateRoom);
		m_Connection.AddEventListener(TNetEventRoom.ROOM_MASTER_CHANGE, OnRoomMasterChange);
		m_Connection.AddEventListener(TNetEventRoom.ROOM_JOIN, OnJoinRoom);
		m_Connection.AddEventListener(TNetEventRoom.USER_ENTER_ROOM, OnUserEnterRoom);
		m_Connection.AddEventListener(TNetEventRoom.USER_EXIT_ROOM, OnUserExitRoom);
		m_Connection.AddEventListener(TNetEventRoom.ROOM_VARIABLES_UPDATE, OnRoomVarChange);
		m_Connection.AddEventListener(TNetEventRoom.ROOM_START, OnRoomStart);
		m_Connection.AddEventListener(TNetEventRoom.OBJECT_MESSAGE, OnCustomMsg);
	}

	public void Unregister()
	{
		m_Connection.RemoveAllEventListeners();
	}

	public void Connect(string sIP, int nPort)
	{
		if (m_Connection == null)
		{
			m_Connection = new TNetObject();
		}
		if (m_Connection.IsContected())
		{
			Debug.Log("connect is alreay worked");
			return;
		}
		m_Connection.Close();
		if (m_Connection.GetStatus() == TNetObject.STATUS.kReady)
		{
			UnityEngine.Debug.Log("Connect " + sIP + " port " + nPort);
			m_Connection.Connect(sIP, nPort);
		}
		Unregister();
		Register();
	}

	public void DisConnect()
	{
		if (m_Connection != null && m_Connection.IsContected())
		{
			m_Connection.Close();
		}
	}

	public void Login(string username, string password = "")
	{
		if (m_Connection != null)
		{
			m_Connection.Send(new LoginRequest(username));
		}
	}

	public void ApplyRoomList(int nGroupID, int nPage, int nPageNum, RoomDragListCmd.ListType ListType)
	{
		if (Connection != null)
		{
			GetRoomListRequest request = new GetRoomListRequest(nGroupID, nPage, nPageNum, ListType);
			Connection.Send(request);
		}
	}

	public void CreateRoom(string sRoomName, string password, int nGroupID, int nMaxPlayer, RoomCreateCmd.RoomType roomlimit, RoomCreateCmd.RoomSwitchMasterType mastertype, string comment = "")
	{
		if (Connection != null)
		{
			CreateRoomRequest request = new CreateRoomRequest(sRoomName, password, nGroupID, nMaxPlayer, roomlimit, mastertype, comment);
			Connection.Send(request);
		}
	}

	public void JoinRoom(int nRoomID, string password = "")
	{
		if (Connection != null)
		{
			JoinRoomRequest request = new JoinRoomRequest(nRoomID, password);
			Connection.Send(request);
		}
	}

	public void LeaveRoom()
	{
		if (Connection != null)
		{
			LeaveRoomRequest request = new LeaveRoomRequest();
			Connection.Send(request);
		}
	}

	public void StartRoom()
	{
		if (Connection != null)
		{
			RoomStartRequest request = new RoomStartRequest();
			Connection.Send(request);
		}
	}

	public void SetRoomVar(TNetRoomVarType key, SFSObject data)
	{
		if (Connection != null)
		{
			SetRoomVariableRequest request = new SetRoomVariableRequest(key, data);
			Connection.Send(request);
		}
	}

	public void SetRoomComment(int roomid, string comment)
	{
		if (Connection != null)
		{
			SetRoomCommentRequest request = new SetRoomCommentRequest(roomid, comment);
			Connection.Send(request);
		}
	}

	public void BroadcastMessage(nmsg_struct msg)
	{
		if (Connection != null)
		{
			SFSObject sFSObject = new SFSObject();
			sFSObject.PutInt("msghead", (int)msg.msghead);
			sFSObject.PutSFSObject("data", msg.Pack());
			BroadcastMessageRequest request = new BroadcastMessageRequest(sFSObject);
			Connection.Send(request);
		}
	}

	public void SendMessage(int nUserID, nmsg_struct msg)
	{
		if (Connection != null)
		{
			SFSObject sFSObject = new SFSObject();
			sFSObject.PutInt("msghead", (int)msg.msghead);
			sFSObject.PutSFSObject("data", msg.Pack());
			ObjectMessageRequest request = new ObjectMessageRequest(nUserID, sFSObject);
			Connection.Send(request);
		}
	}

	public void SendMessage(TNetUser user, nmsg_struct msg)
	{
		SendMessage(user.Id, msg);
	}
}
