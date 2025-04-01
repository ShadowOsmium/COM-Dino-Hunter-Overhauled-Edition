using UnityEngine;

public class GoodsNeedItemBuy : MonoBehaviour
{
	public int m_nIndex = -1;

	public TUILabel label_price_normal;

	public TUILabel label_price_press;

	public TUIMeshSprite img_price_unit_normal;

	public TUIMeshSprite img_price_unit_press;

	private Vector3 m_position = Vector3.zero;

	private string gold_texture = "title_jingbi";

	private string crystal_texture = "title_shuijing";

	private void Start()
	{
		m_position = base.gameObject.transform.localPosition;
	}

	private void Update()
	{
	}

	public void SetInfo(TUIPriceInfo price)
	{
		base.gameObject.transform.localPosition = m_position;
		label_price_normal.Text = price.price.ToString();
		label_price_press.Text = price.price.ToString();
		if (price.unit_type == UnitType.Gold)
		{
			img_price_unit_normal.texture = gold_texture;
			img_price_unit_press.texture = gold_texture;
		}
		else if (price.unit_type == UnitType.Crystal)
		{
			img_price_unit_normal.texture = crystal_texture;
			img_price_unit_press.texture = crystal_texture;
		}
		if (base.GetComponent<Animation>() != null)
		{
			base.GetComponent<Animation>().wrapMode = WrapMode.Loop;
			base.GetComponent<Animation>().Play();
		}
	}

	public void HideInfo()
	{
		label_price_normal.Text = string.Empty;
		label_price_press.Text = string.Empty;
		img_price_unit_normal.texture = string.Empty;
		img_price_unit_press.texture = string.Empty;
		base.gameObject.transform.localPosition = m_position + new Vector3(0f, -1000f, 0f);
		if (base.GetComponent<Animation>() != null)
		{
			base.GetComponent<Animation>().wrapMode = WrapMode.Loop;
			base.GetComponent<Animation>().Stop();
		}
	}
}
