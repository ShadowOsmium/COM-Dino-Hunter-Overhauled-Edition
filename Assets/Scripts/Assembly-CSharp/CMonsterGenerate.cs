using UnityEngine;

public class CMonsterGenerate
{
	public enum GenerateState
	{
		None,
		Destroy,
		Delay,
		Process
	}

	public int nNextWave = -1;

	protected iGameSceneBase m_GameScene;

	protected iGameData m_GameData;

	protected GenerateState m_State;

	protected WaveInfo m_curWaveInfo;

	protected int m_nCurIndex;

	protected int m_nSequence;

	protected float m_fTimeCount;

	public int WaveID
	{
		get
		{
			if (m_curWaveInfo == null)
			{
				return -1;
			}
			return m_curWaveInfo.nID;
		}
	}

	public GenerateState State
	{
		get
		{
			return m_State;
		}
	}

	public CMonsterGenerate()
	{
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		m_GameData = iGameApp.GetInstance().m_GameData;
		m_State = GenerateState.None;
	}

	public void Initialize(int nWaveID)
	{
		m_curWaveInfo = m_GameData.GetWaveInfo(nWaveID);
		if (m_curWaveInfo != null)
		{
			m_nCurIndex = 0;
			m_nSequence = 0;
			if (m_curWaveInfo.m_fDelayTime > 0f)
			{
				m_State = GenerateState.Delay;
				m_fTimeCount = 0f;
			}
			else
			{
				m_State = GenerateState.Process;
				m_fTimeCount = m_curWaveInfo.m_fInterval;
			}
		}
	}

	public void Update(float deltaTime)
	{
		if (m_State == GenerateState.None || m_State == GenerateState.Destroy)
		{
			return;
		}
		switch (m_State)
		{
		case GenerateState.Delay:
		{
			m_fTimeCount += deltaTime;
			if (m_fTimeCount < m_curWaveInfo.m_fDelayTime)
			{
				return;
			}
			m_State = GenerateState.Process;
			m_fTimeCount = m_curWaveInfo.m_fInterval;
			CTaskInfo taskInfo = m_GameData.GetTaskInfo(m_GameScene.m_nCurTaskID);
			if (taskInfo != null && taskInfo.nType == 3)
			{
				iGameUIBase gameUI = m_GameScene.GetGameUI();
				if (gameUI != null)
				{
					gameUI.ShowTip("G O !");
				}
			}
			break;
		}
		case GenerateState.Process:
			m_fTimeCount += deltaTime;
			if (m_fTimeCount < m_curWaveInfo.m_fInterval)
			{
				return;
			}
			m_fTimeCount = 0f;
			break;
		}
		for (int num = m_curWaveInfo.m_nNumAtOnce; num > 0; num--)
		{
			if (GenerateMob(m_nCurIndex))
			{
				m_nCurIndex++;
				m_nSequence++;
			}
		}
		if (m_nCurIndex < m_curWaveInfo.GetWaveMobCount())
		{
			return;
		}
		if (m_curWaveInfo.m_nLoop == -1)
		{
			m_State = GenerateState.Destroy;
		}
		else if (m_curWaveInfo.m_nLoop > 0)
		{
			m_State = GenerateState.Destroy;
			WaveInfo waveInfo = m_GameData.GetWaveInfo(m_curWaveInfo.m_nLoop);
			if (waveInfo != null)
			{
				nNextWave = m_curWaveInfo.m_nLoop;
			}
		}
		else
		{
			m_nCurIndex = 0;
		}
	}

	public bool GenerateMob(int nIndex)
	{
		WaveMobInfo waveMobInfo = null;
		waveMobInfo = ((!m_curWaveInfo.m_bRandom) ? m_curWaveInfo.GetWaveMobInfo(nIndex) : m_curWaveInfo.GetWaveMobInfoRandom());
		if (waveMobInfo == null)
		{
			return false;
		}
		Vector3 v3Pos = Vector3.zero;
		Vector3 onUnitSphere = Random.onUnitSphere;
		onUnitSphere.y = 0f;
		int num = 0;
		num = ((m_GameScene.m_curHunterLevelInfo == null) ? ((waveMobInfo.nLevel != 0) ? waveMobInfo.nLevel : m_curWaveInfo.nDefaultMobLevel) : Random.Range(m_GameScene.m_curHunterLevelInfo.m_nMonsterLvlMin, m_GameScene.m_curHunterLevelInfo.m_nMonsterLvlMax + 1));
		Debug.Log(waveMobInfo.nID + " " + num);
		CMobInfoLevel mobInfo = m_GameData.GetMobInfo(waveMobInfo.nID, num);
		if (mobInfo == null)
		{
			return false;
		}
		CStartPointManager cStartPointManager = null;
		int num2 = -1;
		CAIManagerInfo aIManagerInfo = m_GameData.GetAIManagerInfo(mobInfo.nAIManagerID);
		if (aIManagerInfo != null)
		{
			CAIInfo aIInfo = m_GameData.GetAIInfo(aIManagerInfo.nAI);
			if (aIInfo != null)
			{
				num2 = aIInfo.nBehavior;
			}
		}
		int num3 = num2;
		cStartPointManager = ((num3 != 2) ? m_GameScene.GetSPManagerGround() : m_GameScene.GetSPManagerSky());
		if (cStartPointManager != null)
		{
			CStartPoint cStartPoint = null;
			if (waveMobInfo.nStartPoint == 0)
			{
				CCharUser user = m_GameScene.GetUser();
				cStartPoint = ((!(user != null)) ? cStartPointManager.GetRandom() : cStartPointManager.GetRandomClosePoint(user.Pos, 3, 10f));
			}
			else
			{
				cStartPoint = cStartPointManager.Get(waveMobInfo.nStartPoint);
			}
			if (cStartPoint != null)
			{
				v3Pos = cStartPoint.GetRandom();
			}
		}
		if (CGameNetManager.GetInstance().IsRoomMaster())
		{
			int uID = MyUtils.GetUID();
			CCharMob cCharMob = m_GameScene.AddMobByWave(waveMobInfo.nID, num, uID, m_curWaveInfo.nID, m_nSequence, v3Pos, onUnitSphere);
			if (cCharMob == null)
			{
				return false;
			}
			if (CGameNetManager.GetInstance().IsConnected())
			{
				CGameNetSender.GetInstance().SendMsg_MGMANAGER_ADDMOB(waveMobInfo.nID, num, uID, m_curWaveInfo.nID, m_nSequence, v3Pos, onUnitSphere);
			}
		}
		return true;
	}
}
