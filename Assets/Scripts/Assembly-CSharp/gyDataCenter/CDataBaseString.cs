namespace gyDataCenter
{
	public class CDataBaseString : CDataBase
	{
		public string value;

		public CDataBaseString(string value)
		{
			base.Type = kDataType.STRING;
			this.value = value;
		}
	}
}
