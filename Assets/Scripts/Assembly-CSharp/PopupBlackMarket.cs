using System.Collections.Generic;
using UnityEngine;

public class PopupBlackMarket : MonoBehaviour
{
	public Top_Bar top_bar;

	public Role_Control role_control;

	public TUIButtonClick btn_buy;

	public TUILabel label_alreadygain;

	public LabelInfo_Weapon labelinfo;

	public TUILabel label_title;

	public TUILabel label_role_name;

	public ScrollList_BlackMarket scrolllist_blackmarket;

	public UnlockBlink unlock_blink;

	public PopupGoldToCrystal popup_gold_to_crystal;

	public PopupCrystalNoEnough popup_crystal_no_enough;

	public Dictionary<int, TUIBlackMarketItem> m_dictBlackMarketItem;

	public PopupGoEquip popup_go_equip;

	public int m_nCurBlackID;

	public TUILabel m_TimeLabel;

	private float m_fRefreshTimeInveral;

	private void Awake()
	{
		m_dictBlackMarketItem = new Dictionary<int, TUIBlackMarketItem>();
	}

	private void Start()
	{
		if (label_title != null)
		{
			label_title.Text = string.Empty;
		}
		if (btn_buy != null)
		{
			btn_buy.gameObject.SetActiveRecursively(false);
		}
		if (labelinfo != null)
		{
			labelinfo.SetNull();
		}
		if (label_alreadygain != null)
		{
			label_alreadygain.gameObject.active = false;
		}
	}

	private void Update()
	{
		UpdateTime(Time.deltaTime);
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
		string avatar_name = m_player_info.avatar_name;
		top_bar.SetAllValue(level, exp, level_exp, gold, crystal, role_id);
		if (m_player_info.avatar_model < 1)
		{
			SetRoleModel(role_id);
		}
		else
		{
			SetRoleModel(m_player_info.avatar_model);
			if (TUIMappingInfo.Instance().m_GetAvatarModel != null)
			{
				GameObject modelprefab = null;
				Texture modeltexture = null;
				if (m_player_info.avatar_head > 0 && TUIMappingInfo.Instance().m_GetAvatarModel(m_player_info.avatar_head, role_id, ref modelprefab, ref modeltexture))
				{
					SetRoleAvatar(0, modelprefab, modeltexture);
				}
				if (m_player_info.avatar_body > 0 && TUIMappingInfo.Instance().m_GetAvatarModel(m_player_info.avatar_body, role_id, ref modelprefab, ref modeltexture))
				{
					SetRoleAvatar(1, modelprefab, modeltexture);
				}
				if (m_player_info.avatar_leg > 0 && TUIMappingInfo.Instance().m_GetAvatarModel(m_player_info.avatar_leg, role_id, ref modelprefab, ref modeltexture))
				{
					SetRoleAvatar(2, modelprefab, modeltexture);
				}
				if (m_player_info.avatar_headup > 0 && TUIMappingInfo.Instance().m_GetAvatarModel(m_player_info.avatar_headup, role_id, ref modelprefab, ref modeltexture))
				{
					SetRoleAvatarEffect(3, modelprefab);
				}
				if (m_player_info.avatar_neck > 0 && TUIMappingInfo.Instance().m_GetAvatarModel(m_player_info.avatar_neck, role_id, ref modelprefab, ref modeltexture))
				{
					SetRoleAvatarEffect(6, modelprefab);
				}
				if (m_player_info.avatar_bracelet > 0 && TUIMappingInfo.Instance().m_GetAvatarModel(m_player_info.avatar_bracelet, role_id, ref modelprefab, ref modeltexture))
				{
					SetRoleAvatarEffect(4, modelprefab);
					SetRoleAvatarEffect(5, modelprefab);
				}
			}
		}
		if (m_player_info.default_weapon > 0)
		{
			SetRoleWeapon(m_player_info.default_weapon);
		}
		role_control.SetRoleFixedRotation(new Vector3(0f, -30f, 0f));
		if (label_role_name != null)
		{
			label_role_name.Text = avatar_name;
		}
	}

