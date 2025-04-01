namespace gyDataCenter
{
	public class CDataBaseArrBool : CDataBase
	{
		public bool[] array;

		public CDataBaseArrBool(int num)
		{
			base.Type = kDataType.ARR_BOOL;
			array = new bool[num];
			base.Count = num;
		}

		public void SetValue(int nIndex, bool value)
		{
			if (nIndex >= 0 && nIndex < array.Length)
			{
				array[nIndex] = value;
			}
		}

		public bool GetValue(int nIndex)
		{
			if (nIndex < 0 || nIndex >= array.Length)
			{
				return false;
			}
			return array[nIndex];
		}
	}
}
