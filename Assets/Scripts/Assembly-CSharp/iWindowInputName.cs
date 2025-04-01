using UnityEngine;

public class iWindowInputName : MonoBehaviour
{
	public delegate void OnOK(string sInput);

	public delegate void OnCancel();

	public float m_fWidth = 250f;

	public float m_fHeight = 100f;

	protected OnOK m_OnOK;

	protected OnCancel m_OnCancel;

	protected bool m_bActive;

	public string m_sInput { get; set; }

	private void Awake()
	{
		m_sInput = string.Empty;
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnGUI()
	{
		if (m_bActive)
		{
			Rect screenRect = new Rect(((float)Screen.width - m_fWidth) / 2f, ((float)Screen.height - m_fHeight) / 2f, m_fWidth, m_fHeight);
			GUILayout.BeginArea(screenRect);
			GUILayout.Window(0, screenRect, MyWindow, "InputBox");
			GUILayout.EndArea();
		}
	}

	protected void MyWindow(int nID)
	{
		GUILayout.Label("Please input your nickname");
		GUILayout.BeginHorizontal();
		GUILayout.Label("NickName:", GUILayout.Width(80f));
		m_sInput = GUILayout.TextField(m_sInput);
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("OK"))
		{
			if (m_OnOK != null)
			{
				m_OnOK(m_sInput);
			}
			Show(false);
		}
		if (GUILayout.Button("CANCEL"))
		{
			if (m_OnCancel != null)
			{
				m_OnCancel();
			}
			Show(false);
		}
		GUILayout.EndHorizontal();
	}

	public void Show(bool bShow, OnOK onok = null, OnCancel oncancel = null)
	{
		m_bActive = bShow;
		m_OnOK = onok;
		m_OnCancel = oncancel;
	}
}
