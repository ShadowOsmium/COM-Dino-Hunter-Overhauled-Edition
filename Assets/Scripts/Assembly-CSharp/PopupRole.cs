using System.Collections.Generic;
using EventCenter;
using UnityEngine;

public class PopupRole : MonoBehaviour
{
	public Top_Bar top_bar;

	public Role_Control role_control;

	public ScrollList_Role scroll_list_role;

	public TUILabel label_title;

	public TUILabel label_introduce;

	public TUILabel label_introduce_unlock;

	public PopupRoleBtnBuy btn_buy;

	public UnlockBlink unlock_blink;

	private ScrollList_RoleItem item_choose;

	private int role_now_id;

	public Scene_Tavern scene_tavern;

	public PopupRoleUnlock popup_unlock;

	public PopupRoleBuy popup_buy;

	public PopupGoldToCrystal popup_gold_to_crystal;

	public PopupCrystalNoEnough popup_crystal_no_enough;

	public RoleActiveSkill role_active_skill;

	public Popup_Show popup_active_skill;

	public PopupGoEquip popup_go_equip;

	private void Start()
	{
	}

	private void Update()
	{
		CheckScrollChoose();
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
		btn_buy.SetStateDisable();
		role_now_id = role_id;
		SetRoleID(role_id, m_player_info.avatar_model, m_player_info.avatar_head, m_player_info.avatar_body, m_player_info.avatar_leg, m_player_info.avatar_headup, m_player_info.avatar_neck, m_player_info.avatar_bracelet);
		if (m_player_info.default_weapon > 0)
		{
			SetRoleWeapon(m_player_info.default_weapon);
		}
	}

	public void AddScrollListItem(TUIAllRoleInfo m_all_role_info)
	{
		scroll_list_role.AddScrollListItem(m_all_role_info);
	}

