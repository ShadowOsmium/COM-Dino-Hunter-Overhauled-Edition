using UnityEngine;

public class iTexture2D : MonoBehaviour
{
	public Vector3 m_v3Offset = Vector3.zero;

	public Material m_Material;

	public Vector2 m_v2Size = new Vector2(1f, 1f);

	protected MeshFilter m_MeshFilter;

	protected MeshRenderer m_MeshRenderer;

	public void Start()
	{
		m_MeshFilter = (MeshFilter)base.gameObject.AddComponent(typeof(MeshFilter));
		m_MeshRenderer = (MeshRenderer)base.gameObject.AddComponent(typeof(MeshRenderer));
		m_MeshRenderer.castShadows = false;
		m_MeshRenderer.receiveShadows = false;
		m_MeshRenderer.sharedMaterial = m_Material;
		m_MeshFilter.mesh.Clear();
		Vector3[] vertices = new Vector3[4]
		{
			-Vector3.right * m_v2Size.x / 2f + Vector3.forward * m_v2Size.y / 2f + m_v3Offset,
			-Vector3.right * m_v2Size.x / 2f - Vector3.forward * m_v2Size.y / 2f + m_v3Offset,
			Vector3.right * m_v2Size.x / 2f - Vector3.forward * m_v2Size.y / 2f + m_v3Offset,
			Vector3.right * m_v2Size.x / 2f + Vector3.forward * m_v2Size.y / 2f + m_v3Offset
		};
		Vector2[] uv = new Vector2[4]
		{
			new Vector2(0f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 1f),
			new Vector2(1f, 0f)
		};
		Color[] array = new Color[4];
		Color color = new Color(0.2f, 0.2f, 0.2f, 1f);
		array[0] = color;
		array[1] = color;
		array[2] = color;
		array[3] = color;
		int[] triangles = new int[6] { 0, 3, 1, 1, 3, 2 };
		m_MeshFilter.mesh.vertices = vertices;
		m_MeshFilter.mesh.uv = uv;
		m_MeshFilter.mesh.colors = array;
		m_MeshFilter.mesh.triangles = triangles;
	}

	public void Update()
	{
	}

	public void Show(bool bShow)
	{
		if (!(m_MeshRenderer == null) && m_MeshRenderer.enabled != bShow)
		{
			m_MeshRenderer.enabled = bShow;
		}
	}
}
