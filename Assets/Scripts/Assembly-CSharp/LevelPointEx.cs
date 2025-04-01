using System;
using UnityEngine;

public class LevelPointEx : MonoBehaviour
{
	public enum LevelPointExState
	{
		Passed,
		Open,
		Disable,
		Hide
	}

	public enum LevelPointExType
	{
		None,
		Killing,
		Defended,
		Steal,
		Survival
	}

	public int level_id;

	public TUIMeshSprite img_bottom;

	public TUIMeshSprite img_way;

	public TUIButtonClick btn_level;

	public bool open_btn_animation;

	private LevelPointExState level_point_state = LevelPointExState.Hide;

	private TUISecondaryLevelInfo level_info;

	private bool open_time_gap;

	private float time_gap;

	private float time_total;

	public LevelPointExType level_point_ex_type;

	public TUIMeshSprite img_icon_normal;

	public TUIMeshSprite img_icon_press;

	public TUIMeshSprite img_icon_disable;

	public TUIMeshSprite img_lock;

	private string texture_killing01 = "furenwu_1";

	private string texture_survival01 = "furenwu_2";

	private string texture_defended01 = "furenwu_3";

	private string texture_steal01 = "furenwu_4";

	private string texture_unlock01 = "furenwu_6";

	private string texture_unchoose = "furenwudian";

	private string texture_choose = "sucaisousuodian";

	private bool open_bottom_effect;

	private float effect_time;

	private Vector3 bottom_normal_scale = Vector3.one;

	private void Awake()
	{
		if (img_bottom != null)
		{
			bottom_normal_scale = img_bottom.transform.localScale;
		}
		if (open_btn_animation && btn_level != null && btn_level.GetComponent<Animation>() != null)
		{
			open_btn_animation = false;
			btn_level.GetComponent<Animation>().wrapMode = WrapMode.Loop;
			btn_level.GetComponent<Animation>().Play();
		}
		SetDefaultIcon();
	}

	private void Start()
	{
	}

	private void Update()
	{
		UpdateShowWayAffterTime(Time.deltaTime);
		UpdateBottomEffect(Time.deltaTime);
	}

	public void SetLevelPointState(LevelPointExState m_level_point_state)
	{
		if (btn_level == null || img_bottom == null)
		{
			Debug.Log("error! btn_level or img_bottom no found!");
			return;
		}
		level_point_state = m_level_point_state;
		if (level_point_state == LevelPointExState.Passed)
		{
			btn_level.gameObject.SetActiveRecursively(true);
			btn_level.Reset();
			img_bottom.gameObject.SetActiveRecursively(true);
			if (img_lock != null)
			{
				img_lock.gameObject.SetActiveRecursively(false);
			}
			SetPassIcon();
		}
		else if (level_point_state == LevelPointExState.Open)
		{
			btn_level.gameObject.SetActiveRecursively(true);
			btn_level.Reset();
			img_bottom.gameObject.SetActiveRecursively(true);
			if (img_lock != null)
			{
				img_lock.gameObject.SetActiveRecursively(false);
			}
		}
		else if (level_point_state == LevelPointExState.Disable)
		{
			btn_level.Disable(true);
			btn_level.gameObject.SetActiveRecursively(false);
			img_bottom.gameObject.SetActiveRecursively(true);
			if (img_lock != null)
			{
				img_lock.gameObject.SetActiveRecursively(true);
			}
		}
		else if (level_point_state == LevelPointExState.Hide)
		{
			btn_level.Disable(true);
			btn_level.gameObject.SetActiveRecursively(false);
			img_bottom.gameObject.SetActiveRecursively(false);
			if (img_lock != null)
			{
				img_lock.gameObject.SetActiveRecursively(false);
			}
		}
	}

	public void ShowWay()
	{
		if (img_way == null)
		{
			Debug.Log("error! no found way");
		}
		else
		{
			img_way.gameObject.SetActiveRecursively(true);
		}
	}

	public void ShowWayAffterTime(float m_time_gap)
	{
		time_gap = m_time_gap;
		open_time_gap = true;
	}

