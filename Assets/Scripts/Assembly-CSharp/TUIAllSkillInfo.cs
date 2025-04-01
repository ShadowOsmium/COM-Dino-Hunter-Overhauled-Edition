public class TUIAllSkillInfo
{
	public TUISkillListInfo[] all_role_skill_list;

	public int role_link_id;

	public int role_skill_link_id;

	public bool open_link;

	public TUIAllSkillInfo(TUISkillListInfo[] m_all_role_skill_list)
	{
		all_role_skill_list = m_all_role_skill_list;
	}

	public void SetLinkInfo(int m_role_link_id, int m_role_skill_link_id)
	{
		role_link_id = m_role_link_id;
		role_skill_link_id = m_role_skill_link_id;
		open_link = true;
	}
}
