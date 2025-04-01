public class CTaskInfo
{
	public int nID;

	public int nType;

	public int[] arrFail;

	public int[] arrFailValue;

	public string sName = string.Empty;

	public string sDesc = string.Empty;

	public CTaskInfo()
	{
		arrFail = new int[3];
		arrFailValue = new int[3];
	}

	public bool GetFailValue(int nIndex, ref float fValue)
	{
		if (nIndex < 0 || nIndex >= arrFailValue.Length)
		{
			return false;
		}
		fValue = arrFailValue[nIndex];
		return true;
	}
}
