using UnityEngine;

public class iMobBrokenDead : MonoBehaviour
{
	protected enum kDeadProcess
	{
		None,
		Wait,
		Disappear,
		Destroy
	}

	protected kDeadProcess m_State;

	protected float m_fWaitTime;

	protected float m_fDisappearTime;

	protected bool m_bFall;

	protected float m_fSpeedYInt;

	protected float m_fSpeedYAcc = -40f;

	protected TAudioController m_AudioController;

	private void Start()
	{
		m_State = kDeadProcess.Wait;
		m_fWaitTime = 2f;
		m_fDisappearTime = 2f;
		if (base.GetComponent<Animation>() != null && base.GetComponent<Animation>()["death"] != null)
		{
			base.GetComponent<Animation>().Play("death");
		}
		if (base.transform.position.y > 0f)
		{
			m_bFall = true;
		}
		m_AudioController = GetComponent<TAudioController>();
		PlayAudio("Fx_Burst_body");
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		switch (m_State)
		{
		case kDeadProcess.Wait:
			m_fWaitTime -= deltaTime;
			if (m_fWaitTime <= 0f)
			{
				m_State = kDeadProcess.Disappear;
			}
			break;
		case kDeadProcess.Disappear:
			m_fDisappearTime -= deltaTime;
			if (m_fDisappearTime <= 0f)
			{
				m_fDisappearTime = 0f;
			}
			if (m_fDisappearTime <= 0f)
			{
				m_State = kDeadProcess.Destroy;
				StopAudio("Fx_Burst_body");
				Object.Destroy(base.gameObject);
			}
			break;
		}
		if (m_bFall)
		{
			base.transform.position += new Vector3(0f, m_fSpeedYInt * deltaTime, 0f);
			m_fSpeedYInt += m_fSpeedYAcc * deltaTime;
			if (base.transform.position.y <= 0f)
			{
				base.transform.position = new Vector3(base.transform.position.x, 0f, base.transform.position.z);
				m_bFall = false;
				m_State = kDeadProcess.Disappear;
			}
		}
	}

	public void PlayAudio(string sPrefabName)
	{
		if (!(m_AudioController == null))
		{
			m_AudioController.PlayAudio(sPrefabName);
		}
	}

	public void StopAudio(string sPrefabName)
	{
		if (!(m_AudioController == null))
		{
			m_AudioController.StopAudio(sPrefabName);
		}
	}
}
