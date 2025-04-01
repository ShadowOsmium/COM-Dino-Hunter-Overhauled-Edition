using System.Collections.Generic;
using UnityEngine;

public class PopupNewHelpState : MonoBehaviour
{
	public class NewHelpContentList
	{
		public List<string> content_list;

		public void AddItem(string m_text)
		{
			if (content_list == null)
			{
				content_list = new List<string>();
			}
			content_list.Add(m_text);
		}
	}

	public NewHelpState help_state_now = NewHelpState.Help_Over;

	public TUIRect rect_show;

	public PopupNewHelpStateContent label_content;

	public Transform btn_control;

	public Transform img_arrow;

	public Transform img_circle;

	public Transform target_go;

	protected Dictionary<NewHelpState, NewHelpContentList> new_help_content_list;

	private void Start()
	{
		if (rect_show != null && target_go != null)
		{
			rect_show.transform.position = new Vector3(target_go.transform.position.x, target_go.transform.position.y, rect_show.transform.position.z);
			rect_show.UpdateRect();
		}
	}

	private void Update()
	{
	}

	public NewHelpState GetNewHelpState()
	{
		return help_state_now;
	}

	public void Hide()
	{
		base.gameObject.SetActiveRecursively(false);
	}

	public void Show(float m_control_x = 0f)
	{
		base.gameObject.SetActiveRecursively(true);
		TUIControl component = base.gameObject.GetComponent<TUIControl>();
		if (component != null)
		{
			base.gameObject.GetComponent<TUIControl>().ResetChild();
		}
		if (m_control_x != 0f)
		{
			if (btn_control != null)
			{
				btn_control.position = new Vector3(m_control_x, btn_control.position.y, btn_control.position.z);
			}
			if (img_arrow != null)
			{
				img_arrow.position = new Vector3(m_control_x, img_arrow.position.y, img_arrow.position.z);
			}
		}
		if (label_content != null)
		{
			label_content.BeginTextScroll();
		}
	}

	public void ShowTextScroll()
	{
		if (!IsTextScrollEnd())
		{
			if (label_content != null)
			{
				label_content.UpdateTextScrollEnd();
			}
		}
		else if (label_content != null)
		{
			label_content.NextTextScroll();
		}
	}

	public GameObject GetRectShow()
	{
		return rect_show.gameObject;
	}

	public bool IsTextScrollEnd()
	{
		if (label_content == null)
		{
			Debug.Log("error!");
			return true;
		}
		if (label_content.GetContentShowState() == PopupNewHelpStateContent.ContentShowState.ShowEnd)
		{
			return true;
		}
		return false;
	}

	public bool IsTextOver()
	{
		if (label_content == null)
		{
			return true;
		}
		return label_content.IsTextOver();
	}

	public void ShowArrow(bool m_show)
	{
		if (img_arrow != null)
		{
			img_arrow.gameObject.SetActiveRecursively(m_show);
		}
	}

	public void ShowCircle(bool m_show)
	{
		if (img_circle != null)
		{
			img_circle.gameObject.SetActiveRecursively(m_show);
		}
	}
}
