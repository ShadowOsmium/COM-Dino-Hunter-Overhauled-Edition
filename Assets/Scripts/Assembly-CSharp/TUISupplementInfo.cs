using System.Collections.Generic;

public class TUISupplementInfo
{
	public List<TUISupplementInfoGoods> goods_list;

	public int m_nGold;

	public int m_nCrystal;

	public int m_nTotalCrystalCost;

	public TUISupplementInfo()
	{
		goods_list = new List<TUISupplementInfoGoods>();
	}

	public void Add(int nID, int nCount)
	{
		goods_list.Add(new TUISupplementInfoGoods(nID, nCount));
	}

	public int GetTotalCost()
	{
		return 1;
	}
}
