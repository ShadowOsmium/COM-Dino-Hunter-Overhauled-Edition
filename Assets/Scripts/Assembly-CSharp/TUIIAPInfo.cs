using System.Collections.Generic;

public class TUIIAPInfo
{
	public List<TUISingleIAPInfo> iap_info_list;

	public void AddIAPItem(TUISingleIAPInfo m_info)
	{
		if (iap_info_list == null)
		{
			iap_info_list = new List<TUISingleIAPInfo>();
		}
		iap_info_list.Add(m_info);
	}
}
