using UnityEngine;

public class LabelInfoBlackMarket : MonoBehaviour
{
	public TUILabel label_title01;

	public TUILabel label_title01_value;

	public TUILabel label_title_max;

	public TUILabel label_introduce;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetInfo(int m_title_value, int m_max, string m_introduce)
	{
		if (m_title_value == 0)
		{
			if (label_title01 != null)
			{
				label_title01.gameObject.SetActiveRecursively(false);
			}
			if (label_title01_value != null)
			{
				label_title01_value.gameObject.SetActiveRecursively(false);
			}
			if (label_title_max != null)
			{
				label_title_max.gameObject.SetActiveRecursively(false);
			}
		}
		else
		{
			if (label_title01 != null)
			{
				label_title01.gameObject.SetActiveRecursively(true);
			}
			if (label_title01_value != null)
			{
				label_title01_value.gameObject.SetActiveRecursively(true);
				label_title01_value.Text = m_title_value.ToString();
			}
			if (label_title_max != null)
			{
				label_title_max.gameObject.SetActiveRecursively(true);
				label_title_max.Text = "(Max " + m_max + ")";
			}
		}
		if (label_introduce != null)
		{
			label_introduce.Text = m_introduce;
		}
	}
}
