using System.Collections.Generic;
using TNetSdk;
using UnityEngine;

public class nmsg_player_move : nmsg_struct
{
	public float m_fTime;

	public List<Vector3> m_ltPath = new List<Vector3>();

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutFloat("time", m_fTime);
		sFSObject.PutInt("pointcount", m_ltPath.Count);
		for (int i = 0; i < m_ltPath.Count; i++)
		{
			sFSObject.PutFloatArray("point" + i, new float[3]
			{
				m_ltPath[i].x,
				m_ltPath[i].y,
				m_ltPath[i].z
			});
		}
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_ltPath.Clear();
		m_fTime = data.GetFloat("time");
		int @int = data.GetInt("pointcount");
		for (int i = 0; i < @int; i++)
		{
			float[] floatArray = data.GetFloatArray("point" + i);
			m_ltPath.Add(new Vector3(floatArray[0], floatArray[1], floatArray[2]));
		}
	}
}
