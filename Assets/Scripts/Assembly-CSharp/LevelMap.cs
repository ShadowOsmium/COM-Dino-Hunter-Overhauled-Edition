using System.Collections.Generic;
using UnityEngine;

public class LevelMap : MonoBehaviour
{
	public LevelPoint[] level_point_list;

	public Transform[] mask_list;

	public Transform[] sign_list;

	public Transform left_border;

	public Transform right_border;

	private float map_width;

	protected float map_width_total = 1700f;

	protected Camera m_Camera;

	public GameObject prefab_pass_effect;

	public LevelPointCoop coop_point;

	private TUIMapInfo map_info;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void MoveScreen(float wparam, float lparam)
	{
		base.transform.localPosition = base.transform.localPosition + new Vector3(wparam, lparam);
		float num = base.transform.localPosition.x - 240f;
		float num2 = base.transform.localPosition.x + 240f + 656f;
		if (num > left_border.localPosition.x)
		{
			base.transform.localPosition = new Vector3(left_border.localPosition.x + 240f, base.transform.localPosition.y, base.transform.localPosition.z);
		}
		if (num2 < right_border.localPosition.x)
		{
			base.transform.localPosition = new Vector3(right_border.localPosition.x - 240f - 656f, base.transform.localPosition.y, base.transform.localPosition.z);
		}
	}

	public void SetScreenPos(Vector3 m_pos)
	{
		base.transform.localPosition = base.transform.localPosition - m_pos;
		float num = base.transform.localPosition.x - 240f;
		float num2 = base.transform.localPosition.x + 240f + 656f;
		if (num > left_border.localPosition.x)
		{
			base.transform.localPosition = new Vector3(left_border.localPosition.x + 240f, base.transform.localPosition.y, base.transform.localPosition.z);
		}
		if (num2 < right_border.localPosition.x)
		{
			base.transform.localPosition = new Vector3(right_border.localPosition.x - 240f - 656f, base.transform.localPosition.y, base.transform.localPosition.z);
		}
	}

