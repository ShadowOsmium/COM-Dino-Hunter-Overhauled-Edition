using System.Collections.Generic;

namespace gyEvent
{
	public class EventBase
	{
		public delegate bool TriggerFunc(List<object> ltParam);

		public int nID;

		public TriggerFunc m_TriggerFunc;

		public List<object> m_ltParam;

		public EventCondition m_Compare;

		public bool IsMatch(EventCondition condition)
		{
			if (m_Compare == null)
			{
				return false;
			}
			return m_Compare.IsMatch(condition);
		}

		public bool Trigger()
		{
			if (m_TriggerFunc == null)
			{
				return false;
			}
			return m_TriggerFunc(m_ltParam);
		}
	}
}
