using UnityEngine;

public class gyUIHUD : MonoBehaviour
{
	public string m_sText;

	public float m_fWidth = 100f;

	public UISprite m_Bottom;

	public UISprite m_Left;

	public UISprite m_Right;

	public UISprite m_LeftLine;

	public UISprite m_RightLine;

	public UILabel m_Label;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Show(bool bShow)
	{
		base.transform.gameObject.SetActiveRecursively(bShow);
	}

	public void SetPos(Vector3 v3Pos)
	{
		base.transform.localPosition = v3Pos;
	}

	public void SetText(string str)
	{
		if (!(m_Label == null))
		{
			m_Label.text = str;
			if (m_Label.font != null)
			{
				float num = m_Label.transform.localScale.x * m_Label.font.CalculatePrintedSize(str, m_Label.supportEncoding, m_Label.symbolStyle).x;
				SetWidth(num + 10f);
				Debug.Log(num);
			}
		}
	}

	public void SetWidth(float width)
	{
		m_fWidth = width;
		UpdateWidth();
	}

	protected void UpdateWidth()
	{
		float x = m_Left.transform.localScale.x;
		float x2 = m_Right.transform.localScale.x;
		float x3 = m_Bottom.transform.localScale.x;
		float num = x + x2 + x3;
		if (m_fWidth <= num)
		{
			m_LeftLine.gameObject.active = false;
			m_RightLine.gameObject.active = false;
			m_Left.transform.localPosition = new Vector3((0f - x3) * 0.5f - x * 0.5f, m_Left.transform.localPosition.y, m_Left.transform.localPosition.z);
			m_Right.transform.localPosition = new Vector3(x3 * 0.5f + x * 0.5f, m_Right.transform.localPosition.y, m_Right.transform.localPosition.z);
			return;
		}
		m_LeftLine.gameObject.active = true;
		m_RightLine.gameObject.active = true;
		float f = (m_fWidth - num) * 0.5f;
		f = Mathf.Floor(f);
		m_Left.transform.localPosition = new Vector3((0f - x3) * 0.5f - x * 0.5f - f, m_Left.transform.localPosition.y, m_Left.transform.localPosition.z);
		m_Right.transform.localPosition = new Vector3(x3 * 0.5f + x2 * 0.5f + f, m_Right.transform.localPosition.y, m_Right.transform.localPosition.z);
		m_LeftLine.transform.localScale = new Vector3(f, m_LeftLine.transform.localScale.y, m_LeftLine.transform.localScale.z);
		m_LeftLine.transform.localPosition = new Vector3((0f - x3) * 0.5f - f * 0.5f, m_LeftLine.transform.localPosition.y, m_LeftLine.transform.localPosition.z);
		m_RightLine.transform.localScale = new Vector3(f, m_RightLine.transform.localScale.y, m_RightLine.transform.localScale.z);
		m_RightLine.transform.localPosition = new Vector3(x3 * 0.5f + f * 0.5f, m_RightLine.transform.localPosition.y, m_RightLine.transform.localPosition.z);
	}
}
