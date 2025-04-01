using UnityEngine;

public class PlayerEnterText : MonoBehaviour
{
	public TUIMeshSprite img_arrow;

	public TUIMeshSpriteSliced img_bg;

	public TUILabel label_text;

	private bool open_show;

	private float time_show_now;

	private float time_show = 2f;

	private bool event_text_over;

	private void Start()
	{
	}

	private void Update()
	{
		UpdateShow();
	}

	public void Show(bool m_open)
	{
		base.gameObject.SetActiveRecursively(m_open);
		open_show = m_open;
		if (!open_show)
		{
			time_show_now = 0f;
		}
	}

	public void UpdateShow()
	{
		if (open_show)
		{
			time_show_now += Time.deltaTime;
			if (time_show_now > time_show)
			{
				time_show_now = 0f;
				open_show = false;
				base.gameObject.SetActiveRecursively(false);
				event_text_over = true;
			}
		}
	}

	public void SetInfo(string m_text)
	{
		if (label_text != null)
		{
			label_text.Text = m_text;
		}
	}

	public bool Event_TextShowOver()
	{
		if (event_text_over)
		{
			event_text_over = false;
			return true;
		}
		return false;
	}
}