	public void SetRoleModel(int model)
	{
		if (!(role_control == null))
		{
			role_control.ChangeRole(model);
			role_control.SetRoleFixedRotation(new Vector3(0f, -40f, 0f));
		}
	}

	public void SetRoleAvatar(int index, GameObject modelprefab, Texture tex)
	{
		if (!(role_control == null))
		{
			role_control.ChangeAvatar(index, modelprefab, tex);
		}
	}

	public void SetRoleAvatarEffect(int index, GameObject modelprefab)
	{
		if (!(role_control == null))
		{
			role_control.ChangeAvatarEffect(index, modelprefab);
		}
	}

	public void SetRoleWeapon(int id)
	{
		role_control.ChangeWeapon(id);
	}

	public void SetRoleRotation(float wparam, float lparam)
	{
		if (role_control != null)
		{
			role_control.SetRotation(wparam, lparam);
		}
	}

	public void SetGoodsInfo(TUIBlackMarketInfo blackmarkinfo)
	{
		if (blackmarkinfo == null || blackmarkinfo.m_ltBlackMarketItem.Count < 1 || scrolllist_blackmarket == null)
		{
			return;
		}
		foreach (TUIBlackMarketItem item in blackmarkinfo.m_ltBlackMarketItem)
		{
			if (m_dictBlackMarketItem.ContainsKey(item.m_nBlackMarketID))
			{
				Debug.LogWarning(item.m_nBlackMarketID + " is already exist!");
			}
			else
			{
				m_dictBlackMarketItem.Add(item.m_nBlackMarketID, item);
			}
		}
		scrolllist_blackmarket.SetItems(blackmarkinfo.m_ltBlackMarketItem);
		ChangeNowItemInfo(blackmarkinfo.m_ltBlackMarketItem[0].m_nBlackMarketID);
	}

	public void SetInfo(TUIBlackMarketItem blackmarkitem)
	{
		if (blackmarkitem == null)
		{
			return;
		}
		if (m_TimeLabel != null && blackmarkitem.m_nBlackMarketID == m_nCurBlackID)
		{
			m_TimeLabel.Text = MyUtils.TimeToString(blackmarkitem.m_fLeftTime);
		}
		if (label_title != null)
		{
			label_title.Text = blackmarkitem.m_sName;
		}
		if (labelinfo != null)
		{
			if (blackmarkitem.IsWeapon())
			{
				labelinfo.SetWeaponInfo((int)blackmarkitem.m_fDamage, blackmarkitem.m_fShootSpeed, blackmarkitem.m_nBlastRadius, blackmarkitem.m_nKnockBack, blackmarkitem.m_nCapcity, (int)blackmarkitem.m_fDamageMax, true, string.Empty);
			}
			else if (blackmarkitem.IsArmor() || blackmarkitem.IsAccessory())
			{
				labelinfo.SetArmorAccessoryInfo(blackmarkitem.m_sDesc, blackmarkitem.m_nDefence, blackmarkitem.m_nDefenceMax, true, string.Empty);
			}
		}
		if (blackmarkitem.m_bAlreadyGain)
		{
			if (label_alreadygain != null)
			{
				label_alreadygain.gameObject.active = true;
			}
			ShowBuyButton(false);
			return;
		}
		if (label_alreadygain != null)
		{
			label_alreadygain.gameObject.active = false;
		}
		ShowBuyButton(true);
		if (blackmarkitem.m_fLeftTime > 0f)
		{
			EnableBuyButton(true);
		}
		else
		{
			EnableBuyButton(false);
		}
		if (btn_buy != null)
		{
			ImgPrice component = btn_buy.GetComponent<ImgPrice>();
			if (component != null)
			{
				component.SetInfo(blackmarkitem.m_Price);
			}
		}
	}

