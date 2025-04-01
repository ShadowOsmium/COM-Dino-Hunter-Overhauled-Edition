using UnityEngine;

[RequireComponent(typeof(TAudioController))]
public class iDinoHunterAudioController : MonoBehaviour
{
	public bool m_bBoss;

	protected TAudioController m_AudioController;

	private void Awake()
	{
		m_AudioController = GetComponent<TAudioController>();
	}

	public void PlayAudioByMobType(string sName)
	{
		if (!(m_AudioController == null) && !(base.transform.root == null))
		{
			if (m_bBoss)
			{
				sName += "_Boss";
			}
			m_AudioController.PlayAudio(sName);
		}
	}
}
