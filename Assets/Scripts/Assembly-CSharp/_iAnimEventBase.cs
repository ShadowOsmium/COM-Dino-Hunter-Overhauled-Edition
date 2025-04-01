using UnityEngine;

public class _iAnimEventBase : MonoBehaviour
{
	public Transform m_Node;

	public float m_fTime = 2f;

	public GameObject m_Effect;

	public virtual void PlayEffect(int nPrefabID)
	{
		if (m_Effect != null)
		{
			Object.Destroy(m_Effect);
			m_Effect = null;
		}
		GameObject gameObject = PrefabManager.Get(nPrefabID);
		if (gameObject == null)
		{
			return;
		}
		m_Effect = (GameObject)Object.Instantiate(gameObject, m_Node.position, Quaternion.identity);
		if (!(m_Effect == null))
		{
			TransformRefresh(m_Effect);
			if (m_fTime > 0f)
			{
				Object.Destroy(m_Effect, m_fTime);
			}
		}
	}

	public virtual void PlaySound(string sSoundName)
	{
		if (!(m_Node == null))
		{
			TAudioController tAudioController = m_Node.gameObject.GetComponent<TAudioController>();
			if (tAudioController == null)
			{
				tAudioController = m_Node.gameObject.AddComponent<TAudioController>();
			}
			if (!(tAudioController == null))
			{
				tAudioController.PlayAudio(sSoundName);
			}
		}
	}

	protected virtual void TransformRefresh(GameObject o)
	{
		if (!(m_Node == null))
		{
			o.transform.position = m_Node.position;
			o.transform.rotation = Quaternion.identity;
		}
	}
}
