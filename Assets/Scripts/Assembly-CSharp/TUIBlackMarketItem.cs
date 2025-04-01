public class TUIBlackMarketItem
{
	public int m_nBlackMarketID;

	public int m_nItemID;

	public WeaponType m_WeaponType;

	public string m_sName = string.Empty;

	public string m_sIcon = string.Empty;

	public string m_sDesc = string.Empty;

	public TUIPriceInfo m_Price;

	public float m_fLeftTime;

	public bool m_bAlreadyGain;

	public int m_nDefence;

	public int m_nDefenceMax;

	public float m_fDamage;

	public float m_fDamageMax;

	public float m_fShootSpeed;

	public int m_nBlastRadius;

	public int m_nKnockBack;

	public int m_nCapcity;

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
}
