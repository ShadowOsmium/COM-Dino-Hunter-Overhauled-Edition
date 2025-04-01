using UnityEngine;

public class CMSEffectManager
{
	public class CMSEffectTime
	{
		public float m_fTime;

		public GameObject m_Effect;
	}

	protected static CMSEffectManager m_Instance;

	protected int m_nEffectNumber = 20;

	protected CMSEffectTime[] m_arrEffect;

	public CMSEffectManager()
	{
		m_arrEffect = new CMSEffectTime[m_nEffectNumber];
	}

	public static CMSEffectManager GetInstance()
	{
		if (m_Instance == null)
		{
			m_Instance = new CMSEffectManager();
		}
		return m_Instance;
	}

	public void Initialize()
	{
	}

	public void Destroy()
	{
		for (int i = 0; i < m_arrEffect.Length; i++)
		{
		}
	}

	public void Update(float deltaTime)
	{
		for (int i = 0; i < m_arrEffect.Length; i++)
		{
			if (m_arrEffect[i] == null)
			{
				continue;
			}
			if (m_arrEffect[i].m_Effect == null)
			{
				m_arrEffect[i] = null;
				continue;
			}
			m_arrEffect[i].m_fTime -= deltaTime;
			if (!(m_arrEffect[i].m_fTime > 0f))
			{
				ClearEffect(m_arrEffect[i].m_Effect, 5f);
				m_arrEffect[i] = null;
			}
		}
	}

	public void ClearEffect(GameObject effect, float fDelay = 0f)
	{
		if (effect == null)
		{
			return;
		}
		if (fDelay > 0f)
		{
			ParticleSystem[] componentsInChildren = effect.GetComponentsInChildren<ParticleSystem>();
			if (componentsInChildren != null)
			{
				ParticleSystem[] array = componentsInChildren;
				foreach (ParticleSystem particleSystem in array)
				{
					particleSystem.enableEmission = false;
				}
			}
			Object.Destroy(effect, fDelay);
		}
		else
		{
			Object.Destroy(effect);
		}
	}

	public int PlayEffect(GameObject prefab, Vector3 v3Pos, Vector3 v3Dir, float fTime = 0f, Transform parent = null)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(prefab);
		if (gameObject == null)
		{
			return -1;
		}
		if (parent != null)
		{
			gameObject.transform.parent = parent;
			gameObject.transform.localPosition = v3Pos;
			gameObject.transform.forward = v3Dir;
		}
		else
		{
			gameObject.transform.position = v3Pos;
			gameObject.transform.forward = v3Dir;
		}
		if (fTime > 0f)
		{
			Object.Destroy(gameObject, fTime);
			return -1;
		}
		return 1;
	}
}
