using UnityEngine;

public class PopupGoldToCrystal : MonoBehaviour
{
	public GameObject go_popup;

	public TUILabel label_title;

	public TUILabel label_introduce;

	public TUIButtonClick btn_yes;

	private int gold_exchange_count;

	private int crystal_exchange_count;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetTitle(string m_str)
	{
		if (label_title != null)
		{
			label_title.Text = m_str;
		}
	}

	public void SetIntroduce(string m_str)
	{
		if (label_introduce != null)
		{
			label_introduce.Text = m_str;
		}
	}

	public void SetBtn(int m_value, UnitType m_unit)
	{
		if (btn_yes != null)
		{
			PopupPriceBtn component = btn_yes.GetComponent<PopupPriceBtn>();
			if (component != null)
			{
				component.SetInfo(m_value, m_unit);
			}
		}
	}

	public void Show()
	{
		base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		if (go_popup != null && go_popup.GetComponent<Animation>() != null)
		{
			go_popup.GetComponent<Animation>().Play();
		}
	}

	public void Hide()
	{
		base.transform.localPosition = new Vector3(0f, -1000f, base.transform.localPosition.z);
	}

	public void SetInfo(string m_title, string m_introduce, int m_gold, int m_crystal, UnitType m_unit)
	{
		gold_exchange_count = m_gold;
		crystal_exchange_count = m_crystal;
		SetTitle(m_title);
		SetIntroduce(m_introduce);
		SetBtn(m_crystal, m_unit);
	}

	public void ClearInfo()
	{
		if (label_title != null)
		{
			label_title.Text = string.Empty;
		}
		if (label_introduce != null)
		{
			label_introduce.Text = string.Empty;
		}
		if (btn_yes != null)
		{
			PopupPriceBtn component = btn_yes.GetComponent<PopupPriceBtn>();
			if (component != null)
			{
				component.ClearInfo();
			}
		}
	}

	public int GetGoldExchangeCount()
	{
		return gold_exchange_count;
	}

	public int GetCrystalExchangeCount()
	{
		return crystal_exchange_count;
	}
}
