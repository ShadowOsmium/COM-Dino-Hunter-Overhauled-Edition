using UnityEngine;

public class Btn_BuySkill : MonoBehaviour
{
	public enum StateButtonSkill
	{
		State_Unlock,
		State_Learn,
		State_Update,
		State_Disable
	}

	public TUIMeshSprite img_crystal_normal;

	public TUIMeshSprite img_crystal_press;

	public TUILabel label_price_normal;

	public TUILabel label_price_press;

	private string gold_texture = "title_jingbi";

	private string crystal_texture = "title_shuijing";

	private StateButtonSkill state_btnskill;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetStateUnlock()
	{
		state_btnskill = StateButtonSkill.State_Unlock;
		base.gameObject.SetActiveRecursively(true);
		base.gameObject.GetComponent<TUIButtonClick>().Show();
		img_crystal_normal.texture = string.Empty;
		img_crystal_press.texture = string.Empty;
		label_price_normal.Text = "UNLOCK";
		label_price_press.Text = "UNLOCK";
	}

	public void SetStateBuy()
	{
		state_btnskill = StateButtonSkill.State_Learn;
		base.gameObject.SetActiveRecursively(true);
		base.gameObject.GetComponent<TUIButtonClick>().Show();
		img_crystal_normal.texture = string.Empty;
		img_crystal_press.texture = string.Empty;
		label_price_normal.Text = "LEARN";
		label_price_press.Text = "LEARN";
	}

	public void SetStateUpdate()
	{
		state_btnskill = StateButtonSkill.State_Update;
		base.gameObject.SetActiveRecursively(true);
		base.gameObject.GetComponent<TUIButtonClick>().Show();
		img_crystal_normal.texture = string.Empty;
		img_crystal_press.texture = string.Empty;
		label_price_normal.Text = "UPGRADE";
		label_price_press.Text = "UPGRADE";
	}

	public void SetStateDisable()
	{
		state_btnskill = StateButtonSkill.State_Disable;
		base.gameObject.SetActiveRecursively(false);
		img_crystal_normal.texture = string.Empty;
		img_crystal_press.texture = string.Empty;
		label_price_normal.Text = string.Empty;
		label_price_press.Text = string.Empty;
	}

	public StateButtonSkill GetStateBtnSkill()
	{
		return state_btnskill;
	}
}
