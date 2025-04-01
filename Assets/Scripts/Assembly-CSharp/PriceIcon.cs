using UnityEngine;

public class PriceIcon : MonoBehaviour
{
	public TUILabel label_icon;

	public TUIMeshSprite img_unit;

	private string gold_texture = "title_jingbi";

	private string crystal_texture = "title_shuijing";

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetInfo(TUIPriceInfo m_info)
	{
		if (m_info == null)
		{
			Debug.Log("error!");
			return;
		}
		int price = m_info.price;
		UnitType unit_type = m_info.unit_type;
		if (label_icon != null)
		{
			label_icon.Text = m_info.price + "\nfree";
		}
		if (img_unit != null)
		{
			if (unit_type == UnitType.Gold)
			{
				img_unit.texture = gold_texture;
			}
			if (unit_type == UnitType.Crystal)
			{
				img_unit.texture = crystal_texture;
			}
		}
	}

	public void SetInfo(int m_value, UnitType m_unit)
	{
		if (label_icon != null)
		{
			label_icon.Text = m_value + "\nfree";
		}
		if (img_unit != null)
		{
			if (m_unit == UnitType.Gold)
			{
				img_unit.texture = gold_texture;
			}
			if (m_unit == UnitType.Crystal)
			{
				img_unit.texture = crystal_texture;
			}
		}
	}

	public void Show(bool m_show)
	{
		base.gameObject.SetActiveRecursively(m_show);
	}
}
