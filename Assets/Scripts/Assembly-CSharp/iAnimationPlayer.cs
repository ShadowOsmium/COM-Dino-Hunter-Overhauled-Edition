using UnityEngine;

public class iAnimationPlayer : MonoBehaviour
{
	public Animation m_Animation;

	public string m_sName = string.Empty;

	public bool m_bAutoPlay;

	public float m_fSpeed = 1f;

	public int m_nLoop;

	protected float m_fAnimTime;

	protected float m_fAnimTimeCount;

	protected int m_nLoopCount;

	private void Awake()
	{
	}

	private void Start()
	{
		if (m_bAutoPlay)
		{
			m_fAnimTime = Play(m_fSpeed, m_nLoop);
			m_fAnimTimeCount = 0f;
		}
	}

	private void Update()
	{
		if (!(m_fAnimTime > 0f))
		{
			return;
		}
		m_fAnimTimeCount += Time.deltaTime;
		if (m_fAnimTimeCount >= m_fAnimTime)
		{
			if (m_nLoopCount >= m_nLoop)
			{
				m_fAnimTime = 0f;
				return;
			}
			m_nLoopCount++;
			m_fAnimTime = Play(m_fSpeed);
			m_fAnimTimeCount = 0f;
		}
	}

	public float Play(float speed = 1f, int nLoop = 0)
	{
		if (m_Animation == null || m_sName.Length < 1)
		{
			return 0f;
		}
		if (m_Animation[m_sName] == null)
		{
			return 0f;
		}
		m_Animation[m_sName].speed = speed;
		m_Animation[m_sName].time = 0f;
		m_Animation.Play(m_sName);
		if (nLoop > 0)
		{
			m_nLoop = nLoop;
			m_nLoopCount = 0;
		}
		return m_Animation[m_sName].length / speed;
	}
}