	public void SetMapEnterInfo(TUIMapInfo m_map_info)
	{
		if (m_map_info == null)
		{
			Debug.Log("error!no map info");
			return;
		}
		map_info = m_map_info;
		MapEnterType map_enter_type = m_map_info.map_enter_type;
		int main_level_id = m_map_info.main_level_id;
		int main_level_id_next = m_map_info.main_level_id_next;
		int secondary_level_id = m_map_info.secondary_level_id;
		bool secondary_level_pass = m_map_info.secondary_level_pass;
		int[] level_goods_drop_list = m_map_info.level_goods_drop_list;
		int main_level_camera_stop = m_map_info.main_level_camera_stop;
		Dictionary<int, int> main_level_progress_list = m_map_info.main_level_progress_list;
		bool coop_drop = m_map_info.coop_drop;
		Vector3 m_move_pos = Vector3.zero;
		if (main_level_id < 1)
		{
			Debug.Log("error! you have no open level!!");
			return;
		}
		int num = FindLevelInArea(main_level_id, map_enter_type);
		int num2 = FindLevelInArea(main_level_id_next, map_enter_type);
		switch (map_enter_type)
		{
		case MapEnterType.Normal:
			if (level_point_list != null)
			{
				for (int l = 0; l < level_point_list.Length; l++)
				{
					LevelPoint levelPoint5 = level_point_list[l];
					if (!(levelPoint5 != null))
					{
						continue;
					}
					int levelID3 = levelPoint5.GetLevelID();
					int num6 = FindLevelInArea(levelID3, map_enter_type);
					if (num6 > num)
					{
						continue;
					}
					int passLevelText3 = 0;
					if (main_level_progress_list != null && main_level_progress_list.ContainsKey(levelID3))
					{
						passLevelText3 = main_level_progress_list[levelID3];
					}
					if (levelID3 < main_level_id)
					{
						levelPoint5.SetLevelPass();
						levelPoint5.ShowWayPoint(true);
						levelPoint5.ShowPassSign(true);
						levelPoint5.SetPassLevelText(passLevelText3);
						levelPoint5.ShowPassLevelText(true);
					}
					else if (levelID3 == main_level_id)
					{
						levelPoint5.SetLevelOpen();
						levelPoint5.OpenLevelAnimation(true);
						levelPoint5.ShowWayPoint(false);
						levelPoint5.ShowPassSign(false);
						if (FindSecondaryLevelIsLastLevel(secondary_level_id, main_level_id) && secondary_level_pass)
						{
							levelPoint5.ShowPassSign(true);
						}
						levelPoint5.SetPassLevelText(passLevelText3);
						levelPoint5.ShowPassLevelText(true);
					}
					else
					{
						levelPoint5.SetLevelDisable();
						levelPoint5.ShowWayPoint(false);
						levelPoint5.ShowPassSign(false);
						levelPoint5.SetPassLevelText(passLevelText3);
						levelPoint5.ShowPassLevelText(false);
					}
					if (main_level_camera_stop != 0)
					{
						if (levelID3 == main_level_camera_stop)
						{
							m_move_pos.x = levelPoint5.transform.position.x - base.transform.position.x;
						}
					}
					else if (levelID3 == main_level_id)
					{
						m_move_pos.x = levelPoint5.transform.position.x - base.transform.position.x;
					}
				}
			}
			else
			{
				Debug.Log("no info found!");
			}
			break;
		case MapEnterType.OpenNewLevel:
			if (level_point_list != null)
			{
				LevelPoint levelPoint2 = null;
				LevelPoint levelPoint3 = null;
				for (int k = 0; k < level_point_list.Length; k++)
				{
					LevelPoint levelPoint4 = level_point_list[k];
					if (!(levelPoint4 != null))
					{
						continue;
					}
					int levelID2 = levelPoint4.GetLevelID();
					int num5 = FindLevelInArea(levelID2, map_enter_type);
					if (num5 <= num2)
					{
						int passLevelText2 = 0;
						if (main_level_progress_list != null && main_level_progress_list.ContainsKey(levelID2))
						{
							passLevelText2 = main_level_progress_list[levelID2];
						}
						if (levelID2 < main_level_id)
						{
							levelPoint4.SetLevelPass();
							levelPoint4.ShowWayPoint(true);
							levelPoint4.ShowPassSign(true);
							levelPoint4.SetPassLevelText(passLevelText2);
							levelPoint4.ShowPassLevelText(true);
						}
						else if (levelID2 == main_level_id)
						{
							levelPoint4.SetLevelOpen();
							levelPoint4.ShowPassSign(false);
							levelPoint4.PlayPassSignAnimation(prefab_pass_effect);
							levelPoint4.SetPassLevelText(passLevelText2);
							levelPoint4.ShowPassLevelText(true);
							levelPoint2 = levelPoint4;
						}
						else if (levelID2 == main_level_id_next)
						{
							levelPoint4.SetLevelOpen();
							levelPoint4.ShowWayPoint(false);
							levelPoint4.ShowPassSign(false);
							levelPoint4.SetPassLevelText(passLevelText2);
							levelPoint4.ShowPassLevelText(true);
							levelPoint3 = levelPoint4;
							m_move_pos.x = levelPoint4.transform.position.x - base.transform.position.x;
						}
						else
						{
							levelPoint4.SetLevelDisable();
							levelPoint4.ShowWayPoint(false);
							levelPoint4.ShowPassSign(false);
							levelPoint4.SetPassLevelText(passLevelText2);
							levelPoint4.ShowPassLevelText(false);
						}
					}
				}
				if (levelPoint2 != null && levelPoint3 != null)
				{
					levelPoint2.ShowWayPoint(levelPoint3);
				}
			}
			else
			{
				Debug.Log("no info found!");
			}
			break;
		case MapEnterType.SearchGoods:
			if (level_point_list != null)
			{
				int level = 0;
				for (int i = 0; i < level_point_list.Length; i++)
				{
					LevelPoint levelPoint = level_point_list[i];
					if (!(levelPoint != null))
					{
						continue;
					}
					int levelID = levelPoint.GetLevelID();
					int num3 = FindLevelInArea(levelID, map_enter_type);
					if (num3 <= num)
					{
						int passLevelText = 0;
						if (main_level_progress_list != null && main_level_progress_list.ContainsKey(levelID))
						{
							passLevelText = main_level_progress_list[levelID];
						}
						if (levelID < main_level_id)
						{
							levelPoint.SetLevelPass();
							levelPoint.ShowWayPoint(true);
							levelPoint.ShowPassSign(true);
							levelPoint.SetPassLevelText(passLevelText);
							levelPoint.ShowPassLevelText(true);
						}
						else if (levelID == main_level_id)
						{
							levelPoint.SetLevelOpen();
							levelPoint.ShowWayPoint(false);
							levelPoint.ShowPassSign(false);
							if (FindSecondaryLevelIsLastLevel(secondary_level_id, main_level_id) && secondary_level_pass)
							{
								levelPoint.ShowPassSign(true);
							}
							levelPoint.SetPassLevelText(passLevelText);
							levelPoint.ShowPassLevelText(true);
						}
						else
						{
							levelPoint.SetLevelDisable();
							levelPoint.ShowWayPoint(false);
							levelPoint.ShowPassSign(false);
							levelPoint.SetPassLevelText(passLevelText);
							levelPoint.ShowPassLevelText(false);
						}
					}
					if (level_goods_drop_list == null)
					{
						continue;
					}
					for (int j = 0; j < level_goods_drop_list.Length; j++)
					{
						if (levelID == level_goods_drop_list[j])
						{
							if (num3 <= num)
							{
								levelPoint.ShowDropSign(true);
							}
							level = levelID;
						}
					}
				}
				int num4 = FindLevelInArea(level, map_enter_type);
				if (num4 != 0 && num4 > num)
				{
					Debug.Log("Find In Area:" + num4);
					ShowSign(num4, base.transform.position, ref m_move_pos);
				}
			}
			else
			{
				Debug.Log("error! no info found!");
			}
			break;
		}
		if (coop_point != null)
		{
			coop_point.ShowDropSign(coop_drop);
		}
		if (map_enter_type == MapEnterType.OpenNewLevel)
		{
			ShowMask(num2);
		}
		else
		{
			ShowMask(num);
		}
		SetScreenPos(m_move_pos);
	}

