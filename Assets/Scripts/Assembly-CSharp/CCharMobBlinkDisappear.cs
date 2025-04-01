using UnityEngine;

public class CCharMobBlinkDisappear : MonoBehaviour
{
	public GameObject mEntity;

	public AnimationClip mClip;

	protected Color m_Color;

	protected Renderer mRenderer;

	private void Awake()
	{
		mRenderer = mEntity.GetComponentInChildren<Renderer>();
	}

	private void Start()
	{
		m_Color = mRenderer.material.GetColor("_AtmoColor");
		if (mEntity != null && mEntity.GetComponent<Animation>() != null && mEntity.GetComponent<Animation>()[mClip.name] != null)
		{
			mEntity.GetComponent<Animation>()[mClip.name].time = mEntity.GetComponent<Animation>()[mClip.name].length;
			mEntity.GetComponent<Animation>().Sample();
			mEntity.GetComponent<Animation>().Play(mClip.name);
		}
	}

	private void Update()
	{
		if (!(mRenderer == null))
		{
			m_Color.a -= 0.5f * Time.deltaTime;
			mRenderer.material.SetColor("_AtmoColor", m_Color);
			if (m_Color.a <= 0f)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}
}
