using System.Collections.Generic;
using UnityEngine;

public class PopupInfoCard : MonoBehaviour
{
	public GameObject go_popup;

	public TUILabel label_name;

	public PopupTitleList popup_title_list;

	public TUILabel label_title;

	public TUIButtonClick btn_title_change;

	public TUIMeshSprite img_player;

	public TUILabel label_likes;

	public TUILabel label_rating;

	public TUILabel label_hunter_lv;

	public TUILabel label_hunter_exp;

	public TUIMeshSpriteSliced img_hunter_exp;

	public TUILabel label_progress;

	public TUIMeshSpriteSliced img_progress;

	public TUILabel label_status;

	public TUIButtonClick btn_status;

	public Role_Control m_RoleControl;

	private bool is_open;

	private Vector2 player_texture_size;

	private Vector2 img_texture_size;

	private TUICoopPlayerInfo coop_player_info;

	private Dictionary<int, string> title_list;

	private string default_texture = "juesemoren";

	private void Awake()
	{
		player_texture_size = new Vector2(100f, 100f);
		if (img_player != null)
		{
			img_texture_size = img_player.transform.localScale;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Show(bool m_open, bool m_myself = false, TUICoopPlayerInfo m_player_info = null)
	{
		is_open = m_open;
		coop_player_info = m_player_info;
		if (m_open)
		{
			base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
			if (go_popup != null && go_popup.GetComponent<Animation>() != null)
			{
				go_popup.GetComponent<Animation>().Play();
			}
			if (btn_title_change != null)
			{
				if (!m_myself)
				{
					btn_title_change.Disable(true);
				}
				else
				{
					btn_title_change.Disable(false);
					btn_title_change.Reset();
				}
			}
			if (btn_status != null)
			{
				if (!m_myself)
				{
					btn_status.gameObject.SetActiveRecursively(false);
					return;
				}
				btn_status.gameObject.SetActiveRecursively(true);
				btn_status.Reset();
			}
		}
		else
		{
			base.transform.localPosition = new Vector3(-1000f, 0f, base.transform.localPosition.z);
			SetInfoNull();
		}
	}

	public void SetInfo(TUICoopPlayerInfo m_info, bool m_myself, Dictionary<int, string> m_title_list)
	{
		if (coop_player_info == null)
		{
			coop_player_info = m_info;
			return;
		}
		if (m_info.change_id)
		{
			coop_player_info.id = m_info.id;
		}
		if (m_info.change_name)
		{
			coop_player_info.name = m_info.name;
		}
		if (m_info.change_likes)
		{
			coop_player_info.likes = m_info.likes;
		}
		if (m_info.change_progress)
		{
			coop_player_info.progress = m_info.progress;
		}
		if (m_info.change_status)
		{
			coop_player_info.status = m_info.status;
		}
		if (m_info.change_title_id)
		{
			coop_player_info.title_id = m_info.title_id;
		}
		if (m_info.change_title_id_list)
		{
			coop_player_info.title_id_list = m_info.title_id_list;
		}
		if (m_info.change_all_ranking)
		{
			coop_player_info.all_ranking = m_info.all_ranking;
		}
		if (m_info.change_friends_ranking)
		{
			coop_player_info.friends_ranking = m_info.friends_ranking;
		}
		if (m_info.change_GC_texture)
		{
			coop_player_info.GC_texture = m_info.GC_texture;
		}
		if (m_info.change_hunter_level)
		{
			coop_player_info.hunter_level = m_info.hunter_level;
		}
		if (m_info.change_hunter_exp)
		{
			coop_player_info.hunter_exp = m_info.hunter_exp;
		}
		if (m_info.change_hunter_update_exp)
		{
			coop_player_info.hunter_update_exp = m_info.hunter_update_exp;
		}
		if (m_info.change_role_info)
		{
			coop_player_info.role_info = m_info.role_info;
		}
		title_list = m_title_list;
		if (img_player != null)
		{
			if (coop_player_info.GC_texture != null)
			{
				if (coop_player_info.GC_texture.width != 0 && coop_player_info.GC_texture.height != 0)
				{
					img_player.UseCustomize = true;
					img_player.CustomizeTexture = coop_player_info.GC_texture;
					img_player.CustomizeRect = new Rect(0f, 0f, coop_player_info.GC_texture.width, coop_player_info.GC_texture.height);
					float x = player_texture_size.x * img_texture_size.x / (float)coop_player_info.GC_texture.width;
					float y = player_texture_size.y * img_texture_size.y / (float)coop_player_info.GC_texture.height;
					img_player.transform.localScale = new Vector3(x, y, 1f);
				}
			}
			else
			{
				img_player.UseCustomize = false;
				img_player.texture = default_texture;
				img_player.transform.localScale = img_texture_size;
			}
		}
		if (label_name != null)
		{
			if (coop_player_info.name == string.Empty)
			{
				label_name.Text = "--";
			}
			else
			{
				label_name.Text = coop_player_info.name;
			}
		}
		if (label_hunter_lv != null)
		{
			if (coop_player_info.hunter_level == 0)
			{
				label_hunter_lv.Text = "--";
			}
			else
			{
				label_hunter_lv.Text = coop_player_info.hunter_level.ToString();
			}
		}
		if (label_hunter_exp != null)
		{
			label_hunter_exp.Text = coop_player_info.hunter_exp + "/" + coop_player_info.hunter_update_exp;
		}
		if (img_hunter_exp != null)
		{
			if (coop_player_info.hunter_update_exp != 0)
			{
				float num = (float)coop_player_info.hunter_exp / (float)coop_player_info.hunter_update_exp;
				if (num > 1f)
				{
					num = 1f;
				}
				else if (num < 0f)
				{
					num = 0f;
				}
				img_hunter_exp.Size = new Vector2(198f * num, img_hunter_exp.Size.y);
			}
			else
			{
				img_hunter_exp.Size = new Vector2(0f, img_hunter_exp.Size.y);
			}
		}
		if (label_title != null)
		{
			int title_id = coop_player_info.title_id;
			if (m_title_list != null && m_title_list.ContainsKey(title_id))
			{
				label_title.Text = m_title_list[title_id];
			}
			else
			{
				label_title.Text = "--";
			}
		}
		if (img_progress != null)
		{
			float num2 = coop_player_info.progress;
			if (num2 > 1f)
			{
				num2 = 1f;
			}
			img_progress.Size = new Vector2(446f * num2, img_progress.Size.y);
		}
		if (label_progress != null)
		{
			label_progress.Text = (float)Mathf.FloorToInt(coop_player_info.progress * 1000f) / 10f + "%";
		}
		if (label_rating != null)
		{
			if (coop_player_info.role_info != null)
			{
				label_rating.Text = "{color:FFC217FF}RATING:  {color:FFFFFFFF}" + coop_player_info.role_info.role_rating;
			}
			else
			{
				label_rating.Text = "{color:FFC217FF}RATING:    --";
			}
		}
		if (label_likes != null)
		{
			label_likes.Text = coop_player_info.likes.ToString();
		}
		if (label_status != null)
		{
			label_status.Text = coop_player_info.status;
		}
		if (btn_title_change != null)
		{
			if (!m_myself)
			{
				btn_title_change.Disable(true);
			}
			else
			{
				btn_title_change.Disable(false);
				btn_title_change.Reset();
			}
		}
		if (btn_status != null)
		{
			if (!m_myself)
			{
				btn_status.gameObject.SetActiveRecursively(false);
			}
			else
			{
				btn_status.gameObject.SetActiveRecursively(true);
				btn_status.Reset();
			}
		}
		if (!(m_RoleControl != null) || coop_player_info.role_info == null)
		{
			return;
		}
		TUICoopRoleInfo role_info = coop_player_info.role_info;
		if (role_info.m_nModelID <= 0)
		{
			return;
		}
		m_RoleControl.ChangeRole(role_info.m_nModelID);
		if (TUIMappingInfo.Instance().m_GetAvatarModel != null)
		{
			GameObject modelprefab = null;
			Texture modeltexture = null;
			if (TUIMappingInfo.Instance().m_GetAvatarModel(role_info.m_nAvatarHead, role_info.role_id, ref modelprefab, ref modeltexture))
			{
				m_RoleControl.ChangeAvatar(0, modelprefab, modeltexture);
			}
			if (TUIMappingInfo.Instance().m_GetAvatarModel(role_info.m_nAvatarUpper, role_info.role_id, ref modelprefab, ref modeltexture))
			{
				m_RoleControl.ChangeAvatar(1, modelprefab, modeltexture);
			}
			if (TUIMappingInfo.Instance().m_GetAvatarModel(role_info.m_nAvatarLower, role_info.role_id, ref modelprefab, ref modeltexture))
			{
				m_RoleControl.ChangeAvatar(2, modelprefab, modeltexture);
			}
			if (role_info.m_nAvatarHeadup == -1)
			{
				m_RoleControl.ChangeAvatarEffect(3, null);
			}
			else if (TUIMappingInfo.Instance().m_GetAvatarModel(role_info.m_nAvatarHeadup, role_info.role_id, ref modelprefab, ref modeltexture))
			{
				m_RoleControl.ChangeAvatarEffect(3, modelprefab);
			}
			if (role_info.m_nAvatarBracelet == -1)
			{
				m_RoleControl.ChangeAvatarEffect(4, null);
				m_RoleControl.ChangeAvatarEffect(5, null);
			}
			else if (TUIMappingInfo.Instance().m_GetAvatarModel(role_info.m_nAvatarBracelet, role_info.role_id, ref modelprefab, ref modeltexture))
			{
				m_RoleControl.ChangeAvatarEffect(4, modelprefab);
				m_RoleControl.ChangeAvatarEffect(5, modelprefab);
			}
			if (role_info.m_nAvatarNeck == -1)
			{
				m_RoleControl.ChangeAvatarEffect(6, null);
			}
			else if (TUIMappingInfo.Instance().m_GetAvatarModel(role_info.m_nAvatarNeck, role_info.role_id, ref modelprefab, ref modeltexture))
			{
				m_RoleControl.ChangeAvatarEffect(6, modelprefab);
			}
		}
		m_RoleControl.SetRoleFixedRotation(new Vector3(0f, -40f, 0f));
		m_RoleControl.SetRandomWeapon(true, role_info.weapon_list);
		m_RoleControl.Show(true);
	}

	public void SetInfoNull()
	{
		if (img_player != null)
		{
			img_player.UseCustomize = false;
			img_player.texture = default_texture;
			img_player.transform.localScale = img_texture_size;
		}
		if (label_name != null)
		{
			label_name.Text = "--";
		}
		if (label_hunter_lv != null)
		{
			label_hunter_lv.Text = "--";
		}
		if (label_hunter_exp != null)
		{
			label_hunter_exp.Text = "0/0";
		}
		if (img_hunter_exp != null)
		{
			img_hunter_exp.Size = new Vector2(0f, img_hunter_exp.Size.y);
		}
		if (label_title != null)
		{
			label_title.Text = "--";
		}
		if (img_progress != null)
		{
			img_progress.Size = new Vector2(0f, img_progress.Size.y);
		}
		if (label_progress != null)
		{
			label_progress.Text = "0%";
		}
		if (label_rating != null)
		{
			label_rating.Text = "--";
		}
		if (label_likes != null)
		{
			label_likes.Text = "0";
		}
		if (label_status != null)
		{
			label_status.Text = string.Empty;
		}
		if (btn_title_change != null)
		{
			btn_title_change.Disable(true);
		}
		if (btn_status != null)
		{
			btn_status.gameObject.SetActiveRecursively(false);
		}
		if (m_RoleControl != null)
		{
			m_RoleControl.SetRandomWeapon(false, null);
			m_RoleControl.Show(false);
		}
	}

	public void UpdateInfoCardTexture(TUIUpdatePlayerTextureInfo m_info)
	{
		if (m_info != null && coop_player_info != null && coop_player_info.id == m_info.id && img_player != null && m_info.player_texture != null)
		{
			coop_player_info.GC_texture = m_info.player_texture;
			if (m_info.player_texture.width != 0 && m_info.player_texture.height != 0)
			{
				img_player.UseCustomize = true;
				img_player.CustomizeTexture = m_info.player_texture;
				img_player.CustomizeRect = new Rect(0f, 0f, m_info.player_texture.width, m_info.player_texture.height);
				float x = player_texture_size.x * img_texture_size.x / (float)m_info.player_texture.width;
				float y = player_texture_size.y * img_texture_size.y / (float)m_info.player_texture.height;
				img_player.transform.localScale = new Vector3(x, y, 1f);
				return;
			}
		}
		img_player.UseCustomize = false;
	}

	public void ShowTitleList(bool m_open, Dictionary<int, string> m_title_list, List<int> m_title_id_list, int m_my_title_id)
	{
		if (popup_title_list != null)
		{
			if (m_open)
			{
				popup_title_list.SetInfo(m_title_list, m_title_id_list, m_my_title_id);
				popup_title_list.transform.localPosition = new Vector3(0f, 0f, popup_title_list.transform.localPosition.z);
			}
			else
			{
				popup_title_list.transform.localPosition = new Vector3(0f, -1000f, popup_title_list.transform.localPosition.z);
			}
		}
	}

	public bool GetInfoCardShow()
	{
		return is_open;
	}

	public string GetPlayerID()
	{
		if (coop_player_info != null)
		{
			return coop_player_info.id;
		}
		return string.Empty;
	}

	public void UpdateTitle(int m_id)
	{
		if (title_list != null && title_list != null && title_list.ContainsKey(m_id) && label_title != null)
		{
			label_title.Text = title_list[m_id];
		}
	}

	public string GetStatus()
	{
		if (coop_player_info != null)
		{
			return coop_player_info.status;
		}
		return string.Empty;
	}

	public void SetStatus(string m_status)
	{
		if (label_status != null)
		{
			label_status.Text = m_status;
		}
	}

	public TUILabel GetStatusLabel()
	{
		return label_status;
	}

	public void SetModel(string head, string upper, string lower)
	{
		if (coop_player_info != null && !(m_RoleControl == null))
		{
		}
	}
}
