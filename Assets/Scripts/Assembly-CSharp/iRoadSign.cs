using UnityEngine;

public class iRoadSign : MonoBehaviour
{
	public float m_fSpeed = 1f;

	public MeshFilter m_MeshFilter;

	public Color m_Color;

	public Renderer m_Renderer;

	protected bool m_bActive;

	protected Color m_SrcColor;

	protected Color m_DstColor;

	protected float m_fRate;

	protected int m_nStep;

	protected Color[] m_arrMeshColor;

	private void Awake()
	{
		m_bActive = false;
		if (m_MeshFilter != null)
		{
			m_arrMeshColor = new Color[m_MeshFilter.mesh.vertexCount];
			for (int i = 0; i < m_arrMeshColor.Length; i++)
			{
				m_arrMeshColor[i].a = 0f;
			}
			m_MeshFilter.mesh.colors = m_arrMeshColor;
			m_Renderer = m_MeshFilter.transform.GetComponent<Renderer>();
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (!m_bActive || m_MeshFilter == null)
		{
			return;
		}
		m_fRate += m_fSpeed * Time.deltaTime;
		Color color = Color.Lerp(m_SrcColor, m_DstColor, m_fRate);
		for (int i = 0; i < m_arrMeshColor.Length; i++)
		{
			m_arrMeshColor[i] = color;
		}
		m_MeshFilter.mesh.colors = m_arrMeshColor;
		switch (m_nStep)
		{
		case 0:
			if (m_fRate >= 1f)
			{
				m_fRate = 0f;
				m_nStep = 1;
				m_SrcColor = Color.white;
				m_DstColor = m_Color;
			}
			break;
		case 1:
			if (m_fRate >= 1f)
			{
				if (m_Renderer != null)
				{
					m_Renderer.enabled = false;
				}
				m_bActive = false;
			}
			break;
		}
	}

	public void Go()
	{
		if (!(m_MeshFilter == null))
		{
			m_SrcColor = m_Color;
			m_DstColor = Color.white;
			m_fRate = 0f;
			m_bActive = true;
			m_nStep = 0;
			if (m_Renderer != null)
			{
				m_Renderer.enabled = true;
			}
		}
	}
}
