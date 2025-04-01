using System.Collections.Generic;
using UnityEngine;

public class PopupTitleList : MonoBehaviour
{
	public TUIScrollListEx scroll_list;

	public PopupTitleListItem prefab_item;

	public TUIGrid grid_items;

	public TUIMeshSprite img_bg;

	public TUIMeshSprite img_bg2;

	private Dictionary<int, string> title_info_list;

	private List<int> title_id_list;

	private void Start()
	{
		if (img_bg != null)
		{
			img_bg.gameObject.layer = 9;
		}
		if (img_bg2 != null)
		{
			img_bg2.gameObject.layer = 9;
		}
	}

	private void Update()
	{
	}

	public void SetInfo(Dictionary<int, string> m_title_info_list, List<int> m_title_id_list, int m_title_id = 0)
	{
		if (scroll_list == null || prefab_item == null || grid_items == null)
		{
			Debug.Log("error!");
			return;
		}
		Debug.Log("m_title_id:" + m_title_id);
		PopupTitleListItem popupTitleListItem = null;
		if (title_info_list != null || title_id_list != null || m_title_info_list == null || m_title_id_list == null)
		{
			return;
		}
		title_info_list = m_title_info_list;
		title_id_list = m_title_id_list;
		for (int i = 0; i < m_title_id_list.Count; i++)
		{
			if (m_title_info_list.ContainsKey(m_title_id_list[i]))
			{
				PopupTitleListItem popupTitleListItem2 = (PopupTitleListItem)Object.Instantiate(prefab_item);
				popupTitleListItem2.transform.parent = grid_items.transform;
				popupTitleListItem2.transform.localPosition = new Vector3(0f, 0f, popupTitleListItem2.transform.localPosition.z);
				popupTitleListItem2.SetInfo(m_title_id_list[i], m_title_info_list);
				scroll_list.Add(popupTitleListItem2.gameObject);
				if (m_title_id_list[i] == m_title_id)
				{
					popupTitleListItem = popupTitleListItem2;
				}
			}
		}
		grid_items.Reposition();
		if (popupTitleListItem != null)
		{
			scroll_list.SetNowItem(popupTitleListItem.gameObject);
		}
	}

	public int GetTitleID()
	{
		if (scroll_list != null)
		{
			GameObject nowItem = scroll_list.GetNowItem();
			if (nowItem != null)
			{
				PopupTitleListItem component = nowItem.GetComponent<PopupTitleListItem>();
				if (component != null)
				{
					return component.GetTitleID();
				}
			}
		}
		return 0;
	}
}
