using System.Collections.Generic;
using UnityEngine;

public class PopupPlayerInfo : MonoBehaviour
{
	public TUIMeshSprite img_player;

	public TUILabel label_name;

	public TUILabel label_HunterLV;

	public TUILabel label_HunterLV_Exp;

	public TUIMeshSpriteSliced img_HunterLV_Exp;

	public TUILabel label_title;

	public TUILabel label_rating;

	public TUILabel label_likes;

	public Role_Control role_control;

	public Role_Control role_control_namecard;

	public Dictionary<int, string> title_list;

	public PopupFriends popup_friends;

	public PopupInfoCard popup_info_card;

	public PopupRanking popup_ranking;

	private TUICoopPlayerInfo coop_player_info;

	private Vector2 player_texture_size;

	private Vector2 img_texture_size;

	private string default_texture = "juesemoren";

	private void Start()
	{
		player_texture_size = new Vector2(100f, 100f);
		if (img_player != null)
		{
			img_texture_size = img_player.transform.localScale;
		}
	}

	private void Update()
	{
	}

	public void SetInfo(TUICoopPlayerInfo m_coop_player_info)
	{
		if (m_coop_player_info != null)
		{
			if (coop_player_info == null)
			{
				coop_player_info = m_coop_player_info;
			}
			else
			{
				if (m_coop_player_info.change_id)
				{
					coop_player_info.id = m_coop_player_info.id;
				}
				if (m_coop_player_info.change_name)
				{
					coop_player_info.name = m_coop_player_info.name;
				}
				if (m_coop_player_info.change_likes)
				{
					coop_player_info.likes = m_coop_player_info.likes;
				}
				if (m_coop_player_info.change_progress)
				{
					coop_player_info.progress = m_coop_player_info.progress;
				}
				if (m_coop_player_info.change_status)
				{
					coop_player_info.status = m_coop_player_info.status;
				}
				if (m_coop_player_info.change_title_id)
				{
					coop_player_info.title_id = m_coop_player_info.title_id;
				}
				if (m_coop_player_info.change_title_id_list)
				{
					coop_player_info.title_id_list = m_coop_player_info.title_id_list;
				}
				if (m_coop_player_info.change_all_ranking)
				{
					coop_player_info.all_ranking = m_coop_player_info.all_ranking;
				}
				if (m_coop_player_info.change_friends_ranking)
				{
					coop_player_info.friends_ranking = m_coop_player_info.friends_ranking;
				}
				if (m_coop_player_info.change_GC_texture)
				{
					coop_player_info.GC_texture = m_coop_player_info.GC_texture;
				}
				if (m_coop_player_info.change_hunter_level)
				{
					coop_player_info.hunter_level = m_coop_player_info.hunter_level;
				}
				if (m_coop_player_info.change_hunter_exp)
				{
					coop_player_info.hunter_exp = m_coop_player_info.hunter_exp;
				}
				if (m_coop_player_info.change_hunter_update_exp)
				{
					coop_player_info.hunter_update_exp = m_coop_player_info.hunter_update_exp;
				}
				if (m_coop_player_info.change_role_info)
				{
					coop_player_info.role_info = m_coop_player_info.role_info;
				}
			}
		}
		if (coop_player_info == null)
		{
			return;
		}
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
		if (label_HunterLV != null)
		{
			if (coop_player_info.hunter_level == 0)
			{
				label_HunterLV.Text = "--";
			}
			else
			{
				label_HunterLV.Text = coop_player_info.hunter_level.ToString();
			}
		}
		if (label_HunterLV_Exp != null)
		{
			label_HunterLV_Exp.Text = coop_player_info.hunter_exp + "/" + coop_player_info.hunter_update_exp;
		}
		if (img_HunterLV_Exp != null)
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
				img_HunterLV_Exp.Size = new Vector2(198f * num, img_HunterLV_Exp.Size.y);
			}
			else
			{
				img_HunterLV_Exp.Size = new Vector2(0f, img_HunterLV_Exp.Size.y);
			}
		}
		if (label_title != null)
		{
			int title_id = coop_player_info.title_id;
			if (title_list != null && title_list.ContainsKey(title_id))
			{
				label_title.Text = title_list[title_id];
			}
			else
			{
				label_title.Text = "--";
			}
		}
		if (role_control != null)
		{
			TUICoopRoleInfo role_info = coop_player_info.role_info;
			if (role_info != null)
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
				role_control.SetRoleFixedRotation(new Vector3(0f, -40f, 0f));
				role_control.SetRandomWeapon(true, role_info.weapon_list);
				if (label_rating != null)
				{
					label_rating.Text = "{color:FFC217FF}RATING:  {color:FFFFFFFF}" + role_info.role_rating;
				}
				else
				{
					label_rating.Text = "{color:FFC217FF}RATING:    --";
				}
			}
		}
		if (label_likes != null)
		{
			label_likes.Text = coop_player_info.likes.ToString();
		}
	}

	public void SetTitleList(TUICoopTitleListInfo m_info)
	{
		if (m_info != null)
		{
			title_list = m_info.title_list;
		}
	}

	public void SetRoleRotation(float m_wparam, float m_lparam)
	{
		if (popup_info_card.GetInfoCardShow())
		{
			if (role_control_namecard != null)
			{
				role_control_namecard.SetRotation(m_wparam, m_lparam);
			}
		}
		else if (role_control != null)
		{
			role_control.SetRotation(m_wparam, m_lparam);
		}
	}

	public void SetFriendsList(Dictionary<string, TUICoopPlayerInfo> m_friends_list, GameObject m_invoke_go)
	{
		popup_friends.SetInfo(m_friends_list, m_invoke_go, ref title_list);
	}

	public void SetFriendsEmtpyStr(string str)
	{
		popup_friends.SetEmptyStr(str);
	}

	public void AddFriendsList(Dictionary<string, TUICoopPlayerInfo> m_friends_list, GameObject m_invoke_go)
	{
		popup_friends.AddInfo(m_friends_list, m_invoke_go, ref title_list);
	}

	public void SetRankingList(RankingType m_type, Dictionary<string, TUICoopPlayerInfo> m_ranking_list, GameObject m_invoke_go)
	{
		popup_ranking.SetInfo(m_type, m_ranking_list, m_invoke_go, ref title_list);
	}

	public void SetRankingList(RankingType m_type, TUICoopPlayerInfo m_ranking_info, GameObject m_invoke_go)
	{
		popup_ranking.SetInfo(m_type, m_ranking_info, m_invoke_go, ref title_list);
	}

	public void AddRankingList(RankingType m_type, Dictionary<string, TUICoopPlayerInfo> m_ranking_list, GameObject m_invoke_go)
	{
		popup_ranking.AddInfo(m_type, m_ranking_list, m_invoke_go, ref title_list);
	}

	public void AddRankingList(RankingType m_type, TUICoopPlayerInfo m_ranking_info, GameObject m_invoke_go)
	{
		popup_ranking.SetInfo(m_type, m_ranking_info, m_invoke_go, ref title_list);
	}

	public void ShowFriendsList(bool m_open)
	{
		if (popup_friends != null)
		{
			popup_friends.Show(m_open);
		}
	}

	public void ShowInfoCard(bool m_open, bool m_my_self = false, TUICoopPlayerInfo m_player_info = null)
	{
		if (popup_info_card != null)
		{
			popup_info_card.Show(m_open, m_my_self, m_player_info);
		}
	}

	public void SetInfoCard(TUICoopPlayerInfo m_info)
	{
		if (!(popup_info_card != null))
		{
			return;
		}
		bool myself = false;
		if (coop_player_info != null && m_info != null && coop_player_info.id == m_info.id)
		{
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
			myself = true;
		}
		popup_info_card.SetInfo(m_info, myself, title_list);
	}

	public void ShowTitleList(bool m_open)
	{
		if (popup_info_card != null)
		{
			List<int> title_id_list = null;
			int my_title_id = 0;
			if (coop_player_info != null)
			{
				title_id_list = coop_player_info.title_id_list;
				my_title_id = coop_player_info.title_id;
			}
			popup_info_card.ShowTitleList(m_open, title_list, title_id_list, my_title_id);
		}
	}

	public void ShowRankingList(bool m_open)
	{
		if (popup_ranking != null)
		{
			popup_ranking.Show(m_open);
		}
	}

	public void ChangeRanking(bool m_is_all)
	{
		if (popup_ranking != null)
		{
			popup_ranking.ChangeRanking(m_is_all);
		}
	}

	public bool GetRankingShow()
	{
		if (popup_ranking != null)
		{
			return popup_ranking.GetRankingShow();
		}
		return false;
	}

	public bool GetFriendsListShow()
	{
		if (popup_friends != null)
		{
			return popup_friends.GetFriendsListShow();
		}
		return false;
	}

	public bool GetInfoCardShow()
	{
		if (popup_info_card != null)
		{
			return popup_info_card.GetInfoCardShow();
		}
		return false;
	}

	public string GetPlayerID()
	{
		return coop_player_info.id;
	}

	public TUICoopPlayerInfo GetPlayerInfo()
	{
		return coop_player_info;
	}

	public void UpdatePlayerTexture(TUIUpdatePlayerTextureInfo m_info)
	{
		UpdateMyTexture(m_info);
		UpdateFriendsTexture(m_info);
		UpdateInfoCardTexture(m_info);
	}

	private void UpdateMyTexture(TUIUpdatePlayerTextureInfo m_info)
	{
		if (coop_player_info != null && m_info != null && coop_player_info.id == m_info.id)
		{
			coop_player_info.GC_texture = m_info.player_texture;
			Texture player_texture = m_info.player_texture;
			if (img_player != null && player_texture != null && player_texture.width != 0 && player_texture.height != 0)
			{
				img_player.UseCustomize = true;
				img_player.CustomizeTexture = coop_player_info.GC_texture;
				img_player.CustomizeRect = new Rect(0f, 0f, coop_player_info.GC_texture.width, coop_player_info.GC_texture.height);
				float x = player_texture_size.x * img_texture_size.x / (float)coop_player_info.GC_texture.width;
				float y = player_texture_size.y * img_texture_size.y / (float)coop_player_info.GC_texture.height;
				img_player.transform.localScale = new Vector3(x, y, 1f);
			}
		}
	}

	private void UpdateFriendsTexture(TUIUpdatePlayerTextureInfo m_info)
	{
		if (popup_friends != null && m_info != null)
		{
			popup_friends.UpdateFriendsTexture(m_info);
		}
	}

	private void UpdateInfoCardTexture(TUIUpdatePlayerTextureInfo m_info)
	{
		if (popup_info_card != null && m_info != null)
		{
			popup_info_card.UpdateInfoCardTexture(m_info);
		}
	}

	public void UpdateTitle(int m_id)
	{
		if (popup_info_card != null)
		{
			popup_info_card.UpdateTitle(m_id);
		}
		if (label_title != null)
		{
			if (title_list != null && title_list.ContainsKey(m_id))
			{
				label_title.Text = title_list[m_id];
			}
			else
			{
				label_title.Text = "--";
			}
		}
	}

	public bool GetRankingAniStop()
	{
		if (popup_ranking != null && popup_ranking.GetRankingAniStop())
		{
			return true;
		}
		return false;
	}

	public bool GetFriendsAniStop()
	{
		if (popup_friends != null && popup_friends.GetFriendsAniStop())
		{
			return true;
		}
		return false;
	}
}