	public void CheckScrollChoose()
	{
		ScrollList_RoleItem itemChoose = scroll_list_role.GetItemChoose();
		if (!(item_choose != itemChoose))
		{
			return;
		}
		item_choose = itemChoose;
		if (!(item_choose != null))
		{
			return;
		}
		if (scene_tavern != null)
		{
			if (scene_tavern.GetSFXOpen())
			{
				CUISound.GetInstance().Play("UI_Drag");
			}
		}
		else
		{
			Debug.Log("error!");
		}
		SetInfo(item_choose.GetRoleInfo());
		int id = item_choose.GetRoleInfo().id;
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneTavern(TUIEvent.SceneTavernEventType.TUIEvent_RolesChoose, id));
	}

	public void SetInfo(TUIRoleInfo m_info)
	{
		if (m_info == null)
		{
			Debug.Log("error! no info!");
		}
		label_title.Text = m_info.name;
		label_introduce.Text = m_info.introduce;
		if (!m_info.unlock)
		{
			label_introduce_unlock.Text = m_info.introduce_unlock;
			btn_buy.SetStateUnlock();
		}
		else if (!m_info.do_buy)
		{
			label_introduce_unlock.Text = string.Empty;
			btn_buy.SetStateBuy();
		}
		else
		{
			label_introduce_unlock.Text = string.Empty;
			btn_buy.SetStateDisable();
		}
		SetRoleID(m_info.id, m_info.m_nModelID, m_info.m_nAvatarHead, m_info.m_nAvatarUpper, m_info.m_nAvatarLower, m_info.m_nAvatarHeadup, m_info.m_nAvatarNeck, m_info.m_nAvatarBracelet);
		SetActiveSkill(m_info.active_skill_list);
	}

	public void SetRoleWeapon(int id)
	{
		role_control.ChangeWeapon(id);
	}

	public void SetRoleID(int role_id, int model, int head, int upper, int lower, int headup, int neck, int bracelet)
	{
		role_control.ChangeRole(model);
		if (TUIMappingInfo.Instance().m_GetAvatarModel != null)
		{
			GameObject modelprefab = null;
			Texture modeltexture = null;
			if (TUIMappingInfo.Instance().m_GetAvatarModel(head, role_id, ref modelprefab, ref modeltexture))
			{
				role_control.ChangeAvatar(0, modelprefab, modeltexture);
			}
			if (TUIMappingInfo.Instance().m_GetAvatarModel(upper, role_id, ref modelprefab, ref modeltexture))
			{
				role_control.ChangeAvatar(1, modelprefab, modeltexture);
			}
			if (TUIMappingInfo.Instance().m_GetAvatarModel(lower, role_id, ref modelprefab, ref modeltexture))
			{
				role_control.ChangeAvatar(2, modelprefab, modeltexture);
			}
			if (TUIMappingInfo.Instance().m_GetAvatarModel(headup, role_id, ref modelprefab, ref modeltexture))
			{
				role_control.ChangeAvatarEffect(3, modelprefab);
			}
			if (TUIMappingInfo.Instance().m_GetAvatarModel(bracelet, role_id, ref modelprefab, ref modeltexture))
			{
				role_control.ChangeAvatarEffect(4, modelprefab);
				role_control.ChangeAvatarEffect(5, modelprefab);
			}
			if (TUIMappingInfo.Instance().m_GetAvatarModel(neck, role_id, ref modelprefab, ref modeltexture))
			{
				role_control.ChangeAvatarEffect(6, modelprefab);
			}
		}
		if (role_control.IsEmptyWeapon())
		{
			role_control.ChangeWeapon(1);
		}
		role_control.SetRoleFixedRotation(new Vector3(0f, 0f, 0f));
	}

	public int GetRoleChooseID()
	{
		return item_choose.GetRoleInfo().id;
	}

	public PopupRoleBtnBuy.PopupRoleBuyState GetRoleBuyState()
	{
		return btn_buy.GetState();
	}

	public void SetRoleUnlock(bool m_open_sfx)
	{
		int price = item_choose.GetRoleInfo().unlock_price.price;
		switch (item_choose.GetRoleInfo().unlock_price.unit_type)
		{
		case UnitType.Gold:
		{
			int goldValue = top_bar.GetGoldValue();
			goldValue -= price;
			if (goldValue >= 0)
			{
				top_bar.SetGoldValue(goldValue);
				label_introduce_unlock.Text = string.Empty;
				if (m_open_sfx)
				{
					CUISound.GetInstance().Play("UI_Trade");
				}
				break;
			}
			Debug.Log("you have no gold enough!");
			return;
		}
		case UnitType.Crystal:
		{
			int crystalValue = top_bar.GetCrystalValue();
			crystalValue -= price;
			if (crystalValue >= 0)
			{
				top_bar.SetCrystalValue(crystalValue);
				label_introduce_unlock.Text = string.Empty;
				if (m_open_sfx)
				{
					CUISound.GetInstance().Play("UI_Crystal");
				}
				break;
			}
			Debug.Log("you have no crystal enough!");
			return;
		}
		}
		item_choose.DoUnlock();
		int price2 = item_choose.GetRoleInfo().do_buy_price.price;
		UnitType unit_type = item_choose.GetRoleInfo().do_buy_price.unit_type;
		btn_buy.SetStateBuy();
		int id = item_choose.GetRoleInfo().id;
		unlock_blink.OpenBlinkRole(id, "New Character Unlocked For Purchase!");
		if (scene_tavern != null)
		{
			if (scene_tavern.GetSFXOpen())
			{
				CUISound.GetInstance().Play("UI_Unlocked_weapon");
			}
		}
		else
		{
			Debug.Log("error!");
		}
	}

	public void SetRoleBuy(bool m_open_sfx)
	{
		TUIRoleInfo roleInfo = item_choose.GetRoleInfo();
		if (roleInfo == null)
		{
			Debug.Log("no info!");
			return;
		}
		if (!roleInfo.is_active_buy)
		{
			float discount = roleInfo.discount;
			bool flag = !(discount >= 1f);
			TUIPriceInfo do_buy_price = roleInfo.do_buy_price;
			if (do_buy_price == null)
			{
				Debug.Log("no info!");
				return;
			}
			int num = do_buy_price.price;
			if (flag)
			{
				num = Mathf.CeilToInt((float)num * discount);
			}
			switch (do_buy_price.unit_type)
			{
			case UnitType.Gold:
			{
				int goldValue = top_bar.GetGoldValue();
				goldValue -= num;
				if (goldValue >= 0)
				{
					top_bar.SetGoldValue(goldValue);
					if (m_open_sfx)
					{
						CUISound.GetInstance().Play("UI_Trade");
					}
					break;
				}
				Debug.Log("you have no gold enough!");
				return;
			}
			case UnitType.Crystal:
			{
				int crystalValue = top_bar.GetCrystalValue();
				crystalValue -= num;
				if (crystalValue >= 0)
				{
					top_bar.SetCrystalValue(crystalValue);
					if (m_open_sfx)
					{
						CUISound.GetInstance().Play("UI_Crystal");
					}
					break;
				}
				Debug.Log("you have no crystal enough!");
				return;
			}
			}
		}
		item_choose.DoBuy();
		btn_buy.SetStateDisable();
		int id = roleInfo.id;
		unlock_blink.OpenBlinkRole(id, "Purchase complete!");
		if (scene_tavern != null)
		{
			if (scene_tavern.GetSFXOpen())
			{
				CUISound.GetInstance().Play("UI_Unlocked_weapon");
			}
		}
		else
		{
			Debug.Log("error!");
		}
	}

	public void SetRoleRotation(float m_wparam, float m_lparam)
	{
		role_control.SetRotation(m_wparam, m_lparam);
	}

	public void UpdateNewMark(Dictionary<int, NewMarkType> m_new_mark_list)
	{
		scroll_list_role.UpdateNewMark(m_new_mark_list);
	}

	public void CloseBlink()
	{
		unlock_blink.CloseBlink();
	}

	public void ShowPopupUnlock()
	{
		if (popup_unlock != null)
		{
			popup_unlock.Show();
			popup_unlock.SetInfo(item_choose);
		}
	}

	public void ShowPopupBuy()
	{
		if (popup_buy != null)
		{
			popup_buy.Show();
			popup_buy.SetInfo(item_choose);
		}
	}

	public void ClosePopupUnlock()
	{
		if (popup_unlock != null)
		{
			popup_unlock.Hide();
		}
	}

	public void ClosePopupBuy()
	{
		if (popup_buy != null)
		{
			popup_buy.Hide();
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

	public void SetActiveSkill(List<TUIPopupInfo> m_active_skill_list)
	{
		if (role_active_skill == null || m_active_skill_list == null)
		{
			Debug.Log("warning! no active skill.");
		}
		role_active_skill.SetInfo(m_active_skill_list);
	}

	public void ShowPopupActiveSkill(TUIControl m_control)
	{
		if (popup_active_skill == null || m_control == null)
		{
			Debug.Log("error!");
			return;
		}
		popup_active_skill.Show(PopupType.Skills01, null);
		BtnItem_Item component = m_control.GetComponent<BtnItem_Item>();
		if (component != null)
		{
			TUIPopupInfo info = component.GetInfo();
			popup_active_skill.SetSimpleInfo(info);
		}
	}

	public void HidePopupActiveSkill()
	{
		if (popup_active_skill == null)
		{
			Debug.Log("error!");
		}
		else
		{
			popup_active_skill.Hide();
		}
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
		if (item_choose.GetBuy())
		{
			ShowGoEquip();
			AndroidReturnPlugin.instance.SetCurFunc(scene_tavern.TUIEvent_CloseGoEquip);
		}
	}

	public bool IsGetActiveRole()
	{
		if (item_choose == null)
		{
			Debug.Log("no item_choose!");
			return false;
		}
		return item_choose.IsActiveRole();
	}
}
