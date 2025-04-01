public class TUISingleSaleInfo
{
	public OnSaleType sale_type;

	public int id;

	public string name = string.Empty;

	public string icon = string.Empty;

	public TUIPriceInfo old_price_info;

	public float discount;

	public TUISingleSaleInfo(OnSaleType m_sale_type, int m_id, string m_name, TUIPriceInfo m_old_price_info, float m_discount)
	{
		sale_type = m_sale_type;
		id = m_id;
		name = m_name;
		old_price_info = m_old_price_info;
		discount = m_discount;
	}
}
