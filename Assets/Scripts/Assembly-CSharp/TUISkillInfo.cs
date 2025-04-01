using System.Collections.Generic;

public class TUISkillInfo
{
	public int id;

	public string name = string.Empty;

	public int level;

	public bool unlock;

	public bool active_skill;

	public TUIPriceInfo unlock_price;

	public string skill_introduce_unlock = string.Empty;

	public bool duoren_skill;

	public int duoren_skill_level;

	public Dictionary<int, TUIPriceInfo> level_price;

	public Dictionary<int, string> level_introduce;

	public Dictionary<int, string> level_introduce_ex;

	public float discount = 1f;

	public TUISkillInfo(int m_id, string m_name, int m_level, bool m_unlock, TUIPriceInfo m_unlock_price, Dictionary<int, TUIPriceInfo> m_level_price, Dictionary<int, string> m_level_introduce, Dictionary<int, string> m_level_introduce_ex, string m_skill_introduce_unlock, float m_discount = 1f)
	{
		id = m_id;
		name = m_name;
		level = m_level;
		unlock = m_unlock;
		unlock_price = m_unlock_price;
		level_price = m_level_price;
		level_introduce = m_level_introduce;
		level_introduce_ex = m_level_introduce_ex;
		skill_introduce_unlock = m_skill_introduce_unlock;
		discount = m_discount;
	}

	public TUISkillInfo(int m_id, string m_name, int m_level, bool m_active_skill, Dictionary<int, TUIPriceInfo> m_level_price, Dictionary<int, string> m_level_introduce, Dictionary<int, string> m_level_introduce_ex, bool m_duoren_skill = false, int m_duoren_skill_level = 0, float m_discount = 1f)
	{
		id = m_id;
		name = m_name;
		level = m_level;
		active_skill = m_active_skill;
		level_price = m_level_price;
		level_introduce = m_level_introduce;
		level_introduce_ex = m_level_introduce_ex;
		duoren_skill = m_duoren_skill;
		duoren_skill_level = m_duoren_skill_level;
		discount = m_discount;
	}
}
