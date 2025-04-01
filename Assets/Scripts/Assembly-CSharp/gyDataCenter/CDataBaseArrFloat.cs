namespace gyDataCenter
{
	public class CDataBaseArrFloat : CDataBase
	{
		public float[] array;

		public CDataBaseArrFloat(int num)
		{
			base.Type = kDataType.ARR_FLOAT;
			array = new float[num];
			base.Count = num;
		}

		public void SetValue(int nIndex, float value)
		{
			if (nIndex >= 0 && nIndex < array.Length)
			{
				array[nIndex] = value;
			}
		}

		public float GetValue(int nIndex)
		{
			if (nIndex < 0 || nIndex >= array.Length)
			{
				return -1f;
			}
			return array[nIndex];
		}
	}
}
