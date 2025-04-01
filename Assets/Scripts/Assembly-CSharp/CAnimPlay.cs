using UnityEngine;

public class CAnimPlay
{
	protected GameObject m_Model;

	protected CAnimData m_AnimData;

	protected AnimationState m_curFadeOutAnim;

	protected float m_fFadeOutAnimSpeed;

	public void Initialize(GameObject model, CAnimData data)
	{
		m_Model = model;
		m_AnimData = data;
	}

	public void Destroy()
	{
		m_Model = null;
		m_AnimData = null;
	}

	public void Update(float deltaTime)
	{
		if (m_curFadeOutAnim != null)
		{
			m_curFadeOutAnim.weight -= m_fFadeOutAnimSpeed * deltaTime;
			if (m_curFadeOutAnim.weight <= 0f)
			{
				m_curFadeOutAnim.enabled = false;
				m_curFadeOutAnim = null;
			}
		}
	}

	public bool IsAnimValid(string sAnim)
	{
		if (m_Model == null || m_Model.GetComponent<Animation>() == null || m_Model.GetComponent<Animation>()[sAnim] == null)
		{
			return false;
		}
		return true;
	}

	public void Sample()
	{
		m_Model.GetComponent<Animation>().Sample();
	}

	public float GetAnimLen(kAnimEnum nType)
	{
		string name = m_AnimData.GetName(nType);
		if (!IsAnimValid(name))
		{
			return 0f;
		}
		return m_Model.GetComponent<Animation>()[name].length;
	}

	public bool IsAnimPlaying(kAnimEnum nType)
	{
		string name = m_AnimData.GetName(nType);
		if (!IsAnimValid(name))
		{
			return false;
		}
		return m_Model.GetComponent<Animation>().IsPlaying(name);
	}

	public void Stop(kAnimEnum nType)
	{
		string name = m_AnimData.GetName(nType);
		if (IsAnimValid(name))
		{
			m_Model.GetComponent<Animation>().Stop(name);
		}
	}

	public float GetAnimSpeed(kAnimEnum nType)
	{
		string name = m_AnimData.GetName(nType);
		if (!IsAnimValid(name))
		{
			return -1f;
		}
		return m_Model.GetComponent<Animation>()[name].speed;
	}

	public void SetAnimSpeed(kAnimEnum nType, float speed)
	{
		string name = m_AnimData.GetName(nType);
		if (IsAnimValid(name))
		{
			m_Model.GetComponent<Animation>()[name].speed = speed;
		}
	}

	public bool GetAnimEnable(string sAnim)
	{
		if (!IsAnimValid(sAnim))
		{
			return false;
		}
		return m_Model.GetComponent<Animation>()[sAnim].enabled;
	}

	public void SetAnimEnable(string sAnim, bool bEnable)
	{
		if (IsAnimValid(sAnim))
		{
			m_Model.GetComponent<Animation>()[sAnim].enabled = bEnable;
		}
	}

	public void SetAnimLayer(kAnimEnum nType, int nLayer)
	{
		string name = m_AnimData.GetName(nType);
		if (IsAnimValid(name))
		{
			UnityEngine.Debug.Log(string.Concat("SetAnimLayer ", nType, " ", nLayer));
			m_Model.GetComponent<Animation>()[name].layer = nLayer;
		}
	}

	public void FadeOutAnim(kAnimEnum nType, float fTime)
	{
		string name = m_AnimData.GetName(nType);
		if (IsAnimValid(name))
		{
			if (m_curFadeOutAnim != null)
			{
				m_curFadeOutAnim.enabled = false;
			}
			m_curFadeOutAnim = m_Model.GetComponent<Animation>()[name];
			m_fFadeOutAnimSpeed = 1f / fTime;
		}
	}

	public void StopFadeOut(kAnimEnum nType)
	{
		string name = m_AnimData.GetName(nType);
		if (IsAnimValid(name) && !(m_curFadeOutAnim == null) && !(m_curFadeOutAnim.name != name))
		{
			m_curFadeOutAnim.enabled = false;
			m_curFadeOutAnim = null;
		}
	}

