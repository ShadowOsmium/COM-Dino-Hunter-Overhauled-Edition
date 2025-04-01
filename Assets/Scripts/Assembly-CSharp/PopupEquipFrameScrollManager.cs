using System.Collections.Generic;
using UnityEngine;

public class PopupEquipFrameScrollManager : MonoBehaviour
{
	public TUIScrollList scroll_role;

	public TUIScrollList scroll_skill;

	public TUIScrollList scroll_weapon01;

	public TUIScrollList scroll_weapon02;

	public TUIScrollList scroll_armor_head;

	public TUIScrollList scroll_armor_body;

	public TUIScrollList scroll_armor_bracelet;

	public TUIScrollList scroll_armor_leg;

	public TUIScrollList scroll_accessory_halo;

	public TUIScrollList scroll_accessory_necklace;

	public TUIScrollList scroll_accessory_badge;

	public TUIScrollList scroll_accessory_stoneskin;

	protected Dictionary<PopupType, TUIScrollList> m_dictScrollList;

	protected TUIScrollList scroll_now;

	protected Dictionary<PopupType, List<TUIPopupInfo>> m_dictData;

	private void Awake()
	{
		m_dictScrollList = new Dictionary<PopupType, TUIScrollList>();
		if (scroll_role != null)
		{
			m_dictScrollList.Add(PopupType.Roles, scroll_role);
		}
		if (scroll_skill != null)
		{
			m_dictScrollList.Add(PopupType.Skills, scroll_skill);
		}
		if (scroll_weapon01 != null)
		{
			m_dictScrollList.Add(PopupType.Weapons01, scroll_weapon01);
		}
		if (scroll_weapon02 != null)
		{
			m_dictScrollList.Add(PopupType.Weapons02, scroll_weapon02);
		}
		if (scroll_armor_head != null)
		{
			m_dictScrollList.Add(PopupType.Armor_Head, scroll_armor_head);
		}
		if (scroll_armor_body != null)
		{
			m_dictScrollList.Add(PopupType.Armor_Body, scroll_armor_body);
		}
		if (scroll_armor_bracelet != null)
		{
			m_dictScrollList.Add(PopupType.Armor_Bracelet, scroll_armor_bracelet);
		}
		if (scroll_armor_leg != null)
		{
			m_dictScrollList.Add(PopupType.Armor_Leg, scroll_armor_leg);
		}
		if (scroll_accessory_halo != null)
		{
			m_dictScrollList.Add(PopupType.Accessory_Halo, scroll_accessory_halo);
		}
		if (scroll_accessory_necklace != null)
		{
			m_dictScrollList.Add(PopupType.Accessory_Necklace, scroll_accessory_necklace);
		}
		if (scroll_accessory_badge != null)
		{
			m_dictScrollList.Add(PopupType.Accessory_Badge, scroll_accessory_badge);
		}
		if (scroll_accessory_stoneskin != null)
		{
			m_dictScrollList.Add(PopupType.Accessory_Stoneskin, scroll_accessory_stoneskin);
		}
		scroll_now = null;
		m_dictData = new Dictionary<PopupType, List<TUIPopupInfo>>();
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public TUIScrollList Get(PopupType nType)
	{
		if (!m_dictScrollList.ContainsKey(nType))
		{
			return null;
		}
		return m_dictScrollList[nType];
	}

	public List<TUIPopupInfo> GetData(PopupType nType)
	{
		if (!m_dictData.ContainsKey(nType))
		{
			return null;
		}
		return m_dictData[nType];
	}

	public void Clear(PopupType nType)
	{
		TUIScrollList tUIScrollList = Get(nType);
		if (!(tUIScrollList == null))
		{
		}
	}

	public void CreateList(PopupType nType, List<TUIPopupInfo> ltPopupInfo, BtnSelect_Item prefab, TUIRect m_rect_show, GameObject m_invoke_object)
	{
		if (ltPopupInfo == null || prefab == null)
		{
			Debug.LogError(string.Concat("data null  popupinfo = ", ltPopupInfo, " prefab = ", prefab));
			return;
		}
		TUIScrollList tUIScrollList = Get(nType);
		if (tUIScrollList == null)
		{
			Debug.LogError(string.Concat("scroll ", nType, " does not exist!"));
			return;
		}
		List<TUIPopupInfo> list = GetData(nType);
		if (list == null)
		{
			list = new List<TUIPopupInfo>();
			m_dictData.Add(nType, list);
		}
		list.Clear();
		tUIScrollList.Clear(true);
		for (int i = 0; i < ltPopupInfo.Count; i++)
		{
			TUIPopupInfo tUIPopupInfo = ltPopupInfo[i];
			if (tUIPopupInfo == null)
			{
				continue;
			}
			BtnSelect_Item btnSelect_Item = Object.Instantiate(prefab) as BtnSelect_Item;
			if (btnSelect_Item == null)
			{
				continue;
			}
			btnSelect_Item.DoCreate(tUIPopupInfo, m_rect_show, i);
			TUIButtonSelect component = btnSelect_Item.gameObject.GetComponent<TUIButtonSelect>();
			if (component != null)
			{
				component.invokeOnEvent = true;
				component.invokeObject = m_invoke_object;
				component.componentName = "Scene_Equip";
				if (tUIPopupInfo.IsRole())
				{
					component.invokeFunctionName = "TUIEvent_PopupRoleSelect";
				}
				else if (tUIPopupInfo.IsSkill())
				{
					component.invokeFunctionName = "TUIEvent_PopupSkillSelect";
				}
				else if (tUIPopupInfo.IsWeapon() || tUIPopupInfo.IsArmor() || tUIPopupInfo.IsAccessory())
				{
					component.invokeFunctionName = "TUIEvent_PopupWeaponSelect";
				}
				tUIScrollList.Add(component);
				list.Add(tUIPopupInfo);
			}
		}
	}

	public bool IsEmpty(PopupType nType)
	{
		TUIScrollList tUIScrollList = Get(nType);
		if (tUIScrollList == null || tUIScrollList.transform.childCount == 0)
		{
			return true;
		}
		return false;
	}

	public void Show(PopupType nType)
	{
		if (scroll_now != null)
		{
			scroll_now.transform.localPosition = new Vector3(0f, -1000f, scroll_now.transform.localPosition.z);
			scroll_now = null;
		}
		scroll_now = Get(nType);
		if (!(scroll_now == null))
		{
			scroll_now.ScrollListTo(0f);
			scroll_now.transform.localPosition = new Vector3(0f, 0f, scroll_now.transform.localPosition.z);
		}
	}
}
