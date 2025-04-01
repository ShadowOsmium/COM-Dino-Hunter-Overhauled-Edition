using UnityEngine;

public class PopupRoleBuy : MonoBehaviour
{
	public TUILabel label_title;

	public TUILabel label_introduce;

	public PopupSkillUpdateBuy btn_buy;

	public GameObject go_popup;

	private string gold_texture = "title_jingbi";

	private string crystal_texture = "title_shuijing";

	public PopupWeaponUpdateSaleBuy btn_salebuy;

	public TUIMeshSprite img_sale_sign;

	public TUILabel label_sale_sign;

	public TUIButtonClick btn_claim;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetInfo(ScrollList_RoleItem m_item)
	{
		if (m_item == null)
		{
			Debug.Log("error!");
			return;
		}
		TUIRoleInfo roleInfo = m_item.GetRoleInfo();
		if (roleInfo == null)
		{
			Debug.Log("error!");
			return;
		}
		bool do_buy = roleInfo.do_buy;
		bool is_active_buy = roleInfo.is_active_buy;
		bool can_active_buy = roleInfo.can_active_buy;
		string introduce = roleInfo.introduce;
		if (is_active_buy)
		{
			if (btn_buy != null)
			{
				btn_buy.gameObject.SetActiveRecursively(false);
			}
			if (btn_salebuy != null)
			{
				btn_salebuy.gameObject.SetActiveRecursively(false);
			}
			if (btn_claim != null)
			{
				btn_claim.gameObject.SetActiveRecursively(true);
				btn_claim.Disable(!can_active_buy);
			}
			if (label_sale_sign != null)
			{
				label_sale_sign.gameObject.SetActiveRecursively(true);
				label_sale_sign.Text = "FREE";
			}
			if (label_introduce != null)
			{
				label_introduce.gameObject.SetActiveRecursively(true);
				label_introduce.Text = roleInfo.introduce;
			}
			return;
		}
		if (btn_claim != null)
		{
			btn_claim.gameObject.SetActiveRecursively(false);
		}
		TUIPriceInfo do_buy_price = roleInfo.do_buy_price;
		if (do_buy_price == null)
		{
			Debug.Log("error!");
			return;
		}
		bool unlock = roleInfo.unlock;
		float discount = roleInfo.discount;
		bool flag = !(discount >= 1f);
		int price = do_buy_price.price;
		UnitType unit_type = do_buy_price.unit_type;
		string introduce2 = roleInfo.introduce;
		string text = roleInfo.name;
		if (label_title != null)
		{
			label_title.Text = text;
		}
		if (label_introduce != null)
		{
			label_introduce.Text = introduce2;
		}
		if (flag && unlock)
		{
			int now_price = Mathf.CeilToInt(discount * (float)price);
			if (btn_salebuy != null)
			{
				btn_salebuy.gameObject.SetActiveRecursively(true);
				TUIButtonClick component = btn_salebuy.GetComponent<TUIButtonClick>();
				if (component != null)
				{
					component.Reset();
				}
				btn_salebuy.SetBtnText(price, unit_type, now_price, unit_type);
			}
			if (btn_buy != null)
			{
				btn_buy.gameObject.SetActiveRecursively(false);
			}
			if (img_sale_sign != null)
			{
				img_sale_sign.gameObject.SetActiveRecursively(true);
			}
			if (label_sale_sign != null)
			{
				label_sale_sign.gameObject.SetActiveRecursively(true);
				label_sale_sign.Text = (int)((1f - discount) * 100f + 0.5f) + "% off";
			}
			return;
		}
		if (btn_salebuy != null)
		{
			btn_salebuy.gameObject.SetActiveRecursively(false);
		}
		if (btn_buy != null)
		{
			btn_buy.gameObject.SetActiveRecursively(true);
			TUIButtonClick component2 = btn_buy.GetComponent<TUIButtonClick>();
			if (component2 != null)
			{
				component2.Reset();
			}
			btn_buy.SetBtnText(price, unit_type);
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
