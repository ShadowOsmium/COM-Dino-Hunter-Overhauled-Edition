using System.Collections.Generic;
using UnityEngine;

public class PopupLevel_Frame03 : MonoBehaviour
{
	public GoodsNeedItemImg goods01;

	public GoodsNeedItemImg goods02;

	public GoodsNeedItemImg goods03;

	public PopupLevel_Recommend recommend;

	private Vector3 goods01_position = Vector3.zero;

	private Vector3 goods02_position = Vector3.zero;

	private Vector3 goods03_position = Vector3.zero;

	private void Awake()
	{
		if (goods01 == null || goods02 == null || goods03 == null)
		{
			Debug.Log("error!");
		}
		goods01_position = goods01.transform.localPosition;
		goods02_position = goods02.transform.localPosition;
		goods03_position = goods03.transform.localPosition;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetGoodsInfo(List<TUIGoodsInfo> m_goods_drop_list)
	{
		if (m_goods_drop_list == null || m_goods_drop_list.Count == 0)
		{
			goods01.gameObject.SetActiveRecursively(false);
			goods02.gameObject.SetActiveRecursively(false);
			goods03.gameObject.SetActiveRecursively(false);
			return;
		}
		switch (m_goods_drop_list.Count)
		{
		case 1:
			goods01.SetInfo(m_goods_drop_list[0].id, m_goods_drop_list[0].quality, m_goods_drop_list[0].name);
			goods01.transform.localPosition = goods02_position;
			goods01.gameObject.SetActiveRecursively(true);
			goods02.gameObject.SetActiveRecursively(false);
			goods03.gameObject.SetActiveRecursively(false);
			break;
		case 2:
			goods01.SetInfo(m_goods_drop_list[0].id, m_goods_drop_list[0].quality, m_goods_drop_list[0].name);
			goods02.SetInfo(m_goods_drop_list[1].id, m_goods_drop_list[1].quality, m_goods_drop_list[1].name);
			goods01.transform.localPosition = goods01_position + new Vector3(20f, 0f, 0f);
			goods02.transform.localPosition = goods02_position + new Vector3(20f, 0f, 0f);
			goods01.gameObject.SetActiveRecursively(true);
			goods02.gameObject.SetActiveRecursively(true);
			goods03.gameObject.SetActiveRecursively(false);
			break;
		case 3:
			goods01.SetInfo(m_goods_drop_list[0].id, m_goods_drop_list[0].quality, m_goods_drop_list[0].name);
			goods02.SetInfo(m_goods_drop_list[1].id, m_goods_drop_list[1].quality, m_goods_drop_list[1].name);
			goods03.SetInfo(m_goods_drop_list[2].id, m_goods_drop_list[2].quality, m_goods_drop_list[2].name);
			goods01.transform.localPosition = goods01_position;
			goods02.transform.localPosition = goods02_position;
			goods03.transform.localPosition = goods03_position;
			goods01.gameObject.SetActiveRecursively(true);
			goods02.gameObject.SetActiveRecursively(true);
			goods03.gameObject.SetActiveRecursively(true);
			break;
		default:
			Debug.Log("error!");
			break;
		}
	}

	public bool GetOpenStart()
	{
		if (recommend == null)
		{
			return true;
		}
		return recommend.GetOpenStart();
	}
}
