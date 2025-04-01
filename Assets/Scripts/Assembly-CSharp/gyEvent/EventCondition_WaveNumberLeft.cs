namespace gyEvent
{
	public class EventCondition_WaveNumberLeft : EventCondition
	{
		protected int m_nWaveID;

		protected int m_nWaveNumber;

		public EventCondition_WaveNumberLeft(int nWaveID, int nWaveNumber)
		{
			m_nWaveID = nWaveID;
			m_nWaveNumber = nWaveNumber;
		}

		public override bool IsMatch(EventCondition param)
		{
			EventCondition_WaveNumberLeft eventCondition_WaveNumberLeft = param as EventCondition_WaveNumberLeft;
			if (eventCondition_WaveNumberLeft == null)
			{
				return false;
			}
			if (eventCondition_WaveNumberLeft.m_nWaveID != m_nWaveID)
			{
				return false;
			}
			if (eventCondition_WaveNumberLeft.m_nWaveNumber >= m_nWaveNumber)
			{
				return false;
			}
			return true;
		}
	}
}
