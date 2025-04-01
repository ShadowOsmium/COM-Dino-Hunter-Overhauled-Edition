using System.Collections.Generic;
using UnityEngine;

public class Page_Stash : MonoBehaviour
{
	public List<Btn_Select_Stash> btn_select_list;

	[SerializeField]
	private int index;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetIndex(int id)
	{
		index = id;
	}

	public int GetIndex()
	{
		return index;
	}

	public List<Btn_Select_Stash> GetBtnSelectList()
	{
		return btn_select_list;
	}

	public void SetInvoke(GameObject go_invoke)
	{
		for (int i = 0; i < btn_select_list.Count; i++)
		{
			TUIButtonSelect component = btn_select_list[i].gameObject.GetComponent<TUIButtonSelect>();
			component.invokeOnEvent = true;
			component.invokeObject = go_invoke;
			component.componentName = "Scene_Stash";
			component.invokeFunctionName = "TUIEvent_ChooseGoods";
		}
	}
}
