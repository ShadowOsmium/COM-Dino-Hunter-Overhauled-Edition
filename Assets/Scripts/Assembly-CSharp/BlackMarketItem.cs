using UnityEngine;

public class BlackMarketItem : MonoBehaviour
{
	public TUIMeshSprite img_bg;

	public TUIMeshSprite img_frame;

	public TUIMeshSprite img_frame_choose;

	public TUIMeshSprite img_sale;

	public TUILabel label_sale;

	public TUILabel label_time;

	private TUIBlackMarketItem m_BlackMarketItem;

	private string NGUI_goods_texture_path = "Artist/Textrues/Material/";

	private string NGUI_goods_altas_path = "Artist/Atlas/Material/";

	private string texture_path = "Artist/Textrues/Weapon/";

	private void Awake()
	{
		if (img_sale != null)
		{
			img_sale.gameObject.SetActiveRecursively(false);
		}
		if (label_sale != null)
		{
			label_sale.gameObject.SetActiveRecursively(false);
		}
		DoUnChoose();
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetInfo(TUIBlackMarketItem blackmarketitem)
	{
		m_BlackMarketItem = blackmarketitem;
		if (m_BlackMarketItem == null)
		{
			return;
		}
		string text = blackmarketitem.m_sIcon;
		if (text.Length < 1)
		{
			text = TUIMappingInfo.Instance().GetWeaponTexture(m_BlackMarketItem.m_nItemID);
		}
		string iconPath = blackmarketitem.GetIconPath();
		iconPath = ((iconPath.Length <= 0) ? text : (iconPath + "/" + text));
		if (img_bg != null)
		{
			if (blackmarketitem.IsWeapon())
			{
				SetCustomizeTexture(img_bg, iconPath, true, TUIMappingInfo.Instance().m_sPathRootCustomAtlas + "/Weapon/" + text);
			}
			else
			{
				SetCustomizeTexture(img_bg, iconPath, false, string.Empty);
			}
		}
	}

	public TUIBlackMarketItem GetInfo()
	{
		return m_BlackMarketItem;
	}

	public void DoChoose()
	{
		img_frame.gameObject.SetActiveRecursively(false);
		img_frame_choose.gameObject.SetActiveRecursively(true);
	}

	public void DoUnChoose()
	{
		img_frame.gameObject.SetActiveRecursively(true);
		img_frame_choose.gameObject.SetActiveRecursively(false);
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
}
