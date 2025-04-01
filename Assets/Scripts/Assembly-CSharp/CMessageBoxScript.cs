using UnityEngine;

public class CMessageBoxScript : MonoBehaviour
{
	public delegate void OnEvent();

	protected static CMessageBoxScript m_Instance;

	protected OnEvent m_OnOK;

	protected OnEvent m_OnCancel;

	protected MessageBox m_MessageBox;

	public static CMessageBoxScript GetInstance()
	{
		if (m_Instance == null)
		{
			GameObject gameObject = new GameObject("_MessageBoxScript");
			Object.DontDestroyOnLoad(gameObject);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.AddComponent<MessageBoxManager>();
			m_Instance = gameObject.AddComponent<CMessageBoxScript>();
		}
		return m_Instance;
	}

	public void MessageBox(string title, string message, OnEvent onok, OnEvent oncancel, params string[] buttons)
	{
		if (m_MessageBox == null)
		{
			m_MessageBox = new MessageBox(title, message, HandleMessageBoxBtnClick, buttons);
		}
		else
		{
			m_MessageBox.Title = title;
			m_MessageBox.Message = message;
			m_MessageBox.SetButtons(buttons);
		}
		if (m_MessageBox != null)
		{
			m_OnOK = onok;
			m_OnCancel = oncancel;
			m_MessageBox.Show();
		}
	}

	private void HandleMessageBoxBtnClick(MessageBox messageBox, int buttonIndex)
	{
		switch (buttonIndex)
		{
		case 0:
			Debug.Log("Clicked OK");
			if (m_OnOK != null)
			{
				m_OnOK();
			}
			break;
		case 1:
			Debug.Log("Clicked Cancel");
			if (m_OnCancel != null)
			{
				m_OnCancel();
			}
			break;
		}
	}
}
