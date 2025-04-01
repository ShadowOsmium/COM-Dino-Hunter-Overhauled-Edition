using UnityEngine;

public class iBuffData
{
	public int m_nID;

	public int m_nType;

	public int m_nSlot;

	public int m_nPriority;

	public int m_nFromSkill;

	public CBuffInfo m_curBuffInfo;

	public float m_fEffectTimeCount;

	public GameObject m_Effect;

	public string m_sAudio = string.Empty;

	public float m_fTime;

	public void EffectAdd(int nPrefab, Transform parent)
	{
		GameObject gameObject = PrefabManager.Get(nPrefab);
		if (!(gameObject == null))
		{
			GameObject gameObject2 = (GameObject)Object.Instantiate(gameObject);
			if (!(gameObject2 == null))
			{
				gameObject2.transform.parent = parent;
				gameObject2.transform.localPosition = Vector3.zero;
				gameObject2.transform.localRotation = Quaternion.identity;
				Object.Destroy(gameObject2, 5f);
			}
		}
	}

	public void EffectHold(int nPrefab, Transform parent)
	{
		GameObject gameObject = PrefabManager.Get(nPrefab);
		if (!(gameObject == null))
		{
			m_Effect = (GameObject)Object.Instantiate(gameObject);
			if (!(m_Effect == null))
			{
				m_Effect.transform.parent = parent;
				m_Effect.transform.localPosition = Vector3.zero;
				m_Effect.transform.localRotation = Quaternion.identity;
			}
		}
	}

	public void EffectClear()
	{
		if (m_Effect == null)
		{
			return;
		}
		ParticleSystem[] componentsInChildren = m_Effect.GetComponentsInChildren<ParticleSystem>();
		if (componentsInChildren != null && componentsInChildren.Length > 0)
		{
			ParticleSystem[] array = componentsInChildren;
			foreach (ParticleSystem particleSystem in array)
			{
				particleSystem.Stop();
			}
			Object.Destroy(m_Effect, 2f);
		}
		else
		{
			Object.Destroy(m_Effect);
		}
		m_Effect = null;
	}
}
