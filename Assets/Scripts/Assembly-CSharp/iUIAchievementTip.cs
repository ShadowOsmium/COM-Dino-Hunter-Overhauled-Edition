using UnityEngine;

public class iUIAchievementTip : MonoBehaviour
{
	protected enum kState
	{
		None,
		SlashIn,
		TextIn,
		StarIn,
		StarAnim,
		Hold,
		SlashOut,
		End
	}

	public iUIAchievementStar mUIAchievementStar;

	public UILabel mLabel;

	public UISprite mBackGround;

	protected kState m_State;

	protected float m_fTime;

	protected float m_fTimeCount;

	protected bool m_bActive;

	protected UIAnchor mAnchor;

	protected int m_nStarCount;

	protected int m_nStar;

	public bool isActive
	{
		get
		{
			return m_bActive;
		}
	}

	private void Awake()
	{
		m_bActive = false;
		mAnchor = NGUITools.FindInParents<UIAnchor>(base.gameObject);
		base.gameObject.SetActiveRecursively(false);
	}

	private void Update()
	{
		if (!m_bActive)
		{
			return;
		}
		switch (m_State)
		{
		case kState.SlashIn:
			m_fTimeCount += Time.deltaTime;
			if (!(m_fTimeCount < m_fTime))
			{
				m_State = kState.TextIn;
				m_fTime = 0.2f;
				m_fTimeCount = 0f;
				mLabel.gameObject.SetActiveRecursively(true);
				TweenScale tweenScale2 = TweenScale.Begin(mLabel.gameObject, m_fTime, Vector3.zero);
				if (tweenScale2 != null)
				{
					tweenScale2.from = Vector3.zero;
					tweenScale2.to = new Vector3(25f, 30f);
					tweenScale2.method = UITweener.Method.EaseIn;
				}
			}
			break;
		case kState.TextIn:
			m_fTimeCount += Time.deltaTime;
			if (!(m_fTimeCount < m_fTime))
			{
				m_State = kState.StarIn;
				m_fTime = 0.2f;
				m_fTimeCount = 0f;
				mUIAchievementStar.gameObject.SetActiveRecursively(true);
				mUIAchievementStar.Initialize(m_nStarCount);
				TweenScale tweenScale3 = TweenScale.Begin(mUIAchievementStar.gameObject, m_fTime, Vector3.zero);
				if (tweenScale3 != null)
				{
					tweenScale3.from = Vector3.zero;
					tweenScale3.to = Vector3.one;
					tweenScale3.method = UITweener.Method.EaseIn;
				}
			}
			break;
		case kState.StarIn:
			m_fTimeCount += Time.deltaTime;
			if (!(m_fTimeCount < m_fTime))
			{
				m_State = kState.StarAnim;
				m_fTime = 0.2f;
				m_fTimeCount = 0f;
			}
			break;
		case kState.StarAnim:
			m_fTimeCount += Time.deltaTime;
			if (!(m_fTimeCount < m_fTime))
			{
				if (m_nStarCount >= m_nStar)
				{
					m_State = kState.Hold;
					m_fTime = 1f;
					m_fTimeCount = 0f;
					break;
				}
				m_State = kState.StarAnim;
				m_fTime = 0.2f;
				m_fTimeCount = 0f;
				m_nStarCount++;
				mUIAchievementStar.SetStar(m_nStarCount);
				CUISound.GetInstance().Play("UI_Levelup");
			}
			break;
		case kState.Hold:
			m_fTimeCount += Time.deltaTime;
			if (!(m_fTimeCount < m_fTime))
			{
				m_State = kState.SlashOut;
				m_fTime = 0.5f;
				m_fTimeCount = 0f;
			}
			break;
		case kState.SlashOut:
			m_fTimeCount += Time.deltaTime;
			if (!(m_fTimeCount < m_fTime))
			{
				m_State = kState.End;
				m_fTime = 0.2f;
				m_fTimeCount = 0f;
				mUIAchievementStar.gameObject.SetActiveRecursively(false);
				mLabel.gameObject.SetActiveRecursively(false);
				TweenScale tweenScale = TweenScale.Begin(mBackGround.gameObject, m_fTime, Vector3.zero);
				if (tweenScale != null)
				{
					tweenScale.from = new Vector3(Screen.width, 40f);
					tweenScale.to = Vector3.zero;
					tweenScale.method = UITweener.Method.EaseOut;
				}
			}
			break;
		case kState.End:
			m_fTimeCount += Time.deltaTime;
			if (!(m_fTimeCount < m_fTime))
			{
				base.gameObject.SetActiveRecursively(false);
				m_bActive = false;
			}
			break;
		}
	}

	public void ShowTip(string sTip, int nStar)
	{
		m_bActive = true;
		base.gameObject.SetActiveRecursively(false);
		base.gameObject.active = true;
		base.transform.localPosition = new Vector3(0f, -109f, base.transform.localPosition.z);
		m_nStar = nStar;
		m_nStarCount = ((nStar > 1) ? (nStar - 1) : 0);
		mLabel.text = sTip;
		mBackGround.gameObject.SetActiveRecursively(true);
		TweenScale tweenScale = TweenScale.Begin(mBackGround.gameObject, 0.2f, Vector3.zero);
		if (tweenScale != null)
		{
			tweenScale.from = Vector3.zero;
			tweenScale.to = new Vector3(Screen.width, 40f);
			tweenScale.method = UITweener.Method.EaseIn;
		}
		m_State = kState.SlashIn;
		m_fTime = 0.2f;
		m_fTimeCount = 0f;
	}
}
