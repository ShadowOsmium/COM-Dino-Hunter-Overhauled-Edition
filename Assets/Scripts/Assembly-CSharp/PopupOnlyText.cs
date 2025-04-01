using UnityEngine;

public class PopupOnlyText : MonoBehaviour
{
	public GameObject go_popup;

	public TUILabel label_text;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetInfo(string m_text)
	{
		if (label_text != null)
		{
			label_text.Text = m_text;
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
}
