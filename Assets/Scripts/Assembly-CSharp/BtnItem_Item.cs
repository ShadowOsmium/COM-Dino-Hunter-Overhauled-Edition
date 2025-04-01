using UnityEngine;

public class BtnItem_Item : MonoBehaviour
{
	public int index;

	public PopupType popup_type;

	public TUIMeshSprite img_normal;

	public TUIMeshSprite img_pressed;

	public TUILabel label_value;

	public TUIMeshSprite img_new;

	public TUIMeshSprite img_bg_normal;

	public TUIMeshSprite img_bg_press;

	public TUIMeshSprite img_duoren_sign_normal;

	public TUIMeshSprite img_duoren_sign_press;

	public TUIMeshSprite img_weapon_bg;

	public TUIButtonClick btn_control;

	private string duoren_bg_texture = "icon_bg";

	private string normal_bg_texture = "zhuangbeidaojudikuang";

	private string duoren_sign_texture = "icon_bg_info";

	private string texture_mark = "new";

	private string texture_new = "new2";

	protected TUIPopupInfo m_PopupInfo;

	protected NewMarkType m_NewMark;

	public bool m_bActiveSkill { get; set; }

	public PopupType nPopupType
	{
		get
		{
			return popup_type;
		}
	}

	private void Awake()
	{
		m_NewMark = NewMarkType.None;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetInfo(TUIPopupInfo popupinfo, bool isanimate = false, bool iscustom = false)
	{
		m_PopupInfo = popupinfo;
		if (m_PopupInfo == null)
		{
			img_normal.texture = string.Empty;
			img_normal.CustomizeTexture = null;
			img_pressed.texture = string.Empty;
			img_pressed.CustomizeTexture = null;
			if (label_value != null)
			{
				label_value.Text = string.Empty;
			}
			if (isanimate && btn_control != null && btn_control.GetComponent<Animation>() != null)
			{
				btn_control.GetComponent<Animation>().Play();
			}
			if (img_weapon_bg != null)
			{
				img_weapon_bg.gameObject.SetActiveRecursively(true);
			}
			return;
		}
		string text = m_PopupInfo.m_sIcon;
		if (text.Length < 1)
		{
			text = TUIMappingInfo.Instance().GetTextureByID(m_PopupInfo.m_PopupType, m_PopupInfo.texture_id, m_PopupInfo.m_PopupType != PopupType.Skills01);
		}
		if (m_PopupInfo.IsRole())
		{
			img_normal.texture = text;
			img_pressed.texture = text;
		}
		else if (m_PopupInfo.IsSkill())
		{
			if (!iscustom)
			{
				img_normal.texture = text;
				img_pressed.texture = text;
			}
			else
			{
				SetCustomizeTexture(img_normal, TUIMappingInfo.Instance().m_sPathCustomSkillTex + "/" + text, false, string.Empty);
				SetCustomizeTexture(img_pressed, TUIMappingInfo.Instance().m_sPathCustomSkillTex + "/" + text, false, string.Empty);
			}
			if (m_PopupInfo.m_PopupType == PopupType.Skills01)
			{
				string empty = string.Empty;
				string empty2 = string.Empty;
				string empty3 = string.Empty;
				if (m_PopupInfo.duoren_skill)
				{
					empty = duoren_bg_texture;
					empty2 = duoren_sign_texture;
					empty3 = duoren_sign_texture;
				}
				else
				{
					empty = normal_bg_texture;
				}
				if (img_duoren_sign_normal != null)
				{
					img_duoren_sign_normal.texture = empty2;
				}
				if (img_duoren_sign_press != null)
				{
					img_duoren_sign_press.texture = empty3;
				}
				if (img_bg_normal != null)
				{
					img_bg_normal.texture = empty;
				}
				if (img_bg_press != null)
				{
					img_bg_press.texture = empty;
				}
			}
		}
		else if (m_PopupInfo.IsWeapon() || m_PopupInfo.IsArmor() || m_PopupInfo.IsAccessory())
		{
			if (!iscustom)
			{
				Debug.Log(m_PopupInfo.texture_id + " " + text);
				img_normal.texture = text;
				img_pressed.texture = text;
			}
			else
			{
				bool use_NGUI = false;
				string path = string.Empty;
				string pathforatlas = string.Empty;
				if (m_PopupInfo.IsWeapon())
				{
					path = TUIMappingInfo.Instance().m_sPathRootCustomTex + "/Weapon/" + text;
					use_NGUI = true;
					pathforatlas = TUIMappingInfo.Instance().m_sPathRootCustomAtlas + "/Weapon/" + text;
				}
				else if (m_PopupInfo.IsArmor())
				{
					path = TUIMappingInfo.Instance().m_sPathRootCustomTex + "/Armor/" + text;
				}
				else if (m_PopupInfo.IsAccessory())
				{
					path = TUIMappingInfo.Instance().m_sPathRootCustomTex + "/Accessory/" + text;
				}
				SetCustomizeTexture(img_normal, path, use_NGUI, pathforatlas);
				SetCustomizeTexture(img_pressed, path, use_NGUI, pathforatlas);
			}
			if (img_weapon_bg != null)
			{
				img_weapon_bg.gameObject.SetActiveRecursively(false);
			}
		}
		if (isanimate && btn_control != null && btn_control.GetComponent<Animation>() != null)
		{
			btn_control.GetComponent<Animation>().Play();
		}
	}

	public void SetCustomizeTexture(TUIMeshSprite m_sprite, string m_path, bool m_use_NGUI = false, string pathforatlas = "")
	{
		m_sprite.texture = string.Empty;
		m_sprite.UseCustomize = true;
		m_sprite.CustomizeTexture = Resources.Load(m_path) as Texture;
		if (m_sprite.CustomizeTexture == null)
		{
			Debug.Log("lose texture!");
			return;
		}
		if (!m_use_NGUI)
		{
			m_sprite.CustomizeRect = new Rect(0f, 0f, m_sprite.CustomizeTexture.width, m_sprite.CustomizeTexture.height);
			return;
		}
		Rect rect = new Rect(0f, 0f, m_sprite.CustomizeTexture.width, m_sprite.CustomizeTexture.height);
		Common.GetAtlasSpriteSize(pathforatlas, m_sprite.CustomizeTexture.name, ref rect);
		m_sprite.CustomizeRect = rect;
	}

	public void PlayAnimation()
	{
		base.GetComponent<Animation>().Play();
	}

	public int GetIndex()
	{
		return index;
	}

	public int GetNowEquipID()
	{
		if (m_PopupInfo == null)
		{
			return -1;
		}
		return m_PopupInfo.texture_id;
	}

	public TUIPopupInfo GetInfo()
	{
		return m_PopupInfo;
	}

	public void SetMark(NewMarkType mark)
	{
		m_NewMark = mark;
		if (!(img_new == null))
		{
			switch (mark)
			{
			case NewMarkType.Mark:
				img_new.texture = texture_mark;
				break;
			case NewMarkType.New:
				img_new.texture = texture_new;
				break;
			default:
				img_new.texture = string.Empty;
				break;
			}
		}
	}

	public NewMarkType GetMark()
	{
		return m_NewMark;
	}
}
