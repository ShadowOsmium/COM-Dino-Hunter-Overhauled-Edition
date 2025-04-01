using System.Collections.Generic;
using UnityEngine;

public class PopupFriendsItem : MonoBehaviour
{
	public TUIMeshSprite img_role;

	public TUILabel label_name;

	public TUILabel label_title;

	public TUIButtonClick btn_infocard;

	private TUICoopPlayerInfo coop_player_info;

	private Vector2 player_texture_size;

	private Vector2 img_texture_size;

	private string default_texture = "juesemoren";

	private void Awake()
	{
		player_texture_size = new Vector2(100f, 100f);
		if (img_role != null)
		{
			img_texture_size = img_role.transform.localScale;
		}
	}

	private void Start()
	{
		Transform[] componentsInChildren = base.gameObject.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].gameObject.layer = 9;
			TUIDrawSprite component = componentsInChildren[i].GetComponent<TUIDrawSprite>();
			if (component != null)
			{
				component.clippingType = TUIDrawSprite.Clipping.None;
			}
		}
	}

	private void Update()
	{
	}

	public void SetInfo(TUICoopPlayerInfo m_info, GameObject m_invoke_go, ref Dictionary<int, string> m_title_list)
	{
		if (m_info != null)
		{
			if (coop_player_info == null)
			{
				coop_player_info = m_info;
			}
			else
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
			}
		}
		if (coop_player_info == null)
		{
			return;
		}
		Texture gC_texture = coop_player_info.GC_texture;
		string text = coop_player_info.name;
		string text2 = string.Empty;
		if (m_title_list != null && m_title_list.ContainsKey(coop_player_info.title_id))
		{
			text2 = m_title_list[coop_player_info.title_id];
		}
		if (img_role != null)
		{
			if (gC_texture == null)
			{
				img_role.texture = default_texture;
			}
			else
			{
				img_role.UseCustomize = true;
				img_role.CustomizeTexture = gC_texture;
				img_role.CustomizeRect = new Rect(0f, 0f, gC_texture.width, gC_texture.height);
				float x = player_texture_size.x * img_texture_size.x / (float)gC_texture.width;
				float y = player_texture_size.y * img_texture_size.y / (float)gC_texture.height;
				img_role.transform.localScale = new Vector3(x, y, 1f);
			}
			if (label_name != null)
			{
				label_name.Text = text;
			}
			if (label_title != null)
			{
				label_title.Text = text2;
			}
		}
		if (btn_infocard != null)
		{
			btn_infocard.invokeObject = m_invoke_go;
		}
	}

	public string GetPlayerID()
	{
		if (coop_player_info != null)
		{
			return coop_player_info.id;
		}
		return string.Empty;
	}

	public TUICoopPlayerInfo GetPlayerInfo()
	{
		return coop_player_info;
	}

	public void UpdatePlayerTexture(Texture m_texture)
	{
		if (img_role != null && m_texture != null)
		{
			img_role.UseCustomize = true;
			img_role.CustomizeTexture = m_texture;
			img_role.CustomizeRect = new Rect(0f, 0f, m_texture.width, m_texture.height);
			img_role.transform.localScale = new Vector3(player_texture_size.x / (float)m_texture.width, player_texture_size.y / (float)m_texture.height, 1f);
			img_role.UseCustomize = true;
			img_role.CustomizeTexture = m_texture;
			img_role.CustomizeRect = new Rect(0f, 0f, m_texture.width, m_texture.height);
			float x = player_texture_size.x * img_texture_size.x / (float)m_texture.width;
			float y = player_texture_size.y * img_texture_size.y / (float)m_texture.height;
			img_role.transform.localScale = new Vector3(x, y, 1f);
		}
	}
}
