using UnityEngine;

public class PopupAddBtnBuy : MonoBehaviour
{
	public TUILabel label_value_normal;

	public TUILabel label_value_press;

	public TUIMeshSprite img_normal;

	public TUIMeshSprite img_press;

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
		label_value_normal.Text = m_price.ToString();
		label_value_press.Text = m_price.ToString();
		switch (m_unit_type)
		{
		case UnitType.Gold:
			img_normal.texture = texture_gold;
			img_press.texture = texture_gold;
			break;
		case UnitType.Crystal:
			img_normal.texture = texture_crystal;
			img_press.texture = texture_crystal;
			break;
		}
	}
}
