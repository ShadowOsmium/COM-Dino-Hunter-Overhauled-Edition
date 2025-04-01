using UnityEngine;

public class UnlockBlink : MonoBehaviour
{
	public GameObject go_blink;

	public TUIMeshSprite img_texture;

	public TUILabel label_text;

	public TUILabel label_content;

	private bool open_blink;

	private float fade_time = 0.5f;

	private float now_time;

	private string weapon_texture_path = "TUI/Weapon/";

	private string NGUI_weapon_texture_path = "Artist/Textrues/Weapon/";

	private string NGUI_weapon_altas_path = "Artist/Atlas/Weapon/";

	private string skill_path = "TUI/Skill/";

	private void Start()
	{
	}

	private void Update()
	{
		if (open_blink)
		{
			if (now_time < fade_time)
			{
				now_time += Time.deltaTime;
				float num = Mathf.Clamp01(now_time / fade_time) * 2f;
				go_blink.transform.localScale = new Vector3(num, num, 1f);
			}
			go_blink.transform.localEulerAngles += new Vector3(0f, 0f, -1f);
		}
	}

	public void OpenBlinkWeapon(TUIMeshSprite m_sprite, string m_text)
	{
		open_blink = true;
		now_time = 0f;
		base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		img_texture.UseCustomize = true;
		img_texture.CustomizeTexture = m_sprite.CustomizeTexture;
		img_texture.CustomizeRect = m_sprite.CustomizeRect;
		if (img_texture.GetComponent<Animation>() != null)
		{
			img_texture.GetComponent<Animation>().Play();
		}
		if (label_text != null)
		{
			label_text.Text = m_text;
		}
		if (label_content != null)
		{
			label_content.gameObject.SetActiveRecursively(false);
		}
	}

	public void OpenBlinkWeapon(int id, string m_text, bool m_use_customize = false)
	{
		open_blink = true;
		now_time = 0f;
		base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		if (img_texture != null)
		{
			string weaponTexture = TUIMappingInfo.Instance().GetWeaponTexture(id);
			if (m_use_customize)
			{
				SetCustomizeTexture(img_texture, NGUI_weapon_texture_path + weaponTexture, true);
			}
			else
			{
				img_texture.texture = weaponTexture;
			}
			if (img_texture.GetComponent<Animation>() != null)
			{
				img_texture.GetComponent<Animation>().Play();
			}
		}
		if (label_text != null)
		{
			label_text.Text = m_text;
		}
		if (label_content != null)
		{
			label_content.gameObject.SetActiveRecursively(false);
		}
	}

	public void OpenBlinkWeapon(string m_text, bool m_use_customize, string sPath)
	{
		Debug.Log(sPath);
		open_blink = true;
		now_time = 0f;
		base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		if (img_texture != null)
		{
			if (m_use_customize)
			{
				SetCustomizeTexture(img_texture, sPath, true);
			}
			else
			{
				img_texture.texture = sPath;
			}
			if (img_texture.GetComponent<Animation>() != null)
			{
				img_texture.GetComponent<Animation>().Play();
			}
		}
		if (label_text != null)
		{
			label_text.Text = m_text;
		}
		if (label_content != null)
		{
			label_content.gameObject.SetActiveRecursively(false);
		}
	}

	public void OpenBlinkRole(int m_id, string m_text)
	{
		open_blink = true;
		now_time = 0f;
		base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		string roleTexture = TUIMappingInfo.Instance().GetRoleTexture(m_id);
		img_texture.texture = roleTexture;
		if (img_texture.GetComponent<Animation>() != null)
		{
			img_texture.GetComponent<Animation>().Play();
		}
		if (label_text != null)
		{
			label_text.Text = m_text;
		}
		if (label_content != null)
		{
			label_content.gameObject.SetActiveRecursively(false);
		}
	}

	public void OpenBlinkSkill(int m_id, string m_text, bool m_use_customize = false)
	{
		open_blink = true;
		now_time = 0f;
		base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		string skillTexture = TUIMappingInfo.Instance().GetSkillTexture(m_id);
		if (m_use_customize)
		{
			SetCustomizeTexture(img_texture, skill_path + skillTexture);
		}
		else if (img_texture != null)
		{
			img_texture.texture = skillTexture;
		}
		if (img_texture.GetComponent<Animation>() != null)
		{
			img_texture.GetComponent<Animation>().Play();
		}
		if (label_text != null)
		{
			label_text.Text = m_text;
		}
		if (label_content != null)
		{
			label_content.gameObject.SetActiveRecursively(false);
		}
	}

	public void OpenBlinkSkill(string m_texture_name, string m_text, bool m_use_customize = false)
	{
		open_blink = true;
		now_time = 0f;
		base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		if (m_use_customize)
		{
			SetCustomizeTexture(img_texture, skill_path + m_texture_name);
		}
		else
		{
			img_texture.texture = m_texture_name;
		}
		if (img_texture.GetComponent<Animation>() != null)
		{
			img_texture.GetComponent<Animation>().Play();
		}
		if (label_text != null)
		{
			label_text.Text = m_text;
		}
		if (label_content != null)
		{
			label_content.gameObject.SetActiveRecursively(false);
		}
	}

	public void OpenBlinkTitle(string m_title_name, string m_text)
	{
		open_blink = true;
		now_time = 0f;
		base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		if (label_text != null)
		{
			label_text.Text = m_text;
		}
		if (label_content != null)
		{
			label_content.gameObject.SetActiveRecursively(true);
			label_content.Text = m_title_name;
		}
	}

	public void CloseBlink()
	{
		open_blink = false;
		base.transform.localPosition = new Vector3(0f, -1000f, base.transform.localPosition.z);
		img_texture.UseCustomize = false;
		img_texture.CustomizeTexture = null;
		img_texture.CustomizeRect = new Rect(0f, 0f, 0f, 0f);
		img_texture.texture = string.Empty;
	}

	public void SetCustomizeTexture(TUIMeshSprite m_sprite, string m_path, bool m_use_NGUI = false)
	{
		m_sprite.texture = string.Empty;
		m_sprite.UseCustomize = true;
		m_sprite.CustomizeTexture = Resources.Load(m_path) as Texture;
		if (m_sprite.CustomizeTexture == null)
		{
			Debug.Log("lose texture! " + m_path);
			return;
		}
		if (!m_use_NGUI)
		{
			m_sprite.CustomizeRect = new Rect(0f, 0f, m_sprite.CustomizeTexture.width, m_sprite.CustomizeTexture.height);
			return;
		}
		Rect rect = new Rect(0f, 0f, m_sprite.CustomizeTexture.width, m_sprite.CustomizeTexture.height);
		Common.GetAtlasSpriteSize(NGUI_weapon_altas_path + m_sprite.CustomizeTexture.name, m_sprite.CustomizeTexture.name, ref rect);
		m_sprite.CustomizeRect = rect;
	}
}
