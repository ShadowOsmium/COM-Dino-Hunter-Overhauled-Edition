using UnityEngine;

public class iBulletTrack : MonoBehaviour
{
	protected bool m_bActive;

	protected Transform m_Transform;

	protected Vector3 m_v3Src;

	protected Vector3 m_v3Dst;

	protected float m_fSpeed;

	protected ParticleSystem[] m_arrParicleSystem;

	protected TrailRenderer m_TrailRender;

	protected float m_fWaitTakeBack;

	private void Awake()
	{
		m_bActive = false;
		m_Transform = base.transform;
		m_arrParicleSystem = GetComponentsInChildren<ParticleSystem>();
		Emit(false);
		m_TrailRender = GetComponent<TrailRenderer>();
		m_fWaitTakeBack = 0f;
	}

	private void Update()
	{
		if (!m_bActive)
		{
			return;
		}
		if (m_fWaitTakeBack > 0f)
		{
			m_fWaitTakeBack -= Time.deltaTime;
			if (m_fWaitTakeBack <= 0f)
			{
				m_bActive = false;
				m_fWaitTakeBack = 0f;
				if (m_TrailRender != null)
				{
					m_TrailRender.enabled = false;
				}
				gyUIPoolObject component = GetComponent<gyUIPoolObject>();
				if (component != null)
				{
					component.TakeBack(0f);
				}
			}
		}
		else
		{
			float num = m_fSpeed * Time.deltaTime;
			if (num > (m_v3Dst - m_Transform.position).magnitude)
			{
				m_Transform.position = m_v3Dst;
				Emit(false);
				m_fWaitTakeBack = 2f;
			}
			else
			{
				m_Transform.position += m_Transform.forward * num;
			}
		}
	}

	public void Initialize(Vector3 v3Src, Vector3 v3Dst, float speed)
	{
		m_bActive = true;
		m_v3Src = v3Src;
		m_v3Dst = v3Dst;
		m_fSpeed = speed;
		m_Transform.forward = m_v3Dst - m_v3Src;
		m_Transform.position = m_v3Src + m_Transform.forward * 0.5f;
		if (m_TrailRender != null)
		{
			m_TrailRender.enabled = true;
			float num = Vector3.Distance(v3Src, v3Dst);
			m_TrailRender.time = MyUtils.Lerp(0.01f, 0.2f, num / 20f);
		}
		Emit(true);
	}

	protected void Emit(bool bEmit)
	{
		if (m_arrParicleSystem != null)
		{
			ParticleSystem[] arrParicleSystem = m_arrParicleSystem;
			foreach (ParticleSystem particleSystem in arrParicleSystem)
			{
				particleSystem.enableEmission = bEmit;
			}
		}
	}
}
