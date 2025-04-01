using UnityEngine;

public class PopupPriceBtn : MonoBehaviour
{
	public TUILabel label_value_normal;

	public TUILabel label_value_press;

	public TUIMeshSprite img_unit_normal;

	public TUIMeshSprite img_unit_press;

	private string texture_gold = "title_jingbi";

	private string texture_crystal = "title_shuijing";

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetInfo(string m_text)
	{
		if (label_value_normal == null || label_value_press == null || img_unit_normal == null || img_unit_press == null)
		{
			Debug.Log("error!");
			return;
		}
		label_value_normal.Text = m_text;
		label_value_press.Text = m_text;
		img_unit_normal.texture = string.Empty;
		img_unit_press.texture = string.Empty;
	}

	public void SetInfo(int m_price, UnitType m_unit_type)
	{
		if (label_value_normal == null || label_value_press == null || img_unit_normal == null || img_unit_press == null)
		{
			Debug.Log("error!");
			return;
		}
		label_value_normal.Text = m_price.ToString();
		label_value_press.Text = m_price.ToString();
		switch (m_unit_type)
		{
		case UnitType.Gold:
			img_unit_normal.texture = texture_gold;
			img_unit_press.texture = texture_gold;
			break;
		case UnitType.Crystal:
			img_unit_normal.texture = texture_crystal;
			img_unit_press.texture = texture_crystal;
			break;
		}
	}

	public void ClearInfo()
	{
		if (label_value_normal == null || label_value_press == null || img_unit_normal == null || img_unit_press == null)
		{
			Debug.Log("error!");
		}
	}
}
