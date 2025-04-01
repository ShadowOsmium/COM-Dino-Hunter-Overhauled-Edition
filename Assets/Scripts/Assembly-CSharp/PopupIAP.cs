using UnityEngine;

public class PopupIAP : MonoBehaviour
{
	public Transform popup_yes;

	public Transform popup_waitting;

	public TUILabel label_yes_text;

	public TUILabel label_waiting_text;

	private Vector3 popup_pos = Vector3.zero;

	private Vector3 popup_yes_pos = Vector3.zero;

	private Vector3 popup_waiting_pos = Vector3.zero;

	private void Awake()
	{
		popup_pos = base.transform.localPosition;
		if (popup_yes != null)
		{
			popup_yes_pos = popup_yes.localPosition;
		}
		if (popup_waitting != null)
		{
			popup_waiting_pos = popup_waitting.localPosition;
		}
		Hide();
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Hide()
	{
		base.transform.localPosition = popup_pos + new Vector3(0f, 1000f, 0f);
		if (popup_yes != null)
		{
			popup_yes.localPosition = popup_yes_pos;
		}
		if (popup_waitting != null)
		{
			popup_waitting.localPosition = popup_waiting_pos;
		}
	}

	public void ShowPopupYes(string m_text = "")
	{
		base.transform.localPosition = popup_pos;
		if ((bool)popup_yes)
		{
			popup_yes.localPosition = popup_yes_pos;
			if (popup_yes.GetComponent<Animation>() != null)
			{
				popup_yes.GetComponent<Animation>().Play();
			}
		}
		if (popup_waitting != null)
		{
			popup_waitting.localPosition = popup_waiting_pos + new Vector3(0f, 1000f, 0f);
			if (popup_waitting != null)
			{
				popup_waitting.GetComponent<Animation>().Play();
			}
		}
		if (label_yes_text != null && m_text != string.Empty)
		{
			label_yes_text.Text = m_text;
		}
	}

	public void ShowPopupWaiting(string m_text = "")
	{
		base.transform.localPosition = popup_pos;
		popup_yes.localPosition = popup_yes_pos + new Vector3(0f, 1000f, 0f);
		popup_waitting.localPosition = popup_waiting_pos;
		if (popup_waitting.GetComponent<Animation>() != null)
		{
			popup_waitting.GetComponent<Animation>().Play();
		}
		if (label_waiting_text != null && m_text != string.Empty)
		{
			label_waiting_text.Text = m_text;
		}
	}
}
