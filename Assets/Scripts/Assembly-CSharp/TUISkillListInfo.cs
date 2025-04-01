using System.Collections.Generic;

public class TUISkillListInfo
{
	public int id;

	public TUISkillInfo[] skill_list_info;

	public Dictionary<int, NewMarkType> new_mark_list;

	public TUISkillListInfo()
	{
	}

	public TUISkillListInfo(int m_id, TUISkillInfo[] m_skill_list_info)
	{
		id = m_id;
		skill_list_info = m_skill_list_info;
	}

	public void AddNewMark(int m_id, NewMarkType m_type)
	{
		if (new_mark_list == null)
		{
			new_mark_list = new Dictionary<int, NewMarkType>();
		}
		new_mark_list[m_id] = m_type;
	}
}
