using UnityEngine;

public class _AnimController : MonoBehaviour
{
	protected float m_fAnimTime;

	protected float m_fAnimTimeCount;

	private void Start()
	{
	}

	private void Update()
	{
		m_fAnimTimeCount += m_fAnimTime;
		if (m_fAnimTimeCount >= m_fAnimTime)
		{
			m_fAnimTimeCount = m_fAnimTime;
		}
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			PlayRandom();
		}
	}

	public void PlayRandom()
	{
		if (m_fAnimTimeCount < m_fAnimTime)
		{
			return;
		}
		int num = Random.Range(0, base.GetComponent<Animation>().GetClipCount());
		foreach (AnimationState item in base.GetComponent<Animation>())
		{
			if (num == 0)
			{
				item.layer = 0;
				item.time = 0f;
				item.wrapMode = WrapMode.Loop;
				base.GetComponent<Animation>().Stop();
				base.GetComponent<Animation>().Play(item.name);
				m_fAnimTime = item.length;
				m_fAnimTimeCount = 0f;
				break;
			}
			num--;
		}
	}
}
