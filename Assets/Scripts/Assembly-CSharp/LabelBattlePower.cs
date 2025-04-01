using UnityEngine;

[RequireComponent(typeof(TUILabel))]
public class LabelBattlePower : MonoBehaviour
{
	protected TUILabel m_Label;

	protected bool m_bActive;

	protected int m_nValue;

	protected float m_fTime;

	protected float m_fTimeCount;

	protected TweenScale m_TweenObject;

	protected int m_nDelayValue;

	protected float m_fDelay;

	protected float m_fDelayCount;

	private void Awake()
	{
		m_Label = GetComponent<TUILabel>();
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (!m_bActive)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		if (m_nDelayValue > 0)
		{
			m_fDelayCount += deltaTime;
			if (m_fDelayCount >= m_fDelay)
			{
				Go(m_nDelayValue);
				m_nDelayValue = 0;
			}
		}
		else
		{
			m_fTimeCount += deltaTime;
			if (m_fTimeCount >= m_fTime)
			{
				m_bActive = false;
			}
		}
	}

	public void Set(int nValue, bool bAnim, float fDelay = 0f)
	{
		if (!(m_Label == null))
		{
			m_bActive = true;
			if (fDelay <= 0f)
			{
				Go(nValue);
				return;
			}
			m_nDelayValue = nValue;
			m_fDelay = fDelay;
			m_fDelayCount = 0f;
		}
	}

	public void Go(int nValue)
	{
		if (!(m_Label == null))
		{
			m_Label.Text = nValue.ToString();
			m_nValue = nValue;
			m_fTime = 0.5f;
			m_fTimeCount = 0f;
			m_TweenObject = TweenScale.Begin(base.gameObject, m_fTime, Vector3.one);
			m_TweenObject.from = Vector3.one * 2f;
			m_TweenObject.to = Vector3.one;
			m_TweenObject.method = UITweener.Method.BounceIn;
		}
	}
}
