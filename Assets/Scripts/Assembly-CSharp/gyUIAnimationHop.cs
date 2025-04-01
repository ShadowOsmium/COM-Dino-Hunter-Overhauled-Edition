using UnityEngine;

public class gyUIAnimationHop : MonoBehaviour
{
	public float m_fHopDis = 1f;

	public float m_fHopTime = 1f;

	public bool isLoop;

	public Transform mTranform;

	protected bool m_bActive;

	protected Vector3 m_v3Pos;

	protected Vector3 m_v3Dir;

	protected int m_nStep;

	protected float m_fStepCount;

	protected TweenPosition m_TweenPosition;

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
		m_v3Pos = mTranform.localPosition;
		m_v3Dir = mTranform.forward;
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
		switch (m_nStep)
		{
		case 0:
			m_fStepCount -= Time.deltaTime;
			if (!(m_fStepCount > 0f))
			{
				m_nStep = 1;
				m_fStepCount = m_fHopTime / 2f;
				m_TweenPosition = TweenPosition.Begin(base.gameObject, m_fStepCount, Vector3.zero);
				if (m_TweenPosition != null)
				{
					m_TweenPosition.to = m_v3Pos;
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
					Go(m_v3Dir);
				}
			}
			break;
		}
	}

	public void Go(Vector3 v3Dir)
	{
		m_bActive = true;
		m_v3Pos = mTranform.localPosition;
		m_v3Dir = v3Dir.normalized;
		m_nStep = 0;
		m_fStepCount = m_fHopTime / 2f;
		m_TweenPosition = TweenPosition.Begin(base.gameObject, m_fStepCount, Vector3.zero);
		if (m_TweenPosition != null)
		{
			m_TweenPosition.from = m_v3Pos;
			m_TweenPosition.to = m_v3Pos + m_v3Dir * m_fHopDis;
		}
	}

	public void Stop()
	{
		m_bActive = false;
		mTranform.localPosition = m_v3Pos;
		if (m_TweenPosition != null)
		{
			m_TweenPosition.enabled = false;
		}
	}
}
