using System.Collections.Generic;

public class CAchievementInfo
{
	public int nID;

	public bool isDaily;

	public int nType;

	public List<int> ltParam;

	public string sKey;

	public string sName;

	public string sDesc;

	public List<CAchievementStep> ltStep;

	public CAchievementInfo()
	{
		isDaily = false;
		ltParam = new List<int>();
		ltStep = new List<CAchievementStep>();
	}

	public bool GetParam(int nIndex, ref int nValue)
	{
		if (nIndex < 0 || nIndex >= ltParam.Count)
		{
			return false;
		}
		nValue = ltParam[nIndex];
		return true;
	}

	public int GetStepCount()
	{
		return ltStep.Count;
	}

	public CAchievementStep GetStep(int nIndex)
	{
		if (nIndex < 0 || nIndex >= ltStep.Count)
		{
			return null;
		}
		return ltStep[nIndex];
	}

	public int GetMaxValue()
	{
		if (ltStep.Count < 1)
		{
			return 0;
		}
		return ltStep[ltStep.Count - 1].nStepPurpose;
	}
}
