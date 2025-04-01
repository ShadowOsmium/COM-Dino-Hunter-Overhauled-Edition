using System.Collections.Generic;
using UnityEngine;

public class PopupRanking : MonoBehaviour
{
	public enum RankingState
	{
		All,
		Friends
	}

	public GameObject go_popup;

	public PopupRankingItem prefab_ranking_item;

	public TUIScrollList scrolllist_items_all;

	public TUIScrollList scrolllist_items_friends;

	public PopupRankingItem my_ranking;

	public TUIMeshSprite img_scroll2d_bg;

	public TUIButtonClick btn_all;

	public TUIButtonClick btn_friend;

	public GameObject go_all_bg;

	public GameObject go_friends_bg;

	public TUILabel label_friends_empty;

	private Dictionary<string, TUICoopPlayerInfo> all_ranking_info_list;

	private Dictionary<string, TUICoopPlayerInfo> friends_ranking_info_list;

	private Dictionary<string, PopupRankingItem> all_ranking_item_list;

	private Dictionary<string, PopupRankingItem> friends_ranking_item_list;

	private TUICoopPlayerInfo my_ranking_info;

	private RankingState ranking_state = RankingState.Friends;

	private bool is_open;

	private void Awake()
	{
		all_ranking_info_list = new Dictionary<string, TUICoopPlayerInfo>();
		friends_ranking_info_list = new Dictionary<string, TUICoopPlayerInfo>();
		all_ranking_item_list = new Dictionary<string, PopupRankingItem>();
		friends_ranking_item_list = new Dictionary<string, PopupRankingItem>();
		if (label_friends_empty != null)
		{
			label_friends_empty.gameObject.SetActiveRecursively(false);
		}
	}

	private void Start()
	{
		if (img_scroll2d_bg != null)
		{
			img_scroll2d_bg.gameObject.layer = 9;
		}
	}

	private void Update()
	{
	}

	public void SetInfo(RankingType m_type, Dictionary<string, TUICoopPlayerInfo> m_coop_player_info_list, GameObject m_invoke_go, ref Dictionary<int, string> m_title_list)
	{
		if ((ranking_state == RankingState.All && m_type != 0 && m_type != RankingType.All_Mine) || (ranking_state == RankingState.Friends && m_type != RankingType.Friends_All && m_type != RankingType.Friends_Mine))
		{
			return;
		}
		if (m_type == RankingType.Friends_All)
		{
			if (m_coop_player_info_list == null || m_coop_player_info_list.Count == 0)
			{
				if (label_friends_empty != null)
				{
					label_friends_empty.gameObject.SetActiveRecursively(true);
				}
			}
			else if (label_friends_empty != null)
			{
				label_friends_empty.gameObject.SetActiveRecursively(false);
			}
		}
		else if (label_friends_empty != null)
		{
			label_friends_empty.gameObject.SetActiveRecursively(false);
		}
		if (m_coop_player_info_list == null || scrolllist_items_all == null || scrolllist_items_friends == null || prefab_ranking_item == null)
		{
			Debug.Log("error!");
			return;
		}
		Dictionary<string, TUICoopPlayerInfo> dictionary = null;
		Dictionary<string, PopupRankingItem> dictionary2 = null;
		TUIScrollList tUIScrollList = null;
		switch (m_type)
		{
		case RankingType.All_All:
			dictionary = all_ranking_info_list;
			dictionary2 = all_ranking_item_list;
			tUIScrollList = scrolllist_items_all;
			break;
		case RankingType.Friends_All:
			dictionary = friends_ranking_info_list;
			dictionary2 = friends_ranking_item_list;
			tUIScrollList = scrolllist_items_friends;
			break;
		}
		tUIScrollList.Clear(true);
		dictionary2.Clear();
		dictionary.Clear();
		Debug.Log("Clear All.");
		int num = 0;
		foreach (KeyValuePair<string, TUICoopPlayerInfo> item in m_coop_player_info_list)
		{
			TUICoopPlayerInfo value = item.Value;
			if (value != null)
			{
				bool flag = true;
				flag = ((num % 2 == 0) ? true : false);
				PopupRankingItem popupRankingItem = (PopupRankingItem)Object.Instantiate(prefab_ranking_item);
				popupRankingItem.transform.parent = tUIScrollList.transform;
				popupRankingItem.transform.localPosition = new Vector3(0f, 0f, 0f);
				popupRankingItem.SetInfo(m_type, value, m_invoke_go, ref m_title_list, flag ? PopupRankingItem.ShowBGType.Show : PopupRankingItem.ShowBGType.UnShow);
				if (dictionary != null)
				{
					dictionary[value.id] = popupRankingItem.GetPlayerInfo();
				}
				if (dictionary2 != null)
				{
					dictionary2[value.id] = popupRankingItem;
				}
				if (tUIScrollList != null)
				{
					tUIScrollList.AddWhenMove(popupRankingItem.GetComponent<TUIControl>());
				}
				num++;
			}
		}
	}

