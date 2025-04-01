using System;
using UnityEngine;

public class gyUIEventRegister : MonoBehaviour
{
	public delegate void OnClickFunc();

	public delegate void OnDragFunc(Vector2 delta);

	public delegate void OnPressFunc(bool bPressed);

	public delegate void OnHoldFunc();

	protected OnClickFunc m_OnClickFunc;

	protected OnDragFunc m_OnDragFunc;

	protected OnPressFunc m_OnPressFunc;

	protected OnHoldFunc m_OnHoldFunc;

	protected bool m_bHold;

	protected float m_fHoldDelay;

	protected float m_fHoldDelayCount;

	private void Start()
	{
	}

	private void Update()
	{
		if (!m_bHold && m_fHoldDelayCount >= 0f)
		{
			m_fHoldDelayCount += Time.deltaTime;
			if (m_fHoldDelayCount >= m_fHoldDelay)
			{
				m_bHold = true;
				m_fHoldDelayCount = -1f;
			}
		}
		if (m_bHold && m_OnHoldFunc != null)
		{
			m_OnHoldFunc();
		}
	}

	protected void OnClick()
	{
		if (m_OnClickFunc != null)
		{
			m_OnClickFunc();
		}
	}

	protected void OnPress(bool isPressed)
	{
		if (m_OnPressFunc != null)
		{
			m_OnPressFunc(isPressed);
		}
		if (!isPressed)
		{
			m_bHold = false;
			m_fHoldDelayCount = -1f;
		}
		else
		{
			m_bHold = false;
			m_fHoldDelayCount = 0f;
		}
	}

	protected void OnDrag(Vector2 delta)
	{
		if (m_OnDragFunc != null)
		{
			m_OnDragFunc(delta);
		}
		m_bHold = false;
		m_fHoldDelayCount = 0f;
	}

	public void RegisterOnClick(OnClickFunc func)
	{
		m_OnClickFunc = (OnClickFunc)Delegate.Combine(m_OnClickFunc, func);
	}

	public void UnregisterOnClick(OnClickFunc func)
	{
		m_OnClickFunc = (OnClickFunc)Delegate.Remove(m_OnClickFunc, func);
	}

	public void RegisterOnDrag(OnDragFunc func)
	{
		m_OnDragFunc = (OnDragFunc)Delegate.Combine(m_OnDragFunc, func);
	}

	public void UnregisterOnDrag(OnDragFunc func)
	{
		m_OnDragFunc = (OnDragFunc)Delegate.Remove(m_OnDragFunc, func);
	}

	public void RegisterOnPress(OnPressFunc func)
	{
		m_OnPressFunc = (OnPressFunc)Delegate.Combine(m_OnPressFunc, func);
	}

	public void UnregisterOnPress(OnPressFunc func)
	{
		m_OnPressFunc = (OnPressFunc)Delegate.Remove(m_OnPressFunc, func);
	}

	public void RegisterOnHold(OnHoldFunc func)
	{
		m_OnHoldFunc = (OnHoldFunc)Delegate.Combine(m_OnHoldFunc, func);
	}

	public void UnregisterOnHold(OnHoldFunc func)
	{
		m_OnHoldFunc = (OnHoldFunc)Delegate.Remove(m_OnHoldFunc, func);
	}

	public void SetHoldTime(float fTime)
	{
		m_fHoldDelay = fTime;
	}

	public void Clear()
	{
		m_OnClickFunc = null;
		m_OnDragFunc = null;
		m_OnPressFunc = null;
		m_OnHoldFunc = null;
	}
}
