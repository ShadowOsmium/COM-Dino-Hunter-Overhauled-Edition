namespace gyTaskSystem
{
	public class CTaskBase
	{
		protected enum State
		{
			None,
			Proccessing,
			Completed,
			Failed
		}

		protected State m_State;

		protected CTaskInfo m_curTaskInfo;

		protected bool m_bUpdateData;

		protected float m_fTaskTime;

		public bool isCompleted
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

		public float TaskTime
		{
			get
			{
				return m_fTaskTime;
			}
		}

		public bool isUpdateData
		{
			get
			{
				return m_bUpdateData;
			}
			set
			{
				m_bUpdateData = value;
			}
		}

		public virtual void Initialize(CTaskInfo taskinfo)
		{
			m_State = State.None;
			m_curTaskInfo = taskinfo;
			Reset();
		}

		public virtual void Destroy()
		{
		}

		public virtual void Reset()
		{
			for (int i = 0; i < m_curTaskInfo.arrFail.Length; i++)
			{
				if (m_curTaskInfo.arrFail[i] == 1)
				{
					m_curTaskInfo.GetFailValue(i, ref m_fTaskTime);
					break;
				}
			}
			isUpdateData = true;
			m_State = State.Proccessing;
		}

		public virtual void ResetState()
		{
			m_State = State.Proccessing;
		}

		public virtual void Update(float deltaTime)
		{
			if (!isCompleted && !isFailed && m_fTaskTime > 0f)
			{
				m_fTaskTime -= deltaTime;
				if (m_fTaskTime <= 0f)
				{
					m_fTaskTime = 0f;
					OnTaskLimitTimeOver();
				}
			}
		}

		public void TaskCompleted()
		{
			m_State = State.Completed;
			OnTaskCompleted();
		}

		public void TaskFailed()
		{
			m_State = State.Failed;
			OnTaskFailed();
		}

		public virtual void OnTaskCompleted()
		{
		}

		public virtual void OnTaskFailed()
		{
		}

		public virtual void OnTaskLimitTimeOver()
		{
			TaskFailed();
		}

		public virtual void OnKillMonster(int nMobID, int nCount = 1)
		{
		}

		public virtual void OnGetItem(int nItemID, int nCount = 1)
		{
		}

		public virtual void OnMonsterEnter(int nMobID)
		{
		}

		public virtual void OnKillAllMonsters()
		{
		}

		public virtual void OnWaveBegin()
		{
		}

		public virtual void OnPlayerDead()
		{
			TaskFailed();
		}

		public CTaskInfo GetTaskInfo()
		{
			return m_curTaskInfo;
		}
	}
}
