using UnityEngine;

public class PopupLevel_Item : MonoBehaviour
{
	public TUIButtonSelect btn_select;

	public Transform drop_sign;

	public TUIMeshSprite img_bg;

	public TUIMeshSprite img_sign;

	private TUISecondaryLevelInfo secondary_level_info;

	private LevelPassState item_state;

	private string texture_liang = "renwuxuankuang";

	private string texture_hui = "renwuxuankuanghui";

	private string texture_killing = "furenwu_5";

	private string texture_defended = "furenwu_3";

	private string texture_steal = "furenwu_4";

	private string texture_survival = "furenwu_2";

	private string texture_boss = "furenwu_1";

	private string texture_unlock = "furenwu_6";

	private void Awake()
	{
		ShowDropSign(false);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void SetInfo(TUISecondaryLevelInfo m_info, LevelPassState m_state)
	{
		secondary_level_info = m_info;
		if (m_info == null)
		{
			return;
		}
		if (img_sign != null)
		{
			switch (m_info.level_type)
			{
			case SecondaryLevelType.Killing:
				img_sign.texture = texture_killing;
				break;
			case SecondaryLevelType.Defended:
				img_sign.texture = texture_defended;
				break;
			case SecondaryLevelType.Steal:
				img_sign.texture = texture_steal;
				break;
			case SecondaryLevelType.Survival:
				img_sign.texture = texture_survival;
				break;
			case SecondaryLevelType.BOSS:
				img_sign.texture = texture_boss;
				break;
			}
			if (m_state == LevelPassState.Disable)
			{
				img_sign.texture = texture_unlock;
			}
		}
		SetState(m_state);
	}

	public int GetID()
	{
		if (secondary_level_info == null)
		{
			Debug.Log("no id!");
			return 0;
		}
		return secondary_level_info.id;
	}

	public void SetState(LevelPassState m_state)
	{
		item_state = m_state;
		switch (m_state)
		{
		case LevelPassState.Disable:
			if (img_bg != null)
			{
				img_bg.texture = texture_hui;
			}
			break;
		case LevelPassState.Normal:
			if (img_bg != null)
			{
				img_bg.texture = texture_hui;
			}
			break;
		case LevelPassState.Pass:
			if (img_bg != null)
			{
				img_bg.texture = texture_liang;
			}
			break;
		}
	}

	public TUIButtonSelect GetBtnSelect()
	{
		return btn_select;
	}

	public LevelPassState GetState()
	{
		return item_state;
	}

	public void ShowDropSign(bool m_show)
	{
		if (drop_sign != null)
		{
			drop_sign.gameObject.SetActiveRecursively(m_show);
		}
	}

	public TUISecondaryLevelInfo GetInfo()
	{
		return secondary_level_info;
	}
}
