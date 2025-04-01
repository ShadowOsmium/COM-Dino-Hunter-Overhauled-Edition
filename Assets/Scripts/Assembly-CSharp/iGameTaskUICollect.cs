using gyTaskSystem;
using UnityEngine;

public class iGameTaskUICollect : iGameTaskUIBase
{
	public UISprite mBackground;

	public UIFilledSprite mSpritePrecents;

	public UISprite m_TargetIcon;

	public UILabel m_CurNum;

	public UILabel m_MaxNum;

	public UILabel m_Slash;

	protected CTaskCollection m_TaskCollect;

	protected int m_nIndex;

	private void Awake()
	{
		base.Height = 25f;
		if (base.Height < m_TargetIcon.transform.localScale.y)
		{
			base.Height = m_TargetIcon.transform.localScale.y;
		}
		if (base.Height < m_CurNum.transform.localScale.y)
		{
			base.Height = m_CurNum.transform.localScale.y;
		}
		if (base.Height < m_MaxNum.transform.localScale.y)
		{
			base.Height = m_MaxNum.transform.localScale.y;
		}
		ShowLifeBar(false);
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
	}

	public void Initialize(CTaskBase taskbase, int nIndex)
	{
		base.Initialize(taskbase);
		m_nIndex = nIndex;
	}

	public void SetIcon(string sIcon)
	{
		if (!(m_TargetIcon == null))
		{
			m_TargetIcon.spriteName = sIcon;
		}
	}

	public void SetCurNum(int nNum)
	{
		if (!(m_CurNum == null))
		{
			m_CurNum.text = nNum.ToString();
		}
	}

	public void SetMaxNum(int nNum)
	{
		if (!(m_MaxNum == null))
		{
			m_MaxNum.text = nNum.ToString();
		}
	}

	public void SetPrecents(float fPrecents)
	{
		if (!(mSpritePrecents == null))
		{
			mSpritePrecents.fillAmount = fPrecents;
		}
	}

	public override void Show(bool bShow)
	{
		Debug.Log("collect ui " + bShow);
		if (m_TargetIcon != null)
		{
			m_TargetIcon.gameObject.SetActiveRecursively(bShow);
		}
		if (m_CurNum != null)
		{
			m_CurNum.gameObject.SetActiveRecursively(bShow);
		}
		if (m_Slash != null)
		{
			m_Slash.gameObject.SetActiveRecursively(bShow);
		}
		if (m_MaxNum != null)
		{
			m_MaxNum.gameObject.SetActiveRecursively(bShow);
		}
		if (!bShow)
		{
			ShowLifeBar(bShow);
		}
	}

	public override void InitTask(CTaskBase taskbase)
	{
		m_TaskCollect = m_curTaskBase as CTaskCollection;
		if (m_TaskCollect != null && m_nIndex >= 0 && m_nIndex < m_TaskCollect.m_ltCollections.Count)
		{
			SetCurNum(m_TaskCollect.m_ltCollections[m_nIndex].nCurCount);
			SetMaxNum(m_TaskCollect.m_ltCollections[m_nIndex].nMaxCount);
			SetPrecents(m_TaskCollect.m_ltCollections[m_nIndex].nCurCount / m_TaskCollect.m_ltCollections[m_nIndex].nMaxCount);
		}
	}

	public void ShowLifeBar(bool bShow)
	{
		if (mBackground != null)
		{
			mBackground.gameObject.SetActiveRecursively(bShow);
		}
		if (mSpritePrecents != null)
		{
			mSpritePrecents.gameObject.SetActiveRecursively(bShow);
		}
	}

	public bool IsLifeBarShow()
	{
		if (mBackground != null)
		{
			return mBackground.gameObject.active;
		}
		return false;
	}
}
