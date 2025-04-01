using UnityEngine;

public class gyUIAnimationBump : MonoBehaviour
{
	public Vector3 m_v3BumpScale = Vector3.one;

	public float m_fBumpTime = 1f;

	public bool isLoop;

	public Transform mTranform;

	protected bool m_bActive;

	protected Vector3 m_v3Scale;

	protected int m_nStep;

	protected float m_fStepCount;

	protected TweenScale m_TweenScale;

	public bool isActive
	{
		get
		{
			return m_bActive;
		}
	}

	private void Awake()
	{
		if (mTranform == null)
		{
			mTranform = base.transform;
		}
		m_bActive = false;
		m_v3Scale = mTranform.localScale;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			Go();
		}
		if (!m_bActive)
		{
			return;
		}
		switch (m_nStep)
		{
		case 0:
			m_fStepCount -= Time.deltaTime;
			if (!(m_fStepCount > 0f))
			{
				m_nStep = 1;
				m_fStepCount = m_fBumpTime / 2f;
				m_TweenScale = TweenScale.Begin(base.gameObject, m_fStepCount, Vector3.zero);
				if (m_TweenScale != null)
				{
					m_TweenScale.to = m_v3Scale;
				}
			}
			break;
		case 1:
			m_fStepCount -= Time.deltaTime;
			if (!(m_fStepCount > 0f))
			{
				Stop();
				if (isLoop)
				{
					Go();
				}
			}
			break;
		}
	}

	public void Go()
	{
		m_bActive = true;
		m_nStep = 0;
		m_fStepCount = m_fBumpTime / 2f;
		m_TweenScale = TweenScale.Begin(base.gameObject, m_fStepCount, Vector3.zero);
		if (m_TweenScale != null)
		{
			m_TweenScale.from = m_v3Scale;
			m_TweenScale.to = m_v3Scale + m_v3BumpScale;
		}
	}

	public void Stop()
	{
		m_bActive = false;
		mTranform.localScale = m_v3Scale;
		if (m_TweenScale != null)
		{
			m_TweenScale.enabled = false;
		}
	}
}
