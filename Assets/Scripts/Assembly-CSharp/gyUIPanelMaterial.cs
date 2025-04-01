using System.Collections.Generic;
using UnityEngine;

public class gyUIPanelMaterial : gyUICellPanel
{
	protected class CDelayAnim
	{
		public float fDelayTime;

		public int nCount;

		public CDelayAnim(float delaytime, int count)
		{
			fDelayTime = delaytime;
			nCount = count;
		}
	}

	public gyUIItem[] arrItem;

	public gyUIStash mStash;

	public GameObject mTakeAllButton;

	public GameObject mBackButton;

	public GameObject mTitle;

	public GameObject mStatistics;

	public gyUIHUD m_HUD;

	protected bool m_bShow;

	protected List<CDelayAnim> m_ltDelayTime;

	protected int m_nStashCount;

	protected int m_nStashCountTemp;

	private new void Awake()
	{
		m_bShow = false;
		base.gameObject.SetActiveRecursively(false);
		m_ltDelayTime = new List<CDelayAnim>();
		m_nStashCount = 0;
		m_nStashCountTemp = 0;
		m_HUD.Show(false);
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (m_ltDelayTime.Count < 1)
		{
			if (m_nStashCountTemp < m_nStashCount && mStash != null)
			{
				mStash.SetCur(m_nStashCount);
			}
			return;
		}
		int num = 0;
		while (num < m_ltDelayTime.Count)
		{
			CDelayAnim cDelayAnim = m_ltDelayTime[num];
			cDelayAnim.fDelayTime -= Time.deltaTime;
			if (cDelayAnim.fDelayTime <= 0f)
			{
				m_ltDelayTime.RemoveAt(num);
				if (mStash != null)
				{
					m_nStashCountTemp += cDelayAnim.nCount;
					mStash.SetCur(m_nStashCountTemp, true);
				}
			}
			else
			{
				num++;
			}
		}
	}

	public void Show(bool bShow)
	{
		m_bShow = bShow;
		base.gameObject.SetActiveRecursively(bShow);
		HideMaterialName();
		if (bShow)
		{
			base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
			gyUIItem[] array = arrItem;
			foreach (gyUIItem gyUIItem2 in array)
			{
				gyUIItem2.gameObject.SetActiveRecursively(false);
			}
		}
		else
		{
			base.transform.localPosition = new Vector3(10000f, 10000f, base.transform.localPosition.z);
		}
	}

	public void ShowItem(int nIndex, bool bShow)
	{
		if (nIndex >= 0 && nIndex < arrItem.Length)
		{
			arrItem[nIndex].gameObject.SetActiveRecursively(bShow);
		}
	}

	public void SetIcon(int nIndex, UIAtlas atlas)
	{
		if (nIndex >= 0 && nIndex < arrItem.Length)
		{
			arrItem[nIndex].SetIcon(atlas);
		}
	}

	public void SetIconBG(int nIndex, string sName)
	{
		if (nIndex >= 0 && nIndex < arrItem.Length)
		{
			arrItem[nIndex].SetBG(sName);
		}
	}

	public void SetCount(int nIndex, int nCount)
	{
		if (nIndex >= 0 && nIndex < arrItem.Length)
		{
			arrItem[nIndex].SetCount(nCount);
		}
	}

	public void SetIconAnimate(int nIndex, int nCount)
	{
		if (nIndex >= 0 && nIndex < arrItem.Length)
		{
			GameObject gameObject = new GameObject("material icon " + arrItem[nIndex].mIcon.atlas.name);
			gameObject.layer = base.gameObject.layer;
			UISprite uISprite = gameObject.AddComponent<UISprite>();
			uISprite.atlas = arrItem[nIndex].mIcon.atlas;
			uISprite.transform.parent = arrItem[nIndex].mIcon.transform.parent;
			uISprite.transform.localScale = arrItem[nIndex].mIcon.transform.localScale;
			uISprite.transform.localPosition = arrItem[nIndex].mIcon.transform.localPosition;
			uISprite.transform.parent = mStash.transform.parent;
			TweenPosition tweenPosition = TweenPosition.Begin(gameObject, 0.5f, Vector3.zero);
			tweenPosition.from = uISprite.transform.localPosition;
			tweenPosition.to = mStash.transform.localPosition;
			tweenPosition.method = UITweener.Method.EaseIn;
			m_ltDelayTime.Add(new CDelayAnim(0.5f, nCount));
			m_nStashCount += nCount;
			Object.Destroy(gameObject, 0.5f);
		}
	}

	public void ShowMaterialName(int nIndex, string sName)
	{
		if (!(m_HUD == null) && nIndex >= 0 && nIndex < arrItem.Length)
		{
			Vector3 localPosition = arrItem[nIndex].transform.localPosition;
			localPosition.y += arrItem[nIndex].GetBGHeight() * 0.5f;
			m_HUD.SetPos(localPosition);
			m_HUD.SetText(sName);
			m_HUD.Show(true);
		}
	}

	public void HideMaterialName()
	{
		if (!(m_HUD == null))
		{
			m_HUD.Show(false);
		}
	}

	public void SetStashMax(int nCount, bool bAnim = false)
	{
		if (!(mStash == null))
		{
			mStash.SetMax(nCount, bAnim);
		}
	}

	public void SetStashCur(int nCount, bool bAnim = false)
	{
		if (!(mStash == null))
		{
			mStash.SetCur(nCount, bAnim);
			m_nStashCount = nCount;
			m_nStashCountTemp = nCount;
		}
	}

	public void HideMainFrame()
	{
		if (mTitle != null)
		{
			mTitle.SetActiveRecursively(false);
		}
		if (mStatistics != null)
		{
			mStatistics.SetActiveRecursively(false);
		}
	}
}
