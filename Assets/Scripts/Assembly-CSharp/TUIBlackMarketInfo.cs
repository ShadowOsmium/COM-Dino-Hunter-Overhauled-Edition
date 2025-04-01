using System.Collections.Generic;

public class TUIBlackMarketInfo
{
	public List<TUIBlackMarketItem> m_ltBlackMarketItem;

	public TUIBlackMarketInfo()
	{
		m_ltBlackMarketItem = new List<TUIBlackMarketItem>();
	}

	public void SetData(List<TUIBlackMarketItem> ltList)
	{
		m_ltBlackMarketItem.Clear();
		m_ltBlackMarketItem.AddRange(ltList);
	}

	public void Add(TUIBlackMarketItem blackmarketitem)
	{
		m_ltBlackMarketItem.Add(blackmarketitem);
	}
}
