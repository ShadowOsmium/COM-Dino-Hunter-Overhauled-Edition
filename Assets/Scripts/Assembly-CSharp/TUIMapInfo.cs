using System.Collections.Generic;

public class TUIMapInfo
{
	public MapEnterType map_enter_type;

	public int main_level_id;

	public int main_level_id_next;

	public int secondary_level_id;

	public bool secondary_level_pass;

	public int[] level_goods_drop_list;

	public int main_level_camera_stop;

	public TUIMainLevelInfo level_info;

	public Dictionary<int, int> main_level_progress_list;

	public bool coop_drop;

	public TUIMapInfo(MapEnterType m_map_enter_type, int m_main_level_id, Dictionary<int, int> m_main_level_progress_list, int m_main_level_camera_stop = 0)
	{
		map_enter_type = m_map_enter_type;
		main_level_id = m_main_level_id;
		main_level_progress_list = m_main_level_progress_list;
		main_level_camera_stop = m_main_level_camera_stop;
	}

	public TUIMapInfo(MapEnterType m_map_enter_type, int m_main_level_id, int m_main_level_id_next, Dictionary<int, int> m_main_level_progress_list)
	{
		map_enter_type = m_map_enter_type;
		main_level_id = m_main_level_id;
		main_level_id_next = m_main_level_id_next;
		main_level_progress_list = m_main_level_progress_list;
	}

	public TUIMapInfo(MapEnterType m_map_enter_type, int m_main_level_id, Dictionary<int, int> m_main_level_progress_list, int[] m_level_goods_drop_list, bool m_coop_drop = false)
	{
		map_enter_type = m_map_enter_type;
		main_level_id = m_main_level_id;
		main_level_progress_list = m_main_level_progress_list;
		level_goods_drop_list = m_level_goods_drop_list;
		coop_drop = m_coop_drop;
	}

	public TUIMapInfo(TUIMainLevelInfo m_info)
	{
		level_info = m_info;
	}
}
