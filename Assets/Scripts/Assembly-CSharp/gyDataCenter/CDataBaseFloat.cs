namespace gyDataCenter
{
	public class CDataBaseFloat : CDataBase
	{
		public float value;

		public CDataBaseFloat(float value)
		{
			base.Type = kDataType.FLOAT;
			this.value = value;
		}
	}
}
