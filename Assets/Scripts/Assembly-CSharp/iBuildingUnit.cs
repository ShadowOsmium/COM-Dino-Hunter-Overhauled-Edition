using UnityEngine;

public class iBuildingUnit : MonoBehaviour
{
	public GameObject[] m_arrModel;

	protected Renderer m_Renderer;

	protected Color m_SrcColor;

	protected Color m_DstColor;

	protected float m_fColorRate;

	protected int m_curIndex;

	private void Awake()
	{
		m_fColorRate = 1f;
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (!(m_Renderer == null) && !(m_Renderer.material == null) && m_fColorRate < 1f)
		{
			m_fColorRate += Time.deltaTime;
			m_Renderer.material.color = Color.Lerp(m_SrcColor, m_DstColor, m_fColorRate);
		}
	}

	public void SetModel(int nIndex)
	{
		if (m_arrModel == null || nIndex < 0 || nIndex >= m_arrModel.Length)
		{
			return;
		}
		m_Renderer = null;
		for (int i = 0; i < m_arrModel.Length; i++)
		{
			if (!(m_arrModel[i] == null))
			{
				if (i == nIndex)
				{
					m_arrModel[i].SetActiveRecursively(true);
					m_Renderer = m_arrModel[i].GetComponent<Renderer>();
				}
				else
				{
					m_arrModel[i].SetActiveRecursively(false);
				}
			}
		}
		m_curIndex = nIndex;
	}

	public void PlayColorAnim(Color src, Color dst)
	{
		m_SrcColor = src;
		m_DstColor = dst;
		m_fColorRate = 0f;
	}
}
