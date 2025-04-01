using System.Collections.Generic;
using UnityEngine;

public class CLevelUpAvatar
{
	public class CLackofMaterial
	{
		public int m_nMaterialID;

		public int m_nMaterialCount;
	}

	protected iAvatarCenter m_AvatarCenter;

	protected iDataCenter m_DataCenter;

	public int m_nAvatarID;

	public int m_nAvatarLevel;

	public int m_nAvatarLevelNext;

	public float m_fDiscount;

	public bool m_bCostCrystal;

	public int m_nCost;

	public List<CLackofMaterial> m_ltLackofMaterial;

	public bool m_bLackCrystal;

	public int m_nLackCount;

	public CLevelUpAvatar()
	{
		iGameData gameData = iGameApp.GetInstance().m_GameData;
		if (gameData != null)
		{
			m_AvatarCenter = gameData.m_AvatarCenter;
			m_DataCenter = gameData.m_DataCenter;
		}
		m_ltLackofMaterial = new List<CLackofMaterial>();
	}

	public bool CheckCanUpgrade(int nAvatarID, float fDiscount = 1f)
	{
		m_bLackCrystal = false;
		m_nLackCount = 0;
		m_ltLackofMaterial.Clear();
		CAvatarInfo cAvatarInfo = m_AvatarCenter.Get(nAvatarID);
		if (cAvatarInfo == null)
		{
			return false;
		}
		m_nAvatarID = nAvatarID;
		m_nAvatarLevel = -1;
		m_nAvatarLevelNext = -1;
		m_fDiscount = fDiscount;
		if (!m_DataCenter.GetAvatar(m_nAvatarID, ref m_nAvatarLevel) || m_nAvatarLevel == -1)
		{
			m_nAvatarLevelNext = 1;
		}
		else
		{
			m_nAvatarLevelNext = m_nAvatarLevel + 1;
		}
		CAvatarInfoLevel cAvatarInfoLevel = cAvatarInfo.Get(m_nAvatarLevelNext);
		if (cAvatarInfoLevel == null)
		{
			return false;
		}
		for (int i = 0; i < cAvatarInfoLevel.ltMaterials.Count && i < cAvatarInfoLevel.ltMaterialsCount.Count; i++)
		{
			int num = m_DataCenter.GetMaterialNum(cAvatarInfoLevel.ltMaterials[i]);
			if (num < 0)
			{
				num = 0;
			}
			if (num < cAvatarInfoLevel.ltMaterialsCount[i])
			{
				CLackofMaterial cLackofMaterial = new CLackofMaterial();
				cLackofMaterial.m_nMaterialID = cAvatarInfoLevel.ltMaterials[i];
				cLackofMaterial.m_nMaterialCount = cAvatarInfoLevel.ltMaterialsCount[i] - num;
				m_ltLackofMaterial.Add(cLackofMaterial);
			}
		}
		int num2 = Mathf.CeilToInt((float)cAvatarInfoLevel.nPurchasePrice * m_fDiscount);
		if (cAvatarInfoLevel.isCrystalPurchase)
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
		CAvatarInfoLevel cAvatarInfoLevel = m_AvatarCenter.Get(m_nAvatarID, m_nAvatarLevelNext);
		if (cAvatarInfoLevel == null)
		{
			return;
		}
		for (int i = 0; i < cAvatarInfoLevel.ltMaterials.Count && i < cAvatarInfoLevel.ltMaterialsCount.Count; i++)
		{
			int num = m_DataCenter.GetMaterialNum(cAvatarInfoLevel.ltMaterials[i]) - cAvatarInfoLevel.ltMaterialsCount[i];
			if (num < 0)
			{
				num = 0;
			}
			m_DataCenter.SetMaterialNum(cAvatarInfoLevel.ltMaterials[i], num);
		}
		m_bCostCrystal = cAvatarInfoLevel.isCrystalPurchase;
		m_nCost = Mathf.CeilToInt((float)cAvatarInfoLevel.nPurchasePrice * m_fDiscount);
		if (cAvatarInfoLevel.isCrystalPurchase)
		{
			m_DataCenter.AddCrystal(-Mathf.Abs(m_nCost));
		}
		else
		{
			m_DataCenter.AddGold(-Mathf.Abs(m_nCost));
		}
		m_DataCenter.SetAvatar(m_nAvatarID, m_nAvatarLevelNext);
	}
}
