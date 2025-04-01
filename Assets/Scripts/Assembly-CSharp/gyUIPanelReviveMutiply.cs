using UnityEngine;

public class gyUIPanelReviveMutiply : MonoBehaviour
{
	public gyUIButton m_Back;

	public gyUIButton m_Revive;

	protected bool m_bShow;

	private void Awake()
	{
		m_bShow = false;
		base.gameObject.SetActiveRecursively(false);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Show(bool bShow)
	{
		m_bShow = bShow;
		base.gameObject.SetActiveRecursively(bShow);
		if (bShow)
		{
			base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
			if (m_Revive != null)
			{
				m_Revive.Enable = true;
			}
			if (m_Back != null)
			{
				m_Back.Enable = true;
			}
		}
		else
		{
			base.transform.localPosition = new Vector3(10000f, 10000f, base.transform.localPosition.z);
		}
	}
}