	public void SetMainLevelInfo(TUIMainLevelInfo m_main_level_info)
	{
		if (m_main_level_info == null)
		{
			Debug.Log("error! no level info!");
			return;
		}
		if (level_point_list == null)
		{
			Debug.Log("error! no level list!");
			return;
		}
		for (int i = 0; i < level_point_list.Length; i++)
		{
			LevelPoint levelPoint = level_point_list[i];
			if (levelPoint == null)
			{
				Debug.Log("error!");
				break;
			}
			int levelID = levelPoint.GetLevelID();
			if (levelID == m_main_level_info.id)
			{
				levelPoint.SetMainLevelInfo(m_main_level_info);
			}
		}
	}

	public int FindLevelInArea(int m_level, MapEnterType m_enter_type = MapEnterType.Normal)
	{
		int num = 0;
		if (m_level >= 1 && m_level <= 2)
		{
			return 1;
		}
		if (m_level >= 3 && m_level <= 5)
		{
			return 2;
		}
		if (m_level >= 6 && m_level <= 10)
		{
			return 3;
		}
		if (m_level >= 11 && m_level <= 15)
		{
			return 4;
		}
		if (m_level >= 16)
		{
			return 5;
		}
		if ((m_level >= 1001 && m_level <= 1010) || (m_level >= 2001 && m_level <= 2010))
		{
			return 1;
		}
		if ((m_level >= 3001 && m_level <= 3010) || (m_level >= 4001 && m_level <= 4010) || (m_level >= 5001 && m_level <= 5010))
		{
			return 2;
		}
		if ((m_level >= 6001 && m_level <= 6010) || (m_level >= 7001 && m_level <= 7010) || (m_level >= 8001 && m_level <= 8010) || (m_level >= 9001 && m_level <= 9010) || (m_level >= 10001 && m_level <= 10010))
		{
			return 3;
		}
		if ((m_level >= 11001 && m_level <= 11010) || (m_level >= 12001 && m_level <= 12010) || (m_level >= 13001 && m_level <= 13010) || (m_level >= 14001 && m_level <= 14010) || (m_level >= 15001 && m_level <= 15010))
		{
			return 4;
		}
		if ((m_level >= 16001 && m_level <= 16010) || (m_level >= 17001 && m_level <= 17010) || (m_level >= 18001 && m_level <= 18010) || (m_level >= 19001 && m_level <= 19010) || (m_level >= 20001 && m_level <= 20010))
		{
			return 5;
		}
		if (num == 0)
		{
			Debug.Log("Can't found level in any area!" + m_level);
		}
		return num;
	}

	public int FindSecondaryLevelInMainLevel(int m_level)
	{
		int result = 0;
		for (int i = 0; i < 20; i++)
		{
			int num = 1001 + 1000 * i;
			int num2 = 1010 + 1000 * i;
			if (m_level >= num && m_level <= num2)
			{
				result = i + 1;
			}
		}
		return result;
	}

	public bool FindSecondaryLevelIsLastLevel(int m_level, int m_main_level)
	{
		int num = 10 + 1000 * m_main_level;
		if (m_level == num)
		{
			return true;
		}
		return false;
	}

	public void ShowMask(int m_id)
	{
		if (mask_list == null || mask_list.Length < m_id - 1 || m_id < 1)
		{
			Debug.Log("error!");
			return;
		}
		if (m_id == 5)
		{
			for (int i = 0; i < mask_list.Length; i++)
			{
				mask_list[i].gameObject.SetActiveRecursively(false);
			}
			return;
		}
		for (int j = 0; j < mask_list.Length; j++)
		{
			if (j == m_id - 1)
			{
				mask_list[j].gameObject.SetActiveRecursively(true);
			}
			else
			{
				mask_list[j].gameObject.SetActiveRecursively(false);
			}
		}
	}

	public void ShowSign(int m_id, Vector3 m_pos, ref Vector3 m_move_pos)
	{
		if (sign_list == null || sign_list.Length < m_id || m_id < 1)
		{
			Debug.Log("error!");
		}
		else if (m_id >= 1 && m_id <= 5)
		{
			for (int i = 0; i < sign_list.Length; i++)
			{
				if (i == m_id - 1)
				{
					sign_list[i].gameObject.SetActiveRecursively(true);
					m_move_pos.x = sign_list[i].position.x - m_pos.x;
					LevelMapSign component = sign_list[i].GetComponent<LevelMapSign>();
					if (component != null)
					{
						component.PlaySignAnimation();
					}
				}
				else
				{
					sign_list[i].gameObject.SetActiveRecursively(false);
				}
			}
		}
		else
		{
			for (int j = 0; j < sign_list.Length; j++)
			{
				sign_list[j].gameObject.SetActiveRecursively(false);
			}
		}
	}

	public void ShowLevelCoop(bool m_show)
	{
		if (coop_point != null)
		{
			coop_point.gameObject.SetActiveRecursively(m_show);
		}
	}
}
