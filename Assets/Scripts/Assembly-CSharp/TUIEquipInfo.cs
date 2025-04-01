using System.Collections.Generic;

public class TUIEquipInfo
{
	public TUIPopupInfo skill;

	public TUIPopupInfo role;

	public List<TUIPopupInfo> roles_list;

	public TUIPopupInfo[] arrSkill;

	public List<TUIPopupInfo> skill_list;

	public TUIPopupInfo[] arrWeapon;

	public TUIPopupInfo[] arrArmor;

	public TUIPopupInfo[] arrAccessory;

	public List<TUIPopupInfo> ltWeaponMelee;

	public List<TUIPopupInfo> ltWeaponRange;

	public List<TUIPopupInfo> ltArmorHead;

	public List<TUIPopupInfo> ltArmorUpper;

	public List<TUIPopupInfo> ltArmorBracelet;

	public List<TUIPopupInfo> ltArmorLower;

	public List<TUIPopupInfo> ltAccessoryHeadup;

	public List<TUIPopupInfo> ltAccessoryNeck;

	public List<TUIPopupInfo> ltAccessoryStone;

	public List<TUIPopupInfo> ltAccessoryBadge;

	public PopupType m_GotoEquip_PopupType;

	public TUIEquipInfo()
	{
		arrSkill = new TUIPopupInfo[3];
		arrWeapon = new TUIPopupInfo[3];
		arrArmor = new TUIPopupInfo[4];
		arrAccessory = new TUIPopupInfo[4];
		ltWeaponMelee = new List<TUIPopupInfo>();
		ltWeaponRange = new List<TUIPopupInfo>();
		ltArmorHead = new List<TUIPopupInfo>();
		ltArmorUpper = new List<TUIPopupInfo>();
		ltArmorBracelet = new List<TUIPopupInfo>();
		ltArmorLower = new List<TUIPopupInfo>();
		ltAccessoryHeadup = new List<TUIPopupInfo>();
		ltAccessoryNeck = new List<TUIPopupInfo>();
		ltAccessoryStone = new List<TUIPopupInfo>();
		ltAccessoryBadge = new List<TUIPopupInfo>();
	}

	public void SetSkill(int nIndex, TUIPopupInfo info)
	{
		if (nIndex >= 0 && nIndex < arrWeapon.Length)
		{
			arrSkill[nIndex] = info;
		}
	}

	public void SetWeapon(int nIndex, TUIPopupInfo info)
	{
		if (nIndex >= 0 && nIndex < arrWeapon.Length)
		{
			arrWeapon[nIndex] = info;
		}
	}

	public void SetArmor(int nIndex, TUIPopupInfo info)
	{
		if (nIndex >= 0 && nIndex < arrArmor.Length)
		{
			arrArmor[nIndex] = info;
		}
	}

	public void SetAccessory(int nIndex, TUIPopupInfo info)
	{
		if (nIndex >= 0 && nIndex < arrAccessory.Length)
		{
			arrAccessory[nIndex] = info;
		}
	}
}
