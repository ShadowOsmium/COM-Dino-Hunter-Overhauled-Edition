using System.Collections.Generic;
using UnityEngine;

public class CLevelUpWeapon
{
	public class CLackofMaterial
	{
		public int m_nMaterialID;

		public int m_nMaterialCount;
	}

	protected iWeaponCenter m_WeaponCenter;

	protected iDataCenter m_DataCenter;

	public int m_nWeaponID;

	public int m_nWeaponLevel;

	public int m_nWeaponLevelNext;

	public float m_fDiscount;

	public bool m_bCostCrystal;

	public int m_nCost;

	public List<CLackofMaterial> m_ltLackofMaterial;

	public bool m_bLackCrystal;

	public int m_nLackCount;

	public CLevelUpWeapon()
	{
		iGameData gameData = iGameApp.GetInstance().m_GameData;
		if (gameData != null)
		{
			m_WeaponCenter = gameData.m_WeaponCenter;
			m_DataCenter = gameData.m_DataCenter;
		}
		m_ltLackofMaterial = new List<CLackofMaterial>();
	}

	public bool CheckCanUpgrade(int nWeaponID, float fDiscount = 1f)
	{
		m_bLackCrystal = false;
		m_nLackCount = 0;
		m_ltLackofMaterial.Clear();
		CWeaponInfo cWeaponInfo = m_WeaponCenter.Get(nWeaponID);
		if (cWeaponInfo == null)
		{
			return false;
		}
		m_nWeaponID = nWeaponID;
		m_nWeaponLevel = -1;
		m_nWeaponLevelNext = -1;
		m_fDiscount = fDiscount;
		if (!m_DataCenter.GetWeaponLevel(m_nWeaponID, ref m_nWeaponLevel) || m_nWeaponLevel == -1)
		{
			m_nWeaponLevelNext = 1;
		}
		else
		{
			m_nWeaponLevelNext = m_nWeaponLevel + 1;
		}
		CWeaponInfoLevel cWeaponInfoLevel = cWeaponInfo.Get(m_nWeaponLevelNext);
		if (cWeaponInfoLevel == null)
		{
			return false;
		}
		for (int i = 0; i < cWeaponInfoLevel.ltMaterials.Count && i < cWeaponInfoLevel.ltMaterialsCount.Count; i++)
		{
			int num = m_DataCenter.GetMaterialNum(cWeaponInfoLevel.ltMaterials[i]);
			if (num < 0)
			{
				num = 0;
			}
			if (num < cWeaponInfoLevel.ltMaterialsCount[i])
			{
				CLackofMaterial cLackofMaterial = new CLackofMaterial();
				cLackofMaterial.m_nMaterialID = cWeaponInfoLevel.ltMaterials[i];
				cLackofMaterial.m_nMaterialCount = cWeaponInfoLevel.ltMaterialsCount[i] - num;
				m_ltLackofMaterial.Add(cLackofMaterial);
			}
		}
		int num2 = Mathf.CeilToInt((float)cWeaponInfoLevel.nPurchasePrice * m_fDiscount);
		if (cWeaponInfoLevel.isCrystalPurchase)
		{
			if (m_DataCenter.Crystal < num2)
			{
				m_bLackCrystal = true;
				m_nLackCount = num2 - m_DataCenter.Crystal;
			}
		}
		else if (m_DataCenter.Gold < num2)
		{
			m_bLackCrystal = false;
			m_nLackCount = num2 - m_DataCenter.Gold;
		}
		if (m_nLackCount > 0 || m_ltLackofMaterial.Count > 0)
		{
			return false;
		}
		return true;
	}

	public void Levelup()
	{
		m_bCostCrystal = false;
		m_nCost = -1;
		CWeaponInfoLevel cWeaponInfoLevel = m_WeaponCenter.Get(m_nWeaponID, m_nWeaponLevelNext);
		if (cWeaponInfoLevel == null)
		{
			return;
		}
		for (int i = 0; i < cWeaponInfoLevel.ltMaterials.Count && i < cWeaponInfoLevel.ltMaterialsCount.Count; i++)
		{
			int num = m_DataCenter.GetMaterialNum(cWeaponInfoLevel.ltMaterials[i]) - cWeaponInfoLevel.ltMaterialsCount[i];
			if (num < 0)
			{
				num = 0;
			}
			m_DataCenter.SetMaterialNum(cWeaponInfoLevel.ltMaterials[i], num);
		}
		m_bCostCrystal = cWeaponInfoLevel.isCrystalPurchase;
		m_nCost = Mathf.CeilToInt((float)cWeaponInfoLevel.nPurchasePrice * m_fDiscount);
		if (cWeaponInfoLevel.isCrystalPurchase)
		{
			m_DataCenter.AddCrystal(-Mathf.Abs(m_nCost));
		}
		else
		{
			m_DataCenter.AddGold(-Mathf.Abs(m_nCost));
		}
		m_DataCenter.SetWeaponLevel(m_nWeaponID, m_nWeaponLevelNext);
	}
}
