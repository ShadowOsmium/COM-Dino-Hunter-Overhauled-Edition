using UnityEngine;

public class PopupEquipFrame : MonoBehaviour
{
	public enum PopupEquipItemsType
	{
		None,
		Weapon,
		Armor,
		Accessory
	}

	public PopupEquipItems items_weapon;

	public PopupEquipItems items_armor;

	public PopupEquipItems items_accessory;

	public TUIButtonSelect btn_select_weapon;

	public TUIButtonSelect btn_select_armor;

	public TUIButtonSelect btn_select_accessory;

	private PopupEquipItems items_now;

	private void Start()
	{
		if (items_weapon != null)
		{
			items_weapon.transform.localPosition = new Vector3(-1000f, 0f, items_weapon.transform.localPosition.z);
		}
		if (items_armor != null)
		{
			items_armor.transform.localPosition = new Vector3(-1000f, 0f, items_armor.transform.localPosition.z);
		}
		if (items_accessory != null)
		{
			items_accessory.transform.localPosition = new Vector3(-1000f, 0f, items_accessory.transform.localPosition.z);
		}
	}

	private void Update()
	{
	}

	public void SetInfo(TUIEquipInfo m_equip_info)
	{
		if (m_equip_info == null)
		{
			Debug.Log("error!");
			return;
		}
		if (items_weapon != null)
		{
			for (int i = 0; i < m_equip_info.arrWeapon.Length; i++)
			{
				items_weapon.SetInfo(i, m_equip_info.arrWeapon[i], false, true);
			}
		}
		if (items_armor != null)
		{
			for (int j = 0; j < m_equip_info.arrArmor.Length; j++)
			{
				items_armor.SetInfo(j, m_equip_info.arrArmor[j], false, true);
			}
		}
		if (items_accessory != null)
		{
			for (int k = 0; k < m_equip_info.arrAccessory.Length; k++)
			{
				items_accessory.SetInfo(k, m_equip_info.arrAccessory[k], false, true);
			}
		}
	}

	public void SetItemsChoose(int m_index)
	{
		if (items_now != null)
		{
			items_now.transform.localPosition = new Vector3(-1000f, 0f, items_now.transform.localPosition.z);
		}
		switch (m_index)
		{
		case 1:
			items_now = items_weapon;
			break;
		case 2:
			items_now = items_armor;
			break;
		case 3:
			items_now = items_accessory;
			break;
		}
		if (m_index >= 1 && m_index <= 3 && items_now != null)
		{
			items_now.transform.localPosition = new Vector3(0f, 0f, items_now.transform.localPosition.z);
		}
	}

	public void SetBtnChoose(int m_index)
	{
		switch (m_index)
		{
		case 1:
			if (btn_select_weapon != null)
			{
				btn_select_weapon.Event_CommandSelect();
			}
			break;
		case 2:
			if (btn_select_armor != null)
			{
				btn_select_armor.Event_CommandSelect();
			}
			break;
		case 3:
			if (btn_select_accessory != null)
			{
				btn_select_accessory.Event_CommandSelect();
			}
			break;
		}
	}
}
