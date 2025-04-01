using System;
using System.Collections.Generic;

public class MessageBox
{
	private string guid;

	private string titile;

	private string message;

	private List<string> buttons;

	private Action<MessageBox, int> callback;

	public string Guid
	{
		get
		{
			return guid;
		}
	}

	public string Title
	{
		get
		{
			return titile;
		}
		set
		{
			titile = value;
		}
	}

	public string Message
	{
		get
		{
			return message;
		}
		set
		{
			message = value;
		}
	}

	public Action<MessageBox, int> Callback
	{
		get
		{
			return callback;
		}
	}

	public int ButtonsCount
	{
		get
		{
			return (buttons != null) ? buttons.Count : 0;
		}
	}

	public string this[int index]
	{
		get
		{
			return buttons[index];
		}
	}

	public MessageBox(string title, string message, params string[] buttons)
		: this(title, message, null, buttons)
	{
	}

	public MessageBox(string title, string message, Action<MessageBox, int> callback, params string[] buttons)
	{
		guid = System.Guid.NewGuid().ToString();
		titile = title;
		this.message = message;
		this.callback = callback;
		if (buttons != null)
		{
			this.buttons = new List<string>();
			this.buttons.AddRange(buttons);
		}
	}

	public void SetButtons(params string[] buttons)
	{
		this.buttons = new List<string>();
		this.buttons.AddRange(buttons);
	}

	public void Show()
	{
		MessageBoxManager.Show(this);
	}
}
