using System.Collections.Generic;

public class TUISecondaryLevelInfo
{
	public int id;

	public string introduce01 = string.Empty;

	public List<TUIGoodsInfo> goods_drop_list;

	public SecondaryLevelType level_type;

	public LevelPassState pass_state;

	public TUISecondaryLevelInfo(int m_id, string m_introduce01, List<TUIGoodsInfo> m_goods_drop_list, SecondaryLevelType m_level_type, LevelPassState m_pass_state)
	{
		id = m_id;
		introduce01 = m_introduce01;
		goods_drop_list = m_goods_drop_list;
		level_type = m_level_type;
		pass_state = m_pass_state;
	}
}
