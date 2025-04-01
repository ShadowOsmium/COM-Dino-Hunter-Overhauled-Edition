using UnityEngine;

public class ImgPrice : MonoBehaviour
{
	public TUILabel label_value;

	public TUIMeshSprite img_unit;

	private string gold_texture = "title_jingbi";

	private string crystal_texture = "title_shuijing";

	private void Start()
	{
		SetCenterPos();
	}

	private void Update()
	{
	}

	private void SetCenterPos()
	{
		if (label_value == null || img_unit == null)
		{
			Debug.Log("error!");
			return;
		}
		float x = label_value.CalculateBounds(label_value.Text).size.x;
		x *= label_value.transform.localScale.x;
		TUITextureInfo texInfo = img_unit.texInfo;
		float num = 0f;
		if (texInfo != null)
		{
			num = img_unit.texInfo.rect.width / 2f;
			num *= img_unit.transform.localScale.x;
		}
		float num2 = x + num;
		float x2 = -0.5f * num2 + 0.5f * x;
		label_value.transform.localPosition = new Vector3(x2, label_value.transform.localPosition.y, label_value.transform.localPosition.z);
		float x3 = -0.5f * num2 + x + 0.5f * num;
		img_unit.transform.localPosition = new Vector3(x3, img_unit.transform.localPosition.y, img_unit.transform.localPosition.z);
	}

	public void SetInfo(int m_value, UnitType m_type)
	{
		if (label_value == null || img_unit == null)
		{
			Debug.Log("error!");
			return;
		}
		label_value.Text = m_value.ToString();
		switch (m_type)
		{
		case UnitType.Gold:
			img_unit.texture = gold_texture;
			break;
		case UnitType.Crystal:
			img_unit.texture = crystal_texture;
			break;
		}
		SetCenterPos();
	}

	public void SetInfo(TUIPriceInfo m_info)
	{
		if (label_value == null || img_unit == null || m_info == null)
		{
			Debug.Log("error!");
			return;
		}
		int price = m_info.price;
		UnitType unit_type = m_info.unit_type;
		label_value.Text = price.ToString();
		switch (unit_type)
		{
		case UnitType.Gold:
			img_unit.texture = gold_texture;
			break;
		case UnitType.Crystal:
			img_unit.texture = crystal_texture;
			break;
		}
		SetCenterPos();
	}

	public void SetGrayStyle(bool m_bool)
	{
		if (label_value == null || img_unit == null)
		{
			Debug.Log("error!");
		}
		else if (m_bool)
		{
			label_value.color = new Color32(166, 166, 166, byte.MaxValue);
			label_value.colorBK = new Color32(166, 166, 166, byte.MaxValue);
			img_unit.GrayStyle = true;
		}
		else
		{
			label_value.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			label_value.colorBK = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			img_unit.GrayStyle = false;
		}
	}
}
