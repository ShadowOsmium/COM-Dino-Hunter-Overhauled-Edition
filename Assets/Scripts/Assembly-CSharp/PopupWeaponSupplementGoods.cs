using UnityEngine;

public class PopupWeaponSupplementGoods : MonoBehaviour
{
	public TUIMeshSprite[] m_arrGoodsIcon;

	public TUIMeshSprite[] m_arrGoodsBG;

	public TUILabel[] m_arrGoodsNum;

	public TUIMeshSprite img_price_unit;

	public TUILabel label_price_value;

	private string goods_texture_path = "TUI/Goods/";

	private string NGUI_goods_texture_path = "Artist/Textrues/Material/";

	private string NGUI_goods_altas_path = "Artist/Atlas/Material/";

	private string gold_texture = "title_jingbi";

	private string crystal_texture = "title_shuijing";

	private string texture_quality01 = "kuangdj_1";

	private string texture_quality02 = "kuangdj_2";

	private string texture_quality03 = "kuangdj_3";

	private string texture_quality04 = "kuangdj_4";

	private string texture_quality05 = "kuangdj_5";

	private string texture_quality06 = "kuangdj_6";

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetGoodsInfo(int m_index, int m_goods_id, int m_goods_value, GoodsQualityType m_type)
	{
		string stashTexture = TUIMappingInfo.Instance().GetStashTexture(m_goods_id);
		if (m_index >= 0 && m_index < m_arrGoodsIcon.Length && m_arrGoodsIcon[m_index] != null)
		{
			SetCustomizeTexture(m_arrGoodsIcon[m_index], NGUI_goods_texture_path + stashTexture, true);
		}
		if (m_index >= 0 && m_index < m_arrGoodsNum.Length && m_arrGoodsNum[m_index] != null)
		{
			m_arrGoodsNum[m_index].Text = m_goods_value.ToString();
		}
		if (m_index >= 0 && m_index < m_arrGoodsBG.Length && m_arrGoodsBG[m_index] != null)
		{
			m_arrGoodsBG[m_index].texture = GetQualityType(m_type);
		}
	}

	public void SetPriceInfo(int m_price, UnitType m_unit)
	{
		if (img_price_unit == null || label_price_value == null)
		{
			Debug.Log("error!");
			return;
		}
		switch (m_unit)
		{
		case UnitType.Crystal:
			if (img_price_unit != null)
			{
				img_price_unit.texture = crystal_texture;
				img_price_unit.transform.localPosition = new Vector3(img_price_unit.transform.localPosition.x, -44f, img_price_unit.transform.localPosition.z);
			}
			break;
		case UnitType.Gold:
			if (img_price_unit != null)
			{
				img_price_unit.texture = gold_texture;
				img_price_unit.transform.localPosition = new Vector3(img_price_unit.transform.localPosition.x, -44f, img_price_unit.transform.localPosition.z);
			}
			break;
		}
		label_price_value.Text = m_price.ToString();
		label_price_value.transform.localPosition = new Vector3(label_price_value.transform.localPosition.x, -44f, label_price_value.transform.localPosition.z);
	}

	public void SetOnlyPriceInfo(int m_price, UnitType m_unit)
	{
		if (img_price_unit == null || label_price_value == null)
		{
			Debug.Log("error!");
			return;
		}
		switch (m_unit)
		{
		case UnitType.Crystal:
			if (img_price_unit != null)
			{
				img_price_unit.texture = crystal_texture;
				img_price_unit.transform.localPosition = new Vector3(img_price_unit.transform.localPosition.x, 0f, img_price_unit.transform.localPosition.z);
			}
			break;
		case UnitType.Gold:
			if (img_price_unit != null)
			{
				img_price_unit.texture = gold_texture;
				img_price_unit.transform.localPosition = new Vector3(img_price_unit.transform.localPosition.x, 0f, img_price_unit.transform.localPosition.z);
			}
			break;
		}
		label_price_value.Text = m_price.ToString();
		label_price_value.transform.localPosition = new Vector3(label_price_value.transform.localPosition.x, 0f, label_price_value.transform.localPosition.z);
	}

	public void ClearInfo()
	{
		for (int i = 0; i < m_arrGoodsIcon.Length; i++)
		{
			if (!(m_arrGoodsIcon[i] == null))
			{
				m_arrGoodsIcon[i].texture = string.Empty;
				m_arrGoodsIcon[i].UseCustomize = false;
				m_arrGoodsIcon[i].CustomizeTexture = null;
				m_arrGoodsIcon[i].CustomizeRect = new Rect(0f, 0f, 0f, 0f);
			}
		}
		for (int j = 0; j < m_arrGoodsNum.Length; j++)
		{
			if (!(m_arrGoodsNum[j] == null))
			{
				m_arrGoodsNum[j].Text = string.Empty;
			}
		}
		for (int k = 0; k < m_arrGoodsBG.Length; k++)
		{
			if (!(m_arrGoodsBG[k] == null))
			{
				m_arrGoodsBG[k].texture = string.Empty;
			}
		}
		if (img_price_unit != null)
		{
			img_price_unit.texture = string.Empty;
		}
		if (label_price_value != null)
		{
			label_price_value.Text = string.Empty;
		}
	}

	public void SetCustomizeTexture(TUIMeshSprite m_sprite, string m_path, bool m_use_NGUI = false)
	{
		if (m_sprite == null)
		{
			Debug.Log("error!");
			return;
		}
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

	public string GetQualityType(GoodsQualityType m_type)
	{
		switch (m_type)
		{
		case GoodsQualityType.Quality01:
			return texture_quality01;
		case GoodsQualityType.Quality02:
			return texture_quality02;
		case GoodsQualityType.Quality03:
			return texture_quality03;
		case GoodsQualityType.Quality04:
			return texture_quality04;
		case GoodsQualityType.Quality05:
			return texture_quality05;
		case GoodsQualityType.Quality06:
			return texture_quality06;
		default:
			return texture_quality01;
		}
	}
}
