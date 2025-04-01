using System.Collections.Generic;
using UnityEngine;

public class ScrollList_BlackMarket : MonoBehaviour
{
	public TUIGrid grid_scrolllist;

	public BlackMarketItem prefab_item;

	private TUIScrollListCircle scrolllist;

	private void Awake()
	{
		scrolllist = GetComponent<TUIScrollListCircle>();
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetItems(List<TUIBlackMarketItem> ltBlackMarketItem)
	{
		if (prefab_item == null || scrolllist == null || grid_scrolllist == null || ltBlackMarketItem == null || ltBlackMarketItem.Count < 1)
		{
			return;
		}
		foreach (TUIBlackMarketItem item in ltBlackMarketItem)
		{
			BlackMarketItem blackMarketItem = Object.Instantiate(prefab_item) as BlackMarketItem;
			if (!(blackMarketItem == null))
			{
				blackMarketItem.transform.parent = grid_scrolllist.transform;
				blackMarketItem.SetInfo(item);
				scrolllist.Add(blackMarketItem.gameObject);
			}
		}
		scrolllist.ResetGrid();
		scrolllist.SetItemList();
	}

	public void SetItem(TUIBlackMarketItem blackmarketitem)
	{
		if (blackmarketitem == null)
		{
			return;
		}
		List<GameObject> itemsList = scrolllist.GetItemsList();
		if (itemsList == null)
		{
			return;
		}
		foreach (GameObject item in itemsList)
		{
			BlackMarketItem component = item.GetComponent<BlackMarketItem>();
			if (!(component == null) && component.GetInfo() == blackmarketitem)
			{
				component.SetInfo(blackmarketitem);
			}
		}
	}

	public BlackMarketItem GetItem(int m_id)
	{
		return null;
	}
}
