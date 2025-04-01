public class TUIRecommendRoleInfo
{
	public int id;

	public bool have_buy;

	public bool have_equip;

	public bool required;

	public TUIRecommendRoleInfo(int m_id, bool m_have_buy, bool m_have_equip, bool m_required)
	{
		id = m_id;
		have_buy = m_have_buy;
		have_equip = m_have_equip;
		required = m_required;
	}
}
