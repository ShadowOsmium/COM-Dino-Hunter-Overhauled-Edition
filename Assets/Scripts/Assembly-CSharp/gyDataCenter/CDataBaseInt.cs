namespace gyDataCenter
{
	public class CDataBaseInt : CDataBase
	{
		public int value;

		public CDataBaseInt(int value)
		{
			base.Type = kDataType.INT;
			this.value = value;
		}
	}
}
