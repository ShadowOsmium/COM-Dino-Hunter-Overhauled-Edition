namespace gyDataCenter
{
	public class CDataBaseArrInt : CDataBase
	{
		public int[] array;

		public CDataBaseArrInt(int num)
		{
			base.Type = kDataType.ARR_INT;
			array = new int[num];
			base.Count = num;
		}

		public void SetValue(int nIndex, int value)
		{
			if (nIndex >= 0 && nIndex < array.Length)
			{
				array[nIndex] = value;
			}
		}

		public int GetValue(int nIndex)
		{
			if (nIndex < 0 || nIndex >= array.Length)
			{
				return -1;
			}
			return array[nIndex];
		}
	}
}
