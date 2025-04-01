using UnityEngine;

public class Btn_Search_Sell : MonoBehaviour
{
	public TUILabel label_normal;

	public TUILabel label_press;

	public TUILabel label_disable;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetStateNormal()
	{
		TUIButtonClick component = base.transform.GetComponent<TUIButtonClick>();
		if (component != null)
		{
			component.Disable(false);
		}
	}

	public void SetStatePress()
	{
	}

	public void SetStateDisable()
	{
		TUIButtonClick component = base.transform.GetComponent<TUIButtonClick>();
		if (component != null)
		{
			component.Disable(true);
		}
	}
}
