namespace gyTaskSystem
{
	public class CTaskSurvival : CTaskBase
	{
		public override void OnTaskLimitTimeOver()
		{
			TaskCompleted();
		}
	}
}
