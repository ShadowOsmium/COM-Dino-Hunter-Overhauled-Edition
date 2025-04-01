using System.Collections.Generic;

public class TUIWeaponAttributeInfo
{
	public int m_nID;

	public int m_nLevel;

	public int m_nLevelMax;

	public WeaponType m_WeaponType;

	public string m_sName = string.Empty;

	public string m_sIcon = string.Empty;

	public bool m_bUnlock = true;

	public string m_sUnlockStr = string.Empty;

	public bool m_bCrystalWeapon;

	public float m_fDiscount = 1f;

	public bool m_bActive;

	public string m_sActiveStr = string.Empty;

	public bool m_bActiveCanGet;

	public NewMarkType m_Mark;

	public Dictionary<int, TUIWeaponLevelInfo> m_dictWeaponLevelInfo;

	public TUIWeaponAttributeInfo()
	{
		m_dictWeaponLevelInfo = new Dictionary<int, TUIWeaponLevelInfo>();
		m_Mark = NewMarkType.None;
	}

	public bool IsArmor()
	{
		return m_WeaponType == WeaponType.Armor_Body || m_WeaponType == WeaponType.Armor_Bracelet || m_WeaponType == WeaponType.Armor_Head || m_WeaponType == WeaponType.Armor_Leg;
	}

	public bool IsAccessory()
	{
		return m_WeaponType == WeaponType.Accessory_Badge || m_WeaponType == WeaponType.Accessory_Halo || m_WeaponType == WeaponType.Accessory_Necklace || m_WeaponType == WeaponType.Accessory_Stoneskin;
	}

	public bool IsWeapon()
	{
		return m_WeaponType == WeaponType.CloseWeapon || m_WeaponType == WeaponType.Crossbow || m_WeaponType == WeaponType.MachineGun || m_WeaponType == WeaponType.LiquidFireGun || m_WeaponType == WeaponType.ViolenceGun || m_WeaponType == WeaponType.RPG;
	}

	public string GetIconPath()
	{
		if (IsWeapon())
		{
			return TUIMappingInfo.Instance().m_sPathRootCustomTex + "/Weapon";
		}
		if (IsArmor())
		{
			return TUIMappingInfo.Instance().m_sPathRootCustomTex + "/Armor";
		}
		if (IsAccessory())
		{
			return TUIMappingInfo.Instance().m_sPathRootCustomTex + "/Accessory";
		}
		return string.Empty;
	}

	public TUIWeaponLevelInfo Get(int nLevel)
	{
		if (!m_dictWeaponLevelInfo.ContainsKey(nLevel))
		{
			return null;
		}
		return m_dictWeaponLevelInfo[nLevel];
	}

	public TUIWeaponLevelInfo Get()
	{
		return Get((m_nLevel < 1) ? 1 : m_nLevel);
	}

	public TUIWeaponLevelInfo GetNext()
	{
		return Get((m_nLevel < 1) ? 1 : (m_nLevel + 1));
	}

	public void AddWeaponInfo(int nLevel, TUIWeaponLevelInfo weaponlevelinfo)
	{
		if (!m_dictWeaponLevelInfo.ContainsKey(nLevel))
		{
			m_dictWeaponLevelInfo.Add(nLevel, weaponlevelinfo);
			if (m_nLevelMax < nLevel)
			{
				m_nLevelMax = nLevel;
			}
		}
	}

	public void Init(WeaponType nWeaponType, int nID, int nLevel, string sName, bool bCrystalWeapon, bool bUnlock, string sUnlockStr, float fDiscount = 1f)
	{
		m_WeaponType = nWeaponType;
		m_nID = nID;
		m_nLevel = nLevel;
		m_sName = sName;
		m_bCrystalWeapon = bCrystalWeapon;
		m_bUnlock = bUnlock;
		m_sUnlockStr = sUnlockStr;
		m_fDiscount = fDiscount;
	}

	public void InitActive(WeaponType nWeaponType, int nID, int nLevel, string sName, string sActiveStr, bool bActiveCanGet)
	{
		Init(nWeaponType, nID, nLevel, sName, false, false, string.Empty, 1f);
		m_bActive = true;
		m_sActiveStr = sActiveStr;
		m_bActiveCanGet = bActiveCanGet;
	}
}
