using UnityEngine;

public class DailyLoginBonusItem : MonoBehaviour
{
	public ImgPrice img_price;

	public TUILabel label_title;

	public TUIMeshSprite img_texture;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetInfo(TUIPriceInfo m_price_info)
	{
		if (img_price == null)
		{
			Debug.Log("error!");
			return;
		}
		if (img_texture != null)
		{
			if (m_price_info.unit_type == UnitType.Crystal)
			{
				img_texture.texture = "sj_2";
			}
			if (m_price_info.unit_type == UnitType.Gold)
			{
				img_texture.texture = "jb_2";
			}
		}
		img_price.SetInfo(m_price_info);
	}

	public void SetGrayStyle(bool m_bool)
	{
		if (img_price == null || label_title == null)
		{
			Debug.Log("error!");
			return;
		}
		if (m_bool)
		{
			label_title.color = new Color32(166, 166, 166, byte.MaxValue);
			label_title.colorBK = new Color32(166, 166, 166, byte.MaxValue);
		}
		else
		{
			label_title.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			label_title.colorBK = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}
		img_price.SetGrayStyle(m_bool);
		img_texture.GrayStyle = m_bool;
	}
}
