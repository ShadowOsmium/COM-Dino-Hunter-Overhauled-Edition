using System.Collections.Generic;
using UnityEngine;

public class iRoadSignPath : MonoBehaviour
{
	public GameObject mPrefab;

	protected List<iRoadSign> m_ltRoadSign;

	protected int m_nIndex;

	protected bool m_bActive;

	protected float m_fTime;

	protected float m_fTimeCount;

	protected bool m_bDestroyWhenFinish;

	protected float m_fDelayTime;

	private void Awake()
	{
		m_ltRoadSign = new List<iRoadSign>();
		m_nIndex = 0;
		m_bActive = false;
		m_fTime = 0.05f;
		m_fTimeCount = 0f;
		m_bDestroyWhenFinish = false;
		m_fDelayTime = 0f;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (!m_bActive || m_ltRoadSign.Count < 1)
		{
			return;
		}
		if (m_fDelayTime > 0f)
		{
			m_fDelayTime -= Time.deltaTime;
			if (m_fDelayTime <= 0f)
			{
				m_fDelayTime = 0f;
				m_bDestroyWhenFinish = true;
			}
		}
		m_fTimeCount += Time.deltaTime;
		if (m_fTimeCount < m_fTime)
		{
			return;
		}
		m_fTimeCount = 0f;
		if (m_nIndex >= 0 && m_nIndex < m_ltRoadSign.Count)
		{
			m_ltRoadSign[m_nIndex].Go();
		}
		m_nIndex++;
		if (m_nIndex >= m_ltRoadSign.Count)
		{
			if (m_bDestroyWhenFinish)
			{
				m_bActive = false;
				Destroy();
			}
			else
			{
				m_nIndex = 0;
			}
		}
	}

	public void Initialize(List<Vector3> ltPath)
	{
		GameObject gameObject = null;
		for (int i = 0; i < ltPath.Count; i++)
		{
			GameObject gameObject2 = Object.Instantiate(mPrefab) as GameObject;
			if (gameObject2 == null)
			{
				break;
			}
			gameObject2.transform.parent = base.transform;
			gameObject2.transform.position = ltPath[i] + new Vector3(0f, 0.5f, 0f);
			iRoadSign component = gameObject2.GetComponent<iRoadSign>();
			if (component == null)
			{
				Object.Destroy(gameObject2);
				continue;
			}
			if (gameObject != null)
			{
				gameObject2.transform.forward = gameObject2.transform.position - gameObject.transform.position;
			}
			gameObject = gameObject2;
			m_ltRoadSign.Add(component);
		}
		Go();
	}

	public void Destroy()
	{
		foreach (iRoadSign item in m_ltRoadSign)
		{
			Object.Destroy(item.gameObject);
		}
		m_ltRoadSign.Clear();
	}

	public void DestroyWhenFinish(float delaytime = 0f)
	{
		if (delaytime <= 0f)
		{
			m_bDestroyWhenFinish = true;
		}
		else
		{
			m_fDelayTime = delaytime;
		}
	}

	public void Go()
	{
		m_nIndex = 0;
		m_fTimeCount = 0f;
		m_bActive = true;
	}
}
