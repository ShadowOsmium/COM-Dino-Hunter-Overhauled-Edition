namespace gyEvent
{
	public class EventCondition_MobByID : EventCondition
	{
		protected int m_nMobID;

		protected int m_nBehavior;

		protected int m_nAIState;

		public EventCondition_MobByID(int mobid, int behavior, int aistate = 0)
		{
			m_nMobID = mobid;
			m_nBehavior = behavior;
			m_nAIState = aistate;
		}

		public override bool IsMatch(EventCondition param)
		{
			EventCondition_MobByID eventCondition_MobByID = param as EventCondition_MobByID;
			if (eventCondition_MobByID == null)
			{
				return false;
			}
			if (eventCondition_MobByID.m_nMobID != m_nMobID)
			{
				return false;
			}
			if (eventCondition_MobByID.m_nBehavior != m_nBehavior)
			{
				return false;
			}
			if (m_nBehavior == 2 && eventCondition_MobByID.m_nAIState != m_nAIState)
			{
				return false;
			}
			return true;
		}
	}
}
