using UnityEngine;

[AddComponentMenu("NGUI/UI/Stretch")]
[ExecuteInEditMode]
public class UIStretch : MonoBehaviour
{
	public enum Style
	{
		None,
		Horizontal,
		Vertical,
		Both,
		BasedOnHeight
	}

	public Camera uiCamera;

	public UIWidget widgetContainer;

	public UIPanel panelContainer;

	public Style style;

	public Vector2 relativeSize = Vector2.one;

	private Transform mTrans;

	private UIRoot mRoot;

	private Animation mAnim;

	private void Awake()
	{
		mAnim = base.GetComponent<Animation>();
	}

	private void Start()
	{
		if (uiCamera == null)
		{
			uiCamera = NGUITools.FindCameraForLayer(base.gameObject.layer);
		}
		mRoot = NGUITools.FindInParents<UIRoot>(base.gameObject);
	}

	private void Update()
	{
		if ((mAnim != null && mAnim.isPlaying) || style == Style.None)
		{
			return;
		}
		if (mTrans == null)
		{
			mTrans = base.transform;
		}
		Rect rect = default(Rect);
		if (panelContainer != null)
		{
			if (panelContainer.clipping == UIDrawCall.Clipping.None)
			{
				rect.xMin = (float)(-Screen.width) * 0.5f;
				rect.yMin = (float)(-Screen.height) * 0.5f;
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
			rect = uiCamera.pixelRect;
		}
		float num = rect.width;
		float num2 = rect.height;
		float num3 = ((!(mRoot != null)) ? 1f : mRoot.pixelSizeAdjustment);
		if (num3 != 1f && num2 > 1f)
		{
			float num4 = (float)mRoot.activeHeight / num2;
			num *= num4;
			num2 *= num4;
		}
		Vector3 localScale2 = mTrans.localScale;
		if (style == Style.BasedOnHeight)
		{
			localScale2.x = relativeSize.x * num2;
			localScale2.y = relativeSize.y * num2;
		}
		else
		{
			if (style == Style.Both || style == Style.Horizontal)
			{
				localScale2.x = relativeSize.x * num;
			}
			if (style == Style.Both || style == Style.Vertical)
			{
				localScale2.y = relativeSize.y * num2;
			}
		}
		if (mTrans.localScale != localScale2)
		{
			mTrans.localScale = localScale2;
		}
	}
}
