public class TUIStashUpdateInfo
{
	public int level;

	public TUIPriceInfo price_info;

	public int max_capacity;

	public string introduce = string.Empty;

	public TUIStashUpdateInfo(int m_level, TUIPriceInfo m_price, int m_max_capacity, string m_introduce)
	{
		level = m_level;
		price_info = m_price;
		max_capacity = m_max_capacity;
		introduce = m_introduce;
	}
}
