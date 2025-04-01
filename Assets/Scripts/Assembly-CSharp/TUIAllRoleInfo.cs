using System.Collections.Generic;

public class TUIAllRoleInfo
{
	public TUIRoleInfo[] role_list;

	public Dictionary<int, NewMarkType> new_mark_list;

	public int role_link_id;

	public bool open_link;

	public TUIAllRoleInfo()
	{
	}

	public TUIAllRoleInfo(TUIRoleInfo[] m_role_list)
	{
		role_list = m_role_list;
	}

	public void AddNewMark(int m_id, NewMarkType m_type)
	{
		if (new_mark_list == null)
		{
			new_mark_list = new Dictionary<int, NewMarkType>();
		}
		new_mark_list[m_id] = m_type;
	}

	public void SetLinkInfo(int m_id)
	{
		open_link = true;
		role_link_id = m_id;
	}
}
