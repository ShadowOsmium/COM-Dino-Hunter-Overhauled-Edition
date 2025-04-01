public class TUIPopupInfo
{
	public int texture_id;

	public int level;

	public string name = string.Empty;

	public string introduce = string.Empty;

	public int value;

	public TUIWeaponAttribute weapon_attribute;

	public TUIArmorAttribute armor_accessory_attribute;

	public bool duoren_skill;

	public float discount = 1f;

	public string m_sIcon = string.Empty;

	public PopupType m_PopupType;

	public TUICharacterAttribute m_CharacterAttribute;

	public NewMarkType m_MarkType;

	public TUIPopupInfo()
	{
		m_PopupType = PopupType.None;
		m_CharacterAttribute = null;
		m_MarkType = NewMarkType.None;
	}

	public TUIPopupInfo(int m_id, string m_name, string m_introduce, int m_value = 0, int m_level = 0)
	{
		texture_id = m_id;
		name = m_name;
		introduce = m_introduce;
		value = m_value;
		level = m_level;
	}

	public TUIPopupInfo(int m_id, string m_name, string m_introduce, bool m_duoren_skill)
	{
		texture_id = m_id;
		name = m_name;
		introduce = m_introduce;
		duoren_skill = m_duoren_skill;
	}

	public TUIPopupInfo(int m_id, string m_name, string m_introduce, TUIWeaponAttribute m_weapon_attribute, int m_level = 0)
	{
		texture_id = m_id;
		name = m_name;
		introduce = m_introduce;
		weapon_attribute = m_weapon_attribute;
		level = m_level;
	}

	public TUIPopupInfo(int m_id, string m_name, string m_introduce, TUIArmorAttribute m_stone_skin_attribute, int m_level = 0)
	{
		texture_id = m_id;
		name = m_name;
		introduce = m_introduce;
		armor_accessory_attribute = m_stone_skin_attribute;
		level = m_level;
	}

	public bool IsProps()
	{
		return m_PopupType == PopupType.Props;
	}

	public bool IsRole()
	{
		return m_PopupType == PopupType.Roles;
	}

	public bool IsSkill()
	{
		return m_PopupType == PopupType.Skills || m_PopupType == PopupType.Skills01;
	}

	public bool IsWeapon()
	{
		return m_PopupType == PopupType.Weapons01 || m_PopupType == PopupType.Weapons02;
	}

	public bool IsArmor()
	{
		return m_PopupType == PopupType.Armor_Body || m_PopupType == PopupType.Armor_Bracelet || m_PopupType == PopupType.Armor_Head || m_PopupType == PopupType.Armor_Leg;
	}

	public bool IsAccessory()
	{
		return m_PopupType == PopupType.Accessory_Badge || m_PopupType == PopupType.Accessory_Halo || m_PopupType == PopupType.Accessory_Necklace || m_PopupType == PopupType.Accessory_Stoneskin;
	}
}
