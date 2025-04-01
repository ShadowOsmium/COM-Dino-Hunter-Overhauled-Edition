using UnityEngine;

public class gyUIMessageBox : MonoBehaviour
{
	public enum kMessageBoxType
	{
		None,
		OK,
		OKCANCEL
	}

	public UILabel mMessageLabel;

	public gyUILabelButton mOK;

	public gyUILabelButton mCancel;

	public UISlicedSprite mBackground;

	protected gyUIScreenMask m_ScreenMask;

	protected kMessageBoxType m_msgboxtype;

	protected bool m_bShow;

	protected gyUIEventRegister.OnClickFunc m_OnOK;

	private void Awake()
	{
		Hide();
	}

	protected void ShowMessageBox(kMessageBoxType msgboxtype)
	{
		if (!(mOK == null) && !(mCancel == null))
		{
			mOK.SetLabel("OK");
			mCancel.SetLabel("Cancel");
			switch (msgboxtype)
			{
			case kMessageBoxType.None:
				mOK.gameObject.SetActiveRecursively(false);
				mCancel.gameObject.SetActiveRecursively(false);
				mBackground.transform.localPosition = new Vector3(0f, 16f, mBackground.transform.localPosition.z);
				mBackground.transform.localScale = new Vector3(mBackground.transform.localScale.x, 100.25f, mBackground.transform.localScale.z);
				break;
			case kMessageBoxType.OK:
				mOK.transform.localPosition = new Vector3(0f, mOK.transform.localPosition.y, mOK.transform.localPosition.z);
				mOK.gameObject.SetActiveRecursively(true);
				mCancel.gameObject.SetActiveRecursively(false);
				mBackground.transform.localPosition = new Vector3(0f, 0f, mBackground.transform.localPosition.z);
				mBackground.transform.localScale = new Vector3(mBackground.transform.localScale.x, 133.35f, mBackground.transform.localScale.z);
				break;
			case kMessageBoxType.OKCANCEL:
				mOK.transform.localPosition = new Vector3(-85f, mOK.transform.localPosition.y, mOK.transform.localPosition.z);
				mOK.gameObject.SetActiveRecursively(true);
				mCancel.gameObject.SetActiveRecursively(true);
				mBackground.transform.localPosition = new Vector3(0f, 0f, mBackground.transform.localPosition.z);
				mBackground.transform.localScale = new Vector3(mBackground.transform.localScale.x, 133.35f, mBackground.transform.localScale.z);
				break;
			}
		}
	}

	protected gyUIEventRegister GetEventResiger(GameObject o)
	{
		gyUIEventRegister gyUIEventRegister2 = o.GetComponent<gyUIEventRegister>();
		if (gyUIEventRegister2 == null)
		{
			gyUIEventRegister2 = o.AddComponent<gyUIEventRegister>();
		}
		return gyUIEventRegister2;
	}

	public void SetMask(gyUIScreenMask mask)
	{
		m_ScreenMask = mask;
	}

	public void Message(string str, kMessageBoxType msgboxtype = kMessageBoxType.OK, gyUIEventRegister.OnClickFunc onok = null)
	{
		if (m_bShow)
		{
			Hide();
		}
		m_bShow = true;
		base.gameObject.SetActiveRecursively(true);
		base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		m_msgboxtype = msgboxtype;
		if (mMessageLabel != null)
		{
			mMessageLabel.text = str;
		}
		ShowMessageBox(msgboxtype);
		gyUIEventRegister eventResiger = GetEventResiger(mOK.gameObject);
		eventResiger.Clear();
		eventResiger.RegisterOnClick(Hide);
		m_OnOK = onok;
		if (onok != null)
		{
			eventResiger.RegisterOnClick(onok);
		}
		eventResiger = GetEventResiger(mCancel.gameObject);
		eventResiger.Clear();
		eventResiger.RegisterOnClick(Hide);
		TweenScale tweenScale = TweenScale.Begin(base.gameObject, 0.2f, Vector3.one);
		tweenScale.from = Vector3.zero;
		tweenScale.to = Vector3.one;
		if (m_ScreenMask != null)
		{
			m_ScreenMask.ShowMask(true, -19f, 0);
		}
		switch (m_msgboxtype)
		{
		case kMessageBoxType.OK:
			AndroidReturnPlugin.instance.SetCurFunc(EventOK);
			break;
		case kMessageBoxType.OKCANCEL:
			AndroidReturnPlugin.instance.SetCurFunc(Hide);
			break;
		}
	}

	protected void EventOK()
	{
		Hide();
		if (m_OnOK != null)
		{
			m_OnOK();
		}
	}

	public void Hide()
	{
		m_bShow = false;
		base.gameObject.SetActiveRecursively(false);
		base.transform.localPosition = new Vector3(10000f, 10000f, base.transform.localPosition.z);
		if (m_ScreenMask != null)
		{
			m_ScreenMask.ShowMask(false, -19f, 0);
		}
		switch (m_msgboxtype)
		{
		case kMessageBoxType.OK:
			AndroidReturnPlugin.instance.ClearFunc(EventOK);
			break;
		case kMessageBoxType.OKCANCEL:
			AndroidReturnPlugin.instance.ClearFunc(Hide);
			break;
		}
	}
}
