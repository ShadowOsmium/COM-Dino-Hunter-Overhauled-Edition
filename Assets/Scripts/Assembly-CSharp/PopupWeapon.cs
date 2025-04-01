using System.Collections.Generic;
using EventCenter;
using UnityEngine;

public class PopupWeapon : MonoBehaviour
{
	public Top_Bar top_bar;

	public Role_Control role_control;

	public WeaponKindItem weapon_kind_item;

	public LevelStarsEx level_stars;

	public LabelInfo_Weapon label_info_weapon;

	public TUILabel label_title;

	public PopupWeaponUpdate popup_weapon_update;

	public PopupWeaponSupplement popup_weapon_supplement;

	public PopupWeaponBuy popup_weapon_buy;

	public UnlockBlink unlock_blink;

	public PopupGoldToCrystal popup_gold_to_crystal;

	public PopupCrystalNoEnough popup_crystal_no_enough;

	private TUIWeaponInfo weapon_info;

	public ScrollList_Weapon prefab_scroll_list_weapon;

	protected kShopWeaponCategory m_nCurWeaponCategory;

	protected Dictionary<kShopWeaponCategory, ScrollList_Weapon> m_dictScrollListWeapon;

	public Scene_Forge scene_forge;

	private TUISupplementInfo m_SupplementInfo;

	public PopupGoEquip popup_go_equip;

	public PopupTips popup_tips;

	private bool empty_info;

	private Vector3 normal_scroll_pos = new Vector3(84f, 15f, -3f);

	public TUIWeaponAttributeInfo m_curWeaponAttributeInfo { get; private set; }

	private void Awake()
	{
		m_dictScrollListWeapon = new Dictionary<kShopWeaponCategory, ScrollList_Weapon>();
	}

	private void Start()
	{
	}

	private void Update()
	{
		CheckScrollChoose();
	}

	public void AddScrollList(kShopWeaponCategory nCategory, ScrollList_Weapon ltScrollListWeapon)
	{
		if (!m_dictScrollListWeapon.ContainsKey(nCategory))
		{
			m_dictScrollListWeapon.Add(nCategory, ltScrollListWeapon);
		}
	}

	public ScrollList_Weapon GetScrollList(kShopWeaponCategory nCategory)
	{
		if (!m_dictScrollListWeapon.ContainsKey(nCategory))
		{
			return null;
		}
		return m_dictScrollListWeapon[nCategory];
	}

	public void SetWeaponInfo(TUIWeaponInfo m_weapon_info)
	{
		weapon_info = m_weapon_info;
		weapon_kind_item.SetMark(kShopWeaponCategory.Melee, weapon_info.GetMark(kShopWeaponCategory.Melee));
		weapon_kind_item.SetMark(kShopWeaponCategory.Crossbow, weapon_info.GetMark(kShopWeaponCategory.Crossbow));
		weapon_kind_item.SetMark(kShopWeaponCategory.AutoRifle, weapon_info.GetMark(kShopWeaponCategory.AutoRifle));
		weapon_kind_item.SetMark(kShopWeaponCategory.ShotGun, weapon_info.GetMark(kShopWeaponCategory.ShotGun));
		weapon_kind_item.SetMark(kShopWeaponCategory.HoldGun, weapon_info.GetMark(kShopWeaponCategory.HoldGun));
		weapon_kind_item.SetMark(kShopWeaponCategory.Rocket, weapon_info.GetMark(kShopWeaponCategory.Rocket));
		weapon_kind_item.SetMark(kShopWeaponCategory.Armor, weapon_info.GetMark(kShopWeaponCategory.Armor));
		weapon_kind_item.SetMark(kShopWeaponCategory.Accessory, weapon_info.GetMark(kShopWeaponCategory.Accessory));
		if (m_weapon_info.m_nLinkCategory != 0)
		{
			SetWeaponKindItem(m_weapon_info.m_nLinkCategory);
			if (m_weapon_info.m_nLinkID > 0)
			{
				ScrollList_Weapon scrollList = GetScrollList(m_weapon_info.m_nLinkCategory);
				if (scrollList != null)
				{
					scrollList.SetItemChoose(m_weapon_info.m_nLinkID);
				}
			}
		}
		else
		{
			SetWeaponKindItem(kShopWeaponCategory.Melee);
		}
	}

