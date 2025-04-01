using UnityEngine;

public class CRewardMaterial
{
	public int nID;

	public int nMinCount;

	public int nMaxCount;

	public CRewardMaterial()
	{
		nMinCount = -1;
		nMaxCount = 1;
	}

	public int GetDropCount()
	{
		return Random.Range(nMinCount, nMaxCount + 1);
	}
}
