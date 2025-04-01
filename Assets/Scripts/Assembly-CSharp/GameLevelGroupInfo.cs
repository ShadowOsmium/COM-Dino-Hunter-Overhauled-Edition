using System.Collections.Generic;

public class GameLevelGroupInfo
{
	public int nID;

	public string sName;

	public int nIcon;

	public List<int> ltLevelList;

	public GameLevelGroupInfo()
	{
		sName = string.Empty;
		ltLevelList = new List<int>();
	}

	public int GetLevelCount()
	{
		if (ltLevelList == null)
		{
			return 0;
		}
		return ltLevelList.Count;
	}

	public int GetLevelIndex(int nLevelID)
	{
		if (ltLevelList == null)
		{
			return -1;
		}
		for (int i = 0; i < ltLevelList.Count; i++)
		{
			if (ltLevelList[i] == nLevelID)
			{
				return i;
			}
		}
		return -1;
	}
}
