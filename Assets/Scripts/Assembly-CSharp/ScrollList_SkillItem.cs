using System.Collections.Generic;
using UnityEngine;

public class ScrollList_SkillItem : MonoBehaviour
{
	public TUIMeshSprite img_bg;

	public TUIMeshSprite img_frame;

	public TUIMeshSprite img_frame_choose;

	public TUIMeshSprite img_lock;

	public TUIMeshSprite img_new;

	public TUIMeshSprite img_duoren;

	private bool be_choose;

	private TUISkillInfo skill_info;

	private string texture_mark = "new";

	private string texture_new = "new2";

	private NewMarkType new_mark_type;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void DoChoose()
	{
		if (!be_choose)
		{
			be_choose = true;
			img_frame.gameObject.SetActiveRecursively(false);
			img_frame_choose.gameObject.SetActiveRecursively(true);
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
			img_frame.gameObject.SetActiveRecursively(true);
			img_frame_choose.gameObject.SetActiveRecursively(false);
		}
	}

	public void DoCreate(TUISkillInfo m_skill_info, Dictionary<int, NewMarkType> m_new_mark_list)
	{
		be_choose = true;
		skill_info = m_skill_info;
		if (m_skill_info.unlock || m_skill_info.active_skill)
		{
			img_lock.gameObject.SetActiveRecursively(false);
		}
		if (m_skill_info.active_skill)
		{
			img_bg.texture = TUIMappingInfo.Instance().GetSkillTexture(m_skill_info.id, true);
		}
		else
		{
			img_bg.texture = TUIMappingInfo.Instance().GetSkillTexture(m_skill_info.id);
		}
		if (img_duoren != null)
		{
			img_duoren.gameObject.SetActiveRecursively(false);
			if (skill_info.duoren_skill && skill_info.level >= skill_info.duoren_skill_level)
			{
				img_duoren.gameObject.SetActiveRecursively(true);
			}
		}
		DoUnChoose();
		if (m_new_mark_list != null && m_new_mark_list.ContainsKey(skill_info.id))
		{
			SetNewMark(img_new, m_new_mark_list[skill_info.id]);
		}
		else
		{
			SetNewMark(img_new, NewMarkType.None);
		}
	}

	public int GetSkillID()
	{
		return skill_info.id;
	}

	public string GetSkillName()
	{
		return skill_info.name;
	}

	public int GetSkillLevelMax()
	{
		if (skill_info != null && skill_info.level_price != null)
		{
			return skill_info.level_price.Count;
		}
		return 0;
	}

	public int GetSkillLevel()
	{
		return skill_info.level;
	}

	public bool GetSkillActive()
	{
		return skill_info.active_skill;
	}

	public string GetSkillIntroduce()
	{
		if (skill_info.level_introduce.ContainsKey(skill_info.level + 1))
		{
			return skill_info.level_introduce[skill_info.level + 1];
		}
		Debug.Log("warning! no introduce!");
		return string.Empty;
	}

	public string GetSkillIntroduceEx()
	{
		int key = ((skill_info.level <= 1) ? 1 : skill_info.level);
		Debug.Log("skill_info.level:" + skill_info.level);
		if (skill_info.level_introduce_ex.ContainsKey(key))
		{
			return skill_info.level_introduce_ex[key];
		}
		Debug.Log("warning! no introduce_ex!");
		return string.Empty;
	}

	public bool GetSkillUnlock()
	{
		return skill_info.unlock;
	}

	public TUIPriceInfo GetSkillUnlockPrice()
	{
		return skill_info.unlock_price;
	}

	public string GetUnlockIntroduce()
	{
		return skill_info.skill_introduce_unlock;
	}

	public TUIPriceInfo GetSkillBuyPrice()
	{
		if (skill_info.level_price.ContainsKey(1))
		{
			return skill_info.level_price[1];
		}
		return null;
	}

	public TUIPriceInfo GetSkillUpdatePrice()
	{
		TUIPriceInfo result = null;
		if (skill_info == null || skill_info.level_price == null)
		{
			Debug.Log("error!");
			return result;
		}
		if (skill_info.level_price.ContainsKey(skill_info.level + 1))
		{
			result = skill_info.level_price[skill_info.level + 1];
		}
		else
		{
			Debug.Log("error!");
		}
		return result;
	}

	public void SkillUnlock()
	{
		img_lock.gameObject.SetActiveRecursively(false);
		skill_info.unlock = true;
	}

	public void SkillBuy()
	{
		skill_info.level = 1;
	}

	public void SkillUpdate()
	{
		skill_info.level++;
		if (skill_info.duoren_skill && skill_info.level >= skill_info.duoren_skill_level && img_duoren != null)
		{
			img_duoren.gameObject.SetActiveRecursively(true);
		}
	}

	public string GetCustomizeTexture()
	{
		return img_bg.m_texture;
	}

	public bool ReachLevelMax()
	{
		if (skill_info.level >= skill_info.level_price.Count)
		{
			return true;
		}
		return false;
	}

	public void UpdateNewMark(Dictionary<int, NewMarkType> m_new_mark_list)
	{
		if (skill_info == null || m_new_mark_list == null)
		{
			Debug.Log("error!");
		}
		else if (m_new_mark_list.ContainsKey(skill_info.id))
		{
			SetNewMark(img_new, m_new_mark_list[skill_info.id]);
		}
	}

	private void SetNewMark(TUIMeshSprite m_sprite, NewMarkType m_new_mark)
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

	private void HideNewMark(TUIMeshSprite m_sprite)
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

	public float GetDiscount()
	{
		return skill_info.discount;
	}

	public int GetDuoRenLevel()
	{
		if (skill_info != null)
		{
			return skill_info.duoren_skill_level;
		}
		return 0;
	}
}
