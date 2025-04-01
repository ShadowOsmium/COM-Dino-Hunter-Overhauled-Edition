using System.Collections.Generic;
using UnityEngine;

public class PopupLevel : MonoBehaviour
{
	public GameObject go_popup;

	public PopupLevel_Frame01 popuplevel_frame01;

	public PopupLevel_Frame03 popuplevel_frame03;

	public TUILabel label_title;

	public TUIMeshSprite img_title_bg;

	public TUIButtonClick btn_start;

	public PopupTips popup_tips;

	public List<PopupLevel_Item> popup_level_item_list;

	private TUIMainLevelInfo level_info;

	private PopupLevel_Item level_item_now;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetBtnStartEnable(bool m_enable)
	{
		if (!(btn_start == null))
		{
			if (m_enable)
			{
				btn_start.Disable(false);
			}
			else
			{
				btn_start.Disable(true);
			}
		}
	}

	public void SetInfo(TUIMainLevelInfo m_info)
	{
		level_info = m_info;
		if (level_info == null)
		{
			Debug.Log("empty!");
			return;
		}
		if (popup_level_item_list == null)
		{
			Debug.Log("no popup_level_item!");
			return;
		}
		int id = m_info.id;
		string title = m_info.title;
		MainLevelType level_type = m_info.level_type;
		List<TUISecondaryLevelInfo> secondary_level_info = m_info.secondary_level_info;
		int secondary_level_id = m_info.secondary_level_id;
		int[] level_goods_drop_list = m_info.level_goods_drop_list;
		int count = popup_level_item_list.Count;
		if (label_title != null)
		{
			label_title.Text = title;
		}
		if (img_title_bg != null)
		{
			string mapTexture = TUIMappingInfo.Instance().GetMapTexture((int)level_type);
			img_title_bg.texture = mapTexture;
		}
		if (secondary_level_info == null || secondary_level_info.Count != count)
		{
			Debug.Log("level count no match!");
			return;
		}
		int choose = -1;
		for (int i = 0; i < count; i++)
		{
			PopupLevel_Item popupLevel_Item = popup_level_item_list[i];
			if (!(popupLevel_Item != null))
			{
				continue;
			}
			TUISecondaryLevelInfo tUISecondaryLevelInfo = secondary_level_info[i];
			if (tUISecondaryLevelInfo == null)
			{
				continue;
			}
			int id2 = tUISecondaryLevelInfo.id;
			LevelPassState pass_state = tUISecondaryLevelInfo.pass_state;
			popupLevel_Item.SetInfo(tUISecondaryLevelInfo, pass_state);
			if (id2 == secondary_level_id)
			{
				choose = i;
			}
			if (level_goods_drop_list == null)
			{
				continue;
			}
			for (int j = 0; j < level_goods_drop_list.Length; j++)
			{
				if (id2 == level_goods_drop_list[j])
				{
					popupLevel_Item.ShowDropSign(true);
				}
			}
		}
		SetChoose(choose);
	}

	public void Show()
	{
		base.transform.localPosition = new Vector3(0f, 0f, base.transform.localPosition.z);
		if (go_popup != null && go_popup.GetComponent<Animation>() != null)
		{
			go_popup.GetComponent<Animation>().Play();
		}
	}

	public void Hide()
	{
		base.transform.localPosition = new Vector3(0f, -1000f, base.transform.localPosition.z);
		if (popup_level_item_list == null)
		{
			return;
		}
		for (int i = 0; i < popup_level_item_list.Count; i++)
		{
			PopupLevel_Item popupLevel_Item = popup_level_item_list[i];
			if (popupLevel_Item != null)
			{
				popupLevel_Item.ShowDropSign(false);
				TUIButtonSelect btnSelect = popupLevel_Item.GetBtnSelect();
				if (btnSelect != null)
				{
					btnSelect.Reset();
				}
			}
		}
	}

	public void ShowTips(TUIControl m_control)
	{
		if (popup_tips == null || m_control == null)
		{
			Debug.Log("error!");
			return;
		}
		GoodsNeedItemImg component = m_control.GetComponent<GoodsNeedItemImg>();
		if (component != null)
		{
			string goodsName = component.GetGoodsName();
			popup_tips.SetInfo(goodsName, m_control.transform.position, PopupTips.TipsPivot.TopRight);
		}
	}

