using UnityEngine;

public class PopupWeaponBuy : MonoBehaviour
{
	public enum PopupWeaponBuyState
	{
		State_None,
		State_Craft,
		State_Update,
		State_Disable
	}

	public TUILabel label_normal;

	public TUILabel label_press;

	public TUIButtonClick btn_click;

	private PopupWeaponBuyState btn_state;

	private void Start()
	{
		SetStateCraft();
	}

	private void Update()
	{
	}

	public PopupWeaponBuyState GetState()
	{
		return btn_state;
	}

	public void SetStateCraft(bool m_claim = false)
	{
		if (btn_state != PopupWeaponBuyState.State_Craft)
		{
			if (!m_claim)
			{
				label_normal.Text = "CRAFT";
				label_press.Text = "CRAFT";
			}
			else
			{
				label_normal.Text = "CLAIM";
				label_press.Text = "CLAIM";
			}
			if (btn_click != null)
			{
				btn_click.gameObject.SetActiveRecursively(true);
				btn_click.Show();
			}
			btn_state = PopupWeaponBuyState.State_Craft;
		}
	}

	public void SetStateUpdate()
	{
		if (btn_state != PopupWeaponBuyState.State_Update)
		{
			label_normal.Text = "UPGRADE";
			label_press.Text = "UPGRADE";
			if (btn_click != null)
			{
				btn_click.gameObject.SetActiveRecursively(true);
				btn_click.Show();
			}
			btn_state = PopupWeaponBuyState.State_Update;
		}
	}

	public void SetStateDisable()
	{
		if (btn_state != PopupWeaponBuyState.State_Disable)
		{
			if (btn_click != null)
			{
				btn_click.gameObject.SetActiveRecursively(false);
			}
			btn_state = PopupWeaponBuyState.State_Disable;
		}
	}
}
