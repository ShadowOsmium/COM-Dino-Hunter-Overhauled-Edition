using UnityEngine;

public class PopupRoleBtnBuy : MonoBehaviour
{
	public enum PopupRoleBuyState
	{
		State_Unlock,
		State_Buy,
		State_Disable
	}

	public TUILabel label_normal;

	public TUILabel label_press;

	public TUIMeshSprite img_normal;

	public TUIMeshSprite img_press;

	private string gold_texture = "title_jingbi";

	private string crystal_texture = "title_shuijing";

	private PopupRoleBuyState btn_state;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public PopupRoleBuyState GetState()
	{
		return btn_state;
	}

	public void SetStateUnlock()
	{
		if (btn_state == PopupRoleBuyState.State_Disable)
		{
			base.gameObject.SetActiveRecursively(true);
			base.gameObject.GetComponent<TUIButtonClick>().Show();
		}
		btn_state = PopupRoleBuyState.State_Unlock;
		label_normal.Text = "UNLOCK";
		label_press.Text = "UNLOCK";
		img_normal.texture = string.Empty;
		img_press.texture = string.Empty;
	}

	public void SetStateBuy()
	{
		if (btn_state == PopupRoleBuyState.State_Disable)
		{
			base.gameObject.SetActiveRecursively(true);
			base.gameObject.GetComponent<TUIButtonClick>().Show();
		}
		btn_state = PopupRoleBuyState.State_Buy;
		label_normal.Text = "BUY";
		label_press.Text = "BUY";
		img_normal.texture = string.Empty;
		img_press.texture = string.Empty;
	}

	public void SetStateDisable()
	{
		if (btn_state != PopupRoleBuyState.State_Disable)
		{
			btn_state = PopupRoleBuyState.State_Disable;
			base.gameObject.SetActiveRecursively(false);
		}
	}
}
