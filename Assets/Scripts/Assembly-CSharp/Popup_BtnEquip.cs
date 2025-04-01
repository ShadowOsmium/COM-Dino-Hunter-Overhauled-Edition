using UnityEngine;

public class Popup_BtnEquip : MonoBehaviour
{
	public TUILabel label_normal;

	public TUILabel label_press;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetEquip()
	{
		label_normal.Text = "Equip";
		label_press.Text = "Equip";
	}

	public void SetUnEquip()
	{
		label_normal.Text = "Demount";
		label_press.Text = "Demount";
	}
}
