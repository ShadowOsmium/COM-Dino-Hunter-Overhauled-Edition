using UnityEngine;

[RequireComponent(typeof(iSceneDamage))]
public class iSparyGagma : MonoBehaviour
{
	public Vector2 m_v2RandomTime = new Vector2(2f, 4f);

	public float m_fTime;

	protected bool m_bActive;

	protected float m_fTimeCount;

	protected float m_fNextTime;

	protected ParticleSystem[] m_arrParticleSystem;

	protected iSceneDamage m_SceneDamage;

	protected TAudioController m_AudioController;

	private void Awake()
	{
		m_SceneDamage = GetComponent<iSceneDamage>();
		m_arrParticleSystem = GetComponentsInChildren<ParticleSystem>();
		if (m_arrParticleSystem != null)
		{
			ParticleSystem[] arrParticleSystem = m_arrParticleSystem;
			foreach (ParticleSystem particleSystem in arrParticleSystem)
			{
				particleSystem.enableEmission = false;
			}
		}
		m_bActive = false;
		m_fNextTime = Random.Range(m_v2RandomTime.x, m_v2RandomTime.y);
		m_AudioController = GetComponent<TAudioController>();
		if (m_AudioController == null)
		{
			m_AudioController = base.gameObject.AddComponent<TAudioController>();
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		if (m_bActive)
		{
			if (m_fTime > 0f)
			{
				m_fTimeCount += deltaTime;
				if (m_fTimeCount >= m_fTime)
				{
					Stop();
				}
			}
		}
		else
		{
			m_fNextTime -= deltaTime;
			if (!(m_fNextTime > 0f))
			{
				Play();
			}
		}
	}

	protected void Play()
	{
		if (m_arrParticleSystem != null)
		{
			ParticleSystem[] arrParticleSystem = m_arrParticleSystem;
			foreach (ParticleSystem particleSystem in arrParticleSystem)
			{
				particleSystem.enableEmission = true;
			}
		}
		m_bActive = true;
		m_fTimeCount = 0f;
		m_SceneDamage.SetActive(true);
		if (m_AudioController != null)
		{
			m_AudioController.PlayAudio("Amb_lava");
		}
	}

	protected void Stop()
	{
		if (m_arrParticleSystem != null)
		{
			ParticleSystem[] arrParticleSystem = m_arrParticleSystem;
			foreach (ParticleSystem particleSystem in arrParticleSystem)
			{
				particleSystem.enableEmission = false;
			}
		}
		m_bActive = false;
		m_fNextTime = Random.Range(m_v2RandomTime.x, m_v2RandomTime.y);
		m_SceneDamage.SetActive(false);
		if (m_AudioController != null)
		{
			m_AudioController.StopAudio("Amb_lava");
		}
	}
}
