using System.Collections.Generic;
using UnityEngine;

public class PopupNewHelp : MonoBehaviour
{
	public TUIMeshSprite img_mask;

	public TUIButtonClick btn_mask;

	public List<PopupNewHelpState> help_list;

	private bool later_force_update;

	protected NewHelpState help_state = NewHelpState.Help_Over;

	private PopupNewHelpState go_help;

	public PopupOnlyText popup_skip;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void LateUpdate()
	{
		if (later_force_update)
		{
			later_force_update = false;
			if (img_mask != null)
			{
				img_mask.ForceUpdate();
			}
		}
	}

	protected void ForceUpdateMaskByRect(GameObject m_rect, bool m_now = true)
	{
		if (img_mask != null)
		{
			img_mask.m_hideClipObj = m_rect;
			if (m_now)
			{
				img_mask.ForceUpdate();
			}
			else
			{
				later_force_update = true;
			}
		}
	}

	public void SetHelpState(NewHelpState m_state, float m_pos_x = 0f)
	{
		if (help_list == null)
		{
			Debug.Log("error!");
			return;
		}
		help_state = m_state;
		if (go_help != null)
		{
			go_help.Hide();
			go_help = null;
		}
		for (int i = 0; i < help_list.Count; i++)
		{
			if (!(help_list[i] != null) || help_list[i].GetNewHelpState() != help_state)
			{
				continue;
			}
			help_list[i].Show(m_pos_x);
			if (help_list[i].IsTextOver())
			{
				help_list[i].ShowArrow(true);
				help_list[i].ShowCircle(true);
			}
			else
			{
				help_list[i].ShowArrow(false);
				help_list[i].ShowCircle(false);
			}
			go_help = help_list[i];
			if (btn_mask != null)
			{
				if (go_help.IsTextOver())
				{
					btn_mask.gameObject.SetActiveRecursively(false);
					GameObject rectShow = go_help.GetRectShow();
					ForceUpdateMaskByRect(rectShow, false);
				}
				else
				{
					btn_mask.gameObject.SetActiveRecursively(true);
					if (img_mask != null)
					{
						img_mask.m_hideClipObj = null;
						img_mask.ForceUpdate();
					}
				}
			}
			Debug.Log("change to state:" + help_state);
			return;
		}
		Debug.Log("no this state!" + help_state);
	}

	public void ResetHelpState()
	{
		if (help_list == null)
		{
			Debug.Log("error!");
			return;
		}
		go_help = null;
		for (int i = 0; i < help_list.Count; i++)
		{
			if (help_list[i] != null)
			{
				help_list[i].Hide();
			}
		}
	}

	public NewHelpState GetHelpState()
	{
		return help_state;
	}

	public void Show()
	{
		base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
	}

	public void Hide()
	{
		base.transform.localPosition = new Vector3(0f, -1000f, base.transform.localPosition.z);
	}

	public TUIControl GetBtnMask()
	{
		return btn_mask;
	}

	public void DoBtnMaskEvent()
	{
		if (go_help == null)
		{
			Debug.Log("error!");
		}
		else if (go_help.IsTextOver())
		{
			if (btn_mask != null)
			{
				btn_mask.gameObject.SetActiveRecursively(false);
			}
			go_help.ShowArrow(true);
			go_help.ShowCircle(true);
			GameObject rectShow = go_help.GetRectShow();
			ForceUpdateMaskByRect(rectShow);
		}
		else
		{
			go_help.ShowTextScroll();
		}
	}

	public void ShowSkipTutorial(bool m_show)
	{
		if (popup_skip != null)
		{
			if (m_show)
			{
				popup_skip.Show();
			}
			else
			{
				popup_skip.Hide();
			}
		}
	}
}
