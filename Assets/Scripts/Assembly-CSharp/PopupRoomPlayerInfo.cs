using System.Collections.Generic;
using UnityEngine;

public class PopupRoomPlayerInfo : MonoBehaviour
{
	public TUILabel label_name;

	public TUILabel label_title;

	public TUILabel label_hunter_lv;

	public Role_Control role_control;

	public PlayerEnterText go_text;

	private TUICoopPlayerInfo player_info;

	public TUILabel label_rating;

	public TUIMeshSprite img_rating;

	private void Awake()
	{
		if (img_rating != null)
		{
			img_rating.gameObject.SetActiveRecursively(false);
		}
	}

	private void Start()
	{
		if (go_text != null)
		{
			go_text.gameObject.SetActiveRecursively(false);
		}
	}

	private void Update()
	{
		CheckTextEvent();
	}

	public void SetInfo(TUICoopPlayerInfo m_info, Dictionary<int, string> m_title_list, int m_pos_index)
	{
		player_info = m_info;
		if (m_info == null)
		{
			return;
		}
		if (label_name != null)
		{
			label_name.Text = m_info.name;
		}
		if (label_title != null && m_title_list != null && m_title_list.ContainsKey(m_info.title_id))
		{
			label_title.Text = m_title_list[m_info.title_id];
		}
		if (label_hunter_lv != null)
		{
			label_hunter_lv.Text = m_info.hunter_level.ToString();
		}
		TUICoopRoleInfo role_info = m_info.role_info;
		if (role_info == null)
		{
			return;
		}
		int role_id = role_info.role_id;
		int role_rating = role_info.role_rating;
		int role_level = role_info.role_level;
		if (label_rating != null)
		{
			label_rating.Text = role_rating.ToString();
		}
		if (img_rating != null)
		{
			img_rating.gameObject.SetActiveRecursively(true);
		}
		if (role_control != null)
		{
			role_control.ChangeRole(role_info.m_nModelID);
			if (TUIMappingInfo.Instance().m_GetAvatarModel != null)
			{
				GameObject modelprefab = null;
				Texture modeltexture = null;
				if (TUIMappingInfo.Instance().m_GetAvatarModel(role_info.m_nAvatarHead, role_info.role_id, ref modelprefab, ref modeltexture))
				{
					role_control.ChangeAvatar(0, modelprefab, modeltexture);
				}
				if (TUIMappingInfo.Instance().m_GetAvatarModel(role_info.m_nAvatarUpper, role_info.role_id, ref modelprefab, ref modeltexture))
				{
					role_control.ChangeAvatar(1, modelprefab, modeltexture);
				}
				if (TUIMappingInfo.Instance().m_GetAvatarModel(role_info.m_nAvatarLower, role_info.role_id, ref modelprefab, ref modeltexture))
				{
					role_control.ChangeAvatar(2, modelprefab, modeltexture);
				}
				if (TUIMappingInfo.Instance().m_GetAvatarModel(role_info.m_nAvatarHeadup, role_info.role_id, ref modelprefab, ref modeltexture))
				{
					role_control.ChangeAvatarEffect(3, modelprefab);
				}
				if (TUIMappingInfo.Instance().m_GetAvatarModel(role_info.m_nAvatarBracelet, role_info.role_id, ref modelprefab, ref modeltexture))
				{
					role_control.ChangeAvatarEffect(4, modelprefab);
					role_control.ChangeAvatarEffect(5, modelprefab);
				}
				if (TUIMappingInfo.Instance().m_GetAvatarModel(role_info.m_nAvatarNeck, role_info.role_id, ref modelprefab, ref modeltexture))
				{
					role_control.ChangeAvatarEffect(6, modelprefab);
				}
			}
			role_control.SetRoleFixedRotation(new Vector3(0f, 150f, 0f));
			role_control.SetBottomShow(true);
		}
		List<int> weapon_list = role_info.weapon_list;
		if (weapon_list != null)
		{
			role_control.ChangeWeapon(weapon_list[0]);
			role_control.SetRandomWeapon(true, weapon_list);
			role_control.SetPosMove(GetPlayerRunPos(m_pos_index, -1), GetPlayerRunPos(m_pos_index, 0), Role_Control.MoveType.Enter);
		}
		Debug.Log("PlayerEnter:" + m_info.id + "  Role:" + role_id);
	}

	public bool GetPlayerInfoExist()
	{
		if (player_info == null)
		{
			return false;
		}
		return true;
	}

	public void SetPlayerNull(int m_pos_index)
	{
		if (player_info != null && player_info.role_info != null)
		{
			Debug.Log("PlayerExit:" + player_info.id + "  Role:" + player_info.role_info.role_id);
		}
		player_info = null;
		if (role_control != null)
		{
			role_control.SetRoleFixedRotation(new Vector3(0f, 150f, 0f));
			role_control.SetPosMove(GetPlayerRunPos(m_pos_index, 0), GetPlayerRunPos(m_pos_index, 1), Role_Control.MoveType.Exit);
		}
		if (label_name != null)
		{
			label_name.Text = string.Empty;
		}
		if (label_title != null)
		{
			label_title.Text = string.Empty;
		}
		if (label_hunter_lv != null)
		{
			label_hunter_lv.Text = string.Empty;
		}
		if (label_rating != null)
		{
			label_rating.Text = string.Empty;
		}
		if (img_rating != null)
		{
			img_rating.gameObject.SetActiveRecursively(false);
		}
	}

	public string GetPlayerID()
	{
		if (player_info != null)
		{
			return player_info.id;
		}
		return string.Empty;
	}

	public void SetRoleRotation(float wparam, float lparam)
	{
		if (role_control != null)
		{
			role_control.SetRotation(wparam, lparam);
		}
	}

	private Vector3 GetPlayerRunPos(int m_index, int m_direction)
	{
		Vector3 result = Vector3.zero;
		switch (m_index)
		{
		case 1:
			result = new Vector3(206.9f, -94.76f, 0f);
			break;
		case 2:
			result = new Vector3(51.88f, -94.76f, 0f);
			break;
		case 3:
			result = new Vector3(-102.8f, -94.76f, 0f);
			break;
		}
		switch (m_direction)
		{
		case -1:
			switch (m_index)
			{
			case 1:
				result += new Vector3(-4f, 0f, 0f);
				break;
			case 2:
				result += new Vector3(-6f, 0f, 0f);
				break;
			case 3:
				result += new Vector3(-8f, 0f, 0f);
				break;
			}
			break;
		case 1:
			switch (m_index)
			{
			case 1:
				result += new Vector3(8f, 0f, 0f);
				break;
			case 2:
				result += new Vector3(6f, 0f, 0f);
				break;
			case 3:
				result += new Vector3(4f, 0f, 0f);
				break;
			}
			break;
		}
		return result;
	}

	private void CheckTextEvent()
	{
		if (!(role_control != null))
		{
			return;
		}
		if (role_control.PosMoveStopEvent() && go_text != null)
		{
			string info = string.Empty;
			if (player_info != null)
			{
				info = player_info.status;
			}
			go_text.SetInfo(info);
			go_text.Show(true);
		}
		if (role_control.PosMoveExitEvent() && go_text != null)
		{
			go_text.Show(false);
		}
	}

	public bool Event_TextShowOver()
	{
		if (go_text != null)
		{
			return go_text.Event_TextShowOver();
		}
		return false;
	}
}
