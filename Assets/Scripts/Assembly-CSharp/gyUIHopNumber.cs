using UnityEngine;

public class gyUIHopNumber : MonoBehaviour
{
	protected UILabel mLabel;

	protected float m_fFrom;

	protected float m_fTo;

	protected float m_fRateSpeed;

	protected float m_fRate;

	protected bool m_bHop;

	protected int m_nLastNumber;

	public bool isHop
	{
		get
		{
			return m_bHop;
		}
	}

	private void Awake()
	{
		mLabel = GetComponentInChildren<UILabel>();
		if (mLabel != null)
		{
			mLabel.text = "0";
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (!(mLabel == null) && m_bHop)
		{
			m_fRate += m_fRateSpeed * Time.deltaTime;
			int num = (int)MyUtils.Lerp(m_fFrom, m_fTo, m_fRate);
			mLabel.text = num.ToString();
			if (m_fRate >= 1f)
			{
				m_bHop = false;
			}
			if (Mathf.Abs(num - m_nLastNumber) >= 1)
			{
				m_nLastNumber = num;
				CUISound.GetInstance().Play("UI_Count_oneshot");
			}
		}
	}

	public void Go(float from, float to, float time)
	{
		m_fFrom = from;
		m_fTo = to;
		m_fRateSpeed = 1f / time;
		m_fRate = 0f;
		m_bHop = true;
		if (mLabel != null)
		{
			mLabel.text = m_fFrom.ToString();
		}
		m_nLastNumber = (int)m_fFrom;
	}

	public void Stop()
	{
		m_bHop = false;
		if (mLabel != null)
		{
			mLabel.text = m_fTo.ToString();
		}
	}
}
