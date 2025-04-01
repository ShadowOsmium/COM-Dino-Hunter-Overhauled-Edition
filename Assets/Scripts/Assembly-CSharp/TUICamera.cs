using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class TUICamera : MonoBehaviour
{
	public bool lock960x640;

	public Rect m_viewRect;

	private int layer;

	private int depth;

	private float width;

	private float height;

	public void Initialize(int layer, int depth)
	{
		base.transform.position = Vector3.zero;
		base.transform.rotation = Quaternion.identity;
		base.transform.localScale = Vector3.one;
		bool flag = TUI.IsRetina();
		bool flag2 = TUI.IsDoubleHD();
		base.GetComponent<Camera>().transform.localPosition = new Vector3(1f / ((!flag) ? 2f : 4f) / (float)((!flag2) ? 1 : 2), -1f / ((!flag) ? 2f : 4f) / (float)((!flag2) ? 1 : 2), 0f);
		base.GetComponent<Camera>().nearClipPlane = -128f;
		base.GetComponent<Camera>().farClipPlane = 128f;
		base.GetComponent<Camera>().orthographic = true;
		base.GetComponent<Camera>().depth = depth;
		base.GetComponent<Camera>().cullingMask = 1 << layer;
		base.GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
		this.layer = layer;
		this.depth = depth;
		width = Screen.width;
		height = Screen.height;
		m_viewRect = new Rect(0f, 0f, width, height);
		if (lock960x640)
		{
			if (Screen.width >= 960 && Screen.height >= 640)
			{
				float left = ((float)Screen.width - 960f) / 2f;
				float top = ((float)Screen.height - 640f) / 2f;
				m_viewRect = new Rect(left, top, 960f, 640f);
			}
			else if (Screen.width >= 640 && Screen.height >= 960)
			{
				float left2 = ((float)Screen.width - 640f) / 2f;
				float top2 = ((float)Screen.height - 960f) / 2f;
				m_viewRect = new Rect(left2, top2, 640f, 960f);
			}
		}
		base.GetComponent<Camera>().pixelRect = m_viewRect;
		base.GetComponent<Camera>().aspect = m_viewRect.width / m_viewRect.height;
		base.GetComponent<Camera>().orthographicSize = 160f;
	}

	private void Update()
	{
		float num = Screen.width;
		float num2 = Screen.height;
		if (num != width || num2 != height)
		{
			Initialize(layer, depth);
		}
	}
}
