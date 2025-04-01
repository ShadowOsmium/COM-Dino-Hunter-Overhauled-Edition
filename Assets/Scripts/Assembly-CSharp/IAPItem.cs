using UnityEngine;

public class IAPItem : MonoBehaviour
{
	private int id;

	public ImgPrice money_get;

	public TUIMeshSprite img_texture;

	public TUIButtonClick btn_click;

	public TUILabel label_button01;

	public TUILabel label_button02;

	public PriceIcon free_count_icon;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void DoCreate(TUISingleIAPInfo m_info, GameObject m_invoke_go)
	{
		if (m_info == null)
		{
			Debug.Log("error!");
		}
		id = m_info.id;
		float money_cost = m_info.money_cost;
		TUIPriceInfo tUIPriceInfo = m_info.money_get;
		int free_count = m_info.free_count;
		string money_texture = m_info.money_texture;
		if (label_button01 != null)
		{
			label_button01.Text = "$" + money_cost;
		}
		if (label_button02 != null)
		{
			label_button02.Text = "$" + money_cost;
		}
		if (money_get != null)
		{
			money_get.SetInfo(tUIPriceInfo);
		}
		if (free_count > 0)
		{
			if (free_count_icon != null)
			{
				free_count_icon.SetInfo(free_count, (tUIPriceInfo == null) ? UnitType.Crystal : tUIPriceInfo.unit_type);
				free_count_icon.Show(true);
			}
		}
		else if (free_count_icon != null)
		{
			free_count_icon.Show(false);
		}
		if (img_texture != null)
		{
			img_texture.texture = money_texture;
		}
		btn_click.invokeObject = m_invoke_go;
		money_get.SetInfo(tUIPriceInfo);
	}

	public int GetID()
	{
		return id;
	}
}
