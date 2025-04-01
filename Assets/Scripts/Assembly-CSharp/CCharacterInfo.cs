using System.Collections.Generic;
using UnityEngine;

public class CCharacterInfo
{
	public int nID;

	public Dictionary<int, CCharacterInfoLevel> dictCharacterInfoLevel;

	public int nMaxLevel;

	public int nUnLockLevel;

	public bool isCrystalUnLock;

	public int nUnLockPrice;

	public bool isCrystalPurchase;

	public int nPurchasePrice;

	public List<int> ltCharacterPassiveSkill;

	public CCharacterInfo()
	{
		dictCharacterInfoLevel = new Dictionary<int, CCharacterInfoLevel>();
		ltCharacterPassiveSkill = new List<int>();
	}

	public void Add(int nLevel, CCharacterInfoLevel characterinfolevel)
	{
		if (!dictCharacterInfoLevel.ContainsKey(nLevel))
		{
			dictCharacterInfoLevel.Add(nLevel, characterinfolevel);
			if (nLevel > nMaxLevel)
			{
				nMaxLevel = nLevel;
			}
		}
	}

	public CCharacterInfoLevel Get(int nLevel)
	{
		if (!dictCharacterInfoLevel.ContainsKey(nLevel))
		{
			return null;
		}
		return dictCharacterInfoLevel[nLevel];
	}

	public bool IsMaxLevel(int nLevel)
	{
		return nLevel >= nMaxLevel;
	}

	public float GetExpRate(int nExp, int nLevel)
	{
		CCharacterInfoLevel cCharacterInfoLevel = Get(nLevel);
		if (cCharacterInfoLevel == null)
		{
			return 0f;
		}
		return Mathf.Clamp01((float)nExp / (float)cCharacterInfoLevel.nExp);
	}
}
