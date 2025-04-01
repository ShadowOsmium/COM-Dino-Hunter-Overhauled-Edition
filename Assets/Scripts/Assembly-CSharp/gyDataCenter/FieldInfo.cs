namespace gyDataCenter
{
	public class FieldInfo
	{
		public string sFieldName;

		public kDataType type;

		public bool isKey;

		public FieldInfo(string sFieldName, kDataType type, bool isKey = false)
		{
			this.sFieldName = sFieldName;
			this.type = type;
			this.isKey = isKey;
		}
	}
}