	public void SetInfo(RankingType m_type, TUICoopPlayerInfo m_coop_player_info, GameObject m_invoke_go, ref Dictionary<int, string> m_title_list)
	{
		if ((ranking_state != 0 || m_type == RankingType.All_All || m_type == RankingType.All_Mine) && (ranking_state != RankingState.Friends || m_type == RankingType.Friends_All || m_type == RankingType.Friends_Mine) && m_coop_player_info != null && my_ranking != null)
		{
			my_ranking.SetInfo(m_type, m_coop_player_info, m_invoke_go, ref m_title_list);
			my_ranking_info = my_ranking.GetPlayerInfo();
		}
	}

	public void AddInfo(RankingType m_type, Dictionary<string, TUICoopPlayerInfo> m_coop_player_info_list, GameObject m_invoke_go, ref Dictionary<int, string> m_title_list)
	{
		if (m_coop_player_info_list == null || scrolllist_items_all == null || scrolllist_items_friends == null || prefab_ranking_item == null)
		{
			Debug.Log("error!");
			return;
		}
		Dictionary<string, TUICoopPlayerInfo> dictionary = null;
		Dictionary<string, PopupRankingItem> dictionary2 = null;
		TUIScrollList tUIScrollList = null;
		switch (m_type)
		{
		case RankingType.All_All:
			dictionary = all_ranking_info_list;
			dictionary2 = all_ranking_item_list;
			tUIScrollList = scrolllist_items_all;
			if (label_friends_empty != null)
			{
				label_friends_empty.gameObject.SetActiveRecursively(false);
			}
			break;
		case RankingType.Friends_All:
			dictionary = friends_ranking_info_list;
			dictionary2 = friends_ranking_item_list;
			tUIScrollList = scrolllist_items_friends;
			if (dictionary2 != null && dictionary2.Count > 0)
			{
				if (label_friends_empty != null)
				{
					label_friends_empty.gameObject.SetActiveRecursively(false);
				}
			}
			else if (m_coop_player_info_list != null && m_coop_player_info_list.Count > 0)
			{
				if (label_friends_empty != null)
				{
					label_friends_empty.gameObject.SetActiveRecursively(false);
				}
			}
			else if (label_friends_empty != null)
			{
				label_friends_empty.gameObject.SetActiveRecursively(true);
			}
			break;
		}
		bool flag = true;
		int num = 0;
		if (dictionary2 != null)
		{
			if (dictionary2.Count % 2 == 0)
			{
				flag = true;
			}
			else
			{
				flag = false;
				num = 1;
			}
		}
		foreach (KeyValuePair<string, TUICoopPlayerInfo> item in m_coop_player_info_list)
		{
			TUICoopPlayerInfo value = item.Value;
			if (value == null)
			{
				continue;
			}
			if (dictionary2.ContainsKey(value.id))
			{
				PopupRankingItem popupRankingItem = dictionary2[value.id];
				popupRankingItem.SetInfo(m_type, value, m_invoke_go, ref m_title_list);
				dictionary[value.id] = popupRankingItem.GetPlayerInfo();
				Debug.Log("Replace:" + value.id);
			}
			else if (tUIScrollList != null)
			{
				if (num == 2)
				{
					num = 0;
					flag = true;
				}
				PopupRankingItem popupRankingItem2 = (PopupRankingItem)Object.Instantiate(prefab_ranking_item);
				popupRankingItem2.transform.parent = tUIScrollList.transform;
				popupRankingItem2.transform.localPosition = new Vector3(0f, 0f, popupRankingItem2.transform.localPosition.z);
				popupRankingItem2.SetInfo(m_type, value, m_invoke_go, ref m_title_list, flag ? PopupRankingItem.ShowBGType.Show : PopupRankingItem.ShowBGType.UnShow);
				if (dictionary != null)
				{
					dictionary[value.id] = popupRankingItem2.GetPlayerInfo();
				}
				if (dictionary2 != null)
				{
					dictionary2[value.id] = popupRankingItem2;
				}
				if (tUIScrollList != null)
				{
					tUIScrollList.AddWhenMove(popupRankingItem2.GetComponent<TUIControl>());
				}
				if (flag)
				{
					flag = false;
				}
				num++;
			}
		}
	}

