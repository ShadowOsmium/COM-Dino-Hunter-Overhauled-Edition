using System.Collections.Generic;
using UnityEngine;

public class IAPItems : MonoBehaviour
{
	public TUIScrollList scroll_list;

	public IAPItem prefab_iap_item;

	private List<TUISingleIAPInfo> iap_info_list;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void DoCreate(TUIIAPInfo m_iap_info, GameObject m_invoke_go)
	{
		if (m_iap_info == null)
		{
			Debug.Log("no info!");
		}
		iap_info_list = m_iap_info.iap_info_list;
		if (iap_info_list == null || prefab_iap_item == null)
		{
			Debug.Log("no info!");
			return;
		}
		for (int i = 0; i < iap_info_list.Count; i++)
		{
			IAPItem iAPItem = (IAPItem)Object.Instantiate(prefab_iap_item);
			if (iAPItem != null)
			{
				TUISingleIAPInfo info = iap_info_list[i];
				TUIControl component = iAPItem.GetComponent<TUIControl>();
				iAPItem.DoCreate(info, m_invoke_go);
				iAPItem.transform.parent = base.transform;
				iAPItem.transform.localPosition = new Vector3(-150 + i * 153, 0f, 0f);
				if (scroll_list != null && component != null)
				{
					scroll_list.Add(component);
				}
			}
		}
	}
}
