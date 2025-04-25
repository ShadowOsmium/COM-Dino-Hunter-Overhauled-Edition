using System.Collections;
using System.Collections.Generic;
using gyAchievementSystem;
using gyEvent;
using gyTaskSystem;
using UnityEngine;

public class iGameSceneBase
{
	public enum kGameStatus
	{
		None,
		CutScene,
		Pause,
		GameBegin,
		Gameing,
		GameLeave,
		Gameing_IAP,
		GameOver_Process,
		GameOver_ShowTime,
		GameOver_Camera,
		GameOver,
		GameTutorial
	}

	public enum kGameOverUIStatus
	{
		None,
		Win,
		Win_Mutiply,
		Fail,
		Fail_Mutiply,
		LvlUp,
		Material
	}

	protected class MonsterNumInfo : MonsterNumLimitInfo
	{
		public int curNum;

		public MonsterNumInfo()
		{
			curNum = 0;
		}

		public bool IsMax()
		{
			return curNum >= nMax;
		}
	}

	protected class CPointScreenTip
	{
		public GameObject agent;

		public gyUIScreenTip screentip;
	}

	protected enum kAssistAimState
	{
		None,
		Normal,
		Proccess
	}

	protected class CWorldMonster
	{
		public int nMobID;

		public float fRefreshTime;
	}

	protected kGameStatus m_LastStatus;

	protected kGameStatus m_Status;

	protected float m_StatusTime;

	protected float m_StatusTimeCount;

	protected bool m_bMissionSuccess;

	protected kGameOverUIStatus m_GameOverUIStatus;

	public kGameSceneEnum m_LeaveGameScene;

	protected iGameState m_GameState;

	protected iGameData m_GameData;

	protected iGameLogic m_GameLogic;

	protected iGameUIBase m_GameUI;

	protected bool m_bWaitingRevive;

	protected float m_fWaitingReviveTime;

	protected float m_fWaitingReviveTimeCount;

	protected bool m_bUserDeath;

	protected float m_fUserDeathTime;

	protected float m_fUserDeathTimeCount;

	protected bool m_bPause;

	protected GameLevelInfo m_curGameLevelInfo;

	protected bool m_bCG;

	protected List<int> m_ltCGWave;

	protected bool m_bTutorialStage;

	protected CPathWalkerManager m_PathWalkerManager;

	protected bool m_bIsSkyScene;

	protected CControlBase m_Input;

	protected CCharUser m_User;

	protected Vector3 m_v3BirthPos;

	protected Vector3 m_v3BirthDir;

	protected iCameraTrail m_CameraTrail;

	protected iCameraReveal m_CameraReveal;

	protected iCameraFocus m_CameraFocus;

	protected Dictionary<int, CCharMob> m_MobMap;

	protected Dictionary<int, int> m_dictWaveMobNumber;

	protected Dictionary<int, int> m_dictStealItem;

	protected Dictionary<int, CCharPlayer> m_PlayerMap;

	protected List<GameObject> m_ltItem;

	protected List<GameObject> m_ltSceneGameObject;

	public float m_fNavPlane;

	protected UnityEngine.AI.NavMeshPath m_NavPath;

	protected List<MonsterNumInfo> m_ltMonsterNumInfo;

	protected CStartPointManager m_curBPManager;

	protected Dictionary<int, CStartPointManager> m_dictSPManager;

	protected Dictionary<int, CStartPointManager> m_dictHPManager;

	protected CStartPointManager m_curSPManagerGround;

	protected CStartPointManager m_curSPManagerSky;

	protected CStartPointManager m_curHPManager;

	protected CStartPointManager m_curTriggerManagerBegin;

	protected CStartPointManager m_curTriggerManagerEnd;

	protected List<CPointScreenTip> m_ltScreenTipTriggerEnd;

	public CMonsterGenerateManager m_MGManager;

	public CEventManager m_EventManager;

	public CTaskManager m_TaskManager;

	public int m_nCurTaskID;

	protected kAssistAimState m_AssistAimState;

	protected CCharBase m_AssistTarget;

	protected Transform m_AssistBone;

	protected float m_fAssistClosest;

	protected Vector2 m_v2AssistAimLastPos;

	protected float m_fAssistAimCount;

	protected float m_fAssistAimRate;

	protected iBuilding m_Building;

	protected float m_fCurEggLife;

	protected float m_fMaxEggLife;

	protected List<CWorldMonster> m_ltRefreshWorldMonster;

	public float m_fCombatRatingData_DamageTotal;

	public float m_fCombatRatingData_DamageMy;

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

	public bool isWaitingRevive
	{
		get
		{
			return m_bWaitingRevive;
		}
		set
		{
			m_bWaitingRevive = value;
		}
	}

	public bool m_bObserve { get; private set; }

	public bool isTutorialStage
	{
		get
		{
			return m_bTutorialStage;
		}
		set
		{
			m_bTutorialStage = value;
		}
	}

	public int m_nBlackMonsterCount { get; set; }

	public bool m_bMutiplyGame { get; private set; }

	public CHunterLevelInfo m_curHunterLevelInfo { get; private set; }

	public GameLevelInfo CurGameLevelInfo
	{
		get
		{
			return m_curGameLevelInfo;
		}
	}

	public iBuilding CurBuilding
	{
		get
		{
			return m_Building;
		}
	}

	public float CurEggLife
	{
		get
		{
			return m_fCurEggLife;
		}
	}

	public float MaxEggLife
	{
		get
		{
			return m_fMaxEggLife;
		}
	}

	public kGameStatus GameStatus
	{
		get
		{
			return m_Status;
		}
	}

	public kGameOverUIStatus GameOverUIStatus
	{
		get
		{
			return m_GameOverUIStatus;
		}
	}

	public float GameStatusStepTime
	{
		get
		{
			return m_StatusTime;
		}
		set
		{
			m_StatusTime = value;
			m_StatusTimeCount = 0f;
		}
	}

	public bool isMissionSuccess
	{
		get
		{
			return m_bMissionSuccess;
		}
	}

	public bool isPause
	{
		get
		{
			return m_bPause;
		}
	}

	public bool IsWorldMonster(int nMobID)
	{
		foreach (CWorldMonster item in m_ltRefreshWorldMonster)
		{
			if (item.nMobID == nMobID)
			{
				return true;
			}
		}
		return false;
	}

    public virtual void Initialize()
    {
        m_GameState = iGameApp.GetInstance().m_GameState;
        m_GameData = iGameApp.GetInstance().m_GameData;

        // Safely find and initialize Game UI
        if (m_GameUI == null)
        {
            GameObject gameObject = GameObject.Find("Game");
            if (gameObject == null)
            {
                Debug.LogError("GameObject named 'Game' not found in scene. Make sure it exists.");
                return;
            }

            m_GameUI = gameObject.GetComponent<iGameUIBase>();
            if (m_GameUI == null)
            {
                Debug.LogError("Component 'iGameUIBase' not found on 'Game' object.");
                return;
            }

            m_GameUI.Initialize();
        }

        // Game Logic
        if (m_GameLogic == null)
        {
            m_GameLogic = new iGameLogic();
            m_GameLogic.Initialize();
        }

        // Camera components (safe check on Camera.main)
        if (Camera.main == null)
        {
            Debug.LogError("Main camera not found. Make sure your main camera is tagged as 'MainCamera'.");
            return;
        }

        // Initialize camera components safely
        m_CameraTrail = GetOrAddComponent<iCameraTrail>(Camera.main.gameObject);
        m_CameraReveal = GetOrAddComponent<iCameraReveal>(Camera.main.gameObject);
        m_CameraFocus = GetOrAddComponent<iCameraFocus>(Camera.main.gameObject);

        // Other data structures
        if (m_MobMap == null) m_MobMap = new Dictionary<int, CCharMob>();
        if (m_PathWalkerManager == null) m_PathWalkerManager = new CPathWalkerManager();
        if (m_dictWaveMobNumber == null) m_dictWaveMobNumber = new Dictionary<int, int>();
        if (m_dictStealItem == null) m_dictStealItem = new Dictionary<int, int>();
        if (m_PlayerMap == null) m_PlayerMap = new Dictionary<int, CCharPlayer>();
        if (m_ltMonsterNumInfo == null) m_ltMonsterNumInfo = new List<MonsterNumInfo>();
        if (m_dictSPManager == null) m_dictSPManager = new Dictionary<int, CStartPointManager>();
        if (m_dictHPManager == null) m_dictHPManager = new Dictionary<int, CStartPointManager>();
        if (m_EventManager == null) m_EventManager = new CEventManager();
        if (m_MGManager == null) m_MGManager = new CMonsterGenerateManager();

        if (m_TaskManager == null)
        {
            m_TaskManager = new CTaskManager();
            m_TaskManager.Initialize();
        }

        if (m_ltScreenTipTriggerEnd == null) m_ltScreenTipTriggerEnd = new List<CPointScreenTip>();
        if (m_ltItem == null) m_ltItem = new List<GameObject>();
        if (m_ltSceneGameObject == null) m_ltSceneGameObject = new List<GameObject>();

        // Platform-specific input
        if (m_Input == null)
        {
            if (MyUtils.isWindows) m_Input = new CControlWindows();
            else if (MyUtils.isIOS || MyUtils.isAndroid) m_Input = new CControlIphone();
        }

        if (m_ltCGWave == null) m_ltCGWave = new List<int>();
        if (m_NavPath == null) m_NavPath = new UnityEngine.AI.NavMeshPath();

        m_ltRefreshWorldMonster = new List<CWorldMonster>();
        m_nBlackMonsterCount = 0;
    }

    // Helper for getting or adding components
    private T GetOrAddComponent<T>(GameObject obj) where T : Component
    {
        T comp = obj.GetComponent<T>();
        if (comp == null)
        {
            comp = obj.AddComponent<T>();
        }
        return comp;
    }

    public void InitializeGameLevel(int nLevel)
	{
		m_curGameLevelInfo = m_GameData.GetGameLevelInfo(nLevel);
		if (m_curGameLevelInfo == null)
		{
			return;
		}
		m_bIsSkyScene = m_curGameLevelInfo.bIsSkyScene;
		m_fNavPlane = m_curGameLevelInfo.fNavPlane;
		m_curBPManager = new CStartPointManager();
		m_curBPManager.Load("_Config/_StartPoint/StartPoint_" + m_curGameLevelInfo.nBirthPos);
		CStartPoint random = m_curBPManager.GetRandom();
		if (random != null)
		{
			m_v3BirthPos = random.GetRandom2D();
		}
		int nDefaultSPSky = m_curGameLevelInfo.nDefaultSPSky;
		CStartPointManager cStartPointManager = new CStartPointManager();
		if (cStartPointManager.Load("_Config/_StartPoint/StartPoint_" + nDefaultSPSky))
		{
			m_curSPManagerSky = cStartPointManager;
			if (!m_dictSPManager.ContainsKey(nDefaultSPSky))
			{
				m_dictSPManager.Add(nDefaultSPSky, cStartPointManager);
			}
		}
		nDefaultSPSky = m_curGameLevelInfo.nDefaultSPGround;
		cStartPointManager = new CStartPointManager();
		if (cStartPointManager.Load("_Config/_StartPoint/StartPoint_" + nDefaultSPSky))
		{
			m_curSPManagerGround = cStartPointManager;
			if (!m_dictSPManager.ContainsKey(nDefaultSPSky))
			{
				m_dictSPManager.Add(nDefaultSPSky, cStartPointManager);
			}
		}
		int nDefaultHoverPoint = m_curGameLevelInfo.nDefaultHoverPoint;
		CStartPointManager cStartPointManager2 = new CStartPointManager();
		cStartPointManager2.Load("_Config/_HoverPoint/HoverPoint_" + nDefaultHoverPoint);
		m_curHPManager = cStartPointManager2;
		if (!m_dictHPManager.ContainsKey(nDefaultHoverPoint))
		{
			m_dictHPManager.Add(nDefaultHoverPoint, cStartPointManager2);
		}
		if (m_curGameLevelInfo.ltGameWave.Count > 0)
		{
			m_MGManager.RegisterWaveID(m_curGameLevelInfo.ltGameWave.ToArray());
		}
		if (m_curGameLevelInfo.nTPBeginCfg > 0)
		{
			m_curTriggerManagerBegin = new CStartPointManager();
			m_curTriggerManagerBegin.Load("_Config/_TriggerPoint/TriggerPoint_" + m_curGameLevelInfo.nTPBeginCfg);
		}
		if (m_curGameLevelInfo.nTPEndCfg > 0)
		{
			m_curTriggerManagerEnd = new CStartPointManager();
			m_curTriggerManagerEnd.Load("_Config/_TriggerPoint/TriggerPoint_" + m_curGameLevelInfo.nTPEndCfg);
		}
		if (m_curGameLevelInfo.ltMonsterNumLimit.Count > 0)
		{
			foreach (MonsterNumLimitInfo item in m_curGameLevelInfo.ltMonsterNumLimit)
			{
				MonsterNumInfo monsterNumInfo = new MonsterNumInfo();
				monsterNumInfo.nLimitType = item.nLimitType;
				monsterNumInfo.nLimitValue = item.nLimitValue;
				monsterNumInfo.nMax = item.nMax;
				UnityEngine.Debug.Log(monsterNumInfo.nLimitType + " " + monsterNumInfo.nMax);
				m_ltMonsterNumInfo.Add(monsterNumInfo);
			}
		}
		MyUtils.UIDCount = 0;
		m_TaskManager.AddTask(m_curGameLevelInfo.nTaskID);
		m_nCurTaskID = m_curGameLevelInfo.nTaskID;
		m_GameUI.InitTaskUI();
		if (m_curGameLevelInfo.sCutScene.Length > 0)
		{
			CCameraRoam.GetInstance().PreLoadCG(m_curGameLevelInfo.sCutScene);
		}
		if (m_curGameLevelInfo.sCutSceneContent.Length > 0)
		{
			CCameraRoam.GetInstance().PreLoadCG(m_curGameLevelInfo.sCutSceneContent);
		}
		if (m_curGameLevelInfo.ltGameWave.Count <= 0)
		{
			return;
		}
		WaveInfo waveInfo = null;
		foreach (int item2 in m_curGameLevelInfo.ltGameWave)
		{
			waveInfo = m_GameData.GetWaveInfo(item2);
			if (waveInfo != null)
			{
				if (waveInfo.sCutScene.Length > 0)
				{
					CCameraRoam.GetInstance().PreLoadCG(waveInfo.sCutScene);
				}
				if (waveInfo.sCutSceneContent.Length > 0)
				{
					CCameraRoam.GetInstance().PreLoadCG(waveInfo.sCutSceneContent);
				}
			}
		}
	}

	public void InitializeHunterLevel(int nHunterLevel)
	{
		if (m_GameData.m_HunterLevelCenter != null)
		{
			m_curHunterLevelInfo = m_GameData.m_HunterLevelCenter.Get(nHunterLevel);
		}
	}

	public virtual void Reset()
	{
		ClearNPC();
		ClearPlayer();
		if (m_TaskManager != null)
		{
			m_TaskManager.Reset();
		}
		if (m_MGManager != null)
		{
			m_MGManager.Reset();
		}
		if (m_GameUI != null)
		{
			m_GameUI.Reset();
		}
		foreach (GameObject item in m_ltItem)
		{
			if (item != null)
			{
				Object.Destroy(item);
			}
		}
		m_ltItem.Clear();
		foreach (GameObject item2 in m_ltSceneGameObject)
		{
			if (item2 != null)
			{
				Object.Destroy(item2);
			}
		}
		m_ltSceneGameObject.Clear();
	}

