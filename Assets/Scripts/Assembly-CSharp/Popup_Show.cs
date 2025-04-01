using System.Collections.Generic;
using UnityEngine;

public class Popup_Show : MonoBehaviour
{
	public BtnSelect_Item prefab_items;

	public PopupEquipFrameScrollManager select_group;

	public TUIRect rect_show;

	public TUILabel label_title;

	public TUILabel label_introduce;

	public PopupEquipFrameIntroduce equip_introduce;

	public Popup_BtnEquip btn_equip;

	public LevelStarsEx level_stars;

	public Scene_Equip m_SceneEquip { get; private set; }

	public PopupType m_nPopupType { get; private set; }

	public int m_nExchangeIndex1 { get; private set; }

	public int m_nExchangeIndex2 { get; private set; }

	public BtnSelect_Item m_SelectItem { get; private set; }

	public BtnItem_Item m_ButtonItem { get; private set; }

	private void Awake()
	{
		m_nPopupType = PopupType.None;
		m_nExchangeIndex1 = -1;
		m_nExchangeIndex2 = -1;
	}

	private void Update()
	{
	}

	public void Init(Scene_Equip scene_equip)
	{
		m_SceneEquip = scene_equip;
	}

	public void Show(PopupType nType, BtnItem_Item buttonitem)
	{
		ClearInfo();
		m_nPopupType = nType;
		m_ButtonItem = buttonitem;
		m_SelectItem = null;
		base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		if (select_group != null)
		{
			select_group.Show(nType);
		}
		base.GetComponent<Animation>().Play();
	}

	public void ShowDirectly(PopupType nType)
	{
		ClearInfo();
		m_nPopupType = nType;
		base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		base.GetComponent<Animation>().Play();
	}

	public void Hide()
	{
		base.transform.localPosition = new Vector3(0f, -1000f, base.transform.localPosition.z);
		ClearInfo();
	}

	public void SetRoleInfo(TUIEquipInfo m_info, GameObject invoke_object)
	{
		if (m_info == null || select_group == null)
		{
			Debug.Log(string.Concat("data is null m_info = ", m_info, " select_group = ", select_group));
		}
		else
		{
			select_group.CreateList(PopupType.Roles, m_info.roles_list, prefab_items, rect_show, invoke_object);
		}
	}

	public void SetSkillInfo(TUIEquipInfo m_info, GameObject invoke_object)
	{
		if (m_info == null || select_group == null)
		{
			Debug.Log(string.Concat("data is null m_info = ", m_info, " select_group = ", select_group));
		}
		else
		{
			select_group.CreateList(PopupType.Skills, m_info.skill_list, prefab_items, rect_show, invoke_object);
		}
	}

	public void SetEquipInfo(TUIEquipInfo m_info, GameObject invoke_object)
	{
		if (m_info == null || select_group == null)
		{
			Debug.Log(string.Concat("data is null m_info = ", m_info, " select_group = ", select_group));
			return;
		}
		select_group.CreateList(PopupType.Weapons01, m_info.ltWeaponMelee, prefab_items, rect_show, invoke_object);
		select_group.CreateList(PopupType.Weapons02, m_info.ltWeaponRange, prefab_items, rect_show, invoke_object);
		select_group.CreateList(PopupType.Armor_Head, m_info.ltArmorHead, prefab_items, rect_show, invoke_object);
		select_group.CreateList(PopupType.Armor_Body, m_info.ltArmorUpper, prefab_items, rect_show, invoke_object);
		select_group.CreateList(PopupType.Armor_Bracelet, m_info.ltArmorBracelet, prefab_items, rect_show, invoke_object);
		select_group.CreateList(PopupType.Armor_Leg, m_info.ltArmorLower, prefab_items, rect_show, invoke_object);
		select_group.CreateList(PopupType.Accessory_Halo, m_info.ltAccessoryHeadup, prefab_items, rect_show, invoke_object);
		select_group.CreateList(PopupType.Accessory_Necklace, m_info.ltAccessoryNeck, prefab_items, rect_show, invoke_object);
		select_group.CreateList(PopupType.Accessory_Badge, m_info.ltAccessoryBadge, prefab_items, rect_show, invoke_object);
		select_group.CreateList(PopupType.Accessory_Stoneskin, m_info.ltAccessoryStone, prefab_items, rect_show, invoke_object);
	}

	public void SetSimpleInfo(TUIPopupInfo m_popup_info)
	{
		if (m_popup_info != null)
		{
			label_title.Text = m_popup_info.name;
			if (label_introduce != null)
			{
				label_introduce.Text = m_popup_info.introduce;
			}
			if (equip_introduce != null)
			{
				equip_introduce.SetOnlyTextInfo(m_popup_info);
			}
		}
	}

