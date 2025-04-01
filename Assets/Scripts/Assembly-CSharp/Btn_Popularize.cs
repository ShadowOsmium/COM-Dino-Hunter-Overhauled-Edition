using UnityEngine;

public class Btn_Popularize : MonoBehaviour
{
	private string texture_path = "TUI/Popularize/";

	private string img_bg_path01 = "icon_1";

	private string img_bg_path02 = "icon_2";

	private string img_texture_path01 = "icon_3";

	private string img_texture_path02 = "halo_icon_3";

	private string img_title_path01 = "icon_2";

	private string img_title_path02 = "halo_icon_2";

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void DoCreate(PopularizeType m_type)
	{
	}

	private void SetCustomizeTexture(TUIMeshSprite m_sprite, string m_path)
	{
		if (m_sprite == null)
		{
			Debug.Log("no sprite!");
			return;
		}
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

	public void Show()
	{
		base.gameObject.SetActiveRecursively(true);
		TUIButton component = GetComponent<TUIButton>();
		if (component != null)
		{
			component.Reset();
		}
		if (base.GetComponent<Animation>() != null)
		{
			base.GetComponent<Animation>().wrapMode = WrapMode.Loop;
			base.GetComponent<Animation>().Play();
		}
	}

	public void Hide()
	{
		base.gameObject.SetActiveRecursively(false);
	}
}
