using UnityEngine;

public class LabelInfo_Weapon : MonoBehaviour
{
	public TUILabel label_damage;

	public TUILabel label_damage_value;

	public TUILabel label_fire_rate;

	public TUILabel label_fire_rate_value;

	public TUILabel label_blast_radius;

	public TUILabel label_blast_radius_value;

	public TUILabel label_knockback;

	public TUILabel label_knockback_value;

	public TUILabel label_ammo;

	public TUILabel label_ammo_value;

	public TUILabel label_introduce;

	public TUILabel label_introduce_unlock;

	public TUILabel label_max_value;

	public TUILabel label_unlock_text;

	public TUILabel label_def;

	public TUILabel label_def_value;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetWeaponInfo(int m_damage, float m_fire_rate, int m_blast_radius, int m_knockback, int m_ammo, int m_damage_max, bool m_unlock, string m_unlock_text)
	{
		SetNull();
		if (!m_unlock)
		{
			label_unlock_text.gameObject.SetActiveRecursively(true);
			label_unlock_text.Text = m_unlock_text;
		}
		else
		{
			label_damage.gameObject.SetActiveRecursively(true);
			label_damage_value.gameObject.SetActiveRecursively(true);
			label_fire_rate.gameObject.SetActiveRecursively(true);
			label_fire_rate_value.gameObject.SetActiveRecursively(true);
			label_blast_radius.gameObject.SetActiveRecursively(true);
			label_blast_radius_value.gameObject.SetActiveRecursively(true);
			label_knockback.gameObject.SetActiveRecursively(true);
			label_knockback_value.gameObject.SetActiveRecursively(true);
			label_ammo.gameObject.SetActiveRecursively(true);
			label_ammo_value.gameObject.SetActiveRecursively(true);
			label_max_value.gameObject.SetActiveRecursively(true);
		}
		if (m_damage == 0)
		{
			label_damage_value.Text = "--";
		}
		else
		{
			label_damage_value.Text = m_damage.ToString();
		}
		if (m_damage_max == 0)
		{
			label_max_value.Text = "--";
		}
		else
		{
			label_max_value.Text = "(Max " + m_damage_max + ")";
		}
		if (m_fire_rate == 0f)
		{
			label_fire_rate_value.Text = "--";
		}
		else
		{
			label_fire_rate_value.Text = m_fire_rate.ToString();
		}
		if (m_blast_radius == 0)
		{
			label_blast_radius_value.Text = "--";
		}
		else
		{
			label_blast_radius_value.Text = m_blast_radius.ToString();
		}
		if (m_knockback == 0)
		{
			label_knockback_value.Text = "--";
		}
		else
		{
			label_knockback_value.Text = m_knockback.ToString();
		}
		if (m_ammo == 0)
		{
			label_ammo_value.Text = "--";
		}
		else
		{
			label_ammo_value.Text = m_ammo.ToString();
		}
	}

	public void SetWeaponInfo(float m_fire_rate, int m_blast_radius, int m_knockback, int m_ammo, int m_damage_max, bool m_unlock, string m_unlock_text)
	{
		SetNull();
		if (!m_unlock)
		{
			label_introduce_unlock.gameObject.SetActiveRecursively(true);
			label_introduce_unlock.Text = m_unlock_text;
		}
		else
		{
			label_fire_rate.gameObject.SetActiveRecursively(true);
			label_fire_rate_value.gameObject.SetActiveRecursively(true);
			label_blast_radius.gameObject.SetActiveRecursively(true);
			label_blast_radius_value.gameObject.SetActiveRecursively(true);
			label_knockback.gameObject.SetActiveRecursively(true);
			label_knockback_value.gameObject.SetActiveRecursively(true);
			label_ammo.gameObject.SetActiveRecursively(true);
			label_ammo_value.gameObject.SetActiveRecursively(true);
			label_max_value.gameObject.SetActiveRecursively(true);
		}
		if (m_damage_max == 0)
		{
			label_max_value.Text = "--";
		}
		else
		{
			label_max_value.Text = "(Max " + m_damage_max + ")";
		}
		if (m_fire_rate == 0f)
		{
			label_fire_rate_value.Text = "--";
		}
		else
		{
			label_fire_rate_value.Text = m_fire_rate.ToString();
		}
		if (m_blast_radius == 0)
		{
			label_blast_radius_value.Text = "--";
		}
		else
		{
			label_blast_radius_value.Text = m_blast_radius.ToString();
		}
		if (m_knockback == 0)
		{
			label_knockback_value.Text = "--";
		}
		else
		{
			label_knockback_value.Text = m_knockback.ToString();
		}
		if (m_ammo == 0)
		{
			label_ammo_value.Text = "--";
		}
		else
		{
			label_ammo_value.Text = m_ammo.ToString();
		}
	}

	public void SetArmorAccessoryInfo(string m_introduce, int m_def, int m_def_max, bool m_unlock, string m_unlock_text = "")
	{
		SetNull();
		if (!m_unlock)
		{
			label_unlock_text.gameObject.SetActiveRecursively(true);
			label_unlock_text.Text = m_unlock_text;
		}
		else
		{
			label_def.gameObject.SetActiveRecursively(true);
			label_def_value.gameObject.SetActiveRecursively(true);
			label_introduce.gameObject.SetActiveRecursively(true);
			label_max_value.gameObject.SetActiveRecursively(true);
		}
		if (m_def_max == 0)
		{
			label_max_value.Text = "--";
		}
		else
		{
			label_max_value.Text = "(Max " + m_def_max + ")";
		}
		if (m_def == 0)
		{
			label_def_value.Text = "--";
		}
		else
		{
			label_def_value.Text = m_def.ToString();
		}
		label_introduce.Text = m_introduce;
	}

	public void SetDamage(int m_damage)
	{
		if (label_damage != null)
		{
			label_damage.gameObject.SetActiveRecursively(true);
		}
		if (label_damage_value != null)
		{
			label_damage_value.gameObject.SetActiveRecursively(true);
			label_damage_value.Text = m_damage.ToString();
		}
	}

	public void OpenDamageAnimation()
	{
		if (label_damage_value != null && label_damage_value.GetComponent<Animation>() != null && label_damage_value.gameObject.activeInHierarchy)
		{
			label_damage_value.GetComponent<Animation>().Play();
		}
	}

	public void SetDef(int m_def)
	{
		if (label_def != null)
		{
			label_def.gameObject.SetActiveRecursively(true);
		}
		if (label_def_value != null)
		{
			label_def_value.gameObject.SetActiveRecursively(true);
			if (m_def == 0)
			{
				label_def_value.Text = "--";
			}
			else
			{
				label_def_value.Text = m_def.ToString();
			}
		}
	}

	public void OpenDefAnimation()
	{
		if (label_def_value != null && label_damage_value.gameObject.active && label_def_value.GetComponent<Animation>() != null)
		{
			label_def_value.GetComponent<Animation>().Play();
		}
	}

	public void SetNull()
	{
		TUILabel[] array = new TUILabel[15]
		{
			label_damage, label_damage_value, label_fire_rate, label_fire_rate_value, label_blast_radius, label_blast_radius_value, label_knockback, label_knockback_value, label_ammo, label_ammo_value,
			label_introduce, label_max_value, label_unlock_text, label_def, label_def_value
		};
		foreach (TUILabel tUILabel in array)
		{
			if (tUILabel != null)
			{
				tUILabel.gameObject.SetActiveRecursively(false);
			}
		}
	}
}
