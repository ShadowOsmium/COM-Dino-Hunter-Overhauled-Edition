using System.Collections.Generic;
using UnityEngine;

public class iBuilding : MonoBehaviour
{
	public List<iBuildingUnit> m_ltBudingUnit;

	public List<Transform> m_ltAttackPoint;

	public float[] arrLifeRate = new float[3] { 100f, 50f, 0f };

	public int m_nLifeState;

	public float m_fLife;

	public float m_fLifeMax;

	protected TAudioController m_AudioController;

	public bool IsBroken
	{
		get
		{
			return m_nLifeState == arrLifeRate.Length - 1;
		}
	}

	private void Awake()
	{
		m_AudioController = GetComponent<TAudioController>();
		if (m_AudioController == null)
		{
			m_AudioController = base.gameObject.AddComponent<TAudioController>();
		}
		m_nLifeState = 0;
		foreach (Transform item in base.transform)
		{
			if (item.name == "attackpoint")
			{
				m_ltAttackPoint.Add(item);
			}
			else if (item.name == "ZhalanUnit")
			{
				iBuildingUnit component = item.GetComponent<iBuildingUnit>();
				if (component != null)
				{
					m_ltBudingUnit.Add(component);
				}
			}
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Initialize(float fLifeMax)
	{
		m_fLife = fLifeMax;
		m_fLifeMax = fLifeMax;
	}

	public void SetHP(float fLife)
	{
		m_fLife = fLife;
		m_nLifeState = arrLifeRate.Length - 1;
		float num = m_fLife / m_fLifeMax * 100f;
		for (int i = 0; i < arrLifeRate.Length - 1; i++)
		{
			if (num <= arrLifeRate[i] && num > arrLifeRate[i + 1])
			{
				m_nLifeState = i;
				break;
			}
		}
		SetModel(m_nLifeState);
	}

	public void AddHP(float fDmg)
	{
		m_fLife += fDmg;
		if (m_fLife > m_fLifeMax)
		{
			m_fLife = m_fLifeMax;
		}
		else if (m_fLife <= 0f)
		{
			m_fLife = 0f;
		}
		SetHP(m_fLife);
		if (fDmg < 0f)
		{
			PlayColorAnim(new Color(1f, 0f, 0f, 1f), new Color(1f, 1f, 1f, 1f));
		}
	}

	public Vector3 GetRandomPoint()
	{
		if (m_ltAttackPoint == null)
		{
			return base.transform.position;
		}
		return m_ltAttackPoint[Random.Range(0, m_ltAttackPoint.Count)].position;
	}

	protected void SetModel(int nIndex)
	{
		for (int i = 0; i < m_ltBudingUnit.Count; i++)
		{
			if (!(m_ltBudingUnit[i] == null))
			{
				m_ltBudingUnit[i].SetModel(nIndex);
			}
		}
	}

	protected void PlayColorAnim(Color src, Color dst)
	{
		for (int i = 0; i < m_ltBudingUnit.Count; i++)
		{
			if (!(m_ltBudingUnit[i] == null))
			{
				m_ltBudingUnit[i].PlayColorAnim(src, dst);
			}
		}
	}

	public void PlayAudio(string sName)
	{
		if (!(m_AudioController == null))
		{
			m_AudioController.PlayAudio(sName);
		}
	}

	public void StopAudio(string sName)
	{
		if (!(m_AudioController == null))
		{
			m_AudioController.StopAudio(sName);
		}
	}
}
