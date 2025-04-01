using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(gyUIPoolObject))]
public class iGoldEmitter : MonoBehaviour
{
	protected int[] m_arrGoldValue = new int[4] { 1, 5, 10, 50 };

	protected float[] m_arrGoldSize = new float[4] { 0.5f, 1f, 2f, 3f };

	protected int m_nGold;

	protected bool m_bCrystal;

	protected List<int> m_ltGoldEmitter;

	protected float m_fInterval = 0.2f;

	protected float m_fTimeCount;

	protected gyUIPoolObject m_PoolObject;

	private void Awake()
	{
		m_ltGoldEmitter = new List<int>();
		m_PoolObject = GetComponent<gyUIPoolObject>();
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (m_ltGoldEmitter.Count < 1)
		{
			m_PoolObject.TakeBack(0f);
			return;
		}
		m_fTimeCount += Time.deltaTime;
		if (!(m_fTimeCount < m_fInterval))
		{
			m_fTimeCount = 0f;
			SpawnGold(m_ltGoldEmitter[0]);
			m_ltGoldEmitter.RemoveAt(0);
		}
	}

	public void Initialize(int nGold, bool iscrystal = false)
	{
		base.gameObject.SetActiveRecursively(true);
		m_nGold = nGold;
		m_bCrystal = iscrystal;
		m_ltGoldEmitter.Clear();
		int num = m_arrGoldValue.Length - 1;
		while (nGold > 0)
		{
			if (nGold < m_arrGoldValue[num] && num > 0)
			{
				num--;
			}
			else if (nGold < m_arrGoldValue[num])
			{
				nGold = 0;
				m_ltGoldEmitter.Add(nGold);
			}
			else
			{
				nGold -= m_arrGoldValue[num];
				m_ltGoldEmitter.Add(m_arrGoldValue[num]);
			}
		}
	}

	public void SpawnGold(int nGold)
	{
		int num = 0;
		for (num = 0; num < m_arrGoldValue.Length && nGold > m_arrGoldValue[num]; num++)
		{
		}
		if (num < 0 || num >= m_arrGoldSize.Length)
		{
			return;
		}
		iGameSceneBase gameScene = iGameApp.GetInstance().m_GameScene;
		if (gameScene != null)
		{
			Vector3 onUnitSphere = Random.onUnitSphere;
			onUnitSphere.y = 1f;
			if (m_bCrystal)
			{
				gameScene.AddCrystal(nGold, base.transform.position, onUnitSphere * Random.Range(300f, 500f), m_arrGoldSize[num]);
			}
			else
			{
				gameScene.AddGold(nGold, base.transform.position, onUnitSphere * Random.Range(300f, 500f), m_arrGoldSize[num]);
			}
		}
	}
}
