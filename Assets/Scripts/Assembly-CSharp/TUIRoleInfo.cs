using System.Collections.Generic;

public class TUIRoleInfo
{
	public int id;

	public string name = string.Empty;

	public string introduce = string.Empty;

	public bool unlock;

	public TUIPriceInfo unlock_price;

	public string introduce_unlock = string.Empty;

	public bool do_buy;

	public TUIPriceInfo do_buy_price;

	public List<TUIPopupInfo> active_skill_list;

	public float discount = 1f;

	public bool is_active_buy;

	public bool can_active_buy;

	public int m_nModelID = -1;

	public int m_nAvatarHead = -1;

	public int m_nAvatarUpper = -1;

	public int m_nAvatarLower = -1;

	public int m_nAvatarHeadup = -1;

	public int m_nAvatarNeck = -1;

	public int m_nAvatarBracelet = -1;

	public TUIRoleInfo()
	{
	}

	public TUIRoleInfo(int m_id, string m_name, string m_introduce, bool m_unlock, TUIPriceInfo m_unlock_price, bool m_do_buy, TUIPriceInfo m_do_buy_price, string m_introduce_unlock, List<TUIPopupInfo> m_active_skill_list = null, float m_discount = 1f)
	{
		id = m_id;
		name = m_name;
		introduce = m_introduce;
		unlock = m_unlock;
		unlock_price = m_unlock_price;
		do_buy = m_do_buy;
		do_buy_price = m_do_buy_price;
		introduce_unlock = m_introduce_unlock;
		active_skill_list = m_active_skill_list;
		discount = m_discount;
	}

	public TUIRoleInfo(int m_id, string m_name, string m_introduce, bool m_do_buy, bool m_is_active_buy, bool m_can_active_buy, List<TUIPopupInfo> m_active_skill_list)
	{
		id = m_id;
		name = m_name;
		introduce = m_introduce;
		do_buy = m_do_buy;
		unlock = true;
		is_active_buy = m_is_active_buy;
		can_active_buy = m_can_active_buy;
		active_skill_list = m_active_skill_list;
	}
}
