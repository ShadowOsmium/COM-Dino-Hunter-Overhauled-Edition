using System.Collections.Generic;
using UnityEngine;

public class PopupNewHelpStateContent : MonoBehaviour
{
	public enum ContentShowState
	{
		ShowScroll,
		ShowEnd
	}

	public TUIMeshSprite img_arrow;

	public TUIMeshSpriteSliced img_bg;

	public TUILabel label_text;

	public List<string> text_list;

	private int text_index;

	public float scroll_speed = 0.01f;

	private float scroll_time;

	private ContentShowState content_show_state = ContentShowState.ShowEnd;

	private int show_length = 1;

	private string show_text = string.Empty;

	private void Start()
	{
	}

	private void Update()
	{
		UpdateTextScroll(Time.deltaTime);
	}

	public void AddText(string m_text)
	{
		if (text_list == null)
		{
			text_list = new List<string>();
		}
		text_list.Add(m_text);
	}

	public void BeginTextScroll()
	{
		content_show_state = ContentShowState.ShowScroll;
		if (text_list != null && text_index + 1 <= text_list.Count)
		{
			show_text = text_list[text_index];
		}
		show_length = 0;
	}

	private void UpdateTextScroll(float delta_time)
	{
		if (content_show_state != 0)
		{
			return;
		}
		scroll_time += delta_time;
		if (!(scroll_time > scroll_speed))
		{
			return;
		}
		scroll_time = 0f;
		string text = show_text.Substring(0, show_length);
		if (text.EndsWith("\\") && show_length + 1 <= show_text.Length)
		{
			string text2 = show_text.Substring(0, show_length + 1);
			if (text2.EndsWith("\\n"))
			{
				text = text2;
				show_length++;
			}
		}
		if (label_text != null)
		{
			label_text.Text = text;
		}
		show_length++;
		if (show_length > show_text.Length)
		{
			content_show_state = ContentShowState.ShowEnd;
		}
	}

	public void UpdateTextScrollEnd()
	{
		content_show_state = ContentShowState.ShowEnd;
		show_length = 0;
		if (label_text != null)
		{
			label_text.Text = show_text;
		}
	}

	public void NextTextScroll()
	{
		content_show_state = ContentShowState.ShowScroll;
		show_length = 0;
		text_index++;
		if (text_list != null && text_index + 1 <= text_list.Count)
		{
			show_text = text_list[text_index];
		}
		else
		{
			content_show_state = ContentShowState.ShowEnd;
		}
	}

	public ContentShowState GetContentShowState()
	{
		return content_show_state;
	}

	public bool IsTextOver()
	{
		if (text_list != null && text_index + 1 >= text_list.Count && content_show_state == ContentShowState.ShowEnd)
		{
			return true;
		}
		return false;
	}
}
