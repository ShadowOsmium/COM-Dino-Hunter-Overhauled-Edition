using UnityEngine;

public class iOnOrderEvent : MonoBehaviour, IRoamEvent
{
	public delegate void OnOrderStop();

	public OnOrderStop m_OrderStartFunc;

	public OnOrderStop m_OrderStopFunc;

	public void OnRoamTrigger()
	{
		if (m_OrderStartFunc != null)
		{
			m_OrderStartFunc();
		}
	}

	public void OnRoamStop()
	{
		if (m_OrderStopFunc != null)
		{
			m_OrderStopFunc();
		}
	}
}
