using UnityEngine;

public class PopupRoleUnlock : MonoBehaviour
{
	public TUILabel label_introduce;

	public PopupSkillUpdateBuy btn_buy;

	public GameObject go_popup;

	private string gold_texture = "title_jingbi";

	private string crystal_texture = "title_shuijing";

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
		TUIPriceInfo unlock_price = roleInfo.unlock_price;
		if (unlock_price == null)
		{
			Debug.Log("error!");
			return;
		}
		int price = unlock_price.price;
		UnitType unit_type = unlock_price.unit_type;
		string text = "WOULD YOU LIKE TO UNLOCK THIS CHARACTER EARLY?";
		if (label_introduce != null)
		{
			label_introduce.Text = text;
		}
		if (btn_buy != null)
		{
			btn_buy.SetBtnText(price, unit_type);
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
