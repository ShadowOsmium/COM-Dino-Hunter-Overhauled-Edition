using UnityEngine;

public class PopupWeaponUpdateBuy : MonoBehaviour
{
	public TUILabel label_value_normal;

	public TUILabel label_value_press;

	public TUILabel label_value_disable;

	public TUIMeshSprite img_normal;

	public TUIMeshSprite img_press;

	public TUIMeshSprite img_disable;

	public TUILabel label_text_normal;

	public TUILabel label_text_press;

	public TUILabel label_text_disable;

	private string texture_gold = "title_jingbi";

	private string texture_crystal = "title_shuijing";

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetBtnText(int m_price, UnitType m_unit_type)
	{
		if (label_value_normal != null && label_value_press != null && label_value_disable != null)
		{
			label_value_normal.Text = m_price.ToString();
			label_value_press.Text = m_price.ToString();
			label_value_disable.Text = m_price.ToString();
		}
		if (img_normal != null && img_press != null && img_disable != null)
		{
			switch (m_unit_type)
			{
			case UnitType.Gold:
				img_normal.texture = texture_gold;
				img_press.texture = texture_gold;
				img_disable.texture = texture_gold;
				break;
			case UnitType.Crystal:
				img_normal.texture = texture_crystal;
				img_press.texture = texture_crystal;
				img_disable.texture = texture_crystal;
				break;
			}
		}
		if (label_text_normal != null && label_text_press != null && label_text_disable != null)
		{
			label_text_normal.Text = string.Empty;
			label_text_press.Text = string.Empty;
			label_text_disable.Text = string.Empty;
		}
	}

	public void SetBtnText(string m_text)
	{
		if (label_text_normal != null && label_text_press != null && label_text_disable != null)
		{
			label_text_normal.Text = m_text;
			label_text_press.Text = m_text;
			label_text_disable.Text = m_text;
		}
		if (label_value_normal != null && label_value_press != null && label_value_disable != null)
		{
			label_value_normal.Text = string.Empty;
			label_value_press.Text = string.Empty;
			label_value_disable.Text = string.Empty;
		}
		if (img_normal != null && img_press != null && img_disable != null)
		{
			img_normal.texture = string.Empty;
			img_press.texture = string.Empty;
			img_disable.texture = string.Empty;
		}
	}
}
