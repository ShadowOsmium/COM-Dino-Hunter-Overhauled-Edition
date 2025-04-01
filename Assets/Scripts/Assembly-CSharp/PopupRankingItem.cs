using System.Collections.Generic;
using UnityEngine;

public class PopupRankingItem : MonoBehaviour
{
	public enum ShowBGType
	{
		Keep,
		Show,
		UnShow
	}

	public TUIMeshSprite img_ranking;

	public TUILabel label_ranking;

	public TUILabel label_name;

	public TUILabel label_hunter_lv;

	public TUILabel label_rating;

	public TUILabel label_likes;

	public TUIButtonClick btn_infocard;

	public TUIMeshSprite img_bg;

	private string first_texture = "first";

	private string second_texture = "second";

	private string third_texture = "third";

	private TUICoopPlayerInfo coop_player_info;

	public bool is_myself;

	private void Start()
	{
		if (is_myself)
		{
			return;
		}
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

	public void SetInfo(RankingType m_type, TUICoopPlayerInfo m_info, GameObject m_invoke_go, ref Dictionary<int, string> m_title_list, ShowBGType m_show_bg = ShowBGType.Keep, bool m_show_infocard = true)
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
		int hunter_level = coop_player_info.hunter_level;
		int likes = coop_player_info.likes;
		string text = coop_player_info.name;
		string empty = string.Empty;
		if (m_title_list != null && m_title_list.ContainsKey(coop_player_info.title_id))
		{
			empty = m_title_list[coop_player_info.title_id];
		}
		int num = 0;
		switch (m_type)
		{
		case RankingType.All_All:
		case RankingType.All_Mine:
			num = coop_player_info.all_ranking;
			break;
		case RankingType.Friends_All:
		case RankingType.Friends_Mine:
			num = coop_player_info.friends_ranking;
			break;
		}
		if (num >= 1 && num <= 3)
		{
			if (img_ranking != null)
			{
				img_ranking.gameObject.SetActiveRecursively(true);
				switch (num)
				{
				case 1:
					img_ranking.texture = first_texture;
					break;
				case 2:
					img_ranking.texture = second_texture;
					break;
				case 3:
					img_ranking.texture = third_texture;
					break;
				}
			}
			if (label_ranking != null)
			{
				label_ranking.gameObject.SetActiveRecursively(false);
			}
		}
		else if (num > 3 && num <= 999)
		{
			if (img_ranking != null)
			{
				img_ranking.gameObject.SetActiveRecursively(false);
			}
			if (label_ranking != null)
			{
				label_ranking.gameObject.SetActiveRecursively(true);
				label_ranking.Text = num.ToString();
			}
		}
		else
		{
			if (img_ranking != null)
			{
				img_ranking.gameObject.SetActiveRecursively(false);
			}
			if (label_ranking != null)
			{
				label_ranking.gameObject.SetActiveRecursively(true);
				label_ranking.Text = "--";
			}
		}
		if (label_name != null)
		{
			label_name.Text = text;
		}
		if (label_hunter_lv != null)
		{
			label_hunter_lv.Text = hunter_level.ToString();
		}
		if (label_rating != null)
		{
			if (coop_player_info.role_info != null)
			{
				int role_rating = coop_player_info.role_info.role_rating;
				label_rating.Text = role_rating.ToString();
			}
			else
			{
				label_rating.Text = "--";
			}
		}
		if (label_likes != null)
		{
			label_likes.Text = likes.ToString();
		}
		if (btn_infocard != null)
		{
			btn_infocard.invokeObject = m_invoke_go;
			btn_infocard.gameObject.SetActiveRecursively(m_show_infocard);
			if (m_show_infocard)
			{
				btn_infocard.Reset();
			}
		}
		if (img_bg != null)
		{
			switch (m_show_bg)
			{
			case ShowBGType.Show:
				img_bg.gameObject.SetActiveRecursively(true);
				break;
			case ShowBGType.UnShow:
				img_bg.gameObject.SetActiveRecursively(false);
				break;
			}
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

	public void ChangeRanking(bool m_is_all)
	{
		if (coop_player_info == null)
		{
			return;
		}
		int num = 0;
		num = ((!m_is_all) ? coop_player_info.friends_ranking : coop_player_info.all_ranking);
		Debug.Log("Ranking:" + num);
		switch (num)
		{
		case 0:
			if (img_ranking != null)
			{
				img_ranking.gameObject.SetActiveRecursively(false);
			}
			if (label_ranking != null)
			{
				label_ranking.gameObject.SetActiveRecursively(true);
				label_ranking.Text = "--";
			}
			return;
		case 1:
		case 2:
		case 3:
			if (img_ranking != null)
			{
				img_ranking.gameObject.SetActiveRecursively(true);
				switch (num)
				{
				case 1:
					img_ranking.texture = first_texture;
					break;
				case 2:
					img_ranking.texture = second_texture;
					break;
				case 3:
					img_ranking.texture = third_texture;
					break;
				}
			}
			if (label_ranking != null)
			{
				label_ranking.gameObject.SetActiveRecursively(false);
			}
			return;
		}
		if (num > 3 && num < 1000)
		{
			if (img_ranking != null)
			{
				img_ranking.gameObject.SetActiveRecursively(false);
			}
			if (label_ranking != null)
			{
				label_ranking.gameObject.SetActiveRecursively(true);
				label_ranking.Text = num.ToString();
			}
		}
		else
		{
			if (img_ranking != null)
			{
				img_ranking.gameObject.SetActiveRecursively(false);
			}
			if (label_ranking != null)
			{
				label_ranking.gameObject.SetActiveRecursively(true);
				label_ranking.Text = "999+";
			}
		}
	}
}
