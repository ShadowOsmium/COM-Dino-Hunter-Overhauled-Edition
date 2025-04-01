using System;
using System.Collections.Generic;
using UnityEngine;

public class gyUICellPanel : MonoBehaviour
{
	public delegate void OnClickCellFunc(int nIndex);

	public delegate void OnPressCellFunc(int nIndex, bool bPress);

	protected OnClickCellFunc m_OnClickCellFunc;

	protected OnPressCellFunc m_OnPressCellFunc;

	protected gyUICell[] m_arrCell;

	public void Awake()
	{
		List<gyUICell> list = new List<gyUICell>();
		foreach (Transform item in base.transform)
		{
			gyUICell component = item.GetComponent<gyUICell>();
			if (component != null)
			{
				list.Add(component);
			}
		}
		m_arrCell = list.ToArray();
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void OnClickCell(int nIndex)
	{
		if (m_OnClickCellFunc != null)
		{
			m_OnClickCellFunc(nIndex);
		}
	}

	public void OnPressCell(int nIndex, bool bPress)
	{
		if (m_OnPressCellFunc != null)
		{
			m_OnPressCellFunc(nIndex, bPress);
		}
	}

	public void RegisterOnClickCell(OnClickCellFunc func)
	{
		m_OnClickCellFunc = (OnClickCellFunc)Delegate.Combine(m_OnClickCellFunc, func);
	}

	public void UnregisterOnClickCell(OnClickCellFunc func)
	{
		m_OnClickCellFunc = (OnClickCellFunc)Delegate.Remove(m_OnClickCellFunc, func);
	}

	public void RegisterOnPressCell(OnPressCellFunc func)
	{
		m_OnPressCellFunc = (OnPressCellFunc)Delegate.Combine(m_OnPressCellFunc, func);
	}

	public void UnregisterOnPressCell(OnPressCellFunc func)
	{
		m_OnPressCellFunc = (OnPressCellFunc)Delegate.Remove(m_OnPressCellFunc, func);
	}
}
