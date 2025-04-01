using System.Text.RegularExpressions;
using UnityEngine;

public class IphoneInputPlugin
{
	public delegate void OnEvent(string str);

	protected static IphoneInputPlugin m_Instance;

	protected OnEvent m_OnDone;

	protected TouchScreenKeyboard m_KeyBoard;

	protected int m_nLimitLength;

	protected Regex m_Regex;

	protected int m_nEnterCount;

	public string sValue { get; set; }

	public IphoneInputPlugin()
	{
		sValue = string.Empty;
		m_OnDone = null;
		m_KeyBoard = null;
		m_nLimitLength = 0;
	}

	public static IphoneInputPlugin GetInstance()
	{
		if (m_Instance == null)
		{
			m_Instance = new IphoneInputPlugin();
		}
		return m_Instance;
	}

	public void Update(float deltaTime)
	{
		if (m_KeyBoard == null)
		{
			return;
		}
		if (!m_KeyBoard.active || m_KeyBoard.done)
		{
			Debug.Log("keyboard input done or hide!");
			if (m_OnDone != null)
			{
				m_OnDone(sValue);
			}
			m_KeyBoard.active = false;
			m_KeyBoard = null;
			return;
		}
		if (m_KeyBoard.text.Length < 1)
		{
			sValue = string.Empty;
			return;
		}
		m_nEnterCount = 0;
		for (int i = 0; i < m_KeyBoard.text.Length; i++)
		{
			if (m_KeyBoard.text[i] == '\n')
			{
				m_nEnterCount++;
			}
		}
		Debug.Log("etner count = " + m_nEnterCount);
		if (m_nEnterCount > 2)
		{
			m_KeyBoard.text = sValue;
			return;
		}
		if (m_nLimitLength > 0 && m_KeyBoard.text.Length > m_nLimitLength)
		{
			Debug.Log("keyboard input so long!");
			m_KeyBoard.text = m_KeyBoard.text.Substring(0, m_nLimitLength);
		}
		if (m_KeyBoard.text != sValue)
		{
			if (!m_Regex.IsMatch(m_KeyBoard.text))
			{
				UnityEngine.Debug.Log("keyboard input illegal! " + m_KeyBoard.text + " " + m_Regex.ToString());
				m_KeyBoard.text = sValue;
			}
			else
			{
				sValue = m_KeyBoard.text;
			}
		}
	}

	public void Open(string title, string text, int limitlength, OnEvent ondone, TouchScreenKeyboardType keyboardtype = TouchScreenKeyboardType.Default, string regexstr = "^[\\w]+${0,12}", bool secure = false, bool autocorrection = true, bool multiline = false, bool alert = false)
	{
		Debug.Log("open keyboard");
		m_KeyBoard = TouchScreenKeyboard.Open(text, TouchScreenKeyboardType.ASCIICapable, autocorrection, multiline, secure, alert, title);
		m_nLimitLength = limitlength;
		m_OnDone = ondone;
		m_Regex = new Regex(regexstr);
		sValue = text;
		if (!m_Regex.IsMatch(sValue))
		{
			sValue = string.Empty;
		}
	}
}
