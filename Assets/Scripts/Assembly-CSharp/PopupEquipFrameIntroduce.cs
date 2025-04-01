using UnityEngine;

public class PopupEquipFrameIntroduce : MonoBehaviour
{
	public TUILabel label_introduce;

	public TUILabel label_damage_value;

	public TUILabel label_fire_rate_value;

	public TUILabel label_blast_radius_value;

	public TUILabel label_knockback_value;

	public TUILabel label_ammo_value;

	public TUILabel label_def_value;

	public TUILabel label_damage;

	public TUILabel label_fire_rate;

	public TUILabel label_blast_radius;

	public TUILabel label_knockback;

	public TUILabel label_ammo;

	public TUILabel label_def;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetWeaponInfo(TUIPopupInfo m_popup_info)
	{
		HideInfo();
		if (m_popup_info == null)
		{
			Debug.Log("warning!no info!");
			return;
		}
		TUILabel[] array = new TUILabel[10] { label_damage, label_damage_value, label_fire_rate, label_fire_rate_value, label_blast_radius, label_blast_radius_value, label_knockback, label_knockback_value, label_ammo, label_ammo_value };
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null)
			{
				array[i].gameObject.SetActiveRecursively(true);
			}
		}
		TUIWeaponAttribute weapon_attribute = m_popup_info.weapon_attribute;
		if (weapon_attribute == null)
		{
			return;
		}
		if (label_damage_value != null)
		{
			if (weapon_attribute.damage <= 0f)
			{
				label_damage_value.Text = "--";
			}
			else
			{
				label_damage_value.Text = weapon_attribute.damage.ToString();
			}
		}
		if (label_fire_rate_value != null)
		{
			if (weapon_attribute.fire_rate <= 0f)
			{
				label_fire_rate_value.Text = "--";
			}
			else
			{
				label_fire_rate_value.Text = weapon_attribute.fire_rate.ToString();
			}
		}
		if (label_blast_radius_value != null)
		{
			if (weapon_attribute.blast_radius <= 0f)
			{
				label_blast_radius_value.Text = "--";
			}
			else
			{
				label_blast_radius_value.Text = weapon_attribute.blast_radius.ToString();
			}
		}
		if (label_knockback_value != null)
		{
			if (weapon_attribute.knockback <= 0f)
			{
				label_knockback_value.Text = "--";
			}
			else
			{
				label_knockback_value.Text = weapon_attribute.knockback.ToString();
			}
		}
		if (label_ammo_value != null)
		{
			if (weapon_attribute.ammo <= 0f)
			{
				label_ammo_value.Text = "--";
			}
			else
			{
				label_ammo_value.Text = weapon_attribute.ammo.ToString();
			}
		}
	}

	public void SetArmorAccessoryInfo(TUIPopupInfo m_popup_info)
	{
		HideInfo();
		if (m_popup_info == null)
		{
			Debug.Log("warning!no info!");
			return;
		}
		TUILabel[] array = new TUILabel[3] { label_introduce, label_def, label_def_value };
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null)
			{
				array[i].gameObject.SetActiveRecursively(true);
			}
		}
		TUIArmorAttribute armor_accessory_attribute = m_popup_info.armor_accessory_attribute;
		if (label_introduce != null)
		{
			label_introduce.Text = m_popup_info.introduce;
		}
		if (armor_accessory_attribute != null && label_def_value != null)
		{
			if (armor_accessory_attribute.def <= 0f)
			{
				label_def_value.Text = "--";
			}
			else
			{
				label_def_value.Text = armor_accessory_attribute.def.ToString();
			}
		}
	}

	public void SetOnlyTextInfo(TUIPopupInfo m_popup_info)
	{
		HideInfo();
		if (m_popup_info == null)
		{
			Debug.Log("warning!no info!");
		}
		else if (label_introduce != null)
		{
			label_introduce.gameObject.SetActiveRecursively(true);
			label_introduce.Text = m_popup_info.introduce;
		}
	}

	public void HideInfo()
	{
		TUILabel[] array = new TUILabel[13]
		{
			label_introduce, label_damage_value, label_fire_rate_value, label_blast_radius_value, label_knockback_value, label_ammo_value, label_damage, label_fire_rate, label_blast_radius, label_knockback,
			label_ammo, label_def, label_def_value
		};
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null)
			{
				array[i].gameObject.SetActiveRecursively(false);
			}
		}
	}
}
