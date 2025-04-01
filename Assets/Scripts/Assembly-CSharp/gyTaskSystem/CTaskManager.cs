using System.Collections.Generic;
using UnityEngine;

namespace gyTaskSystem
{
	public class CTaskManager
	{
		protected enum State
		{
			None,
			Proccessing,
			Completed,
			Failed
		}

		protected State m_State;

		protected int m_nTaskNum;

		protected int m_nTaskNumCompleted;

		protected int m_nTaskNumFailed;

		protected iTaskCenter m_TaskCenter;

		protected List<CTaskBase> m_ltTask;

		protected List<CTaskBase> m_ltTaskDestroy;

		public bool isAllCompleted
		{
			get
			{
				return m_State == State.Completed;
			}
		}

		public bool isFailed
		{
			get
			{
				return m_State == State.Failed;
			}
		}

		public CTaskManager()
		{
			m_ltTask = new List<CTaskBase>();
			iGameData gameData = iGameApp.GetInstance().m_GameData;
			if (gameData != null)
			{
				m_TaskCenter = gameData.GetTaskCenter();
			}
		}

		public void Initialize()
		{
		}

		public void Reset()
		{
			m_State = State.Proccessing;
			if (m_ltTask == null)
			{
				return;
			}
			foreach (CTaskBase item in m_ltTask)
			{
				item.Reset();
			}
		}

		public void ResetState()
		{
			m_State = State.Proccessing;
			if (m_ltTask == null)
			{
				return;
			}
			foreach (CTaskBase item in m_ltTask)
			{
				item.ResetState();
			}
		}

		public void Destroy()
		{
		}

		public void Update(float deltaTime)
		{
			State state = m_State;
			if (state != State.Proccessing)
			{
				return;
			}
			m_nTaskNumCompleted = 0;
			m_nTaskNumFailed = 0;
			foreach (CTaskBase item in m_ltTask)
			{
				item.Update(deltaTime);
				if (item.isCompleted)
				{
					Debug.Log("task completed " + item.GetTaskInfo().nID);
					m_nTaskNumCompleted++;
				}
				else if (item.isFailed)
				{
					Debug.Log("task failed " + item.GetTaskInfo().nID);
					m_nTaskNumFailed++;
				}
			}
			if (m_nTaskNumCompleted >= m_nTaskNum)
			{
				m_State = State.Completed;
			}
			else if (m_nTaskNumFailed > 0)
			{
				m_State = State.Failed;
			}
		}

		public void Start()
		{
			m_State = State.Proccessing;
			m_nTaskNum = m_ltTask.Count;
			m_nTaskNumCompleted = 0;
			m_nTaskNumFailed = 0;
		}

		public void AddTask(int nTaskID)
		{
			CTaskInfo cTaskInfo = m_TaskCenter.Get(nTaskID);
			if (cTaskInfo != null)
			{
				CTaskBase cTaskBase = null;
				switch (cTaskInfo.nType)
				{
				case 2:
					cTaskBase = new CTaskHunter();
					break;
				case 1:
					cTaskBase = new CTaskCollection();
					break;
				case 3:
					cTaskBase = new CTaskDefence();
					break;
				case 4:
					cTaskBase = new CTaskSurvival();
					break;
				case 5:
					cTaskBase = new CTaskButcher();
					break;
				case 6:
					cTaskBase = new CTaskInfinite();
					break;
				}
				if (cTaskBase != null)
				{
					cTaskBase.Initialize(cTaskInfo);
					m_ltTask.Add(cTaskBase);
				}
			}
		}

		public void DelTask(int nTaskID)
		{
			foreach (CTaskBase item in m_ltTask)
			{
				CTaskInfo taskInfo = item.GetTaskInfo();
				if (taskInfo.nID == nTaskID)
				{
					m_ltTaskDestroy.Add(item);
				}
			}
			foreach (CTaskBase item2 in m_ltTaskDestroy)
			{
				m_ltTask.Remove(item2);
			}
			m_ltTaskDestroy.Clear();
		}

		public List<CTaskBase> GetTaskList()
		{
			return m_ltTask;
		}

		public CTaskBase GetTask()
		{
			if (m_ltTask == null || m_ltTask.Count < 1)
			{
				return null;
			}
			return m_ltTask[0];
		}

		public void OnAllTaskCompelete(bool isSuccess)
		{
			foreach (CTaskBase item in m_ltTask)
			{
				if (isSuccess)
				{
					item.TaskCompleted();
				}
				else
				{
					item.TaskFailed();
				}
			}
		}

		public void OnKillMonster(int nMobID, int nCount = 1)
		{
			foreach (CTaskBase item in m_ltTask)
			{
				if (!item.isCompleted && !item.isFailed)
				{
					item.OnKillMonster(nMobID, nCount);
				}
			}
		}

		public void OnGetItem(int nItemID, int nCount = 1)
		{
			foreach (CTaskBase item in m_ltTask)
			{
				if (!item.isCompleted && !item.isFailed)
				{
					item.OnGetItem(nItemID, nCount);
				}
			}
		}

		public void OnMonsterEnter(int nMobID)
		{
			foreach (CTaskBase item in m_ltTask)
			{
				if (!item.isCompleted && !item.isFailed)
				{
					item.OnMonsterEnter(nMobID);
				}
			}
		}

		public void OnKillAllMonsters()
		{
			foreach (CTaskBase item in m_ltTask)
			{
				if (!item.isCompleted && !item.isFailed)
				{
					item.OnKillAllMonsters();
				}
			}
		}

		public void OnWaveBegin()
		{
			foreach (CTaskBase item in m_ltTask)
			{
				if (!item.isCompleted && !item.isFailed)
				{
					item.OnWaveBegin();
				}
			}
		}

		public void OnPlayerDead()
		{
			foreach (CTaskBase item in m_ltTask)
			{
				if (!item.isCompleted && !item.isFailed)
				{
					item.OnPlayerDead();
				}
			}
		}
	}
}
