using System.Collections.Generic;
using UnityEngine;

public class Popup_Sale : MonoBehaviour
{
	public GameObject go_popup;

	public GameObject prefab_item_group;

	public Popup_Sale_Item prefab_item;

	public TUIScrollList scroll_list;

	public TUIRect rect_show;

	public Popup_Sale_Blink sale_blink;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Show()
	{
		base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		if (go_popup != null && go_popup.GetComponent<Animation>() != null)
		{
			go_popup.GetComponent<Animation>().Play();
		}
		if (sale_blink != null)
		{
			sale_blink.gameObject.SetActiveRecursively(false);
		}
	}

	public void Hide()
	{
		base.transform.localPosition = new Vector3(0f, -1000f, base.transform.localPosition.z);
		if (sale_blink != null)
		{
			sale_blink.gameObject.SetActiveRecursively(false);
		}
	}

	public void DoCreate(TUIAllSaleInfo m_all_sale_info, GameObject m_invoke_go)
	{
		if (m_all_sale_info == null)
		{
			Debug.Log("no info");
			return;
		}
		List<TUISingleSaleInfo> all_sale_info = m_all_sale_info.all_sale_info;
		if (all_sale_info == null || prefab_item_group == null || go_popup == null || prefab_item == null || scroll_list == null)
		{
			Debug.Log("warning!");
			return;
		}
		GameObject gameObject = null;
		for (int i = 0; i < all_sale_info.Count; i++)
		{
			if ((i % 2 == 0) ? true : false)
			{
				gameObject = (GameObject)Object.Instantiate(prefab_item_group);
				if (gameObject == null)
				{
					Debug.Log("error!");
					break;
				}
				gameObject.transform.parent = scroll_list.transform;
				gameObject.transform.localPosition = new Vector3(0f, 0f, -1f);
				scroll_list.Add(gameObject.GetComponent<TUIControl>());
			}
			Popup_Sale_Item popup_Sale_Item = (Popup_Sale_Item)Object.Instantiate(prefab_item);
			if (popup_Sale_Item == null)
			{
				Debug.Log("error!");
				break;
			}
			popup_Sale_Item.transform.parent = gameObject.transform;
			popup_Sale_Item.transform.localPosition = new Vector3((i % 2 != 0) ? 89 : (-86), 0f, -1f);
			popup_Sale_Item.DoCreate(all_sale_info[i], rect_show, m_invoke_go);
		}
	}
}
