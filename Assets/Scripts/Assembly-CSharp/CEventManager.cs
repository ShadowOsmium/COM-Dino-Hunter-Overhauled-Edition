using System.Collections.Generic;
using gyEvent;

public class CEventManager
{
	protected int m_nCount;

	protected Dictionary<int, EventBase> m_dictEventImmediately;

	protected Dictionary<int, EventByFrame> m_dictEventByFrame;

	protected List<int> m_ltEventByFrameDestroy;

	protected List<int> m_ltEventImmediatelyDestroy;

	public CEventManager()
	{
		m_dictEventImmediately = new Dictionary<int, EventBase>();
		m_dictEventByFrame = new Dictionary<int, EventByFrame>();
		m_ltEventByFrameDestroy = new List<int>();
		m_ltEventImmediatelyDestroy = new List<int>();
	}

	public void Clear()
	{
		m_dictEventImmediately.Clear();
		m_dictEventByFrame.Clear();
	}

	protected int GenerateID()
	{
		if (++m_nCount > 99999999)
		{
			m_nCount = 1;
		}
		return m_nCount;
	}

	public void Update(float deltaTime)
	{
		foreach (EventByFrame value in m_dictEventByFrame.Values)
		{
			if (value.m_Compare != null && value.m_Compare.Update(deltaTime))
			{
				value.m_bTrigger = true;
			}
			if (value.m_bTrigger)
			{
				if (value.Trigger())
				{
					m_ltEventByFrameDestroy.Add(value.nID);
				}
				value.m_bTrigger = false;
			}
		}
		foreach (int item in m_ltEventByFrameDestroy)
		{
			m_dictEventByFrame.Remove(item);
		}
		m_ltEventByFrameDestroy.Clear();
	}

	public void AddEvent(EventByFrame iEvent)
	{
		iEvent.nID = GenerateID();
		m_dictEventByFrame.Add(iEvent.nID, iEvent);
	}

	public void AddEvent(EventBase iEvent)
	{
		iEvent.nID = GenerateID();
		m_dictEventImmediately.Add(iEvent.nID, iEvent);
	}

	public void Trigger(EventCondition condition)
	{
		foreach (EventByFrame value in m_dictEventByFrame.Values)
		{
			if (!value.m_bTrigger && value.IsMatch(condition))
			{
				value.m_bTrigger = true;
			}
		}
	}

	public void TriggerImmediately(EventCondition condition)
	{
		foreach (EventBase value in m_dictEventImmediately.Values)
		{
			if (value.IsMatch(condition) && value.Trigger())
			{
				m_ltEventImmediatelyDestroy.Add(value.nID);
			}
		}
		foreach (int item in m_ltEventImmediatelyDestroy)
		{
			m_dictEventImmediately.Remove(item);
		}
		m_ltEventImmediatelyDestroy.Clear();
	}
}
