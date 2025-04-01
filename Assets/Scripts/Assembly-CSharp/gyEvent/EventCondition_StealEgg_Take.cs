namespace gyEvent
{
	public class EventCondition_StealEgg_Take : EventCondition
	{
		protected int m_nStealItemID;

		protected int m_nStealItemNumber;

		public EventCondition_StealEgg_Take(int nID, int nCount)
		{
			m_nStealItemID = nID;
			m_nStealItemNumber = nCount;
		}

		public override bool IsMatch(EventCondition param)
		{
			EventCondition_StealEgg_Take eventCondition_StealEgg_Take = param as EventCondition_StealEgg_Take;
			if (eventCondition_StealEgg_Take == null)
			{
				return false;
			}
			if (eventCondition_StealEgg_Take.m_nStealItemID != m_nStealItemID || eventCondition_StealEgg_Take.m_nStealItemNumber != m_nStealItemNumber)
			{
				return false;
			}
			return true;
		}
	}
}
