using System.Collections.Generic;

public class CBlackMarketInfo
{
	public string m_sDate;

	public int m_nTime;

	public List<CBlackItemInfo> m_ltItem;

	public CBlackMarketInfo()
	{
		m_ltItem = new List<CBlackItemInfo>();
	}
}
