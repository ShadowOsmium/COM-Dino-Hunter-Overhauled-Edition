using System.Collections.Generic;

public class TUIAllSaleInfo
{
	public List<TUISingleSaleInfo> all_sale_info;

	public void AddItem(TUISingleSaleInfo m_info)
	{
		if (all_sale_info == null)
		{
			all_sale_info = new List<TUISingleSaleInfo>();
		}
		all_sale_info.Add(m_info);
	}
}
