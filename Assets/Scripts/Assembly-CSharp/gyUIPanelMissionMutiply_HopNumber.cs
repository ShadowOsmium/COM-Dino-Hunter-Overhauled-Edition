using UnityEngine;

[RequireComponent(typeof(gyUIHopNumber))]
public class gyUIPanelMissionMutiply_HopNumber : MonoBehaviour
{
	protected gyUIHopNumber m_AnimScript;

	protected int m_nValue;

	public bool IsAnim
	{
		get
		{
			return m_AnimScript.isHop;
		}
	}

	public int Value
	{
		get
		{
			return m_nValue;
		}
		set
		{
			if (m_AnimScript != null)
			{
				m_AnimScript.Go(m_nValue, value, 1.5f);
			}
			m_nValue = value;
		}
	}

	private void Awake()
	{
		m_AnimScript = GetComponent<gyUIHopNumber>();
	}
}
