using UnityEngine;

public class gyUIButton : MonoBehaviour
{
	public UISprite m_Normal;

	public UISprite m_Disable;

	public UISprite m_Pressed;

	protected Collider m_Collider;

	public bool Enable
	{
		get
		{
			if (m_Collider == null)
			{
				return false;
			}
			return m_Collider.enabled;
		}
		set
		{
			if (m_Collider == null)
			{
				return;
			}
			m_Collider.enabled = value;
			if (m_Normal != null)
			{
				m_Normal.enabled = value;
			}
			if (m_Disable != null)
			{
				m_Disable.enabled = !value;
			}
			if (base.GetComponent<Animation>() != null && base.GetComponent<Animation>()["Ani_ScaleChange03"] != null)
			{
				if (value)
				{
					base.GetComponent<Animation>().Play("Ani_ScaleChange03");
				}
				else
				{
					base.GetComponent<Animation>().Stop();
				}
				base.transform.localScale = Vector3.one;
			}
		}
	}

	private void Awake()
	{
		m_Collider = GetComponent<Collider>();
		Enable = true;
	}

	private void Start()
	{
	}

	private void OnPress(bool press)
	{
		if (Enable && m_Pressed != null)
		{
			m_Pressed.enabled = press;
			if (m_Normal != null)
			{
				m_Normal.enabled = !press;
			}
		}
	}
}
