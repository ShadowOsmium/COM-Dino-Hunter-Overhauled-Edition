using System.Collections.Generic;

public class CWeaponInfoLevel
{
	public int nLevel;

	public int nType;

	public int nElementType;

	public int nActionType;

	public int nModel;

	public int nFire;

	public int nBullet;

	public int nHit;

	public string sAudioFire = string.Empty;

	public int nAttackMode;

	public List<float> ltAttackModeValue;

	public string sName = string.Empty;

	public string sDesc = string.Empty;

	public string sIcon = string.Empty;

	public float fShootSpeed;

	public float fMSDownRateShoot;

	public float fMSDownRateEquip;

	public float fPrecise = 1f;

	public int nCapacity;

	public int[] arrFunc;

	public int[] arrValueX;

	public int[] arrValueY;

	public float fDamage;

	public float fCritical;

	public float fCriticalDmg;

	public float fElementUp;

	public List<int> ltElementUpMonster;

	public float fElementDown;

	public List<int> ltElementDownMonster;

	public List<int> ltMaterials;

	public List<int> ltMaterialsCount;

	public bool isCrystalPurchase;

	public int nPurchasePrice;

	public string sLevelUpDesc = string.Empty;

	public CWeaponInfoLevel()
	{
		ltAttackModeValue = new List<float>();
		sAudioFire = string.Empty;
		nBullet = -1;
		nFire = -1;
		nHit = -1;
		arrFunc = new int[3];
		arrValueX = new int[3];
		arrValueY = new int[3];
		ltElementUpMonster = new List<int>();
		ltElementDownMonster = new List<int>();
		ltMaterials = new List<int>();
		ltMaterialsCount = new List<int>();
	}

	public bool GetAtkModeValue(int nIndex, ref float fValue)
	{
		if (nIndex < 0 || nIndex >= ltAttackModeValue.Count)
		{
			return false;
		}
		fValue = ltAttackModeValue[nIndex];
		return true;
	}

	public bool GetAtkModeValue(int nIndex, ref int nValue)
	{
		if (nIndex < 0 || nIndex >= ltAttackModeValue.Count)
		{
			return false;
		}
		nValue = (int)ltAttackModeValue[nIndex];
		return true;
	}

	public float GetElementValue(int nMobID)
	{
		if (ltElementUpMonster != null)
		{
			for (int i = 0; i < ltElementUpMonster.Count; i++)
			{
				if (nMobID == ltElementUpMonster[i])
				{
					return fElementUp;
				}
			}
		}
		if (ltElementDownMonster != null)
		{
			for (int j = 0; j < ltElementDownMonster.Count; j++)
			{
				if (nMobID == ltElementDownMonster[j])
				{
					return fElementDown;
				}
			}
		}
		return 0f;
	}

	public float CalcDPS()
	{
		float fValue = 0f;
		if (nAttackMode == 5)
		{
			GetAtkModeValue(2, ref fValue);
		}
		else
		{
			fValue = fShootSpeed;
		}
		return (fValue != 0f) ? (fDamage / fValue) : fDamage;
	}
}
