using System.Collections.Generic;
using UnityEngine;

public class PopupFriends : MonoBehaviour
{
	public GameObject go_popup;

	public TUIScrollList scrollist_items;

	public TUIControl prefab_group;

	public PopupFriendsItem prefab_item;

	public TUILabel label_friends_empty;

	private Dictionary<string, TUICoopPlayerInfo> friends_info_list;

	private Dictionary<string, PopupFriendsItem> friends_item_list;

	private bool is_open;

	private void Awake()
	{
		friends_info_list = new Dictionary<string, TUICoopPlayerInfo>();
		friends_item_list = new Dictionary<string, PopupFriendsItem>();
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetEmptyStr(string str)
	{
		if (!(label_friends_empty == null))
		{
			label_friends_empty.Text = str;
		}
	}

	public void SetInfo(Dictionary<string, TUICoopPlayerInfo> m_coop_player_info_list, GameObject m_invoke_go, ref Dictionary<int, string> m_title_list)
	{
		if (friends_item_list != null && friends_item_list.Count > 0)
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
		if (m_coop_player_info_list == null || friends_info_list == null || scrollist_items == null || prefab_group == null || prefab_item == null)
		{
			Debug.Log("error!");
			return;
		}
		scrollist_items.Clear(true);
		TUIControl tUIControl = null;
		int num = 0;
		foreach (KeyValuePair<string, TUICoopPlayerInfo> item in m_coop_player_info_list)
		{
			TUICoopPlayerInfo value = item.Value;
			if (value != null)
			{
				bool flag = ((num % 2 == 0) ? true : false);
				if (flag)
				{
					tUIControl = (TUIControl)Object.Instantiate(prefab_group);
					tUIControl.transform.parent = scrollist_items.transform;
					tUIControl.transform.localPosition = new Vector3(0f, 0f, 0f);
				}
				PopupFriendsItem popupFriendsItem = (PopupFriendsItem)Object.Instantiate(prefab_item);
				popupFriendsItem.transform.parent = tUIControl.transform;
				popupFriendsItem.transform.localPosition = new Vector3((!flag) ? 88 : (-102), 0f, -5f);
				popupFriendsItem.SetInfo(value, m_invoke_go, ref m_title_list);
				friends_info_list[value.id] = popupFriendsItem.GetPlayerInfo();
				if (scrollist_items != null && flag)
				{
					scrollist_items.Add(tUIControl.GetComponent<TUIControl>());
				}
				if (friends_item_list != null)
				{
					friends_item_list[value.id] = popupFriendsItem;
				}
			}
			num++;
		}
	}

	public void AddInfo(Dictionary<string, TUICoopPlayerInfo> m_coop_player_info_list, GameObject m_invoke_go, ref Dictionary<int, string> m_title_list)
	{
		if (m_coop_player_info_list == null || scrollist_items == null || prefab_group == null || prefab_item == null)
		{
			Debug.Log("error!");
			return;
		}
		if (friends_item_list != null && friends_item_list.Count > 0)
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
		bool flag = ((friends_item_list.Count % 2 == 0) ? true : false);
		TUIControl tUIControl = null;
		int num = 0;
		if (!flag)
		{
			tUIControl = scrollist_items.GetLastControlONListObjs();
			num = 1;
		}
		int num2 = 0;
		foreach (KeyValuePair<string, TUICoopPlayerInfo> item in m_coop_player_info_list)
		{
			TUICoopPlayerInfo value = item.Value;
			if (value != null)
			{
				if (friends_item_list.ContainsKey(value.id))
				{
					PopupFriendsItem popupFriendsItem = friends_item_list[value.id];
					popupFriendsItem.SetInfo(value, m_invoke_go, ref m_title_list);
					friends_info_list[value.id] = popupFriendsItem.GetPlayerInfo();
				}
				else
				{
					if (num == 2)
					{
						num = 0;
						flag = true;
					}
					if (flag)
					{
						tUIControl = (TUIControl)Object.Instantiate(prefab_group);
						tUIControl.transform.parent = scrollist_items.transform;
						tUIControl.transform.localPosition = new Vector3(0f, 0f, 0f);
					}
					if (tUIControl != null)
					{
						PopupFriendsItem popupFriendsItem2 = (PopupFriendsItem)Object.Instantiate(prefab_item);
						popupFriendsItem2.transform.parent = tUIControl.transform;
						popupFriendsItem2.transform.localPosition = new Vector3((!flag) ? 88 : (-102), 0f, -5f);
						popupFriendsItem2.SetInfo(value, m_invoke_go, ref m_title_list);
						if (flag)
						{
							flag = false;
							scrollist_items.AddWhenMove(tUIControl.GetComponent<TUIControl>());
						}
						friends_item_list[value.id] = popupFriendsItem2;
						friends_info_list[value.id] = popupFriendsItem2.GetPlayerInfo();
						num++;
					}
				}
			}
			num2++;
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

	public bool GetFriendsListShow()
	{
		return is_open;
	}

	public void UpdateFriendsTexture(TUIUpdatePlayerTextureInfo m_info)
	{
		if (friends_item_list != null && m_info != null && friends_item_list.ContainsKey(m_info.id))
		{
			PopupFriendsItem popupFriendsItem = friends_item_list[m_info.id];
			if (popupFriendsItem != null)
			{
				popupFriendsItem.UpdatePlayerTexture(m_info.player_texture);
			}
		}
	}

	public bool GetFriendsAniStop()
	{
		if (go_popup != null && go_popup.GetComponent<Animation>() != null && !go_popup.GetComponent<Animation>().isPlaying)
		{
			return true;
		}
		return false;
	}
}
