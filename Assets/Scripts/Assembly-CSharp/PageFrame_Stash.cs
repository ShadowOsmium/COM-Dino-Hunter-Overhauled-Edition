using System.Collections.Generic;
using UnityEngine;

public class PageFrame_Stash : MonoBehaviour
{
	public GameObject prefab_page;

	public GameObject prefab_page_point;

	public GameObject page_points_parent;

	public string point_choose_texture;

	public string point_normal_texture;

	private List<TUIMeshSprite> img_points_list;

	private TUIPageFrameEx page_frame_ex;

	public TUIMeshSprite img_2d_bg;

	[SerializeField]
	private int current_page_index;

	private void Awake()
	{
		current_page_index = 0;
		img_points_list = new List<TUIMeshSprite>();
		page_frame_ex = base.gameObject.GetComponent<TUIPageFrameEx>();
	}

	private void Start()
	{
		Transform[] componentsInChildren = base.gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].gameObject.layer = 9;
			TUIDrawSprite component = componentsInChildren[i].GetComponent<TUIDrawSprite>();
			if (component != null)
			{
				component.clippingType = TUIDrawSprite.Clipping.None;
			}
		}
		if (img_2d_bg != null)
		{
			img_2d_bg.gameObject.layer = 9;
		}
	}

	private void Update()
	{
		CheckChangePoint();
	}

	public void AddPage(List<TUIGoodsInfo> goods_info_list, GameObject go_invoke)
	{
		if (goods_info_list == null)
		{
			Debug.Log("no goods_info_list!");
			return;
		}
		int num = Mathf.CeilToInt((float)goods_info_list.Count / 24f);
		if (num < 1)
		{
			num = 1;
		}
		List<Btn_Select_Stash> list = new List<Btn_Select_Stash>();
		for (int i = 0; i < num; i++)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(prefab_page);
			gameObject.transform.parent = base.gameObject.transform;
			gameObject.transform.localPosition = new Vector3(i * 430, 0f, 0f);
			gameObject.GetComponent<Page_Stash>().SetIndex(i + 1);
			gameObject.GetComponent<Page_Stash>().SetInvoke(go_invoke);
			page_frame_ex.AddPage(gameObject.GetComponent<TUIPageEx>());
			list.AddRange(gameObject.GetComponent<Page_Stash>().GetBtnSelectList());
		}
		for (int j = 0; j < num; j++)
		{
			GameObject gameObject2 = (GameObject)Object.Instantiate(prefab_page_point);
			gameObject2.transform.parent = page_points_parent.transform;
			gameObject2.transform.localPosition = new Vector3(j * 12 - 12 * num / 2 + 6, 0f, 0f);
			img_points_list.Add(gameObject2.GetComponent<TUIMeshSprite>());
		}
		for (int k = 0; k < goods_info_list.Count; k++)
		{
			list[k].SetGoodsInfo(goods_info_list[k]);
		}
		for (int l = goods_info_list.Count; l < list.Count; l++)
		{
			list[l].gameObject.SetActiveRecursively(false);
		}
		for (int m = 0; m < list.Count; m++)
		{
			list[m].SetIndex(m);
		}
	}

	public void CheckChangePoint()
	{
		TUIPageEx currentPage = page_frame_ex.CurrentPage;
		if (currentPage == null)
		{
			Debug.Log("no m_page_ex!");
			return;
		}
		int index = currentPage.gameObject.GetComponent<Page_Stash>().GetIndex();
		if (current_page_index == index)
		{
			return;
		}
		current_page_index = index;
		for (int i = 0; i < img_points_list.Count; i++)
		{
			if (i == current_page_index - 1)
			{
				img_points_list[i].texture = point_choose_texture;
			}
			else
			{
				img_points_list[i].texture = point_normal_texture;
			}
		}
	}
}
