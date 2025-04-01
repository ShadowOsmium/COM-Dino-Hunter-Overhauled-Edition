using UnityEngine;

public class PopupWeaponUpdateSaleBuy : MonoBehaviour
{
	public TUILabel label_value_normal;

	public TUILabel label_value_press;

	public TUIMeshSprite img_normal;

	public TUIMeshSprite img_press;

	public TUILabel label_old_value_normal;

	public TUILabel label_old_value_press;

	public TUIMeshSprite img_old_normal;

	public TUIMeshSprite img_old_press;

	private string texture_gold = "title_jingbi";

	private string texture_crystal = "title_shuijing";

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetBtnText(int m_price, UnitType m_unit_type, int m_now_price, UnitType m_now_unit_type)
	{
		if (label_value_normal != null && label_value_press != null)
		{
			label_value_normal.Text = m_now_price.ToString();
			label_value_press.Text = m_now_price.ToString();
		}
		if (img_normal != null && img_press != null)
		{
			switch (m_now_unit_type)
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
		if (label_old_value_normal != null && label_old_value_press != null)
		{
			label_old_value_normal.Text = m_price.ToString();
			label_old_value_press.Text = m_price.ToString();
		}
		if (img_old_normal != null && img_old_press != null)
		{
			switch (m_unit_type)
			{
			case UnitType.Gold:
				img_old_normal.texture = texture_gold;
				img_old_press.texture = texture_gold;
				break;
			case UnitType.Crystal:
				img_old_normal.texture = texture_crystal;
				img_old_press.texture = texture_crystal;
				break;
			}
		}
	}
}
