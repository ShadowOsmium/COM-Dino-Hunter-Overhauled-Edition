using UnityEngine;

public class Btn_Select_Stash : MonoBehaviour
{
	public TUIMeshSprite img_quality;

	public TUIMeshSprite img_mask;

	public TUIMeshSprite img_texture;

	public TUILabel label_count;

	private TUIGoodsInfo goods_info;

	private int index;

	private string goods_texture_path = "TUI/Goods/";

	private string NGUI_goods_texture_path = "Artist/Textrues/Material/";

	private string NGUI_goods_altas_path = "Artist/Atlas/Material/";

	private string texture_quality01 = "kuangdj_1";

	private string texture_quality02 = "kuangdj_2";

	private string texture_quality03 = "kuangdj_3";

	private string texture_quality04 = "kuangdj_4";

	private string texture_quality05 = "kuangdj_5";

	private string texture_quality06 = "kuangdj_6";

	private void Awake()
	{
		if (img_quality == null)
		{
			Debug.Log("error! no img_quality!");
		}
		if (img_mask == null)
		{
			Debug.Log("error! no img_mask!");
		}
		if (img_texture == null)
		{
			Debug.Log("error! no img_texture!");
		}
		if (label_count == null)
		{
			Debug.Log("error! no label_count!");
		}
		img_mask.gameObject.SetActiveRecursively(false);
		img_quality.gameObject.SetActiveRecursively(false);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetIndex(int id)
	{
		index = id;
	}

	public int GetIndex()
	{
		return index;
	}

	public void SetGoodsInfo(TUIGoodsInfo m_goods_info)
	{
		goods_info = m_goods_info;
		string stashTexture = TUIMappingInfo.Instance().GetStashTexture(m_goods_info.id);
		SetGoodsCustomizeTexture(img_texture, NGUI_goods_texture_path + stashTexture, true);
		SetGoodsCount(m_goods_info.count);
		SetQualityTexture(goods_info.quality);
	}

	public void SetGoodsCustomizeTexture(TUIMeshSprite m_sprite, string m_path, bool m_use_NGUI = false)
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

	public void SetGoodsTexture(string m_texture)
	{
		img_texture.texture = m_texture;
	}

	public void SetQualityTexture(GoodsQualityType m_type)
	{
		img_quality.gameObject.SetActiveRecursively(true);
		switch (m_type)
		{
		case GoodsQualityType.Quality01:
			img_quality.texture = texture_quality01;
			break;
		case GoodsQualityType.Quality02:
			img_quality.texture = texture_quality02;
			break;
		case GoodsQualityType.Quality03:
			img_quality.texture = texture_quality03;
			break;
		case GoodsQualityType.Quality04:
			img_quality.texture = texture_quality04;
			break;
		case GoodsQualityType.Quality05:
			img_quality.texture = texture_quality05;
			break;
		case GoodsQualityType.Quality06:
			img_quality.texture = texture_quality06;
			break;
		}
	}

	public void SetGoodsCount(int m_count)
	{
		if (m_count == 0)
		{
			label_count.Text = string.Empty;
			img_mask.gameObject.SetActiveRecursively(true);
			img_mask.color = new Color(1f, 1f, 1f, 0.1f);
			img_texture.color = new Color(1f, 1f, 1f, 0.3f);
			img_quality.gameObject.SetActiveRecursively(true);
			img_quality.color = new Color(1f, 1f, 1f, 0.5f);
		}
		else
		{
			label_count.Text = m_count.ToString();
			img_mask.gameObject.SetActiveRecursively(false);
			img_texture.color = new Color(1f, 1f, 1f, 1f);
			img_quality.gameObject.SetActiveRecursively(true);
			img_quality.color = new Color(1f, 1f, 1f, 1f);
		}
	}

	public TUIGoodsInfo GetGoodsInfo()
	{
		return goods_info;
	}

	public void SetSellInfo(int m_count)
	{
		SetGoodsCount(m_count);
		goods_info.SetCount(m_count);
	}
}
