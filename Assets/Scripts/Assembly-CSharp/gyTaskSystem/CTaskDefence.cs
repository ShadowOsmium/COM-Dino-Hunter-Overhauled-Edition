namespace gyTaskSystem
{
	public class CTaskDefence : CTaskBase
	{
		public int m_nCurWave;

		public int m_nMaxWave;

		public float m_fCurLife;

		public float m_fMaxLife;

		public bool isLifeChange;

		public override void Initialize(CTaskInfo taskinfo)
		{
			base.Initialize(taskinfo);
			CTaskInfoDefence cTaskInfoDefence = m_curTaskInfo as CTaskInfoDefence;
			if (cTaskInfoDefence != null)
			{
				m_nMaxWave = cTaskInfoDefence.nWaveCount;
				m_nCurWave = 0;
				m_fMaxLife = cTaskInfoDefence.fLife;
				m_fCurLife = m_fMaxLife;
				isLifeChange = false;
			}
		}

		public override void Reset()
		{
			base.Reset();
			m_nCurWave = 0;
			m_fCurLife = m_fMaxLife;
			isLifeChange = true;
		}

		public void AddDamage(float fDmg)
		{
			m_fCurLife += fDmg;
			if (m_fCurLife <= 0f)
			{
				m_fCurLife = 0f;
				TaskFailed();
			}
			isLifeChange = true;
		}

		public override void OnMonsterEnter(int nMobID)
		{
			TaskFailed();
		}

		public override void OnWaveBegin()
		{
			m_nCurWave++;
			if (m_nCurWave > m_nMaxWave)
			{
				m_nCurWave = m_nMaxWave;
			}
			base.isUpdateData = true;
		}

		public override void OnKillAllMonsters()
		{
			if (m_nCurWave == m_nMaxWave)
			{
				TaskCompleted();
			}
		}

		public override void OnTaskLimitTimeOver()
		{
			TaskCompleted();
		}
	}
}
