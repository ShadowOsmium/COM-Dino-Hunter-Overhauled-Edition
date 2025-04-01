using UnityEngine;

public class gyUIStash : MonoBehaviour
{
	public UISprite mIcon;

	public UILabel mValueCur;

	public UILabel mValueMax;

	protected Vector3 m_v3ValueCurScale;

	protected int m_nCur;

	protected int m_nMax;

	private void Awake()
	{
		if (mValueCur != null)
		{
			m_v3ValueCurScale = mValueCur.transform.localScale;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetMax(int nCount, bool bAnim = false)
	{
		if (mValueMax == null)
		{
			return;
		}
		m_nMax = nCount;
		mValueMax.text = nCount.ToString();
		if (bAnim)
		{
			TweenScale tweenScale = TweenScale.Begin(mValueMax.gameObject, 0.5f, Vector3.zero);
			if (tweenScale != null)
			{
				tweenScale.from = m_v3ValueCurScale * 2f;
				tweenScale.to = m_v3ValueCurScale;
				tweenScale.method = UITweener.Method.BounceIn;
			}
		}
		if (m_nCur < m_nMax)
		{
			mValueMax.color = new Color(255f, 199f, 0f);
		}
		else
		{
			mValueMax.color = Color.red;
		}
	}

	public void SetCur(int nCount, bool bAnim = false)
	{
		if (mValueCur == null)
		{
			return;
		}
		m_nCur = nCount;
		mValueCur.text = nCount.ToString();
		if (bAnim)
		{
			TweenScale tweenScale = TweenScale.Begin(mValueCur.gameObject, 0.5f, Vector3.zero);
			if (tweenScale != null)
			{
				tweenScale.from = m_v3ValueCurScale * 2f;
				tweenScale.to = m_v3ValueCurScale;
				tweenScale.method = UITweener.Method.BounceIn;
			}
		}
		mValueCur.color = new Color(255f, 199f, 0f);
	}
}
