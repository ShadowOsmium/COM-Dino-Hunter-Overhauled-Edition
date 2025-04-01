using EventCenter;
using UnityEngine;

public class PopupSkill : MonoBehaviour
{
	public TUIButtonSelect btn_role_prefab;

	public Transform roles;

	public TUILabel label_skill_introduce;

	public TUILabel label_skill_introduce_unlock;

	public TUILabel label_skill_name;

	public LevelStarsEx level_stars;

	public Btn_BuySkill btn_click;

	public UnlockBlink unlock_blink;

	private TUIButtonSelect[] btn_role_list;

	public Top_Bar top_bar;

	public PopupSkillUpdate popup_skill_update;

	public ScrollSkill scroll_skill;

	private TUIScrollListEx scroll_list_ex_now;

	private ScrollList_SkillItem item_choose;

	private PopupSkillBtnRole popup_skill_btn_role_now;

	public Scene_Skill scene_skill;

	public PopupSkillUnlock popup_skill_unlock;

	public PopupSkillBuy popup_skill_buy;

	public PopupGoldToCrystal popup_gold_to_crystal;

	public PopupCrystalNoEnough popup_crystal_no_enough;

	public PopupGoEquip popup_go_equip;

	private void Start()
	{
	}

	private void Update()
	{
		CheckScrollChoose();
	}

	public void SetInfo(ScrollList_SkillItem m_item)
	{
		if (m_item == null)
		{
			Debug.Log("no m_item!");
			return;
		}
		label_skill_name.Text = m_item.GetSkillName();
		bool skillUnlock = m_item.GetSkillUnlock();
		int skillLevel = m_item.GetSkillLevel();
		bool skillActive = m_item.GetSkillActive();
		int skillLevelMax = m_item.GetSkillLevelMax();
		if (skillActive)
		{
			label_skill_introduce.Text = m_item.GetSkillIntroduceEx();
			int duoRenLevel = m_item.GetDuoRenLevel();
			if (duoRenLevel != 0 && skillLevel < duoRenLevel)
			{
				btn_click.SetStateUpdate();
			}
			else
			{
				btn_click.SetStateDisable();
			}
			level_stars.SetStarsDisable();
			label_skill_introduce_unlock.Text = string.Empty;
			float x = label_skill_name.CalculateBounds(label_skill_name.Text).size.x;
			x *= label_skill_name.transform.localScale.x;
			Vector3 position = new Vector3(label_skill_name.transform.localPosition.x + x + 12f, label_skill_name.transform.localPosition.y, label_skill_name.transform.localPosition.z);
			if (skillLevel > 0)
			{
				level_stars.SetStars(skillLevel, position);
			}
			else
			{
				level_stars.SetStarsDisable();
			}
			return;
		}
		label_skill_introduce.Text = m_item.GetSkillIntroduceEx();
		float x2 = label_skill_name.CalculateBounds(label_skill_name.Text).size.x;
		x2 *= label_skill_name.transform.localScale.x;
		Vector3 position2 = new Vector3(label_skill_name.transform.localPosition.x + x2 + 12f, label_skill_name.transform.localPosition.y, label_skill_name.transform.localPosition.z);
		if (skillLevel > 0)
		{
			level_stars.SetStars(skillLevel, position2);
		}
		else
		{
			level_stars.SetStarsDisable();
		}
		if (!skillUnlock)
		{
			if (m_item.GetSkillUnlockPrice() == null)
			{
				Debug.Log("error!");
				return;
			}
			btn_click.SetStateUnlock();
			label_skill_introduce_unlock.Text = m_item.GetUnlockIntroduce();
		}
		else if (skillLevel == 0)
		{
			if (m_item.GetSkillBuyPrice() == null)
			{
				Debug.Log("error!");
				return;
			}
			btn_click.SetStateBuy();
			label_skill_introduce_unlock.Text = string.Empty;
		}
		else if (skillLevel < skillLevelMax)
		{
			btn_click.SetStateUpdate();
			label_skill_introduce_unlock.Text = string.Empty;
		}
		else
		{
			btn_click.SetStateDisable();
			label_skill_introduce_unlock.Text = string.Empty;
		}
	}

	public Btn_BuySkill.StateButtonSkill GetStateBtnSkill()
	{
		return btn_click.GetStateBtnSkill();
	}

