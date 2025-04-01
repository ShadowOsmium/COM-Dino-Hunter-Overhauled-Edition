using UnityEngine;

public class Popup_Capacity_Stash : MonoBehaviour
{
	public TUILabel label_introduce;

	public PopupAddBtnBuy btn_buy;

	private string str_introduce = string.Empty;

	private TUIPriceInfo update_price;

	private int now_capacity;

	private int max_capacity;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetIntroduce(string introduce)
	{
		str_introduce = introduce;
		label_introduce.Text = str_introduce;
	}

	public void SetMaxCapacity(int max)
	{
		max_capacity = max;
	}

	public void SetNowCapacity(int now)
	{
		now_capacity = now;
	}

	public int GetMaxCapacity()
	{
		return max_capacity;
	}

	public int GetNowCapacity()
	{
		return now_capacity;
	}

	public TUIPriceInfo GetAddCapacityPrice()
	{
		return update_price;
	}

	public void SetInfo(int m_now, int m_max, TUIPriceInfo m_price, string m_introduce)
	{
		now_capacity = m_now;
		max_capacity = m_max;
		update_price = m_price;
		str_introduce = m_introduce;
		label_introduce.Text = str_introduce;
		btn_buy.SetBtnText(m_price.price, m_price.unit_type);
	}
}
