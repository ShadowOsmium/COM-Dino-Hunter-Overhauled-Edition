using gyTaskSystem;
using UnityEngine;

public class iGameTaskUIHunterList : iGameTaskUIBase
{
	protected iGameTaskUIPlane m_GameTaskUIPlane;

	protected iGameTaskUIHunter[] m_arrTaskUIHunter;

	protected CTaskHunter m_TaskHunter;

	private void Awake()
	{
		base.Height = 0f;
	}

	public override void UpdateTask(float deltaTime)
	{
		if (m_TaskHunter == null)
		{
			return;
		}
		if (m_TaskTime != null)
		{
			m_TaskTime.SetTime(m_TaskHunter.TaskTime);
		}
		if (!m_TaskHunter.isUpdateData)
		{
			return;
		}
		for (int i = 0; i < m_TaskHunter.m_ltHunter.Count; i++)
		{
			if (!(m_arrTaskUIHunter[i] == null))
			{
				CTaskHunter.CHunter cHunter = m_TaskHunter.m_ltHunter[i];
				if (cHunter != null)
				{
					m_arrTaskUIHunter[i].SetCurNum(cHunter.nCurCount);
				}
			}
		}
		m_TaskHunter.isUpdateData = false;
	}

	public override void InitTask(CTaskBase taskbase)
	{
		m_TaskHunter = taskbase as CTaskHunter;
		if (m_TaskHunter != null && m_TaskHunter.m_ltHunter != null)
		{
			m_arrTaskUIHunter = new iGameTaskUIHunter[m_TaskHunter.m_ltHunter.Count];
			for (int i = 0; i < m_TaskHunter.m_ltHunter.Count; i++)
			{
				CTaskHunter.CHunter cHunter = m_TaskHunter.m_ltHunter[i];
				if (cHunter == null)
				{
					continue;
				}
				CMobInfoLevel mobInfo = m_GameData.GetMobInfo(cHunter.nMobID, 1);
				if (mobInfo == null)
				{
					continue;
				}
				GameObject gameObject = m_GameUI.AddControl(2001, base.transform);
				if (!(gameObject == null))
				{
					gameObject.transform.localPosition = new Vector3(0f, 0f - base.Height, 0f);
					gameObject.transform.localRotation = Quaternion.identity;
					iGameTaskUIHunter component = gameObject.GetComponent<iGameTaskUIHunter>();
					if (!(component == null))
					{
						component.SetIcon(mobInfo.sIcon);
						component.SetCurNum(cHunter.nCurCount);
						component.SetMaxNum(cHunter.nMaxCount);
						Add(i, component);
						base.Height += component.Height;
					}
				}
			}
		}
		AddTimeLimitUI(m_TaskHunter.TaskTime);
	}

	public override void Destroy()
	{
		for (int i = 0; i < m_arrTaskUIHunter.Length; i++)
		{
			if (!(m_arrTaskUIHunter[i] == null))
			{
				Object.Destroy(m_arrTaskUIHunter[i]);
				m_arrTaskUIHunter[i] = null;
			}
		}
		m_arrTaskUIHunter = null;
	}

	protected void Add(int nIndex, iGameTaskUIHunter hunter)
	{
		if (nIndex >= 0 && nIndex < m_arrTaskUIHunter.Length)
		{
			m_arrTaskUIHunter[nIndex] = hunter;
		}
	}

	public void SetLevel(int nMobID, int nLevel)
	{
		if (m_TaskHunter == null || m_TaskHunter.m_ltHunter == null)
		{
			return;
		}
		for (int i = 0; i < m_TaskHunter.m_ltHunter.Count; i++)
		{
			CTaskHunter.CHunter cHunter = m_TaskHunter.m_ltHunter[i];
			if (cHunter != null && !(m_arrTaskUIHunter[i] == null) && cHunter.nMobID == nMobID)
			{
				m_arrTaskUIHunter[i].SetLV(nLevel);
			}
		}
	}
}