	public BtnSelect_Item SetItemSelectInfo(PopupType nType, int nID)
	{
		TUIScrollList tUIScrollList = select_group.Get(nType);
		if (tUIScrollList == null)
		{
			return null;
		}
		List<TUIControl> tUIControlONListObjs = tUIScrollList.GetTUIControlONListObjs(false);
		if (tUIControlONListObjs == null)
		{
			return null;
		}
		for (int i = 0; i < tUIControlONListObjs.Count; i++)
		{
			if (tUIControlONListObjs[i] == null)
			{
				continue;
			}
			BtnSelect_Item component = tUIControlONListObjs[i].gameObject.GetComponent<BtnSelect_Item>();
			if (component != null && component.GetInfo() != null && component.GetInfo().texture_id == nID)
			{
				TUIButtonSelect component2 = tUIControlONListObjs[i].gameObject.GetComponent<TUIButtonSelect>();
				if (component2 != null)
				{
					component2.SetSelected(true);
				}
				SetItemSelectInfo(component);
				return component;
			}
		}
		return null;
	}

	public void SetItemSelectInfo(BtnSelect_Item selectitem)
	{
		m_SelectItem = selectitem;
		if (m_SelectItem == null)
		{
			ClearInfo();
			return;
		}
		TUIPopupInfo info = m_SelectItem.GetInfo();
		if (info == null)
		{
			return;
		}
		if (info.IsWeapon())
		{
			label_title.Text = info.name;
			if (label_introduce != null)
			{
				label_introduce.Text = info.introduce;
			}
			if (equip_introduce != null)
			{
				equip_introduce.SetWeaponInfo(info);
			}
		}
		else if (info.IsArmor() || info.IsAccessory())
		{
			label_title.Text = info.name;
			if (label_introduce != null)
			{
				label_introduce.Text = info.introduce;
			}
			if (equip_introduce != null)
			{
				equip_introduce.SetArmorAccessoryInfo(info);
			}
		}
		else
		{
			label_title.Text = info.name;
			if (label_introduce != null)
			{
				label_introduce.Text = info.introduce;
			}
			if (equip_introduce != null)
			{
				equip_introduce.SetOnlyTextInfo(info);
			}
		}
		SetBtnEquip(true);
		if ((info.IsSkill() || info.IsArmor() || info.IsAccessory()) && m_ButtonItem != null && m_ButtonItem.GetInfo() != null && m_ButtonItem.GetInfo().texture_id == info.texture_id)
		{
			SetBtnEquip(false);
		}
		if (level_stars != null)
		{
			if (info.level > 0)
			{
				float x = label_title.CalculateBounds(label_title.Text).size.x;
				x *= label_title.transform.localScale.x;
				Vector3 position = new Vector3(label_title.transform.localPosition.x + x + 12f, label_title.transform.localPosition.y, label_title.transform.localPosition.z);
				level_stars.SetStars(info.level, position);
			}
			else
			{
				level_stars.SetStarsDisable();
			}
		}
	}

	public int InitScrollList(int nChooseID = -1)
	{
		if (select_group == null)
		{
			return -1;
		}
		TUIScrollList tUIScrollList = select_group.Get(m_nPopupType);
		if (tUIScrollList == null)
		{
			return -1;
		}
		List<TUIControl> tUIControlONListObjs = tUIScrollList.GetTUIControlONListObjs(false);
		if (tUIControlONListObjs == null)
		{
			return -1;
		}
		int result = -1;
		for (int i = 0; i < tUIControlONListObjs.Count; i++)
		{
			TUIControl tUIControl = tUIControlONListObjs[i];
			if (tUIControl == null)
			{
				continue;
			}
			BtnSelect_Item component = tUIControl.gameObject.GetComponent<BtnSelect_Item>();
			if (component == null)
			{
				continue;
			}
			TUIButtonSelect component2 = tUIControl.gameObject.GetComponent<TUIButtonSelect>();
			if (component2 == null)
			{
				continue;
			}
			TUIPopupInfo info = component.GetInfo();
			if (info != null)
			{
				bool flag = false;
				if (nChooseID == -1 && i == 0)
				{
					flag = true;
				}
				else if (info != null && info.texture_id == nChooseID)
				{
					flag = true;
				}
				if (flag)
				{
					tUIScrollList.Remove(tUIControl, false);
					tUIScrollList.Insert(0, tUIControl);
					component2.SetSelected(true);
					result = info.texture_id;
					SetItemSelectInfo(component);
				}
				else
				{
					component2.SetSelected(false);
				}
			}
		}
		return result;
	}

