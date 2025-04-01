using UnityEngine;

public class gyUIPortrait : MonoBehaviour
{
	protected class CLifeColor
	{
		public float m_fLifeColorRate;

		public Color m_fLifeColor;

		public CLifeColor(float rate, Color color)
		{
			m_fLifeColorRate = rate;
			m_fLifeColor = color;
		}
	}

	public GameObject m_Protrait;

	public UISprite m_ProtraitIcon;

	public UILabel m_ProtraitName;

	public gyUILifeBar m_ProtraitLife;

	public gyUILifeBar m_ProtraitExp;

	public UILabel m_ProtraitLevel;

	public UISprite m_ProtraitDeathFlag;

	protected CLifeColor[] m_arrLifeColor;

	private void Awake()
	{
		if (m_Protrait == null)
		{
			m_Protrait = base.gameObject;
		}
		m_arrLifeColor = new CLifeColor[5];
		m_arrLifeColor[0] = new CLifeColor(0.2f, new Color(0.88f, 0.03f, 0f));
		m_arrLifeColor[1] = new CLifeColor(0.4f, new Color(0.96f, 0.5f, 0f));
		m_arrLifeColor[2] = new CLifeColor(0.6f, new Color(0.96f, 1f, 0f));
		m_arrLifeColor[3] = new CLifeColor(0.8f, new Color(0.63f, 0.9f, 0f));
		m_arrLifeColor[4] = new CLifeColor(1f, new Color(0.06f, 0.6f, 0f));
		ShowDeathFlag(false);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Show(bool bShow)
	{
		if (!(m_Protrait == null))
		{
			m_Protrait.SetActiveRecursively(bShow);
		}
	}

	public void SetIcon(string sName)
	{
		if (!(m_ProtraitIcon == null))
		{
			m_ProtraitIcon.spriteName = sName;
		}
	}

	public void SetName(string sName)
	{
		if (!(m_ProtraitName == null))
		{
			m_ProtraitName.text = sName;
		}
	}

	public void SetLife(float fRate)
	{
		if (m_ProtraitLife == null)
		{
			return;
		}
		m_ProtraitLife.SetValue(fRate);
		for (int i = 0; i < m_arrLifeColor.Length; i++)
		{
			if (fRate <= m_arrLifeColor[i].m_fLifeColorRate)
			{
				m_ProtraitLife.SetColor(m_arrLifeColor[i].m_fLifeColor);
				break;
			}
		}
	}

	public void SetExp(float fRate)
	{
		if (!(m_ProtraitExp == null))
		{
			m_ProtraitExp.SetValue(fRate);
		}
	}

	public void SetLevel(string sLevel)
	{
		if (!(m_ProtraitLevel == null))
		{
			m_ProtraitLevel.text = sLevel;
		}
	}

	public void ShowLevelAnim()
	{
		if (!(m_ProtraitLevel == null))
		{
			TweenScale tweenScale = TweenScale.Begin(m_ProtraitLevel.gameObject, 0.5f, Vector3.zero);
			if (tweenScale != null)
			{
				tweenScale.from = m_ProtraitLevel.transform.localScale * 2f;
				tweenScale.to = m_ProtraitLevel.transform.localScale;
				tweenScale.method = UITweener.Method.BounceOut;
			}
		}
	}

	public void ShowDeathFlag(bool bShow)
	{
		if (!(m_ProtraitDeathFlag == null))
		{
			m_ProtraitDeathFlag.enabled = bShow;
		}
	}
}
