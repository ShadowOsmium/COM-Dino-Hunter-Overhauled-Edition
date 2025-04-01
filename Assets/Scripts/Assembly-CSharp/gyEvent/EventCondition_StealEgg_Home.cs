namespace gyEvent
{
	public class EventCondition_StealEgg_Home : EventCondition
	{
		protected int m_nStealItemID;

		protected int m_nStealItemNumber;

		public EventCondition_StealEgg_Home(int nID, int nCount)
		{
			m_nStealItemID = nID;
			m_nStealItemNumber = nCount;
		}

		public override bool IsMatch(EventCondition param)
		{
			EventCondition_StealEgg_Home eventCondition_StealEgg_Home = param as EventCondition_StealEgg_Home;
			if (eventCondition_StealEgg_Home == null)
			{
				return false;
			}
			if (eventCondition_StealEgg_Home.m_nStealItemID != m_nStealItemID || eventCondition_StealEgg_Home.m_nStealItemNumber != m_nStealItemNumber)
			{
				return false;
			}
			return true;
		}
	}
}