	public void ChangeNowItemInfo(int nBlackID)
	{
		if (m_nCurBlackID == nBlackID || !m_dictBlackMarketItem.ContainsKey(nBlackID))
		{
			return;
		}
		if (m_nCurBlackID > 0)
		{
			TUIBlackMarketItem tUIBlackMarketItem = m_dictBlackMarketItem[m_nCurBlackID];
			if (tUIBlackMarketItem != null)
			{
				int avatarid = -1;
				if (TUIMappingInfo.Instance().m_GetCharacterDefaultAvatar != null && TUIMappingInfo.Instance().m_GetCharacterDefaultAvatar(top_bar.GetRoleID(), tUIBlackMarketItem.m_WeaponType, ref avatarid))
				{
					GameObject modelprefab = null;
					Texture modeltexture = null;
					if (TUIMappingInfo.Instance().m_GetAvatarModel != null)
					{
						TUIMappingInfo.Instance().m_GetAvatarModel(avatarid, top_bar.GetRoleID(), ref modelprefab, ref modeltexture);
					}
					switch (tUIBlackMarketItem.m_WeaponType)
					{
					case WeaponType.Armor_Head:
						SetRoleAvatar(0, modelprefab, modeltexture);
						break;
					case WeaponType.Armor_Body:
						SetRoleAvatar(1, modelprefab, modeltexture);
						break;
					case WeaponType.Armor_Leg:
						SetRoleAvatar(2, modelprefab, modeltexture);
						break;
					case WeaponType.Accessory_Halo:
						SetRoleAvatarEffect(3, modelprefab);
						break;
					case WeaponType.Accessory_Necklace:
						SetRoleAvatarEffect(6, modelprefab);
						break;
					case WeaponType.Armor_Bracelet:
						SetRoleAvatarEffect(4, modelprefab);
						SetRoleAvatarEffect(5, modelprefab);
						break;
					}
				}
			}
		}
		m_nCurBlackID = nBlackID;
		TUIBlackMarketItem tUIBlackMarketItem2 = m_dictBlackMarketItem[nBlackID];
		if (tUIBlackMarketItem2 == null)
		{
			if (label_title != null)
			{
				label_title.Text = "--";
			}
			if (labelinfo != null)
			{
				labelinfo.SetNull();
			}
			return;
		}
		SetInfo(tUIBlackMarketItem2);
		if (tUIBlackMarketItem2.IsWeapon())
		{
			SetRoleWeapon(tUIBlackMarketItem2.m_nItemID);
			return;
		}
		GameObject modelprefab2 = null;
		Texture modeltexture2 = null;
		if (TUIMappingInfo.Instance().m_GetAvatarModel != null && TUIMappingInfo.Instance().m_GetAvatarModel(tUIBlackMarketItem2.m_nItemID, top_bar.GetRoleID(), ref modelprefab2, ref modeltexture2))
		{
			switch (tUIBlackMarketItem2.m_WeaponType)
			{
			case WeaponType.Armor_Head:
				SetRoleAvatar(0, modelprefab2, modeltexture2);
				break;
			case WeaponType.Armor_Body:
				SetRoleAvatar(1, modelprefab2, modeltexture2);
				break;
			case WeaponType.Armor_Leg:
				SetRoleAvatar(2, modelprefab2, modeltexture2);
				break;
			case WeaponType.Accessory_Halo:
				SetRoleAvatarEffect(3, modelprefab2);
				break;
			case WeaponType.Accessory_Necklace:
				SetRoleAvatarEffect(6, modelprefab2);
				break;
			case WeaponType.Armor_Bracelet:
				SetRoleAvatarEffect(4, modelprefab2);
				SetRoleAvatarEffect(5, modelprefab2);
				break;
			case WeaponType.Accessory_Badge:
				break;
			}
		}
	}

	public TUIBlackMarketItem GetNowItem()
	{
		if (m_nCurBlackID == -1)
		{
			return null;
		}
		if (!m_dictBlackMarketItem.ContainsKey(m_nCurBlackID))
		{
			return null;
		}
		return m_dictBlackMarketItem[m_nCurBlackID];
	}

