using UnityEngine;

public class gyUISkillButton : MonoBehaviour
{
	public UIFilledSprite mMask;

	public UISprite mIcon;

	public UISprite mIconAnim;

	public UISprite mMutiplyFlag;

	protected bool m_bCD;

	protected float m_fTime;

	protected float m_fTimeCount;

	public bool m_bPause { get; set; }

	private void Awake()
	{
		mMask.fillAmount = 0f;
		mMutiplyFlag.enabled = false;
		m_bPause = false;
	}

	private void Update()
	{
		if (m_bCD && !m_bPause)
		{
			m_fTimeCount += Time.deltaTime;
			if (m_fTimeCount >= m_fTime)
			{
				FinishCD();
			}
			else
			{
				mMask.fillAmount = 1f - m_fTimeCount / m_fTime;
			}
		}
	}

	public void SetIcon(string str)
	{
		if (!(mMask == null) && !(mIcon == null))
		{
			mMask.spriteName = str;
			mIcon.spriteName = str;
			mIconAnim.spriteName = str;
		}
	}

	public void SetCD(float fTime)
	{
		m_bCD = true;
		m_fTime = fTime;
		m_fTimeCount = 0f;
		mMask.fillAmount = 1f;
	}

	public void FinishCD()
	{
		m_bCD = false;
		mMask.fillAmount = 0f;
		TweenAlpha tweenAlpha = TweenAlpha.Begin(mIconAnim.gameObject, 0.5f, 0f);
		tweenAlpha.from = 1f;
		tweenAlpha.to = 0f;
		TweenScale tweenScale = TweenScale.Begin(mIconAnim.gameObject, 0.5f, Vector3.zero);
		tweenScale.from = mIcon.transform.localScale;
		tweenScale.to = tweenScale.from * 2f;
	}

	public void SetMutiplyFlag(bool bShow)
	{
		if (!(mMutiplyFlag == null))
		{
			mMutiplyFlag.enabled = bShow;
		}
	}
}
