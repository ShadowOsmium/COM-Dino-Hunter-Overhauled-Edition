using System.Collections.Generic;

public class TUIMainLevelInfo
{
	public int id;

	public string title = string.Empty;

	public MainLevelType level_type;

	public List<TUISecondaryLevelInfo> secondary_level_info;

	public int secondary_level_id;

	public int[] level_goods_drop_list;

	public TUIMainLevelInfo(int m_id, string m_title, MainLevelType m_type, int m_secondary_level_id, int[] m_level_goods_drop_list = null)
	{
		id = m_id;
		title = m_title;
		if (secondary_level_info == null)
		{
			secondary_level_info = new List<TUISecondaryLevelInfo>();
		}
		secondary_level_id = m_secondary_level_id;
		level_type = m_type;
		level_goods_drop_list = m_level_goods_drop_list;
	}

	public void AddSecondaryLevelInfo(TUISecondaryLevelInfo m_info)
	{
		if (secondary_level_info == null)
		{
			secondary_level_info = new List<TUISecondaryLevelInfo>();
		}
		if (m_info != null)
		{
			secondary_level_info.Add(m_info);
		}
	}
}