	public void RefreshMark(Dictionary<int, NewMarkType> dictMark)
	{
		if (weapon_info == null)
		{
			return;
		}
		weapon_info.RefreshMark(dictMark);
		foreach (ScrollList_Weapon value in m_dictScrollListWeapon.Values)
		{
			value.RefreshMark();
		}
		weapon_kind_item.SetMark(kShopWeaponCategory.Melee, weapon_info.GetMark(kShopWeaponCategory.Melee));
		weapon_kind_item.SetMark(kShopWeaponCategory.Crossbow, weapon_info.GetMark(kShopWeaponCategory.Crossbow));
		weapon_kind_item.SetMark(kShopWeaponCategory.AutoRifle, weapon_info.GetMark(kShopWeaponCategory.AutoRifle));
		weapon_kind_item.SetMark(kShopWeaponCategory.ShotGun, weapon_info.GetMark(kShopWeaponCategory.ShotGun));
		weapon_kind_item.SetMark(kShopWeaponCategory.HoldGun, weapon_info.GetMark(kShopWeaponCategory.HoldGun));
		weapon_kind_item.SetMark(kShopWeaponCategory.Rocket, weapon_info.GetMark(kShopWeaponCategory.Rocket));
		weapon_kind_item.SetMark(kShopWeaponCategory.Armor, weapon_info.GetMark(kShopWeaponCategory.Armor));
		weapon_kind_item.SetMark(kShopWeaponCategory.Accessory, weapon_info.GetMark(kShopWeaponCategory.Accessory));
	}