	public virtual void Destroy()
	{
		if (m_User != null)
		{
			m_User.Destroy();
			m_User = null;
		}
		ClearNPC();
		ClearPlayer();
		if (m_GameUI != null)
		{
			m_GameUI.Destroy();
		}
		if (m_ltScreenTipTriggerEnd != null)
		{
			foreach (CPointScreenTip item in m_ltScreenTipTriggerEnd)
			{
				if (item.agent != null)
				{
					Object.Destroy(item.agent);
				}
				if (item.screentip != null)
				{
					item.screentip.gameObject.SetActiveRecursively(false);
				}
			}
			m_ltScreenTipTriggerEnd.Clear();
		}
		CSoundScene.GetInstance().Destroy();
	}

	public virtual void ClearNPC()
	{
		if (m_MobMap != null)
		{
			foreach (CCharMob value in m_MobMap.Values)
			{
				value.Destroy();
			}
			m_MobMap.Clear();
		}
		m_dictWaveMobNumber.Clear();
		m_ltMonsterNumInfo.Clear();
		m_nBlackMonsterCount = 0;
	}

	public virtual void ClearPlayer()
	{
		if (m_PlayerMap == null)
		{
			return;
		}
		foreach (CCharPlayer value in m_PlayerMap.Values)
		{
			if (!(value == m_User))
			{
				value.Destroy();
			}
		}
		m_PlayerMap.Clear();
		if (m_User != null)
		{
			m_PlayerMap.Add(m_User.UID, m_User);
		}
	}

	protected void StartCG()
	{
		m_GameUI.MovieUIIn(1f);
		m_GameUI.HideGameUI();
		m_GameUI.ShowSkipUI(true);
	}

	protected void FinishCG()
	{
		m_GameUI.MovieUIOut(1f);
		m_GameUI.ShowGameUI();
		m_GameUI.ShowSkipUI(false);
		StartGame();
	}

	public virtual void StartGame()
	{
		if (m_curGameLevelInfo == null)
		{
			return;
		}
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		if (TNetManager.GetInstance().Connection == null)
		{
			m_bTutorialStage = iGameApp.GetInstance().IsTutorialStage(m_curGameLevelInfo.nID);
			dataCenter.LastLevel = m_curGameLevelInfo.nID;
			if (!dataCenter.IsLevelIgnoreCG(m_curGameLevelInfo.nID))
			{
				dataCenter.SetLevelIgnoreCG(m_curGameLevelInfo.nID, true);
				if (!m_bCG)
				{
					m_bCG = true;
					CCameraRoam.CCGInfo cCGInfo = new CCameraRoam.CCGInfo();
					cCGInfo.sCG = m_curGameLevelInfo.sCutScene;
					cCGInfo.sCGContent = m_curGameLevelInfo.sCutSceneContent;
					cCGInfo.sCGAmbience = m_curGameLevelInfo.sCutSceneAmbience;
					cCGInfo.sCGBGM = string.Empty;
					if (CCameraRoam.GetInstance().Start(Camera.main, cCGInfo, StartCG, FinishCG))
					{
						m_Status = kGameStatus.CutScene;
						return;
					}
				}
			}
		}
		CCharSaveInfo character = dataCenter.GetCharacter(dataCenter.CurCharID);
		if (character == null || character.nLevel < 0)
		{
			return;
		}
		CCharacterInfoLevel characterInfo = m_GameData.GetCharacterInfo(character.nID, character.nLevel);
		if (characterInfo == null)
		{
			return;
		}
		int nUID = 0;
		if (TNetManager.GetInstance().Connection != null)
		{
			CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo(TNetManager.GetInstance().Connection.Myself.Id);
			if (netUserInfo != null)
			{
				nUID = netUserInfo.m_nUID;
				m_v3BirthPos = netUserInfo.m_v3Pos;
				m_v3BirthDir = netUserInfo.m_v3Dir;
			}
		}
		else
		{
			m_v3BirthDir = -m_v3BirthPos;
		}
		if (m_User == null)
		{
			m_User = AddUser(characterInfo.nModel, nUID, m_v3BirthPos, m_v3BirthDir);
			if (m_User == null)
			{
				return;
			}
		}
		m_User.isDead = false;
		m_User.SetRotateLimit(-10f, 60f, 0f, 0f);
		m_User.SetBehavior(0);
		UnityEngine.Debug.Log("user level = " + character.nLevel);
		for (int i = 0; i < 3; i++)
		{
			int nID = 0;
			int nLevel = 0;
			if (m_GameState.GetCarryPassiveSkill(i, ref nID, ref nLevel))
			{
				m_User.CarryPassiveSkill(i, nID, nLevel);
			}
		}
		m_User.InitChar(character.nID, character.nLevel, character.nExp, dataCenter.AvatarHead, dataCenter.AvatarUpper, dataCenter.AvatarLower, dataCenter.AvatarHeadup, dataCenter.AvatarNeck, dataCenter.AvatarWrist, dataCenter.AvatarBadge, dataCenter.AvatarStone);
		if (m_User.Property != null)
		{
			if (m_bIsSkyScene)
			{
				m_User.Property.SetValueBase(kProEnum.MoveSpeed, 10f);
				m_User.Property.SetValueBase(kProEnum.MoveSpeedAcc, 10f);
			}
			else
			{
				m_User.Property.SetValueBase(kProEnum.MoveSpeed, 6f);
				m_User.Property.SetValueBase(kProEnum.MoveSpeedAcc, 6f);
			}
		}
		bool flag = false;
		if (!CurGameLevelInfo.m_bLimitMelee)
		{
			for (int j = 0; j < 3; j++)
			{
				CWeaponBase weapon = m_GameState.GetWeapon(j);
				if (weapon != null && weapon.CurWeaponLvlInfo != null && weapon.CurWeaponLvlInfo.nType != 1)
				{
					m_User.SwitchWeapon(j);
					flag = true;
					break;
				}
			}
		}
		if (!flag)
		{
			m_User.SwitchWeapon(0);
		}
		m_User.SetMoveMode(m_bIsSkyScene ? kCharMoveMode.Fly : kCharMoveMode.Ground);
		if (m_GameUI != null)
		{
			if (dataCenter.NickName.Length > 0)
			{
				UnityEngine.Debug.Log(dataCenter.NickName);
				m_GameUI.SetProtraitName(dataCenter.NickName, m_User.UID);
			}
			m_GameUI.SetProtraitLevel(character.nLevel.ToString(), m_User.UID);
			m_GameUI.SetProtraitExp(0, (float)character.nExp / (float)characterInfo.nExp, m_User.UID);
		}
		if (TNetManager.GetInstance().Connection != null)
		{
			Dictionary<int, CGameNetManager.CNetUserInfo> netUserInfoData = CGameNetManager.GetInstance().GetNetUserInfoData();
			if (netUserInfoData != null)
			{
				foreach (KeyValuePair<int, CGameNetManager.CNetUserInfo> item in netUserInfoData)
				{
					int key = item.Key;
					CGameNetManager.CNetUserInfo value = item.Value;
					if (key == TNetManager.GetInstance().Connection.Myself.Id)
					{
						continue;
					}
					CCharPlayer cCharPlayer = AddPlayer(value.m_nCharID, value.m_nUID, value.m_v3Pos, value.m_v3Dir);
					if (cCharPlayer == null)
					{
						continue;
					}
					cCharPlayer.SetBehavior(100);
					cCharPlayer.InitChar(value.m_nCharID, value.m_nCharLvl, 1, value.m_nAvatarHead, value.m_nAvatarUpper, value.m_nAvatarLower, value.m_nAvatarHeadup, value.m_nAvatarNeck, value.m_nAvatarBracelet, value.m_nAvatarBadge, value.m_nAvatarStone);
					cCharPlayer.EquipWeapon(value.m_nWeaponID, value.m_nWeaponLvl);
					cCharPlayer.SetMoveMode(m_bIsSkyScene ? kCharMoveMode.Fly : kCharMoveMode.Ground);
					cCharPlayer.SetPlayerHUD(value.m_sName);
					if (m_GameUI != null)
					{
						m_GameUI.AddTeamMateProtrait(cCharPlayer.UID);
						m_GameUI.SetProtraitName(value.m_sName, cCharPlayer.UID);
						m_GameUI.SetProtraitLevel(value.m_nCharLvl.ToString(), cCharPlayer.UID);
						m_GameUI.SetProtraitLife(1f, cCharPlayer.UID);
						if (cCharPlayer.CurCharInfoLevel != null)
						{
							m_GameUI.SetProtraitIcon(cCharPlayer.CurCharInfoLevel.sIcon, cCharPlayer.UID);
						}
					}
				}
			}
		}
		CSoundScene.GetInstance().PlayBGM(GetBGM());
		CSoundScene.GetInstance().PlayAmbienceBGM(m_curGameLevelInfo.sBGMAmbience);
		m_CameraTrail.Initialize(m_User, !flag);
		m_CameraTrail.SetRotateLimit(-10f, 60f, 0f, 0f);
		m_CameraTrail.Active = true;
		m_GameState.nLastLevel = character.nLevel;
		m_Input.Initialize();
		InitTask(m_nCurTaskID);
		m_TaskManager.Start();
		m_MGManager.Start();
		m_ltRefreshWorldMonster.Clear();
		if (!m_bIsSkyScene && !isTutorialStage)
		{
			iServerVerify.CServerConfigInfo serverConfigInfo = iServerVerify.GetInstance().GetServerConfigInfo();
			if (serverConfigInfo != null && serverConfigInfo.m_ltWorldMonsterInfo != null)
			{
				CTaskInfo taskInfo = m_GameData.GetTaskInfo(m_nCurTaskID);
				foreach (iServerVerify.CServerConfigInfo.CWorldMonsterInfo item2 in serverConfigInfo.m_ltWorldMonsterInfo)
				{
					if ((taskInfo == null || !item2.m_ltTaskTypeLimit.Contains(taskInfo.nType)) && dataCenter.GetWorldMonsterKill(item2.m_nMobID) < item2.m_nDailyMax)
					{
						int num = Random.Range(0, 100000000);
						if ((float)num <= item2.m_fRate * 1000000f)
						{
							CWorldMonster cWorldMonster = new CWorldMonster();
							cWorldMonster.nMobID = item2.m_nMobID;
							cWorldMonster.fRefreshTime = Random.Range(item2.m_arrRefreshTime[0], item2.m_arrRefreshTime[1]);
							m_ltRefreshWorldMonster.Add(cWorldMonster);
						}
					}
				}
			}
		}
		m_Status = kGameStatus.GameBegin;
		m_StatusTime = 1f;
		m_StatusTimeCount = 0f;
		m_bMissionSuccess = false;
		PrefabLoadResource();
		iGameApp.GetInstance().Flurry_EnterStage(m_curGameLevelInfo.nID);
		m_GameUI.HideGameUI();
		m_GameUI.ShowGameUI();
		if (!dataCenter.isTutorial)
		{
			StartTutorial();
		}
		else
		{
			m_GameUI.FadeIn(1f);
			SetPause(false);
		}
		if (CGameNetManager.GetInstance().IsConnected())
		{
			m_bMutiplyGame = true;
			InitializeHunterLevel(m_GameState.m_nCurHunterLevelID);
			m_GameState.m_nCurHunterLevelID = 0;
			CGameNetManager.GetInstance().IsGaming = false;
			CGameNetSender.GetInstance().SendMsg_Game_START_REQUEST();
			CTrinitiCollectManager.GetInstance().SendChalleageBoss();
		}
		else
		{
			m_bMutiplyGame = false;
		}
		AndroidReturnPlugin.instance.SetIngame(true);
		AndroidReturnPlugin.instance.SetIngameMutiply(m_bMutiplyGame);
		AndroidReturnPlugin.instance.SetIngamePause(m_GameUI.Event_Pause);
		AndroidReturnPlugin.instance.SetIngameContinue(m_GameUI.Event_Pause_Close);
		AndroidReturnPlugin.instance.SetIngamePause(false);
	}

	private string GetBGM()
	{
		switch (Application.loadedLevelName)
		{
			case "SceneLava":
			case "SceneLava2":
				return "BGM_Map03";

			case "SceneIce":
			case "SceneSnow":
				return "BGM_Map02";

			default:
				return "BGM_Map01";
		}
	}

	public void RestartGame()
	{
		bool isDead = m_User.isDead;
		m_GameState.Reset();
		Reset();
		StartGame();
		if (isDead)
		{
			m_User.Revive(m_User.MaxHP);
			if (m_bIsSkyScene && CurGameLevelInfo != null)
			{
				Vector3 pos = m_User.Pos;
				pos.y = CurGameLevelInfo.fNavPlane;
				m_User.Pos = pos;
			}
		}
		m_CameraTrail.Active = true;
		m_CameraFocus.Active = false;
		m_CameraReveal.Active = false;
		m_User.ResetSkillCD();
	}

	public void StartTutorial()
	{
		m_LastStatus = m_Status;
		m_Status = kGameStatus.GameTutorial;
		m_GameUI.FadeIn(0f);
		m_GameUI.ShowTutorials(true);
		SetPause(true);
	}

