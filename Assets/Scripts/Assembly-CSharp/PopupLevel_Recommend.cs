using UnityEngine;

public class PopupLevel_Recommend : MonoBehaviour
{
	public enum RecommendType
	{
		None,
		Role,
		Weapon
	}

	public enum RequiredType
	{
		None,
		Role,
		Weapon
	}

	public enum RecommendBtnState
	{
		Disable,
		RoleBuy,
		RoleEquip,
		WeaponBuy,
		WeaponEquip
	}

	public TUIMeshSprite img_role;

	public TUIMeshSprite img_weapon;

	public LevelStars level_stars;

	public TUILabel label_recommend_title;

	public TUIButtonClick btn_buy;

	public TUILabel label_btn_buy_normal;

	public TUILabel label_btn_buy_press;

	private string weapon_texture_path = "TUI/Weapon/";

	private Vector3 role_normal_pos = Vector3.zero;

	private Vector3 weapon_normal_pos = Vector3.zero;

	private Vector3 stars_normal_pos = Vector3.zero;

	private Vector3 delta_pos = Vector3.zero;

	private RecommendType recommend_type;

	private RequiredType required_type;

	private RecommendBtnState recommend_btn_state;

	private TUIRecommendRoleInfo recommend_role;

	private TUIRecommendWeaponInfo recommend_weapon;

	private bool open_start = true;

