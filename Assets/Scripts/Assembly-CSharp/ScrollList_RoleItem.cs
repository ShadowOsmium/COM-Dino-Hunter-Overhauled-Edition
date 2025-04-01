using System.Collections.Generic;
using UnityEngine;

public class ScrollList_RoleItem : MonoBehaviour
{
	public TUIMeshSprite img_bg;

	public TUIMeshSprite img_frame;

	public TUIMeshSprite img_lock;

	public TUIMeshSprite img_new;

	private int index;

	private bool be_choose = true;

	private TUIRoleInfo role_info;

	private NewMarkType new_mark_type;

	private string texture_mark = "new";

	private string texture_new = "new2";

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void DoCreate(TUIRoleInfo m_info, Dictionary<int, NewMarkType> m_new_mark_list)
	{
		role_info = m_info;
		img_bg.texture = TUIMappingInfo.Instance().GetRoleTexture(role_info.id);
		if (m_info.unlock)
		{
			img_lock.gameObject.SetActiveRecursively(false);
		}
		DoUnChoose();
		if (m_new_mark_list != null && m_new_mark_list.ContainsKey(role_info.id))
		{
			SetNewMark(img_new, m_new_mark_list[role_info.id]);
		}
		else
		{
			SetNewMark(img_new, NewMarkType.None);
		}
	}

	public void UpdateNewMark(Dictionary<int, NewMarkType> m_new_mark_list)
	{
		if (m_new_mark_list != null && m_new_mark_list.ContainsKey(role_info.id))
		{
			SetNewMark(img_new, m_new_mark_list[role_info.id]);
		}
		else
		{
			SetNewMark(img_new, NewMarkType.None);
		}
	}

	public void DoChoose()
	{
		if (!be_choose)
		{
			be_choose = true;
			img_frame.gameObject.SetActiveRecursively(true);
			if (new_mark_type != NewMarkType.Mark)
			{
				HideNewMark(img_new);
			}
		}
	}

	public void DoUnChoose()
	{
		if (be_choose)
		{
			be_choose = false;
			img_frame.gameObject.SetActiveRecursively(false);
		}
	}

	public void DoLock()
	{
		if (role_info.unlock)
		{
			img_lock.gameObject.SetActiveRecursively(true);
			role_info.unlock = false;
		}
	}

	public void DoUnlock()
	{
		if (!role_info.unlock)
		{
			img_lock.gameObject.SetActiveRecursively(false);
			role_info.unlock = true;
		}
	}

	public void DoBuy()
	{
		if (role_info != null && !role_info.do_buy)
		{
			role_info.do_buy = true;
		}
	}

	public int GetIndex()
	{
		return index;
	}

	public TUIRoleInfo GetRoleInfo()
	{
		return role_info;
	}

	public void SetNewMark(TUIMeshSprite m_sprite, NewMarkType m_new_mark)
	{
		switch (m_new_mark)
		{
		case NewMarkType.Mark:
			if (m_sprite != null)
			{
				m_sprite.texture = texture_mark;
			}
			break;
		case NewMarkType.New:
			if (m_sprite != null)
			{
				m_sprite.texture = texture_new;
			}
			break;
		case NewMarkType.None:
			if (m_sprite != null)
			{
				m_sprite.texture = string.Empty;
			}
			break;
		}
		new_mark_type = m_new_mark;
	}

	public void HideNewMark(TUIMeshSprite m_sprite)
	{
		if (m_sprite != null)
		{
			m_sprite.texture = string.Empty;
			new_mark_type = NewMarkType.None;
		}
	}

	public NewMarkType GetNewMark()
	{
		return new_mark_type;
	}

	public bool GetBuy()
	{
		if (role_info == null)
		{
			return false;
		}
		return role_info.do_buy;
	}

	public bool IsActiveRole()
	{
		if (role_info == null)
		{
			return false;
		}
		return role_info.is_active_buy;
	}
}
