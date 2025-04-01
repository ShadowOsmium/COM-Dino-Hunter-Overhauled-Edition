using UnityEngine;

public class iDisappear : MonoBehaviour
{
	protected enum kState
	{
		None,
		Normal,
		Disappear
	}

	public float fTime = 5f;

	public float fDisappearTime = 1f;

	public ParticleSystem[] arrParticleSystem;

	protected kState m_State;

	private void Awake()
	{
		m_State = kState.Normal;
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
			fTime -= deltaTime;
			if (!(fTime <= 0f))
			{
				break;
			}
			m_State = kState.Disappear;
			if (arrParticleSystem != null)
			{
				ParticleSystem[] array = arrParticleSystem;
				foreach (ParticleSystem particleSystem in array)
				{
					particleSystem.enableEmission = false;
				}
			}
			break;
		case kState.Disappear:
			fDisappearTime -= deltaTime;
			if (fDisappearTime <= 0f)
			{
				Object.Destroy(base.gameObject);
			}
			break;
		}
	}
}
