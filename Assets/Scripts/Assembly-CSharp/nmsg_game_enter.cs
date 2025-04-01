using System.Collections.Generic;
using TNetSdk;
using UnityEngine;

public class nmsg_game_enter : nmsg_struct
{
	public class player_pos
	{
		public int m_nId;

		public int m_nUID;

		public string m_sName = string.Empty;

		public Vector3 m_v3Pos;

		public Vector3 m_v3Dir;
	}

	public int m_nGameLevelID;

	public int m_nHunterLevelID;

	public List<player_pos> ltPlayerPos = new List<player_pos>();

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutInt("gamelevel", m_nGameLevelID);
		sFSObject.PutInt("hunterlevel", m_nHunterLevelID);
		sFSObject.PutInt("playercount", ltPlayerPos.Count);
		for (int i = 0; i < ltPlayerPos.Count; i++)
		{
			player_pos player_pos = ltPlayerPos[i];
			sFSObject.PutInt("playerid" + i, player_pos.m_nId);
			sFSObject.PutInt("playeruid" + i, player_pos.m_nUID);
			sFSObject.PutUtfString("playername" + i, player_pos.m_sName);
			sFSObject.PutFloatArray("playerpos" + i, new float[3]
			{
				player_pos.m_v3Pos.x,
				player_pos.m_v3Pos.y,
				player_pos.m_v3Pos.z
			});
			sFSObject.PutFloatArray("playerdir" + i, new float[3]
			{
				player_pos.m_v3Dir.x,
				player_pos.m_v3Dir.y,
				player_pos.m_v3Dir.z
			});
		}
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_nGameLevelID = data.GetInt("gamelevel");
		m_nHunterLevelID = data.GetInt("hunterlevel");
		int @int = data.GetInt("playercount");
		for (int i = 0; i < @int; i++)
		{
			player_pos player_pos = new player_pos();
			player_pos.m_nId = data.GetInt("playerid" + i);
			player_pos.m_nUID = data.GetInt("playeruid" + i);
			player_pos.m_sName = data.GetUtfString("playername" + i);
			float[] floatArray = data.GetFloatArray("playerpos" + i);
			player_pos.m_v3Pos = new Vector3(floatArray[0], floatArray[1], floatArray[2]);
			floatArray = data.GetFloatArray("playerdir" + i);
			player_pos.m_v3Dir = new Vector3(floatArray[0], floatArray[1], floatArray[2]);
			ltPlayerPos.Add(player_pos);
		}
	}
}
