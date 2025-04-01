using UnityEngine;

public class PopupWeaponUpdate : MonoBehaviour
{
	public TUILabel label_title;

	public TUILabel label_introduce;

	public LevelStarsEx level_stars;

	public PopupWeaponUpdateBuy btn_buy;

	public GoodsNeedItem[] m_arrGoodsNeedItem;

	private int goods_kind;

	public PopupWeaponUpdateSaleBuy btn_salebuy;

	public TUIMeshSprite img_sale_sign;

	public TUILabel label_sale_sign;

	private string img_sale_sign_texture01 = "sale_onsalebiaoqian";

	private string img_sale_sign_texture02 = "sale_onsalebiaoqian2";

	public TUILabel label_sale_introduce;

	public TUIButtonClick btn_claim;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void ShowWeaponUpdate()
	{
		base.gameObject.transform.localPosition = new Vector3(0f, 0f, base.gameObject.transform.localPosition.z);
		base.gameObject.GetComponent<Animation>().Play();
	}

	public void HideWeaponUpdate()
	{
		base.gameObject.transform.localPosition = new Vector3(0f, -1000f, base.gameObject.transform.localPosition.z);
	}

	public void Clear()
	{
		for (int i = 0; i < m_arrGoodsNeedItem.Length; i++)
		{
			if (!(m_arrGoodsNeedItem[i] == null))
			{
				m_arrGoodsNeedItem[i].HideGoodsNeedItem();
			}
		}
		if (btn_buy != null)
		{
			btn_buy.gameObject.SetActiveRecursively(false);
		}
		if (btn_claim != null)
		{
			btn_claim.gameObject.SetActiveRecursively(false);
		}
		if (btn_salebuy != null)
		{
			btn_salebuy.gameObject.SetActiveRecursively(false);
		}
		if (label_sale_introduce != null)
		{
			label_sale_introduce.gameObject.SetActiveRecursively(false);
		}
		if (img_sale_sign != null)
		{
			img_sale_sign.gameObject.SetActiveRecursively(false);
		}
		if (label_sale_sign != null)
		{
			label_sale_sign.gameObject.SetActiveRecursively(false);
		}
	}

	public void SetInfo(TUIWeaponAttributeInfo weaponattributeinfo)
	{
		if (weaponattributeinfo == null)
		{
			Debug.Log("no m_attribute_info!");
			return;
		}
		if (weaponattributeinfo.m_nLevel >= weaponattributeinfo.m_nLevelMax)
		{
			Debug.Log("error!you reach max level!");
			return;
		}
		label_title.Text = weaponattributeinfo.m_sName;
		TUIWeaponLevelInfo next = weaponattributeinfo.GetNext();
		if (next == null)
		{
			return;
		}
		label_introduce.Text = next.m_sLevelupDesc;
		Clear();
		if (weaponattributeinfo.m_nLevel > 0)
		{
			float x = label_title.CalculateBounds(label_title.Text).size.x;
			x *= label_title.transform.localScale.x;
			Vector3 position = new Vector3(label_title.transform.localPosition.x + x + 12f, level_stars.transform.localPosition.y, label_title.transform.localPosition.z);
			level_stars.SetStars(weaponattributeinfo.m_nLevel, position);
		}
		else
		{
			level_stars.SetStarsDisable();
		}
		if (weaponattributeinfo.m_bActive && weaponattributeinfo.m_nLevel < 1)
		{
			if (btn_claim != null)
			{
				btn_claim.gameObject.SetActiveRecursively(true);
				btn_claim.Disable(!weaponattributeinfo.m_bActiveCanGet);
			}
			if (label_sale_introduce != null)
			{
				label_sale_introduce.gameObject.SetActiveRecursively(true);
				label_sale_introduce.Text = weaponattributeinfo.m_sActiveStr;
			}
			if (img_sale_sign != null)
			{
				img_sale_sign.gameObject.SetActiveRecursively(true);
				img_sale_sign.texture = img_sale_sign_texture02;
			}
			if (label_sale_sign != null)
			{
				label_sale_sign.gameObject.SetActiveRecursively(true);
				label_sale_sign.Text = "Free";
			}
			return;
		}
		int nLevel = weaponattributeinfo.m_nLevel;
		nLevel = ((nLevel < 1) ? 1 : (nLevel + 1));
		TUIWeaponLevelInfo tUIWeaponLevelInfo = weaponattributeinfo.Get(nLevel);
		if (tUIWeaponLevelInfo == null)
		{
			return;
		}
		TUIPriceInfo price = tUIWeaponLevelInfo.m_Price;
		if (weaponattributeinfo.m_fDiscount < 1f && weaponattributeinfo.m_bUnlock)
		{
			int now_price = Mathf.CeilToInt(weaponattributeinfo.m_fDiscount * (float)price.price);
			if (btn_salebuy != null)
			{
				btn_salebuy.gameObject.SetActiveRecursively(true);
				TUIButtonClick component = btn_salebuy.GetComponent<TUIButtonClick>();
				if (component != null)
				{
					component.Reset();
				}
				btn_salebuy.SetBtnText(price.price, price.unit_type, now_price, price.unit_type);
			}
			if (img_sale_sign != null)
			{
				img_sale_sign.gameObject.SetActiveRecursively(true);
				img_sale_sign.texture = img_sale_sign_texture01;
			}
			if (label_sale_sign != null)
			{
				label_sale_sign.gameObject.SetActiveRecursively(true);
				label_sale_sign.Text = (int)((1f - weaponattributeinfo.m_fDiscount) * 100f + 0.5f) + "% off";
			}
		}
		else if (btn_buy != null)
		{
			btn_buy.gameObject.SetActiveRecursively(true);
			btn_buy.SetBtnText(price.price, price.unit_type);
			TUIButtonClick component2 = btn_buy.GetComponent<TUIButtonClick>();
			if (component2 != null)
			{
				component2.Reset();
			}
		}
		Debug.Log(weaponattributeinfo.m_nID + " " + nLevel + " " + tUIWeaponLevelInfo.m_ltGoodsNeed.Count);
		for (int i = 0; i < tUIWeaponLevelInfo.m_ltGoodsNeed.Count && i < m_arrGoodsNeedItem.Length; i++)
		{
			TUIGoodsNeedInfo tUIGoodsNeedInfo = tUIWeaponLevelInfo.m_ltGoodsNeed[i];
			if (tUIGoodsNeedInfo != null && !(m_arrGoodsNeedItem[i] == null))
			{
				int materialCount = TUIMappingInfo.Instance().GetMaterialCount(tUIGoodsNeedInfo.m_nID);
				int nNeedCount = tUIGoodsNeedInfo.m_nNeedCount;
				m_arrGoodsNeedItem[i].ShowGoodsNeedItem(tUIGoodsNeedInfo.m_nID, materialCount, nNeedCount);
			}
		}
	}
}
