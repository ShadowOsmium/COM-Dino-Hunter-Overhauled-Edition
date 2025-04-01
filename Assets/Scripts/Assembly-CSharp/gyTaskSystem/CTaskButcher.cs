namespace gyTaskSystem
{
	public class CTaskButcher : CTaskBase
	{
		public override void OnKillAllMonsters()
		{
			TaskCompleted();
		}
	}
}
