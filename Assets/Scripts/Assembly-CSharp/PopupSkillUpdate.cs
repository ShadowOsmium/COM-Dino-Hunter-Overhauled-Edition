using UnityEngine;

public class PopupSkillUpdate : MonoBehaviour
{
	public TUILabel label_title;

	public TUILabel label_introduce;

	public PopupSkillUpdateBuy btn_buy;

	public PopupWeaponUpdateSaleBuy btn_salebuy;

	public TUIMeshSprite img_sale_sign;

	public TUILabel label_sale_sign;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void ShowSkillUpdate()
	{
		base.gameObject.transform.localPosition = new Vector3(0f, 0f, base.gameObject.transform.localPosition.z);
		base.gameObject.GetComponent<Animation>().Play();
	}

	public void HideSkillUpdate()
	{
		base.gameObject.transform.localPosition = new Vector3(0f, 1000f, base.gameObject.transform.localPosition.z);
	}

	public void SetInfo(ScrollList_SkillItem m_item)
	{
		if (m_item == null)
		{
			Debug.Log("error!");
			return;
		}
		bool skillUnlock = m_item.GetSkillUnlock();
		float discount = m_item.GetDiscount();
		bool flag = !(discount >= 1f);
		int skillLevelMax = m_item.GetSkillLevelMax();
		int skillLevel = m_item.GetSkillLevel();
		label_introduce.Text = m_item.GetSkillIntroduce();
		label_title.Text = m_item.GetSkillName();
		TUIPriceInfo skillUpdatePrice = m_item.GetSkillUpdatePrice();
		if (skillUpdatePrice == null)
		{
			Debug.Log("error!");
			return;
		}
		int price = skillUpdatePrice.price;
		UnitType unit_type = skillUpdatePrice.unit_type;
		if (flag && skillUnlock)
		{
			int now_price = Mathf.CeilToInt(discount * (float)skillUpdatePrice.price);
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
		if (img_sale_sign != null)
		{
			img_sale_sign.gameObject.SetActiveRecursively(false);
		}
		if (label_sale_sign != null)
		{
			label_sale_sign.gameObject.SetActiveRecursively(false);
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
	}
}
