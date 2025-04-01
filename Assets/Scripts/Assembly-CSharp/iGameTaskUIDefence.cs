using gyTaskSystem;
using UnityEngine;

public class iGameTaskUIDefence : iGameTaskUIBase
{
	public UIFilledSprite mSpritePrecents;

	public UILabel mLabelCurWave;

	public UILabel mLabelMaxWave;

	protected float m_fLabelCurWaveScale;

	protected CTaskDefence m_TaskDefence;

	private void Awake()
	{
		base.Height = 25f;
		m_fLabelCurWaveScale = mLabelCurWave.transform.localScale.x;
	}

	public override void UpdateTask(float deltaTime)
	{
		if (m_TaskDefence != null)
		{
			if (m_TaskTime != null)
			{
				m_TaskTime.SetTime(m_TaskDefence.TaskTime);
			}
			if (m_TaskDefence.isUpdateData)
			{
				m_TaskDefence.isUpdateData = false;
				SetCurNum(m_TaskDefence.m_nCurWave);
			}
			if (m_TaskDefence.isLifeChange)
			{
				m_TaskDefence.isLifeChange = false;
				SetPrecents(m_TaskDefence.m_fCurLife / m_TaskDefence.m_fMaxLife);
			}
		}
	}

	public override void InitTask(CTaskBase taskbase)
	{
		m_TaskDefence = m_curTaskBase as CTaskDefence;
		if (m_TaskDefence != null)
		{
			SetCurNum(m_TaskDefence.m_nCurWave);
			SetMaxNum(m_TaskDefence.m_nMaxWave);
			SetPrecents(m_TaskDefence.m_fCurLife / m_TaskDefence.m_fMaxLife);
			AddTimeLimitUI(m_TaskDefence.TaskTime);
		}
	}

	public void SetPrecents(float fPrecents)
	{
		if (!(mSpritePrecents == null))
		{
			mSpritePrecents.fillAmount = fPrecents;
		}
	}

	public void SetCurNum(int nNum)
	{
		if (!(mLabelCurWave == null))
		{
			mLabelCurWave.text = nNum.ToString();
			TweenScale tweenScale = TweenScale.Begin(mLabelCurWave.gameObject, 0.5f, Vector3.zero);
			tweenScale.from = new Vector3(m_fLabelCurWaveScale, m_fLabelCurWaveScale, 0f) * 1.5f;
			tweenScale.to = new Vector3(m_fLabelCurWaveScale, m_fLabelCurWaveScale, 0f);
		}
	}

	public void SetMaxNum(int nNum)
	{
		if (!(mLabelMaxWave == null))
		{
			mLabelMaxWave.text = nNum.ToString();
		}
	}
}
