using System.Collections.Generic;
using gyEvent;
using gyTaskSystem;
using UnityEngine;

public class CMonsterGenerateManager
{
	protected iGameData m_GameData;

	protected iGameSceneBase m_GameScene;

	protected iGameState m_GameState;

	protected List<int> m_ltWaveID;

	protected List<WaveInfo> m_ltWaveInfo;

	protected List<CMonsterGenerate> m_ltMonsterGenerate;

	protected List<CMonsterGenerate> m_ltMonsterGenerateDestroy;

	protected List<int> m_ltReadyToCreate;

	protected CEventManager m_EventManager;

	public CMonsterGenerateManager()
	{
		m_GameData = iGameApp.GetInstance().m_GameData;
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		m_GameState = iGameApp.GetInstance().m_GameState;
		m_ltWaveID = new List<int>();
		m_ltWaveInfo = new List<WaveInfo>();
		m_ltMonsterGenerate = new List<CMonsterGenerate>();
		m_ltMonsterGenerateDestroy = new List<CMonsterGenerate>();
		m_ltReadyToCreate = new List<int>();
		m_EventManager = new CEventManager();
	}

	public List<WaveInfo> GetWaveList()
	{
		return m_ltWaveInfo;
	}

	public void RegisterWaveID(int[] arrWaveID)
	{
		m_ltWaveID.Clear();
		for (int i = 0; i < arrWaveID.Length; i++)
		{
			m_ltWaveID.Add(arrWaveID[i]);
		}
	}

	public void Clear()
	{
		m_ltWaveID.Clear();
		m_ltWaveInfo.Clear();
		m_EventManager.Clear();
		m_ltMonsterGenerate.Clear();
		m_ltReadyToCreate.Clear();
	}

	public void Reset()
	{
		m_ltWaveInfo.Clear();
		m_EventManager.Clear();
		m_ltMonsterGenerate.Clear();
		m_ltReadyToCreate.Clear();
	}

	public void Start()
	{
		for (int i = 0; i < m_ltWaveID.Count; i++)
		{
			WaveInfo waveInfo = m_GameData.GetWaveInfo(m_ltWaveID[i]);
			if (waveInfo == null)
			{
				continue;
			}
			m_ltWaveInfo.Add(waveInfo);
			if (waveInfo.ltEventParam == null)
			{
				continue;
			}
			EventByFrame eventByFrame = new EventByFrame();
			eventByFrame.m_TriggerFunc = MGStart;
			eventByFrame.m_ltParam = new List<object> { waveInfo.nID };
			switch (waveInfo.nEventType)
			{
			case 0:
				if (waveInfo.ltEventParam.Count == 1)
				{
					eventByFrame.m_Compare = new EventCondition_Time(waveInfo.ltEventParam[0]);
				}
				break;
			case 1:
				if (waveInfo.ltEventParam.Count == 4)
				{
					eventByFrame.m_Compare = new EventCondition_MobByWave(waveInfo.ltEventParam[0], waveInfo.ltEventParam[1], waveInfo.ltEventParam[2], waveInfo.ltEventParam[3]);
				}
				break;
			case 2:
				if (waveInfo.ltEventParam.Count == 3)
				{
					eventByFrame.m_Compare = new EventCondition_MobByID(waveInfo.ltEventParam[0], waveInfo.ltEventParam[1], waveInfo.ltEventParam[2]);
				}
				break;
			case 3:
				if (waveInfo.ltEventParam.Count == 2)
				{
					eventByFrame.m_Compare = new EventCondition_StealEgg_Home(waveInfo.ltEventParam[0], waveInfo.ltEventParam[1]);
				}
				break;
			case 4:
				if (waveInfo.ltEventParam.Count == 2)
				{
					eventByFrame.m_Compare = new EventCondition_WaveNumberLeft(waveInfo.ltEventParam[0], waveInfo.ltEventParam[1]);
				}
				break;
			case 5:
				if (waveInfo.ltEventParam.Count == 2)
				{
					eventByFrame.m_Compare = new EventCondition_StealEgg_Take(waveInfo.ltEventParam[0], waveInfo.ltEventParam[1]);
				}
				break;
			default:
				continue;
			}
			m_EventManager.AddEvent(eventByFrame);
		}
	}

	protected bool MGStart(List<object> ltParam)
	{
		if (ltParam.Count < 1)
		{
			return true;
		}
		int num = (int)ltParam[0];
		Debug.Log("MGStart " + num);
		WaveInfo waveInfo = m_GameData.GetWaveInfo(num);
		if (waveInfo == null)
		{
			return true;
		}
		MGStart(num);
		return !waveInfo.bEventLoop;
	}

	public void MGStart(int nWaveID)
	{
		CMonsterGenerate cMonsterGenerate = new CMonsterGenerate();
		cMonsterGenerate.Initialize(nWaveID);
		m_ltMonsterGenerate.Add(cMonsterGenerate);
		if (m_GameScene == null)
		{
			return;
		}
		m_GameScene.StartWaveCG(nWaveID);
		if (m_GameScene.m_TaskManager == null)
		{
			return;
		}
		m_GameScene.m_TaskManager.OnWaveBegin();
		CTaskDefence cTaskDefence = m_GameScene.m_TaskManager.GetTask() as CTaskDefence;
		if (cTaskDefence != null)
		{
			iGameUIBase gameUI = m_GameScene.GetGameUI();
			if (gameUI != null)
			{
				gameUI.ShowTip("WAVE " + cTaskDefence.m_nCurWave);
			}
		}
	}

	public void Update(float deltaTime)
	{
		foreach (CMonsterGenerate item in m_ltMonsterGenerate)
		{
			item.Update(deltaTime);
			if (item.State == CMonsterGenerate.GenerateState.Destroy)
			{
				m_ltMonsterGenerateDestroy.Add(item);
				if (item.nNextWave > 0)
				{
					m_ltReadyToCreate.Add(item.nNextWave);
				}
			}
		}
		foreach (CMonsterGenerate item2 in m_ltMonsterGenerateDestroy)
		{
			m_ltMonsterGenerate.Remove(item2);
		}
		m_ltMonsterGenerateDestroy.Clear();
		foreach (int item3 in m_ltReadyToCreate)
		{
			MGStart(item3);
		}
		m_ltReadyToCreate.Clear();
		if (m_EventManager != null)
		{
			m_EventManager.Update(deltaTime);
		}
	}

	public CEventManager GetEventManager()
	{
		return m_EventManager;
	}

	public bool IsWaveProcess(int nWaveID)
	{
		foreach (CMonsterGenerate item in m_ltMonsterGenerate)
		{
			if (item.WaveID == nWaveID && (item.State == CMonsterGenerate.GenerateState.Process || item.State == CMonsterGenerate.GenerateState.Delay))
			{
				return true;
			}
		}
		return false;
	}

	public bool IsWaveCompleted()
	{
		return m_ltMonsterGenerate.Count == 0;
	}
}
