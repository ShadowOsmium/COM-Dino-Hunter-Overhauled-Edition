using UnityEngine;

public class iUVAnim : MonoBehaviour
{
	public enum kMode
	{
		offsetY,
		offsetX,
		offsetYClip,
		offsetXClip
	}

	public kMode m_Mode;

	public int m_nInvert;

	public string sMaterialName;

	public int m_nClipNum;

	protected Material m_Material;

	protected float m_fClipOffset;

	protected float m_fOffset;

	protected float m_fCount;

	private void Awake()
	{
		Renderer component = GetComponent<MeshRenderer>();
		Material[] materials = component.materials;
		foreach (Material material in materials)
		{
			if (material.name.IndexOf(sMaterialName) != -1)
			{
				m_Material = material;
				break;
			}
		}
	}

	private void Start()
	{
		if (!(m_Material == null))
		{
			if (m_nClipNum < 1)
			{
				m_fClipOffset = 1f;
				return;
			}
			m_fClipOffset = 1f / (float)m_nClipNum;
			m_Material.mainTextureScale = new Vector2(m_fClipOffset, 1f);
		}
	}

	private void Update()
	{
		if (m_Material == null)
		{
			return;
		}
		Vector2 mainTextureOffset = m_Material.mainTextureOffset;
		switch (m_Mode)
		{
		case kMode.offsetX:
			mainTextureOffset.x += (float)m_nInvert * Time.deltaTime * 0.05f;
			break;
		case kMode.offsetY:
			mainTextureOffset.y += (float)m_nInvert * Time.deltaTime * 0.05f;
			break;
		case kMode.offsetXClip:
			m_fCount -= Time.deltaTime;
			if (m_fCount > 0f)
			{
				return;
			}
			m_fCount = 0.1f;
			mainTextureOffset.x += (float)m_nInvert * m_fClipOffset;
			if (mainTextureOffset.x > 1f)
			{
				mainTextureOffset.x = 0f;
			}
			break;
		case kMode.offsetYClip:
			mainTextureOffset.y += (float)m_nInvert * m_fClipOffset;
			if (mainTextureOffset.y > 1f)
			{
				mainTextureOffset.y = 0f;
			}
			break;
		}
		m_Material.mainTextureOffset = mainTextureOffset;
	}
}