	public void FinishTutorial()
	{
		if (m_LastStatus != 0)
		{
			m_Status = m_LastStatus;
		}
		m_GameUI.FadeIn(1f);
		m_GameUI.ShowTutorials(false);
		SetPause(false);
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter != null)
		{
			dataCenter.isTutorial = true;
			dataCenter.Save();
		}
	}

	public void PrefabLoadResource()
	{
		List<WaveInfo> waveList = m_MGManager.GetWaveList();
		if (waveList != null)
		{
			int num = 0;
			WaveMobInfo waveMobInfo = null;
			CMobInfoLevel cMobInfoLevel = null;
			foreach (WaveInfo item in waveList)
			{
				num = item.GetWaveMobInfoCount();
				for (int i = 0; i < num; i++)
				{
					waveMobInfo = item.GetWaveMobInfo(i);
					if (waveMobInfo != null)
					{
						cMobInfoLevel = m_GameData.GetMobInfo(waveMobInfo.nID, waveMobInfo.nLevel);
						if (cMobInfoLevel != null)
						{
							PrefabManager.Get(cMobInfoLevel.nModel);
						}
					}
				}
			}
		}
		CWeaponBase cWeaponBase = null;
		for (int j = 0; j < 3; j++)
		{
			cWeaponBase = m_GameState.GetWeapon(j);
			if (cWeaponBase != null && cWeaponBase.CurWeaponLvlInfo != null)
			{
				if (cWeaponBase.CurWeaponLvlInfo.nFire != -1)
				{
					PrefabManager.AddPool(cWeaponBase.CurWeaponLvlInfo.nFire, 5);
				}
				if (cWeaponBase.CurWeaponLvlInfo.nHit != -1)
				{
					PrefabManager.AddPool(cWeaponBase.CurWeaponLvlInfo.nHit, 5);
				}
				if (cWeaponBase.CurWeaponLvlInfo.nBullet != -1)
				{
					PrefabManager.AddPool(cWeaponBase.CurWeaponLvlInfo.nBullet, 5);
				}
			}
		}
		if (m_User != null)
		{
			CSkillInfoLevel skillInfo = m_GameData.GetSkillInfo(m_User.SkillID, 1);
			if (skillInfo != null)
			{
				CBuffInfo cBuffInfo = null;
				for (int k = 0; k < 3; k++)
				{
					if (skillInfo.arrFunc[k] == 3)
					{
						cBuffInfo = m_GameData.GetBuffInfo(skillInfo.arrValueX[k]);
						if (cBuffInfo != null)
						{
							PrefabManager.Get(cBuffInfo.arrEffAdd[0]);
							PrefabManager.Get(cBuffInfo.arrEffDel[0]);
							PrefabManager.Get(cBuffInfo.arrEffHold[0]);
						}
					}
				}
			}
		}
		PrefabManager.AddPool(1115, 10);
	}

	public virtual void FinishGame(bool bSuccess, int nLastKillBoss = -1)
	{
		m_Status = kGameStatus.GameOver_Process;
		m_StatusTime = 2f;
		m_StatusTimeCount = 0f;
		m_CameraTrail.Active = false;
		m_CameraReveal.Active = false;
		m_CameraFocus.Active = false;
		m_bMissionSuccess = bSuccess;
		if (nLastKillBoss > 0)
		{
			m_GameState.LastKillBoss = nLastKillBoss;
		}
		CSoundScene.GetInstance().StopBGM();
		if (m_bMissionSuccess)
		{
			CTaskBase task = m_TaskManager.GetTask();
			if (task != null)
			{
				CTaskInfo taskInfo = task.GetTaskInfo();
				if (taskInfo != null && taskInfo.nType == 2 && m_GameState.LastKillBoss != -1)
				{
					CCharMob mob = GetMob(m_GameState.LastKillBoss);
					UnityEngine.Debug.Log(m_GameState.LastKillBoss + " " + mob);
					if (mob != null)
					{
						m_CameraFocus.Go(mob.GetBone(1).gameObject, 8f, 1.5f);
						m_StatusTime = mob.GetActionLen(kAnimEnum.Mob_Dead);
						Time.timeScale = 0.4f;
					}
					CSoundScene.GetInstance().PlayBGM("Amb_Cheer_Crowd");
					m_GameState.LastKillBoss = -1;
				}
			}
		}
		else
		{
			m_CameraReveal.Go(m_User.GetBone(1).gameObject, 70f, 5f, 5f);
			Time.timeScale = 0.4f;
		}
		m_User.SetFire(false);
		m_User.MoveStop();
		if (MyUtils.isWindows)
		{
			//Screen.lockCursor = false;
		}
	}

	public virtual void GameOver(bool bSuccess)
	{
		m_Status = kGameStatus.GameOver_Camera;
		m_StatusTimeCount = 0f;
		m_CameraTrail.Active = false;
		m_CameraFocus.Active = false;
		m_CameraReveal.Active = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        foreach (CCharMob value2 in m_MobMap.Values)
		{
			value2.SetActive(false);
		}
		m_GameUI.HideGameUI();
		if (bSuccess)
		{
			m_StatusTime = 5f;
			m_CameraReveal.Go(m_User.GetBone(1).gameObject, -20f, 2f, 10f);
			m_User.CrossAnim(kAnimEnum.VictoryIdle, WrapMode.Loop, 0.3f, 1f, 0f);
			m_User.SetActionLayer(kAnimEnum.VictoryIdle, -1);
			m_User.CrossAnim(kAnimEnum.Victory, WrapMode.Once, 0.3f, 1f, 0f);
			if (m_bMutiplyGame)
			{
				if (!m_User.isDead)
				{
					if (m_GameData.m_HunterLevelCenter != null && m_curHunterLevelInfo != null)
					{
						CHunterLevelInfo cHunterLevelInfo = m_GameData.m_HunterLevelCenter.Get(m_curHunterLevelInfo.m_nID);
						if (cHunterLevelInfo != null)
						{
							m_GameState.m_nLevelRewardGold = cHunterLevelInfo.m_nGold;
							m_GameState.m_nLevelRewardExp = cHunterLevelInfo.m_nExp;
							m_GameState.m_nLevelHunterExp = cHunterLevelInfo.m_nHunterExpWin;
							float value = m_User.Property.GetValue(kProEnum.Char_IncreaseGold);
							if (value > 0f)
							{
								m_GameState.m_nLevelRewardGold = (int)((float)m_GameState.m_nLevelRewardGold * (1f + value / 50f));
							}
							value = m_User.Property.GetValue(kProEnum.Char_IncreaseExp);
							if (value > 0f)
							{
								m_GameState.m_nLevelRewardExp = (int)((float)m_GameState.m_nLevelRewardExp * (1f + value / 50f));
							}
						}
					}
					if (m_GameData.m_HunterCenter != null)
					{
						CHunterInfo cHunterInfo = m_GameData.m_HunterCenter.Get(m_DataCenter.HunterLvl - 1);
						if (cHunterInfo != null && m_GameState.m_nLevelHunterExp > cHunterInfo.m_nHunterExpWin)
						{
							m_GameState.m_nLevelHunterExp = cHunterInfo.m_nHunterExpWin;
						}
					}
				}
				else
				{
					m_GameState.m_nLevelRewardGold = 0;
					m_GameState.m_nLevelRewardExp = 0;
					m_GameState.m_nLevelHunterExp = 0;
				}
			}
			else if (m_curGameLevelInfo != null)
			{
				m_GameState.m_nLevelRewardGold = m_curGameLevelInfo.nRewardGold;
				m_GameState.m_nLevelRewardExp = m_curGameLevelInfo.nRewardExp;
				if (m_DataCenter != null)
				{
					if (m_curGameLevelInfo.nID == m_DataCenter.LatestLevel)
					{
						m_DataCenter.UnlockNewLevelPrepare();
					}
					if (!m_DataCenter.IsLevelPassed(m_curGameLevelInfo.nID))
					{
						m_DataCenter.SetPassedLevel(m_curGameLevelInfo.nID);
						iGameLevelCenter gameLevelCenter = m_GameData.GetGameLevelCenter();
						if (gameLevelCenter != null)
						{
							float proccess = gameLevelCenter.GetProccess(m_curGameLevelInfo.nID);
							if (proccess >= 0f)
							{
								m_DataCenter.SceneProccess = proccess;
							}
						}
						CAchievementManager.GetInstance().AddAchievement(2, new object[1] { (int)m_DataCenter.SceneProccess });
						CTrinitiCollectManager.GetInstance().SendPassLevel(m_curGameLevelInfo.nID);
					}
				}
			}
			m_GameState.isCheckUnLock = true;
			CAchievementManager.GetInstance().AddAchievement(12);
			if (m_curGameLevelInfo.ltRewardMaterial != null)
			{
				int num = 0;
				foreach (CRewardMaterial item in m_curGameLevelInfo.ltRewardMaterial)
				{
					num = item.GetDropCount();
					if (num > 0)
					{
						m_GameState.AddMaterial(item.nID, num);
					}
				}
			}
			CSoundScene.GetInstance().PlayBGM("BGM_Victory");
			iGameApp.GetInstance().Flurry_WinStage(m_curGameLevelInfo.nID);
		}
		else
		{
			m_StatusTime = 5f;
			if (!m_User.isDead)
			{
				m_CameraReveal.Go(m_User.GetBone(1).gameObject, -20f, 2f, 10f);
				m_User.CrossAnim(kAnimEnum.FailIdle, WrapMode.Loop, 0.3f, 1f, 0f);
				m_User.SetActionLayer(kAnimEnum.FailIdle, -1);
				m_User.CrossAnim(kAnimEnum.Fail, WrapMode.Once, 0.3f, 1f, 0f);
			}
			else
			{
				m_CameraReveal.Go(m_User.GetBone(1).gameObject, 70f, 5f, 10f);
			}
			if (m_bMutiplyGame)
			{
				if (m_GameData.m_HunterLevelCenter != null && m_curHunterLevelInfo != null)
				{
					CHunterLevelInfo cHunterLevelInfo2 = m_GameData.m_HunterLevelCenter.Get(m_curHunterLevelInfo.m_nID);
					if (cHunterLevelInfo2 != null)
					{
						m_GameState.m_nLevelHunterExp = cHunterLevelInfo2.m_nHunterExpLose;
					}
				}
				if (m_GameData.m_HunterCenter != null)
				{
					CHunterInfo cHunterInfo2 = m_GameData.m_HunterCenter.Get(m_DataCenter.HunterLvl - 1);
					if (cHunterInfo2 != null && m_GameState.m_nLevelHunterExp > cHunterInfo2.m_nHunterExpLose)
					{
						m_GameState.m_nLevelHunterExp = cHunterInfo2.m_nHunterExpLose;
					}
				}
				m_GameState.m_nLevelHunterExp *= -1;
			}
			CSoundScene.GetInstance().PlayBGM("BGM_Fail");
			iGameApp.GetInstance().Flurry_LoseStage(m_curGameLevelInfo.nID);
		}
		int num2 = m_GameState.m_nLevelRewardGold + m_GameState.GainGoldInGame;
		m_DataCenter.AddGold(num2);
		CAchievementManager.GetInstance().AddAchievement(9, new object[1] { num2 });
		m_DataCenter.AddCrystal(m_GameState.GainCrystalInGame);
		m_GameState.m_nLstExp = m_User.EXP;
		m_GameState.m_nLstLevel = m_User.Level;
		m_User.AddExp(m_GameState.m_nLevelRewardExp);
		m_GameState.m_nCurExp = m_User.EXP;
		m_GameState.m_nCurLevel = m_User.Level;
		m_GameState.m_nLstHunterExp = m_DataCenter.HunterExp;
		m_GameState.m_nLstHunterLevel = m_DataCenter.HunterLvl;
		m_DataCenter.AddHunterExp(m_GameState.m_nLevelHunterExp);
		if (m_GameState.m_nLevelHunterExpBackgroundPunish > 0)
		{
			m_DataCenter.AddHunterExp(m_GameState.m_nLevelHunterExpBackgroundPunish);
		}
		if (m_GameData.m_HunterCenter != null)
		{
			int nLevel = 0;
			int nExp = m_DataCenter.HunterExpTotal;
			m_GameData.m_HunterCenter.CalcHunterLvl(ref nLevel, ref nExp);
			if (nLevel != m_DataCenter.HunterLvl)
			{
				CTrinitiCollectManager.GetInstance().SendHunterLevel(nLevel);
			}
			m_DataCenter.HunterLvl = nLevel;
			m_DataCenter.HunterExp = nExp;
		}
		m_GameState.m_nCurHunterExp = m_DataCenter.HunterExp;
		m_GameState.m_nCurHunterLevel = m_DataCenter.HunterLvl;
		if (CGameNetManager.GetInstance().IsConnected())
		{
			//int nBeadmireCount = 0;
			//if (m_DataCenter != null)
			//{
			//	nBeadmireCount = m_DataCenter.BeAdmire;
			//}
			//float num3 = 1f;
			//CCharacterInfoLevel characterInfo = m_GameData.GetCharacterInfo(m_User.ID, m_User.Level + 1);
			//if (characterInfo != null)
			//{
			//	num3 = Mathf.Clamp01((float)m_User.EXP / (float)characterInfo.nExp);
			//}
			//CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo();
			//if (netUserInfo != null)
			//{
			//	netUserInfo.m_nCombatRatings = CalcCombatRating();
			//	netUserInfo.m_nGainCrystalInGame = m_GameState.GainCrystalInGame;
			//	netUserInfo.m_nGainGoldInGame = num2;
			//	netUserInfo.m_nBeadmireCount = nBeadmireCount;
			//	netUserInfo.m_nCharLvlResult = m_User.Level;
			//	netUserInfo.m_nCharExpResult = m_User.EXP;
			//	netUserInfo.m_nHunterLvlResult = m_DataCenter.HunterLvl;
			//	netUserInfo.m_nHunterExpResult = m_DataCenter.HunterExp;
			//}
			//CGameNetSender.GetInstance().SendMsg_BATTLE_RESULT_PLAYERREWARDS(netUserInfo.m_nTitle, netUserInfo.m_nCombatRatings, m_GameState.GainCrystalInGame, num2, nBeadmireCount, m_User.Level, m_User.EXP, m_DataCenter.HunterLvl, m_DataCenter.HunterExp);
		}
		m_User.SetFire(false);
		m_User.MoveStop();
		if (MyUtils.isWindows)
		{
			//Screen.lockCursor = false;
		}
	}

	public virtual void ReviveGame()
	{
		m_bMissionSuccess = false;
		Time.timeScale = 1f;
		m_TaskManager.ResetState();
		foreach (CCharMob value in m_MobMap.Values)
		{
			value.SetActive(true);
		}
		if (m_GameUI.UIManager.mWheelMove != null)
		{
			m_GameUI.UIManager.mWheelMove.gameObject.SetActiveRecursively(true);
		}
		if (m_GameUI.UIManager.mWheelShoot != null)
		{
			m_GameUI.UIManager.mWheelShoot.gameObject.SetActiveRecursively(true);
		}
	}

	public virtual void LeaveGame(float fDelayTime = 0f, kGameSceneEnum leavegamescene = kGameSceneEnum.None)
	{
		Debug.LogError(leavegamescene);
		m_Status = kGameStatus.GameLeave;
		m_StatusTime = fDelayTime;
		m_StatusTimeCount = 0f;
		m_LeaveGameScene = leavegamescene;
		CCameraRoam.GetInstance().UnLoad();
		iGameApp.GetInstance().SaveData();
		AndroidReturnPlugin.instance.SetIngame(false);
		AndroidReturnPlugin.instance.SetIngameMutiply(false);
		OpenClikPlugin.Hide();
	}

	public void LeaveMutiplyPunish()
	{
		if (!m_bMutiplyGame || GameStatus != kGameStatus.Gameing || !CGameNetManager.GetInstance().IsConnected() || m_GameState.m_nLevelHunterExpBackgroundPunish != 0)
		{
			return;
		}
		m_GameState.m_nLevelHunterExpBackgroundPunish = 1;
		if (m_GameData.m_HunterLevelCenter != null && m_curHunterLevelInfo != null)
		{
			CHunterLevelInfo cHunterLevelInfo = m_GameData.m_HunterLevelCenter.Get(m_curHunterLevelInfo.m_nID);
			if (cHunterLevelInfo != null)
			{
				m_GameState.m_nLevelHunterExpBackgroundPunish = cHunterLevelInfo.m_nHunterExpLose;
			}
		}
		if (m_GameData.m_HunterCenter != null)
		{
			CHunterInfo cHunterInfo = m_GameData.m_HunterCenter.Get(m_DataCenter.HunterLvl - 1);
			if (cHunterInfo != null && m_GameState.m_nLevelHunterExpBackgroundPunish > cHunterInfo.m_nHunterExpLose)
			{
				m_GameState.m_nLevelHunterExpBackgroundPunish = cHunterInfo.m_nHunterExpLose;
			}
		}
		m_DataCenter.AddHunterExp(-m_GameState.m_nLevelHunterExpBackgroundPunish);
		if (m_GameData.m_HunterCenter != null)
		{
			int nLevel = 1;
			int nExp = m_DataCenter.HunterExpTotal;
			m_GameData.m_HunterCenter.CalcHunterLvl(ref nLevel, ref nExp);
			if (nLevel != m_DataCenter.HunterLvl)
			{
				CTrinitiCollectManager.GetInstance().SendHunterLevel(nLevel);
			}
			m_DataCenter.HunterLvl = nLevel;
			m_DataCenter.HunterExp = nExp;
		}
		iGameApp.GetInstance().SaveData();
	}

	public virtual void Update(float deltaTime)
	{
		if (m_Input != null)
		{
			m_Input.Update(deltaTime);
		}
		switch (m_Status)
		{
		case kGameStatus.GameBegin:
			UpdateStatus_GameBegin(deltaTime);
			break;
		case kGameStatus.GameOver_Camera:
			UpdateStatus_GameOver_Camera(deltaTime);
			break;
		case kGameStatus.GameOver:
			UpdateStatus_GameOver(deltaTime);
			break;
		case kGameStatus.Gameing:
			UpdateStatus_Gaming(deltaTime);
			break;
		case kGameStatus.CutScene:
			UpdateStatus_CutScene(deltaTime);
			break;
		case kGameStatus.Pause:
			break;
		case kGameStatus.GameOver_Process:
			UpdateStatus_GameOver_Process(deltaTime);
			break;
		case kGameStatus.GameOver_ShowTime:
			UpdateStatus_GameOver_ShowTime(deltaTime);
			break;
		case kGameStatus.GameLeave:
			UpdateStatus_GameLeave(deltaTime);
			break;
		case kGameStatus.Gameing_IAP:
			break;
		}
	}

	public virtual void FixedUpdate(float deltaTime)
	{
	}

	public virtual void LateUpdate(float deltaTime)
	{
		if (m_Input != null)
		{
			m_Input.LateUpdate(deltaTime);
		}
	}

	protected virtual void UpdateStatus_Movie(float deltaTime)
	{
	}

	protected virtual void UpdateStatus_GameBegin(float deltaTime)
	{
		m_StatusTimeCount += deltaTime;
		if (!(m_StatusTimeCount < m_StatusTime))
		{
			m_StatusTimeCount = 0f;
			m_Status = kGameStatus.Gameing;
		}
	}

	protected virtual void UpdateStatus_GameOver_Process(float deltaTime)
	{
		m_StatusTimeCount += deltaTime;
		if (m_StatusTimeCount < m_StatusTime)
		{
			return;
		}
		Time.timeScale = 1f;
		if (m_bMissionSuccess)
		{
			CTaskBase task = m_TaskManager.GetTask();
			if (task != null)
			{
				CTaskInfo taskInfo = task.GetTaskInfo();
				if (taskInfo != null && taskInfo.nType == 2)
				{
					m_GameUI.ShowTip("Return village in 10 seconds");
					m_Status = kGameStatus.GameOver_ShowTime;
					m_StatusTime = 10f;
					m_StatusTimeCount = 0f;
					m_CameraTrail.Active = true;
					m_CameraFocus.Active = false;
					m_CameraReveal.Active = false;
					return;
				}
			}
		}
		GameOver(m_bMissionSuccess);
	}

	protected virtual void UpdateStatus_GameOver_ShowTime(float deltaTime)
	{
		m_StatusTimeCount += deltaTime;
		if (m_StatusTimeCount >= m_StatusTime)
		{
			GameOver(true);
			return;
		}
		m_GameState.AddGameTime(deltaTime);
		UpdateAssistAim(deltaTime);
		List<CCharMob> list = new List<CCharMob>();
		foreach (CCharMob value in m_MobMap.Values)
		{
			if (value.isNeedDestroy)
			{
				list.Add(value);
			}
		}
		foreach (CCharMob item in list)
		{
			RemoveMob(item);
		}
	}

	protected virtual void UpdateStatus_GameOver_Camera(float deltaTime)
	{
		m_StatusTimeCount += deltaTime;
		if (m_StatusTimeCount < m_StatusTime)
		{
			return;
		}
		m_Status = kGameStatus.GameOver;
		CSoundScene.GetInstance().StopAmbienceBGM();
		if (m_bMissionSuccess)
		{
			if (CGameNetManager.GetInstance().IsConnected())
			{
				ShowGameOverUI_Win_Mutiply();
			}
			else
			{
				ShowGameOverUI_Win();
			}
			return;
		}
		CSoundScene.GetInstance().StopAmbienceBGM();
		if (isTutorialStage)
		{
			RestartGame();
		}
		else if (CGameNetManager.GetInstance().IsConnected())
		{
			ShowGameOverUI_Fail_Mutiply();
		}
		else
		{
			ShowGameOverUI_Fail();
		}
	}

	protected virtual void UpdateStatus_GameOver(float deltaTime)
	{
		if (m_GameOverUIStatus != kGameOverUIStatus.Win_Mutiply || !(m_GameUI.UIManager != null) || !(m_GameUI.UIManager.mPanelMissionSuccessMutiply != null) || m_GameUI.UIManager.mPanelMissionSuccessMutiply.m_Champion.active)
		{
			return;
		}
		bool flag = true;
		for (int i = 0; i < 3; i++)
		{
			gyUIPlayerRewards playerRewards = m_GameUI.UIManager.mPanelMissionSuccessMutiply.GetPlayerRewards(i);
			if (playerRewards != null && playerRewards.IsAnim)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			return;
		}
		if (m_GameUI.UIManager.mPanelMissionSuccessMutiply.m_Champion != null)
		{
			CUISound.GetInstance().Play("UI_Crown");
			m_GameUI.UIManager.mPanelMissionSuccessMutiply.m_Champion.SetActiveRecursively(true);
			m_GameUI.UIManager.mPanelMissionSuccessMutiply.m_Champion.GetComponent<Animation>().Play("ChampionAppear");
		}
		if (m_GameState.m_nMVPGold <= 0 && m_GameState.m_nMVPCrystal <= 0)
		{
			return;
		}
		gyUIPlayerRewards playerRewards2 = m_GameUI.UIManager.mPanelMissionSuccessMutiply.GetPlayerRewards(0);
		if (playerRewards2 != null)
		{
			if (playerRewards2.m_Gold != null)
			{
				playerRewards2.m_Gold.Value += m_GameState.m_nMVPGold;
			}
			if (playerRewards2.m_Crystal != null)
			{
				playerRewards2.m_Crystal.Value += m_GameState.m_nMVPCrystal;
			}
			m_GameState.m_nMVPGold = 0;
			m_GameState.m_nMVPCrystal = 0;
		}
	}

	protected virtual void UpdateStatus_GameLeave(float deltaTime)
	{
		m_StatusTimeCount += deltaTime;
		if (m_StatusTimeCount < m_StatusTime)
		{
			return;
		}
		m_Status = kGameStatus.None;
		m_StatusTimeCount = 0f;
		//if (CGameNetManager.GetInstance().IsConnected())
		//{
		//	TNetManager.GetInstance().LeaveRoom();
		//	return;
		//}
		if (m_LeaveGameScene == kGameSceneEnum.None)
		{
			m_LeaveGameScene = kGameSceneEnum.Home;
		}
		iGameApp.GetInstance().EnterScene(m_LeaveGameScene);
	}

	protected virtual void UpdateStatus_Gaming(float deltaTime)
	{
		if (CGameNetManager.GetInstance().IsConnected() && !CGameNetManager.GetInstance().IsGaming)
		{
			return;
		}
		m_GameState.AddGameTime(deltaTime);
		UpdateAssistAim(deltaTime);
		List<CCharMob> list = new List<CCharMob>();
		foreach (CCharMob value in m_MobMap.Values)
		{
			if (value.isNeedDestroy)
			{
				list.Add(value);
			}
		}
		foreach (CCharMob item in list)
		{
			RemoveMob(item);
		}
		if (m_bWaitingRevive && m_fWaitingReviveTime > 0f)
		{
			m_fWaitingReviveTimeCount += deltaTime;
			if (m_fWaitingReviveTimeCount >= m_fWaitingReviveTime)
			{
				FinishRevive(false);
			}
		}
		if (m_MGManager != null)
		{
			m_MGManager.Update(deltaTime);
		}
		if (m_EventManager != null)
		{
			m_EventManager.Update(deltaTime);
		}
		if (m_TaskManager != null)
		{
			m_TaskManager.Update(deltaTime);
			if (CGameNetManager.GetInstance().IsRoomMaster())
			{
				if (IsPlayerAllDead())
				{
					m_TaskManager.OnPlayerDead();
				}
				if ((m_TaskManager.isAllCompleted || m_TaskManager.isFailed) && (CGameNetManager.GetInstance().IsConnected() || (!m_bWaitingRevive && !m_bUserDeath)))
				{
					UnityEngine.Debug.Log("over " + m_TaskManager.isAllCompleted + " " + m_TaskManager.isFailed);
					FinishRevive(false);
					FinishGame(m_TaskManager.isAllCompleted);
					if (CGameNetManager.GetInstance().IsConnected())
					{
						CGameNetSender.GetInstance().SendMsg_GAME_OVER(m_TaskManager.isAllCompleted);
					}
					return;
				}
			}
			List<CTaskBase> taskList = m_TaskManager.GetTaskList();
			if (taskList != null && m_curTriggerManagerEnd != null)
			{
				foreach (CTaskBase item2 in taskList)
				{
					CTaskInfo taskInfo = item2.GetTaskInfo();
					if (taskInfo == null)
					{
						continue;
					}
					switch (taskInfo.nType)
					{
					case 1:
						if (!(m_User == null) && m_User.IsTakenItem() && m_curTriggerManagerEnd.IsInside2D(m_User.Pos))
						{
							int carryItem = m_User.GetCarryItem();
							m_TaskManager.OnGetItem(carryItem);
							m_User.DropItem();
							AddStealItem(carryItem, 1);
							CAchievementManager.GetInstance().AddAchievement(7, new object[1] { carryItem });
							CEventManager eventManager = m_MGManager.GetEventManager();
							if (eventManager != null)
							{
								eventManager.Trigger(new EventCondition_StealEgg_Home(carryItem, GetStealItem(carryItem)));
							}
						}
						break;
					case 3:
						foreach (CCharMob value2 in m_MobMap.Values)
						{
							if (m_curTriggerManagerEnd.IsInside2D(value2.Pos))
							{
								m_TaskManager.OnMonsterEnter(value2.ID);
							}
						}
						break;
					}
				}
			}
			if (m_ltRefreshWorldMonster.Count > 0)
			{
				foreach (CWorldMonster item3 in m_ltRefreshWorldMonster)
				{
					if (!(item3.fRefreshTime >= 0f) || !(item3.fRefreshTime <= m_GameState.GetGameTime()))
					{
						continue;
					}
					item3.fRefreshTime = -1f;
					Vector3 vector = Vector3.zero;
					Vector3 v3Dir = Vector3.forward;
					if (m_User != null)
					{
						vector = m_User.Pos;
						if (m_curSPManagerGround != null)
						{
							CStartPoint randomClosePoint = m_curSPManagerGround.GetRandomClosePoint(m_User.Pos, 3, 10f);
							if (randomClosePoint != null)
							{
								vector = randomClosePoint.GetRandom();
								v3Dir = (m_User.Pos - vector).normalized;
							}
						}
					}
					if (!CGameNetManager.GetInstance().IsRoomMaster())
					{
						break;
					}
					int uID = MyUtils.GetUID();
					CCharMob cCharMob = AddMob(item3.nMobID, m_User.Level, uID, vector, v3Dir);
					if (cCharMob != null)
					{
						cCharMob.m_bShowTime = false;
						AddEffect(cCharMob.GetBone(0).position, Vector3.forward, 2f, 1950);
						if (CGameNetManager.GetInstance().IsConnected())
						{
							CGameNetSender.GetInstance().SendMsg_MGMANAGER_ADDMOB_SPECIAL(item3.nMobID, m_User.Level, uID, vector, v3Dir);
						}
					}
					break;
				}
			}
		}
		if (m_bUserDeath)
		{
			m_fUserDeathTimeCount += deltaTime;
			if (m_fUserDeathTimeCount >= m_fUserDeathTime)
			{
				m_bUserDeath = false;
				if (!CGameNetManager.GetInstance().IsConnected() || !IsPlayerAllDead())
				{
					Debug.Log(string.Concat(m_Status, " ", isTutorialStage));
					if (m_Status == kGameStatus.Gameing && !isTutorialStage)
					{
						StartRevive(5f);
					}
					if (CGameNetManager.GetInstance().IsConnected())
					{
						foreach (CCharPlayer value3 in m_PlayerMap.Values)
						{
							if (value3 == null || value3.isDead || value3 == m_User)
							{
								continue;
							}
							StartOBCamera(value3);
							break;
						}
					}
				}
			}
		}
		if (!m_bObserve || (!(m_CameraTrail.m_Target == null) && !m_CameraTrail.m_Target.isDead))
		{
			return;
		}
		foreach (CCharPlayer value4 in m_PlayerMap.Values)
		{
			if (value4 == null || value4.isDead || value4 == m_User)
			{
				continue;
			}
			StartOBCamera(value4);
			break;
		}
	}

	protected virtual void UpdateStatus_CutScene(float deltaTime)
	{
		m_GameState.AddGameTime(deltaTime);
		if (m_MGManager != null)
		{
			m_MGManager.Update(deltaTime);
		}
		if (m_EventManager != null)
		{
			m_EventManager.Update(deltaTime);
		}
	}

	public CControlBase GetInput()
	{
		return m_Input;
	}

	public void InitEggLife(float fLife)
	{
		m_fCurEggLife = fLife;
		m_fMaxEggLife = fLife;
	}

	public void AddEggLife(float fDmg)
	{
		if (!(m_fMaxEggLife < 0f))
		{
			m_fCurEggLife += fDmg;
			if (m_fCurEggLife < 0f)
			{
				m_fCurEggLife = 0f;
			}
		}
	}

	public void StartMobCG()
	{
		foreach (CCharMob value in m_MobMap.Values)
		{
			value.SetActive(false);
		}
		m_GameUI.MovieUIIn(1f);
		m_GameUI.HideGameUI();
		m_GameUI.ShowSkipUI(true);
		m_User.SetFire(false);
		m_User.MoveStop();
	}

	public void FinishMobCG()
	{
		foreach (CCharMob value in m_MobMap.Values)
		{
			value.SetActive(true);
		}
		m_Status = kGameStatus.GameBegin;
		m_StatusTime = 1f;
		m_StatusTimeCount = 0f;
		m_GameUI.FadeIn(1f);
		m_GameUI.MovieUIOut(1f);
		m_GameUI.ShowGameUI();
		m_GameUI.ShowSkipUI(false);
	}

	public void StartWaveCG(int nWaveID)
	{
		if (m_Status != kGameStatus.Gameing || m_ltCGWave.Contains(nWaveID))
		{
			return;
		}
		WaveInfo waveInfo = m_GameData.GetWaveInfo(nWaveID);
		if (waveInfo == null)
		{
			return;
		}
		if (waveInfo.sCutScene.Length > 0)
		{
			CCameraRoam.CCGInfo cCGInfo = new CCameraRoam.CCGInfo();
			cCGInfo.sCG = waveInfo.sCutScene;
			cCGInfo.sCGContent = waveInfo.sCutSceneContent;
			cCGInfo.sCGAmbience = waveInfo.sCutSceneAmbience;
			cCGInfo.sCGBGM = waveInfo.sCutSceneBGM;
			if (CCameraRoam.GetInstance().Start(Camera.main, cCGInfo, StartMobCG, FinishMobCG))
			{
				m_Status = kGameStatus.CutScene;
				m_ltCGWave.Add(nWaveID);
			}
		}
		else
		{
			CSoundScene.GetInstance().PlayBGM(waveInfo.sCutSceneBGM);
		}
	}

	public CCharMob AddMobByWave(int nMobID, int nMobLevel, int nMobUID, int nWaveID, int nSequence, Vector3 v3Pos, Vector3 v3Dir)
	{
		CMobInfoLevel mobInfo = m_GameData.GetMobInfo(nMobID, nMobLevel);
		if (mobInfo == null)
		{
			return null;
		}
		CCharMob cCharMob = AddMob(nMobID, nMobLevel, nMobUID, v3Pos, v3Dir);
		if (cCharMob == null)
		{
			return null;
		}
		cCharMob.GenerateWaveID = nWaveID;
		cCharMob.GenerateSequence = nSequence;
		AddWaveMobNumber(nWaveID, 1);
		CEventManager eventManager = m_MGManager.GetEventManager();
		if (eventManager != null)
		{
			eventManager.Trigger(new EventCondition_MobByWave(nWaveID, nSequence, 0));
			eventManager.Trigger(new EventCondition_MobByID(nMobID, 0));
		}
		return cCharMob;
	}

	public CCharMob AddMob(int nMobID, int nMobLevel, int nMobUID, Vector3 v3Pos, Vector3 v3Dir)
	{
		CMobInfoLevel mobInfo = m_GameData.GetMobInfo(nMobID, nMobLevel);
		if (mobInfo == null)
		{
			return null;
		}
		int num = -1;
		CAIManagerInfo aIManagerInfo = m_GameData.GetAIManagerInfo(mobInfo.nAIManagerID);
		if (aIManagerInfo != null)
		{
			CAIInfo aIInfo = m_GameData.GetAIInfo(aIManagerInfo.nAI);
			if (aIInfo != null)
			{
				num = aIInfo.nBehavior;
			}
		}
		if (mobInfo.nRareType != 2 && IsMonsterNumFull(nMobID, mobInfo.nType, num))
		{
			return null;
		}
		GameObject gameObject = PrefabManager.Get(mobInfo.nModel);
		if (gameObject == null)
		{
			return null;
		}
		GameObject gameObject2 = (GameObject)Object.Instantiate(gameObject);
		if (gameObject2 == null)
		{
			return null;
		}
		CCharMob component = gameObject2.GetComponent<CCharMob>();
		if (component == null)
		{
			return null;
		}
		component.UID = nMobUID;
		component.gameObject.name = "mob_" + component.UID;
		component.InitAI(mobInfo.nAIManagerID);
		component.InitMob(nMobID, nMobLevel);
		component.MobType = mobInfo.nType;
		component.MobBehavior = num;
		component.name = "mob_" + component.UID;
		RaycastHit hitInfo;
		if (Physics.Raycast(new Ray(v3Pos + new Vector3(0f, 10f, 0f), Vector3.down), out hitInfo, 20f, 536870912))
		{
			v3Pos = hitInfo.point;
		}
		component.Pos = v3Pos;
		if (m_User != null)
		{
			component.Dir2D = m_User.Pos - component.Pos;
		}
		else
		{
			component.Dir2D = v3Dir;
		}
		m_MobMap.Add(component.UID, component);
		AddMonsterNumLimit(nMobID, mobInfo.nType, num);
		if (component.IsBoss() && m_GameUI != null && m_GameUI.UIManager != null && m_GameUI.UIManager.mTaskPlane != null)
		{
			List<iGameTaskUIBase> data = m_GameUI.UIManager.mTaskPlane.GetData();
			if (data != null)
			{
				foreach (iGameTaskUIBase item in data)
				{
					iGameTaskUIHunterList iGameTaskUIHunterList2 = item as iGameTaskUIHunterList;
					if (iGameTaskUIHunterList2 != null)
					{
						iGameTaskUIHunterList2.SetLevel(component.ID, component.Level);
						bossHP = component.MaxHP;
						bossLvl = component.Level;
					}
				}
			}
		}
		return component;
	}

	private float bossHP;

	private float bossLvl;

	public CCharUser AddUser(int nID, int nUID, Vector3 v3Pos, Vector3 v3Dir)
	{
		GameObject gameObject = PrefabManager.Get(10);
		if (gameObject == null)
		{
			return null;
		}
		GameObject gameObject2 = (GameObject)Object.Instantiate(gameObject);
		if (gameObject2 == null)
		{
			return null;
		}
		CCharUser cCharUser = gameObject2.AddComponent<CCharUser>();
		if (cCharUser == null)
		{
			return null;
		}
		cCharUser.Pos = v3Pos;
		cCharUser.Dir2D = v3Dir;
		cCharUser.UID = nUID;
		cCharUser.name = "main_player";
		m_PlayerMap.Add(cCharUser.UID, cCharUser);
		return cCharUser;
	}

	public CCharPlayer AddPlayer(int nID, int nUID, Vector3 v3Pos, Vector3 v3Dir)
	{
		GameObject gameObject = PrefabManager.Get(10);
		if (gameObject == null)
		{
			return null;
		}
		GameObject gameObject2 = (GameObject)Object.Instantiate(gameObject);
		if (gameObject2 == null)
		{
			return null;
		}
		CCharPlayer cCharPlayer = gameObject2.AddComponent<CCharPlayer>();
		if (cCharPlayer == null)
		{
			return null;
		}
		cCharPlayer.UID = nUID;
		cCharPlayer.gameObject.name = "player_" + cCharPlayer.UID;
		cCharPlayer.Pos = v3Pos;
		cCharPlayer.Dir2D = v3Dir;
		cCharPlayer.AimTo(cCharPlayer.Pos + cCharPlayer.Dir2D * 1000f);
		m_PlayerMap.Add(cCharPlayer.UID, cCharPlayer);
		return cCharPlayer;
	}

	public void RemoveMob(CCharMob charmob)
	{
		if (!(charmob == null))
		{
			m_MobMap.Remove(charmob.UID);
			charmob.Destroy();
		}
	}

	public void RemoveMob(int nUID)
	{
		if (m_MobMap.ContainsKey(nUID))
		{
			RemoveMob(m_MobMap[nUID]);
		}
	}

	public void RemovePlayer(int nUID)
	{
		if (m_PlayerMap.ContainsKey(nUID))
		{
			m_PlayerMap[nUID].Destroy();
			m_PlayerMap.Remove(nUID);
			m_GameUI.DelTeamMateProtrait(nUID);
		}
	}

	public void AddWaveMobNumber(int nWaveID, int nNum)
	{
		if (!m_dictWaveMobNumber.ContainsKey(nWaveID))
		{
			m_dictWaveMobNumber.Add(nWaveID, 0);
		}
		Dictionary<int, int> dictWaveMobNumber;
		Dictionary<int, int> dictionary = (dictWaveMobNumber = m_dictWaveMobNumber);
		int key;
		int key2 = (key = nWaveID);
		key = dictWaveMobNumber[key];
		dictionary[key2] = key + nNum;
	}

	public int GetWaveMobNumber(int nWaveID)
	{
		if (!m_dictWaveMobNumber.ContainsKey(nWaveID))
		{
			return 0;
		}
		return m_dictWaveMobNumber[nWaveID];
	}

	public void AddStealItem(int nItemID, int nNum)
	{
		if (!m_dictStealItem.ContainsKey(nItemID))
		{
			m_dictStealItem.Add(nItemID, 0);
		}
		Dictionary<int, int> dictStealItem;
		Dictionary<int, int> dictionary = (dictStealItem = m_dictStealItem);
		int key;
		int key2 = (key = nItemID);
		key = dictStealItem[key];
		dictionary[key2] = key + nNum;
	}

	public int GetStealItem(int nItemID)
	{
		if (!m_dictStealItem.ContainsKey(nItemID))
		{
			return 0;
		}
		return m_dictStealItem[nItemID];
	}

	public void DelPlayer(int nUID)
	{
		if (m_PlayerMap.ContainsKey(nUID))
		{
			Object.Destroy(m_PlayerMap[nUID].gameObject);
			m_PlayerMap.Remove(nUID);
		}
	}

	public void AddBulletTrack(Vector3 v3Src, Vector3 v3Dst, int nPrefab)
	{
		GameObject poolObject = PrefabManager.GetPoolObject(nPrefab, 0f);
		if (!(poolObject == null))
		{
			iBulletTrack component = poolObject.GetComponent<iBulletTrack>();
			if (!(component == null))
			{
				component.Initialize(v3Src, v3Dst, 200f);
				poolObject.SetActiveRecursively(true);
			}
		}
	}

	public void AddHitEffect(Vector3 v3Pos, Vector3 v3Dir, int nPrefab)
	{
		GameObject poolObject = PrefabManager.GetPoolObject(nPrefab, 2f);
		if (!(poolObject == null))
		{
			poolObject.transform.position = v3Pos;
			poolObject.transform.forward = v3Dir;
			poolObject.SetActiveRecursively(true);
		}
	}

	public GameObject AddFireEffect(Transform transform, Vector3 v3Dir, int nPrefab, float fTime = 2f)
	{
		if (transform == null)
		{
			return null;
		}
		GameObject poolObject = PrefabManager.GetPoolObject(nPrefab, fTime);
		if (poolObject == null)
		{
			return null;
		}
		poolObject.transform.parent = transform;
		poolObject.transform.localPosition = Vector3.zero;
		poolObject.transform.forward = v3Dir;
		poolObject.SetActiveRecursively(true);
		if (fTime > 0f)
		{
		}
		return poolObject;
	}

	public iSpawnBullet AddSpawn(int nUID, int nID, CWeaponInfoLevel weaponinfolvl, Vector3 v3Pos, Vector3 v3Force)
	{
		GameObject gameObject = PrefabManager.Get(nID);
		if (gameObject == null)
		{
			return null;
		}
		GameObject gameObject2 = (GameObject)Object.Instantiate(gameObject);
		if (gameObject2 == null)
		{
			return null;
		}
		iSpawnBullet component = gameObject2.GetComponent<iSpawnBullet>();
		if (component == null)
		{
			return null;
		}
		component.InitializeFromWeapon(nUID, weaponinfolvl, v3Pos, v3Force);
		return component;
	}

	public iSpawnBullet AddSpawn(int nUID, int nID, CSkillInfoLevel skillinfolvl, Vector3 v3Pos, Vector3 v3Force)
	{
		GameObject gameObject = PrefabManager.Get(nID);
		if (gameObject == null)
		{
			return null;
		}
		GameObject gameObject2 = (GameObject)Object.Instantiate(gameObject);
		if (gameObject2 == null)
		{
			return null;
		}
		iSpawnBullet component = gameObject2.GetComponent<iSpawnBullet>();
		if (component == null)
		{
			return null;
		}
		component.InitializeFromSkill(nUID, skillinfolvl, v3Pos, v3Force);
		return component;
	}

	public GameObject AddEffect(Vector3 v3Pos, Vector3 v3Dir, float fTime, int nPrefab)
	{
		GameObject gameObject = PrefabManager.Get(nPrefab);
		if (gameObject == null)
		{
			return null;
		}
		GameObject gameObject2 = (GameObject)Object.Instantiate(gameObject, v3Pos, Quaternion.identity);
		if (gameObject2 == null)
		{
			return null;
		}
		gameObject2.transform.position = v3Pos;
		gameObject2.transform.forward = v3Dir;
		Object.Destroy(gameObject2, fTime);
		return gameObject2;
	}

	public GameObject AddSceneGameObject(int nPrefab, Vector3 v3Pos, Vector3 v3Dir, float fDisappearTime = -1f)
	{
		Object @object = PrefabManager.Get(nPrefab);
		if (@object == null)
		{
			return null;
		}
		GameObject gameObject = (GameObject)Object.Instantiate(@object, v3Pos, Quaternion.LookRotation(v3Dir));
		if (gameObject == null)
		{
			return null;
		}
		if (fDisappearTime > 0f)
		{
			Object.Destroy(gameObject, fDisappearTime);
		}
		m_ltSceneGameObject.Add(gameObject);
		return gameObject;
	}

	public void AddItem(int nItemID, Vector3 v3Pos, Vector3 v3Dir, float fDisappearTime = -1f, int nItemUID = -1)
	{
		CItemInfoLevel itemInfo = m_GameData.GetItemInfo(nItemID, 1);
		if (itemInfo == null)
		{
			return;
		}
		GameObject gameObject = PrefabManager.Get(itemInfo.nModel);
		if (gameObject == null)
		{
			return;
		}
		GameObject gameObject2 = (GameObject)Object.Instantiate(gameObject);
		if (gameObject2 == null)
		{
			return;
		}
		gameObject2.transform.position = v3Pos;
		gameObject2.transform.forward = v3Dir;
		iItem component = gameObject2.GetComponent<iItem>();
		if (component != null)
		{
			component.Initialize(nItemID);
			component.UID = nItemUID;
			component.AddForce(v3Dir);
			if (component.isHasScreenTip)
			{
				component.m_ScreenTip = m_GameUI.CreateScreenTip(m_User.gameObject, gameObject2);
				component.m_ScreenTip.SetIcon("dan");
			}
		}
		if (fDisappearTime > 0f)
		{
			Object.Destroy(gameObject2, fDisappearTime);
		}
		m_ltItem.Add(gameObject2);
	}

	public void AddObject(int nPrefabID, Vector3 v3Pos, Vector3 v3Dir)
	{
		GameObject gameObject = PrefabManager.Get(nPrefabID);
		if (!(gameObject == null))
		{
			GameObject gameObject2 = (GameObject)Object.Instantiate(gameObject);
			if (!(gameObject2 == null))
			{
				gameObject2.transform.position = v3Pos;
				gameObject2.transform.forward = v3Dir;
				m_ltItem.Add(gameObject2);
			}
		}
	}

	public void AddGold(int nGold, Vector3 v3Pos, Vector3 v3Dir, float fScaleRate)
	{
		GameObject gameObject = PrefabManager.Get(251);
		if (gameObject == null)
		{
			return;
		}
		GameObject gameObject2 = (GameObject)Object.Instantiate(gameObject);
		if (gameObject2 == null)
		{
			return;
		}
		gameObject2.transform.position = v3Pos;
		gameObject2.transform.forward = v3Dir;
		gameObject2.transform.localScale *= fScaleRate;
		iItem component = gameObject2.GetComponent<iItem>();
		if (component != null)
		{
			bool bAbsorb = false;
			CTaskInfo taskInfo = m_GameData.GetTaskInfo(m_nCurTaskID);
			if ((taskInfo != null && taskInfo.nType == 3) || m_curGameLevelInfo.bIsSkyScene)
			{
				bAbsorb = true;
			}
			component.Initialize(50001, bAbsorb);
			component.UID = -1;
			component.AddForce(v3Dir);
			component.UpdateFunc(0, 100, nGold, 0);
			if (component.isHasScreenTip)
			{
				component.m_ScreenTip = m_GameUI.CreateScreenTip(m_User.gameObject, gameObject2);
				component.m_ScreenTip.SetIcon("dan");
			}
		}
		m_ltItem.Add(gameObject2);
	}

	public void AddCrystal(int nCrystal, Vector3 v3Pos, Vector3 v3Dir, float fScaleRate)
	{
		GameObject gameObject = PrefabManager.Get(253);
		if (gameObject == null)
		{
			return;
		}
		GameObject gameObject2 = (GameObject)Object.Instantiate(gameObject);
		if (gameObject2 == null)
		{
			return;
		}
		gameObject2.transform.position = v3Pos;
		gameObject2.transform.forward = v3Dir;
		gameObject2.transform.localScale *= fScaleRate;
		iItem component = gameObject2.GetComponent<iItem>();
		if (component != null)
		{
			component.Initialize(50001);
			component.UID = -1;
			component.AddForce(v3Dir);
			component.UpdateFunc(0, 103, nCrystal, 0);
			if (component.isHasScreenTip)
			{
				component.m_ScreenTip = m_GameUI.CreateScreenTip(m_User.gameObject, gameObject2);
				component.m_ScreenTip.SetIcon("dan");
			}
		}
		m_ltItem.Add(gameObject2);
	}

	public List<GameObject> GetSceneItemList()
	{
		return m_ltItem;
	}

	public iCameraTrail GetCamera()
	{
		return m_CameraTrail;
	}

	public iCameraReveal GetCameraReveal()
	{
		return m_CameraReveal;
	}

	public CCharUser GetUser()
	{
		return m_User;
	}

	public IEnumerable GetMobEnumerator()
	{
		foreach (CCharMob value in m_MobMap.Values)
		{
			yield return value;
		}
	}

	public CCharMob GetMob(int uid)
	{
		if (!m_MobMap.ContainsKey(uid))
		{
			return null;
		}
		return m_MobMap[uid];
	}

	public CCharPlayer GetPlayer(int uid)
	{
		if (!m_PlayerMap.ContainsKey(uid))
		{
			return null;
		}
		return m_PlayerMap[uid];
	}

	public IEnumerable GetPlayerEnumerator()
	{
		foreach (CCharPlayer value in m_PlayerMap.Values)
		{
			yield return value;
		}
	}

	public int GetPlayerCount()
	{
		if (m_PlayerMap == null)
		{
			return 0;
		}
		return m_PlayerMap.Count;
	}

	public List<CCharBase> GetUnitList()
	{
		List<CCharBase> list = new List<CCharBase>();
		foreach (CCharMob value in m_MobMap.Values)
		{
			list.Add(value);
		}
		foreach (CCharPlayer value2 in m_PlayerMap.Values)
		{
			list.Add(value2);
		}
		return list;
	}

	public bool IsSkyScene()
	{
		return m_bIsSkyScene;
	}

	public void SendAttack(int uid_atk, int uid_def, int skillid)
	{
		if (!(m_User == null))
		{
			CCharMob mob = GetMob(uid_def);
			if (!(mob == null))
			{
			}
		}
	}

	public void OnAttackMsg(int uid_atk, int uid_def, int skillid)
	{
	}

	public bool WorldToScreenPoint(Vector3 v3WorldPos, ref Vector2 v2ScreenPos)
	{
		v3WorldPos = Camera.main.WorldToScreenPoint(v3WorldPos);
		if (v3WorldPos.z < 0f)
		{
			return false;
		}
		v2ScreenPos.x = v3WorldPos.x;
		v2ScreenPos.y = v3WorldPos.y;
		return true;
	}

	public bool WorldToScreenPointNGUI(Vector3 v3World, ref Vector3 v3Screen)
	{
		if (m_CameraTrail == null)
		{
			return false;
		}
		v3Screen = m_CameraTrail.GetComponent<Camera>().WorldToScreenPoint(v3World);
		v3Screen -= m_GameState.GetScreenCenterV3();
		return true;
	}

	public bool WorldToScreenPointTUI(Vector3 v3World, ref Vector3 v3Screen)
	{
		return false;
	}

	public iGameLogic GetGameLogic()
	{
		return m_GameLogic;
	}

	public iGameUIBase GetGameUI()
	{
		return m_GameUI;
	}

	public CStartPointManager GetSPManagerGround()
	{
		return m_curSPManagerGround;
	}

	public CStartPointManager GetSPManagerSky()
	{
		return m_curSPManagerSky;
	}

	public CStartPointManager GetHPManager()
	{
		return m_curHPManager;
	}

	public CPathWalkerManager GetPathWalkerManager()
	{
		return m_PathWalkerManager;
	}

	public void SetUserUID(int nUID)
	{
		if (m_User.UID == -1)
		{
			m_User.UID = nUID;
		}
	}

	public void AddMonsterNumLimit(int nMobID, int nMobType, int nBehavior, int nNum = 1)
	{
		foreach (MonsterNumInfo item in m_ltMonsterNumInfo)
		{
			switch (item.nLimitType)
			{
			case 1:
				item.curNum += nNum;
				break;
			case 2:
				if (nMobID == item.nLimitValue)
				{
					item.curNum += nNum;
				}
				break;
			case 3:
				if (nMobType == item.nLimitValue)
				{
					item.curNum += nNum;
				}
				break;
			case 0:
				if (nBehavior == item.nLimitValue)
				{
					item.curNum += nNum;
				}
				break;
			}
			if (item.curNum < 0)
			{
				item.curNum = 0;
			}
		}
	}

	protected bool IsMonsterNumFull(int nMobID, int nMobType, int nBehavior)
	{
		foreach (MonsterNumInfo item in m_ltMonsterNumInfo)
		{
			switch (item.nLimitType)
			{
			case 1:
				return item.IsMax();
			case 2:
				if (nMobID == item.nLimitValue && item.IsMax())
				{
					return true;
				}
				break;
			case 3:
				if (nMobType == item.nLimitValue && item.IsMax())
				{
					return true;
				}
				break;
			case 0:
				if (nBehavior == item.nLimitValue && item.IsMax())
				{
					return true;
				}
				break;
			}
		}
		return false;
	}

	public void AddDamageText(float fDamage, Vector3 v3Pos, bool bCritical = false)
	{
		if (!(m_GameUI == null))
		{
			WorldToScreenPointNGUI(v3Pos, ref v3Pos);
			if (!bCritical)
			{
				m_GameUI.AddDmgUI(fDamage, v3Pos, Color.white, gyUILabelDmg.kMode.Mode2);
			}
			else
			{
				m_GameUI.AddDmgUI(fDamage, v3Pos, Color.yellow, gyUILabelDmg.kMode.Mode1);
			}
		}
	}

	public void AddDamageText(float fDamage, Vector3 v3Pos, Color color, bool bCritical = false)
	{
		if (!(m_GameUI == null))
		{
			WorldToScreenPointNGUI(v3Pos, ref v3Pos);
			if (!bCritical)
			{
				m_GameUI.AddDmgUI(fDamage, v3Pos, color, gyUILabelDmg.kMode.Mode2);
			}
			else
			{
				m_GameUI.AddDmgUI(fDamage, v3Pos, color, gyUILabelDmg.kMode.Mode1);
			}
		}
	}

	public void AddExpText(int nExp, Vector3 v3Pos)
	{
		if (!(m_GameUI == null))
		{
			WorldToScreenPointNGUI(v3Pos, ref v3Pos);
			m_GameUI.AddExpText(v3Pos, nExp);
		}
	}

	public void AddGoldText(float fValue, Vector3 v3Pos)
	{
		if (!(m_GameUI == null))
		{
			WorldToScreenPointNGUI(v3Pos, ref v3Pos);
			m_GameUI.AddGoldUI(fValue, v3Pos);
		}
	}

	public void AddCrystalText(float fValue, Vector3 v3Pos)
	{
		if (!(m_GameUI == null))
		{
			WorldToScreenPointNGUI(v3Pos, ref v3Pos);
			m_GameUI.AddCrystalUI(fValue, v3Pos);
		}
	}

	public void AddMaterial(Vector3 v3Pos, string sIcon, int nCount)
	{
		if (!(m_GameUI == null))
		{
			WorldToScreenPointNGUI(v3Pos, ref v3Pos);
			m_GameUI.AddMaterialUI(v3Pos, sIcon, nCount);
		}
	}

	public void AddHealText(float fHeal, Vector3 v3Pos, bool bCritical = false)
	{
		if (!(m_GameUI == null))
		{
			WorldToScreenPointNGUI(v3Pos, ref v3Pos);
			m_GameUI.AddDmgUI(fHeal, v3Pos, Color.green, gyUILabelDmg.kMode.Mode2);
		}
	}

	public void AddBulletText(int nCount, Vector3 v3Pos)
	{
		if (!(m_GameUI == null))
		{
			WorldToScreenPointNGUI(v3Pos, ref v3Pos);
			m_GameUI.AddDmgUI(nCount, v3Pos, Color.blue, gyUILabelDmg.kMode.Mode2);
		}
	}

	public Dictionary<int, CCharMob> GetMobData()
	{
		return m_MobMap;
	}

	public Dictionary<int, CCharPlayer> GetPlayerData()
	{
		return m_PlayerMap;
	}

	public int GetMobAliveCount()
	{
		if (m_MobMap.Count == 0)
		{
			return 0;
		}
		int num = 0;
		foreach (CCharMob value in m_MobMap.Values)
		{
			if (!value.isDead)
			{
				num++;
			}
		}
		return num;
	}

	public void ShakeCamera(float fShakeTime = 0.5f, float fShakeRange = 0.05f)
	{
		iCameraController cameraController = m_CameraTrail.GetCameraController();
		if (cameraController != null)
		{
			cameraController.Shake(fShakeTime, fShakeRange);
		}
	}

	public CStartPointManager GetTPManagerBegin()
	{
		return m_curTriggerManagerBegin;
	}

	public CStartPointManager GetTPManagerEnd()
	{
		return m_curTriggerManagerEnd;
	}

	public void InitTask(int nTaskID)
	{
		UnityEngine.Debug.Log("inittask " + nTaskID);
		CTaskInfo taskInfo = m_GameData.GetTaskInfo(nTaskID);
		if (taskInfo == null)
		{
			return;
		}
		switch (taskInfo.nType)
		{
		case 1:
		{
			if (CGameNetManager.GetInstance().IsRoomMaster())
			{
				CTaskInfoCollect cTaskInfoCollect = taskInfo as CTaskInfoCollect;
				if (cTaskInfoCollect == null || m_curTriggerManagerBegin == null)
				{
					break;
				}
				foreach (CTaskInfoCollect.CCollectInfo item in cTaskInfoCollect.ltCollectInfo)
				{
					List<Vector3> list = new List<Vector3>();
					for (int i = 0; i < item.nMaxCount; i++)
					{
						CStartPoint random = m_curTriggerManagerBegin.GetRandom();
						if (random == null)
						{
							continue;
						}
						int uID = MyUtils.GetUID();
						Vector3 v3GroundPos = random.GetRandom();
						GetGroundPos(v3GroundPos, ref v3GroundPos);
						AddItem(item.nItemID, v3GroundPos, Vector3.forward, -1f, uID);
						bool flag = true;
						foreach (Vector3 item2 in list)
						{
							if (Vector3.Distance(v3GroundPos, item2) < 5f)
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							list.Add(v3GroundPos);
							switch (CurGameLevelInfo.sSceneName)
							{
							case "SceneForest":
								AddObject(255, v3GroundPos, Vector3.forward);
								break;
							case "SceneLava":
							case "SceneLava2":
								AddObject(256, v3GroundPos, Vector3.forward);
								break;
							case "SceneSnow":
								AddObject(258, v3GroundPos, Vector3.forward);
								break;
							case "SceneIce":
								AddObject(257, v3GroundPos, Vector3.forward);
								break;
							default:
								AddObject(255, v3GroundPos, Vector3.forward);
								break;
							}
						}
					}
				}
			}
			Dictionary<int, CStartPoint> data = m_curTriggerManagerEnd.GetData();
			if (data == null)
			{
				break;
			}
			{
				foreach (KeyValuePair<int, CStartPoint> item3 in data)
				{
					CPointScreenTip cPointScreenTip = new CPointScreenTip();
					cPointScreenTip.agent = new GameObject("triggerend_" + item3.Key);
					if (!(cPointScreenTip.agent == null))
					{
						cPointScreenTip.agent.transform.position = item3.Value.GetCenter();
						cPointScreenTip.screentip = m_GameUI.CreateScreenTip(m_User.gameObject, cPointScreenTip.agent);
						if (cPointScreenTip.screentip == null)
						{
							Object.Destroy(cPointScreenTip.agent);
							continue;
						}
						cPointScreenTip.screentip.SetIcon("baoxiang");
						cPointScreenTip.screentip.isActive = false;
						m_ltScreenTipTriggerEnd.Add(cPointScreenTip);
						Vector3 v3GroundPos2 = cPointScreenTip.agent.transform.position;
						GetGroundPos(v3GroundPos2, ref v3GroundPos2);
						AddObject(254, v3GroundPos2, Vector3.forward);
					}
				}
				break;
			}
		}
		case 3:
		{
			CTaskInfoDefence cTaskInfoDefence = taskInfo as CTaskInfoDefence;
			if (cTaskInfoDefence == null)
			{
				break;
			}
			Object @object = Resources.Load("Artist/Custom/" + CurGameLevelInfo.sSceneName + "_ZhaLan");
			if (!(@object != null))
			{
				break;
			}
			GameObject gameObject = Object.Instantiate(@object) as GameObject;
			if (gameObject != null)
			{
				m_Building = gameObject.GetComponent<iBuilding>();
				if (m_Building != null)
				{
					m_Building.Initialize(cTaskInfoDefence.fLife);
					m_Building.SetHP(cTaskInfoDefence.fLife);
				}
			}
			break;
		}
		case 2:
			break;
		}
	}

	public bool IsMyself(CCharBase target)
	{
		if (target == null || target != m_User)
		{
			return false;
		}
		return true;
	}

	public bool IsPlayerAllDead()
	{
		if (CGameNetManager.GetInstance().IsConnected())
		{
			Dictionary<int, CGameNetManager.CNetUserInfo> netUserInfoData = CGameNetManager.GetInstance().GetNetUserInfoData();
			if (netUserInfoData != null)
			{
				foreach (CGameNetManager.CNetUserInfo value in netUserInfoData.Values)
				{
					if (value.m_bLeaved || !value.m_bRevive)
					{
						continue;
					}
					return false;
				}
			}
		}
		foreach (CCharPlayer value2 in m_PlayerMap.Values)
		{
			if (!value2.isDead)
			{
				return false;
			}
		}
		return true;
	}

	public List<Vector3> GetNavMeshPath(Vector3 point1, Vector3 point2)
	{
		List<Vector3> list = new List<Vector3>();
		if (!UnityEngine.AI.NavMesh.CalculatePath(point1, point2, -1, m_NavPath))
		{
			return list;
		}
		for (int i = 0; i < m_NavPath.corners.Length; i++)
		{
			list.Add(m_NavPath.corners[i]);
		}
		return list;
	}

	public bool IsAssistAim()
	{
		return m_AssistAimState != kAssistAimState.None;
	}

	public void AssistAim_Start(float fDelayTime = 0.5f)
	{
	}

	public void AssistAim_Stop()
	{
	}

	protected void UpdateAssistAim(float deltaTime)
	{
	}

	public void ShowItemScreenTip(bool bShow)
	{
		foreach (GameObject item in m_ltItem)
		{
			if (!(item == null))
			{
				iItem component = item.GetComponent<iItem>();
				if (!(component == null) && !(component.m_ScreenTip == null))
				{
					component.m_ScreenTip.isActive = bShow;
				}
			}
		}
	}

	public void ShowTriggerEndScreenTip(bool bShow)
	{
		foreach (CPointScreenTip item in m_ltScreenTipTriggerEnd)
		{
			if (!(item.screentip == null))
			{
				item.screentip.isActive = bShow;
			}
		}
	}

	public bool IsLevelUp(int nExp)
	{
		CCharacterInfoLevel curCharInfoLevel = m_User.CurCharInfoLevel;
		if (curCharInfoLevel == null)
		{
			return false;
		}
		if (curCharInfoLevel.nExp > m_User.EXP + nExp)
		{
			return false;
		}
		return true;
	}

	public void PlayAudio(Vector3 v3Pos, string sAuido)
	{
		GameObject gameObject = new GameObject("tempsound");
		if (!(gameObject == null))
		{
			TAudioController tAudioController = gameObject.AddComponent<TAudioController>();
			if (!(tAudioController == null))
			{
				tAudioController.PlayAudio(sAuido);
				Object.Destroy(gameObject, 2f);
			}
		}
	}

	public void SetGamePause(bool bPause)
	{
		if ((bPause && m_Status != kGameStatus.Gameing) || (bPause && TNetManager.GetInstance().Connection != null))
		{
			return;
		}
		if (bPause)
		{
			m_LastStatus = m_Status;
			m_Status = kGameStatus.Pause;
			if (m_User != null)
			{
				m_User.SetFire(false);
				m_User.MoveStop();
			}
			if (OpenClikPlugin.IsAdReady())
			{
				OpenClikPlugin.Show(false);
			}
			CSoundScene.GetInstance().StopBGM();
		}
		else
		{
			if (m_LastStatus != 0)
			{
				m_Status = m_LastStatus;
			}
			OpenClikPlugin.Hide();
			CSoundScene.GetInstance().PlayBGM(string.Empty);
		}
		SetPause(bPause);
		m_GameUI.ShowPauseUI(bPause);
	}

	public void SetPause(bool bPause)
	{
		if (/*(TNetManager.GetInstance().Connection != null && bPause) || */m_bPause == bPause)
		{
			return;
		}
		m_bPause = bPause;
		AndroidReturnPlugin.instance.SetIngamePause(m_bPause);
		foreach (CCharMob value in m_MobMap.Values)
		{
			if (!(value == null))
			{
				value.SetActive(!bPause);
			}
		}
		foreach (CCharPlayer value2 in m_PlayerMap.Values)
		{
			if (!(value2 == null))
			{
				value2.SetActive(!bPause);
			}
		}
	}

	public void FullWeaponBullet()
	{
		if (m_User == null)
		{
			return;
		}
		for (int i = 0; i < 3; i++)
		{
			CWeaponBase weapon = m_GameState.GetWeapon(i);
			if (weapon != null && weapon.BulletNum != weapon.BulletNumMax)
			{
				int nCount = weapon.BulletNumMax - weapon.BulletNum;
				weapon.FullBullet();
				if (weapon == m_User.GetWeapon())
				{
					weapon.RefreshBulletUI(m_User, true);
					AddBulletText(nCount, m_User.GetBone(0).position);
				}
			}
		}
	}

	public void StartIAPPurchase(int nNeed)
	{
		Debug.Log("StartIAPPurchase " + m_Status);
		m_LastStatus = m_Status;
		m_Status = kGameStatus.Gameing_IAP;
		m_GameUI.ShowIAPUI(true, nNeed);
		SetPause(true);
		if (m_bWaitingRevive)
		{
			m_GameUI.PauseReviveTime(true);
		}
	}

	public void FinishIAPPurchase()
	{
		if (m_LastStatus != 0)
		{
			m_Status = m_LastStatus;
			m_LastStatus = kGameStatus.None;
		}
		m_GameUI.ShowIAPUI(false);
		SetPause(false);
		if (m_bWaitingRevive)
		{
			m_GameUI.PauseReviveTime(false);
		}
	}

	protected void IAPPurchaseSuccess()
	{
		CUISound.GetInstance().Play("UI_Button");
		if (m_LastStatus != 0)
		{
			m_Status = m_LastStatus;
			m_LastStatus = kGameStatus.None;
		}
		m_GameUI.ShowIAPUI(false);
		SetPause(false);
		if (m_bWaitingRevive)
		{
			m_GameUI.PauseReviveTime(false);
			m_GameUI.ResetReviveTime();
			m_fWaitingReviveTimeCount = 0f;
		}
		Debug.Log(m_LastStatus);
	}

	protected void IAPPurchaseFailed()
	{
		CUISound.GetInstance().Play("UI_Button");
		if (m_LastStatus != 0)
		{
			m_Status = m_LastStatus;
			m_LastStatus = kGameStatus.None;
		}
		m_GameUI.ShowIAPUI(false);
		SetPause(false);
		if (m_bWaitingRevive)
		{
			m_GameUI.PauseReviveTime(false);
			m_GameUI.ResetReviveTime();
			m_fWaitingReviveTimeCount = 0f;
		}
	}

	public void PurchaseIAP(int nIAP)
	{
		CIAPInfo iAPInfo = m_GameData.GetIAPInfo(nIAP);
		if (iAPInfo == null)
		{
			OnPurchaseIAPFailed();
			return;
		}
		iServerSaveData.GetInstance().IsBackgroundUpload = false;
		iServerSaveData.GetInstance().IsBackgroundBack = false;
		iServerSaveData.GetInstance().IsBackgroundRelogin = false;
		iIAPManager.GetInstance().StartGooglePurchase(iAPInfo.sKey, OnPurchaseIAPSuccess, OnPurchaseIAPFailed, OnPurchaseIAPCancel, OnPurchaseIAPNetError);
	}

	public void OnPurchaseIAPSuccess(string sIAPKey, string sIdentifier, string sReceipt, string sSignature)
	{
		iServerSaveData.GetInstance().IsBackgroundUpload = true;
		iServerSaveData.GetInstance().IsBackgroundBack = true;
		iServerSaveData.GetInstance().IsBackgroundRelogin = false;
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter != null)
		{
			dataCenter.AddIAPTransactionInfo(sIAPKey, sIdentifier, sReceipt, sSignature, string.Empty, 0, 0, 0);
			iGameApp.GetInstance().SaveData();
			iServerIAPVerify.GetInstance().VerifyIAP(sIAPKey, sIdentifier, sReceipt, sSignature, OnIAPVerifySuccess, OnIAPVerifyFailed, OnIAPVerifyNetError, iGameApp.GetInstance().OnPurchaseIAP, OnIAPWriteIdentifier, OnIAPDeleteIdentifier);
			m_GameUI.ShowMessageBox("Please wait while the system is verifying your purchase...", gyUIMessageBox.kMessageBoxType.None);
		}
	}

	public void OnPurchaseIAPFailed()
	{
		iServerSaveData.GetInstance().IsBackgroundUpload = true;
		iServerSaveData.GetInstance().IsBackgroundBack = true;
		iServerSaveData.GetInstance().IsBackgroundRelogin = false;
		CUISound.GetInstance().Play("UI_Error");
		m_GameUI.ShowMessageBox("Purchase Failed!", gyUIMessageBox.kMessageBoxType.OK, IAPPurchaseFailed);
	}

	public void OnPurchaseIAPCancel()
	{
		iServerSaveData.GetInstance().IsBackgroundUpload = true;
		iServerSaveData.GetInstance().IsBackgroundBack = true;
		iServerSaveData.GetInstance().IsBackgroundRelogin = false;
		CUISound.GetInstance().Play("UI_Error");
		IAPPurchaseFailed();
		m_GameUI.HideMessageBox();
	}

	public void OnPurchaseIAPNetError()
	{
		CUISound.GetInstance().Play("UI_Error");
		m_GameUI.ShowMessageBox("Connect Error!", gyUIMessageBox.kMessageBoxType.OK, IAPPurchaseFailed);
	}

	protected void OnIAPVerifySuccess(string sKey, string sIdentifier, string sReceipt)
	{
		CUISound.GetInstance().Play("UI_Crystal");
		m_GameUI.ShowMessageBox("Purchase Successed!", gyUIMessageBox.kMessageBoxType.OK, IAPPurchaseSuccess);
	}

	protected void OnIAPVerifyFailed(string sKey, string sIdentifier, string sReceipt)
	{
		CUISound.GetInstance().Play("UI_Error");
		m_GameUI.ShowMessageBox("Connect Error!", gyUIMessageBox.kMessageBoxType.OK, IAPPurchaseFailed);
	}

	protected void OnIAPVerifyNetError(string sKey)
	{
		CUISound.GetInstance().Play("UI_Error");
		m_GameUI.ShowMessageBox("Connect Error!", gyUIMessageBox.kMessageBoxType.OK, IAPPurchaseFailed);
	}

	protected void OnIAPWriteIdentifier(string sKey, string sIdentifier, string sReceipt, string sSignature, string sRandom, int nRat, int nRatA, int nRatB)
	{
		iGameData gameData = iGameApp.GetInstance().m_GameData;
		if (gameData != null)
		{
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter != null)
			{
				dataCenter.AddIAPTransactionInfo(sKey, sIdentifier, sReceipt, sSignature, sRandom, nRat, nRatA, nRatB);
				iGameApp.GetInstance().SaveData();
			}
		}
	}

	protected void OnIAPDeleteIdentifier(string sKey, string sIdentifier, string sReceipt)
	{
		iGameData gameData = iGameApp.GetInstance().m_GameData;
		if (gameData != null)
		{
			iDataCenter dataCenter = gameData.GetDataCenter();
			if (dataCenter != null)
			{
				dataCenter.DelIAPTransactionInfo(sKey, sIdentifier, sReceipt);
				iGameApp.GetInstance().SaveData();
			}
		}
	}

	public void Boom(Vector3 v3Pos, float fRadius, int[] arrFunc, int[] arrValueX, int[] arrValueY, string sAudio, int nEffectHit, CCharBase ignoretarget = null, CCharBase actor = null, int targetlimit = 2)
	{
		PlayAudio(v3Pos, sAudio);
		AddEffect(v3Pos, Vector3.forward, 2f, nEffectHit);
		List<CCharBase> unitList = GetUnitList();
		if (unitList == null)
		{
			return;
		}
		foreach (CCharBase item in unitList)
		{
			if (item == null || item.isDead || item == ignoretarget)
			{
				continue;
			}
			if (actor != null)
			{
				switch (targetlimit)
				{
				case 2:
					if (actor.IsAlly(item))
					{
						continue;
					}
					break;
				case 1:
					if (!actor.IsAlly(item))
					{
						continue;
					}
					break;
				case 3:
					if (actor != item)
					{
						continue;
					}
					break;
				}
			}
			if (Vector3.Distance(v3Pos, item.Pos) > fRadius)
			{
				continue;
			}
			iGameLogic.HitInfo hitinfo = new iGameLogic.HitInfo();
			hitinfo.v3HitPos = v3Pos;
			hitinfo.v3HitDir = item.Pos - v3Pos;
			m_GameLogic.CaculateFunc(actor, item, arrFunc, arrValueX, arrValueY, ref hitinfo);
			if (item.isDead)
			{
				CCharMob cCharMob = item as CCharMob;
				CCharUser cCharUser = actor as CCharUser;
				if (cCharUser != null && cCharMob != null)
				{
					CMobInfoLevel mobInfo = cCharMob.GetMobInfo();
					if (mobInfo != null)
					{
						int num = mobInfo.nExp;
						float value = cCharUser.Property.GetValue(kProEnum.Char_IncreaseExp);
						if (value > 0f)
						{
							num = (int)((float)num * (1f + value / 100f));
						}
						cCharUser.AddExp(num);
						AddExpText(num, v3Pos);
					}
					cCharMob.m_v3DeadDirection = item.Pos - v3Pos;
					cCharMob.m_v3DeadDirection.y = 0f;
					cCharMob.m_v3DeadDirection.Normalize();
				}
			}
			item.PlayAudio(kAudioEnum.HitBody);
		}
	}

	public bool GetGroundPos(Vector3 v3Pos, ref Vector3 v3GroundPos)
	{
		v3Pos.y += 10f;
		Ray ray = new Ray(v3Pos, Vector3.down);
		RaycastHit hitInfo;
		if (!Physics.Raycast(ray, out hitInfo, 15f, 536870912))
		{
			return false;
		}
		v3GroundPos = hitInfo.point;
		return true;
	}

	public void StartRevive(float fTime)
	{
		Debug.Log("StartRevive " + fTime);
		if (!m_bMutiplyGame)
		{
			m_bWaitingRevive = true;
			m_fWaitingReviveTime = fTime;
			m_fWaitingReviveTimeCount = 0f;
			if (m_GameUI != null)
			{
				CSoundScene.GetInstance().StopBGM();
				CSoundScene.GetInstance().StopAmbienceBGM();
				m_GameUI.ShowRevive(true, 5f);
				m_GameUI.ShowBlackWarning(false);
			}
			SetPause(true);
			return;
		}
		m_bWaitingRevive = true;
		m_fWaitingReviveTime = 0f;
		m_fWaitingReviveTimeCount = 0f;
		if (m_GameUI != null)
		{
			m_GameUI.ShowReviveMutiply(true, m_DataCenter.Crystal >= 10);
			if (m_GameUI.UIManager.mWheelMove != null)
			{
				m_GameUI.UIManager.mWheelMove.gameObject.SetActiveRecursively(false);
			}
			if (m_GameUI.UIManager.mWheelShoot != null)
			{
				m_GameUI.UIManager.mWheelShoot.gameObject.SetActiveRecursively(false);
			}
			if (m_GameUI.UIManager.mWeapon != null)
			{
				m_GameUI.UIManager.mWeapon.gameObject.SetActiveRecursively(false);
			}
			if (m_GameUI.UIManager.mSkill != null)
			{
				m_GameUI.UIManager.mSkill.gameObject.SetActiveRecursively(false);
			}
			if (m_GameUI.UIManager.mFastWeapon != null)
			{
				m_GameUI.UIManager.mFastWeapon.gameObject.SetActiveRecursively(false);
			}
			m_GameUI.ShowAimCross(false);
			m_GameUI.ShowBlackWarning(false);
		}
	}

	public void FinishRevive(bool bSuccess = true)
	{
		m_bWaitingRevive = false;
		if (!CGameNetManager.GetInstance().IsConnected())
		{
			SetPause(false);
		}
		if (!bSuccess)
		{
			if (!m_bMutiplyGame)
			{
				m_GameUI.ShowRevive(false, 0f);
			}
			else
			{
				m_GameUI.ShowReviveMutiply(false);
			}
			return;
		}
		if (!m_bMutiplyGame)
		{
			m_GameUI.ShowRevive(false, 0f);
			CSoundScene.GetInstance().PlayBGM(GetBGM());
			CSoundScene.GetInstance().PlayAmbienceBGM(m_curGameLevelInfo.sBGMAmbience);
		}
		else
		{
			m_GameUI.ShowReviveMutiply(false);
		}
		m_GameUI.ShowGameUI();
		m_CameraTrail.Active = true;
		m_CameraFocus.Active = false;
		m_CameraReveal.Active = false;
		if (m_bObserve)
		{
			StopOBCamera();
		}
		if (!m_User.isDead)
		{
			return;
		}
		iDataCenter dataCenter = m_GameData.GetDataCenter();
		if (dataCenter == null)
		{
			return;
		}
		dataCenter.AddCrystal(-10);
		CAchievementManager.GetInstance().AddAchievement(13);
		CTrinitiCollectManager.GetInstance().SendRevive();
		CTrinitiCollectManager.GetInstance().SendConsumeCrystal(10, "rel", -1, -1);
		dataCenter.Save();
		CUISound.GetInstance().Play("UI_Crystal");
		m_User.Revive(m_User.MaxHP);
		m_User.AddBuff(10, 4f);
		m_User.ResetSkillCD();
		if (m_bIsSkyScene && CurGameLevelInfo != null)
		{
			Vector3 pos = m_User.Pos;
			pos.y = CurGameLevelInfo.fNavPlane;
			m_User.Pos = pos + Vector3.up * 0.1f;
		}
		else
		{
			Vector3 pos2 = m_User.Pos;
			RaycastHit hitInfo;
			if (Physics.Raycast(new Ray(m_User.Pos + Vector3.up * 100f, Vector3.down), out hitInfo, 120f, 536870912))
			{
				pos2.y = hitInfo.point.y;
			}
			m_User.Pos = pos2 + Vector3.up * 0.1f;
		}
		CFlurryManager.GetInstance().ConsumeCrystal(CFlurryManager.kConsumeType.Revive);
		if (CurGameLevelInfo != null)
		{
			iGameApp.GetInstance().Flurry_CharRevive(CurGameLevelInfo.nID);
		}
	}

	public void ShowGameOverUI_Fail()
	{
		m_GameOverUIStatus = kGameOverUIStatus.Fail;
		m_GameUI.ShowFailed(true);
		float lstRate = 0f;
		float curRate = 0f;
		int num = 1;
		int num2 = 1;
		if (m_GameData.m_CharacterCenter != null)
		{
			CCharacterInfo cCharacterInfo = m_GameData.m_CharacterCenter.Get(m_User.ID);
			if (cCharacterInfo != null)
			{
				num = m_GameState.m_nLstLevel;
				lstRate = cCharacterInfo.GetExpRate(m_GameState.m_nLstExp, num);
				num2 = m_GameState.m_nCurLevel;
				curRate = cCharacterInfo.GetExpRate(m_GameState.m_nCurExp, num2);
			}
		}
		if (m_GameUI.UIManager != null && m_GameUI.UIManager.mPanelMissionFailedMutiply != null)
		{
			m_GameUI.UIManager.mPanelMissionFailed.SetCharExp(num, lstRate, num2, curRate);
		}
		if (OpenClikPlugin.IsAdReady())
		{
			OpenClikPlugin.Show(false);
		}
	}

	public void ShowGameOverUI_Fail_Mutiply()
	{
		m_GameOverUIStatus = kGameOverUIStatus.Fail_Mutiply;
		m_GameUI.ShowFailedMutiply(true);
		float lstRate = 0f;
		float curRate = 0f;
		float lstRate2 = 0f;
		float curRate2 = 0f;
		int num = 1;
		int num2 = 1;
		int num3 = 1;
		int num4 = 1;
		CGameNetManager.CNetUserInfo netUserInfo = CGameNetManager.GetInstance().GetNetUserInfo();
		if (netUserInfo != null)
		{
			if (m_GameData.m_CharacterCenter != null)
			{
				CCharacterInfo cCharacterInfo = m_GameData.m_CharacterCenter.Get(netUserInfo.m_nCharID);
				if (cCharacterInfo != null)
				{
					num = netUserInfo.m_nCharLvl;
					lstRate = cCharacterInfo.GetExpRate(netUserInfo.m_nCharExp, num);
					num2 = netUserInfo.m_nCharLvlResult;
					curRate = cCharacterInfo.GetExpRate(netUserInfo.m_nCharExpResult, num2);
				}
			}
			if (m_GameData.m_HunterCenter != null)
			{
				num3 = netUserInfo.m_nHunterLvl;
				lstRate2 = m_GameData.m_HunterCenter.GetExpRate(netUserInfo.m_nHunterExp, num3 + 1);
				num4 = netUserInfo.m_nHunterLvlResult;
				curRate2 = m_GameData.m_HunterCenter.GetExpRate(netUserInfo.m_nHunterExpResult, num4 + 1);
			}
		}
		if (m_GameUI.UIManager != null && m_GameUI.UIManager.mPanelMissionFailedMutiply != null)
		{
			m_GameUI.UIManager.mPanelMissionFailedMutiply.SetCharExp(num, lstRate, num2, curRate);
			m_GameUI.UIManager.mPanelMissionFailedMutiply.SetHunterExp(num3, lstRate2, num4, curRate2);
		}
		if (OpenClikPlugin.IsAdReady())
		{
			OpenClikPlugin.Show(false);
		}
	}

	public void ShowGameOverUI_Win()
	{
		m_GameOverUIStatus = kGameOverUIStatus.Win;
		m_GameUI.ShowSuccess(true);
		float curRate = 0f;
		float lstRate = 0f;
		int curLevel = 1;
		int lstLevel = 1;
		if (m_User != null && m_GameData.m_CharacterCenter != null)
		{
			CCharacterInfo cCharacterInfo = m_GameData.m_CharacterCenter.Get(m_User.ID);
			if (cCharacterInfo != null)
			{
				lstLevel = m_GameState.m_nLstLevel;
				lstRate = cCharacterInfo.GetExpRate(m_GameState.m_nLstExp, m_GameState.m_nLstLevel);
				curLevel = m_GameState.m_nCurLevel;
				curRate = cCharacterInfo.GetExpRate(m_GameState.m_nCurExp, m_GameState.m_nCurLevel);
			}
		}
		if (m_GameUI.UIManager != null && m_GameUI.UIManager.mPanelMissionComplete != null)
		{
			m_GameUI.UIManager.mPanelMissionComplete.SetCharExp(lstLevel, lstRate, curLevel, curRate);
			m_GameUI.UIManager.mPanelMissionComplete.SetGainGold(m_GameState.m_nLevelRewardGold + m_GameState.GainGoldInGame);
			m_GameUI.UIManager.mPanelMissionComplete.SetGainCrystal(m_GameState.GainCrystalInGame);
		}
		if (OpenClikPlugin.IsAdReady())
		{
			OpenClikPlugin.Show(false);
		}
	}

	public void HideGameOverUI_Win()
	{
		m_GameOverUIStatus = kGameOverUIStatus.None;
		m_GameUI.ShowSuccess(false);
	}

    public void ShowGameOverUI_Win_Mutiply()
    {
        int crystals = Mathf.Clamp(Mathf.FloorToInt(bossHP / 1000f / Mathf.Clamp(bossLvl / 4, 1, 15)), 2, 10);
        m_GameState.AddCrystal(crystals);
        m_DataCenter.AddCrystal(crystals);
        int num2 = m_GameState.m_nLevelRewardGold + m_GameState.GainGoldInGame;
        int nBeadmireCount = 0;
		if (m_DataCenter != null)
		{
			nBeadmireCount = m_DataCenter.BeAdmire;
		}
		float num3 = 1f;
		CCharacterInfoLevel characterInfo = m_GameData.GetCharacterInfo(m_User.ID, m_User.Level + 0);
		if (characterInfo != null)
		{
			num3 = Mathf.Clamp01((float)m_User.EXP / (float)characterInfo.nExp);
		}
		int nCombatRatings = CalcCombatRating();
		int nGainCrystalInGame = m_GameState.GainCrystalInGame;
		int nGainGoldInGame = num2;
		int nCharLvlResult = m_User.Level;
		int nCharExpResult = m_User.EXP;
		int nHunterLvlResult = m_DataCenter.HunterLvl;
		int nHunterExpResult = m_DataCenter.HunterExp;
		m_GameOverUIStatus = kGameOverUIStatus.Win_Mutiply;
		m_GameUI.ShowSuccessMutiply(true);
        m_GameState.m_nMVPGold = Mathf.FloorToInt((float)nGainGoldInGame * 0.4f);
        m_GameState.m_nMVPCrystal = Mathf.FloorToInt((float)nGainCrystalInGame * 0.4f);
        m_DataCenter.AddGold(m_GameState.m_nMVPGold);
		m_DataCenter.AddCrystal(m_GameState.m_nMVPCrystal);
		gyUIPlayerRewards panelMissionSuccessMutiply_PlayerRewards = m_GameUI.GetPanelMissionSuccessMutiply_PlayerRewards(0);
		if (panelMissionSuccessMutiply_PlayerRewards == null)
		{
			return;
		}
		if (panelMissionSuccessMutiply_PlayerRewards.m_PlayerName != null)
		{
			panelMissionSuccessMutiply_PlayerRewards.m_PlayerName.text = m_User.m_sName;
		}
		//if (panelMissionSuccessMutiply_PlayerRewards.m_PlayerPhoto != null)
		//{
		//	CNameCardInfo nameCardInfo = m_DataCenterNet.GetNameCardInfo(m_User.m_sDeivceId);
		//	if (nameCardInfo != null && nameCardInfo.GetPhoto() != null)
		//	{
		//		iGameApp.GetInstance().ScreenLog(nameCardInfo.m_sNickName + " set photo");
		//		panelMissionSuccessMutiply_PlayerRewards.m_PlayerPhoto.mainTexture = nameCardInfo.GetPhoto();
		//	}
		//}
		if (panelMissionSuccessMutiply_PlayerRewards.m_PlayerTitle != null)
		{
			panelMissionSuccessMutiply_PlayerRewards.m_PlayerTitle.text = string.Empty;
			if (m_GameData.m_TitleCenter != null)
			{
				CTitleInfo cTitleInfo = m_GameData.m_TitleCenter.Get(m_DataCenter.Title);
				if (cTitleInfo != null)
				{
					panelMissionSuccessMutiply_PlayerRewards.m_PlayerTitle.text = cTitleInfo.sName;
				}
			}
		}
		if (panelMissionSuccessMutiply_PlayerRewards.m_AdmireCount != null)
		{
			panelMissionSuccessMutiply_PlayerRewards.m_AdmireCount.Value = nBeadmireCount;
		}
		if (panelMissionSuccessMutiply_PlayerRewards.m_BattleRate != null)
		{
			panelMissionSuccessMutiply_PlayerRewards.m_BattleRate.Value = nCombatRatings;
		}
		if (panelMissionSuccessMutiply_PlayerRewards.m_Gold != null)
		{
			panelMissionSuccessMutiply_PlayerRewards.m_Gold.Value = nGainGoldInGame;
		}
		if (panelMissionSuccessMutiply_PlayerRewards.m_Crystal != null)
		{
			panelMissionSuccessMutiply_PlayerRewards.m_Crystal.Value = nGainCrystalInGame;
		}
		if (panelMissionSuccessMutiply_PlayerRewards.m_CharExp != null)
		{
			float barValue = 0f;
			float fCurRate = 0f;
			int num4 = 1;
			int num5 = 1;
			CCharacterInfo characterInfo2 = m_GameData.GetCharacterInfo(m_DataCenter.CurCharID);
			if (characterInfo2 != null)
			{
				num4 = m_DataCenter.GetCharacter(m_DataCenter.CurCharID).nLevel;
				barValue = characterInfo2.GetExpRate(m_DataCenter.GetCharacter(m_DataCenter.CurCharID).nExp, num4);
				num5 = m_User.Level;
				fCurRate = characterInfo2.GetExpRate(m_User.EXP, num5);
			}
			panelMissionSuccessMutiply_PlayerRewards.m_CharExp.Level = num4;
			panelMissionSuccessMutiply_PlayerRewards.m_CharExp.BarValue = barValue;
			panelMissionSuccessMutiply_PlayerRewards.m_CharExp.SetAnimation(fCurRate, num5);
		}
		if (panelMissionSuccessMutiply_PlayerRewards.m_HunterExp != null)
		{
			float barValue2 = 0f;
			float fCurRate2 = 0f;
			int num4 = 1;
			int num5 = 1;
			if (m_GameData.m_HunterCenter != null)
			{
				num4 = m_DataCenter.HunterLvl;
				barValue2 = m_GameData.m_HunterCenter.GetExpRate(m_DataCenter.HunterExp, num4 + 1);
				num5 = m_DataCenter.HunterLvl;
				fCurRate2 = m_GameData.m_HunterCenter.GetExpRate(m_DataCenter.HunterExp, num5 + 1);
			}
			panelMissionSuccessMutiply_PlayerRewards.m_HunterExp.Level = num4;
			panelMissionSuccessMutiply_PlayerRewards.m_HunterExp.BarValue = barValue2;
			panelMissionSuccessMutiply_PlayerRewards.m_HunterExp.SetAnimation(fCurRate2, num5);
		}
		if (m_User != null && m_User.isDead)
		{
			panelMissionSuccessMutiply_PlayerRewards.m_PlayerDeathFlag.enabled = true;
		}
		else
		{
			panelMissionSuccessMutiply_PlayerRewards.m_PlayerDeathFlag.enabled = false;
		}
		panelMissionSuccessMutiply_PlayerRewards.Show(true);
		//if (cNetUserInfo == CGameNetManager.GetInstance().GetNetUserInfo())
		//{
			Debug.Log("hide admire");
			panelMissionSuccessMutiply_PlayerRewards.m_Admire.Enable = false;
		//}
	}

	public void HideGameOverUI_Win_Mutiply()
	{
		m_GameOverUIStatus = kGameOverUIStatus.None;
		m_GameUI.ShowSuccessMutiply(false);
	}

	public void ShowGameOverUI_LvlUp()
	{
		m_GameOverUIStatus = kGameOverUIStatus.LvlUp;
		m_GameUI.ShowLevelUp(true);
	}

	public void HideGameOverUI_LvlUp()
	{
		m_GameOverUIStatus = kGameOverUIStatus.None;
		m_GameUI.ShowLevelUp(false);
	}

	public void ShowGameOverUI_Material()
	{
		m_GameOverUIStatus = kGameOverUIStatus.Material;
		m_GameUI.ShowMaterial(true);
		OpenClikPlugin.Hide();
	}

	public void HideGameOverUI_Material()
	{
		m_GameOverUIStatus = kGameOverUIStatus.None;
		m_GameUI.ShowMaterial(false);
	}

	public void AddMyDamage(float dmg, float curLife)
	{
		m_fCombatRatingData_DamageMy += ((!(dmg < curLife)) ? curLife : dmg);
	}

	public int CalcCombatRating()
	{
		if (m_fCombatRatingData_DamageTotal == 0f)
		{
			return 0;
		}
		return Mathf.FloorToInt(Mathf.Clamp01(m_fCombatRatingData_DamageMy / m_fCombatRatingData_DamageTotal) * 1000f);
	}

	public void StartUserDeathCamera()
	{
		m_bUserDeath = true;
		m_fUserDeathTime = 2f;
		m_fUserDeathTimeCount = 0f;
		m_CameraFocus.Active = false;
		m_CameraTrail.Active = false;
		m_CameraReveal.Active = true;
		m_CameraReveal.Go(m_User.GetBone(1).gameObject, 70f, 5f, 2f);
	}

	public void StartOBCamera(CCharBase player)
	{
		m_bObserve = true;
		m_CameraFocus.Active = false;
		m_CameraReveal.Active = false;
		m_CameraTrail.Active = true;
		m_CameraTrail.m_Target = player;
	}

	public void StopOBCamera()
	{
		m_bObserve = false;
		m_CameraTrail.Active = true;
		m_CameraTrail.m_Target = m_User;
	}
}
