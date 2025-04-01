using System.Collections.Generic;
using UnityEngine;

public class DebugGUI : MonoBehaviour
{
	public int m_nMaxCount = 30;

	protected List<string> m_DebugList = new List<string>();

	protected int m_nLineHeight = 20;

	protected int m_nLineInterval = 5;

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}

	private void OnGUI()
	{
		if (!Application.isEditor)
		{
			return;
		}
		GUI.color = Color.green;
		for (int i = 0; i < m_DebugList.Count; i++)
		{
			if (i == 0)
			{
				GUI.Label(new Rect(0f, Screen.height - (m_nLineHeight * (i + 1) + 2), Screen.width, m_nLineHeight), m_DebugList[i]);
			}
			else
			{
				GUI.Label(new Rect(0f, Screen.height - (m_nLineHeight * (i + 1) + m_nLineInterval), Screen.width, m_nLineHeight), m_DebugList[i]);
			}
		}
	}

	public void Debug(string str)
	{
		if (!Application.isEditor)
		{
			return;
		}
		m_DebugList.Insert(0, str);
		if (m_DebugList.Count > m_nMaxCount)
		{
			m_DebugList.RemoveAt(m_DebugList.Count - 1);
		}
	}

	public void Clear()
	{
		m_DebugList.Clear();
	}
}
