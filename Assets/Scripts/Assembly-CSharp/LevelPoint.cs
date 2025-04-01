using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPoint : MonoBehaviour
{
	public enum LevelPointState
	{
		Passed,
		Open,
		Disable,
		Hide
	}

	public int level_id;

	public List<GameObject> way_points_list;

	public TUIButtonClick btn_level;

	public bool open_aniamtion;

	public bool open_way_points_show;

	public LevelPointBottom img_bottom;

	public TUIMeshSprite img_btn_normal;

	public TUIMeshSprite img_btn_press;

	public TUIMeshSprite img_btn_disable;

	public GameObject finished_sign;

	public GameObject drop_sign;

	public TUILabel label_text;

	private LevelPointState level_point_state = LevelPointState.Hide;

	private float m_time;

	private int way_points_count;

	private int way_points_index;

	private float time_gap = 0.14f;

	private TUIMainLevelInfo main_level_info;

	private LevelPoint next_level;

	private string texture_btn_normal = "zhurenwu_1";

	private string texture_btn_hui = "zhurenwu_1hui";

	private void Awake()
	{
		way_points_count = way_points_list.Count;
		if (open_aniamtion)
		{
			open_aniamtion = false;
			btn_level.GetComponent<Animation>().wrapMode = WrapMode.Loop;
			btn_level.GetComponent<Animation>().Play();
		}
		SetLevelPointState(LevelPointState.Hide);
		ShowWayPoint(false);
		ShowDropSign(false);
		ShowPassLevelText(false);
	}

	private void Start()
	{
	}

	private void Update()
	{
		UpdateWayPointAni(Time.deltaTime);
	}

	public void SetLevelPointState(LevelPointState m_level_point_state, bool change_level_point_ex = true)
	{
		level_point_state = m_level_point_state;
		if (level_point_state == LevelPointState.Passed)
		{
			btn_level.gameObject.SetActiveRecursively(true);
			btn_level.Reset();
			img_bottom.OpenChoose(false);
			if (img_btn_normal != null)
			{
				img_btn_normal.texture = texture_btn_normal;
			}
			if (img_btn_press != null)
			{
				img_btn_press.texture = texture_btn_normal;
			}
			if (finished_sign != null)
			{
				finished_sign.SetActiveRecursively(true);
			}
		}
		else if (level_point_state == LevelPointState.Open)
		{
			btn_level.gameObject.SetActiveRecursively(true);
			btn_level.Reset();
			img_bottom.OpenChoose(false);
			if (img_btn_normal != null)
			{
				img_btn_normal.texture = texture_btn_normal;
			}
			if (img_btn_press != null)
			{
				img_btn_press.texture = texture_btn_normal;
			}
			if (finished_sign != null)
			{
				finished_sign.SetActiveRecursively(false);
			}
		}
		else if (level_point_state == LevelPointState.Disable)
		{
			btn_level.gameObject.SetActiveRecursively(true);
			btn_level.Disable(true);
			img_bottom.OpenChoose(false);
			if (img_btn_normal != null)
			{
				img_btn_normal.texture = texture_btn_hui;
			}
			if (img_btn_press != null)
			{
				img_btn_press.texture = texture_btn_hui;
			}
			if (finished_sign != null)
			{
				finished_sign.SetActiveRecursively(false);
			}
		}
		else if (level_point_state == LevelPointState.Hide)
		{
			if (btn_level != null)
			{
				btn_level.Disable(true);
				btn_level.gameObject.SetActiveRecursively(false);
			}
			if (img_bottom != null)
			{
				img_bottom.Hide();
			}
			if (img_btn_normal != null)
			{
				img_btn_normal.texture = texture_btn_hui;
			}
			if (img_btn_press != null)
			{
				img_btn_press.texture = texture_btn_hui;
			}
			if (finished_sign != null)
			{
				finished_sign.SetActiveRecursively(false);
			}
		}
	}

	public void UpdateWayPointAni(float delta_time)
	{
		if (!open_way_points_show)
		{
			return;
		}
		m_time += delta_time;
		if (!(m_time >= time_gap))
		{
			return;
		}
		m_time = 0f;
		way_points_list[way_points_index].SetActiveRecursively(true);
		way_points_index++;
		if (way_points_index >= way_points_count)
		{
			way_points_index = 0;
			open_way_points_show = false;
			OpenLevelAnimation(false);
			if (next_level != null)
			{
				next_level.SetLevelPointState(LevelPointState.Open, false);
				next_level.OpenLevelAnimation(true);
			}
		}
	}

	public int GetLevelID()
	{
		return level_id;
	}

	public void SetMainLevelInfo(TUIMainLevelInfo m_info)
	{
		main_level_info = m_info;
	}

	public TUIMainLevelInfo GetLevelInfo()
	{
		return main_level_info;
	}

	public void OpenLevelAnimation(bool m_open)
	{
		if (btn_level == null || img_bottom == null)
		{
			Debug.Log("error!");
		}
		else if (m_open)
		{
			btn_level.GetComponent<Animation>().wrapMode = WrapMode.Loop;
			btn_level.GetComponent<Animation>().Play();
			img_bottom.OpenChoose(true);
		}
		else
		{
			btn_level.GetComponent<Animation>().Stop();
			img_bottom.OpenChoose(false);
		}
	}

	public void ShowWayPoint(bool m_show)
	{
		if (way_points_list == null)
		{
			Debug.Log("error! no level way!");
			return;
		}
		for (int i = 0; i < way_points_list.Count; i++)
		{
			way_points_list[i].SetActiveRecursively(m_show);
		}
	}

	public void ShowWayPoint(LevelPoint m_next_level)
	{
		if (m_next_level == null)
		{
			Debug.Log("error! no next level info!");
			return;
		}
		open_way_points_show = true;
		next_level = m_next_level;
		ShowWayPoint(false);
		OpenLevelAnimation(true);
	}

	public void ShowPassSign(bool m_show)
	{
		if (finished_sign == null)
		{
			Debug.Log("error!");
		}
		else
		{
			finished_sign.SetActiveRecursively(m_show);
		}
	}

	public void ShowDropSign(bool m_show)
	{
		if (drop_sign == null)
		{
			Debug.Log("error!");
		}
		else
		{
			drop_sign.SetActiveRecursively(m_show);
		}
	}

	public void PlayPassSignAnimation(GameObject m_prefab_effect)
	{
		if (btn_level == null || m_prefab_effect == null)
		{
			Debug.Log("error!");
		}
		else
		{
			StartCoroutine(CreatePassSignAnimation(m_prefab_effect));
		}
	}

	public void SetPassLevelText(int m_count)
	{
		if (label_text == null)
		{
			Debug.Log("error!");
		}
		else
		{
			label_text.Text = m_count + "/10";
		}
	}

	public void ShowPassLevelText(bool m_show)
	{
		if (label_text == null)
		{
			Debug.Log("error!");
		}
		else
		{
			label_text.gameObject.SetActiveRecursively(m_show);
		}
	}

	public void SetLevelHide()
	{
		SetLevelPointState(LevelPointState.Hide);
	}

	public void SetLevelDisable()
	{
		SetLevelPointState(LevelPointState.Disable);
	}

	public void SetLevelOpen()
	{
		SetLevelPointState(LevelPointState.Open);
	}

	public void SetLevelPass()
	{
		SetLevelPointState(LevelPointState.Passed);
	}

	private IEnumerator CreatePassSignAnimation(GameObject m_prefab_effect)
	{
		yield return new WaitForSeconds(1f);
		GameObject m_effect = (GameObject)Object.Instantiate(m_prefab_effect);
		if (m_effect != null)
		{
			m_effect.transform.parent = btn_level.transform;
			m_effect.transform.localPosition = new Vector3(0f, 0f, -0.2f);
			Object.Destroy(m_effect, 0.3f);
			StartCoroutine(ShowPassSignAffterAniamtion(0.25f));
		}
	}

	private IEnumerator ShowPassSignAffterAniamtion(float m_time)
	{
		yield return new WaitForSeconds(m_time);
		ShowPassSign(true);
	}
}
