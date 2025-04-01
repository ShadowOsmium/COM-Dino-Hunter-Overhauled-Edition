using System.Collections.Generic;
using UnityEngine;

public class PopupWeaponSupplement : MonoBehaviour
{
	public GameObject go_popup;

	public PopupSkillUpdateBuy btn_buy;

	public PopupWeaponSupplementGoods supplement_goods;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetSupplementInfo(TUISupplementInfo m_supplement_info)
	{
		if (m_supplement_info == null || supplement_goods == null)
		{
			Debug.Log("error!");
			return;
		}
		supplement_goods.ClearInfo();
		List<TUISupplementInfoGoods> goods_list = m_supplement_info.goods_list;
		if (goods_list != null)
		{
			for (int i = 0; i < goods_list.Count; i++)
			{
				TUIMappingInfo.CTUIMaterialInfo material = TUIMappingInfo.Instance().GetMaterial(goods_list[i].m_nMaterialID);
				if (material != null)
				{
					supplement_goods.SetGoodsInfo(i, goods_list[i].m_nMaterialID, goods_list[i].m_nMaterialCount, material.m_nQuality);
				}
			}
		}
		int nTotalCrystalCost = m_supplement_info.m_nTotalCrystalCost;
		if (nTotalCrystalCost != 0)
		{
			if (goods_list == null || goods_list.Count == 0)
			{
				supplement_goods.SetOnlyPriceInfo(nTotalCrystalCost, UnitType.Crystal);
			}
			else
			{
				supplement_goods.SetPriceInfo(nTotalCrystalCost, UnitType.Crystal);
			}
		}
		btn_buy.SetBtnText(m_supplement_info.m_nTotalCrystalCost, UnitType.Crystal);
	}

	public void SetSupplementBtnInfo(TUIPriceInfo m_info)
	{
		if (m_info == null)
		{
			Debug.Log("error!");
		}
		else
		{
			btn_buy.SetBtnText(m_info.price, m_info.unit_type);
		}
	}

	public void Show()
	{
		base.gameObject.transform.localPosition = new Vector3(0f, 0f, base.gameObject.transform.localPosition.z);
		if (go_popup != null && go_popup.GetComponent<Animation>() != null)
		{
			go_popup.GetComponent<Animation>().Play();
		}
	}

	public void Hide()
	{
		base.gameObject.transform.localPosition = new Vector3(0f, -1000f, base.gameObject.transform.localPosition.z);
	}
}
