namespace gyDataCenter
{
	public class CDataBaseArrString : CDataBase
	{
		public string[] array;

		public CDataBaseArrString(int num)
		{
			base.Type = kDataType.ARR_STRING;
			array = new string[num];
			base.Count = num;
		}

		public void SetValue(int nIndex, string value)
		{
			if (nIndex >= 0 && nIndex < array.Length)
			{
				array[nIndex] = value;
			}
		}

		public string GetValue(int nIndex)
		{
			if (nIndex < 0 || nIndex >= array.Length)
			{
				return string.Empty;
			}
			return array[nIndex];
		}
	}
}
