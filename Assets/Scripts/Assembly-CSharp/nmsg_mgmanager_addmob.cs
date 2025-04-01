using TNetSdk;
using UnityEngine;

public class nmsg_mgmanager_addmob : nmsg_struct
{
	public int m_nMobID;

	public int m_nMobLvl;

	public int m_nMobUID;

	public int m_nWaveID;

	public int m_nSequence;

	public Vector3 m_v3Pos;

	public Vector3 m_v3Dir;

	public override SFSObject Pack()
	{
		SFSObject sFSObject = new SFSObject();
		sFSObject.PutInt("mobid", m_nMobID);
		sFSObject.PutInt("moblvl", m_nMobLvl);
		sFSObject.PutInt("mobuid", m_nMobUID);
		sFSObject.PutInt("waveid", m_nWaveID);
		sFSObject.PutInt("sequence", m_nSequence);
		sFSObject.PutFloatArray("pos", new float[3] { m_v3Pos.x, m_v3Pos.y, m_v3Pos.z });
		sFSObject.PutFloatArray("dir", new float[3] { m_v3Dir.x, m_v3Dir.y, m_v3Dir.z });
		return sFSObject;
	}

	public override void UnPack(SFSObject data)
	{
		m_nMobID = data.GetInt("mobid");
		m_nMobLvl = data.GetInt("moblvl");
		m_nMobUID = data.GetInt("mobuid");
		m_nWaveID = data.GetInt("waveid");
		m_nSequence = data.GetInt("sequence");
		float[] floatArray = data.GetFloatArray("pos");
		m_v3Pos = new Vector3(floatArray[0], floatArray[1], floatArray[2]);
		floatArray = data.GetFloatArray("dir");
		m_v3Dir = new Vector3(floatArray[0], floatArray[1], floatArray[2]);
	}
}
