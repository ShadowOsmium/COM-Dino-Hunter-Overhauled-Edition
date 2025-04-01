public class TUIRecommendWeaponInfo
{
	public int id;

	public int level;

	public int level_need;

	public bool have_equip;

	public bool required;

	public TUIRecommendWeaponInfo(int m_id, int m_level, int m_level_need, bool m_have_equip, bool m_required)
	{
		id = m_id;
		level = m_level;
		level_need = m_level_need;
		have_equip = m_have_equip;
		required = m_required;
	}
}
