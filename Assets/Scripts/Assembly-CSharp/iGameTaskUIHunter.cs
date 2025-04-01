using gyTaskSystem;
using UnityEngine;

public class iGameTaskUIHunter : iGameTaskUIBase
{
	protected UISprite m_TargetIcon;

	protected UILabel m_CurNum;

	protected UILabel m_MaxNum;

	protected UILabel m_LV;

	private void Awake()
	{
		base.Height = 25f;
		Transform transform = null;
		transform = base.transform.Find("icon");
		if (transform != null)
		{
			m_TargetIcon = transform.GetComponent<UISprite>();
			if (base.Height < transform.localScale.y)
			{
				base.Height = transform.localScale.y;
			}
		}
		transform = base.transform.Find("txtCur");
		if (transform != null)
		{
			m_CurNum = transform.GetComponent<UILabel>();
			if (base.Height < transform.localScale.y)
			{
				base.Height = transform.localScale.y;
			}
		}
		transform = base.transform.Find("txtMax");
		if (transform != null)
		{
			m_MaxNum = transform.GetComponent<UILabel>();
			if (base.Height < transform.localScale.y)
			{
				base.Height = transform.localScale.y;
			}
		}
		transform = base.transform.Find("txtLV");
		if (transform != null)
		{
			m_LV = transform.GetComponent<UILabel>();
			m_LV.enabled = false;
			if (base.Height < transform.localScale.y)
			{
				base.Height = transform.localScale.y;
			}
		}
	}

	private void Update()
	{
		float deltaTime = Time.deltaTime;
	}

	public void SetIcon(string sIcon)
	{
		if (!(m_TargetIcon == null))
		{
			GameObject gameObject = (GameObject)Resources.Load("Artist/Atlas/MonstersIcon/" + sIcon);
			if (gameObject != null)
			{
				m_TargetIcon.atlas = gameObject.GetComponent<UIAtlas>();
			}
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

	public void SetLV(int nLevel)
	{
		if (!(m_LV == null))
		{
			m_LV.enabled = true;
			m_LV.text = "LV" + nLevel;
		}
	}

	public override void InitTask(CTaskBase taskbase)
	{
	}
}
