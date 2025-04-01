using UnityEngine;

public class iAlphaDisappear : MonoBehaviour
{
	public float fTime = 2f;

	protected Renderer[] m_arrRenderer;

	protected float m_fSpeed;

	private void Awake()
	{
		m_fSpeed = 1f / fTime;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (m_arrRenderer == null)
		{
			m_arrRenderer = GetComponentsInChildren<Renderer>();
		}
		if (m_arrRenderer == null)
		{
			return;
		}
		Renderer[] arrRenderer = m_arrRenderer;
		foreach (Renderer renderer in arrRenderer)
		{
			if (!(renderer == null) && !(renderer.material == null))
			{
				Color color = renderer.material.color;
				color.a -= m_fSpeed * Time.deltaTime;
				if (color.a < 0f)
				{
					color.a = 0f;
				}
				renderer.material.color = color;
			}
		}
	}
}