	public void UpdateShowWayAffterTime(float delta_time)
	{
		if (open_time_gap)
		{
			time_total += delta_time;
			if (time_total > time_gap)
			{
				open_time_gap = false;
				time_gap = 0f;
				time_total = 0f;
				ShowWay();
				SetLevelPointState(LevelPointExState.Open);
			}
		}
	}

	public void HideWay()
	{
		if (img_way == null)
		{
			Debug.Log("error! no found way");
		}
		else
		{
			img_way.gameObject.SetActiveRecursively(false);
		}
	}

	public void OpenLevelAnimation()
	{
		btn_level.GetComponent<Animation>().wrapMode = WrapMode.Loop;
		btn_level.GetComponent<Animation>().Play();
		OpenBottomEffect(true);
	}

	public void CloseLevelAniamtion()
	{
		btn_level.GetComponent<Animation>().Stop();
		OpenBottomEffect(false);
	}

	public int GetLevelID()
	{
		return level_id;
	}

	public void SetLevelInfo(TUISecondaryLevelInfo m_info)
	{
		level_info = m_info;
	}

	public TUISecondaryLevelInfo GetLevelInfo()
	{
		return level_info;
	}

	public void SetDefaultIcon()
	{
		if (img_icon_normal == null && img_icon_press == null && img_icon_disable == null)
		{
			Debug.Log("error!");
		}
		else if (level_point_ex_type == LevelPointExType.Killing)
		{
			img_icon_normal.texture = texture_killing01;
			img_icon_press.texture = texture_killing01;
			img_icon_disable.texture = texture_killing01;
		}
		else if (level_point_ex_type == LevelPointExType.Defended)
		{
			img_icon_normal.texture = texture_defended01;
			img_icon_press.texture = texture_defended01;
			img_icon_disable.texture = texture_defended01;
		}
		else if (level_point_ex_type == LevelPointExType.Steal)
		{
			img_icon_normal.texture = texture_steal01;
			img_icon_press.texture = texture_steal01;
			img_icon_disable.texture = texture_steal01;
		}
		else if (level_point_ex_type == LevelPointExType.Survival)
		{
			img_icon_normal.texture = texture_survival01;
			img_icon_press.texture = texture_survival01;
			img_icon_disable.texture = texture_survival01;
		}
		else
		{
			Debug.Log("warning! level type no set!");
		}
	}

	public void SetPassIcon()
	{
		if (img_icon_normal == null && img_icon_press == null && img_icon_disable == null)
		{
			Debug.Log("error!");
		}
		else if (level_point_ex_type == LevelPointExType.Killing)
		{
			img_icon_normal.texture = texture_killing01;
			img_icon_press.texture = texture_killing01;
			img_icon_disable.texture = texture_killing01;
		}
		else if (level_point_ex_type == LevelPointExType.Defended)
		{
			img_icon_normal.texture = texture_defended01;
			img_icon_press.texture = texture_defended01;
			img_icon_disable.texture = texture_defended01;
		}
		else if (level_point_ex_type == LevelPointExType.Steal)
		{
			img_icon_normal.texture = texture_steal01;
			img_icon_press.texture = texture_steal01;
			img_icon_disable.texture = texture_steal01;
		}
		else if (level_point_ex_type == LevelPointExType.Survival)
		{
			img_icon_normal.texture = texture_survival01;
			img_icon_press.texture = texture_survival01;
			img_icon_disable.texture = texture_survival01;
		}
		else
		{
			Debug.Log("warning! level type no set!");
		}
	}

	public void OpenBottomEffect(bool m_bool)
	{
		if (!(img_bottom == null))
		{
			if (m_bool)
			{
				open_bottom_effect = true;
				img_bottom.texture = texture_choose;
			}
			else
			{
				open_bottom_effect = false;
				img_bottom.transform.localScale = bottom_normal_scale;
				img_bottom.texture = texture_unchoose;
			}
		}
	}

	public void UpdateBottomEffect(float delta_time)
	{
		if (open_bottom_effect && !(img_bottom == null))
		{
			effect_time += Time.deltaTime;
			float num = Mathf.Sin((float)Math.PI * 4f * effect_time) * 0.15f;
			img_bottom.transform.localScale = bottom_normal_scale + new Vector3(num, num, 0f);
		}
	}
}
