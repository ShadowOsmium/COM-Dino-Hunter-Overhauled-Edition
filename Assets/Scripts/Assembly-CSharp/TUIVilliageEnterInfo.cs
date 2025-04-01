using System.Collections.Generic;

public class TUIVilliageEnterInfo
{
	public string finished_text = string.Empty;

	public List<TUIUnlockInfo> unlock_list;

	public NewMarkType forge_sign;

	public NewMarkType skill_sign;

	public NewMarkType tavern_sign;

	public NewMarkType stash_sign;

	public NewMarkType equip_sign;

	public NewMarkType blackmarket_sign;

	public void AddUnlockItem(TUIUnlockInfo m_item)
	{
		if (unlock_list == null)
		{
			unlock_list = new List<TUIUnlockInfo>();
		}
		unlock_list.Add(m_item);
	}
}
