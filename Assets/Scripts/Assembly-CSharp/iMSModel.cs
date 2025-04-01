using UnityEngine;

[RequireComponent(typeof(TAudioController))]
public class iMSModel : MonoBehaviour
{
	protected Transform m_Transfrom;

	protected TAudioController m_AudioController;

	protected Vector3 m_v3Src;

	protected Vector3 m_v3Dst;

	protected float m_fMoveTime;

	protected float m_fMoveTimeCount;

	protected string m_sAnimMove = "walk";

	protected float m_fAnimMoveRate = 1f;

	private void Awake()
	{
		m_Transfrom = base.transform;
		m_AudioController = GetComponent<TAudioController>();
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
		if (m_fMoveTime > 0f)
		{
			m_fMoveTimeCount += deltaTime;
			m_Transfrom.position = Vector3.Lerp(m_v3Src, m_v3Dst, m_fMoveTimeCount / m_fMoveTime);
			m_Transfrom.forward = m_v3Dst - m_v3Src;
			if (m_fMoveTimeCount >= m_fMoveTime)
			{
				m_fMoveTime = 0f;
				m_fMoveTimeCount = 0f;
				StopAnim(m_sAnimMove);
			}
		}
	}

	public void Move(Vector3 v3Dst, string sAnim, float fAnimMoveRate, float fTime)
	{
		if (m_Transfrom != null)
		{
			m_v3Src = m_Transfrom.position;
		}
		m_v3Dst = v3Dst;
		m_fMoveTime = fTime;
		m_fMoveTimeCount = 0f;
		m_sAnimMove = sAnim;
		m_fAnimMoveRate = fAnimMoveRate;
		if (m_sAnimMove.Length > 1)
		{
			float num = Vector3.Distance(m_v3Src, m_v3Dst);
			CrossAnim(m_sAnimMove, WrapMode.Loop, num / fTime * m_fAnimMoveRate, 0f);
		}
	}

	public void PlayAnim(string sAnim, WrapMode mode = WrapMode.Once, float speed = 1f, float time = 0f)
	{
		if (!(base.GetComponent<Animation>() == null) && !(base.GetComponent<Animation>()[sAnim] == null))
		{
			base.GetComponent<Animation>()[sAnim].wrapMode = mode;
			base.GetComponent<Animation>()[sAnim].time = time;
			base.GetComponent<Animation>()[sAnim].speed = speed;
			base.GetComponent<Animation>().Play(sAnim);
		}
	}

	public void CrossAnim(string sAnim, WrapMode mode = WrapMode.Once, float speed = 1f, float time = 0f)
	{
		if (!(base.GetComponent<Animation>() == null) && !(base.GetComponent<Animation>()[sAnim] == null))
		{
			base.GetComponent<Animation>()[sAnim].wrapMode = mode;
			base.GetComponent<Animation>()[sAnim].time = time;
			base.GetComponent<Animation>()[sAnim].speed = speed;
			base.GetComponent<Animation>().CrossFade(sAnim);
		}
	}

	public void StopAnim(string sAnim)
	{
		if (!(base.GetComponent<Animation>() == null) && !(base.GetComponent<Animation>()[sAnim] == null))
		{
			base.GetComponent<Animation>()[sAnim].speed = 0f;
		}
	}

	public void PlayAudio(string sAudio)
	{
		if (!(m_AudioController == null))
		{
			m_AudioController.PlayAudio(sAudio);
		}
	}
}
