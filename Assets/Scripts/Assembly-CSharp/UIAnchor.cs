using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Anchor")]
public class UIAnchor : MonoBehaviour
{
	public enum Side
	{
		BottomLeft,
		Left,
		TopLeft,
		Top,
		TopRight,
		Right,
		BottomRight,
		Bottom,
		Center
	}

	private bool mIsWindows;

	public Camera uiCamera;

	public UIWidget widgetContainer;

	public UIPanel panelContainer;

	public Side side = Side.Center;

	public bool halfPixelOffset = true;

	public float depthOffset;

	public Vector2 relativeOffset = Vector2.zero;

	private Animation mAnim;

	private UIRoot mRoot;

	private void Awake()
	{
		mAnim = base.GetComponent<Animation>();
	}

	private void Start()
	{
		mRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
		mIsWindows = Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor;
		if (uiCamera == null)
		{
			uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
	}

	private void Update()
	{
		if (mAnim != null && mAnim.enabled && mAnim.isPlaying)
		{
			return;
		}
		Rect rect = default(Rect);
		bool flag = false;
		if (panelContainer != null)
		{
			if (panelContainer.clipping == UIDrawCall.Clipping.None)
			{
				float num = ((!(mRoot != null)) ? 0.5f : ((float)mRoot.manualHeight / (float)Screen.height * 0.5f));
				rect.xMin = (float)(-Screen.width) * num;
				rect.yMin = (float)(-Screen.height) * num;
				rect.xMax = 0f - rect.xMin;
				rect.yMax = 0f - rect.yMin;
			}
			else
			{
				Vector4 clipRange = panelContainer.clipRange;
				rect.x = clipRange.x - clipRange.z * 0.5f;
				rect.y = clipRange.y - clipRange.w * 0.5f;
				rect.width = clipRange.z;
				rect.height = clipRange.w;
			}
		}
		else if (widgetContainer != null)
		{
			Transform cachedTransform = widgetContainer.cachedTransform;
			Vector3 localScale = cachedTransform.localScale;
			Vector3 localPosition = cachedTransform.localPosition;
			Vector3 vector = widgetContainer.relativeSize;
			Vector3 vector2 = widgetContainer.pivotOffset;
			vector2.y -= 1f;
			vector2.x *= widgetContainer.relativeSize.x * localScale.x;
			vector2.y *= widgetContainer.relativeSize.y * localScale.y;
			rect.x = localPosition.x + vector2.x;
			rect.y = localPosition.y + vector2.y;
			rect.width = vector.x * localScale.x;
			rect.height = vector.y * localScale.y;
		}
		else
		{
			if (!(uiCamera != null))
			{
				return;
			}
			flag = true;
			rect = uiCamera.pixelRect;
		}
		float x = (rect.xMin + rect.xMax) * 0.5f;
		float y = (rect.yMin + rect.yMax) * 0.5f;
		Vector3 vector3 = new Vector3(x, y, depthOffset);
		if (side != Side.Center)
		{
			if (side == Side.Right || side == Side.TopRight || side == Side.BottomRight)
			{
				vector3.x = rect.xMax;
			}
			else if (side == Side.Top || side == Side.Center || side == Side.Bottom)
			{
				vector3.x = x;
			}
			else
			{
				vector3.x = rect.xMin;
			}
			if (side == Side.Top || side == Side.TopRight || side == Side.TopLeft)
			{
				vector3.y = rect.yMax;
			}
			else if (side == Side.Left || side == Side.Center || side == Side.Right)
			{
				vector3.y = y;
			}
			else
			{
				vector3.y = rect.yMin;
			}
		}
		float width = rect.width;
		float height = rect.height;
		vector3.x += relativeOffset.x * width;
		vector3.y += relativeOffset.y * height;
		if (flag)
		{
			if (uiCamera.orthographic)
			{
				vector3.x = Mathf.RoundToInt(vector3.x);
				vector3.y = Mathf.RoundToInt(vector3.y);
				if (halfPixelOffset && mIsWindows)
				{
					vector3.x -= 0.5f;
					vector3.y += 0.5f;
				}
			}
			vector3 = uiCamera.ScreenToWorldPoint(vector3);
			if (base.transform.position != vector3)
			{
				base.transform.position = vector3;
			}
			return;
		}
		vector3.x = Mathf.RoundToInt(vector3.x);
		vector3.y = Mathf.RoundToInt(vector3.y);
		if (panelContainer != null)
		{
			vector3 = panelContainer.transform.TransformPoint(vector3);
		}
		else if (widgetContainer != null)
		{
			Transform parent = widgetContainer.transform.parent;
			if (parent != null)
			{
				vector3 = parent.TransformPoint(vector3);
			}
		}
		if (base.transform.position != vector3)
		{
			base.transform.position = vector3;
		}
	}
}
