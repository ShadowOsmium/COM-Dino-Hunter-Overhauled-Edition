using UnityEngine;

public class AchievementBar : MonoBehaviour
{
	public TUISlider slider;

	public TUILabel label_text;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Show(int m_value, string m_text)
	{
		base.gameObject.SetActiveRecursively(true);
		if (slider != null)
		{
			slider.sliderValue = (float)m_value / 100f;
		}
		if (label_text != null)
		{
			label_text.Text = m_text;
		}
	}

	public void Hide()
	{
		base.gameObject.SetActiveRecursively(false);
	}
}
