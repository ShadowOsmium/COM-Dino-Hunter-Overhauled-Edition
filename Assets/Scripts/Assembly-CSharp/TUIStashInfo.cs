using System.Collections.Generic;

public class TUIStashInfo
{
	public int level;

	public TUIStashUpdateInfo[] stash_update_info;

	public List<TUIGoodsInfo> goods_info_list;

	public TUIStashInfo()
	{
	}

	public TUIStashInfo(int m_level, TUIStashUpdateInfo[] m_stash, List<TUIGoodsInfo> m_goods_info_list)
	{
		level = m_level;
		stash_update_info = m_stash;
		goods_info_list = m_goods_info_list;
	}

	public TUIStashUpdateInfo GetStashLevelInfo(int m_level)
	{
		if (stash_update_info != null)
		{
			for (int i = 0; i < stash_update_info.Length; i++)
			{
				if (stash_update_info[i].level == m_level)
				{
					return stash_update_info[i];
				}
			}
		}
		return null;
	}

	public TUIStashUpdateInfo GetStashLevelInfo()
	{
		if (stash_update_info != null)
		{
			for (int i = 0; i < stash_update_info.Length; i++)
			{
				if (stash_update_info[i].level == level)
				{
					return stash_update_info[i];
				}
			}
		}
		return null;
	}

	public bool IsStashLevelMax()
	{
		if (stash_update_info != null && stash_update_info.Length != 0 && stash_update_info[stash_update_info.Length - 1].level == level)
		{
			return true;
		}
		return false;
	}

	public int GetNowCapacity()
	{
		int num = 0;
		if (goods_info_list != null)
		{
			for (int i = 0; i < goods_info_list.Count; i++)
			{
				num += goods_info_list[i].count;
			}
		}
		return num;
	}
}
