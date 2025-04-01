namespace gyDataCenter
{
	public class CDataBaseBool : CDataBase
	{
		public bool value;

		public CDataBaseBool(bool value)
		{
			base.Type = kDataType.BOOL;
			this.value = value;
		}
	}
}