	public void CheckScrollChoose()
	{
		TUIScrollListEx scrollListChoose = scroll_skill.GetScrollListChoose();
		if (scroll_list_ex_now != scrollListChoose)
		{
			if (scroll_list_ex_now != null)
			{
				if (item_choose != null)
				{
					item_choose.DoUnChoose();
				}
				scroll_list_ex_now.GetComponent<ScrollList_Skill>().ResetPositionNow();
			}
			scroll_list_ex_now = scrollListChoose;
		}
		ScrollList_SkillItem itemChoose = scroll_skill.GetItemChoose();
		if (!(item_choose != itemChoose))
		{
			return;
		}
		item_choose = itemChoose;
		if (!(item_choose != null))
		{
			return;
		}
		if (scene_skill != null)
		{
			if (scene_skill.GetSFXOpen())
			{
				CUISound.GetInstance().Play("UI_Drag");
			}
		}
		else
		{
			Debug.Log("error!");
		}
		SetInfo(item_choose);
		int wparam = 0;
		if (popup_skill_btn_role_now != null)
		{
			wparam = popup_skill_btn_role_now.GetID();
		}
		int skillID = item_choose.GetSkillID();
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneSkill(TUIEvent.SceneSkillEventType.TUIEvent_SkillChoose, wparam, skillID));
	}

	public void ShowSkillUpdate()
	{
		popup_skill_update.ShowSkillUpdate();
		popup_skill_update.SetInfo(item_choose);
	}

	public void ShowSkillUnlock()
	{
		popup_skill_unlock.Show();
		popup_skill_unlock.SetInfo(item_choose);
	}

	public void ShowSkillBuy()
	{
		popup_skill_buy.Show();
		popup_skill_buy.SetInfo(item_choose);
	}

	public void CloseSkillUpdate()
	{
		popup_skill_update.HideSkillUpdate();
	}

	public void CloseSkillUnlock()
	{
		popup_skill_unlock.Hide();
	}

	public void CloseSkillBuy()
	{
		popup_skill_buy.Hide();
	}

	public void ScrollListChoose(TUIControl m_control, int m_skill_index = -1)
	{
		PopupSkillBtnRole component = m_control.GetComponent<PopupSkillBtnRole>();
		if (component == null)
		{
			Debug.Log("error! no popup_skill_btn_role!");
			return;
		}
		popup_skill_btn_role_now = component;
		scroll_skill.ScrollListChoose(popup_skill_btn_role_now.GetIndex(), m_skill_index);
		CheckScrollChoose();
	}

	public int GetScrollListIndex()
	{
		return scroll_list_ex_now.GetComponent<ScrollList_Skill>().GetIndex();
	}

	public int GetSkillID()
	{
		return item_choose.GetSkillID();
	}

	public void AddScrollList(TUIAllSkillInfo m_role_skill_info, GameObject m_go_invoke)
	{
		if (m_role_skill_info == null || m_role_skill_info.all_role_skill_list == null)
		{
			Debug.Log("no skill!");
			return;
		}
		scroll_skill.AddScrollList(m_role_skill_info);
		btn_role_list = new TUIButtonSelect[m_role_skill_info.all_role_skill_list.Length];
		for (int i = 0; i < m_role_skill_info.all_role_skill_list.Length; i++)
		{
			TUIButtonSelect tUIButtonSelect = (TUIButtonSelect)Object.Instantiate(btn_role_prefab);
			tUIButtonSelect.transform.parent = roles;
			tUIButtonSelect.transform.localPosition = new Vector3(0 + i * 48, 0f, 0f);
			PopupSkillBtnRole component = tUIButtonSelect.GetComponent<PopupSkillBtnRole>();
			if (component != null)
			{
				component.SetIndex(i);
				component.SetID(m_role_skill_info.all_role_skill_list[i].id);
				component.SetTexture(m_role_skill_info.all_role_skill_list[i].id);
			}
			else
			{
				Debug.Log("error!");
			}
			tUIButtonSelect.invokeObject = m_go_invoke;
			btn_role_list[i] = tUIButtonSelect;
		}
		SetRoleBtnChoose(0);
		ScrollListChoose(btn_role_list[0]);
	}

	public void UpdateNewMark(TUIAllSkillInfo m_role_skill_info)
	{
		if (scroll_skill == null || m_role_skill_info == null)
		{
			Debug.Log("error!");
		}
		else
		{
			scroll_skill.UpdateNewMark(m_role_skill_info);
		}
	}

	public int GetSkillLevel()
	{
		return item_choose.GetSkillLevel();
	}

	public void SkillUnlock()
	{
		if (item_choose.GetSkillUnlockPrice() == null)
		{
			Debug.Log("error!");
		}
		int price = item_choose.GetSkillUnlockPrice().price;
		UnitType unit_type = item_choose.GetSkillUnlockPrice().unit_type;
		int num = 0;
		switch (unit_type)
		{
		case UnitType.Gold:
			num = top_bar.GetGoldValue();
			num -= price;
			if (num >= 0)
			{
				top_bar.SetGoldValue(num);
				item_choose.SkillUnlock();
				if (item_choose.GetSkillBuyPrice() != null)
				{
					btn_click.SetStateBuy();
				}
				else
				{
					Debug.Log("error!");
				}
				label_skill_introduce.Text = item_choose.GetSkillIntroduceEx();
				label_skill_introduce_unlock.Text = string.Empty;
				unlock_blink.OpenBlinkSkill(item_choose.GetCustomizeTexture(), "New Skill Unlocked For Purchase!");
				if (scene_skill != null)
				{
					if (scene_skill.GetSFXOpen())
					{
						CUISound.GetInstance().Play("UI_Unlocked_weapon");
					}
				}
				else
				{
					Debug.Log("error!");
				}
			}
			else
			{
				Debug.Log("!!!you have no gold enough!!!");
			}
			break;
		case UnitType.Crystal:
			num = top_bar.GetCrystalValue();
			num -= price;
			if (num >= 0)
			{
				top_bar.SetCrystalValue(num);
				item_choose.SkillUnlock();
				if (item_choose.GetSkillBuyPrice() != null)
				{
					btn_click.SetStateBuy();
				}
				else
				{
					Debug.Log("error!");
				}
				label_skill_introduce.Text = item_choose.GetSkillIntroduceEx();
				label_skill_introduce_unlock.Text = string.Empty;
				unlock_blink.OpenBlinkSkill(item_choose.GetCustomizeTexture(), "New Skill Unlocked For Purchase!");
				if (scene_skill != null)
				{
					if (scene_skill.GetSFXOpen())
					{
						CUISound.GetInstance().Play("UI_Unlocked_weapon");
					}
				}
				else
				{
					Debug.Log("error!");
				}
			}
			else
			{
				Debug.Log("!!!you have no crystal enough!!!");
			}
			break;
		}
	}

	public void SkillBuy()
	{
		if (item_choose.GetSkillBuyPrice() == null)
		{
			Debug.Log("error!");
			return;
		}
		TUIPriceInfo skillBuyPrice = item_choose.GetSkillBuyPrice();
		if (skillBuyPrice == null)
		{
			Debug.Log("no info!");
			return;
		}
		float discount = item_choose.GetDiscount();
		bool flag = !(discount >= 1f);
		int num = skillBuyPrice.price;
		if (flag)
		{
			num = Mathf.CeilToInt((float)num * discount);
		}
		UnitType unit_type = skillBuyPrice.unit_type;
		int num2 = 0;
		switch (unit_type)
		{
		case UnitType.Gold:
			num2 = top_bar.GetGoldValue();
			num2 -= num;
			if (num2 >= 0)
			{
				top_bar.SetGoldValue(num2);
				item_choose.SkillBuy();
				btn_click.SetStateUpdate();
				unlock_blink.OpenBlinkSkill(item_choose.GetCustomizeTexture(), "Purchase complete!");
				if (scene_skill != null)
				{
					if (scene_skill.GetSFXOpen())
					{
						CUISound.GetInstance().Play("UI_Unlocked_weapon");
					}
				}
				else
				{
					Debug.Log("error!");
				}
				label_skill_introduce.Text = item_choose.GetSkillIntroduceEx();
				label_skill_introduce_unlock.Text = string.Empty;
			}
			else
			{
				Debug.Log("!!!you have no gold enough!!!");
			}
			break;
		case UnitType.Crystal:
			num2 = top_bar.GetCrystalValue();
			num2 -= num;
			if (num2 >= 0)
			{
				top_bar.SetCrystalValue(num2);
				item_choose.SkillBuy();
				btn_click.SetStateUpdate();
				unlock_blink.OpenBlinkSkill(item_choose.GetCustomizeTexture(), "Purchase complete!");
				if (scene_skill != null)
				{
					if (scene_skill.GetSFXOpen())
					{
						CUISound.GetInstance().Play("UI_Unlocked_weapon");
					}
				}
				else
				{
					Debug.Log("error!");
				}
				label_skill_introduce.Text = item_choose.GetSkillIntroduceEx();
				label_skill_introduce_unlock.Text = string.Empty;
			}
			else
			{
				Debug.Log("!!!you have no crystal enough!!!");
			}
			break;
		}
	}

	public void SkillUpdate()
	{
		if (!item_choose.ReachLevelMax())
		{
			if (item_choose.GetSkillBuyPrice() == null)
			{
				Debug.Log("error!");
				return;
			}
			float discount = item_choose.GetDiscount();
			bool flag = !(discount >= 1f);
			int num = item_choose.GetSkillUpdatePrice().price;
			if (flag)
			{
				num = Mathf.CeilToInt((float)num * discount);
			}
			UnitType unit_type = item_choose.GetSkillUpdatePrice().unit_type;
			int num2 = 0;
			switch (unit_type)
			{
			case UnitType.Gold:
				num2 = top_bar.GetGoldValue();
				num2 -= num;
				Debug.Log("cost:" + num + unit_type.ToString());
				if (num2 >= 0)
				{
					top_bar.SetGoldValue(num2);
					item_choose.SkillUpdate();
					if (item_choose.ReachLevelMax())
					{
						btn_click.SetStateDisable();
					}
					label_skill_introduce.Text = item_choose.GetSkillIntroduceEx();
					label_skill_introduce_unlock.Text = string.Empty;
					unlock_blink.OpenBlinkSkill(item_choose.GetCustomizeTexture(), "Upgrade complete!");
					if (scene_skill != null)
					{
						if (scene_skill.GetSFXOpen())
						{
							CUISound.GetInstance().Play("UI_Unlocked_weapon");
						}
					}
					else
					{
						Debug.Log("error!");
					}
				}
				else
				{
					Debug.Log("!!!you have no gold enough!!!");
				}
				break;
			case UnitType.Crystal:
				num2 = top_bar.GetCrystalValue();
				num2 -= num;
				Debug.Log("coss:" + num + unit_type.ToString());
				if (num2 >= 0)
				{
					top_bar.SetCrystalValue(num2);
					item_choose.SkillUpdate();
					if (item_choose.ReachLevelMax())
					{
						btn_click.SetStateDisable();
					}
					label_skill_introduce.Text = item_choose.GetSkillIntroduceEx();
					label_skill_introduce_unlock.Text = string.Empty;
					unlock_blink.OpenBlinkSkill(item_choose.GetCustomizeTexture(), "Upgrade complete!");
					if (scene_skill != null)
					{
						if (scene_skill.GetSFXOpen())
						{
							CUISound.GetInstance().Play("UI_Unlocked_weapon");
						}
					}
					else
					{
						Debug.Log("error!");
					}
				}
				else
				{
					Debug.Log("!!!you have no crystal enough!!!");
				}
				break;
			}
		}
		else
		{
			Debug.Log("you reach max level!");
		}
	}

	public void SetTopBarInfo(TUIPlayerInfo m_player_info)
	{
		if (m_player_info == null)
		{
			Debug.Log("error! no found info");
			return;
		}
		int role_id = m_player_info.role_id;
		int level = m_player_info.level;
		int exp = m_player_info.exp;
		int level_exp = m_player_info.level_exp;
		int gold = m_player_info.gold;
		int crystal = m_player_info.crystal;
		top_bar.SetAllValue(level, exp, level_exp, gold, crystal, role_id);
	}

	public void CloseBlink()
	{
		unlock_blink.CloseBlink();
	}

	public void StarsBlink()
	{
		if (item_choose == null)
		{
			Debug.Log("error!");
			return;
		}
		if (scene_skill != null)
		{
			if (scene_skill.GetSFXOpen())
			{
				CUISound.GetInstance().Play("UI_Levelup");
			}
		}
		else
		{
			Debug.Log("error!");
		}
		int skillLevelMax = item_choose.GetSkillLevelMax();
		int skillLevel = item_choose.GetSkillLevel();
		float x = label_skill_name.CalculateBounds(label_skill_name.Text).size.x;
		x *= label_skill_name.transform.localScale.x;
		Vector3 position = new Vector3(label_skill_name.transform.localPosition.x + x + 12f, label_skill_name.transform.localPosition.y, label_skill_name.transform.localPosition.z);
		if (skillLevel > 0)
		{
			level_stars.SetStars(skillLevel, position, true);
		}
		else
		{
			level_stars.SetStarsDisable();
		}
	}

	public void SetRoleBtnChoose(int m_index)
	{
		for (int i = 0; i < btn_role_list.Length; i++)
		{
			if (i == m_index)
			{
				btn_role_list[i].SetSelected(true);
			}
			else
			{
				btn_role_list[i].SetSelected(false);
			}
		}
	}

	public void SetLinkItem(TUIAllSkillInfo role_skill_info)
	{
		if (role_skill_info == null || role_skill_info.all_role_skill_list == null)
		{
			Debug.Log("no role info!");
		}
		else
		{
			if (!role_skill_info.open_link)
			{
				return;
			}
			TUISkillListInfo[] all_role_skill_list = role_skill_info.all_role_skill_list;
			TUISkillListInfo tUISkillListInfo = null;
			int role_link_id = role_skill_info.role_link_id;
			int role_skill_link_id = role_skill_info.role_skill_link_id;
			int num = 0;
			for (int i = 0; i < all_role_skill_list.Length; i++)
			{
				if (all_role_skill_list[i].id == role_link_id)
				{
					tUISkillListInfo = role_skill_info.all_role_skill_list[i];
					num = i;
					break;
				}
			}
			if (tUISkillListInfo == null)
			{
				Debug.Log("no m_role_skill_list!");
				return;
			}
			int skill_index = 0;
			TUISkillInfo[] skill_list_info = tUISkillListInfo.skill_list_info;
			for (int j = 0; j < skill_list_info.Length; j++)
			{
				if (skill_list_info[j].id == role_skill_link_id)
				{
					skill_index = j;
					break;
				}
			}
			SetRoleBtnChoose(num);
			ScrollListChoose(btn_role_list[num], skill_index);
		}
	}

	public void ShowPopupGoldToCrystal(int m_gold, int m_crystal)
	{
		if (popup_gold_to_crystal != null)
		{
			string title = "You need more gold";
			string introduce = "Buy the missing " + m_gold + " Gold?";
			popup_gold_to_crystal.Show();
			popup_gold_to_crystal.SetInfo(title, introduce, m_gold, m_crystal, UnitType.Crystal);
		}
	}

	public void HidePopupGoldToCrystal()
	{
		if (popup_gold_to_crystal != null)
		{
			popup_gold_to_crystal.Hide();
		}
	}

	public int GetGoldExchangeCount()
	{
		if (popup_gold_to_crystal != null)
		{
			return popup_gold_to_crystal.GetGoldExchangeCount();
		}
		return 0;
	}

	public int GetCrystalExchangeCount()
	{
		if (popup_gold_to_crystal != null)
		{
			return popup_gold_to_crystal.GetCrystalExchangeCount();
		}
		return 0;
	}

	public void DoGoldExchange()
	{
		if (popup_gold_to_crystal == null || top_bar == null)
		{
			Debug.Log("error!");
			return;
		}
		int goldExchangeCount = popup_gold_to_crystal.GetGoldExchangeCount();
		int crystalExchangeCount = popup_gold_to_crystal.GetCrystalExchangeCount();
		int goldValue = top_bar.GetGoldValue();
		int crystalValue = top_bar.GetCrystalValue();
		goldValue += goldExchangeCount;
		crystalValue -= crystalExchangeCount;
		if (crystalValue < 0)
		{
			Debug.Log("error!");
			return;
		}
		top_bar.SetGoldValue(goldValue);
		top_bar.SetCrystalValue(crystalValue);
	}

	public void ShowPopupCrystalNoEnough(int m_crystal)
	{
		if (popup_crystal_no_enough != null)
		{
			string title = "you're " + m_crystal + " crystals short";
			string introduce = "Get more now?";
			popup_crystal_no_enough.Show();
			popup_crystal_no_enough.SetInfo(title, introduce, m_crystal, "OK");
		}
	}

	public void HidePopupCrystalNoEnough()
	{
		if (popup_crystal_no_enough != null)
		{
			popup_crystal_no_enough.Hide();
		}
	}

	public int GetCrystalNoEnoughCount()
	{
		if (popup_crystal_no_enough != null)
		{
			return popup_crystal_no_enough.GetCrystalNoEnoughCount();
		}
		return 0;
	}

	public void ShowGoEquip()
	{
		if (popup_go_equip != null)
		{
			popup_go_equip.Show();
		}
	}

	public void HideGoEquip()
	{
		if (popup_go_equip != null)
		{
			popup_go_equip.Hide();
		}
	}

	public void ShowGoEquipAfterBuy(bool m_sfx_open_now)
	{
		if (item_choose == null)
		{
			Debug.Log("error!");
			return;
		}
		if (m_sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (item_choose.GetSkillLevel() == 1)
		{
			ShowGoEquip();
			AndroidReturnPlugin.instance.SetCurFunc(scene_skill.TUIEvent_CloseGoEquip);
		}
	}
}
