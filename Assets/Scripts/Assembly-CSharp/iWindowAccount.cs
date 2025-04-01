using System.Collections;
using EventCenter;
using LitJson;
using UnityEngine;

public class iWindowAccount : MonoBehaviour
{
	public KeyCode[] m_arrKeyCode;

	public string[] m_arrAccount;

	protected iGameData m_GameData
	{
		get
		{
			return iGameApp.GetInstance().m_GameData;
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

	private void Start()
	{
	}

	private void Update()
	{
		for (int i = 0; i < m_arrKeyCode.Length; i++)
		{
			if (Input.GetKeyDown(m_arrKeyCode[i]) && i < m_arrAccount.Length)
			{
				MyUtils.g_sWindowsAccount = m_arrAccount[i];
				iLoginManager.GetInstance().StartApp(OnLoginSuccess, OnLoginFailed, OnLoginNetError);
			}
		}
		if (Input.GetKeyDown(KeyCode.T))
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("game", "dinohunter");
			hashtable.Add("version", "3.1.7a");
			hashtable.Add("deviceId", MyUtils.GetUUID());
			hashtable.Add("facebookId", string.Empty);
			hashtable.Add("gamecenterId", string.Empty);
			iServerHttp.SendRequest("http://192.225.224.202:8081/gameapi/engine.do", -1f, "dkeoi45793de2k56", "multiCsHandler.enter", JsonMapper.ToJson(hashtable), OnSuccess, OnFailed);
		}
		if (Input.GetKeyDown(KeyCode.K))
		{
			for (int j = 0; j < 200; j++)
			{
				string text = "TA_WIN_" + (j + 1);
				CGameNetManager.GetInstance().UploadRankData(text, text, 1000, 1000 + (j + 1), j + 1, 100, 100, j * 2);
				CNameCardInfo namecardinfo = new CNameCardInfo();
				m_DataCenter.GenerateNameCard(ref namecardinfo);
				namecardinfo.m_sID = text;
				namecardinfo.m_sNickName = text;
				namecardinfo.m_nCombatPower = 1000;
				namecardinfo.m_nHunterLvl = j + 1;
				namecardinfo.m_nGold = 100;
				namecardinfo.m_nCrystal = 100;
				CGameNetManager.GetInstance().UploadNameCard(namecardinfo.m_sID, namecardinfo.m_sNickName, namecardinfo.m_nTitle, namecardinfo.m_NCPack);
			}
		}
		if (Input.GetKeyDown(KeyCode.M) && m_DataCenter != null)
		{
			Debug.Log(iServerSaveData.GetInstance().CurDeviceId + " + " + 100);
			m_DataCenter.HunterExpTotal += 100;
			CGameNetManager.GetInstance().UploadRankData(iServerSaveData.GetInstance().CurDeviceId, m_DataCenter.NickName, m_DataCenter.CombatPower, m_DataCenter.HunterExpTotal, m_DataCenter.HunterLvl, m_DataCenter.Gold, m_DataCenter.Crystal, m_DataCenter.BeAdmire);
			CNameCardInfo namecardinfo2 = new CNameCardInfo();
			m_DataCenter.GenerateNameCard(ref namecardinfo2);
			CGameNetManager.GetInstance().UploadNameCard(namecardinfo2.m_sID, namecardinfo2.m_sNickName, namecardinfo2.m_nTitle, namecardinfo2.m_NCPack);
		}
		if (Input.GetKeyDown(KeyCode.N) && m_DataCenter != null)
		{
			Debug.Log(iServerSaveData.GetInstance().CurDeviceId + " - " + 100);
			m_DataCenter.HunterExpTotal -= 100;
			CGameNetManager.GetInstance().UploadRankData(iServerSaveData.GetInstance().CurDeviceId, m_DataCenter.NickName, m_DataCenter.CombatPower, m_DataCenter.HunterExpTotal, m_DataCenter.HunterLvl, m_DataCenter.Gold, m_DataCenter.Crystal, m_DataCenter.BeAdmire);
			CNameCardInfo namecardinfo3 = new CNameCardInfo();
			m_DataCenter.GenerateNameCard(ref namecardinfo3);
			CGameNetManager.GetInstance().UploadNameCard(namecardinfo3.m_sID, namecardinfo3.m_sNickName, namecardinfo3.m_nTitle, namecardinfo3.m_NCPack);
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			CheckConfig();
		}
	}

	protected void OnSuccess(string response)
	{
		Debug.Log("OnSuccess ");
		Debug.Log(response);
	}

	protected void OnFailed(int result)
	{
		Debug.Log("OnFailed " + result);
	}

	protected void OnLoginSuccess()
	{
		iServerVerify.CServerConfigInfo serverConfigInfo = iServerVerify.GetInstance().GetServerConfigInfo();
		if (serverConfigInfo != null && serverConfigInfo.m_sServerMessage.Length > 0 && iServerSaveData.GetInstance().IsBackgroundRelogin)
		{
			if (!iGameApp.GetInstance().UpgradeVersion("3.1.7a"))
			{
				OnLoginFailed(iLoginManager.kFailedType.Timeout);
				return;
			}
			CMessageBoxScript.GetInstance().MessageBox(serverConfigInfo.m_sServerTitle, serverConfigInfo.m_sServerMessage, null, null, "OK");
			iTrinitiDataCollect.GetInstance().SetUserSymbol(iServerSaveData.GetInstance().CurDeviceId);
			iTrinitiDataCollect.GetInstance().SetUserName(m_DataCenter.NickName);
			if (iServerSaveData.GetInstance().m_bFirstRegister)
			{
				CTrinitiCollectManager.GetInstance().SendRegister();
			}
			CTrinitiCollectManager.GetInstance().SendLogin();
		}
		iGameState gameState = iGameApp.GetInstance().m_GameState;
		if (gameState != null)
		{
			gameState.m_bNeedAutoSaleUI = true;
		}
		iGameData gameData = iGameApp.GetInstance().m_GameData;
		if (gameData != null)
		{
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter != null)
			{
				for (int i = 0; i < m_arrAccount.Length; i++)
				{
					if (!(MyUtils.g_sWindowsAccount == m_arrAccount[i]) && !dataCenter.IsFriend(m_arrAccount[i]))
					{
						dataCenter.AddFriend(m_arrAccount[i]);
					}
				}
			}
		}
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ConnectResult, true));
	}

	protected void OnLoginFailed(iLoginManager.kFailedType type)
	{
		Debug.Log("OnLoginFailed " + type);
		switch (type)
		{
		case iLoginManager.kFailedType.VersionError:
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ConnectResult, false, 2));
			break;
		case iLoginManager.kFailedType.ServerMaintain:
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ConnectResult, false, 5));
			break;
		case iLoginManager.kFailedType.GameCenterChanged:
			CMessageBoxScript.GetInstance().MessageBox(string.Empty, "Game Center ID changed. Please re-login.", OnLoginFailedOnOK, null, "Reconnect");
			break;
		case iLoginManager.kFailedType.GMUsing:
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ConnectResult, false, 4));
			break;
		default:
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ConnectResult, false, 3));
			break;
		}
	}

	protected void OnLoginNetError()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.BackEvent_SceneMain(TUIEvent.SceneMainEventType.TUIEvent_ConnectResult, false, 1));
	}

	protected void OnLoginFailedOnOK()
	{
		iGameApp.GetInstance().EnterScene("Scene_Main");
	}

	protected void CheckConfig()
	{
		iGameData gameData = iGameApp.GetInstance().m_GameData;
		if (gameData == null)
		{
			return;
		}
		Debug.Log("CheckConfig");
		foreach (WaveInfo value in gameData.m_MGCenter.GetData().Values)
		{
			foreach (WaveMobInfo item in value.m_ltWaveMobInfo)
			{
				if (gameData.m_MobCenter.Get(item.nID) == null)
				{
					Debug.LogWarning(value.nID + "'s mob " + item.nID + " is not exist");
				}
			}
		}
	}
}
