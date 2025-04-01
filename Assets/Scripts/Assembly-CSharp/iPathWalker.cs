using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class iPathWalker : MonoBehaviour
{
	protected enum kState
	{
		None,
		Normal,
		WaitEnd
	}

	public float fOffSetY = 5f;

	public float fMoveSpeedMin = 50f;

	public float fMoveSpeedMax = 60f;

	public bool isLoop;

	protected kState m_State;

	[SerializeField]
	protected List<Vector3> m_ltPath;

	protected int m_nPathIndex;

	protected Transform m_Transform;

	protected TrailRenderer m_TrailRenderer;

	protected Vector3 m_v3SrcPosition;

	protected float m_fTime;

	protected float m_fTimeCount;

	protected float m_fMoveSpeed;

	private void Awake()
	{
		m_ltPath = new List<Vector3>();
		m_Transform = base.transform;
		m_TrailRenderer = GetComponent<TrailRenderer>();
		if (m_TrailRenderer != null)
		{
			m_TrailRenderer.enabled = false;
		}
		m_State = kState.None;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (m_State == kState.None)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		switch (m_State)
		{
		case kState.Normal:
		{
			Vector3 normalized = (m_ltPath[m_nPathIndex] - m_v3SrcPosition).normalized;
			float num = m_fMoveSpeed * deltaTime;
			if (num >= Vector3.Distance(m_ltPath[m_nPathIndex], m_v3SrcPosition))
			{
				m_v3SrcPosition = m_ltPath[m_nPathIndex];
				m_Transform.position = m_v3SrcPosition + new Vector3(0f, fOffSetY, 0f);
				m_nPathIndex++;
				if (m_nPathIndex >= m_ltPath.Count)
				{
					Stop();
				}
			}
			else
			{
				m_v3SrcPosition += normalized * num;
				m_Transform.position = m_v3SrcPosition + new Vector3(0f, fOffSetY, 0f);
			}
			break;
		}
		case kState.WaitEnd:
			m_fTimeCount += deltaTime;
			if (m_fTimeCount >= m_fTime)
			{
				if (m_TrailRenderer != null)
				{
					m_TrailRenderer.enabled = false;
				}
				if (isLoop)
				{
					Go();
					break;
				}
				m_State = kState.None;
				Object.Destroy(base.gameObject);
			}
			break;
		}
	}

	public void Initialize(List<Vector3> ltPath)
	{
		m_ltPath.Clear();
		for (int i = 0; i < ltPath.Count; i++)
		{
			m_ltPath.Add(ltPath[i]);
		}
	}

	public void Go()
	{
		m_State = kState.Normal;
		m_nPathIndex = 0;
		m_v3SrcPosition = m_ltPath[m_nPathIndex];
		m_Transform.position = m_v3SrcPosition + new Vector3(0f, fOffSetY, 0f);
		if (m_TrailRenderer != null)
		{
			m_TrailRenderer.enabled = true;
		}
		m_fMoveSpeed = Random.Range(fMoveSpeedMin, fMoveSpeedMax);
	}

	public void Stop()
	{
		m_State = kState.WaitEnd;
		if (m_TrailRenderer != null)
		{
			m_fTime = m_TrailRenderer.time;
		}
		else
		{
			m_fTime = 0f;
		}
		m_fTimeCount = 0f;
	}
}