	public void OnBuyItem()
	{
		TUIBlackMarketItem nowItem = GetNowItem();
		if (nowItem == null)
		{
			return;
		}
		if (nowItem.m_Price.unit_type == UnitType.Crystal)
		{
			top_bar.SetCrystalValue(top_bar.GetCrystalValue() - nowItem.m_Price.price);
			CUISound.GetInstance().Play("UI_Crystal");
		}
		else if (nowItem.m_Price.unit_type == UnitType.Gold)
		{
			top_bar.SetGoldValue(top_bar.GetGoldValue() - nowItem.m_Price.price);
			CUISound.GetInstance().Play("UI_Trade");
		}
		nowItem.m_bAlreadyGain = true;
		SetInfo(nowItem);
		if (unlock_blink != null)
		{
			string text = nowItem.m_sIcon;
			if (text.Length < 1)
			{
				text = TUIMappingInfo.Instance().GetWeaponTexture(nowItem.m_nItemID);
			}
			string iconPath = nowItem.GetIconPath();
			if (iconPath.Length > 0)
			{
				CUISound.GetInstance().Play("UI_Unlocked_weapon");
				unlock_blink.OpenBlinkWeapon("Purchase complete!", true, iconPath + "/" + text);
			}
		}
	}

	public void CloseUnlockBlink()
	{
		if (unlock_blink != null)
		{
			unlock_blink.CloseBlink();
		}
	}

	public void ShowGoEquipAfterBuy(bool m_sfx_open_now)
	{
		if (m_nCurBlackID > 0 && m_dictBlackMarketItem.ContainsKey(m_nCurBlackID))
		{
			if (m_sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_go_equip != null)
			{
				popup_go_equip.Show();
			}
		}
	}

	public void HideGoEquip()
	{
		if (popup_go_equip != null)
		{
			popup_go_equip.Hide();
		}
	}

	private void UpdateTime(float delta_time)
	{
		m_fRefreshTimeInveral += delta_time;
		if (m_fRefreshTimeInveral >= 1f)
		{
			m_fRefreshTimeInveral = 0f;
		}
		foreach (TUIBlackMarketItem value in m_dictBlackMarketItem.Values)
		{
			if (value.m_fLeftTime <= 0f)
			{
				continue;
			}
			value.m_fLeftTime -= delta_time;
			if (value.m_fLeftTime <= 0f)
			{
				value.m_fLeftTime = 0f;
				if (GetNowItem() == value)
				{
					EnableBuyButton(false);
				}
			}
			if ((m_fRefreshTimeInveral == 0f || value.m_fLeftTime == 0f) && m_nCurBlackID == value.m_nBlackMarketID && m_TimeLabel != null)
			{
				m_TimeLabel.Text = MyUtils.TimeToString(value.m_fLeftTime);
			}
		}
	}

	public void ShowBuyButton(bool bShow)
	{
		if (!(btn_buy == null))
		{
			btn_buy.gameObject.SetActiveRecursively(bShow);
			if (bShow)
			{
				btn_buy.Reset();
			}
		}
	}

	public void EnableBuyButton(bool bEnable)
	{
		if (!(btn_buy == null))
		{
			btn_buy.Disable(!bEnable);
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

	public void HidePopupGoldToCrystal()
	{
		if (popup_gold_to_crystal != null)
		{
			popup_gold_to_crystal.Hide();
		}
	}

	public void HidePopupCrystalNoEnough()
	{
		if (popup_crystal_no_enough != null)
		{
			popup_crystal_no_enough.Hide();
		}
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

	public string TimeToString(float fTime)
	{
		if (fTime <= 0f)
		{
			return "--:--";
		}
		int num = Mathf.FloorToInt(fTime);
		int num2 = num / 60;
		num %= 60;
		int num3 = num2 / 60;
		num2 %= 60;
		string text = string.Empty;
		if (num3 > 0)
		{
			text = text + num3.ToString("d2") + ":";
		}
		text = text + num2.ToString("d2") + ":";
		return text + num.ToString("d2");
	}
}