	public void Show(bool m_open)
	{
		is_open = m_open;
		if (m_open)
		{
			base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
			if (go_popup != null && go_popup.GetComponent<Animation>() != null)
			{
				go_popup.GetComponent<Animation>().Play();
			}
		}
		else
		{
			base.transform.localPosition = new Vector3(-1000f, 0f, base.transform.localPosition.z);
		}
	}

	public void ChangeRanking(bool m_is_all)
	{
		if (m_is_all)
		{
			Debug.Log("Change to all.");
			ranking_state = RankingState.All;
			go_all_bg.SetActiveRecursively(true);
			go_friends_bg.SetActiveRecursively(false);
			if (scrolllist_items_all != null)
			{
				scrolllist_items_all.transform.localPosition = new Vector3(0f, -5f, scrolllist_items_all.transform.localPosition.z);
			}
			if (scrolllist_items_friends != null)
			{
				scrolllist_items_friends.transform.localPosition = new Vector3(-1000f, -5f, scrolllist_items_friends.transform.localPosition.z);
			}
			if (label_friends_empty != null)
			{
				label_friends_empty.gameObject.SetActiveRecursively(false);
			}
		}
		else
		{
			Debug.Log("Change to friends.");
			ranking_state = RankingState.Friends;
			go_all_bg.SetActiveRecursively(false);
			go_friends_bg.SetActiveRecursively(true);
			if (scrolllist_items_all != null)
			{
				scrolllist_items_all.transform.localPosition = new Vector3(-1000f, -5f, scrolllist_items_all.transform.localPosition.z);
			}
			if (scrolllist_items_friends != null)
			{
				scrolllist_items_friends.transform.localPosition = new Vector3(0f, -5f, scrolllist_items_friends.transform.localPosition.z);
			}
			if (friends_ranking_item_list != null && friends_ranking_item_list.Count > 0 && label_friends_empty != null)
			{
				label_friends_empty.gameObject.SetActiveRecursively(false);
			}
		}
		if (my_ranking != null)
		{
			my_ranking.ChangeRanking(m_is_all);
		}
	}

	public bool GetRankingShow()
	{
		return is_open;
	}

	public bool GetRankingAniStop()
	{
		if (go_popup != null && go_popup.GetComponent<Animation>() != null && !go_popup.GetComponent<Animation>().isPlaying)
		{
			return true;
		}
		return false;
	}
}