	public bool IsExchange()
	{
		if (m_SelectItem == null || m_ButtonItem == null || m_SceneEquip == null)
		{
			return false;
		}
		BtnItem_Item[] array = null;
		switch (m_nPopupType)
		{
		case PopupType.Skills:
			array = m_SceneEquip.m_arrButtonSkill;
			break;
		case PopupType.Weapons02:
			if (m_SceneEquip.popup_equip_frame != null && m_SceneEquip.popup_equip_frame.items_weapon != null)
			{
				array = m_SceneEquip.popup_equip_frame.items_weapon.m_arrItem;
			}
			break;
		}
		if (array == null)
		{
			return false;
		}
		for (int i = 0; i < array.Length; i++)
		{
			if (!(m_ButtonItem == array[i]))
			{
				TUIPopupInfo info = array[i].GetInfo();
				TUIPopupInfo info2 = m_SelectItem.GetInfo();
				if (info != null && info2 != null && info.texture_id == info2.texture_id)
				{
					m_nExchangeIndex1 = i;
					m_nExchangeIndex2 = m_ButtonItem.index;
					return true;
				}
			}
		}
		return false;
	}

	public void Equip()
	{
		if (!(m_ButtonItem == null) && !(m_SelectItem == null))
		{
			switch (m_nPopupType)
			{
			case PopupType.Roles:
				m_ButtonItem.SetInfo(m_SelectItem.GetInfo());
				break;
			case PopupType.Weapons01:
			case PopupType.Weapons02:
			case PopupType.Armor_Head:
			case PopupType.Armor_Body:
			case PopupType.Armor_Bracelet:
			case PopupType.Armor_Leg:
			case PopupType.Accessory_Halo:
			case PopupType.Accessory_Necklace:
			case PopupType.Accessory_Badge:
			case PopupType.Accessory_Stoneskin:
				m_ButtonItem.SetInfo(m_SelectItem.GetInfo(), true, true);
				break;
			default:
				m_ButtonItem.SetInfo(m_SelectItem.GetInfo(), true);
				break;
			}
		}
	}

	public void UnEquip()
	{
		if (!(m_ButtonItem == null))
		{
			m_ButtonItem.SetInfo(null);
		}
	}

	public void SetBtnEquip(bool isEquip)
	{
		if (btn_equip == null)
		{
			Debug.Log("error!");
			return;
		}
		Popup_BtnEquip component = btn_equip.GetComponent<Popup_BtnEquip>();
		if (!(component == null))
		{
			if (isEquip)
			{
				component.SetEquip();
			}
			else
			{
				component.SetUnEquip();
			}
		}
	}

	public void ClearInfo()
	{
		if (equip_introduce != null)
		{
			equip_introduce.HideInfo();
		}
	}

	public NewMarkType RefreshMark(PopupType popuptype)
	{
		TUIScrollList tUIScrollList = select_group.Get(popuptype);
		if (tUIScrollList == null)
		{
			return NewMarkType.None;
		}
		List<TUIScrollListObject> childList = tUIScrollList.GetChildList();
		if (childList == null)
		{
			return NewMarkType.None;
		}
		NewMarkType newMarkType = NewMarkType.None;
		foreach (TUIScrollListObject item in childList)
		{
			BtnSelect_Item component = item.gameObject.GetComponent<BtnSelect_Item>();
			if (!(component == null) && component.GetInfo() != null)
			{
				if (component.GetInfo().m_MarkType == NewMarkType.New)
				{
					newMarkType = NewMarkType.New;
				}
				else if (component.GetInfo().m_MarkType == NewMarkType.Mark && newMarkType == NewMarkType.None)
				{
					newMarkType = NewMarkType.Mark;
				}
				component.SetMark(component.GetInfo().m_MarkType);
			}
		}
		return newMarkType;
	}

	public bool IsEmpty()
	{
		return IsEmpty(m_nPopupType);
	}

	public bool IsEmpty(PopupType nType)
	{
		TUIScrollList tUIScrollList = select_group.Get(nType);
		if (tUIScrollList == null || tUIScrollList.GetChildList() == null || tUIScrollList.GetChildList().Count == 0)
		{
			return true;
		}
		return false;
	}

	public List<TUIPopupInfo> GetData(PopupType nType)
	{
		if (select_group == null)
		{
			return null;
		}
		return select_group.GetData(nType);
	}
}
