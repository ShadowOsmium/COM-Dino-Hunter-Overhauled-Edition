using System.Collections.Generic;

public class TUICoopTitleListInfo
{
	public Dictionary<int, string> title_list;

	public void AddTitle(int m_id, string m_name)
	{
		if (title_list == null)
		{
			title_list = new Dictionary<int, string>();
		}
		title_list[m_id] = m_name;
	}
}
