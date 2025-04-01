public class TUIGoodsInfo
{
	public int id;

	public GoodsQualityType quality;

	public string name = string.Empty;

	public int count;

	public TUIPriceInfo price_info;

	public TUIGoodsInfo(int m_id, GoodsQualityType m_quality, string m_name, int m_count, TUIPriceInfo m_price_info)
	{
		id = m_id;
		quality = m_quality;
		name = m_name;
		price_info = m_price_info;
		count = m_count;
	}

	public TUIGoodsInfo(int m_id, GoodsQualityType m_quality = GoodsQualityType.Quality01, string m_name = "")
	{
		id = m_id;
		quality = m_quality;
		name = m_name;
	}

	public void SetCount(int m_count)
	{
		count = m_count;
	}
}
