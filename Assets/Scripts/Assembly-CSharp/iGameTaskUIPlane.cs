using System.Collections.Generic;
using UnityEngine;

public class iGameTaskUIPlane : MonoBehaviour
{
	protected iGameSceneBase m_GameScene;

	protected iGameUIBase m_GameUI;

	protected List<iGameTaskUIBase> m_ltTaskUI;

	protected float m_fTotalHeight;

	public List<iGameTaskUIBase> GetData()
	{
		return m_ltTaskUI;
	}

	private void Awake()
	{
		m_ltTaskUI = new List<iGameTaskUIBase>();
		m_fTotalHeight = 0f;
	}

	public void Initialize()
	{
		m_GameScene = iGameApp.GetInstance().m_GameScene;
		m_GameUI = m_GameScene.GetGameUI();
	}

	public void Clear()
	{
		foreach (iGameTaskUIBase item in m_ltTaskUI)
		{
			item.Destroy();
			Object.Destroy(item.gameObject);
		}
		m_ltTaskUI.Clear();
	}

	public iGameTaskUIBase Get(int nIndex)
	{
		if (nIndex < 0 || nIndex >= m_ltTaskUI.Count)
		{
			return null;
		}
		return m_ltTaskUI[nIndex];
	}

	public void Show(bool bShow)
	{
		base.gameObject.active = bShow;
		foreach (iGameTaskUIBase item in m_ltTaskUI)
		{
			item.Show(bShow);
		}
	}

	public iGameTaskUIBase Add(int nTaskType)
	{
		GameObject gameObject = null;
		switch (nTaskType)
		{
		case 2:
			gameObject = m_GameUI.AddControl(2002, base.transform);
			break;
		case 1:
			gameObject = m_GameUI.AddControl(2004, base.transform);
			break;
		case 3:
			gameObject = m_GameUI.AddControl(2006, base.transform);
			break;
		case 4:
			gameObject = m_GameUI.AddControl(2005, base.transform);
			break;
		case 5:
			gameObject = m_GameUI.AddControl(2007, base.transform);
			break;
		case 6:
			gameObject = m_GameUI.AddControl(2008, base.transform);
			break;
		}
		if (gameObject == null)
		{
			return null;
		}
		gameObject.transform.localPosition = new Vector3(0f, m_fTotalHeight, 0f);
		gameObject.transform.localRotation = Quaternion.identity;
		iGameTaskUIBase component = gameObject.GetComponent<iGameTaskUIBase>();
		if (component == null)
		{
			return null;
		}
		m_fTotalHeight -= component.Height;
		m_ltTaskUI.Add(component);
		return component;
	}
}
