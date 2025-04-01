using System.Collections.Generic;
using EventCenter;
using UnityEngine;

public class Scene_Equip : MonoBehaviour
{
	public TUIFade m_fade;

	private float m_fade_in_time;

	private float m_fade_out_time;

	private bool do_fade_in;

	private bool is_fade_out;

	private bool do_fade_out;

	private string next_scene = string.Empty;

	public BtnItem_Item m_ButtonSkill;

	public BtnItem_Item[] m_arrButtonSkill;

	public BtnItem_Item m_ButtonRole;

	public PopupEquipFrame popup_equip_frame;

	public Popup_Show m_PopupShow_ActiveSkill;

	public Popup_Show m_PopupShow_PassiveSkill;

	public Popup_Show m_PopupShow_Equip;

	public Popup_Show m_PopupShow_Role;

	public Role_Control go_role;

	public TUILabel label_role_name;

	public LabelBattlePower m_BattlePower;

	public Top_Bar top_bar;

	public PopupGoBuy popup_go_buy;

	public PopupNewHelp popup_new_help;

	private bool sfx_open_now = true;

	private bool music_open_now = true;

	private void Awake()
	{
		m_PopupShow_ActiveSkill.Init(this);
		m_PopupShow_PassiveSkill.Init(this);
		m_PopupShow_Role.Init(this);
		m_PopupShow_Equip.Init(this);
		TUIDataServer.Instance().Initialize();
		global::EventCenter.EventCenter.Instance.Register<TUIEvent.BackEvent_SceneEquip>(TUIEvent_SetUIInfo);
	}

