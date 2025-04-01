using UnityEngine;

public class gyUIMovieMask : MonoBehaviour
{
	public float fWidthRate;

	public float fHeightRate;

	public float fTime;

	public UISprite mMaskUp;

	public UISprite mMaskDown;

	protected Transform mTransform;

	protected bool m_bInProcess;

	protected bool m_bFinishHide;

	protected Vector2 m_v2SrcUp;

	protected Vector2 m_v2DstUp;

	protected Vector2 m_v2SrcDown;

	protected Vector2 m_v2DstDown;

	protected float m_fSpeed;

	protected float m_fRate;

	protected UIAnchor mAnchor;

	private void Awake()
	{
		mTransform = base.transform;
		mAnchor = NGUITools.FindInParents<UIAnchor>(base.gameObject);
		if (mMaskUp != null)
		{
			mMaskUp.pivot = UIWidget.Pivot.Bottom;
			mMaskUp.transform.localScale = new Vector3((float)Screen.width * fWidthRate, (float)Screen.height * fHeightRate, 1f);
		}
		if (mMaskDown != null)
		{
			mMaskDown.pivot = UIWidget.Pivot.Top;
			mMaskDown.transform.localScale = new Vector3((float)Screen.width * fWidthRate, (float)Screen.height * fHeightRate, 1f);
		}
		base.gameObject.SetActiveRecursively(false);
	}

	private void Update()
	{
		if (!m_bInProcess)
		{
			return;
		}
		m_fRate += m_fSpeed * Time.deltaTime;
		mMaskUp.transform.localPosition = Vector2.Lerp(m_v2SrcUp, m_v2DstUp, m_fRate);
		mMaskDown.transform.localPosition = Vector2.Lerp(m_v2SrcDown, m_v2DstDown, m_fRate);
		if (m_fRate >= 1f)
		{
			m_bInProcess = false;
			if (m_bFinishHide)
			{
				base.gameObject.SetActiveRecursively(false);
			}
		}
	}

	public void MoveIn(bool bFinishHide)
	{
		Debug.Log((float)Screen.width * fWidthRate + " " + (float)Screen.height * fHeightRate + " " + mAnchor.transform.localScale.x);
		mMaskUp.transform.localScale = new Vector3((float)Screen.width * fWidthRate, (float)Screen.height * fHeightRate, 1f) / mAnchor.transform.localScale.x;
		mMaskDown.transform.localScale = new Vector3((float)Screen.width * fWidthRate, (float)Screen.height * fHeightRate, 1f) / mAnchor.transform.localScale.x;
		m_bInProcess = true;
		m_bFinishHide = bFinishHide;
		m_v2SrcUp = new Vector2(0f, (float)(-Screen.height) * 0.5f / mAnchor.transform.localScale.x - mMaskUp.transform.localScale.y);
		m_v2DstUp = new Vector2(0f, (float)(-Screen.height) * 0.5f) / mAnchor.transform.localScale.x;
		m_v2SrcDown = new Vector2(0f, (float)Screen.height * 0.5f / mAnchor.transform.localScale.x + mMaskDown.transform.localScale.y);
		m_v2DstDown = new Vector2(0f, (float)Screen.height * 0.5f) / mAnchor.transform.localScale.x;
		m_fRate = 0f;
		m_fSpeed = 1f / fTime;
		mMaskUp.transform.localPosition = m_v2SrcUp;
		mMaskDown.transform.localPosition = m_v2SrcDown;
		base.gameObject.SetActiveRecursively(true);
	}

	public void MoveOut(bool bFinishHide)
	{
		mMaskUp.transform.localScale = new Vector3((float)Screen.width * fWidthRate, (float)Screen.height * fHeightRate, 1f) / mAnchor.transform.localScale.x;
		mMaskDown.transform.localScale = new Vector3((float)Screen.width * fWidthRate, (float)Screen.height * fHeightRate, 1f) / mAnchor.transform.localScale.x;
		m_bInProcess = true;
		m_bFinishHide = bFinishHide;
		m_v2SrcUp = new Vector2(0f, (float)(-Screen.height) * 0.5f) / mAnchor.transform.localScale.x;
		m_v2DstUp = new Vector2(0f, (float)(-Screen.height) * 0.5f / mAnchor.transform.localScale.x - mMaskUp.transform.localScale.y);
		m_v2SrcDown = new Vector2(0f, (float)Screen.height * 0.5f) / mAnchor.transform.localScale.x;
		m_v2DstDown = new Vector2(0f, (float)Screen.height * 0.5f / mAnchor.transform.localScale.x + mMaskDown.transform.localScale.y);
		m_fRate = 0f;
		m_fSpeed = 1f / fTime;
		mMaskUp.transform.localPosition = m_v2SrcUp;
		mMaskDown.transform.localPosition = m_v2SrcDown;
		base.gameObject.SetActiveRecursively(true);
	}
}
