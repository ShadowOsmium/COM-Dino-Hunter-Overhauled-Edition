using System.Collections.Generic;

public class TUICoopRoleInfo
{
	public int role_id;

	public int role_level;

	public int role_rating;

	public List<int> weapon_list;

	public int m_nModelID = -1;

	public int m_nAvatarHead = -1;

	public int m_nAvatarUpper = -1;

	public int m_nAvatarLower = -1;

	public int m_nAvatarHeadup = -1;

	public int m_nAvatarNeck = -1;

	public int m_nAvatarBracelet = -1;

	public TUICoopRoleInfo()
	{
	}

	public TUICoopRoleInfo(int nRoleID, int nRoleLevel, int nRoleRating, List<int> ltWeapon)
	{
		role_id = nRoleID;
		role_level = nRoleLevel;
		role_rating = nRoleRating;
		weapon_list = ltWeapon;
	}

	public void Init(int nRoleID, int nRoleLevel, int nRoleRating, List<int> ltWeapon, int nAvatarModel, int nAvatarHead, int nAvatarUpper, int nAvatarLower, int nAvatarHeadup, int nAvatarNeck, int nAvatarBracelet)
	{
		role_id = nRoleID;
		role_level = nRoleLevel;
		role_rating = nRoleRating;
		weapon_list = ltWeapon;
		m_nModelID = nAvatarModel;
		m_nAvatarHead = nAvatarHead;
		m_nAvatarUpper = nAvatarUpper;
		m_nAvatarLower = nAvatarLower;
		m_nAvatarHeadup = nAvatarHeadup;
		m_nAvatarNeck = nAvatarNeck;
		m_nAvatarBracelet = nAvatarBracelet;
	}
}
