using UnityEngine;

public class gyUIPanelTool : gyUICellPanel
{
	public float fRemainTime = 2f;

	protected bool m_bMoveIn;

	protected float m_fTimeCount;

	protected Collider[] m_arrCollider;

	public bool IsMoveIn
	{
		get
		{
			return m_bMoveIn;
		}
	}

	public new void Awake()
	{
		base.Awake();
		m_bMoveIn = false;
		if (m_arrCell != null && m_arrCell.Length > 0)
		{
			m_arrCollider = new Collider[m_arrCell.Length];
			for (int i = 0; i < m_arrCell.Length; i++)
			{
				m_arrCollider[i] = m_arrCell[i].transform.GetComponent<Collider>();
				if (m_arrCollider[i] != null)
				{
					m_arrCollider[i].enabled = false;
				}
			}
		}
		RegisterOnClickCell(RemainTime);
	}

	private void Start()
	{
		MoveOut();
	}

	private void Update()
	{
		if (m_bMoveIn)
		{
			m_fTimeCount += Time.deltaTime;
			if (!(m_fTimeCount < fRemainTime))
			{
				MoveOut();
			}
		}
	}

	public void MoveIn()
	{
		m_bMoveIn = true;
		m_fTimeCount = 0f;
		TweenPosition tweenPosition = TweenPosition.Begin(base.gameObject, 0.5f, Vector3.zero);
		tweenPosition.to = new Vector3(0f, 27f, 0f);
		tweenPosition.method = UITweener.Method.BounceIn;
		for (int i = 0; i < m_arrCollider.Length; i++)
		{
			if (m_arrCollider[i] != null)
			{
				m_arrCollider[i].enabled = true;
			}
		}
	}

	public void MoveOut()
	{
		m_bMoveIn = false;
		TweenPosition tweenPosition = TweenPosition.Begin(base.gameObject, 0.5f, Vector3.zero);
		tweenPosition.to = new Vector3(0f, -10f, 0f);
		tweenPosition.method = UITweener.Method.EaseOut;
		for (int i = 0; i < m_arrCollider.Length; i++)
		{
			if (m_arrCollider[i] != null)
			{
				m_arrCollider[i].enabled = false;
			}
		}
	}

	public void RemainTime(int nIndex)
	{
		if (m_bMoveIn)
		{
			m_fTimeCount = 0f;
		}
	}
}
