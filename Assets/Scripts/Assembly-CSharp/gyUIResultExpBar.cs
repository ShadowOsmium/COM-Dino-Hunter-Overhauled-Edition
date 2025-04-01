using UnityEngine;

public class gyUIResultExpBar : MonoBehaviour
{
	public float m_fAnimationSpeed = 1f;

	public UIFilledSprite m_ExpBar;

	public UILabel m_Label;

	protected bool m_bInAnimation;

	protected bool m_bIncrease;

	protected int m_nAnimationCount;

	protected float m_fCurValue;

	protected float m_fSrcValue;

	protected float m_fDstValue;

	protected int m_nLevel;

	public bool IsAnim
	{
		get
		{
			return m_bInAnimation;
		}
	}

	public float BarValue
	{
		get
		{
			return m_fCurValue;
		}
		set
		{
			m_fCurValue = value;
			if (!(m_ExpBar == null))
			{
				m_ExpBar.fillAmount = m_fCurValue;
			}
		}
	}

	public int Level
	{
		get
		{
			return m_nLevel;
		}
		set
		{
			m_nLevel = value;
			if (m_Label != null)
			{
				m_Label.text = m_nLevel.ToString();
			}
		}
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (!m_bInAnimation)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		if (m_bIncrease)
		{
			m_fCurValue += m_fAnimationSpeed * deltaTime;
			if (m_nAnimationCount == 0)
			{
				if (m_fCurValue >= m_fDstValue)
				{
					m_fCurValue = m_fDstValue;
					m_bInAnimation = false;
				}
			}
			else if (m_fCurValue >= 1f)
			{
				m_fCurValue = 0f;
				m_nAnimationCount--;
				Level++;
			}
		}
		else
		{
			m_fCurValue -= m_fAnimationSpeed * deltaTime;
			if (m_nAnimationCount == 0)
			{
				if (m_fCurValue <= m_fDstValue)
				{
					m_fCurValue = m_fDstValue;
					m_bInAnimation = false;
				}
			}
			else if (m_fCurValue <= 0f)
			{
				m_fCurValue = 1f;
				m_nAnimationCount--;
				Level--;
			}
		}
		BarValue = m_fCurValue;
	}

	public void Show(bool bShow)
	{
		base.gameObject.SetActiveRecursively(bShow);
	}

	public void SetAnimation(float fCurRate, int nCurLevel)
	{
		m_bInAnimation = true;
		float num = fCurRate - BarValue;
		int num2 = nCurLevel - Level;
		m_bIncrease = num2 >= 0 && (num2 > 0 || !(num < 0f));
		m_nAnimationCount = Mathf.Abs(num2);
		m_fSrcValue = BarValue;
		m_fDstValue = fCurRate;
	}
}
