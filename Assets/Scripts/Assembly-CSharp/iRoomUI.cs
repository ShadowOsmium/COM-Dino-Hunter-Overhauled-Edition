using System.Collections.Generic;
using TNetSdk;
using UnityEngine;

public class iRoomUI : MonoBehaviour
{
	protected int m_nGameLevel = 1004;

	protected float m_fReadyTime = 5f;

	protected float m_fReadyTimeCount;

	protected int m_nLastReadyTime;

	private void Awake()
	{
	}

	private void Update()
	{
		TNetRoom curRoom = CGameNetManager.GetInstance().GetCurRoom();
		if (curRoom == null || !CGameNetManager.GetInstance().IsRoomMaster() || CGameNetManager.GetInstance().MutiplyState != CGameNetManager.kMutiplyState.ReadyToPlay)
		{
			return;
		}
		if (curRoom.UserCount <= 1)
		{
			Stop();
		}
		m_fReadyTimeCount += Time.deltaTime;
		int num = Mathf.CeilToInt(m_fReadyTime - m_fReadyTimeCount);
		if (m_nLastReadyTime == 0 || m_nLastReadyTime != num)
		{
			m_nLastReadyTime = num;
			iGameApp.GetInstance().ScreenLog(num + "s left...");
		}
		if (m_fReadyTimeCount >= m_fReadyTime)
		{
			CGameNetManager.GetInstance().MutiplyState = CGameNetManager.kMutiplyState.Gaming;
			if (curRoom.RoomMaster.IsItMe)
			{
				iGameApp.GetInstance().ScreenLog("play game !!!");
				CGameNetSender.GetInstance().SendMsg_GAME_ENTER(m_nGameLevel, 1, curRoom);
			}
			else
			{
				iGameApp.GetInstance().ScreenLog("wait room master !!!");
			}
		}
	}

	private void OnGUI()
	{
		TNetRoom curRoom = CGameNetManager.GetInstance().GetCurRoom();
		if (curRoom == null)
		{
			return;
		}
		GUILayout.Label("room: " + curRoom.Name + " roommaster: " + curRoom.RoomMaster.Name);
		Dictionary<int, CGameNetManager.CNetUserInfo> netUserInfoData = CGameNetManager.GetInstance().GetNetUserInfoData();
		if (netUserInfoData == null)
		{
			return;
		}
		if (CGameNetManager.GetInstance().IsRoomMaster())
		{
			GUILayout.BeginHorizontal();
			m_nGameLevel = int.Parse(GUILayout.TextField(m_nGameLevel.ToString(), GUILayout.Width(100f)));
			if (GUILayout.Button("StartGame"))
			{
				StartGame(m_nGameLevel);
			}
			if (GUILayout.Button("Leave"))
			{
				TNetManager.GetInstance().LeaveRoom();
			}
			GUILayout.EndHorizontal();
		}
		foreach (KeyValuePair<int, CGameNetManager.CNetUserInfo> item in netUserInfoData)
		{
			GUILayout.FlexibleSpace();
			int key = item.Key;
			CGameNetManager.CNetUserInfo value = item.Value;
			GUILayout.Label("===== " + value.m_sName + " " + key + " =======");
			GUILayout.Label("hunterlvl: " + value.m_nHunterLvl);
			GUILayout.Label("charid: " + value.m_nCharID);
			GUILayout.Label("charlvl: " + value.m_nCharLvl);
			GUILayout.Label("weaponid: " + value.m_nWeaponID);
			GUILayout.Label("weaponlvl: " + value.m_nWeaponLvl);
		}
	}

	protected void StartGame(int nGameLevel)
	{
		if (iGameApp.GetInstance().m_GameData != null && iGameApp.GetInstance().m_GameData.GetGameLevelInfo(nGameLevel) != null)
		{
			TNetRoom curRoom = CGameNetManager.GetInstance().GetCurRoom();
			if (curRoom != null && curRoom.UserCount > 1)
			{
				CGameNetManager.GetInstance().MutiplyState = CGameNetManager.kMutiplyState.ReadyToPlay;
				m_fReadyTimeCount = 0f;
			}
		}
	}

	protected void Stop()
	{
		CGameNetManager.GetInstance().MutiplyState = CGameNetManager.kMutiplyState.None;
	}
}
