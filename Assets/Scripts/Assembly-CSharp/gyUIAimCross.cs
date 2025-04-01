using UnityEngine;

public class gyUIAimCross : MonoBehaviour
{
	public float Min = 7f;

	public float Max = 20f;

	public float fExpand = 10f;

	public float fRecover = 10f;

	protected float m_fDisBase;

	protected float m_fCurDis;

	protected float m_fRate;

	protected float m_fRecoverSpeed;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		if (m_fRate < 1f)
		{
			m_fRate += m_fRecoverSpeed * deltaTime;
			m_fCurDis = Lerp(m_fCurDis, m_fDisBase, m_fRate);
			if (m_fRate > 1f)
			{
				m_fRate = 1f;
			}
			SetArrow(m_fCurDis);
		}
	}

	protected virtual void SetArrow(float fDis)
	{
	}

	protected float Lerp(float src, float dst, float rate)
	{
		if (rate >= 1f)
		{
			return dst;
		}
		if (rate <= 0f)
		{
			return src;
		}
		return src + (dst - src) * rate;
	}

	public void Initialize(float fPrecise)
	{
		fPrecise = Mathf.Clamp01(fPrecise);
		if (fPrecise == 0f)
		{
			fPrecise = 0.1f;
		}
		m_fDisBase = Min;
		m_fCurDis = m_fDisBase;
		m_fRecoverSpeed = 1f / ((Max - Min) / (fRecover * fPrecise));
		m_fRate = 1f;
		SetArrow(m_fCurDis);
	}

	public void Expand()
	{
		m_fCurDis += fExpand;
		if (m_fCurDis > Max)
		{
			m_fCurDis = Max;
		}
		m_fRate = 0f;
	}
}
