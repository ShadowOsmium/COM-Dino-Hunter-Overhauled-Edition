namespace gyEvent
{
	public class EventCondition_Time : EventCondition
	{
		protected float m_fTime;

		protected float m_fTimeCount;

		public EventCondition_Time(float fTime)
		{
			m_fTime = fTime;
			m_fTimeCount = 0f;
		}

		public override bool Update(float deltaTime)
		{
			m_fTimeCount += deltaTime;
			if (m_fTimeCount < m_fTime)
			{
				return false;
			}
			m_fTimeCount = 0f;
			return true;
		}
	}
}
