using UnityEngine;

public class gyUIPanelMissionSuccess : MonoBehaviour
{
	public GameObject mLightBase;

	public GameObject mLightAnim;

	public GameObject mTitleText;

	public GameObject mTitleIcon;

	public GameObject mStatisticsBackground;

	public gyUIResultExpBar m_CharExp;

	public gyUIHopNumber mContext1;

	public gyUIHopNumber mContext2;

	protected bool m_bShow;

	protected int m_nStep;

	protected float m_fStepCount;

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
		if (!m_bShow)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		switch (m_nStep)
		{
		case 0:
			m_fStepCount -= deltaTime;
			if (m_fStepCount <= 0f)
			{
				mLightAnim.SetActiveRecursively(true);
				mTitleText.SetActiveRecursively(true);
				TweenPosition tweenPosition = TweenPosition.Begin(mTitleText, 0.5f, Vector3.zero);
				tweenPosition.from = new Vector3(3f, 260f, 0f);
				tweenPosition.to = new Vector3(3f, 79f, 0f);
				tweenPosition.method = UITweener.Method.BounceIn;
				m_nStep = 1;
				m_fStepCount = 0.2f;
			}
			break;
		case 1:
			m_fStepCount -= deltaTime;
			if (m_fStepCount <= 0f)
			{
				mTitleIcon.SetActiveRecursively(true);
				TweenPosition tweenPosition2 = TweenPosition.Begin(mTitleIcon, 0.5f, Vector3.zero);
				tweenPosition2.from = new Vector3(-120f, 260f, 0f);
				tweenPosition2.to = new Vector3(-120f, 83f, 0f);
				tweenPosition2.method = UITweener.Method.EaseIn;
				m_nStep = 2;
				m_fStepCount = 0.2f;
			}
			break;
		case 2:
			m_fStepCount -= deltaTime;
			if (m_fStepCount <= 0f)
			{
				mStatisticsBackground.SetActiveRecursively(true);
				m_nStep = 3;
				m_fStepCount = 0.2f;
			}
			break;
		}
	}

	public void Show(bool bShow)
	{
		m_bShow = bShow;
		base.gameObject.SetActiveRecursively(bShow);
		mLightBase.SetActiveRecursively(false);
		mLightAnim.SetActiveRecursively(false);
		mTitleText.SetActiveRecursively(false);
		mTitleIcon.SetActiveRecursively(false);
		if (bShow)
		{
			base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		}
		else
		{
			base.transform.localPosition = new Vector3(10000f, 10000f, base.transform.localPosition.z);
		}
		if (bShow)
		{
			mLightBase.SetActiveRecursively(true);
			TweenScale tweenScale = TweenScale.Begin(mLightBase, 0.5f, Vector3.zero);
			tweenScale.from = Vector3.zero;
			tweenScale.to = Vector3.one;
			tweenScale.method = UITweener.Method.EaseIn;
			m_nStep = 0;
			m_fStepCount = 0.5f;
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

	public void SetGainCrystal(int nValue)
	{
		if (!(mContext1 == null))
		{
			mContext1.Go(0f, nValue, Mathf.Clamp01((float)nValue / 300f) * 5f);
		}
	}

	public void SetGainGold(int nValue)
	{
		if (!(mContext2 == null))
		{
			mContext2.Go(0f, nValue, Mathf.Clamp01((float)nValue / 300f) * 5f);
		}
	}

	public bool IsContextHop()
	{
		if (mContext1 == null || !mContext1.gameObject.active || !mContext1.isHop)
		{
			return false;
		}
		if (mContext2 == null || !mContext2.gameObject.active || !mContext2.isHop)
		{
			return false;
		}
		return true;
	}

	public void StopContextHop()
	{
		if (mContext1 != null)
		{
			mContext1.Stop();
		}
		if (mContext2 != null)
		{
			mContext2.Stop();
		}
	}
}