	public void CheckScrollChoose()
	{
		if (m_nCurWeaponCategory == kShopWeaponCategory.None)
		{
			if (!empty_info)
			{
				empty_info = true;
				SetInfo(null);
			}
			return;
		}
		empty_info = false;
		ScrollList_Weapon scrollList = GetScrollList(m_nCurWeaponCategory);
		if (scrollList == null)
		{
			return;
		}
		ScrollList_WeaponItem itemChoose = scrollList.GetItemChoose();
		if (itemChoose == null)
		{
			return;
		}
		TUIWeaponAttributeInfo weaponAttributeInfo = itemChoose.GetWeaponAttributeInfo();
		if (weaponAttributeInfo != null)
		{
			if (m_curWeaponAttributeInfo == weaponAttributeInfo)
			{
				return;
			}
			CUISound.GetInstance().Play("UI_Drag");
			if (m_curWeaponAttributeInfo != null)
			{
				int avatarid = -1;
				if (TUIMappingInfo.Instance().m_GetCharacterDefaultAvatar != null && TUIMappingInfo.Instance().m_GetCharacterDefaultAvatar(top_bar.GetRoleID(), m_curWeaponAttributeInfo.m_WeaponType, ref avatarid))
				{
					GameObject modelprefab = null;
					Texture modeltexture = null;
					if (TUIMappingInfo.Instance().m_GetAvatarModel != null)
					{
						TUIMappingInfo.Instance().m_GetAvatarModel(avatarid, top_bar.GetRoleID(), ref modelprefab, ref modeltexture);
					}
					switch (m_curWeaponAttributeInfo.m_WeaponType)
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
			SetInfo(weaponAttributeInfo);
			if (weaponAttributeInfo.IsWeapon())
			{
				SetRoleWeapon(weaponAttributeInfo.m_nID);
			}
			else
			{
				GameObject modelprefab2 = null;
				Texture modeltexture2 = null;
				if (TUIMappingInfo.Instance().m_GetAvatarModel != null && TUIMappingInfo.Instance().m_GetAvatarModel(weaponAttributeInfo.m_nID, top_bar.GetRoleID(), ref modelprefab2, ref modeltexture2))
				{
					switch (weaponAttributeInfo.m_WeaponType)
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
					}
				}
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneForge(TUIEvent.SceneForgeEventType.TUIEvent_WeaponChoose, weaponAttributeInfo.m_nID, (int)weaponAttributeInfo.m_WeaponType));
		}
		else
		{
			SetInfo(null);
		}
		m_curWeaponAttributeInfo = weaponAttributeInfo;
	}

	public void SetInfo(TUIWeaponAttributeInfo weaponattributeinfo)
	{
		if (weaponattributeinfo == null)
		{
			Debug.LogError("NULL INFO!!");
			if (level_stars != null)
			{
				level_stars.SetStarsDisable();
			}
			if (label_info_weapon != null)
			{
				label_info_weapon.SetNull();
			}
			if (popup_weapon_buy != null)
			{
				popup_weapon_buy.SetStateDisable();
			}
			if (level_stars != null)
			{
				level_stars.SetStarsDisable();
			}
			return;
		}
		TUIWeaponLevelInfo tUIWeaponLevelInfo = weaponattributeinfo.Get((weaponattributeinfo.m_nLevel < 1) ? 1 : weaponattributeinfo.m_nLevel);
		if (tUIWeaponLevelInfo == null)
		{
			return;
		}
		label_title.Text = weaponattributeinfo.m_sName;
		if (weaponattributeinfo.m_nLevel > 0)
		{
			float x = label_title.CalculateBounds(label_title.Text).size.x;
			x *= label_title.transform.localScale.x;
			Vector3 position = new Vector3(label_title.transform.localPosition.x + x + 12f, label_title.transform.localPosition.y, label_title.transform.localPosition.z);
			level_stars.SetStars(weaponattributeinfo.m_nLevel, position);
		}
		else
		{
			level_stars.SetStarsDisable();
		}
		if (weaponattributeinfo.IsArmor() || weaponattributeinfo.IsAccessory())
		{
			int def_max = 0;
			TUIWeaponLevelInfo tUIWeaponLevelInfo2 = weaponattributeinfo.Get(weaponattributeinfo.m_nLevelMax);
			if (tUIWeaponLevelInfo2 != null)
			{
				def_max = tUIWeaponLevelInfo2.m_nDefence;
			}
			label_info_weapon.SetArmorAccessoryInfo(tUIWeaponLevelInfo.m_sDesc, tUIWeaponLevelInfo.m_nDefence, def_max, weaponattributeinfo.m_bUnlock, weaponattributeinfo.m_sUnlockStr);
		}
		else if (weaponattributeinfo.IsWeapon())
		{
			int damage_max = 0;
			TUIWeaponLevelInfo tUIWeaponLevelInfo3 = weaponattributeinfo.Get(weaponattributeinfo.m_nLevelMax);
			if (tUIWeaponLevelInfo3 != null)
			{
				damage_max = tUIWeaponLevelInfo3.m_nDamage;
			}
			label_info_weapon.SetWeaponInfo(tUIWeaponLevelInfo.m_nDamage, tUIWeaponLevelInfo.m_fShootRate, tUIWeaponLevelInfo.m_fBlastRadius, tUIWeaponLevelInfo.m_nKnockBack, tUIWeaponLevelInfo.m_nCapcity, damage_max, weaponattributeinfo.m_bUnlock, weaponattributeinfo.m_sUnlockStr);
		}
		if (!weaponattributeinfo.m_bUnlock)
		{
			popup_weapon_buy.SetStateDisable();
		}
		else if (weaponattributeinfo.m_nLevel >= weaponattributeinfo.m_nLevelMax)
		{
			popup_weapon_buy.SetStateDisable();
		}
		else if (weaponattributeinfo.m_nLevel <= 0)
		{
			popup_weapon_buy.SetStateCraft(weaponattributeinfo.m_bActive);
		}
		else
		{
			popup_weapon_buy.SetStateUpdate();
		}
	}

	public void OpenWeaponUpdate()
	{
		if (m_curWeaponAttributeInfo == null)
		{
			Debug.Log("error! no item_choose");
			return;
		}
		popup_weapon_update.ShowWeaponUpdate();
		popup_weapon_update.SetInfo(m_curWeaponAttributeInfo);
	}

	public void CloseWeaponUpdate()
	{
		popup_weapon_update.HideWeaponUpdate();
	}

	public void OpenWeaponSupplement(TUISupplementInfo supplementinfo)
	{
		if (popup_weapon_supplement == null)
		{
			Debug.Log("error!");
			return;
		}
		popup_weapon_supplement.SetSupplementInfo(supplementinfo);
		popup_weapon_supplement.Show();
		m_SupplementInfo = supplementinfo;
	}

	public void CloseWeaponSupplement()
	{
		if (popup_weapon_supplement == null)
		{
			Debug.Log("error!");
			return;
		}
		popup_weapon_supplement.Hide();
		m_SupplementInfo = null;
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

	public void OnGoodsBuy(int goods_id, int goods_count)
	{
		TUIMappingInfo.CTUIMaterialInfo material = TUIMappingInfo.Instance().GetMaterial(goods_id);
		if (material != null)
		{
			if (material.m_PurchasePrice.unit_type == UnitType.Crystal)
			{
				int crystalValue = top_bar.GetCrystalValue();
				crystalValue -= material.m_PurchasePrice.price * goods_count;
				top_bar.SetCrystalValue(crystalValue);
				CUISound.GetInstance().Play("UI_Crystal");
			}
			else if (material.m_PurchasePrice.unit_type == UnitType.Gold)
			{
				int goldValue = top_bar.GetGoldValue();
				goldValue -= material.m_PurchasePrice.price * goods_count;
				top_bar.SetGoldValue(goldValue);
				CUISound.GetInstance().Play("UI_Trade");
			}
			TUIMappingInfo.Instance().SetMaterialCount(goods_id, TUIMappingInfo.Instance().GetMaterialCount(goods_id) + goods_count);
			popup_weapon_update.SetInfo(m_curWeaponAttributeInfo);
		}
	}

	public void OnSupplementSuccess()
	{
		if (m_SupplementInfo == null)
		{
			return;
		}
		top_bar.SetGoldValue(top_bar.GetGoldValue() + m_SupplementInfo.m_nGold);
		top_bar.SetCrystalValue(top_bar.GetCrystalValue() - m_SupplementInfo.m_nTotalCrystalCost);
		CUISound.GetInstance().Play("UI_Crystal");
		if (m_SupplementInfo.goods_list != null)
		{
			for (int i = 0; i < m_SupplementInfo.goods_list.Count; i++)
			{
				if (m_SupplementInfo.goods_list[i] != null)
				{
					int nMaterialID = m_SupplementInfo.goods_list[i].m_nMaterialID;
					int nMaterialCount = m_SupplementInfo.goods_list[i].m_nMaterialCount;
					TUIMappingInfo.Instance().SetMaterialCount(nMaterialID, TUIMappingInfo.Instance().GetMaterialCount(nMaterialID) + nMaterialCount);
				}
			}
		}
		popup_weapon_update.SetInfo(m_curWeaponAttributeInfo);
	}

	public void OnUpgradeWeapon(int weaponid, int weaponlevel)
	{
		if (m_curWeaponAttributeInfo == null)
		{
			return;
		}
		if (!m_curWeaponAttributeInfo.m_bActive)
		{
			TUIWeaponLevelInfo tUIWeaponLevelInfo = m_curWeaponAttributeInfo.Get(weaponlevel);
			if (tUIWeaponLevelInfo == null)
			{
				return;
			}
			int num = Mathf.CeilToInt((float)tUIWeaponLevelInfo.m_Price.price * m_curWeaponAttributeInfo.m_fDiscount);
			if (tUIWeaponLevelInfo.m_Price.unit_type == UnitType.Crystal)
			{
				top_bar.SetCrystalValue(top_bar.GetCrystalValue() - num);
				CUISound.GetInstance().Play("UI_Crystal");
			}
			else if (tUIWeaponLevelInfo.m_Price.unit_type == UnitType.Gold)
			{
				top_bar.SetGoldValue(top_bar.GetGoldValue() - num);
				CUISound.GetInstance().Play("UI_Trade");
			}
			for (int i = 0; i < tUIWeaponLevelInfo.m_ltGoodsNeed.Count; i++)
			{
				int nID = tUIWeaponLevelInfo.m_ltGoodsNeed[i].m_nID;
				int nNeedCount = tUIWeaponLevelInfo.m_ltGoodsNeed[i].m_nNeedCount;
				TUIMappingInfo.Instance().SetMaterialCount(nID, TUIMappingInfo.Instance().GetMaterialCount(nID) - nNeedCount);
			}
		}
		m_curWeaponAttributeInfo.m_nLevel = weaponlevel;
		if (unlock_blink != null)
		{
			string text = m_curWeaponAttributeInfo.m_sIcon;
			if (text.Length < 1)
			{
				text = TUIMappingInfo.Instance().GetWeaponTexture(weaponid);
			}
			string iconPath = m_curWeaponAttributeInfo.GetIconPath();
			if (iconPath.Length > 0)
			{
				CUISound.GetInstance().Play("UI_Unlocked_weapon");
				if (weaponlevel == 1)
				{
					unlock_blink.OpenBlinkWeapon("Purchase complete!", true, iconPath + "/" + text);
				}
				else
				{
					unlock_blink.OpenBlinkWeapon("Upgrade complete!", true, iconPath + "/" + text);
				}
			}
		}
		StarsBlink();
		OpenValueAnimation();
		SetInfo(m_curWeaponAttributeInfo);
	}

	public TUISupplementInfo GetSupplementInfo()
	{
		return m_SupplementInfo;
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
	}

	public void CloseBlink()
	{
		unlock_blink.CloseBlink();
	}

	public void StarsBlink()
	{
		if (m_curWeaponAttributeInfo == null)
		{
			return;
		}
		if (m_curWeaponAttributeInfo.m_nLevel > 0)
		{
			float x = label_title.CalculateBounds(label_title.Text).size.x;
			x *= label_title.transform.localScale.x;
			Vector3 position = new Vector3(label_title.transform.localPosition.x + x + 12f, label_title.transform.localPosition.y, label_title.transform.localPosition.z);
			level_stars.SetStars(m_curWeaponAttributeInfo.m_nLevel, position, true);
		}
		else
		{
			level_stars.SetStarsDisable();
		}
		if (scene_forge != null)
		{
			if (scene_forge.GetSFXOpen())
			{
				CUISound.GetInstance().Play("UI_Levelup");
			}
		}
		else
		{
			Debug.Log("error!");
		}
	}

	public void OpenValueAnimation()
	{
		if (m_curWeaponAttributeInfo == null)
		{
			return;
		}
		TUIWeaponLevelInfo tUIWeaponLevelInfo = m_curWeaponAttributeInfo.Get();
		if (tUIWeaponLevelInfo != null)
		{
			if (m_curWeaponAttributeInfo.IsAccessory() || m_curWeaponAttributeInfo.IsArmor())
			{
				label_info_weapon.SetDef(tUIWeaponLevelInfo.m_nDefence);
				label_info_weapon.OpenDefAnimation();
			}
			else
			{
				label_info_weapon.SetDamage(tUIWeaponLevelInfo.m_nDamage);
				label_info_weapon.OpenDamageAnimation();
			}
		}
	}

	public void SetRoleRotation(float wparam, float lparam)
	{
		role_control.SetRotation(wparam, lparam);
	}

	public void SetWeaponKindItem(kShopWeaponCategory nCategory)
	{
		if (weapon_info == null || m_nCurWeaponCategory == nCategory)
		{
			return;
		}
		weapon_kind_item.SetSelectWeaponBtn(nCategory);
		if (m_nCurWeaponCategory != 0)
		{
			ScrollList_Weapon scrollList = GetScrollList(m_nCurWeaponCategory);
			if (scrollList != null)
			{
				scrollList.Hide();
			}
		}
		m_nCurWeaponCategory = nCategory;
		ScrollList_Weapon scrollList_Weapon = GetScrollList(m_nCurWeaponCategory);
		if (scrollList_Weapon == null)
		{
			scrollList_Weapon = CreateScrollListWeapon(prefab_scroll_list_weapon.gameObject, weapon_info.Get(m_nCurWeaponCategory));
			if (scrollList_Weapon == null)
			{
				return;
			}
			AddScrollList(nCategory, scrollList_Weapon);
		}
		scrollList_Weapon.Show();
	}

	private ScrollList_Weapon CreateScrollListWeapon(GameObject prefab, List<TUIWeaponAttributeInfo> m_weapon_list)
	{
		if (prefab == null)
		{
			return null;
		}
		if (m_weapon_list == null)
		{
			return null;
		}
		GameObject gameObject = Object.Instantiate(prefab) as GameObject;
		if (gameObject == null)
		{
			return null;
		}
		ScrollList_Weapon component = gameObject.GetComponent<ScrollList_Weapon>();
		component.transform.parent = base.transform.parent;
		component.transform.localPosition = normal_scroll_pos;
		component.SetItem(m_weapon_list);
		component.Hide();
		return component;
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
		if (m_curWeaponAttributeInfo != null)
		{
			if (m_sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (m_curWeaponAttributeInfo.m_nLevel == 1)
			{
				ShowGoEquip();
				AndroidReturnPlugin.instance.SetCurFunc(scene_forge.TUIEvent_CloseGoEquip);
			}
		}
	}

	public void ShowTips(TUIControl m_control)
	{
		if (popup_tips == null || m_control == null)
		{
			Debug.Log("error!");
			return;
		}
		GoodsNeedItemImg component = m_control.GetComponent<GoodsNeedItemImg>();
		if (component != null)
		{
			string goodsName = component.GetGoodsName();
			popup_tips.SetInfo(goodsName, m_control.transform.position, PopupTips.TipsPivot.TopRight);
		}
	}

	public void HideTips()
	{
		if (popup_tips == null)
		{
			Debug.Log("error!");
		}
		else
		{
			popup_tips.Hide();
		}
	}
}
