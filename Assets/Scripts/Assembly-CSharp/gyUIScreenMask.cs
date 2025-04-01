using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(UISprite))]
public class gyUIScreenMask : MonoBehaviour
{
	public class CMaskInfo
	{
		public float z;

		public int depth;

		public int count;
	}

	public bool isCollider = true;

	protected Transform mTransform;

	protected BoxCollider mCollider;

	protected UISprite mSprite;

	protected bool m_bInProcess;

	protected bool m_bFinishHide;

	protected float m_fSrc;

	protected float m_fDst;

	protected float m_fSpeed;

	protected float m_fRate;

	protected UIAnchor mAnchor;

	protected List<CMaskInfo> m_ltMaskInfo;

	private void Awake()
	{
		mAnchor = NGUITools.FindInParents<UIAnchor>(base.gameObject);
		mTransform = base.transform;
		mTransform.localScale = new Vector3(Screen.width, Screen.height, 1f);
		mCollider = GetComponent<BoxCollider>();
		if (mCollider != null)
		{
			mCollider.isTrigger = true;
			mCollider.center = Vector3.zero;
			mCollider.size = Vector3.one;
			if (!isCollider)
			{
				mCollider.enabled = false;
			}
		}
		mSprite = GetComponent<UISprite>();
		if (mSprite != null)
		{
			mSprite.pivot = UIWidget.Pivot.Center;
		}
		base.gameObject.active = false;
		m_ltMaskInfo = new List<CMaskInfo>();
	}

	private void Update()
	{
		if (!m_bInProcess || mSprite == null)
		{
			return;
		}
		m_fRate += m_fSpeed * Time.deltaTime;
		Color color = mSprite.color;
		color.a = Lerp(m_fSrc, m_fDst, m_fRate);
		mSprite.color = color;
		if (m_fRate >= 1f)
		{
			m_bInProcess = false;
			if (m_bFinishHide)
			{
				base.gameObject.active = false;
			}
		}
	}

	protected float Lerp(float src, float dst, float rate)
	{
		if (rate >= 1f)
		{
			return dst;
		}
		if (rate <= 0f)
		{
			return src;
		}
		return src + (dst - src) * rate;
	}

	public void FadeIn(float fTime)
	{
		if (fTime == 0f)
		{
			if (mSprite != null)
			{
				Color color = mSprite.color;
				color.a = 0f;
				mSprite.color = color;
				base.gameObject.active = false;
			}
			return;
		}
		m_bInProcess = true;
		m_bFinishHide = true;
		m_fSrc = 1f;
		m_fDst = 0f;
		m_fSpeed = 1f / fTime;
		m_fRate = 0f;
		mTransform.localScale = new Vector3(Screen.width, Screen.height, 1f) / mAnchor.transform.localScale.x;
		mTransform.localPosition = new Vector3(mTransform.localPosition.x, mTransform.localPosition.y, -100f);
		base.gameObject.active = true;
		Color color2 = mSprite.color;
		color2.a = m_fSrc;
		mSprite.color = color2;
	}

	public void FadeOut(float fTime)
	{
		if (fTime == 0f)
		{
			if (mSprite != null)
			{
				Color color = mSprite.color;
				color.a = 1f;
				mSprite.color = color;
			}
			return;
		}
		m_bInProcess = true;
		m_bFinishHide = false;
		m_fSrc = 0f;
		m_fDst = 1f;
		m_fSpeed = 1f / fTime;
		m_fRate = 0f;
		mTransform.localScale = new Vector3(Screen.width, Screen.height, 1f) / mAnchor.transform.localScale.x;
		mTransform.localPosition = new Vector3(mTransform.localPosition.x, mTransform.localPosition.y, -100f);
		base.gameObject.active = true;
		Color color2 = mSprite.color;
		color2.a = m_fSrc;
		mSprite.color = color2;
	}

	public void ShowMask(bool bShow, float z, int depth, float fAlpha = 0.8f)
	{
		bool flag = true;
		for (int i = 0; i < m_ltMaskInfo.Count; i++)
		{
			CMaskInfo cMaskInfo = m_ltMaskInfo[i];
			if (cMaskInfo == null || z > cMaskInfo.z)
			{
				continue;
			}
			if (cMaskInfo.z == z)
			{
				if (depth < cMaskInfo.depth || cMaskInfo.depth != depth)
				{
					continue;
				}
				if (bShow)
				{
					cMaskInfo.count++;
					continue;
				}
				cMaskInfo.count--;
				if (cMaskInfo.count <= 0)
				{
					m_ltMaskInfo.Remove(cMaskInfo);
					flag = false;
					break;
				}
				continue;
			}
			CMaskInfo cMaskInfo2 = new CMaskInfo();
			cMaskInfo2.z = z;
			cMaskInfo2.depth = depth;
			cMaskInfo2.count = 1;
			m_ltMaskInfo.Insert(i, cMaskInfo2);
			flag = false;
			Debug.Log("inster " + i);
			break;
		}
		if (bShow && flag)
		{
			CMaskInfo cMaskInfo3 = new CMaskInfo();
			cMaskInfo3.z = z;
			cMaskInfo3.depth = depth;
			cMaskInfo3.count = 1;
			m_ltMaskInfo.Add(cMaskInfo3);
		}
		if (m_ltMaskInfo.Count < 1)
		{
			base.gameObject.active = false;
			return;
		}
		mTransform.localScale = new Vector3(Screen.width, Screen.height, 1f) / mAnchor.transform.localScale.x;
		mTransform.localPosition = new Vector3(mTransform.localPosition.x, mTransform.localPosition.y, m_ltMaskInfo[0].z);
		Color color = mSprite.color;
		color.a = fAlpha;
		mSprite.color = color;
		mSprite.depth = m_ltMaskInfo[0].depth;
		base.gameObject.active = true;
	}
}
