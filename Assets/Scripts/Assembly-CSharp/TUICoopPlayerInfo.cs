using System.Collections.Generic;
using UnityEngine;

public class TUICoopPlayerInfo
{
	public string id = string.Empty;

	public Texture GC_texture;

	public string name = string.Empty;

	public int title_id;

	public List<int> title_id_list;

	public int likes;

	public int hunter_level;

	public int hunter_exp;

	public int hunter_update_exp;

	public float progress;

	public string status = string.Empty;

	public int all_ranking;

	public int friends_ranking;

	public TUICoopRoleInfo role_info;

	public bool change_id;

	public bool change_GC_texture;

	public bool change_name;

	public bool change_title_id;

	public bool change_title_id_list;

	public bool change_likes;

	public bool change_hunter_level;

	public bool change_hunter_exp;

	public bool change_hunter_update_exp;

	public bool change_progress;

	public bool change_status;

	public bool change_all_ranking;

	public bool change_friends_ranking;

	public bool change_role_info;

	public TUICoopPlayerInfo()
	{
	}

	public TUICoopPlayerInfo(string m_id, Texture m_GC_texture, string m_name, int m_title_id, List<int> m_title_id_list, int m_likes, int m_hunter_level, int m_hunter_exp, int m_hunter_update_exp, float m_progress, string m_status, int m_all_ranking, int m_friends_ranking, TUICoopRoleInfo m_role_info)
	{
		id = m_id;
		GC_texture = m_GC_texture;
		name = m_name;
		title_id = m_title_id;
		title_id_list = m_title_id_list;
		likes = m_likes;
		hunter_level = m_hunter_level;
		hunter_exp = m_hunter_exp;
		hunter_update_exp = m_hunter_update_exp;
		progress = m_progress;
		status = m_status;
		all_ranking = m_all_ranking;
		friends_ranking = m_friends_ranking;
		role_info = m_role_info;
		change_id = true;
		change_GC_texture = true;
		change_name = true;
		change_title_id = true;
		change_title_id_list = true;
		change_likes = true;
		change_hunter_level = true;
		change_hunter_exp = true;
		change_hunter_update_exp = true;
		change_progress = true;
		change_status = true;
		change_all_ranking = true;
		change_friends_ranking = true;
		change_role_info = true;
	}

	public void SetID(string m_id)
	{
		id = m_id;
		change_id = true;
	}

	public void SetTexture(Texture m_GC_texture)
	{
		GC_texture = m_GC_texture;
		change_GC_texture = true;
	}

	public void SetName(string m_name)
	{
		name = m_name;
		change_name = true;
	}

	public void SetTitleID(int m_title_id)
	{
		title_id = m_title_id;
		change_title_id = true;
	}

	public void SetTitleIDList(List<int> m_title_id_list)
	{
		title_id_list = m_title_id_list;
		change_title_id_list = true;
	}

	public void SetLikes(int m_likes)
	{
		likes = m_likes;
		change_likes = true;
	}

	public void SetHunterLV(int m_hunter_level)
	{
		hunter_level = m_hunter_level;
		change_hunter_level = true;
	}

	public void SetHunterExp(int m_hunter_exp)
	{
		hunter_exp = m_hunter_exp;
		change_hunter_exp = true;
	}

	public void SetHunterUpdateExp(int m_hunter_update_exp)
	{
		hunter_update_exp = m_hunter_update_exp;
		change_hunter_update_exp = true;
	}

	public void SetProgress(float m_progress)
	{
		progress = m_progress;
		change_progress = true;
	}

	public void SetStatus(string m_status)
	{
		status = m_status;
		change_status = true;
	}

	public void SetAllRanking(int m_all_ranking)
	{
		all_ranking = m_all_ranking;
		change_all_ranking = true;
	}

	public void SetFriendsRanking(int m_friends_ranking)
	{
		friends_ranking = m_friends_ranking;
		change_friends_ranking = true;
	}

	public void SetRoleInfo(TUICoopRoleInfo m_role_info)
	{
		role_info = m_role_info;
		change_role_info = true;
	}
}