	public void HideTips()
	{
		if (popup_tips == null)
		{
			Debug.Log("error!");
		}
		else
		{
			popup_tips.Hide();
		}
	}

	public void SetChoose(int m_index)
	{
		if (m_index < 0)
		{
			if (popuplevel_frame01 != null)
			{
				popuplevel_frame01.SetInfo(string.Empty);
			}
			if (popuplevel_frame03 != null)
			{
				popuplevel_frame03.SetGoodsInfo(null);
			}
			SetBtnStartEnable(false);
		}
		else
		{
			if (popup_level_item_list == null)
			{
				return;
			}
			level_item_now = popup_level_item_list[m_index];
			if (level_item_now == null)
			{
				if (popuplevel_frame01 != null)
				{
					popuplevel_frame01.SetInfo(string.Empty);
				}
				if (popuplevel_frame03 != null)
				{
					popuplevel_frame03.SetGoodsInfo(null);
				}
				SetBtnStartEnable(false);
				return;
			}
			TUISecondaryLevelInfo info = level_item_now.GetInfo();
			if (info == null)
			{
				return;
			}
			int id = info.id;
			string introduce = info.introduce01;
			List<TUIGoodsInfo> goods_drop_list = info.goods_drop_list;
			LevelPassState state = level_item_now.GetState();
			Debug.Log("Choose SecondaryLevel:" + id);
			if (state == LevelPassState.Disable)
			{
				if (popuplevel_frame01 != null)
				{
					popuplevel_frame01.SetInfo(string.Empty);
				}
				if (popuplevel_frame03 != null)
				{
					popuplevel_frame03.SetGoodsInfo(null);
				}
				SetBtnStartEnable(false);
			}
			else
			{
				if (popuplevel_frame01 != null)
				{
					popuplevel_frame01.SetInfo(introduce);
				}
				if (popuplevel_frame03 != null)
				{
					popuplevel_frame03.SetGoodsInfo(goods_drop_list);
				}
				SetBtnStartEnable(true);
			}
			TUIButtonSelect btnSelect = level_item_now.GetBtnSelect();
			if (btnSelect != null)
			{
				btnSelect.SetSelected(true);
			}
		}
	}

	public void SetChoose(PopupLevel_Item m_control)
	{
		if (popup_level_item_list == null)
		{
			return;
		}
		level_item_now = m_control;
		if (level_item_now == null)
		{
			if (popuplevel_frame01 != null)
			{
				popuplevel_frame01.SetInfo(string.Empty);
			}
			if (popuplevel_frame03 != null)
			{
				popuplevel_frame03.SetGoodsInfo(null);
			}
			SetBtnStartEnable(false);
			return;
		}
		TUIButtonSelect btnSelect = level_item_now.GetBtnSelect();
		TUISecondaryLevelInfo info = level_item_now.GetInfo();
		LevelPassState state = level_item_now.GetState();
		if (btnSelect != null)
		{
			btnSelect.SetSelected(true);
		}
		if (info == null)
		{
			return;
		}
		Debug.Log("Choose SecondaryLevel:" + info.id);
		if (state == LevelPassState.Disable)
		{
			if (popuplevel_frame01 != null)
			{
				popuplevel_frame01.SetInfo(string.Empty);
			}
			if (popuplevel_frame03 != null)
			{
				popuplevel_frame03.SetGoodsInfo(null);
			}
			SetBtnStartEnable(false);
			return;
		}
		int id = info.id;
		string introduce = info.introduce01;
		List<TUIGoodsInfo> goods_drop_list = info.goods_drop_list;
		if (popuplevel_frame01 != null)
		{
			popuplevel_frame01.SetInfo(introduce);
		}
		if (popuplevel_frame03 != null)
		{
			popuplevel_frame03.SetGoodsInfo(goods_drop_list);
		}
		SetBtnStartEnable(true);
	}

	public PopupLevel_Item GetChoose()
	{
		return level_item_now;
	}
}