	private void Start()
	{
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_TopBar));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_RoleSign));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_SkillSign));
		global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_WeaponSign));
		DoNewHelp();
	}

	private void Update()
	{
		m_fade_in_time += Time.deltaTime;
		if (m_fade_in_time >= m_fade.fadeInTime && !do_fade_in)
		{
			do_fade_in = true;
		}
		if (!is_fade_out)
		{
			return;
		}
		m_fade_out_time += Time.deltaTime;
		if (m_fade_out_time >= m_fade.fadeOutTime && !do_fade_out)
		{
			do_fade_out = true;
			m_fade.SetFadeOutEnd();
			TUIMappingInfo.SwitchSceneStr switchSceneStr = TUIMappingInfo.Instance().GetSwitchSceneStr();
			if (switchSceneStr != null)
			{
				switchSceneStr(next_scene);
			}
		}
	}

	private void OnDestroy()
	{
		global::EventCenter.EventCenter.Instance.Unregister<TUIEvent.BackEvent_SceneEquip>(TUIEvent_SetUIInfo);
	}

	public void TUIEvent_SetUIInfo(object sender, TUIEvent.BackEvent_SceneEquip m_event)
	{
		if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_TopBar)
		{
			if (m_event.GetEventInfo() != null && m_event.GetEventInfo().player_info != null)
			{
				int role_id = m_event.GetEventInfo().player_info.role_id;
				int level = m_event.GetEventInfo().player_info.level;
				int exp = m_event.GetEventInfo().player_info.exp;
				int level_exp = m_event.GetEventInfo().player_info.level_exp;
				int gold = m_event.GetEventInfo().player_info.gold;
				int crystal = m_event.GetEventInfo().player_info.crystal;
				top_bar.SetAllValue(level, exp, level_exp, gold, crystal, role_id);
				if (label_role_name != null)
				{
					label_role_name.Text = m_event.GetEventInfo().player_info.avatar_name;
				}
			}
			else
			{
				Debug.Log("error!");
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_SetBattlePower)
		{
			if (!(m_BattlePower == null))
			{
				int wparam = m_event.GetWparam();
				bool flag = m_event.GetLparam() > 0;
				m_BattlePower.Set(wparam, flag, (!flag) ? 0f : 0.5f);
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_RoleSign)
		{
			if (m_event.GetEventInfo() != null && m_event.GetEventInfo().equip_info != null)
			{
				if (m_event.GetEventInfo().equip_info.role != null)
				{
					TUICharacterAttribute characterAttribute = m_event.GetEventInfo().equip_info.role.m_CharacterAttribute;
					if (characterAttribute != null)
					{
						go_role.ChangeRole(characterAttribute.m_nModelID);
						if (TUIMappingInfo.Instance().m_GetAvatarModel != null)
						{
							GameObject modelprefab = null;
							Texture modeltexture = null;
							if (TUIMappingInfo.Instance().m_GetAvatarModel(characterAttribute.m_nAvatarHead, top_bar.GetRoleID(), ref modelprefab, ref modeltexture))
							{
								go_role.ChangeAvatar(0, modelprefab, modeltexture);
							}
							if (TUIMappingInfo.Instance().m_GetAvatarModel(characterAttribute.m_nAvatarUpper, top_bar.GetRoleID(), ref modelprefab, ref modeltexture))
							{
								go_role.ChangeAvatar(1, modelprefab, modeltexture);
							}
							if (TUIMappingInfo.Instance().m_GetAvatarModel(characterAttribute.m_nAvatarLower, top_bar.GetRoleID(), ref modelprefab, ref modeltexture))
							{
								go_role.ChangeAvatar(2, modelprefab, modeltexture);
							}
							if (TUIMappingInfo.Instance().m_GetAvatarModel(characterAttribute.m_nAvatarHeadup, top_bar.GetRoleID(), ref modelprefab, ref modeltexture))
							{
								go_role.ChangeAvatarEffect(3, modelprefab);
							}
							if (TUIMappingInfo.Instance().m_GetAvatarModel(characterAttribute.m_nAvatarBracelet, top_bar.GetRoleID(), ref modelprefab, ref modeltexture))
							{
								go_role.ChangeAvatarEffect(4, modelprefab);
								go_role.ChangeAvatarEffect(5, modelprefab);
							}
							if (TUIMappingInfo.Instance().m_GetAvatarModel(characterAttribute.m_nAvatarNeck, top_bar.GetRoleID(), ref modelprefab, ref modeltexture))
							{
								go_role.ChangeAvatarEffect(6, modelprefab);
							}
						}
					}
					go_role.SetRoleFixedRotation(new Vector3(0f, -40f, 0f));
				}
				m_ButtonRole.SetInfo(m_event.GetEventInfo().equip_info.role);
				m_PopupShow_Role.SetRoleInfo(m_event.GetEventInfo().equip_info, base.gameObject);
				m_ButtonRole.SetMark(m_PopupShow_Role.RefreshMark(PopupType.Roles));
			}
			else
			{
				Debug.Log("error!");
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_SkillSign)
		{
			if (m_event.GetEventInfo() != null && m_event.GetEventInfo().equip_info != null)
			{
				m_ButtonSkill.SetInfo(m_event.GetEventInfo().equip_info.skill);
				for (int i = 0; i < m_event.GetEventInfo().equip_info.arrSkill.Length; i++)
				{
					m_arrButtonSkill[i].SetInfo(m_event.GetEventInfo().equip_info.arrSkill[i]);
				}
				m_PopupShow_PassiveSkill.SetSkillInfo(m_event.GetEventInfo().equip_info, base.gameObject);
				for (int j = 0; j < m_event.GetEventInfo().equip_info.arrSkill.Length; j++)
				{
					m_arrButtonSkill[j].SetMark(m_PopupShow_PassiveSkill.RefreshMark(PopupType.Skills));
				}
			}
			else
			{
				Debug.Log("error!");
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_WeaponSign)
		{
			if (m_event.GetEventInfo() != null)
			{
				TUIEquipInfo equip_info = m_event.GetEventInfo().equip_info;
				if (popup_equip_frame != null)
				{
					popup_equip_frame.SetInfo(equip_info);
					switch (equip_info.m_GotoEquip_PopupType)
					{
					case PopupType.Armor_Head:
					case PopupType.Armor_Body:
					case PopupType.Armor_Bracelet:
					case PopupType.Armor_Leg:
						popup_equip_frame.SetBtnChoose(2);
						break;
					case PopupType.Accessory_Halo:
					case PopupType.Accessory_Necklace:
					case PopupType.Accessory_Badge:
					case PopupType.Accessory_Stoneskin:
						popup_equip_frame.SetBtnChoose(3);
						break;
					default:
						popup_equip_frame.SetBtnChoose(1);
						break;
					}
					if (equip_info != null && equip_info.arrWeapon != null && equip_info.arrWeapon.Length > 0)
					{
						TUIPopupInfo tUIPopupInfo = equip_info.arrWeapon[0];
						if (tUIPopupInfo != null && go_role != null)
						{
							go_role.ChangeWeapon(tUIPopupInfo.texture_id);
						}
					}
				}
				m_PopupShow_Equip.SetEquipInfo(equip_info, base.gameObject);
				popup_equip_frame.items_weapon.m_arrItem[0].SetMark(m_PopupShow_Equip.RefreshMark(PopupType.Weapons01));
				NewMarkType mark = m_PopupShow_Equip.RefreshMark(PopupType.Weapons02);
				popup_equip_frame.items_weapon.m_arrItem[1].SetMark(mark);
				popup_equip_frame.items_weapon.m_arrItem[2].SetMark(mark);
				popup_equip_frame.items_armor.m_arrItem[0].SetMark(m_PopupShow_Equip.RefreshMark(PopupType.Armor_Head));
				popup_equip_frame.items_armor.m_arrItem[1].SetMark(m_PopupShow_Equip.RefreshMark(PopupType.Armor_Body));
				popup_equip_frame.items_armor.m_arrItem[2].SetMark(m_PopupShow_Equip.RefreshMark(PopupType.Armor_Bracelet));
				popup_equip_frame.items_armor.m_arrItem[3].SetMark(m_PopupShow_Equip.RefreshMark(PopupType.Armor_Leg));
				popup_equip_frame.items_accessory.m_arrItem[0].SetMark(m_PopupShow_Equip.RefreshMark(PopupType.Accessory_Halo));
				popup_equip_frame.items_accessory.m_arrItem[1].SetMark(m_PopupShow_Equip.RefreshMark(PopupType.Accessory_Necklace));
				popup_equip_frame.items_accessory.m_arrItem[2].SetMark(m_PopupShow_Equip.RefreshMark(PopupType.Accessory_Badge));
				popup_equip_frame.items_accessory.m_arrItem[3].SetMark(m_PopupShow_Equip.RefreshMark(PopupType.Accessory_Stoneskin));
			}
			else
			{
				Debug.Log("error!");
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_RoleEquip)
		{
			if (!m_event.GetControlSuccess() || m_PopupShow_Role.m_SelectItem == null || m_PopupShow_Role.m_SelectItem.GetInfo() == null)
			{
				return;
			}
			TUIPopupInfo info = m_PopupShow_Role.m_SelectItem.GetInfo();
			int texture_id = info.texture_id;
			string text = info.name;
			go_role.SetRoleFixedRotation(new Vector3(0f, -40f, 0f));
			TUICharacterAttribute characterAttribute2 = info.m_CharacterAttribute;
			if (characterAttribute2 != null)
			{
				go_role.ChangeRole(characterAttribute2.m_nModelID);
				if (TUIMappingInfo.Instance().m_GetAvatarModel != null)
				{
					GameObject modelprefab2 = null;
					Texture modeltexture2 = null;
					if (TUIMappingInfo.Instance().m_GetAvatarModel(characterAttribute2.m_nAvatarHead, texture_id, ref modelprefab2, ref modeltexture2))
					{
						go_role.ChangeAvatar(0, modelprefab2, modeltexture2);
					}
					if (TUIMappingInfo.Instance().m_GetAvatarModel(characterAttribute2.m_nAvatarUpper, texture_id, ref modelprefab2, ref modeltexture2))
					{
						go_role.ChangeAvatar(1, modelprefab2, modeltexture2);
					}
					if (TUIMappingInfo.Instance().m_GetAvatarModel(characterAttribute2.m_nAvatarLower, texture_id, ref modelprefab2, ref modeltexture2))
					{
						go_role.ChangeAvatar(2, modelprefab2, modeltexture2);
					}
					if (TUIMappingInfo.Instance().m_GetAvatarModel(characterAttribute2.m_nAvatarHeadup, texture_id, ref modelprefab2, ref modeltexture2))
					{
						go_role.ChangeAvatarEffect(3, modelprefab2);
					}
					if (TUIMappingInfo.Instance().m_GetAvatarModel(characterAttribute2.m_nAvatarBracelet, texture_id, ref modelprefab2, ref modeltexture2))
					{
						go_role.ChangeAvatarEffect(4, modelprefab2);
						go_role.ChangeAvatarEffect(5, modelprefab2);
					}
					if (TUIMappingInfo.Instance().m_GetAvatarModel(characterAttribute2.m_nAvatarNeck, texture_id, ref modelprefab2, ref modeltexture2))
					{
						go_role.ChangeAvatarEffect(6, modelprefab2);
					}
				}
			}
			label_role_name.Text = text;
			m_PopupShow_Role.Equip();
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_SkillEquip)
		{
			if (m_event.GetControlSuccess())
			{
				m_PopupShow_PassiveSkill.Equip();
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_SkillUnEquip)
		{
			if (m_event.GetControlSuccess())
			{
				m_PopupShow_PassiveSkill.UnEquip();
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_SkillExchange)
		{
			if (m_event.GetControlSuccess())
			{
				int nExchangeIndex = m_PopupShow_PassiveSkill.m_nExchangeIndex1;
				int nExchangeIndex2 = m_PopupShow_PassiveSkill.m_nExchangeIndex2;
				if (nExchangeIndex >= 0 && nExchangeIndex < m_arrButtonSkill.Length && nExchangeIndex2 >= 0 && nExchangeIndex2 < m_arrButtonSkill.Length)
				{
					TUIPopupInfo info2 = m_arrButtonSkill[nExchangeIndex].GetInfo();
					m_arrButtonSkill[nExchangeIndex].SetInfo(m_arrButtonSkill[nExchangeIndex2].GetInfo(), true);
					m_arrButtonSkill[nExchangeIndex2].SetInfo(info2, true);
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_WeaponEquip)
		{
			if (!m_event.GetControlSuccess() || go_role == null)
			{
				return;
			}
			TUIPopupInfo info3 = m_PopupShow_Equip.m_SelectItem.GetInfo();
			if (info3 != null)
			{
				Debug.Log(info3.texture_id);
				if (info3.IsWeapon())
				{
					go_role.ChangeWeapon(info3.texture_id);
				}
				else if (info3.IsArmor() || info3.IsAccessory())
				{
					List<TUIPopupInfo> data = m_PopupShow_Role.GetData(PopupType.Roles);
					if (data != null)
					{
						foreach (TUIPopupInfo item in data)
						{
							if (item.m_CharacterAttribute != null)
							{
								switch (info3.m_PopupType)
								{
								case PopupType.Armor_Head:
									item.m_CharacterAttribute.m_nAvatarHead = info3.texture_id;
									break;
								case PopupType.Armor_Body:
									item.m_CharacterAttribute.m_nAvatarUpper = info3.texture_id;
									break;
								case PopupType.Armor_Leg:
									item.m_CharacterAttribute.m_nAvatarLower = info3.texture_id;
									break;
								case PopupType.Accessory_Halo:
									item.m_CharacterAttribute.m_nAvatarHeadup = info3.texture_id;
									break;
								case PopupType.Armor_Bracelet:
									item.m_CharacterAttribute.m_nAvatarBracelet = info3.texture_id;
									break;
								case PopupType.Accessory_Necklace:
									item.m_CharacterAttribute.m_nAvatarNeck = info3.texture_id;
									break;
								}
							}
						}
					}
					if (TUIMappingInfo.Instance().m_GetAvatarModel != null)
					{
						GameObject modelprefab3 = null;
						Texture modeltexture3 = null;
						if (TUIMappingInfo.Instance().m_GetAvatarModel(info3.texture_id, top_bar.GetRoleID(), ref modelprefab3, ref modeltexture3))
						{
							switch (info3.m_PopupType)
							{
							case PopupType.Armor_Head:
								go_role.ChangeAvatar(0, modelprefab3, modeltexture3);
								break;
							case PopupType.Armor_Body:
								go_role.ChangeAvatar(1, modelprefab3, modeltexture3);
								break;
							case PopupType.Armor_Leg:
								go_role.ChangeAvatar(2, modelprefab3, modeltexture3);
								break;
							case PopupType.Accessory_Halo:
								go_role.ChangeAvatarEffect(3, modelprefab3);
								break;
							case PopupType.Armor_Bracelet:
								go_role.ChangeAvatarEffect(4, modelprefab3);
								go_role.ChangeAvatarEffect(5, modelprefab3);
								break;
							case PopupType.Accessory_Necklace:
								go_role.ChangeAvatarEffect(6, modelprefab3);
								break;
							}
						}
					}
				}
			}
			m_PopupShow_Equip.Equip();
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_WeaponUnEquip)
		{
			if (!m_event.GetControlSuccess() || go_role == null)
			{
				return;
			}
			TUIPopupInfo info4 = m_PopupShow_Equip.m_SelectItem.GetInfo();
			if (info4 != null)
			{
				Debug.Log(info4.texture_id);
				if (info4.IsArmor() || info4.IsAccessory())
				{
					List<TUIPopupInfo> data2 = m_PopupShow_Role.GetData(PopupType.Roles);
					if (data2 != null)
					{
						foreach (TUIPopupInfo item2 in data2)
						{
							if (item2.m_CharacterAttribute != null && TUIMappingInfo.Instance().m_GetCharacterDefaultAvatar != null)
							{
								int avatarid = -1;
								switch (info4.m_PopupType)
								{
								case PopupType.Armor_Head:
									TUIMappingInfo.Instance().m_GetCharacterDefaultAvatar(item2.texture_id, WeaponType.Armor_Head, ref avatarid);
									item2.m_CharacterAttribute.m_nAvatarHead = avatarid;
									break;
								case PopupType.Armor_Body:
									TUIMappingInfo.Instance().m_GetCharacterDefaultAvatar(item2.texture_id, WeaponType.Armor_Body, ref avatarid);
									item2.m_CharacterAttribute.m_nAvatarUpper = avatarid;
									break;
								case PopupType.Armor_Leg:
									TUIMappingInfo.Instance().m_GetCharacterDefaultAvatar(item2.texture_id, WeaponType.Armor_Leg, ref avatarid);
									item2.m_CharacterAttribute.m_nAvatarLower = avatarid;
									break;
								case PopupType.Accessory_Halo:
									TUIMappingInfo.Instance().m_GetCharacterDefaultAvatar(item2.texture_id, WeaponType.Accessory_Halo, ref avatarid);
									item2.m_CharacterAttribute.m_nAvatarHeadup = avatarid;
									break;
								case PopupType.Armor_Bracelet:
									TUIMappingInfo.Instance().m_GetCharacterDefaultAvatar(item2.texture_id, WeaponType.Armor_Bracelet, ref avatarid);
									item2.m_CharacterAttribute.m_nAvatarBracelet = avatarid;
									break;
								case PopupType.Accessory_Necklace:
									TUIMappingInfo.Instance().m_GetCharacterDefaultAvatar(item2.texture_id, WeaponType.Accessory_Necklace, ref avatarid);
									item2.m_CharacterAttribute.m_nAvatarNeck = avatarid;
									break;
								}
							}
						}
					}
					WeaponType weaponType = WeaponType.None;
					switch (info4.m_PopupType)
					{
					case PopupType.Armor_Head:
						weaponType = WeaponType.Armor_Head;
						break;
					case PopupType.Armor_Body:
						weaponType = WeaponType.Armor_Body;
						break;
					case PopupType.Armor_Leg:
						weaponType = WeaponType.Armor_Leg;
						break;
					case PopupType.Armor_Bracelet:
						go_role.ChangeAvatarEffect(4, null);
						go_role.ChangeAvatarEffect(5, null);
						break;
					case PopupType.Accessory_Halo:
						go_role.ChangeAvatarEffect(3, null);
						break;
					case PopupType.Accessory_Necklace:
						go_role.ChangeAvatarEffect(6, null);
						break;
					}
					if (weaponType != 0)
					{
						int avatarid2 = -1;
						if (TUIMappingInfo.Instance().m_GetCharacterDefaultAvatar != null && TUIMappingInfo.Instance().m_GetCharacterDefaultAvatar(top_bar.GetRoleID(), weaponType, ref avatarid2))
						{
							GameObject modelprefab4 = null;
							Texture modeltexture4 = null;
							switch (info4.m_PopupType)
							{
							case PopupType.Armor_Head:
								if (TUIMappingInfo.Instance().m_GetAvatarModel != null && TUIMappingInfo.Instance().m_GetAvatarModel(avatarid2, top_bar.GetRoleID(), ref modelprefab4, ref modeltexture4))
								{
									go_role.ChangeAvatar(0, modelprefab4, modeltexture4);
								}
								break;
							case PopupType.Armor_Body:
								if (TUIMappingInfo.Instance().m_GetAvatarModel != null && TUIMappingInfo.Instance().m_GetAvatarModel(avatarid2, top_bar.GetRoleID(), ref modelprefab4, ref modeltexture4))
								{
									go_role.ChangeAvatar(1, modelprefab4, modeltexture4);
								}
								break;
							case PopupType.Armor_Leg:
								if (TUIMappingInfo.Instance().m_GetAvatarModel != null && TUIMappingInfo.Instance().m_GetAvatarModel(avatarid2, top_bar.GetRoleID(), ref modelprefab4, ref modeltexture4))
								{
									go_role.ChangeAvatar(2, modelprefab4, modeltexture4);
								}
								break;
							}
						}
					}
				}
			}
			m_PopupShow_Equip.UnEquip();
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_WeaponExchange)
		{
			if (!m_event.GetControlSuccess() || popup_equip_frame == null || popup_equip_frame.items_weapon == null || m_PopupShow_Equip.m_nPopupType != PopupType.Weapons02)
			{
				return;
			}
			BtnItem_Item[] arrItem = popup_equip_frame.items_weapon.m_arrItem;
			int nExchangeIndex3 = m_PopupShow_Equip.m_nExchangeIndex1;
			int nExchangeIndex4 = m_PopupShow_Equip.m_nExchangeIndex2;
			if (nExchangeIndex3 >= 0 && nExchangeIndex3 < arrItem.Length && nExchangeIndex4 >= 0 && nExchangeIndex4 < arrItem.Length)
			{
				TUIPopupInfo info5 = arrItem[nExchangeIndex3].GetInfo();
				arrItem[nExchangeIndex3].SetInfo(arrItem[nExchangeIndex4].GetInfo(), true, true);
				arrItem[nExchangeIndex4].SetInfo(info5, true, true);
				if (go_role != null)
				{
					go_role.ChangeWeapon(info5.texture_id);
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_Back)
		{
			if (m_event.GetControlSuccess())
			{
				DoSceneChange(m_event.GetWparam(), "Scene_MainMenu");
				return;
			}
			m_fade_in_time = 0f;
			do_fade_in = false;
			m_fade.FadeIn();
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_WeaponChoose)
		{
			if (m_PopupShow_Equip.m_SelectItem == null)
			{
				return;
			}
			TUIPopupInfo info6 = m_PopupShow_Equip.m_SelectItem.GetInfo();
			if (info6 == null || info6.m_MarkType != NewMarkType.New)
			{
				return;
			}
			info6.m_MarkType = NewMarkType.None;
			NewMarkType newMarkType = NewMarkType.None;
			if (info6.IsWeapon())
			{
				newMarkType = m_PopupShow_Equip.RefreshMark(PopupType.Weapons01);
				popup_equip_frame.items_weapon.m_arrItem[0].SetMark(newMarkType);
				newMarkType = m_PopupShow_Equip.RefreshMark(PopupType.Weapons02);
				popup_equip_frame.items_weapon.m_arrItem[1].SetMark(newMarkType);
				popup_equip_frame.items_weapon.m_arrItem[2].SetMark(newMarkType);
			}
			else if (info6.IsArmor())
			{
				switch (info6.m_PopupType)
				{
				case PopupType.Armor_Head:
					newMarkType = m_PopupShow_Equip.RefreshMark(PopupType.Armor_Head);
					popup_equip_frame.items_armor.m_arrItem[0].SetMark(newMarkType);
					break;
				case PopupType.Armor_Body:
					newMarkType = m_PopupShow_Equip.RefreshMark(PopupType.Armor_Body);
					popup_equip_frame.items_armor.m_arrItem[1].SetMark(newMarkType);
					break;
				case PopupType.Armor_Bracelet:
					newMarkType = m_PopupShow_Equip.RefreshMark(PopupType.Armor_Bracelet);
					popup_equip_frame.items_armor.m_arrItem[2].SetMark(newMarkType);
					break;
				case PopupType.Armor_Leg:
					newMarkType = m_PopupShow_Equip.RefreshMark(PopupType.Armor_Leg);
					popup_equip_frame.items_armor.m_arrItem[3].SetMark(newMarkType);
					break;
				}
			}
			else if (info6.IsAccessory())
			{
				switch (info6.m_PopupType)
				{
				case PopupType.Accessory_Halo:
					newMarkType = m_PopupShow_Equip.RefreshMark(PopupType.Accessory_Halo);
					popup_equip_frame.items_accessory.m_arrItem[0].SetMark(newMarkType);
					break;
				case PopupType.Accessory_Necklace:
					newMarkType = m_PopupShow_Equip.RefreshMark(PopupType.Accessory_Necklace);
					popup_equip_frame.items_accessory.m_arrItem[1].SetMark(newMarkType);
					break;
				case PopupType.Accessory_Badge:
					newMarkType = m_PopupShow_Equip.RefreshMark(PopupType.Accessory_Badge);
					popup_equip_frame.items_accessory.m_arrItem[2].SetMark(newMarkType);
					break;
				case PopupType.Accessory_Stoneskin:
					newMarkType = m_PopupShow_Equip.RefreshMark(PopupType.Accessory_Stoneskin);
					popup_equip_frame.items_accessory.m_arrItem[3].SetMark(newMarkType);
					break;
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_RolesChoose)
		{
			if (!(m_PopupShow_Role.m_SelectItem == null))
			{
				TUIPopupInfo info7 = m_PopupShow_Role.m_SelectItem.GetInfo();
				if (info7 != null && info7.m_MarkType == NewMarkType.New)
				{
					info7.m_MarkType = NewMarkType.None;
					m_ButtonRole.SetMark(m_PopupShow_Role.RefreshMark(PopupType.Roles));
				}
			}
		}
		else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_SkillChoose)
		{
			if (m_PopupShow_PassiveSkill.m_SelectItem == null)
			{
				return;
			}
			TUIPopupInfo info8 = m_PopupShow_PassiveSkill.m_SelectItem.GetInfo();
			if (info8 == null || info8.m_MarkType != NewMarkType.New)
			{
				return;
			}
			info8.m_MarkType = NewMarkType.None;
			NewMarkType mark2 = m_PopupShow_PassiveSkill.RefreshMark(PopupType.Skills);
			for (int k = 0; k < m_arrButtonSkill.Length; k++)
			{
				if (!(m_arrButtonSkill[k] == null))
				{
					m_arrButtonSkill[k].SetMark(mark2);
				}
			}
		}
		else
		{
			if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_RoleNewMarkInfo || m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_SkillNewMarkInfo)
			{
				return;
			}
			if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_WeaponNewMarkInfo)
			{
				if (m_event.GetEventInfo() != null && m_event.GetEventInfo().equip_info == null)
				{
				}
			}
			else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_EnterIAP)
			{
				if (m_event.GetControlSuccess())
				{
					DoSceneChange(m_event.GetWparam(), "Scene_IAP");
					return;
				}
				m_fade_in_time = 0f;
				do_fade_in = false;
				m_fade.FadeIn();
			}
			else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_EnterGold)
			{
				if (m_event.GetControlSuccess())
				{
					DoSceneChange(m_event.GetWparam(), "Scene_Gold");
					return;
				}
				m_fade_in_time = 0f;
				do_fade_in = false;
				m_fade.FadeIn();
			}
			else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_EnterGoBuyWeapon)
			{
				if (m_event.GetControlSuccess())
				{
					DoSceneChange(m_event.GetWparam(), "Scene_Forge");
					return;
				}
				m_fade_in_time = 0f;
				do_fade_in = false;
				m_fade.FadeIn();
			}
			else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_EnterGoBuyWeaponInBlack)
			{
				if (m_event.GetControlSuccess())
				{
					DoSceneChange(m_event.GetWparam(), "Scene_BlackMarket");
					return;
				}
				m_fade_in_time = 0f;
				do_fade_in = false;
				m_fade.FadeIn();
			}
			else if (m_event.GetEventName() == TUIEvent.SceneEquipEventType.TUIEvent_EnterGoBuySkill)
			{
				if (m_event.GetControlSuccess())
				{
					DoSceneChange(m_event.GetWparam(), "Scene_Skill");
					return;
				}
				m_fade_in_time = 0f;
				do_fade_in = false;
				m_fade.FadeIn();
			}
			else
			{
				if (m_event.GetEventName() != TUIEvent.SceneEquipEventType.TUIEvent_SkipTutorial)
				{
					return;
				}
				if (m_event.GetControlSuccess())
				{
					if (popup_new_help != null)
					{
						popup_new_help.Hide();
						popup_new_help.ShowSkipTutorial(false);
					}
					AndroidReturnPlugin.instance.SetSkipTutorialFunc(null);
					AndroidReturnPlugin.instance.m_bSkipTutorial = false;
					AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseSkipTutorial);
				}
				else
				{
					if (popup_new_help != null)
					{
						popup_new_help.ShowSkipTutorial(false);
					}
					AndroidReturnPlugin.instance.m_bSkipTutorial = false;
					AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseSkipTutorial);
				}
			}
		}
	}

	public void TUIEvent_PopupRole(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		Debug.Log("you click role");
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (m_PopupShow_Role == null)
		{
			return;
		}
		BtnItem_Item controlInParent = control.GetControlInParent<BtnItem_Item>();
		if (!(controlInParent == null))
		{
			m_PopupShow_Role.Show(PopupType.Roles, controlInParent);
			AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_ClosePopupRole);
			int nChooseID = -1;
			if (controlInParent.GetInfo() != null)
			{
				nChooseID = controlInParent.GetInfo().texture_id;
			}
			nChooseID = m_PopupShow_Role.InitScrollList(nChooseID);
			if (nChooseID > 0)
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_RolesChoose, nChooseID));
			}
		}
	}

	public void TUIEvent_PopupSkill01(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (!(m_PopupShow_ActiveSkill == null))
		{
			BtnItem_Item controlInParent = control.GetControlInParent<BtnItem_Item>();
			if (!(controlInParent == null))
			{
				m_PopupShow_ActiveSkill.ShowDirectly(PopupType.Skills01);
				m_PopupShow_ActiveSkill.SetSimpleInfo(controlInParent.GetInfo());
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_ClosePopupSkill01);
			}
		}
	}

	public void TUIEvent_PopupSkill(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		Debug.Log("You click skill");
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (m_PopupShow_PassiveSkill == null)
		{
			return;
		}
		if (m_PopupShow_PassiveSkill.IsEmpty(PopupType.Skills))
		{
			popup_go_buy.Show(PopupGoBuy.GoBuyType.Skill, top_bar.GetRoleID());
			AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseGoBuy);
			return;
		}
		BtnItem_Item controlInParent = control.GetControlInParent<BtnItem_Item>();
		if (!(controlInParent == null))
		{
			m_PopupShow_PassiveSkill.Show(PopupType.Skills, controlInParent);
			AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_ClosePopupSkill);
			int nChooseID = -1;
			if (controlInParent.GetInfo() != null)
			{
				nChooseID = controlInParent.GetInfo().texture_id;
			}
			nChooseID = m_PopupShow_PassiveSkill.InitScrollList(nChooseID);
			if (nChooseID > 0)
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_SkillChoose, nChooseID));
			}
		}
	}

	public void TUIEvent_ChangeWeaponItems(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 1)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_equip_frame != null)
			{
				popup_equip_frame.SetItemsChoose(1);
			}
		}
	}

	public void TUIEvent_ChangeArmorItems(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 1)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_equip_frame != null)
			{
				popup_equip_frame.SetItemsChoose(2);
			}
		}
	}

	public void TUIEvent_ChangeAccessoryItems(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 1)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_equip_frame != null)
			{
				popup_equip_frame.SetItemsChoose(3);
			}
		}
	}

	public void TUIEvent_PopupWeapon(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		Debug.Log("You click weapon");
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (m_PopupShow_Equip == null)
		{
			return;
		}
		BtnItem_Item controlInParent = control.GetControlInParent<BtnItem_Item>();
		if (controlInParent == null)
		{
			return;
		}
		PopupType nPopupType = controlInParent.nPopupType;
		if (m_PopupShow_Equip.IsEmpty(nPopupType))
		{
			if (popup_go_buy != null)
			{
				if (nPopupType == PopupType.Accessory_Halo || nPopupType == PopupType.Accessory_Necklace || nPopupType == PopupType.Accessory_Badge)
				{
					popup_go_buy.Show(PopupGoBuy.GoBuyType.WeaponInBlack, (int)nPopupType);
				}
				else
				{
					popup_go_buy.Show(PopupGoBuy.GoBuyType.Weapon, (int)nPopupType);
				}
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseGoBuy);
			}
			return;
		}
		m_PopupShow_Equip.Show(nPopupType, controlInParent);
		AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_ClosePopupWeapon);
		int nChooseID = -1;
		if (controlInParent.GetInfo() != null)
		{
			nChooseID = controlInParent.GetInfo().texture_id;
		}
		nChooseID = m_PopupShow_Equip.InitScrollList(nChooseID);
		if (nChooseID > 0)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_WeaponChoose, nChooseID));
		}
	}

	public void TUIEvent_PopupRoleSelect(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 1)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Drag");
		}
		BtnSelect_Item component = control.GetComponent<BtnSelect_Item>();
		if (!(component == null))
		{
			TUIPopupInfo info = component.GetInfo();
			if (info != null)
			{
				m_PopupShow_Role.SetItemSelectInfo(component);
				int texture_id = info.texture_id;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_RolesChoose, texture_id));
			}
		}
	}

	public void TUIEvent_PopupSkillSelect(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 1)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Drag");
		}
		BtnSelect_Item component = control.GetComponent<BtnSelect_Item>();
		if (!(component == null))
		{
			TUIPopupInfo info = component.GetInfo();
			if (info != null)
			{
				m_PopupShow_PassiveSkill.SetItemSelectInfo(component);
				int texture_id = info.texture_id;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_SkillChoose, texture_id));
			}
		}
	}

	public void TUIEvent_PopupWeaponSelect(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 1)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Drag");
		}
		BtnSelect_Item component = control.GetComponent<BtnSelect_Item>();
		if (!(component == null))
		{
			TUIPopupInfo info = component.GetInfo();
			if (info != null)
			{
				m_PopupShow_Equip.SetItemSelectInfo(component);
				int texture_id = info.texture_id;
				Debug.Log("chooseID:" + texture_id);
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_WeaponChoose, texture_id));
			}
		}
	}

	public void TUIEvent_PopupRoleEquip(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
				CUISound.GetInstance().Play("UI_Use");
			}
			if (m_PopupShow_Role.m_SelectItem != null && m_PopupShow_Role.m_SelectItem.GetInfo() != null)
			{
				int texture_id = m_PopupShow_Role.m_SelectItem.GetInfo().texture_id;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_RoleEquip, texture_id));
			}
			m_PopupShow_Role.Hide();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_ClosePopupRole);
		}
	}

	public void TUIEvent_PopupSkillEquip(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Equip");
		}
		if (m_PopupShow_PassiveSkill.m_SelectItem != null && m_PopupShow_PassiveSkill.m_ButtonItem != null && m_PopupShow_PassiveSkill.m_SelectItem.GetInfo() != null)
		{
			if (m_PopupShow_PassiveSkill.IsExchange())
			{
				int nExchangeIndex = m_PopupShow_PassiveSkill.m_nExchangeIndex1;
				int nExchangeIndex2 = m_PopupShow_PassiveSkill.m_nExchangeIndex2;
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_SkillExchange, nExchangeIndex, nExchangeIndex2));
			}
			else
			{
				int index = m_PopupShow_PassiveSkill.m_ButtonItem.index;
				int texture_id = m_PopupShow_PassiveSkill.m_SelectItem.GetInfo().texture_id;
				if (m_PopupShow_PassiveSkill.m_ButtonItem.GetInfo() != null && m_PopupShow_PassiveSkill.m_ButtonItem.GetInfo().texture_id == texture_id)
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_SkillUnEquip, index, texture_id));
				}
				else
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_SkillEquip, index, texture_id));
				}
			}
		}
		m_PopupShow_PassiveSkill.Hide();
		AndroidReturnPlugin.instance.ClearFunc(TUIEvent_ClosePopupSkill);
	}

	public void TUIEvent_PopupWeaponEquip(TUIControl control, int event_type, float wapram, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Equip");
		}
		if (m_PopupShow_Equip.m_SelectItem != null && m_PopupShow_Equip.m_SelectItem.GetInfo() != null && m_PopupShow_Equip.m_ButtonItem != null)
		{
			if (m_PopupShow_Equip.IsExchange())
			{
				int nExchangeIndex = m_PopupShow_Equip.m_nExchangeIndex1;
				int nExchangeIndex2 = m_PopupShow_Equip.m_nExchangeIndex2;
				Debug.Log(nExchangeIndex + "  " + nExchangeIndex2);
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_WeaponExchange, nExchangeIndex, nExchangeIndex2, m_PopupShow_Equip.m_ButtonItem.nPopupType));
			}
			else
			{
				int index = m_PopupShow_Equip.m_ButtonItem.index;
				int texture_id = m_PopupShow_Equip.m_SelectItem.GetInfo().texture_id;
				if (m_PopupShow_Equip.m_ButtonItem.GetInfo() != null && m_PopupShow_Equip.m_ButtonItem.GetInfo().texture_id == texture_id)
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_WeaponUnEquip, index, texture_id, m_PopupShow_Equip.m_ButtonItem.nPopupType));
				}
				else
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_WeaponEquip, index, texture_id, m_PopupShow_Equip.m_ButtonItem.nPopupType));
				}
			}
		}
		m_PopupShow_Equip.Hide();
		AndroidReturnPlugin.instance.ClearFunc(TUIEvent_ClosePopupWeapon);
	}

	public void TUIEvent_ClosePopupRole(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			m_PopupShow_Role.Hide();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_ClosePopupRole);
		}
	}

	public void TUIEvent_ClosePopupSkill01(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			m_PopupShow_ActiveSkill.Hide();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_ClosePopupSkill01);
		}
	}

	public void TUIEvent_ClosePopupSkill(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			m_PopupShow_PassiveSkill.Hide();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_ClosePopupSkill);
		}
	}

	public void TUIEvent_ClosePopupWeapon(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			m_PopupShow_Equip.Hide();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_ClosePopupWeapon);
		}
	}

	public void TUIEvent_MoveScreen(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 2)
		{
			go_role.SetRotation(wparam, lparam);
		}
	}

	public void TUIEvent_Back(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_Back));
		}
	}

	public void TUIEvent_IAP(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_EnterIAP));
		}
	}

	public void TUIEvent_Gold(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_EnterGold));
		}
	}

	public void TUIEvent_CloseGoBuy(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Cancle");
			}
			popup_go_buy.Hide();
			AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseGoBuy);
		}
	}

	public void TUIEvent_ClickGoBuy(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (control.transform.parent == null || control.transform.parent.parent == null)
		{
			Debug.Log("error!");
			return;
		}
		PopupGoBuy component = control.transform.parent.parent.GetComponent<PopupGoBuy>();
		if (component == null)
		{
			Debug.Log("error!");
		}
		else if (component.GetGoBuyType() == PopupGoBuy.GoBuyType.Weapon)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_EnterGoBuyWeapon, component.m_nLinkID));
		}
		else if (component.GetGoBuyType() == PopupGoBuy.GoBuyType.Skill)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_EnterGoBuySkill, component.m_nLinkID));
		}
		else if (component.GetGoBuyType() == PopupGoBuy.GoBuyType.WeaponInBlack)
		{
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_EnterGoBuyWeaponInBlack, component.m_nLinkID));
		}
	}

	public void TUIEvent_ClickNewHelp(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type != 3)
		{
			return;
		}
		if (sfx_open_now)
		{
			CUISound.GetInstance().Play("UI_Button");
		}
		if (control == popup_new_help.GetBtnMask())
		{
			popup_new_help.DoBtnMaskEvent();
			return;
		}
		switch (TUIMappingInfo.Instance().GetNewHelpState())
		{
		case NewHelpState.Help05_ClickOpenWeaponEquip:
		{
			m_PopupShow_Equip.Show(PopupType.Weapons02, popup_equip_frame.items_weapon.m_arrItem[2]);
			BtnSelect_Item btnSelect_Item = m_PopupShow_Equip.SetItemSelectInfo(PopupType.Weapons02, 4);
			if (btnSelect_Item != null && btnSelect_Item.GetInfo() != null)
			{
				int texture_id2 = btnSelect_Item.GetInfo().texture_id;
				if (texture_id2 != 0)
				{
					global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_WeaponChoose, texture_id2));
				}
			}
			TUIMappingInfo.Instance().NextNewHelpState();
			NewHelpState newHelpState3 = TUIMappingInfo.Instance().GetNewHelpState();
			popup_new_help.SetHelpState(newHelpState3, 0f);
			break;
		}
		case NewHelpState.Help06_ClickWeaponEquip:
		{
			int index2 = m_PopupShow_Equip.m_ButtonItem.index;
			int texture_id3 = m_PopupShow_Equip.m_SelectItem.GetInfo().texture_id;
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_WeaponEquip, index2, texture_id3, PopupType.Weapons02));
			m_PopupShow_Equip.Hide();
			TUIMappingInfo.Instance().NextNewHelpState();
			NewHelpState newHelpState4 = TUIMappingInfo.Instance().GetNewHelpState();
			popup_new_help.SetHelpState(newHelpState4, 0f);
			break;
		}
		case NewHelpState.Help07_ClickBackToVillage:
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_Back));
			TUIMappingInfo.Instance().NextNewHelpState();
			break;
		case NewHelpState.Help15_ClickOpenSkillEquip:
		{
			m_PopupShow_PassiveSkill.Show(PopupType.Skills, m_arrButtonSkill[0]);
			int num = m_PopupShow_PassiveSkill.InitScrollList(1001);
			if (num != 0)
			{
				global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_SkillChoose, num));
			}
			TUIMappingInfo.Instance().NextNewHelpState();
			NewHelpState newHelpState2 = TUIMappingInfo.Instance().GetNewHelpState();
			popup_new_help.SetHelpState(newHelpState2, 0f);
			break;
		}
		case NewHelpState.Help16_ClickSkillEquip:
		{
			int index = m_PopupShow_PassiveSkill.m_ButtonItem.index;
			int texture_id = m_PopupShow_PassiveSkill.m_SelectItem.GetInfo().texture_id;
			Debug.Log(index + "  " + texture_id);
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_SkillEquip, index, texture_id));
			m_PopupShow_PassiveSkill.Hide();
			TUIMappingInfo.Instance().NextNewHelpState();
			NewHelpState newHelpState = TUIMappingInfo.Instance().GetNewHelpState();
			popup_new_help.SetHelpState(newHelpState, 0f);
			break;
		}
		case NewHelpState.Help17_ClickBackToVillage:
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_Back));
			TUIMappingInfo.Instance().NextNewHelpState();
			break;
		}
	}

	public void TUIEvent_OpenSkipTutorial(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_new_help != null)
			{
				popup_new_help.ShowSkipTutorial(true);
				AndroidReturnPlugin.instance.m_bSkipTutorial = true;
				AndroidReturnPlugin.instance.SetCurFunc(TUIEvent_CloseSkipTutorial);
			}
		}
	}

	public void TUIEvent_SkipTutorial(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			global::EventCenter.EventCenter.Instance.Publish(this, new TUIEvent.SendEvent_SceneEquip(TUIEvent.SceneEquipEventType.TUIEvent_SkipTutorial));
		}
	}

	public void TUIEvent_CloseSkipTutorial(TUIControl control, int event_type, float wparam, float lparam, object data)
	{
		if (event_type == 3)
		{
			if (sfx_open_now)
			{
				CUISound.GetInstance().Play("UI_Button");
			}
			if (popup_new_help != null)
			{
				popup_new_help.ShowSkipTutorial(false);
				AndroidReturnPlugin.instance.m_bSkipTutorial = false;
				AndroidReturnPlugin.instance.ClearFunc(TUIEvent_CloseSkipTutorial);
			}
		}
	}

	public void DoSceneChange(int m_scene_id, string m_scene_normal)
	{
		string sceneName = TUIMappingInfo.Instance().GetSceneName(m_scene_id);
		if (sceneName != string.Empty)
		{
			next_scene = sceneName;
		}
		else
		{
			next_scene = m_scene_normal;
		}
		if (!is_fade_out)
		{
			is_fade_out = true;
			m_fade.FadeOut();
		}
	}

	private void DoNewHelp()
	{
		if (popup_new_help != null)
		{
			popup_new_help.ResetHelpState();
			NewHelpState newHelpState = TUIMappingInfo.Instance().GetNewHelpState();
			if (newHelpState != NewHelpState.Help_Over && newHelpState != NewHelpState.None)
			{
				popup_new_help.Show();
				popup_new_help.SetHelpState(newHelpState, 0f);
				AndroidReturnPlugin.instance.SetSkipTutorialFunc(TUIEvent_OpenSkipTutorial);
			}
		}
	}
}
