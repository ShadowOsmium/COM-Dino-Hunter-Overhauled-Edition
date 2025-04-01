using System.Collections.Generic;
using UnityEngine;

public class AndroidReturnPlugin
{
	public delegate void OnEvent();

	public delegate void OnTUIEvent(TUIControl control, int event_type, float wparam, float lparam, object data);

	protected static AndroidReturnPlugin m_Instance;

	public static AndroidReturnPlugin instance
	{
		get
		{
			if (m_Instance == null)
			{
				m_Instance = new AndroidReturnPlugin();
			}
			return m_Instance;
		}
	}

	public OnEvent m_FuncQuit { get; protected set; }

	public List<OnEvent> m_ltFuncCur { get; protected set; }

	public OnEvent m_FuncBack { get; protected set; }

	public OnTUIEvent m_FuncSkipTutorial { get; protected set; }

	public bool m_bSkipTutorial { get; set; }

	public OnEvent m_FuncIngamePause { get; protected set; }

	public OnEvent m_FuncIngameContinue { get; protected set; }

	public bool m_bIngame { get; protected set; }

	public bool m_bIngameMutiply { get; protected set; }

	public bool m_bIngamePause { get; protected set; }

	public List<OnTUIEvent> m_ltFuncTUICur { get; protected set; }

	public AndroidReturnPlugin()
	{
		m_FuncQuit = null;
		m_ltFuncCur = new List<OnEvent>();
		m_ltFuncTUICur = new List<OnTUIEvent>();
		m_bIngame = false;
		m_bIngameMutiply = false;
		m_bIngamePause = false;
		m_FuncIngameContinue = null;
		m_FuncIngamePause = null;
	}

	public void Clear()
	{
		m_ltFuncCur.Clear();
		m_ltFuncTUICur.Clear();
	}

	public void SetQuitFunc(OnEvent func)
	{
		m_FuncQuit = func;
	}

	public void SetBackFunc(OnEvent func)
	{
		m_FuncBack = func;
	}

	public void SetSkipTutorialFunc(OnTUIEvent func)
	{
		m_FuncSkipTutorial = func;
	}

	public void SetCurFunc(OnEvent func)
	{
		if (!m_ltFuncCur.Contains(func))
		{
			m_ltFuncCur.Add(func);
		}
		m_ltFuncTUICur.Clear();
	}

	public void ClearFunc(OnEvent func)
	{
		if (m_ltFuncCur.Contains(func))
		{
			m_ltFuncCur.Remove(func);
		}
	}

	public void SetIngamePause(OnEvent func)
	{
		m_FuncIngamePause = func;
	}

	public void SetIngameContinue(OnEvent func)
	{
		m_FuncIngameContinue = func;
	}

	public void SetIngame(bool ingame)
	{
		m_bIngame = ingame;
	}

	public void SetIngameMutiply(bool ingamemutiply)
	{
		m_bIngameMutiply = ingamemutiply;
	}

	public void SetIngamePause(bool bPause)
	{
		m_bIngamePause = bPause;
	}

	public void SetCurFunc(OnTUIEvent func)
	{
		if (!m_ltFuncTUICur.Contains(func))
		{
			m_ltFuncTUICur.Add(func);
		}
		m_ltFuncCur.Clear();
	}

	public void ClearFunc(OnTUIEvent func)
	{
		if (m_ltFuncTUICur.Contains(func))
		{
			m_ltFuncTUICur.Remove(func);
		}
	}

	public void ClickAndroidReturn()
	{
		if (m_FuncSkipTutorial != null && !m_bSkipTutorial)
		{
			m_FuncSkipTutorial(null, 3, -1f, -1f, null);
		}
		else if (m_ltFuncCur.Count > 0)
		{
			m_ltFuncCur[m_ltFuncCur.Count - 1]();
		}
		else if (m_ltFuncTUICur.Count > 0)
		{
			m_ltFuncTUICur[m_ltFuncTUICur.Count - 1](null, 3, -1f, -1f, null);
		}
		else if (m_bIngame)
		{
			if (m_bIngameMutiply)
			{
				return;
			}
			if (!m_bIngamePause)
			{
				if (m_FuncIngamePause != null)
				{
					m_FuncIngamePause();
				}
			}
			else if (m_FuncIngameContinue != null)
			{
				m_FuncIngameContinue();
			}
		}
		else if (m_ltFuncCur.Count == 0 && m_ltFuncTUICur.Count == 0)
		{
			if (m_FuncBack != null)
			{
				m_FuncBack();
				Debug.Log("***********************************return back ");
			}
			else if (m_FuncQuit != null)
			{
				m_FuncQuit();
			}
		}
	}
}
