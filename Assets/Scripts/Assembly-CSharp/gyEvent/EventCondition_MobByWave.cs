namespace gyEvent
{
	public class EventCondition_MobByWave : EventCondition
	{
		protected int m_nWaveID;

		protected int m_nSequence;

		protected int m_nBehavior;

		protected int m_nAIState;

		public EventCondition_MobByWave(int waveid, int sequence, int behavior, int aistate = 0)
		{
			m_nWaveID = waveid;
			m_nSequence = sequence;
			m_nBehavior = behavior;
			m_nAIState = aistate;
		}

		public override bool IsMatch(EventCondition param)
		{
			EventCondition_MobByWave eventCondition_MobByWave = param as EventCondition_MobByWave;
			if (eventCondition_MobByWave == null)
			{
				return false;
			}
			if (eventCondition_MobByWave.m_nWaveID != m_nWaveID)
			{
				return false;
			}
			if (eventCondition_MobByWave.m_nSequence != m_nSequence)
			{
				return false;
			}
			if (eventCondition_MobByWave.m_nBehavior != m_nBehavior)
			{
				return false;
			}
			if (m_nBehavior == 2 && eventCondition_MobByWave.m_nAIState != m_nAIState)
			{
				return false;
			}
			return true;
		}
	}
}