	private void Awake()
	{
		delta_pos = new Vector3(27f, 0f, 0f);
		if (img_role != null)
		{
			role_normal_pos = img_role.transform.localPosition;
		}
		if (img_weapon != null)
		{
			weapon_normal_pos = img_weapon.transform.localPosition;
		}
		if (level_stars != null)
		{
			stars_normal_pos = level_stars.transform.localPosition;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public RecommendType GetRecommendType()
	{
		return recommend_type;
	}

	public RequiredType GetRequiredType()
	{
		return required_type;
	}

	public void SetRecommendNone()
	{
		open_start = true;
		recommend_type = RecommendType.None;
		required_type = RequiredType.None;
		label_recommend_title.Text = string.Empty;
		img_role.texture = string.Empty;
		img_weapon.UseCustomize = false;
		img_weapon.CustomizeTexture = null;
		img_role.gameObject.SetActiveRecursively(false);
		img_role.transform.localPosition = role_normal_pos;
		img_weapon.gameObject.SetActiveRecursively(false);
		img_weapon.transform.localPosition = weapon_normal_pos;
		level_stars.gameObject.SetActiveRecursively(false);
		level_stars.transform.localPosition = stars_normal_pos;
		btn_buy.gameObject.SetActiveRecursively(false);
	}

	public void SetRecommendWeapon(TUIRecommendWeaponInfo m_recommend_weapon)
	{
		open_start = true;
		if (m_recommend_weapon == null)
		{
			return;
		}
		recommend_weapon = m_recommend_weapon;
		recommend_type = RecommendType.Weapon;
		bool required = m_recommend_weapon.required;
		int level_need = m_recommend_weapon.level_need;
		int level = m_recommend_weapon.level;
		bool have_equip = m_recommend_weapon.have_equip;
		int id = m_recommend_weapon.id;
		if (required)
		{
			required_type = RequiredType.Weapon;
			label_recommend_title.Text = "Required";
		}
		else
		{
			required_type = RequiredType.None;
			label_recommend_title.Text = "Recommended";
		}
		img_role.texture = string.Empty;
		img_role.gameObject.SetActiveRecursively(false);
		string weaponTexture = TUIMappingInfo.Instance().GetWeaponTexture(id);
		SetCustomizeTexture(img_weapon, weapon_texture_path + weaponTexture);
		img_weapon.gameObject.SetActiveRecursively(true);
		level_stars.SetStars(level_need);
		level_stars.gameObject.SetActiveRecursively(true);
		if (level < level_need)
		{
			label_btn_buy_normal.Text = "Buy";
			label_btn_buy_press.Text = "Buy";
			btn_buy.gameObject.SetActiveRecursively(true);
			btn_buy.Show();
			recommend_btn_state = RecommendBtnState.WeaponBuy;
			img_weapon.transform.localPosition = weapon_normal_pos;
			level_stars.transform.localPosition = stars_normal_pos;
			if (required)
			{
				open_start = false;
			}
		}
		else
		{
			if (level_need == 0 && required)
			{
				level_stars.gameObject.SetActiveRecursively(false);
			}
			if (!have_equip)
			{
				label_btn_buy_normal.Text = "Equip";
				label_btn_buy_press.Text = "Equip";
				btn_buy.gameObject.SetActiveRecursively(true);
				btn_buy.Show();
				recommend_btn_state = RecommendBtnState.WeaponEquip;
				img_weapon.transform.localPosition = weapon_normal_pos;
				level_stars.transform.localPosition = stars_normal_pos;
				if (required)
				{
					open_start = false;
				}
			}
			else
			{
				btn_buy.gameObject.SetActiveRecursively(false);
				recommend_btn_state = RecommendBtnState.Disable;
				img_weapon.transform.localPosition = weapon_normal_pos + delta_pos;
				level_stars.transform.localPosition = stars_normal_pos + delta_pos;
			}
		}
		UpdateRequiredAni();
	}

	public void SetRecommendRole(TUIRecommendRoleInfo m_recommend_role)
	{
		open_start = true;
		recommend_role = m_recommend_role;
		recommend_type = RecommendType.Role;
		bool required = m_recommend_role.required;
		bool have_buy = m_recommend_role.have_buy;
		bool have_equip = m_recommend_role.have_equip;
		int id = m_recommend_role.id;
		if (required)
		{
			required_type = RequiredType.Role;
			label_recommend_title.Text = "Required";
		}
		else
		{
			required_type = RequiredType.None;
			label_recommend_title.Text = "Recommended";
		}
		img_weapon.UseCustomize = false;
		img_weapon.CustomizeTexture = null;
		img_weapon.gameObject.SetActiveRecursively(false);
		level_stars.gameObject.SetActiveRecursively(false);
		img_role.texture = TUIMappingInfo.Instance().GetRoleTexture(id);
		img_role.gameObject.SetActiveRecursively(true);
		if (!have_buy)
		{
			label_btn_buy_normal.Text = "Buy";
			label_btn_buy_press.Text = "Buy";
			btn_buy.gameObject.SetActiveRecursively(true);
			btn_buy.Show();
			recommend_btn_state = RecommendBtnState.RoleBuy;
			img_role.transform.localPosition = role_normal_pos;
			if (required)
			{
				open_start = false;
			}
		}
		else if (!have_equip)
		{
			label_btn_buy_normal.Text = "Use";
			label_btn_buy_press.Text = "Use";
			btn_buy.gameObject.SetActiveRecursively(true);
			btn_buy.Show();
			recommend_btn_state = RecommendBtnState.RoleEquip;
			img_role.transform.localPosition = role_normal_pos;
			if (required)
			{
				open_start = false;
			}
		}
		else
		{
			btn_buy.gameObject.SetActiveRecursively(false);
			recommend_btn_state = RecommendBtnState.Disable;
			img_role.transform.localPosition = role_normal_pos + delta_pos;
		}
		UpdateRequiredAni();
	}

	private void SetCustomizeTexture(TUIMeshSprite m_sprite, string m_path)
	{
		m_sprite.texture = string.Empty;
		m_sprite.UseCustomize = true;
		m_sprite.CustomizeTexture = Resources.Load(m_path) as Texture;
		if (m_sprite.CustomizeTexture == null)
		{
			Debug.Log("lose texture!");
		}
		else
		{
			m_sprite.CustomizeRect = new Rect(0f, 0f, m_sprite.CustomizeTexture.width, m_sprite.CustomizeTexture.height);
		}
	}

	public RecommendBtnState GetRecommendBtnState()
	{
		return recommend_btn_state;
	}

	public TUIRecommendWeaponInfo GetRecommendWeaponInfo()
	{
		return recommend_weapon;
	}

	public TUIRecommendRoleInfo GetRecommendRoleInfo()
	{
		return recommend_role;
	}

	public void UpdateRequiredAni()
	{
		if (img_role != null && img_role.GetComponent<Animation>() != null)
		{
			img_role.GetComponent<Animation>().Stop();
		}
		if (img_weapon != null && img_weapon.GetComponent<Animation>() != null)
		{
			img_weapon.GetComponent<Animation>().Stop();
		}
		if (open_start)
		{
			return;
		}
		if (required_type == RequiredType.Role)
		{
			if (img_role != null && img_role.GetComponent<Animation>() != null)
			{
				img_role.GetComponent<Animation>().wrapMode = WrapMode.Loop;
				img_role.GetComponent<Animation>().Play();
			}
			if (img_weapon != null && img_weapon.GetComponent<Animation>() != null)
			{
				img_weapon.GetComponent<Animation>().Stop();
			}
		}
		else if (required_type == RequiredType.Weapon)
		{
			if (img_role != null && img_role.GetComponent<Animation>() != null)
			{
				img_role.GetComponent<Animation>().Stop();
			}
			if (img_weapon != null && img_weapon.GetComponent<Animation>() != null)
			{
				img_weapon.GetComponent<Animation>().wrapMode = WrapMode.Loop;
				img_weapon.GetComponent<Animation>().Play();
			}
		}
	}

	public bool GetOpenStart()
	{
		return open_start;
	}
}
