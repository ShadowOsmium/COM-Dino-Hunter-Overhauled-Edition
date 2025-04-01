using UnityEngine;

public class GoodsNeedItemImg : MonoBehaviour
{
	public TUIMeshSprite img_goods;

	public TUIMeshSprite img_bg;

	private string goods_texture_path = "TUI/Goods/";

	private string NGUI_goods_texture_path = "Artist/Textrues/Material/";

	private string NGUI_goods_altas_path = "Artist/Atlas/Material/";

	private string texture_quality01 = "kuangdj_1";

	private string texture_quality02 = "kuangdj_2";

	private string texture_quality03 = "kuangdj_3";

	private string texture_quality04 = "kuangdj_4";

	private string texture_quality05 = "kuangdj_5";

	private string texture_quality06 = "kuangdj_6";

	private int goods_id;

	private string goods_name = string.Empty;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetInfo(int m_goods_id, GoodsQualityType m_type, string m_goods_name)
	{
		goods_id = m_goods_id;
		goods_name = m_goods_name;
		string stashTexture = TUIMappingInfo.Instance().GetStashTexture(m_goods_id);
		SetGoodsCustomizeTexture(img_goods, NGUI_goods_texture_path + stashTexture, true);
		switch (m_type)
		{
		case GoodsQualityType.Quality01:
			img_bg.texture = texture_quality01;
			break;
		case GoodsQualityType.Quality02:
			img_bg.texture = texture_quality02;
			break;
		case GoodsQualityType.Quality03:
			img_bg.texture = texture_quality03;
			break;
		case GoodsQualityType.Quality04:
			img_bg.texture = texture_quality04;
			break;
		case GoodsQualityType.Quality05:
			img_bg.texture = texture_quality05;
			break;
		case GoodsQualityType.Quality06:
			img_bg.texture = texture_quality06;
			break;
		}
	}

	private void SetGoodsCustomizeTexture(TUIMeshSprite m_sprite, string m_path, bool m_use_NGUI = false)
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
		Common.GetAtlasSpriteSize(NGUI_goods_altas_path + m_sprite.CustomizeTexture.name, m_sprite.CustomizeTexture.name, ref rect);
		m_sprite.CustomizeRect = rect;
	}

	public string GetGoodsName()
	{
		return goods_name;
	}

	public int GetGoodsID()
	{
		return goods_id;
	}
}