	public void PlayAnim(kAnimEnum nType, WrapMode mode, float speed = 1f, float time = 0f)
	{
		string name = m_AnimData.GetName(nType);
		if (IsAnimValid(name))
		{
			m_Model.GetComponent<Animation>()[name].wrapMode = mode;
			m_Model.GetComponent<Animation>()[name].speed = speed;
			m_Model.GetComponent<Animation>()[name].layer = 0;
			if (time >= 0f)
			{
				m_Model.GetComponent<Animation>()[name].time = time;
			}
			m_Model.GetComponent<Animation>().Play(name);
		}
	}

	public void CrossFade(kAnimEnum nType, WrapMode mode, float fadetime = 0.3f, float speed = 1f, float time = 0f)
	{
		string name = m_AnimData.GetName(nType);
		if (IsAnimValid(name))
		{
			m_Model.GetComponent<Animation>()[name].wrapMode = mode;
			m_Model.GetComponent<Animation>()[name].speed = speed;
			m_Model.GetComponent<Animation>()[name].layer = 0;
			if (time >= 0f)
			{
				m_Model.GetComponent<Animation>()[name].time = time;
			}
			m_Model.GetComponent<Animation>().CrossFade(name, fadetime);
		}
	}

	public void PlayAnimMix(kAnimEnum nType, WrapMode mode, float speed = 1f)
	{
		string name = m_AnimData.GetName(nType);
		if (IsAnimValid(name))
		{
			Transform transform = m_Model.transform.Find("Bip01/UpBodyRotate/Bip01 Spine");
			if (!(transform == null))
			{
				StopFadeOut(nType);
				m_Model.GetComponent<Animation>()[name].AddMixingTransform(transform, true);
				m_Model.GetComponent<Animation>()[name].wrapMode = mode;
				m_Model.GetComponent<Animation>()[name].speed = speed;
				m_Model.GetComponent<Animation>()[name].time = 0f;
				m_Model.GetComponent<Animation>()[name].layer = 1;
				m_Model.GetComponent<Animation>().Play(name);
			}
		}
	}

	public void PlayAnimMix(kAnimEnum nType, WrapMode mode, Transform bone, float speed = 1f)
	{
		if (!(bone == null))
		{
			string name = m_AnimData.GetName(nType);
			if (IsAnimValid(name))
			{
				Debug.Log(name);
				m_Model.GetComponent<Animation>()[name].AddMixingTransform(bone, true);
				m_Model.GetComponent<Animation>()[name].wrapMode = mode;
				m_Model.GetComponent<Animation>()[name].speed = speed;
				m_Model.GetComponent<Animation>()[name].time = 0f;
				m_Model.GetComponent<Animation>()[name].layer = 1;
				m_Model.GetComponent<Animation>().Play(name);
			}
		}
	}

	public void CrossFadeMix(kAnimEnum nType, WrapMode mode, float fadetime = 0.3f, float speed = 1f)
	{
		string name = m_AnimData.GetName(nType);
		if (IsAnimValid(name))
		{
			Transform transform = m_Model.transform.Find("Bip01/UpBodyRotate/Bip01 Spine");
			if (!(transform == null))
			{
				m_Model.GetComponent<Animation>()[name].AddMixingTransform(transform, true);
				m_Model.GetComponent<Animation>()[name].wrapMode = mode;
				m_Model.GetComponent<Animation>()[name].speed = speed;
				m_Model.GetComponent<Animation>()[name].time = 0f;
				m_Model.GetComponent<Animation>()[name].layer = 1;
				m_Model.GetComponent<Animation>().CrossFade(name, fadetime);
			}
		}
	}

	public void CrossFadeMix(kAnimEnum nType, WrapMode mode, Transform bone, float fadetime = 0.3f, float speed = 1f)
	{
		if (!(bone == null))
		{
			string name = m_AnimData.GetName(nType);
			if (IsAnimValid(name))
			{
				m_Model.GetComponent<Animation>()[name].AddMixingTransform(bone, true);
				m_Model.GetComponent<Animation>()[name].wrapMode = mode;
				m_Model.GetComponent<Animation>()[name].speed = speed;
				m_Model.GetComponent<Animation>()[name].time = 0f;
				m_Model.GetComponent<Animation>()[name].layer = 1;
				m_Model.GetComponent<Animation>().CrossFade(name, fadetime);
			}
		}
	}
}
