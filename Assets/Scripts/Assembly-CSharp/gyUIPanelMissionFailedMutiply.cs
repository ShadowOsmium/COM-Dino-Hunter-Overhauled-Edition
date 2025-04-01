using UnityEngine;

public class gyUIPanelMissionFailedMutiply : MonoBehaviour
{
	public gyUIResultExpBar m_CharExp;

	public gyUIResultExpBar m_HunterExp;

	protected bool m_bShow;

	private void Awake()
	{
		m_bShow = false;
		base.gameObject.SetActiveRecursively(false);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Show(bool bShow)
	{
		m_bShow = bShow;
		base.gameObject.SetActiveRecursively(bShow);
		if (bShow)
		{
			base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		}
		else
		{
			base.transform.localPosition = new Vector3(10000f, 10000f, base.transform.localPosition.z);
		}
	}

	public void SetCharExp(int lstLevel, float lstRate, int curLevel, float curRate)
	{
		if (!(m_CharExp == null))
		{
			m_CharExp.BarValue = lstRate;
			m_CharExp.Level = lstLevel;
			if (lstLevel != curLevel || lstRate != curRate)
			{
				m_CharExp.SetAnimation(curRate, curLevel);
			}
		}
	}

	public void SetHunterExp(int lstLevel, float lstRate, int curLevel, float curRate)
	{
		if (!(m_HunterExp == null))
		{
			m_HunterExp.BarValue = lstRate;
			m_HunterExp.Level = lstLevel;
			if (lstLevel != curLevel || lstRate != curRate)
			{
				m_HunterExp.SetAnimation(curRate, curLevel);
			}
		}
	}
}
