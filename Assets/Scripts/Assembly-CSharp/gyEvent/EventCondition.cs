namespace gyEvent
{
	public class EventCondition
	{
		public virtual bool Update(float deltaTime)
		{
			return false;
		}

		public virtual bool IsMatch(EventCondition param)
		{
			return false;
		}
	}
}
